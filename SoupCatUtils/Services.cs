using System;

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

  internal static DebugState FanDanceIV_DebugState { get; set; } = null!;
  internal static FanDanceIV_Module FanDanceIV_Module { get; set; } = null!;

  [PluginService][RequiredVersion("1.0")][AllowNull, NotNull] public static ClientState ClientState { get; private set; }
  [PluginService][RequiredVersion("1.0")][AllowNull, NotNull] public static CommandManager CommandManager { get; private set; }
  [PluginService][RequiredVersion("1.0")][AllowNull, NotNull] public static DalamudPluginInterface PluginInterface { get; private set; }
  [PluginService][RequiredVersion("1.0")][AllowNull, NotNull] public static Framework Framework { get; private set; }
  [PluginService][RequiredVersion("1.0")][AllowNull, NotNull] public static GameGui GameGui { get; private set; }
  [PluginService][RequiredVersion("1.0")][AllowNull, NotNull] public static GameNetwork GameNetwork { get; private set; }
  [PluginService][RequiredVersion("1.0")][AllowNull, NotNull] public static DataManager DataManager { get; private set; }
  [PluginService][RequiredVersion("1.0")][AllowNull, NotNull] public static ObjectTable ObjectTable { get; private set; }
  [PluginService][RequiredVersion("1.0")][AllowNull, NotNull] public static SigScanner SigScanner { get; private set; }
  [PluginService][RequiredVersion("1.0")][AllowNull, NotNull] public static TargetManager TargetManager { get; private set; }

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
