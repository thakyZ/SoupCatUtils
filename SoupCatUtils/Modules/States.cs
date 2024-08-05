using Dalamud.Game.ClientState.Conditions;

using Dalamud.Plugin.Services;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

public class States : ModuleBase {
  private State _state { get; set; } = State.None;

  public States() : base(true) { }

  public bool this[State state] {
    get {
      return (_state & state) != 0;
    }
  }

  internal override void Update(IFramework framework) {
    if (Svc.Condition[ConditionFlag.PreparingToCraft] && (_state & State.IsCraftingLogOpen) == 0) {
      _state |= State.IsCraftingLogOpen;
    } else if (!Svc.Condition[ConditionFlag.PreparingToCraft] && (_state & State.IsCraftingLogOpen) != 0) {
      _state &= ~State.IsCraftingLogOpen;
    }
    if (Svc.GameGui.GetAddonByName("GatheringNote") != nint.Zero && (_state & State.IsFishingLogOpen) == 0) {
      _state |= State.IsGatheringLogOpen;
    } else if (Svc.GameGui.GetAddonByName("GatheringNote") == nint.Zero && (_state & State.IsFishingLogOpen) != 0) {
      _state &= ~State.IsGatheringLogOpen;
    }
    if (Svc.GameGui.GetAddonByName("FishingGuide2") != nint.Zero && (_state & State.IsFishingLogOpen) == 0) {
      _state |= State.IsFishingLogOpen;
    } else if (Svc.GameGui.GetAddonByName("FishingGuide2") == nint.Zero && (_state & State.IsFishingLogOpen) != 0) {
      _state &= ~State.IsFishingLogOpen;
    }
  }
}
