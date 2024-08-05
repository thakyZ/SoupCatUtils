namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Exceptions;

public class InvalidArgumentException : Exception {
  public string ArgumentName { get; }

  public InvalidArgumentException(string argument, string? message = "") : base(message) {
    ArgumentName = argument;
  }

  public InvalidArgumentException(string argument, string? message, Exception? innerException) : base(message, innerException) {
    ArgumentName = argument;
  }

  [Obsolete("Use a constructor that passes an argument instead.")]
  public InvalidArgumentException(string? message) : base(message) {
    ArgumentName = string.Empty;
  }

  [Obsolete("Use a constructor that passes an argument instead.")]
  public InvalidArgumentException(string? message, Exception? innerException) : base(message, innerException) {
    ArgumentName = string.Empty;
  }
}
