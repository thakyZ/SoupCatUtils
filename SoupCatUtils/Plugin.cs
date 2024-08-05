using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;

using ECommons;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Configuration;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;
public class Plugin : IDalamudPlugin {
  /// <summary>
  /// The private internal instance for the name of the plugin
  /// </summary>
  public const string StaticName = "${Title}";

  /// <summary>
  /// The private internal instance for the name of the plugin
  /// </summary>
  public const string StaticAuthor = "${Authors}";

  /// <summary>
  /// The name of the plugin
  /// </summary>
  public string Name => StaticName;

  /// <summary>
  /// The plugin's main command name.
  /// </summary>
  private const string CommandName = "/soupcat";

  public Plugin(IDalamudPluginInterface pluginInterface) {
    System.PluginInstance = this;

    ECommonsMain.Init(pluginInterface, System.PluginInstance, Module.SplatoonAPI);
    System.Init();

    System.PluginConfig = Svc.PluginInterface.GetPluginConfig() as Config ?? new Config();

    System.WindowSystem.AddWindow(System.UI);

    Svc.Commands.AddHandler(CommandName, new CommandInfo(OnCommand) {
      HelpMessage = "The soup cat utilities command",
      ShowInHelp = true
    });

    Svc.PluginInterface.UiBuilder.Draw += DrawUI;
    Svc.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
  }

  public void Dispose() {
    System.PluginConfig.Save();
    System.WindowSystem.RemoveAllWindows();
    Svc.PluginInterface.UiBuilder.Draw -= DrawUI;
    Svc.PluginInterface.UiBuilder.OpenConfigUi -= DrawConfigUI;
    Svc.Commands.RemoveHandler(CommandName);
    System.Modules.Dispose();
    System.UI.Dispose();
    ECommonsMain.Dispose();
    GC.SuppressFinalize(this);
  }

  private void OnCommand(string command, string args) {
    System.UI.IsOpen ^= true;
  }

  private void DrawUI() {
    System.WindowSystem.Draw();
  }

  private void DrawConfigUI() {
    System.UI.IsOpen = true;
  }
}
