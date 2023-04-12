using System;
using System.Data;
using System.Linq;
using System.Reflection.Emit;

using Dalamud.Interface;
using Dalamud.Interface.Windowing;

using FFXIVClientStructs.FFXIV.Common.Math;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Tools;
using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

using static Dalamud.Interface.GameFonts.GameFontLayoutPlan;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;
public class HousingSection : SectionBase, IDisposable {
  public new string Name { get; set; } = "Housing##SoupCatUtils";
  protected override string NameImpl {
    get { return Name; }
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed;

  protected virtual void Dispose(bool disposing) {
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

  public void DrawTableData() {
    foreach (DataRow row in housingData!.Rows) {
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

  [StringRange(typeof(TerritoryTypes), "None")]
  private string districtDataFilter = "None";
  [StringRange(1, 60, "None")]
  private string plotDataFilter = "None";
  private int plotSelect = 0;
  [StringRange("None", "Small", "Medium", "Large")]
  private string sizeDataFilter = "None";

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
    /*if (ImGui.BeginCombo("District##DataFilter", districtDataFilter)) {
      foreach (var district in districtDataFilter.GetRange()) {
        if (!ImGui.Selectable($"{district}##district-DataFilter-{district.ToPascalCase()}", districtDataFilter == district)) {
          continue;
        }

        districtDataFilter = district;
      }

      ImGui.EndCombo();
    }
    ImGui.SameLine();
    var _plotSelect = plotSelect.ToString();
    ImGui.BeginGroup();
    ImGui.PushID("Plot # ##DataFilter");
    const float button_size = GetFrameHeight();
    ImGui.SetNextItemWidth(Math.Max(1.0f, ImGui.CalcItemWidth() - (button_size + style.ItemInnerSpacing.x) * 2));
    if (ImGui.InputText("Plot # ##DataFilter", ref _plotSelect, 4)) {
      if (plotSelect == 0) {
        //ImGui.Get
      }
      foreach (var plot in plotDataFilter.GetRange()) {
        if (!ImGui.Selectable($"{plot}##district-DataFilter-{plot.ToPascalCase()}", districtDataFilter == district)) {
          continue;
        }

        districtDataFilter = district;
      }

      ImGui.EndCombo();
    }
    ImGui.SameLine();
    if (ImGui.BeginCombo("Size##DataFilter", sizeDataFilter)) {

    }
    ImGui.SameLine();*/
    ImGui.BeginTable("##SoupCatUtils-HousingDataFromLumina", 5, ImGuiTableFlags.ScrollY);
    DrawTableHeaders();
    if (DataLoaded) {
      ImGui.TableNextColumn();
      DrawTableData();
    }
    ImGui.EndTable();
  }
}
