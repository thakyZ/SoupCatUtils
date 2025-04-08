using System.Diagnostics.CodeAnalysis;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;

using FFXIVClientStructs.FFXIV.Common.Math;

using ImGuiNET;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;

// It is good to have this be disposable in general, in case you ever need it to do any cleanup
public class MainWindow : Window, IDisposable {
  private static ImGuiWindowFlags WindowFlags { get => ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse; }
  public static string Name { get => $"{Plugin.Name} Settings"; }

  private List<SectionBase> Sections { get; } = System.GenerateSectionBases();

  public MainWindow() : base(Name, WindowFlags) {
    Size = new Vector2(630, 500) * ImGuiHelpers.GlobalScale;
    SizeCondition = ImGuiCond.Always;
  }

  public override void OnClose() {
    System.PluginConfig.Save();
    base.OnClose();
  }

  public void Dispose() {
    foreach (SectionBase section in Sections) {
      section.Dispose();
    }
    GC.SuppressFinalize(this);
  }

  [SuppressMessage("Minor Code Smell", "S3267:Loops should be simplified with \"LINQ\" expressions", Justification = "<Pending>")]
  public override void Draw() {
    try {
      if (ImGui.BeginTabBar("A##SoupCatUtilsTabs", ImGuiTabBarFlags.NoTooltip)) {
        foreach (SectionBase section in Sections) {
          if (ImGui.BeginTabItem(section.Name)) {
            try {
              section.Draw();
            } catch (Exception exception) {
              Svc.Log.Error(exception, $"Failed to draw section with name {section.Name}.");
            }
            ImGui.EndTabItem();
          }
        }
        ImGui.EndTabBar();
      }
    } catch (Exception exception) {
      Svc.Log.Error(exception, $"Failed to draw tab bar.");
    }
  }
}
