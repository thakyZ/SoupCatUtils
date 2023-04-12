using System;
using System.Diagnostics.CodeAnalysis;

using Dalamud.Configuration;
using Dalamud.Plugin;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;

[Serializable]
public class Configuration : IPluginConfiguration {
  public int Version { get; set; } = 0;

  // the below exist just to make saving less cumbersome

  [NonSerialized][AllowNull, NotNull] private DalamudPluginInterface pluginInterface;

  public void Initialize(DalamudPluginInterface pluginInterface) {
    this.pluginInterface = pluginInterface;
  }

  public void Save() {
    pluginInterface.SavePluginConfig(this);
  }
}
