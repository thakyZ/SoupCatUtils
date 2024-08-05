namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

[Flags]
public enum State {
  None = 0,
  IsGatheringLogOpen = 1,
  IsFishingLogOpen = 2,
  IsCraftingLogOpen = 4,
}
