using System;
using System.Data;
using System.Globalization;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI.Data;
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
    if (DataFilter.Get().districtDataFilter != "None" && (string)row["district"] != DataFilter.Get().districtDataFilter) {
      output |= true;
    }
    if (DataFilter.Get().plotDataFilter != "None" && (int)row["plot_num"] != DataFilter.Get().plotSelect) {
      output |= true;
    }
    if (DataFilter.Get().sizeDataFilter != "None" && (string)row["size"] != DataFilter.Get().sizeDataFilter) {
      output |= true;
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
    if (ImGui.BeginCombo($"District##DataFilter", DataFilter.Get().districtDataFilter)) {
      foreach (var district in DataFilter.GetRange(nameof(DataFilter.districtDataFilter))) {
        if (ImGui.Selectable($"{district}##district-DataFilter-{district.ToPascalCase()}", DataFilter.Get().districtDataFilter == district)) {
          DataFilter.Get().districtDataFilter = district;
        }
      }

      ImGui.EndCombo();
    }

    ImGui.SameLine();
    ImGui.Separator();
    ImGui.SameLine();

    ImGui.SetNextItemWidth(itemSizes);
    if (ImGui.InputText("Plot # ##DataFilter", ref DataFilter.Get().plotDataFilter, 2, ImGuiInputTextFlags.CharsNoBlank | ImGuiInputTextFlags.CharsDecimal)) {
      if (DataFilter.Get().plotDataFilter == "0") {
        DataFilter.Get().plotDataFilter = "None";
      }
      if ($"{DataFilter.Get().plotDataFilter}a" == "a") {
        DataFilter.Get().plotDataFilter = "None";
      }
      if (DataFilter.Get().plotDataFilter == "None") {
        DataFilter.Get().plotSelect = 0;
      } else {
        if (int.TryParse(DataFilter.Get().plotDataFilter, new CultureInfo("en-US"), out int attempted) && attempted >= 0 && attempted <= 60) {
          DataFilter.Get().plotSelect = attempted;
        } else {
          DataFilter.Get().plotDataFilter = DataFilter.Get().plotSelect.ToString() == "0" ? "None" : DataFilter.Get().plotSelect.ToString();
        }
      }
    }

    ImGui.SameLine();
    ImGui.Separator();
    ImGui.SameLine();

    ImGui.SetNextItemWidth(itemSizes);
    if (ImGui.BeginCombo("Size##DataFilter", DataFilter.Get().sizeDataFilter)) {
      foreach (var size in DataFilter.GetRange(nameof(DataFilter.sizeDataFilter))) {
        if (ImGui.Selectable($"{size}##size-DataFilter-{size.ToPascalCase()}", DataFilter.Get().sizeDataFilter == size)) {
          DataFilter.Get().sizeDataFilter = size;
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

    if (DataLoaded && ImGui.BeginChild("##DataFilter", new System.Numerics.Vector2(ImGui.GetWindowWidth() - (ImGui.GetStyle().WindowPadding.X * 2), 32.0f))) {
      DrawDataFilter((ImGui.GetWindowWidth() - (ImGui.GetStyle().WindowPadding.X * 2)) / 4);
      ImGui.EndChild();
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
