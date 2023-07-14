using System;
using System.Numerics;

using ECommons;
using ECommons.Schedulers;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;
using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;
public class SPLSection : SectionBase, IDisposable {
  public new string Name { get; set; } = "SPL##SoupCatUtils";
  protected override string NameImplementation {
    get { return Name; }
  }

  public SPLSection() {
  }

  public new void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed = false;

  protected virtual void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      Services.FanDanceIV_Module.Dispose();
      _isDisposed = true;
    }
  }

  public override void Draw() {
    CreateTitle("SPL");

    var enableSPL_FDIV = Services.PluginConfig.SplatoonFanDanceIV;

    if (ImGui.Checkbox("FanDance IV", ref enableSPL_FDIV)) {
      Services.PluginConfig.SplatoonFanDanceIV = enableSPL_FDIV;
    }

    if (Services.PluginConfig.SplatoonFanDanceIV) {
      if (ImGui.BeginChild("DebugFanDanceIV#SPL##SoupCatUtils", new Vector2(270, 48), false)) {
        ImGui.TextUnformatted($"{Services.FanDanceIV_DebugState}");
        ImGui.EndChild();
      }
    }
  }
}
