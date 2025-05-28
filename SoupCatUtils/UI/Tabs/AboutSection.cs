using System.Numerics;

using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;

using FFXIVClientStructs.FFXIV.Client.Game;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI.Tabs;

public class AboutSection : SectionBase {
  internal override string Name => "About##SoupCatUtils";
  private bool _trackActions;
  private ActionHandler? _actionHandler;

  public AboutSection(Window parent) : base(parent) { }

  internal bool TrackActions {
    get {
      return this._trackActions;
    }
    set {
      _trackActions = value;
      if (value) {
        _actionHandler ??= new ActionHandler();
      } else {
        _actionHandler?.Dispose();
        _actionHandler = null;
      }
    }
  }

  public override void Draw() {
    CreateTitle("About", Plugin.Name, $"by: {Plugin.Author}");
    ImGui.Text("Misc Tools");

    var _trackActions = TrackActions;

    if (ImGui.Checkbox("Track Actions used...", ref _trackActions)) {
      TrackActions = _trackActions;

      if (!_trackActions) {
        _actionHandler?.ActionsTracked.Clear();
      }
    }

    if (TrackActions && ImGui.BeginTable("ActionTrackTable", 3, ImGuiTableFlags.SizingStretchSame | ImGuiTableFlags.BordersOuter,
        new Vector2((ImGui.GetWindowWidth() - ImGui.GetStyle().WindowPadding.X * 2) * ImGuiHelpers.GlobalScale, (280 - ImGui.GetStyle().WindowPadding.X * 2) * ImGuiHelpers.GlobalScale))) {
      ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.NoSort);
      ImGui.TableSetupColumn("ID",   ImGuiTableColumnFlags.NoSort);
      ImGui.TableSetupColumn("Type", ImGuiTableColumnFlags.NoSort);
      ImGui.TableHeadersRow();
      foreach ((string name, uint rowId, ActionType actionType) in _actionHandler?.ActionsTracked ?? []) {
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text(name);
        ImGui.TableNextColumn();
        ImGui.Text(rowId.ToString());
        ImGui.TableNextColumn();
        ImGui.Text(actionType.ToString());
      }
      ImGui.EndTable();
    }

    ImGui.TextDisabled("- End -");
  }

  public override void Dispose() {
    GC.SuppressFinalize(this);
  }

  public override void FrameworkUpdate() {
  }
}
