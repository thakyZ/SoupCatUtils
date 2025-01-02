namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Exceptions;

[Serializable]
public class InvalidArgumentException : Exception {
  public string? ArgumentName { get; }

  public InvalidArgumentException(string argument, string? message = "") : base(message) {
    ArgumentName = argument;
  }

  public InvalidArgumentException(string argument, string? message, Exception? innerException) : base(message, innerException) {
    ArgumentName = argument;
  }

  [Obsolete("Use a constructor that passes an argument instead.")]
  public InvalidArgumentException() : base("Invalid argument provided.") { }

  [Obsolete("Use a constructor that passes an argument instead.")]
  public InvalidArgumentException(string? message) : base(message) { }

  [Obsolete("Use a constructor that passes an argument instead.")]
  public InvalidArgumentException(string? message, Exception? innerException) : base(message, innerException) { }
}
