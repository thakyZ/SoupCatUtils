using System.Numerics;

using Dalamud.Interface.Utility;

using FFXIVClientStructs.FFXIV.Client.Game;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;

public class AboutSection : SectionBase {
  internal override string Name => "About##SoupCatUtils";
  private bool trackActions = false;
  private ActionHandler? actionHandler;
  internal bool TrackActions {
    get {
      return this.trackActions;
    }
    set {
      trackActions = value;
      if (value) {
        actionHandler ??= new ActionHandler();
      } else {
        actionHandler?.Dispose();
        actionHandler = null;
      }
    }
  }

  public override void Draw() {
    CreateTitle("About", Plugin.StaticName, $"by: {Plugin.StaticAuthor}");
    ImGui.Text("Misc Tools");

    var _trackActions = this.TrackActions;

    if (ImGui.Checkbox("Track Actions used...", ref _trackActions)) {
      this.TrackActions = _trackActions;

      if (!_trackActions) {
        actionHandler?.ActionsTracked.Clear();
      }
    }

    if (this.TrackActions && ImGui.BeginTable("ActionTrackTable", 3, ImGuiTableFlags.SizingStretchSame | ImGuiTableFlags.BordersOuter,
        new Vector2((ImGui.GetWindowWidth() - (ImGui.GetStyle().WindowPadding.X * 2)) * ImGuiHelpers.GlobalScale, (280 - (ImGui.GetStyle().WindowPadding.X * 2)) * ImGuiHelpers.GlobalScale))) {
      ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.NoSort);
      ImGui.TableSetupColumn("ID",   ImGuiTableColumnFlags.NoSort);
      ImGui.TableSetupColumn("Type", ImGuiTableColumnFlags.NoSort);
      ImGui.TableHeadersRow();
      foreach ((string name, uint rowId, ActionType actionType) in actionHandler?.ActionsTracked ?? []) {
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
