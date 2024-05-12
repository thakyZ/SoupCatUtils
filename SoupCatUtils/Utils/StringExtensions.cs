using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;
public static partial class StringExtensions {
  public static string ToSnakeCase(this string text) {
    text = Regex.Replace(text, @"[^A-Z\sa-z0-9]", "");
    text = Regex.Replace(text, @"\s(?=[a-z0-9])", "_");
    text = Regex.Replace(text, @"\s(?=[A-Z])", "");
    text = Regex.Replace(text, @"(.)([A-Z][a-z]+)", "$1_$2");
    text = Regex.Replace(text, @"([a-z0-9])([A-Z])", "$1_$2");
    return text.ToLower().Trim();
  }

  public static string ToPascalCase(this string text) {
    Regex invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
    Regex whiteSpace = new Regex(@"(?<=\s)");
    Regex startsWithLowerCaseChar = new Regex("^[a-z]");
    Regex firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
    Regex lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
    Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");
    // replace white spaces with underscore, then replace all invalid chars with empty string
    var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(text, "_"), string.Empty) // split by underscores
      .Split("_", StringSplitOptions.RemoveEmptyEntries) // set first letter to uppercase
      .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper())) // replace second and all following upper case letters to lower if there is no next lower (ABC -> Abc)
      .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower())) // set upper case the first lower case following a number (Ab9cd -> Ab9Cd)
      .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper())) // lower second and next upper case letters except the last if it follows by any lower (ABcDEf -> AbcDef)
      .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));
    return string.Concat(pascalCase).Trim();
  }

  public static string ToCamelCase(this string text) {
    Regex invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
    Regex whiteSpace = new Regex(@"(?<=\s)");
    Regex startsWithUpperCaseChar = new Regex("^[A-Z]");
    Regex startsWithLowerCaseChar = new Regex("^[a-z]");
    Regex firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
    Regex lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
    Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");
    // replace white spaces with underscore, then replace all invalid chars with empty string
    var camelCase = invalidCharsRgx.Replace(whiteSpace.Replace(text, "_"), string.Empty) // split by underscores
      .Split("_", StringSplitOptions.RemoveEmptyEntries) // set first letter to uppercase
      .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper())) // replace second and all following upper case letters to lower if there is no next lower (ABC -> Abc)
      .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower())) // set upper case the first lower case following a number (Ab9cd -> Ab9Cd)
      .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper())) // lower second and next upper case letters except the last if it follows by any lower (ABcDEf -> AbcDef)
      .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower())).Select((w, i) => i == 0 ? startsWithUpperCaseChar.Replace(w, m => m.Value.ToLower()) : w); // set first letter to lowercase
    return string.Concat(camelCase).Trim();
  }

	public static IEnumerable<string> SplitInParts(this string s, int partLength)
	{
    if (string.IsNullOrEmpty(s)) {
      throw new ArgumentNullException(nameof(s));
    }
    if (partLength <= 0) {
      throw new ArgumentException("Part length has to be positive.", nameof(partLength));
    }
    return SplitInPartsIterator(s, partLength);
  }

  private static IEnumerable<string> SplitInPartsIterator(string s, int partLength) {
    for (var i = 0; i < s.Length; i += partLength) {
      yield return s.Substring(i, Math.Min(partLength, s.Length - i));
    }
  }
}
