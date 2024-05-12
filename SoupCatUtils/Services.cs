using System;
using System.Diagnostics.CodeAnalysis;

using Dalamud.Game;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Interface.ManagedFontAtlas;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;
using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Tools;
using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;
public class Services : IDisposable {
  internal static Services servicesInstance { get; set; } = null!;
  internal static void Initialize(DalamudPluginInterface pluginInterface) {
    servicesInstance = pluginInterface.Create<Services>() ?? throw new ArgumentNullException(nameof(pluginInterface));
  }

  internal static Plugin PluginInstance { get; set; } = null!;
  internal static Configuration PluginConfig { get; set; } = null!;
  internal static class Tools {
    public static Housing Housing { get; set; } = null!;
  }
  internal static PluginUI UI { get; set; } = null!;
  internal static FontContainer FontContainer { get; set; } = null!;

  internal static DebugState FanDance4DebugState { get; set; } = null!;
  internal static FanDance4Module FanDance4Module { get; set; } = null!;

  [PluginService][AllowNull, NotNull] public static IClientState ClientState { get; private set; }
  [PluginService][AllowNull, NotNull] public static ICommandManager CommandManager { get; private set; }
  [PluginService][AllowNull, NotNull] public static DalamudPluginInterface PluginInterface { get; private set; }
  [PluginService][AllowNull, NotNull] public static IFramework Framework { get; private set; }
  [PluginService][AllowNull, NotNull] public static IGameGui GameGui { get; private set; }
  [PluginService][AllowNull, NotNull] public static IGameNetwork GameNetwork { get; private set; }
  [PluginService][AllowNull, NotNull] public static IDataManager DataManager { get; private set; }
  [PluginService][AllowNull, NotNull] public static ISigScanner SigScanner { get; private set; }
  [PluginService][AllowNull, NotNull] public static ITargetManager TargetManager { get; private set; }
  [PluginService][AllowNull, NotNull] public static IPluginLog PluginLog { get; private set; }
  [PluginService][AllowNull, NotNull] public static IChatGui ChatGui { get; private set; }

  public static void DisposeProxy() {
    servicesInstance.Dispose();
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed;

  protected virtual void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      FontContainer.Dispose();
      UI.Dispose();
      _isDisposed = true;
    }
  }
}
