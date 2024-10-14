namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

internal sealed class DebugState {
  private string? lastDebugMessage;
  private string? debugMessage;
  public string? DebugMessage {
    get => debugMessage;
    set {
      lastDebugMessage = debugMessage;
      debugMessage = value;
      if (lastDebugMessage != debugMessage && ErrorMessage is not null) {
        ErrorMessage = null;
      }
    }
  }
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
