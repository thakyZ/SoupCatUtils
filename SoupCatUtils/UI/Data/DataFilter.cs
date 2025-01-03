using System.Data;
using System.Reflection;
using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Attributes;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI.Data;

internal sealed class DataFilter {
  [StringRange<TerritoryTypes>("None")]
  public string districtDataFilter = "None";

  [StringRange(1, 60, "None")]
  public string plotDataFilter = "None";

  public int plotSelect = 0;

  [StringRange("None", "Small", "Medium", "Large")]
  public string sizeDataFilter = "None";

  private static DataFilter? _instance;

  private DataFilter() {}

  public static DataFilter Get() {
    _instance ??= new();
    return _instance;
  }

  public static IEnumerable<string> GetRange(string fieldName) {
    try {
      var fields = typeof(DataFilter).GetFields();
      var attributes = fields.Where(f => f.GetCustomAttribute(typeof(StringRangeAttribute)) != null && f.Name == fieldName)
                             .Select(f => (f, (StringRangeAttribute)f.GetCustomAttribute(typeof(StringRangeAttribute))!));

      if (attributes.ToList().Count == 0) {
        return [$"Error: {fields.Length}"];
      } else {
        return [..attributes.First().Item2.AllowableValues];
      }
    } catch (Exception exception) {
      Svc.Log.Error(exception, "Failed to get attribute.");
    }
    return ["Error"];
  }
}
