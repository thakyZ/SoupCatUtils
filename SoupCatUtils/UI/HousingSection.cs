using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

using Dalamud.Logging;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Tools;
using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;
public class HousingSection : SectionBase, IDisposable {
  public new string Name { get; set; } = "Housing##SoupCatUtils";
  protected override string NameImplementation {
    get { return Name; }
  }

  public new void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed;

  protected new void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      _isDisposed = true;
    }
  }

  private DataTable? housingData = null;

  private bool DataLoaded => housingData is not null && housingData.Rows.Count != 0;

  private void LoadData() {
    if (!DataLoaded) {
      housingData = Services.Tools.Housing.GetHousingPlotPrices();
    }
  }

  private void ClearData() {
    housingData?.Clear();
  }

  public void DrawTableHeaders() {
    ImGui.TableSetupScrollFreeze(5, 1);
    ImGui.TableSetupColumn("Index", ImGuiTableColumnFlags.Disabled | ImGuiTableColumnFlags.WidthFixed | ImGuiTableColumnFlags.DefaultSort);
    ImGui.TableSetupColumn("District", ImGuiTableColumnFlags.WidthFixed, 150.0f);
    ImGui.TableSetupColumn("Plot #", ImGuiTableColumnFlags.WidthFixed, 100.0f);
    ImGui.TableSetupColumn("Size", ImGuiTableColumnFlags.WidthFixed, 100.0f);
    ImGui.TableSetupColumn("Price Mil", ImGuiTableColumnFlags.WidthStretch);
    ImGui.TableHeadersRow();
  }
  public bool TestRow(DataRow row) {
    bool output = false;
    if (DataFilter.districtDataFilter != "None") {
      if ((string)row["district"] != DataFilter.districtDataFilter) {
        output |= true;
      }
    }
    if (DataFilter.plotDataFilter != "None") {
      if ((int)row["plot_num"] != DataFilter.plotSelect) {
        output |= true;
      }
    }
    if (DataFilter.sizeDataFilter != "None") {
      if ((string)row["size"] != DataFilter.sizeDataFilter) {
        output |= true;
      }
    }
    return !output;
  }

  public void DrawTableData() {
    foreach (DataRow row in housingData!.Rows) {
      if (TestRow(row)) {
        ImGui.Text($"{(uint)row["index"]}");
        ImGui.TableNextColumn();
        ImGui.Text($"{(string)row["district"]}");
        ImGui.TableNextColumn();
        ImGui.Text($"{(int)row["plot_num"]}");
        ImGui.TableNextColumn();
        ImGui.Text($"{(string)row["size"]}");
        ImGui.TableNextColumn();
        ImGui.Text($"{(int)((float)row["price"] * 1000000):n0} \uE049");
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
      }
    }
  }

  public void DrawDataFilter(float itemSizes = 0.0f) {
    ImGui.SetNextItemWidth(itemSizes);
    if (ImGui.BeginCombo($"District##DataFilter", DataFilter.districtDataFilter)) {
      foreach (var district in DataFilter.GetRange(nameof(DataFilter.districtDataFilter))) {
        if (ImGui.Selectable($"{district}##district-DataFilter-{district.ToPascalCase()}", DataFilter.districtDataFilter == district)) {
          DataFilter.districtDataFilter = district;
        }
      }

      ImGui.EndCombo();
    }

    ImGui.SameLine();
    ImGui.Separator();
    ImGui.SameLine();

    ImGui.SetNextItemWidth(itemSizes);
    if (ImGui.InputText("Plot # ##DataFilter", ref DataFilter.plotDataFilter, 2, ImGuiInputTextFlags.CharsNoBlank | ImGuiInputTextFlags.CharsDecimal)) {
      if (DataFilter.plotDataFilter == "0") {
        DataFilter.plotDataFilter = "None";
      }
      if ($"{DataFilter.plotDataFilter}a" == "a") {
        DataFilter.plotDataFilter = "None";
      }
      if (DataFilter.plotDataFilter == "None") {
        DataFilter.plotSelect = 0;
      } else {
        if (int.TryParse(DataFilter.plotDataFilter, new CultureInfo("en-US"), out int attempted) && attempted >= 0 && attempted <= 60) {
          DataFilter.plotSelect = attempted;
        } else {
          DataFilter.plotDataFilter = DataFilter.plotSelect.ToString() == "0" ? "None" : DataFilter.plotSelect.ToString();
        }
      }
    }

    ImGui.SameLine();
    ImGui.Separator();
    ImGui.SameLine();

    ImGui.SetNextItemWidth(itemSizes);
    if (ImGui.BeginCombo("Size##DataFilter", DataFilter.sizeDataFilter)) {
      foreach (var size in DataFilter.GetRange(nameof(DataFilter.sizeDataFilter))) {
        if (ImGui.Selectable($"{size}##size-DataFilter-{size.ToPascalCase()}", DataFilter.sizeDataFilter == size)) {
          DataFilter.sizeDataFilter = size;
        }
      }

      ImGui.EndCombo();
    }
  }

  public override void Draw() {
    CreateTitle("Housing Tools");

    ImGui.SameLine();

    if (ImGui.Button("Load Data##SoupCatUtils")) {
      LoadData();
    }

    ImGui.SameLine();

    if (ImGui.Button("Clear Data##SoupCatUtils")) {
      ClearData();
    }

    if (DataLoaded) {
      if (ImGui.BeginChild("##DataFilter", new System.Numerics.Vector2(ImGui.GetWindowWidth() - (ImGui.GetStyle().WindowPadding.X * 2), 32.0f))) {
        DrawDataFilter((ImGui.GetWindowWidth() - (ImGui.GetStyle().WindowPadding.X * 2)) / 4);
        ImGui.EndChild();
      }
    }

    if (ImGui.BeginTable("##SoupCatUtils-HousingDataFromLumina", 5, ImGuiTableFlags.ScrollY)) {
      DrawTableHeaders();
      if (DataLoaded) {
        ImGui.TableNextColumn();
        DrawTableData();
      }
      ImGui.EndTable();
    }
  }

  protected override void DisposeImpl() {
  }
}

internal static class DataFilter {

  [StringRange(typeof(TerritoryTypes), "None")]
  public static string districtDataFilter = "None";

  [StringRange(1, 60, "None")]
  public static string plotDataFilter = "None";

  public static int plotSelect = 0;

  [StringRange("None", "Small", "Medium", "Large")]
  public static string sizeDataFilter = "None";

  public static IEnumerable<string> GetRange(string fieldName) {
    try {
      var fields = typeof(DataFilter).GetFields();
      var attributes = fields
                    .Where(f => f.GetCustomAttribute(typeof(StringRangeAttribute)) != null && f.Name == fieldName)
                    .Select(f => (f, (StringRangeAttribute)f.GetCustomAttribute(typeof(StringRangeAttribute))!));

      if (attributes.ToList().Count == 0) {
        return new List<string>() { $"Error: {fields.Length}" };
      } else {
        return attributes.First().Item2.AllowableValues.ToList();
      }
    } catch (Exception exception) {
      PluginLog.Error(exception, "Failed to get attribute.");
    }
    return new List<string>() { "Error" };
  }
}
