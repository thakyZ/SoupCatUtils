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

  protected override void DisposeImpl() {
      Services.FanDanceIV_Module.Dispose();
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
