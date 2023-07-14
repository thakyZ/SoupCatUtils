// Ignore Spelling: Splatoon

using System;
using System.Diagnostics.CodeAnalysis;

using Dalamud.Configuration;
using Dalamud.Plugin;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;

[Serializable]
public class Configuration : IPluginConfiguration {
  public int Version { get; set; } = 0;

  // the below exist just to make saving less cumbersome

  public bool SplatoonFanDanceIV { get; set; }

  public void Save() {
    Services.PluginInterface.SavePluginConfig(this);
  }
}
