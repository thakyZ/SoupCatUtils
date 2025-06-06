using System.IO;
using System.Text.RegularExpressions;

using Dalamud.Interface.GameFonts;
using Dalamud.Interface.ManagedFontAtlas;
using Dalamud.Plugin.Services;

using ImGuiNET;

using Dalamud.Interface.Utility;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

public class FontContainer : ModuleBase {
  private readonly Dictionary<string, IFontHandle> _imGuiFonts = [];
  private string[] _fontList;

  public const string DALAMUD_FONT_KEY = "Dalamud Font";
  public static readonly List<string> _defaultFontKeys = ["Expressway_24", "Expressway_20", "Expressway_16"];
  public static string DefaultBigFontKey => _defaultFontKeys[0];
  public static string DefaultMediumFontKey => _defaultFontKeys[1];
  public static string DefaultSmallFontKey => _defaultFontKeys[2];

  public FontContainer() : base(false) {
    _fontList = [DALAMUD_FONT_KEY];
    this.BuildFonts(GetFontsFromEnum());
  }

  public IEnumerable<FontData> GetFontsFromEnum() {
    List<FontData> output = [];
    foreach (int i in Enum.GetValues(typeof(GameFontFamilyAndSize))) {
      if (i == 0) {
        continue;
      }
      if (i >= Enum.GetValues(typeof(GameFontFamilyAndSize)).Length) {
        continue;
      }
      var nameProcess = ((GameFontFamilyAndSizeProxy)i).ToDescriptionString()?.Split("_");
      if (nameProcess is null) {
        continue;
      }
      output.Add(new FontData(nameProcess[0], "", float.Parse(nameProcess[1]), false, false));
    }
    if (output.Count == 0) Svc.Log.Warning("Fonts from enum returned empty.");
    return output;
  }

  private void BuildFonts(IEnumerable<FontData> fontData) {
    string fontDir = GetUserFontPath();
    if (string.IsNullOrEmpty(fontDir)) {
      Svc.Log.Warning("Font directory is not found.");
      return;
    }

    this.DisposeFontHandles();

    foreach (FontData font in fontData) {
      string path = string.IsNullOrEmpty(font.Path) ? $"{fontDir}{font.Name}.ttf" : font.Path;
      if (!File.Exists(path)) {
        try {
          GameFontFamily fontFamily = font.GetFontFamily();
          if (fontFamily == GameFontFamily.Undefined) {
            continue;
          }
          IFontHandle fontHandle = Svc.PluginInterface.UiBuilder.FontAtlas.NewGameFontHandle(new GameFontStyle {
            FamilyAndSize = GameFontStyle.GetRecommendedFamilyAndSize(fontFamily, font.Size),
            SizePt = font.Size,
          });
          _imGuiFonts.Add(GetFontKey(font), fontHandle);
        } catch (Exception ex) {
          Svc.Log.Error(ex, $"Failed to load font with name [{font.Name}]!");
        }
      } else {
        try {
          IFontHandle fontHandle = Svc.PluginInterface.UiBuilder.FontAtlas.NewDelegateFontHandle(
            e => e.OnPreBuild(
              tk => tk.AddFontFromFile(
                path,
                new SafeFontConfig {
                  SizePx = font.Size,
                  GlyphRanges = this.GetCharacterRanges(font.Chinese, font.Korean),
                }
              )
            )
          );
          _imGuiFonts.Add(GetFontKey(font), fontHandle);
        } catch (Exception ex) {
          Svc.Log.Error($"Failed to load font from path [{path}]!");
          Svc.Log.Error(ex.ToString());
        }
      }
    }

    _fontList = [DALAMUD_FONT_KEY, .._imGuiFonts.Keys];
  }

  public static FontData[] GetDefaultFontData() {
    FontData[] defaults = new FontData[_defaultFontKeys.Count];
    for (int i = 0; i < _defaultFontKeys.Count; i++) {
      string[] splits = _defaultFontKeys[i].Split("_", StringSplitOptions.RemoveEmptyEntries);
      if (splits.Length == 2 && int.TryParse(splits[1], out int size)) {
        defaults[i] = new(splits[0], $"{GetUserFontPath()}{splits[0]}.ttf", size, false, false);
      }
    }

    return defaults;
  }

  public void UpdateFonts(IEnumerable<FontData> fonts) {
    this.BuildFonts(fonts);
  }

  private void DisposeFontHandles() {
    foreach ((string _, IFontHandle value) in _imGuiFonts) {
      value.Dispose();
    }

    _imGuiFonts.Clear();
  }

  private unsafe ushort[]? GetCharacterRanges(bool chinese, bool korean) {
    if (!chinese && !korean) {
      return null;
    }

    ImGuiIOPtr io = ImGui.GetIO();
    using (ImGuiHelpers.NewFontGlyphRangeBuilderPtrScoped(out ImFontGlyphRangesBuilderPtr builder)) {
      if (chinese) {
        // GetGlyphRangesChineseFull() includes Default + Hiragana, Katakana, Half-Width, Selection of 1946 Ideographs
        // https://skia.googlesource.com/external/github.com/ocornut/imgui/+/v1.53/extra_fonts/README.txt
        builder.AddRanges(io.Fonts.GetGlyphRangesChineseFull());
      }

      if (korean) {
        builder.AddRanges(io.Fonts.GetGlyphRangesKorean());
      }

      return builder.BuildRangesToArray();
    }
  }

  public static int GetFontIndex(string fontKey) {
    foreach ((int i, string font) in System.Modules.Get<FontContainer>()?._fontList.GetIndexEnumerator() ?? []) {
      if (font.Equals(fontKey)) {
        return i;
      }
    }

    return 0;
  }

  public static bool ValidateFont(string[] fontOptions, int fontId, string fontKey) {
    return fontId < fontOptions.Length && fontOptions[fontId].Equals(fontKey);
  }

  public static FontScope PushFont(string fontKey) {
    if (!string.IsNullOrEmpty(fontKey) && System.Modules.Get<FontContainer>()?._imGuiFonts.TryGetValue(fontKey, out IFontHandle? fontHandle) == true && fontHandle is not null) {
      return new FontScope(fontHandle);
    }

    return new FontScope();
  }

  public static string[] GetFontList() {
    return System.Modules.Get<FontContainer>()?._fontList ?? [];
  }

  // cSpell:ignore _cnjp;
  public static string GetFontKey(FontData font) {
      string key = $"{font.Name}_{font.Size}";
      key += font.Chinese ? "_cnjp" : string.Empty;
      key += font.Korean ? "_kr" : string.Empty;
      return key;
  }

  public static void CopyPluginFontsToUserPath() {
    string pluginFontPath = GetPluginFontPath();
    string userFontPath = GetUserFontPath();

    if (string.IsNullOrEmpty(pluginFontPath) || string.IsNullOrEmpty(userFontPath)) {
      return;
    }

    try {
      Directory.CreateDirectory(userFontPath);
    } catch (Exception ex) {
      Svc.Log.Warning($"Failed to create User Font Directory {ex}");
    }

    if (!Directory.Exists(userFontPath)) {
      return;
    }

    string[] pluginFonts;
    try {
      pluginFonts = Directory.GetFiles(pluginFontPath).Where(x => x.EndsWith(".ttf") || x.EndsWith(".otf")).ToArray();
    } catch {
      pluginFonts = [];
    }

    foreach (string fontFileNames in pluginFonts) {
      try {
        if (!string.IsNullOrEmpty(fontFileNames)) {
          string fileName = fontFileNames.Replace(pluginFontPath, string.Empty);
          string copyPath = Path.Combine(userFontPath, fileName);
          if (!File.Exists(copyPath)) {
            File.Copy(fontFileNames, copyPath, false);
          }
        }
      }
      catch (Exception ex) {
        Svc.Log.Warning($"Failed to copy font {fontFileNames} to User Font Directory: {ex}");
      }
    }
  }

  public static string GetPluginFontPath() {
    string? path = Svc.PluginInterface.AssemblyLocation.DirectoryName;
    if (path is not null) {
      return Path.Join(path, "Media\\Fonts\\");
    }

    return string.Empty;
  }

  public static string GetUserFontPath() {
    return Path.Join(Svc.PluginInterface.GetPluginConfigDirectory(), "\\Fonts\\");
  }

  public static string GetFontName(string? fontPath, string fontFile)
  {
    return fontFile.Replace(fontPath ?? string.Empty, string.Empty)
      .Replace(".otf", string.Empty, StringComparison.OrdinalIgnoreCase)
      .Replace(".ttf", string.Empty, StringComparison.OrdinalIgnoreCase);
  }

  public static string[] GetFontPaths(string? path) {
    if (string.IsNullOrEmpty(path)) {
      return [];
    }

    string[] fonts;
    try {
      fonts = Directory.GetFiles(path).Where(x => x.EndsWith(".ttf") || x.EndsWith(".otf")).ToArray();
    } catch {
      fonts = [];
    }

    return fonts;
  }

  internal override void DisposeManaged() {
    this.DisposeFontHandles();
  }
}
