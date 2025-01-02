using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Dalamud.Configuration;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Configuration;

[Serializable]
public class Config : IPluginConfiguration, INotifyPropertyChanged {
  public event PropertyChangedEventHandler? PropertyChanged;

  public void TriggerPropertyChangedEventHandler([CallerMemberName] string? propertyName = null) {
    if (!string.IsNullOrEmpty(propertyName)) {
      PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }
  }

  public int Version { get; set; } = 0;

  // the below exist just to make saving less cumbersome

  public bool SplatoonFanDanceIV { get; set; }

  [JsonProperty(PropertyName = nameof(EnableUseBestPotionCommand))]
  public bool enableUseBestPotionCommand;

  [JsonIgnore]
  public bool EnableUseBestPotionCommand {
    get => enableUseBestPotionCommand;
    set {
      enableUseBestPotionCommand = value;
      TriggerPropertyChangedEventHandler();
    }
  }

  public AutoGathererRoleAbility AutoGathererRoleAbility { get; set; } = new();

  public void Save() {
    Svc.PluginInterface.SavePluginConfig(this);
  }
}
