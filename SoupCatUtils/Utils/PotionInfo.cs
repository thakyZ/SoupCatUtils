using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommons.ExcelServices;
using Lumina.Excel;
using Lumina.Excel.Sheets;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;
public class PotionInfo : IComparable<PotionInfo> {
  public string Name { get; set; }
  public uint ID { get; set; }
  public ushort Max { get; set; }
  public ushort Percentage { get; set; }
  public PotionType Type { get; set; }
  public PotionInfo(Item item) {
    Name = item.GetName();
    ID = item.RowId;
    var itemAction = GetItemAction(item.ItemAction);
    Max = itemAction?.Data[1] ?? 0;
    Percentage = itemAction?.Data[0] ?? 0;
    Type = GetItemType(item);
  }

  private PotionType GetItemType(Item item) {
    var description = item.Description.ExtractText();
    if (description.Contains("HP")) {
      return PotionType.Health;
    }
    if (description.Contains("MP") || description.Contains("GP")) {
      return PotionType.Mana;
    }
    if (description.Contains("temporarily increases", StringComparison.InvariantCulture)) {
      return PotionType.Tincture;
    }
    return PotionType.Unknown;
  }

  private ItemAction? GetItemAction(RowRef<ItemAction> lazyRow) {
    if (!lazyRow.IsValid)
      return Svc.Data.GetExcelSheet<ItemAction>()?.FirstOrDefault((ItemAction action) => action.RowId == lazyRow.RowId);
    return lazyRow.Value;
  }

  public int CompareTo(PotionInfo? other) {
    if (other is null) {
      return 1;
    }
    var id = ID.CompareTo(other.ID);
    var max = Max.CompareTo(other.Max);
    var percentage = Percentage.CompareTo(other.Percentage);
    return id != 0 ? id : max != 0 ? max : percentage != 0 ? percentage : 0;
  }

  public bool IsValid() {
    return Type switch {
      PotionType.Tincture => Max != 0 && Percentage != 0,
      PotionType.Mana => Max != 0 && Percentage != 0,
      PotionType.Health => Max != 0 && Percentage != 0,
      _ => false
    };
  }

  public enum PotionType {
    Unknown = 0,
    Health = 1,
    Mana = 2,
    Tincture = 3
  }
}
