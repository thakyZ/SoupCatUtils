using Newtonsoft.Json;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Configuration;

[Serializable]
public abstract class Feature {
  [JsonIgnore]
  public abstract string Name { get; }

  public virtual bool Enabled { get; set; }
}
