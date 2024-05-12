using ImGuiNET;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;
public class TweaksSection : SectionBase {
  public new string Name { get; set; } = "Tweaks##SoupCatUtils";
  protected override string NameImplementation {
    get { return Name; }
  }

  protected override void DisposeImpl() {
  }

  public override void Draw() {
    CreateTitle("Tweaks");

    var enabled = Services.PluginConfig.AutoGathererRoleAbility.Enabled;

    if (ImGui.Checkbox($"##SoupCatUtils-AutoGathererRoleAbility-Enable", ref enabled)) {
      Services.PluginConfig.AutoGathererRoleAbility.Enabled = enabled;
    }

    ImGui.SameLine();

    if (Services.PluginConfig.AutoGathererRoleAbility.Enabled) {
      if (ImGui.CollapsingHeader($"{Services.PluginConfig.AutoGathererRoleAbility.Name}##SoupCatUtils-AutoGathererRoleAbility")) {
        var delay = Services.PluginConfig.AutoGathererRoleAbility.Delay;
        if (ImGui.SliderFloat("Set Delay (seconds)##SoupCatUtils", ref delay, 0.1f, 10.0f, "%.1f")) {
          Services.PluginConfig.AutoGathererRoleAbility.Delay = delay;
        }
        var include1 = Services.PluginConfig.AutoGathererRoleAbility.IncludeTruthOfMountainsForests;
        if (ImGui.Checkbox($"Include Truth of Mountains/Forests##SoupCatUtils-AutoGathererRoleAbility", ref include1)) {
          Services.PluginConfig.AutoGathererRoleAbility.IncludeTruthOfMountainsForests = include1;
        }
        var include2 = Services.PluginConfig.AutoGathererRoleAbility.IncludeFathom;
        if (ImGui.Checkbox($"Include Fathom##SoupCatUtils-AutoGathererRoleAbility", ref include2)) {
          Services.PluginConfig.AutoGathererRoleAbility.IncludeFathom = include2;
        }
        var include3 = Services.PluginConfig.AutoGathererRoleAbility.IncludeTruthOfOceans;
        if (ImGui.Checkbox($"Include Truth of Oceans##SoupCatUtils-AutoGathererRoleAbility", ref include3)) {
          Services.PluginConfig.AutoGathererRoleAbility.IncludeTruthOfOceans = include3;
        }
      }
    } else {
      ImGui.Text(Services.PluginConfig.AutoGathererRoleAbility.Name);
    }

    base.Draw();
  }
}
