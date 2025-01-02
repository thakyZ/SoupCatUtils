using Dalamud.Game.ClientState.Conditions;

using Dalamud.Plugin.Services;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

public class States : ModuleBase {
  private State State { get; set; } = State.None;

  public States() : base(true) { }

  public bool this[State state] {
    get {
      return (State & state) != 0;
    }
  }

  internal override void Update(IFramework framework) {
    if (Svc.Condition[ConditionFlag.PreparingToCraft] && (State & State.IsCraftingLogOpen) == 0) {
      State |= State.IsCraftingLogOpen;
    } else if (!Svc.Condition[ConditionFlag.PreparingToCraft] && (State & State.IsCraftingLogOpen) != 0) {
      State &= ~State.IsCraftingLogOpen;
    }
    if (Svc.GameGui.GetAddonByName("GatheringNote") != nint.Zero && (State & State.IsFishingLogOpen) == 0) {
      State |= State.IsGatheringLogOpen;
    } else if (Svc.GameGui.GetAddonByName("GatheringNote") == nint.Zero && (State & State.IsFishingLogOpen) != 0) {
      State &= ~State.IsGatheringLogOpen;
    }
    if (Svc.GameGui.GetAddonByName("FishingGuide2") != nint.Zero && (State & State.IsFishingLogOpen) == 0) {
      State |= State.IsFishingLogOpen;
    } else if (Svc.GameGui.GetAddonByName("FishingGuide2") == nint.Zero && (State & State.IsFishingLogOpen) != 0) {
      State &= ~State.IsFishingLogOpen;
    }
  }
}
