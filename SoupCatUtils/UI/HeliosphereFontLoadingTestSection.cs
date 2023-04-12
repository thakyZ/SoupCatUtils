using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dalamud.Interface;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;
internal class HeliosphereFontLoadingTestSection : SectionBase, IDisposable {
  public new string Name { get; set; } = "HeliosphereFontLoadingTestSection##SoupCatUtils";
  internal static HeliosphereGameFont GameFont { get; private set; }

  public HeliosphereFontLoadingTestSection() {
    GameFont = new HeliosphereGameFont(Services.PluginInstance);
  }

  /*****************************************************************
   * Code borrowed from https://github.com/heliosphere-xiv/plugin/ *
   *****************************************************************/
  internal static void TextUnformattedSize(string text, uint size) {
    var font = size == 0 ? null : GameFont[size];
    if (font != null) {
      ImGui.PushFont(font.ImFont);
    }

    ImGui.TextUnformatted(text);

    if (font != null) {
      ImGui.PopFont();
    }
  }

  internal const int TitleSize = 36;

  internal static void TextUnformattedCentred(string text, uint size = 0) {
    var widthAvail = ImGui.GetContentRegionAvail().X;
    var titleFont = size == 0 ? null : GameFont[size];
    if (titleFont != null) {
      ImGui.PushFont(titleFont.ImFont);
    }

    var textSize = ImGui.CalcTextSize(text);
    if (textSize.X < widthAvail) {
      var cursor = ImGui.GetCursorPos();
      ImGui.SetCursorPos(cursor with {
        X = widthAvail / 2 - textSize.X / 2,
      });
    }

    ImGui.TextUnformatted(text);

    if (titleFont != null) {
      ImGui.PopFont();
    }
  }

  internal static void TextUnformattedCentredSoupCat(string text, uint size = 0) {
    var widthAvail = ImGui.GetContentRegionAvail().X;
    var titleFont = Services.FontContainer.GetFont("AXIS", size);
    ImGui.PushFont(titleFont);

    var textSize = ImGui.CalcTextSize(text);
    if (textSize.X < widthAvail) {
      var cursor = ImGui.GetCursorPos();
      ImGui.SetCursorPos(cursor with {
        X = widthAvail / 2 - textSize.X / 2,
      });
    }

    ImGui.TextUnformatted(text);

    ImGui.PopFont();
  }

  protected override string NameImpl {
    get { return Name; }
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed = false;

  protected virtual void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      _isDisposed = true;
    }
  }

  bool clicked1 = false;
  bool clicked2 = false;

  public override void Draw() {
    CreateTitle("Heliosphere Font Loading Test");

    if (ImGui.Button("Run Helio Test##Heliosphere Font Loading Test")) {
      clicked1 = true;
    }

    if (clicked1) {
      Task.Run(async () => { await Task.Delay(1000); clicked1 = false; });
      TextUnformattedCentred("Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur", TitleSize);
    } else {
      ImGui.Text("Stars are cooked!");
    }

    if (ImGui.Button("Run Soup Cat Test##Heliosphere Font Loading Test")) {
      clicked2 = true;
    }

    if (clicked2) {
      Task.Run(async () => { await Task.Delay(1000); clicked2 = false; });
      TextUnformattedCentredSoupCat("Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur", TitleSize);
    } else {
      ImGui.Text("Soup is cooked!");
    }
  }
}
