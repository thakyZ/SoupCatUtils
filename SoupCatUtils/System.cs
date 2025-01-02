using System.Reflection;
using System.Resources;

using Dalamud.Interface.Windowing;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;
using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Configuration;
using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public static class System {
  internal static void Init() {
    System.Modules = [
      new Housing(),
      new FontContainer(),
      new States(),
      new FanDance4Module(),
    ];
    System.UI = new();
  }
  /// <summary>
  /// The instance object of this plugin.
  /// </summary>
  internal static Plugin PluginInstance { get; set; }
  /// <summary>
  /// The configuration of this plugin.
  /// </summary>
  internal static Config PluginConfig { get; set; }
  /// <summary>
  /// List of the plugin's modules.
  /// </summary>
  internal static ModulesList<ModuleBase?> Modules { get; private set; }
  /// <summary>
  /// State worker
  /// </summary>
  internal static States? States => Modules.Get<States>();
  /// <summary>
  /// The instance of the plugin ui.
  /// </summary>
  internal static MainWindow UI { get; set; }
  /// <summary>
  /// An instance of a resource manager for windows resources.
  /// </summary>
  internal static ResourceManager ResourceManager { get; } = new ResourceManager(Plugin.Name + ".Properties.Resources", Assembly.GetExecutingAssembly());
  /// <summary>
  /// The window system of the plugin.
  /// </summary>
  internal static WindowSystem WindowSystem { get; } = new(Plugin.Name.Replace(" ",string.Empty));

  internal static List<SectionBase> GenerateSectionBases() {
    List<SectionBase> output = [];
    try {
      var assembly = Assembly.GetExecutingAssembly();
      foreach (var sectionBaseType in assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(SectionBase)))) {
        var constrcutor = sectionBaseType.GetConstructor([]);
        if (constrcutor is null) {
          Svc.Log.Warning("Constructor of type {0} doesn't have a parameterless constructor.", sectionBaseType.Name);
          continue;
        }
        if (constrcutor.Invoke([]) is not SectionBase sectionBase) {
          Svc.Log.Warning("Failed to construct type of {0}.", sectionBaseType.Name);
          continue;
        }
        output.Add(sectionBase);
      }
    } catch (Exception exception) {
      Svc.Log.Error(exception, "Failed to create section bases.");
    }
    return output;
  }
}
