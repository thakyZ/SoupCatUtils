using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions;

/// <summary>
/// An extension class for the <see langword="string" /> type.
/// </summary>
public static partial class StringExtensions {
  /// <summary>
  /// Returns the <see cref="object.ToString()"/> method unless it is <see langword="null"/> where it will use the fallback
  /// instead.
  /// </summary>
  /// <typeparam name="TSource">Any type param allowing <see langword="null"/>.</typeparam>
  /// <param name="object">The <typeparamref name="TSource"/> to return as a <see langword="string"/>.</param>
  /// <param name="fallback">
  /// The fallback <see langword="string"/> if <paramref name="object"/> is <see langword="null"/>.
  /// </param>
  /// <returns>
  /// The fallback <see langword="string"/> if <typeparamref name="TSource"/> is <see langword="null"/>; otherwise the
  /// <see cref="object.ToString()"/> method's return value..
  /// </returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ToString<TSource>(this TSource? @object, string fallback)
    => @object?.ToString() ?? fallback;

  /// <summary>
  /// Returns the <see cref="object.ToString()"/> method unless it is <see langword="null"/> where it will use the fallback
  /// <see cref="object.ToString()"/> method instead, and if that is <see langword="null"/> will return a
  /// <see langword="string"/> containing <c>"null"</c>.
  /// </summary>
  /// <typeparam name="TSource">Any type param allowing <see langword="null"/>.</typeparam>
  /// <param name="object">The <typeparamref name="TSource"/> to return as a <see langword="string"/>.</param>
  /// <param name="fallback">
  /// The fallback <see langword="object"/> if <paramref name="object"/> is <see langword="null"/>.
  /// </param>
  /// <returns>
  /// The fallback <typeparamref name="TSource"/>'s <see cref="object.ToString()"/> method's return value if
  /// <typeparamref name="TSource"/> is <see langword="null"/>; otherwise the <see cref="object.ToString()"/> method's return
  /// value.
  /// </returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ToString<TSource>(this TSource? @object, TSource fallback)
    => @object?.ToString() ?? fallback.ToString("null");

  /// <summary>
  /// Returns the <see cref="object.ToString()"/> method unless it is <see langword="null"/> where it will use the fallback
  /// <see cref="object.ToString()"/> method instead, and if that is <see langword="null"/> will return a
  /// <see langword="string"/> containing <c>"null"</c>.
  /// </summary>
  /// <typeparam name="TSource">Any type param allowing <see langword="null"/>.</typeparam>
  /// <typeparam name="TFallback">Any type param however not <see langword="null"/>.</typeparam>
  /// <param name="object">The <typeparamref name="TSource"/> to return as a <see langword="string"/>.</param>
  /// <param name="fallback">
  /// The fallback <see langword="object"/> if <paramref name="object"/> is <see langword="null"/>.
  /// </param>
  /// <returns>
  /// The fallback <typeparamref name="TFallback"/>'s <see cref="object.ToString()"/> method's return value if
  /// <typeparamref name="TSource"/> is <see langword="null"/>; otherwise the <see cref="object.ToString()"/> method's return
  /// value.
  /// </returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string ToString<TSource, TFallback>(this TSource? @object, TFallback fallback) where TFallback : notnull
    => @object?.ToString() ?? fallback.ToString("null");

  /// <summary>
  /// Indicates whether the specified string is <see langword="null"/> or an empty string ("").
  /// </summary>
  /// <param name="value">The <see langword="string"/> to test.</param>
  /// <returns>
  /// <see langword="true"/> if the <paramref name="value" /> parameter is <see langword="null"/> or an empty string ("");
  /// otherwise, <see langword="false"/>.
  /// </returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsNullOrEmpty([NotNullWhen(false)] this string? @string) {
    return string.IsNullOrEmpty(@string);
  }

  /// <summary>
  /// Indicates whether a specified string is <see langword="null"/>, empty, or consists only of white-space characters.
  /// </summary>
  /// <param name="value">The <see langword="string"/> to test.</param>
  /// <returns>
  /// <see langword="true"/> if the value parameter is <see langword="null"/> or <see cref="string.Empty"/>, or if
  /// <paramref name="value" /> consists exclusively of white-space characters.
  /// </returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? @string) {
    return string.IsNullOrWhiteSpace(@string);
  }

  /// <summary>
  /// Indicates whether a specified string is <see langword="null"/>, empty, or consists only of white-space characters.
  /// </summary>
  /// <param name="value">The <see langword="string"/> to test.</param>
  /// <returns>
  /// <see langword="true"/> if the value parameter is <see langword="null"/> or <see cref="string.Empty"/>, or if
  /// <paramref name="value" /> consists exclusively of white-space characters.
  /// </returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsNullOrEmptyOrWhiteSpace([NotNullWhen(false)] this string? @string) {
    return @string.IsNullOrEmpty() || @string.IsNullOrWhiteSpace();
  }

  public static string LowerFirstLetter(this string @string)
    => char.ToLowerInvariant(@string[0]) + @string[1..];

  public static string UpperFirstLetter(this string @string)
    => char.ToUpperInvariant(@string[0]) + @string[1..];

  private static IEnumerable<string> ToTitleCaseInternal(this string @string) {
    return GlobalSeparatorRegex().Split(@string.Trim()).SelectMany(
        (string entry) => UpperCaseLowerCasePrecedingUpperCaseRegex().Split(entry)
      ).FilterEmpty().Select(
      (string entry) => entry.UpperFirstLetter()
    );
  }

  public static string ToTitleCase(this string @string) {
    return string.Join(' ', @string.ToTitleCaseInternal().CombineSingleLetter());
  }

  public static string ToSnakeCase(this string @string) {
    return string.Join('_', @string.ToTitleCaseInternal()).ToLower();
  }

  public static string ToKebabCase(this string @string) {
    return string.Join('-', @string.ToTitleCaseInternal()).ToLower();
  }

  public static string ToPascalCase(this string @string) {
    return string.Join(
      string.Empty,
      @string.ToTitleCaseInternal().Select(
        (string entry) => entry.UpperFirstLetter()
      ).CombineSingleLetter()
    ).UpperFirstLetter();
  }

  public static string ToCamelCase(this string @string) {
    return string.Join(
      string.Empty,
      @string.ToTitleCaseInternal().Select(
        (string entry) => entry.UpperFirstLetter()
      )
    ).LowerFirstLetter();
  }

  public static IEnumerable<string> SplitInParts(this string @string, int partLength) {
    ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(partLength, 0);
    ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(partLength, @string.Length);

    return SplitInPartsIterator(@string, partLength);
  }

  private static IEnumerable<string> SplitInPartsIterator(string @string, int partLength) {
    ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(partLength, 0);
    ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(partLength, @string.Length);

    for (var i = 0; i < @string.Length; i += partLength) {
      yield return @string.Substring(i, Math.Min(partLength, @string.Length - i));
    }
  }

  /// <summary>
  /// Matches parts in <see langword="string" />s where the character is a hyphen, underscore or space.
  /// </summary>
  [GeneratedRegex(@"[-_ ]")]
  private static partial Regex GlobalSeparatorRegex();
  
  /// <summary>
  /// Matches parts in <see langword="string" />s where lowercase characters, uppercase characters or numbers followed by
  /// uppercase characters.
  /// </summary>
  [GeneratedRegex(@"(?<=[a-z0-9A-Z])([A-Z][a-z0-9]*)")]
  private static partial Regex UpperCaseLowerCasePrecedingUpperCaseRegex();
}
