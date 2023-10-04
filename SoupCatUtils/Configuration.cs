// Ignore Spelling: Splatoon

using System;
using System.Collections.Generic;

using Dalamud.Configuration;

using FFXIVClientStructs.FFXIV.Application.Network.WorkDefinitions;

using Newtonsoft.Json;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;

[Serializable]
public class Configuration : IPluginConfiguration {
  public int Version { get; set; } = 0;

  // the below exist just to make saving less cumbersome

  public bool SplatoonFanDanceIV { get; set; }

  public AutoGathererRoleAbility AutoGathererRoleAbility { get; set; } = new();

  public void Save() {
    Services.PluginInterface.SavePluginConfig(this);
  }
}

[Serializable]
public abstract class Feature {
  [JsonIgnore]
  public abstract string Name { get; }

  public virtual bool Enabled { get; set; }
}

[Serializable]
public class AutoGathererRoleAbility : Feature {
  public override string Name => "Auto Gatherer Role Ability";

  public float Delay { get; set; } = 0.1f;

  public bool IncludeTruthOfMountainsForests { get; set; }

  public bool DetectNonHomework { get; set; } = true;

  public bool IncludeFathom { get; set; }

  public bool IncludeTruthOfOceans { get; set; }
}
