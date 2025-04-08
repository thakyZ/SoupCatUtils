using System.ComponentModel;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Utility;

using ECommons;
using ECommons.ExcelServices;

using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;

using Lumina.Excel.Sheets;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Configuration;

using static Dalamud.Interface.Utility.Raii.ImRaii;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;
public class Plugin : IDalamudPlugin {
  /// <summary>
  /// The private internal instance for the name of the plugin
  /// </summary>
  private const string _STATIC_NAME = "Soup Cat Utils";

  /// <summary>
  /// The private internal instance for the name of the plugin author
  /// </summary>
  private const string _STATIC_AUTHOR = "Neko Boi Nick";

  /// <summary>
  /// The name of the plugin
  /// </summary>
  public static string Name => _STATIC_NAME;

  /// <summary>
  /// The name of the plugin author
  /// </summary>
  public static string Author => _STATIC_AUTHOR;

  /// <summary>
  /// The plugin's main command name.
  /// </summary>
  private const string _COMMAND_NAME = "/soupcat";

  public Plugin(IDalamudPluginInterface pluginInterface) {
    System.PluginInstance = this;

    ECommonsMain.Init(pluginInterface, System.PluginInstance, Module.SplatoonAPI);
    System.Init();

    System.PluginConfig = Svc.PluginInterface.GetPluginConfig() as Config ?? new Config();

    System.PluginConfig.PropertyChanged += PluginConfig_PropertyChanged;

    if (System.PluginConfig.EnableUseBestPotionCommand) {
      Svc.Log.Information($"AddHandler (ctor) {nameof(BestPotionCommand)}");
      Svc.Commands.AddHandler("/bpotion", new CommandInfo(BestPotionCommand));
    }

    System.WindowSystem.AddWindow(System.UI);

    Svc.Commands.AddHandler(_COMMAND_NAME, new CommandInfo(OnCommand) {
      HelpMessage = "The soup cat utilities command",
      ShowInHelp = true
    });

    Svc.PluginInterface.UiBuilder.Draw += DrawUI;
    Svc.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
  }

  private void PluginConfig_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
    if (e.PropertyName?.Equals(nameof(Config.EnableUseBestPotionCommand)) == true) {
      if (System.PluginConfig.EnableUseBestPotionCommand) {
        Svc.Log.Information($"AddHandler {nameof(BestPotionCommand)}");
        Svc.Commands.AddHandler("/bpotion", new CommandInfo(BestPotionCommand));
      } else {
        Svc.Log.Information($"RemoveHandler {nameof(BestPotionCommand)}");
        Svc.Commands.RemoveHandler("/bpotion");
      }
    }
  }

  // private static IEnumerable<string>? _potionNamesIds;
  private readonly static IEnumerable<uint> _potionIds = [
    4_551,  // Potion
    4_552,  // Hi-Potion
    4_553,  // Mega-Potion
    4_554,  // X-Potion
    13_637, // Max-Potion
    23_167, // Super-Potion
    38_956, // Hyper-Potion
  ];

  private void BestPotionCommand(string command, string arguments) {
    Svc.Log.Information($"Ran {nameof(BestPotionCommand)}");
    try {
      List<uint> items = [];
      unsafe {
        var manager = InventoryManager.Instance();
        // ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (manager == null) {
          return;
        }
        for (int i = 0; i < 4; i++) {
          var inventoryType = i switch {
            1 => InventoryType.Inventory2,
            2 => InventoryType.Inventory3,
            3 => InventoryType.Inventory4,
            _ => InventoryType.Inventory1,
          };
          var container = manager->GetInventoryContainer(inventoryType);
          if (container == null || container->Loaded == 0) {
            continue;
          }
          for (uint j = 0; j < container->Size; j++) {
            var item = &container->Items[j];
            uint itemId = item->ItemId;
            if (_potionIds?.Contains(itemId) == true) {
              var flags = item->Flags;
              if ((flags & InventoryItem.ItemFlags.HighQuality) != 0) {
                items.Add(itemId + 1_000_000);
              } else {
                items.Add(itemId);
              }
            }
          }
        }
        items.Sort((a, b) => (a > 1_000_000 ? a - 1_000_000 : a).CompareTo(b > 1_000_000 ? b - 1_000_000 : b));

        var agentInventoryContext = AgentInventoryContext.Instance();
        if (agentInventoryContext == null) {
          return;
        }
        uint id = items[^1];
        // ReSharper restore ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        agentInventoryContext->UseItem(id);
      }
    } catch (Exception exception) {
      Svc.Log.Error(exception, "Failed to execute the command /bpotion");
    }
  }

  public void Dispose() {
    System.PluginConfig.Save();
    System.WindowSystem.RemoveAllWindows();
    Svc.PluginInterface.UiBuilder.Draw -= DrawUI;
    Svc.PluginInterface.UiBuilder.OpenConfigUi -= DrawConfigUI;
    Svc.Commands.RemoveHandler(_COMMAND_NAME);
    try {
      if (System.PluginConfig.EnableUseBestPotionCommand) {
        Svc.Log.Information($"RemoveHandler (dispose (if)) {nameof(BestPotionCommand)}");
        Svc.Commands.RemoveHandler("bpotion");
      }
      Svc.Log.Information($"RemoveHandler (dispose) {nameof(BestPotionCommand)}");
      Svc.Commands.RemoveHandler("bpotion");
    } catch { /* Ignore */}
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
