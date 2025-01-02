using System.ComponentModel;
using System.Reflection;

using Dalamud.Interface.GameFonts;

using ECommons;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions;

/// <summary>
/// Extension methods for <see cref="GameFontFamilyAndSizeProxy" />
/// </summary>
internal static class GameFontFamilyAndSizeProxyExtensions {
  /// <summary>
  /// Returns an instance of <see cref="GameFontFamilyAndSize" /> <see cref="Enum" /> from the provided <see cref="FontData" />.
  /// </summary>
  /// <param name="fontData">The provided <see cref="FontData" />.</param>
  /// <returns>An instance of <see cref="GameFontFamilyAndSize" />.</returns>
  public static GameFontFamilyAndSize GetFontFamilyAndSize(this FontData fontData) {
    return $"{fontData.Name}_{fontData.Size}".GameFontFamilyAndSizeUnProxy();
  }

  /// <summary>
  ///  Returns an instance of <see cref="GameFontFamily" /> <see cref="Enum" /> from the provided <see cref="FontData" />.
  /// </summary>
  /// <param name="fontData">The provided <see cref="FontData" />.</param>
  /// <returns>An instance of <see cref="GameFontFamily" />.</returns>
  public static GameFontFamily GetFontFamily(this FontData fontData) {
    return $"{fontData.Name}".GameFontFamilyUnProxy();
  }

  /// <summary>
  ///  Returns an instance of <see cref="GameFontFamilyAndSize" /> <see cref="Enum" /> from the provided <see cref="GameFontFamilyAndSizeProxy" />.
  /// </summary>
  /// <param name="proxy">The provided <see cref="GameFontFamilyAndSizeProxy" />.</param>
  /// <returns>An instance of <see cref="GameFontFamilyAndSize" />.</returns>
  public static GameFontFamilyAndSize UnProxy(this GameFontFamilyAndSizeProxy proxy) {
    var toInt = (int)proxy;
    return (GameFontFamilyAndSize)toInt;
  }

  /// <summary>
  /// Returns an instance of <see cref="GameFontFamily" /> <see cref="Enum" /> from the provided <see langword="string" /> value.
  /// </summary>
  /// <param name="string">The provided <see langword="string" /> value.</param>
  /// <returns>An instance of <see cref="GameFontFamily" />.</returns>
  public static GameFontFamily GameFontFamilyUnProxy(this string @string) {
    string[]         names = Enum.GetNames(typeof(GameFontFamily));
    GameFontFamily[] values = Enum.GetValues<GameFontFamily>();
    return (GameFontFamily)(values.GetValue(names.IndexOf(x => x.Equals(@string, StringComparison.OrdinalIgnoreCase))) ?? GameFontFamily.Axis);
  }

  /// <summary>
  /// Returns an instance of <see cref="GameFontFamilyAndSize" /> <see cref="Enum" /> from the provided <see langword="string" /> value.
  /// </summary>
  /// <param name="string">The provided <see langword="string" /> value.</param>
  /// <returns>An instance of <see cref="GameFontFamily" />.</returns>
  public static GameFontFamilyAndSize GameFontFamilyAndSizeUnProxy(this string @string) {
    FieldInfo[] fields = typeof(GameFontFamilyAndSizeProxy).GetFields(BindingFlags.Public | BindingFlags.Static);
    var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
      (f, a) => new { Field = f, Att = a })
      .SingleOrDefault(a =>((DescriptionAttribute)a.Att).Description == @string)!;
    return field is null || field.Field.GetRawConstantValue() is not GameFontFamilyAndSize gfmas ? default : gfmas;
  }
}
