namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions;

public static class StackExtensions {
  public static Stack<T> RPush<T>(this Stack<T> stack, T item) {
    stack.Push(item);
    return stack;
  }
  public static Stack<T> RPop<T>(this Stack<T> stack, out T? result) {
    result = stack.Pop();
    return stack;
  }
  public static Stack<T> RTryPop<T>(this Stack<T> stack, out T? result, out bool successful) {
    successful = stack.TryPop(out result);
    return stack;
  }
}
