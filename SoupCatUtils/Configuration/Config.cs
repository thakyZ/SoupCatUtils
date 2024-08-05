using Dalamud.Configuration;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Configuration;

[Serializable]
public class Config : IPluginConfiguration {
  public int Version { get; set; } = 0;

  // the below exist just to make saving less cumbersome

  public bool SplatoonFanDanceIV { get; set; }

  public AutoGathererRoleAbility AutoGathererRoleAbility { get; set; } = new();

  public void Save() {
    Svc.PluginInterface.SavePluginConfig(this);
  }
}
