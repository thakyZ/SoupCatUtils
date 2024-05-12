using System.Numerics;

using ImGuiNET;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;
public class SquidSection : SectionBase {
  public new string Name { get; set; } = "SPL##SoupCatUtils";
  protected override string NameImplementation {
    get { return Name; }
  }

  public SquidSection() {
  }

  protected override void DisposeImpl() {
      Services.FanDance4Module.Dispose();
  }

  public override void Draw() {
    CreateTitle("SPL");

    var enableSPL_FDIV = Services.PluginConfig.SplatoonFanDanceIV;

    if (ImGui.Checkbox("FanDance IV", ref enableSPL_FDIV)) {
      Services.PluginConfig.SplatoonFanDanceIV = enableSPL_FDIV;
    }

    if (Services.PluginConfig.SplatoonFanDanceIV && ImGui.BeginChild("DebugFanDanceIV#SPL##SoupCatUtils", new Vector2(270, 48), false)) {
      ImGui.TextUnformatted($"{Services.FanDance4DebugState}");
      ImGui.EndChild();
    }
  }
}
