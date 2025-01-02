using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dalamud.Interface.Colors;
using Dalamud.Interface.Style;
using Dalamud.Plugin;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions.ImGui;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;
internal class PluginsSection : SectionBase {
  internal override string Name => $"Plugins##{nameof(SoupCatUtils)}";

  private Dictionary<IExposedPlugin, IExposedPlugin> PluginDevMap { get; } = [];

  public PluginsSection() : base() {
    Svc.PluginInterface.ActivePluginsChanged += this.ActivePluginsChanged;
    this.PluginsChanged();
  }

  private void ActivePluginsChanged(PluginListInvalidationKind kind, bool affectedThisPlugin) {
    this.PluginsChanged();
  }

  private void PluginsChanged() {
    this.PluginDevMap.Clear();
    foreach (var p1 in Svc.PluginInterface.InstalledPlugins) {
      if (Svc.PluginInterface.InstalledPlugins.FirstOrDefault(p2 => p2.InternalName.Equals(p1.InternalName + "2")) is IExposedPlugin plugin) {
        this.PluginDevMap.Add(p1, plugin);
      }
    }
  }

  public override void Draw() {
    base.Draw();

    if (ImGui.BeginTable($"##Plugins-ScrollingArea-{nameof(SoupCatUtils)}", 4, ImGuiTableFlags.ScrollY)) {
      ImGui.TableSetupScrollFreeze(4, 1);
      ImGui.TableSetupColumn("Remove Plugin",         ImGuiTableColumnFlags.WidthStretch, 150.0f);
      ImGui.TableSetupColumn("Remove Plugin Version", ImGuiTableColumnFlags.WidthStretch, 100.0f);
      ImGui.TableSetupColumn("Local Plugin",          ImGuiTableColumnFlags.WidthStretch, 150.0f);
      ImGui.TableSetupColumn("Local Plugin Version",  ImGuiTableColumnFlags.WidthStretch, 100.0f);
      ImGui.TableHeadersRow();
      foreach (var (nonDev, dev) in this.PluginDevMap) {
        try {
          ImGui.TableNextColumn();
          ImGui.Text(nonDev?.Name.ToString("Unknown Remove Plugin"));
          ImGui.TableNextColumn();
          using (var _ = ImGuiRaii.PushColor(ImGuiCol.Text, ImGuiColors.DalamudRed, VersionCompare(nonDev?.Version, dev?.Version, -1))) {
            ImGui.Text(nonDev?.Version?.ToString("null"));
          }
          ImGui.TableNextColumn();
          ImGui.Text(dev?.Name.ToString("Unknown Local Plugin"));
          ImGui.TableNextColumn();
          using (var _ = ImGuiRaii.PushColor(ImGuiCol.Text, ImGuiColors.DalamudRed, VersionCompare(dev?.Version, nonDev?.Version, -1))) {
            ImGui.Text(dev?.Version?.ToString("null"));
          }
        } catch (Exception exception) {
          Svc.Log.Error(exception, "Failed when making row in plugins table.");
        }
      }
      ImGui.EndTable();
    }
  }

  private static bool VersionCompare(Version? first, Version? second, int direction) {
    if (direction is not -1 or 1 or 0) {
      throw new ArgumentOutOfRangeException(nameof(direction));
    }

    if (first is null || second is null) {
      return false;
    }

    var major = InternalCompare(first.Major, second.Major);
    var minor = InternalCompare(first.Minor, second.Minor);
    var build = InternalCompare(first.Build, second.Build);
    var revision = InternalCompare(first.Revision, second.Revision);

    if (major == direction || minor == direction || build == direction || revision == direction) {
      return true;
    }

    return false;

    static int InternalCompare(int f, int s) {
      if ((f == 0 && s == -1) || (f == -1 && s == 0)) {
        return 0;
      }
      return f.CompareTo(s);
    }
  }

  public override void Dispose() {
    Svc.PluginInterface.ActivePluginsChanged -= this.ActivePluginsChanged;
    this.PluginDevMap.Clear();
    GC.SuppressFinalize(this);
  }

  public override void FrameworkUpdate() { }
}
