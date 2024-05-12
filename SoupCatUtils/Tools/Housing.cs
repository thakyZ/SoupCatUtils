using System.ComponentModel;
using System.Data;

using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Tools;
/* Territory Type Key
  * 339 => Mist
  * 340 => The Lavender Beds
  * 341 => The Goblet
  * 641 => Shirogane
  * 979 => The Empyreum
  */
/// <summary>
/// Territory Type Key.
/// <remarks>Can be passed as a uint</remarks>
/// <list type="table">
///   <listheader>
///     <term>ID</term>
///     <description>Correlation</description>
///   </listheader>
///   <item>
///     <term>339</term>
///     <description>Mist</description>
///   </item>
///   <item>
///     <term>340</term>
///     <description>The Lavender Beds</description>
///   </item>
///   <item>
///     <term>341</term>
///     <description>The Goblet</description>
///   </item>
///   <item>
///     <term>641</term>
///     <description>Shirogane</description>
///   </item>
///   <item>
///     <term>979</term>
///     <description>The Empyreum</description>
///   </item>
/// </list>
/// </summary>
public enum TerritoryTypes {
  [Description("Mist")]
  Mist = 339,
  [Description("The Lavender Beds")]
  LavenderBeds = 340,
  [Description("The Goblet")]
  Goblet = 341,
  [Description("Shirogane")]
  Shirogane = 641,
  [Description("The Empyreum")]
  Empyreum = 979
}

public class Housing {
  private readonly ExcelSheet<HousingLandSet>? HousingLandSets;
  private readonly TerritoryTypes[] territoryTypes = [TerritoryTypes.Mist, TerritoryTypes.LavenderBeds, TerritoryTypes.Goblet, TerritoryTypes.Shirogane, TerritoryTypes.Empyreum];

  public Housing() {
    HousingLandSets = Services.DataManager.GetExcelSheet<HousingLandSet>();
  }

  public static uint TerritoryTypeIdToLandSetId(TerritoryTypes val) {
    return (uint)val switch {
      641 => 3, // Shirogane
      979 => 4, // Empyreum
      _ => (uint)val - 339 // Mist, The Lavender Beds, and The Goblet are 339-341
    };
  }

  private static DataTable InitDataTable(string tableName) {
    DataTable dataTable = new DataTable(tableName);
    DataColumn dataColumn;
    // Create index column
    dataColumn = new DataColumn {
      DataType = typeof(uint),
      ColumnName = "index",
      Caption = "Index",
      ReadOnly = false,
      Unique = true
    };
    // Add column to the DataColumnCollection.
    dataTable.Columns.Add(dataColumn);
    // Create district column
    dataColumn = new DataColumn {
      DataType = typeof(string),
      ColumnName = "district",
      Caption = "District",
      AutoIncrement = false,
      ReadOnly = false,
      Unique = false
    };
    // Add column to the DataColumnCollection.
    dataTable.Columns.Add(dataColumn);
    // Create plot number column
    dataColumn = new DataColumn {
      DataType = typeof(int),
      ColumnName = "plot_num",
      Caption = "Plot #",
      AutoIncrement = false,
      ReadOnly = false,
      Unique = false
    };
    // Add column to the DataColumnCollection.
    dataTable.Columns.Add(dataColumn);
    // Create plot size column
    dataColumn = new DataColumn {
      DataType = typeof(string),
      ColumnName = "size",
      Caption = "Size",
      AutoIncrement = false,
      ReadOnly = false,
      Unique = false
    };
    // Add column to the DataColumnCollection.
    dataTable.Columns.Add(dataColumn);
    // Create plot size column
    dataColumn = new DataColumn {
      DataType = typeof(float),
      ColumnName = "price",
      Caption = "Price Mil",
      AutoIncrement = false,
      ReadOnly = false,
      Unique = false
    };
    // Add column to the DataColumnCollection.
    dataTable.Columns.Add(dataColumn);
    DataColumn[] primaryKeyColumns = [dataTable.Columns["index"]!];
    dataTable.PrimaryKey = primaryKeyColumns;
    return dataTable;
  }

  public DataTable GetHousingPlotPrices() {
    DataTable dataTable = InitDataTable("ffxivHousingData");
    if (HousingLandSets is null) return dataTable;
    DataSet dataSet = new DataSet();
    dataSet.Tables.Add(dataTable);
    DataRow dataRow;
    int index = 0;
    foreach (TerritoryTypes territoryType in territoryTypes) {
      HousingLandSet? landSet = HousingLandSets.GetRow(TerritoryTypeIdToLandSetId(territoryType));
      if (landSet is null) return dataTable;
      for (int plotNumber = 0; plotNumber < 60; plotNumber++) {
        dataRow = dataTable.NewRow();
        string districtName = territoryType.ToDescriptionString();
        byte? houseSize = landSet.PlotSize[plotNumber];
        uint realPrice = landSet.InitialPrice[plotNumber];
        float housePriceMillions = realPrice / 1000000f;
        string houseSizeName = houseSize switch {
          0 => "Small",
          1 => "Medium",
          _ => "Large"
        };
        dataRow["index"] = index;
        dataRow["district"] = districtName;
        dataRow["plot_num"] = plotNumber + 1;
        dataRow["size"] = houseSizeName;
        dataRow["price"] = housePriceMillions;
        dataTable.Rows.Add(dataRow);
        index++;
      }
    }
    return dataTable;
  }
}
