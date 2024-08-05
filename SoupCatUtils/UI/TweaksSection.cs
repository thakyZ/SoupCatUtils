using ImGuiNET;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;

public class TweaksSection : SectionBase {
  internal override string Name => "Tweaks##SoupCatUtils";

  public override void Draw() {
    base.Draw();

    var enabled = System.PluginConfig.AutoGathererRoleAbility.Enabled;

    if (ImGui.Checkbox("##SoupCatUtils-AutoGathererRoleAbility-Enable", ref enabled)) {
      System.PluginConfig.AutoGathererRoleAbility.Enabled = enabled;
    }

    ImGui.SameLine();

    if (System.PluginConfig.AutoGathererRoleAbility.Enabled) {
      if (ImGui.CollapsingHeader($"{System.PluginConfig.AutoGathererRoleAbility.Name}##SoupCatUtils-AutoGathererRoleAbility")) {
        var delay = System.PluginConfig.AutoGathererRoleAbility.Delay;
        if (ImGui.SliderFloat("Set Delay (seconds)##SoupCatUtils", ref delay, 0.1f, 10.0f, "%.1f")) {
          System.PluginConfig.AutoGathererRoleAbility.Delay = delay;
        }
        var include1 = System.PluginConfig.AutoGathererRoleAbility.IncludeTruthOfMountainsForests;
        if (ImGui.Checkbox("Include Truth of Mountains/Forests##SoupCatUtils-AutoGathererRoleAbility", ref include1)) {
          System.PluginConfig.AutoGathererRoleAbility.IncludeTruthOfMountainsForests = include1;
        }
        var include2 = System.PluginConfig.AutoGathererRoleAbility.IncludeFathom;
        if (ImGui.Checkbox("Include Fathom##SoupCatUtils-AutoGathererRoleAbility", ref include2)) {
          System.PluginConfig.AutoGathererRoleAbility.IncludeFathom = include2;
        }
        var include3 = System.PluginConfig.AutoGathererRoleAbility.IncludeTruthOfOceans;
        if (ImGui.Checkbox("Include Truth of Oceans##SoupCatUtils-AutoGathererRoleAbility", ref include3)) {
          System.PluginConfig.AutoGathererRoleAbility.IncludeTruthOfOceans = include3;
        }
      }
    } else {
      ImGui.Text(System.PluginConfig.AutoGathererRoleAbility.Name);
    }

    base.Draw();
  }

  public override void Dispose() {
    GC.SuppressFinalize(this);
  }

  public override void FrameworkUpdate() {
  }
}
