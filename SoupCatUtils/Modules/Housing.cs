using System.Data;

using Dalamud.Plugin.Services;

using Lumina.Excel;
using Lumina.Excel.Sheets;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

public class Housing : ModuleBase {
  private readonly ExcelSheet<HousingLandSet>? HousingLandSets;
  private readonly TerritoryTypes[] territoryTypes = [TerritoryTypes.Mist, TerritoryTypes.LavenderBeds, TerritoryTypes.Goblet, TerritoryTypes.Shirogane, TerritoryTypes.Empyreum];

  public Housing() : base(false) {
    HousingLandSets = Svc.Data.GetExcelSheet<HousingLandSet>();
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
    // Create index column
    DataColumn dataColumn = new() {
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
      HousingLandSet landSet = HousingLandSets.GetRow(TerritoryTypeIdToLandSetId(territoryType));
      for (int plotNumber = 0; plotNumber < 60; plotNumber++) {
        dataRow = dataTable.NewRow();
        string districtName = territoryType.ToDescriptionString();
        byte? houseSize = landSet.LandSet[plotNumber].PlotSize;
        uint realPrice = landSet.LandSet[plotNumber].InitialPrice;
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
