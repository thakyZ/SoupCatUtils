using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Tools;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

public static class EnumExtensions {
  public static string ToDescriptionString(this Enum val) {
    if (val.GetType().GetField(val.ToString())!.GetCustomAttributes(typeof(DescriptionAttribute), false) is not DescriptionAttribute[] attributes) {
      return string.Empty;
    } else if (attributes.Length > 0) {
      return attributes[0].Description;
    } else {
      return string.Empty;
    }
  }
}
