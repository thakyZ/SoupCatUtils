using System;

using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;

using ECommons;
using ECommons.Schedulers;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

using Module = ECommons.Module;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;
public class Plugin : IDalamudPlugin {
  /// <summary>
  /// The private internal instance for the name of the plugin
  /// </summary>
  public const string StaticName = "$(Title)";

  /// <summary>
  /// The private internal instance for the name of the plugin
  /// </summary>
  public const string StaticAuthor = "$(Authors)";

  /// <summary>
  /// The name of the plugin
  /// </summary>
  public string Name => StaticName;

  /// <summary>
  /// The plugin's main command name.
  /// </summary>
  private const string CommandName = "/soupcat";

  /// <summary>
  /// The window system of the plugin.
  /// </summary>
  internal WindowSystem WindowSystem = new(StaticName.Replace(" ",string.Empty));

  public Plugin(DalamudPluginInterface pluginInterface) {
    Services.Initialize(pluginInterface);

    Services.PluginInstance = this;

    ECommonsMain.Init(Services.PluginInterface, Services.PluginInstance, Module.SplatoonAPI);

    Services.PluginConfig = Services.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
    Services.FontContainer = new FontContainer();

    Services.UI = new PluginUI();
    WindowSystem.AddWindow(Services.UI);

    Services.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand) {
      HelpMessage = "The soup cat utilities command",
      ShowInHelp = true
    });

    Services.PluginInterface.UiBuilder.Draw += DrawUI;
    Services.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;

    Services.Tools.Housing = new Tools.Housing();

    _ = new TickScheduler(delegate {
      Services.FanDance4DebugState = new();
      Services.FanDance4Module = new();
    });
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed;

  protected virtual void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      Services.PluginConfig.Save();
      WindowSystem.RemoveAllWindows();
      Services.PluginInterface.UiBuilder.Draw -= DrawUI;
      Services.PluginInterface.UiBuilder.OpenConfigUi -= DrawConfigUI;
      Services.CommandManager.RemoveHandler(CommandName);
      Services.DisposeProxy();
      ECommonsMain.Dispose();
      _isDisposed = true;
    }
  }

  private void OnCommand(string command, string args) {
    Services.UI.IsOpen ^= true;
  }

  private void DrawUI() {
    WindowSystem.Draw();
  }

  private void DrawConfigUI() {
    Services.UI.IsOpen = true;
  }
}
