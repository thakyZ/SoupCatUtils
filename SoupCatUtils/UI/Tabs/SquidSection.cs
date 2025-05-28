using System.Numerics;

using Dalamud.Interface.Windowing;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI.Tabs;

public class SquidSection : SectionBase {
  internal override string Name => "SPL##SoupCatUtils";

  public SquidSection(Window parent) : base(parent) { }

  public override void Dispose() {
    GC.SuppressFinalize(this);
  }

  public override void FrameworkUpdate() {
  }

  public override void Draw() {
    base.Draw();
    var enableSplatoon_FanDanceIV = System.PluginConfig.SplatoonFanDanceIV;

    if (ImGui.Checkbox("FanDance IV", ref enableSplatoon_FanDanceIV)) {
      System.PluginConfig.SplatoonFanDanceIV = enableSplatoon_FanDanceIV;
    }

    if (System.PluginConfig.SplatoonFanDanceIV && ImGui.BeginChild("DebugFanDanceIV#SPL##SoupCatUtils", new Vector2(270, 48), false)) {
      ImGui.TextUnformatted($"{FanDance4Module.DebugState}");
      ImGui.EndChild();
    }
  }
}
