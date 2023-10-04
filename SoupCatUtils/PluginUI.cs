using System;
using System.Collections.Generic;
using System.Linq;

using Dalamud.Game.Command;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Dalamud.IoC;
using Dalamud.Plugin;

using FFXIVClientStructs.FFXIV.Common.Math;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;

// It is good to have this be disposable in general, in case you ever need it to do any cleanup
public class PluginUI : Window, IDisposable {
  private static ImGuiWindowFlags WindowFlags { get => ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse; }
  public static string Name { get => $"{Plugin.StaticName} Settings"; }

  private readonly List<SectionBase> Sections;

  public PluginUI() : base(Name, WindowFlags) {
    Size = new Vector2(630, 500) * ImGuiHelpers.GlobalScale;
    SizeCondition = ImGuiCond.Always;
    Sections = new() {
      new AboutSection(),
      new HousingSection(),
      new SPLSection(),
      new TweaksSection()
    };
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
