using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

using Dalamud.Interface;
using Dalamud.Interface.GameFonts;
using Dalamud.Logging;

using ImGuiNET;

using Lumina.Excel.GeneratedSheets;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

internal enum GameFontFamilyAndSizeProxy {
  /// <summary>
  /// Placeholder meaning unused.
  /// </summary>
  [Description("Placeholder meaning unused.")]
  Undefined,

  /// <summary>
  /// <para>AXIS (9.6pt)</para>
  /// <para>Contains Japanese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("AXIS (9.6pt)")]
  Axis96,

  /// <summary>
  /// <para>AXIS (12pt)</para>
  /// <para>Contains Japanese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("AXIS (12pt)")]
  Axis12,

  /// <summary>
  /// <para>AXIS (14pt)</para>
  /// <para>Contains Japanese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("AXIS (14pt)")]
  Axis14,

  /// <summary>
  /// <para>AXIS (18pt)</para>
  /// <para>Contains Japanese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("AXIS (18pt)")]
  Axis18,

  /// <summary>
  /// <para>AXIS (36pt)</para>
  /// <para>Contains Japanese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("AXIS (36pt)")]
  Axis36,

  /// <summary>
  /// <para>CHNAXIS (120pt)</para>
  /// <para>Contains Chinese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("CHNAXIS (120pt)")]
  ChnAxis120,

  /// <summary>
  /// <para>CHNAXIS (140pt)</para>
  /// <para>Contains Chinese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("CHNAXIS (140pt)")]
  ChnAxis140,

  /// <summary>
  /// <para>CHNAXIS (180pt)</para>
  /// <para>Contains Chinese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("CHNAXIS (180pt)")]
  ChnAxis180,

  /// <summary>
  /// <para>Jupiter (16pt)</para>
  /// <para>Serif font. Contains mostly ASCII range. Used in game for job names.</para>
  /// </summary>
  [Description("Jupiter (16pt)")]
  Jupiter16,

  /// <summary>
  /// <para>Jupiter (20pt)</para>
  /// <para>Serif font. Contains mostly ASCII range. Used in game for job names.</para>
  /// </summary>
  [Description("Jupiter (20pt)")]
  Jupiter20,

  /// <summary>
  /// <para>Jupiter (23pt)</para>
  /// <para>Serif font. Contains mostly ASCII range. Used in game for job names.</para>
  /// </summary>
  [Description("Jupiter (23pt)")]
  Jupiter23,

  /// <summary>
  /// <para>Jupiter (45pt)</para>
  /// <para>Serif font. Contains mostly numbers. Used in game for flying texts.</para>
  /// </summary>
  [Description("Jupiter (45pt)")]
  Jupiter45,

  /// <summary>
  /// <para>Jupiter (46pt)</para>
  /// <para>Serif font. Contains mostly ASCII range. Used in game for job names.</para>
  /// </summary>
  [Description("Jupiter (46pt)")]
  Jupiter46,

  /// <summary>
  /// <para>Jupiter (90pt)</para>
  /// <para>Serif font. Contains mostly numbers. Used in game for flying texts.</para>
  /// </summary>
  [Description("Jupiter (90pt)")]
  Jupiter90,

  /// <summary>
  /// <para>Meidinger (16pt)</para>
  /// <para>Horizontally wide. Contains mostly numbers. Used in game for HP/MP/IL stuff.</para>
  /// </summary>
  [Description("Meidinger (16pt)")]
  Meidinger16,

  /// <summary>
  /// <para>Meidinger (20pt)</para>
  /// <para>Horizontally wide. Contains mostly numbers. Used in game for HP/MP/IL stuff.</para>
  /// </summary>
  [Description("Meidinger (20pt)")]
  Meidinger20,

  /// <summary>
  /// <para>Meidinger (40pt)</para>
  /// <para>Horizontally wide. Contains mostly numbers. Used in game for HP/MP/IL stuff.</para>
  /// </summary>
  [Description("Meidinger (40pt)")]
  Meidinger40,

  /// <summary>
  /// <para>MiedingerMid (10pt)</para>
  /// <para>Horizontally wide. Contains mostly ASCII range.</para>
  /// </summary>
  [Description("MiedingerMid (10pt)")]
  MiedingerMid10,

  /// <summary>
  /// <para>MiedingerMid (12pt)</para>
  /// <para>Horizontally wide. Contains mostly ASCII range.</para>
  /// </summary>
  [Description("MiedingerMid (12pt)")]
  MiedingerMid12,

  /// <summary>
  /// <para>MiedingerMid (14pt)</para>
  /// <para>Horizontally wide. Contains mostly ASCII range.</para>
  /// </summary>
  [Description("MiedingerMid (14pt)")]
  MiedingerMid14,

  /// <summary>
  /// <para>MiedingerMid (18pt)</para>
  /// <para>Horizontally wide. Contains mostly ASCII range.</para>
  /// </summary>
  [Description("MiedingerMid (18pt)")]
  MiedingerMid18,

  /// <summary>
  /// <para>MiedingerMid (36pt)</para>
  /// <para>Horizontally wide. Contains mostly ASCII range.</para>
  /// </summary>
  [Description("MiedingerMid (36pt)")]
  MiedingerMid36,

  /// <summary>
  /// <para>TrumpGothic (18.4pt)</para>
  /// <para>Horizontally narrow. Contains mostly ASCII range. Used for addon titles.</para>
  /// </summary>
  [Description("TrumpGothic (18.4pt)")]
  TrumpGothic184,

  /// <summary>
  /// <para>TrumpGothic (23pt)</para>
  /// <para>Horizontally narrow. Contains mostly ASCII range. Used for addon titles.</para>
  /// </summary>
  [Description("TrumpGothic (23pt)")]
  TrumpGothic23,

  /// <summary>
  /// <para>TrumpGothic (34pt)</para>
  /// <para>Horizontally narrow. Contains mostly ASCII range. Used for addon titles.</para>
  /// </summary>
  [Description("TrumpGothic (34pt)")]
  TrumpGothic34,

  /// <summary>
  /// <para>TrumpGothic (688pt)</para>
  /// <para>Horizontally narrow. Contains mostly ASCII range. Used for ad don titles.</para>
  /// </summary>
  [Description("TrumpGothic (688pt)")]
  TrumpGothic68,
}
internal static class Extensions {
  public static string ToDescriptionString(this GameFontFamilyAndSizeProxy val) {
    if (val.GetType().GetField(val.ToString())!.GetCustomAttributes(typeof(DescriptionAttribute), false) is not DescriptionAttribute[] attributes) {
      return string.Empty;
    } else if (attributes.Length > 0) {
      return attributes[0].Description;
    } else {
      return string.Empty;
    }
  }
  public static T GetEnumValueByDescription<T>(this string val) where T : Enum {
    FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
    var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
      (f, a) => new { Field = f, Att = a })
      .SingleOrDefault(a =>((DescriptionAttribute)a.Att).Description == val)!;
    return field is null || field.Field.GetRawConstantValue() is null ? default! : (T)field.Field.GetRawConstantValue()!;
  }
  public static GameFontFamilyAndSize UnProxy(this GameFontFamilyAndSizeProxy val) {
    var toInt = (int)val;
    var nonProxy = (GameFontFamilyAndSize)toInt;
    return nonProxy;
  }
  public static GameFontFamilyAndSize GameFontFamilyAndSizeUnProxy(this string val) {
    FieldInfo[] fields = typeof(GameFontFamilyAndSizeProxy).GetFields(BindingFlags.Public | BindingFlags.Static);
    var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
      (f, a) => new { Field = f, Att = a })
      .SingleOrDefault(a =>((DescriptionAttribute)a.Att).Description == val)!;
    var isProxy = field is null || field.Field.GetRawConstantValue() is null ? default : (GameFontFamilyAndSizeProxy)field.Field.GetRawConstantValue()!;
    var toInt = (int)isProxy;
    var nonProxy = (GameFontFamilyAndSize)toInt;
    return nonProxy;
  }
}

public partial class FontContainer : IDisposable {
  private readonly List<(string, string)> fontsByName = new();
  private Dictionary<string, GameFontHandle> LoadedFonts = new();

  [GeneratedRegex("[\\d.]{1,3}pt$")]
  private static partial Regex SizeStringRegex();

  public FontContainer() {
    foreach (int i in Enum.GetValues(typeof(GameFontFamilyAndSize))) {
      if (i == 0) {
        continue;
      }
      var nameProcess = ((GameFontFamilyAndSizeProxy)i).ToDescriptionString().Replace("(",string.Empty).Replace(")",string.Empty).Split(" ");
      fontsByName.Add((nameProcess[0], nameProcess[1]));
    }
  }

  private static string ParseSizeValue<T>(T size) {
    if (size is null) {
      return string.Empty;
    }
    if (size is int || size is float || size is double) {
      return string.Format("{0}pt", size.ToString());
    } else if (size is string) {
      if (SizeStringRegex().IsMatch(size.ToString()!)) {
        return size.ToString()!;
      } else {
        return string.Format("{0}pt", size.ToString());
      }
    }
    return string.Empty;
  }

  private static string UnparseSizeValue(string name, string size) {
    return string.Format("{0} ({1})", name, size);
  }

  private bool CheckSizeExists(string fontName, string size) {
    return fontsByName.Exists(x => x.Item1 == fontName && x.Item2 == size);
  }


  private GameFontHandle LoadFont(string fontName, string size) {
    string unparsedSize = UnparseSizeValue(fontName, size);
    if (LoadedFonts.TryGetValue(unparsedSize, out var value)) {
      return value!;
    }
    LoadedFonts.Add(unparsedSize,
      Services.PluginInterface.UiBuilder.GetGameFontHandle(
        new GameFontStyle(unparsedSize.GameFontFamilyAndSizeUnProxy())));
    return LoadFont(fontName, size);
  }

  public ImFontPtr GetFont(string fontName, int size) {
    var parsedSize = ParseSizeValue(size);
    if (CheckSizeExists(fontName, parsedSize)) {
      return LoadFont(fontName, parsedSize).ImFont;
    }
    return UiBuilder.DefaultFont;
  }

  public ImFontPtr GetFont(string fontName, float size) {
    var parsedSize = ParseSizeValue(size);
    if (CheckSizeExists(fontName, parsedSize)) {
      return LoadFont(fontName, parsedSize).ImFont;
    }
    return UiBuilder.DefaultFont;
  }

  public ImFontPtr GetFont(string fontName, double size) {
    var parsedSize = ParseSizeValue(size);
    if (CheckSizeExists(fontName, parsedSize)) {
      return LoadFont(fontName, parsedSize).ImFont;
    }
    return UiBuilder.DefaultFont;
  }

  public ImFontPtr GetFont(string fontName, string size) {
    var parsedSize = ParseSizeValue(size);
    if (CheckSizeExists(fontName, parsedSize)) {
      return LoadFont(fontName, parsedSize).ImFont;
    }
    return UiBuilder.DefaultFont;
  }

  public static ImFontPtr GetFont() {
    return UiBuilder.DefaultFont;
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed;

  protected virtual void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      foreach (var name in LoadedFonts.Keys) {
        PluginLog.Debug($"Disposing of font: '{name}'");
        LoadedFonts[name].Dispose();
      }
      _isDisposed = true;
    }
  }
}
