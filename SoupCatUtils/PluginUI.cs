using System;
using System.Collections.Generic;

using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;

using FFXIVClientStructs.FFXIV.Common.Math;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;

// It is good to have this be disposable in general, in case you ever need it to do any cleanup
public class PluginUI : Window, IDisposable {
  private static ImGuiWindowFlags WindowFlags { get => ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse; }
  public static string Name { get => $"{Plugin.StaticName} Settings"; }

  private readonly List<SectionBase> Sections;

  public PluginUI() : base(Name, WindowFlags) {
    Size = new Vector2(630, 500) * ImGuiHelpers.GlobalScale;
    SizeCondition = ImGuiCond.Always;
    Sections = [
      new AboutSection(),
      new HousingSection(),
      new SquidSection(),
      new TweaksSection()
    ];
  }

  public override void OnClose() {
    Services.PluginConfig.Save();
    base.OnClose();
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed;

  protected void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      foreach (SectionBase section in Sections) {
        section.Dispose();
      }
      _isDisposed = true;
    }
  }

  public override void Draw() {
    if (ImGui.BeginTabBar("##SoupCatUtilsTabs", ImGuiTabBarFlags.None)) {
      foreach (SectionBase section in Sections) {
        if (ImGui.BeginTabItem(section.Name)) {
          section.Draw();
          ImGui.EndTabItem();
        }
      }
      ImGui.EndTabBar();
    }
    ImGui.End();
  }
}
