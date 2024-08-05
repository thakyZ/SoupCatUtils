using System.ComponentModel;
using System.Reflection;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions;

public static class EnumExtensions {
  public static string ToDescriptionString(this Enum val) {
    FieldInfo? field = val.GetType().GetField(val.ToString());
    if (field is null) {
      return string.Empty;
    }
    if (field.GetCustomAttributes(typeof(DescriptionAttribute), false) is not DescriptionAttribute[] attributes) {
      return string.Empty;
    } else if (attributes.Length > 0) {
      return attributes[0].Description;
    }
    return string.Empty;
  }
}
