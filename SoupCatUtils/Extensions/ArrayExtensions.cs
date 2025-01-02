namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions;

public static class ArrayExtensions {
  public static (int index, T entry)[] GetIndexEnumerator<T>(this T[] array) {
    return [..array.Select((T entry, int index) => (index, entry))];
  }

  public static string[] FilterEmpty(this string[] array) { 
    return [..array.Where((string @string) => !@string.IsNullOrEmptyOrWhiteSpace())];
  }

  public static IEnumerable<string> FilterEmpty(this IEnumerable<string> enumerable) { 
    return enumerable.Where((string @string) => !@string.IsNullOrEmptyOrWhiteSpace());
  }

  public static IEnumerable<string> CombineSingleLetter(this IEnumerable<string> enumerable) {
    string[] array = [..enumerable];
    List<string> output = [];
    for (int i = 0; i < array.Length; i++) {
      if (i < array.Length - 1 && array[i].Length == 1 && char.IsUpper(array[i][0])) {
        output.Add(array[i] + array[i + 1]);
        i++;
      } else {
        output.Add(array[i]);
      }
    }
    array = [..output];
    output.Clear();
    for (int i = 0; i < array.Length; i++) {
      if (i < array.Length - 1 && array[i].Length == 1 && char.IsUpper(array[i][0])) {
        output.Add(array[i] + array[i + 1]);
        i++;
      } else {
        output.Add(array[i]);
      }
    }
    return [..output];
  }
}
