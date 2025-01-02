using System.ComponentModel;
using System.Reflection;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions;

/// <summary>
/// Extension methods for <see cref="Enum" />
/// </summary>
public static class EnumExtensions {
  /// <summary>
  /// Gets the <see cref="DescriptionAttribute.Description" /> that is attached to an <see cref="Enum" /> value.
  /// </summary>
  /// <typeparam name="T">The <see cref="Enum" /> type to query.</typeparam>
  /// <param name="value">The value of the <see cref="Enum" /> to fetch.</param>
  /// <returns><see langword="string" /> if there was a <see cref="DescriptionAttribute" /> found; otherwise <see langword="null" />.</returns>
  public static string? ToDescriptionString<T>(this T value) where T : struct, Enum {
    FieldInfo? field = value.GetType().GetField(value.ToString());
    if (field is null) {
      return null;
    }
    if (field.GetCustomAttributes(typeof(DescriptionAttribute), false) is not DescriptionAttribute[] attributes) {
      return null;
    }
    if (attributes.Length == 0) {
      return null;
    }
    return attributes[0].Description;
  }

  /// <summary>
  /// Gets the <see cref="Enum" /> value by the <see cref="DescriptionAttribute.Description" /> value that is assigned to each value in the <see cref="Enum" />.
  /// </summary>
  /// <typeparam name="T">The type of an <see cref="Enum" />.</typeparam>
  /// <param name="description">The <see cref="DescriptionAttribute.Description" /> value.</param>
  /// <returns>A value of the <see cref="Enum" /> if found otherwise the default value.</returns>
  public static T GetEnumValueByDescription<T>(this string? description) where T : struct, Enum {
    if (description is null) return default;
    FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
    var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
      (field, attribute) => new { Field = field, Attribute = attribute })
      .SingleOrDefault(attribute => ((DescriptionAttribute)attribute.Attribute).Description == description)!;
    return field is null || field.Field.GetRawConstantValue() is not T value ? default : value;
  }
}
