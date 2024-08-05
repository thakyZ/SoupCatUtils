using Newtonsoft.Json;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Configuration;

[Serializable]
public class AutoGathererRoleAbility : Feature {
  [JsonIgnore]
  public override string Name => "Auto Gatherer Role Ability";

  public float Delay { get; set; } = 0.1f;

  public bool IncludeTruthOfMountainsForests { get; set; }

  public bool DetectNonHomework { get; set; } = true;

  public bool IncludeFathom { get; set; }

  public bool IncludeTruthOfOceans { get; set; }
}
