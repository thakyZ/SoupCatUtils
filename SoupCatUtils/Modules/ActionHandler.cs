using Dalamud.Hooking;
using Dalamud.Plugin.Services;

using Lumina.Excel;
using Action = Lumina.Excel.Sheets.Action;
using Lumina.Data;

using FFXIVClientStructs.FFXIV.Client.Game;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

internal sealed class ActionHandler : ModuleBase {
  private readonly Hook<UseActionHandler>? _useActionHook;
  internal List<(string, uint, ActionType)> ActionsTracked { get; } = [];
  private static ExcelSheet<Action>? ActionsSheet { get; set; }
  internal static DebugState DebugState { get; } = new();

  public unsafe ActionHandler() : base(false) {
    ActionsSheet ??= Svc.Data.Excel.GetSheet<Action>(Language.English);
    IntPtr renderAddress = ActionManager.Addresses.UseAction.Value;
    if (renderAddress is 0) {
      DebugState.ErrorMessage = "Unable to load UseAction address";
      return;
    }
    if (DebugState.ErrorMessage is not null) {
      DebugState.ErrorMessage = null;
    }
    _useActionHook = Svc.Hook.HookFromAddress<UseActionHandler>(renderAddress, OnUseAction);
    _useActionHook?.Enable();
  }

  public unsafe delegate byte UseActionHandler(ActionManager* thisPtr, ActionType actionType, uint actionId, ulong targetId, uint extraParam, ActionManager.UseActionMode mode, uint comboRouteId, bool* outOptAreaTargeted);

  private string GetActionName(uint actionId) {
    IEnumerable<Action>? foundActions = ActionsSheet?.Where((x) => x.RowId == actionId);
    return foundActions?.Any() == true ? foundActions.First().Name.ExtractText() : "empty";
  }

  private unsafe byte OnUseAction(ActionManager* actionManager, ActionType actionType, uint actionId, ulong targetId, uint extraParam, ActionManager.UseActionMode mode, uint comboRouteId, bool* outOptAreaTargeted) {
    ActionsTracked.Add((GetActionName(actionId), actionId, actionType));

    return _useActionHook!.Original(actionManager, actionType, actionId, targetId, extraParam, mode, comboRouteId, outOptAreaTargeted);
  }

  internal override void DisposeManaged() {
    _useActionHook?.Dispose();
  }
}
