namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

internal sealed class DebugState {
  private string? _lastDebugMessage;
  private string? _debugMessage;
  public string? DebugMessage {
    get => _debugMessage;
    set {
      _lastDebugMessage = _debugMessage;
      _debugMessage = value;
      if (_lastDebugMessage != _debugMessage && ErrorMessage is not null) {
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
