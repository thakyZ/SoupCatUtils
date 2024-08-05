using System.ComponentModel;
using System.Reflection;

using Dalamud.Interface.GameFonts;

using ECommons;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions;

internal static class GameFontFamilyAndSizeProxyExtensions {
  public static string ToDescriptionString(this GameFontFamilyAndSizeProxy val) {
    if (val.GetType().GetField(val.ToString())!.GetCustomAttributes(typeof(DescriptionAttribute), false) is not DescriptionAttribute[] attributes) {
      return string.Empty;
    } else if (attributes.Length > 0) {
      return attributes[0].Description;
    } else {
      return string.Empty;
    }
  }
  public static T GetEnumValueByDescription<T>(this string val) where T : Enum {
    FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
    var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
      (f, a) => new { Field = f, Att = a })
      .SingleOrDefault(a =>((DescriptionAttribute)a.Att).Description == val)!;
    return field is null || field.Field.GetRawConstantValue() is null ? default! : (T)field.Field.GetRawConstantValue()!;
  }

  public static GameFontFamilyAndSize GetFontFamilyAndSize(this FontData val) {
    return $"{val.Name}_{val.Size}".GameFontFamilyAndSizeUnProxy();
  }

  public static GameFontFamily GetFontFamily(this FontData val) {
    return $"{val.Name}".GameFontFamilyUnProxy();
  }

  public static GameFontFamilyAndSize UnProxy(this GameFontFamilyAndSizeProxy val) {
    var toInt = (int)val;
    return (GameFontFamilyAndSize)toInt;
  }

  public static GameFontFamily GameFontFamilyUnProxy(this string val) {
    string[]         names = Enum.GetNames(typeof(GameFontFamily));
    GameFontFamily[] values = Enum.GetValues<GameFontFamily>();
    return (GameFontFamily)(values.GetValue(names.IndexOf(x => x.Equals(val, StringComparison.OrdinalIgnoreCase))) ?? GameFontFamily.Axis);
  }

  public static GameFontFamilyAndSize GameFontFamilyAndSizeUnProxy(this string val) {
    FieldInfo[] fields = typeof(GameFontFamilyAndSizeProxy).GetFields(BindingFlags.Public | BindingFlags.Static);
    var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
      (f, a) => new { Field = f, Att = a })
      .SingleOrDefault(a =>((DescriptionAttribute)a.Att).Description == val)!;
    var isProxy = field is null || field.Field.GetRawConstantValue() is null ? default : (GameFontFamilyAndSizeProxy)field.Field.GetRawConstantValue()!;
    var toInt = (int)isProxy;
    return (GameFontFamilyAndSize)toInt;
  }
}
