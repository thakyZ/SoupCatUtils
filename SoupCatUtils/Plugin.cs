using System;

using Dalamud.Game.Command;
using Dalamud.Interface.GameFonts;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;
public class Plugin : IDalamudPlugin {
  /// <summary>
  /// The private internal instance for the name of the plugin
  /// </summary>
  public const string StaticName = "Soup Cat Utils";

  /// <summary>
  /// The private internal instance for the name of the plugin
  /// </summary>
  public const string StaticAuthor = "Neko Boi Nick";

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
  internal WindowSystem WindowSystem = new(StaticName.Replace(" ",String.Empty));

  public Plugin([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface) {
    Services.Initialize(pluginInterface);

    Services.PluginConfig = Services.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
    Services.PluginConfig.Initialize(Services.PluginInterface);

    Services.PluginInstance = this;
    Services.FontContainer = new FontContainer();

    Services.UI = new PluginUI();
    WindowSystem.AddWindow(Services.UI);

    Services.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand) {
      HelpMessage = "The soup cat util command",
      ShowInHelp = true
    });

    Services.PluginInterface.UiBuilder.Draw += DrawUI;
    Services.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;

    Services.Tools.Housing = new Tools.Housing();
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed;

  protected virtual void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      WindowSystem.RemoveAllWindows();
      Services.PluginInterface.UiBuilder.Draw -= DrawUI;
      Services.PluginInterface.UiBuilder.OpenConfigUi -= DrawConfigUI;
      Services.CommandManager.RemoveHandler(CommandName);
      Services.DisposeProxy();
      _isDisposed = true;
    }
  }

  private void OnCommand(string command, string args) {
    Services.UI.IsOpen ^= true;
    // in response to the slash command, just display our main ui
  }

  private void DrawUI() {
    WindowSystem.Draw();
  }

  private void DrawConfigUI() {
    Services.UI.IsOpen = true;
  }
}
