using System;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;
internal sealed class DebugState {
  public string? DebugMessage { get; set; }
  public string? ErrorMessage { get; set; }

  public void SetFromException(Exception e) {
    ErrorMessage = $"{e}";
  }

  public override string ToString() {
    return $"{DebugMessage ?? "No Message"}\n{ErrorMessage ?? "No Error"}";
  }

  public void Reset() {
    DebugMessage = null;
  }
}
