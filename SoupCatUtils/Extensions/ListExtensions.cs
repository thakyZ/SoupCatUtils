namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions;

public static class ListExtensions {
  public static (int, T)[] GetIndexEnumerator<T>(this T[] array) {
    return [..array.Select((x, i) => (i, x))];
  }
}
