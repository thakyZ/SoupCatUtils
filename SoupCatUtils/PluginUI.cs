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

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils;

// It is good to have this be disposable in general, in case you ever need it to do any cleanup
public class PluginUI : Window, IDisposable {
  private static ImGuiWindowFlags WindowFlags { get => ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse; }
  public static string Name { get => $"{Plugin.StaticName} Settings"; }

  private readonly AboutSection aboutSection;
  private readonly HousingSection housingSection;
  private readonly HeliosphereFontLoadingTestSection heliosphereFontLoadingTestSection;

  public PluginUI() : base(Name, WindowFlags) {
    Size = new Vector2(630, 500) * ImGuiHelpers.GlobalScale;
    SizeCondition = ImGuiCond.Always;
    aboutSection = new AboutSection();
    housingSection = new HousingSection();
    heliosphereFontLoadingTestSection = new HeliosphereFontLoadingTestSection();
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed;

  protected virtual void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      aboutSection.Dispose();
      housingSection.Dispose();
      heliosphereFontLoadingTestSection.Dispose();
      _isDisposed = true;
    }
  }

  public override void Draw() {
    if (ImGui.BeginTabBar("##SoupCatUtilsTabs", ImGuiTabBarFlags.None)) {
      if (ImGui.BeginTabItem(aboutSection.Name)) {
        aboutSection.Draw();
        ImGui.EndTabItem();
      }

      if (ImGui.BeginTabItem(housingSection.Name)) {
        housingSection.Draw();
        ImGui.EndTabItem();
      }

      if (ImGui.BeginTabItem(heliosphereFontLoadingTestSection.Name)) {
        heliosphereFontLoadingTestSection.Draw();
        ImGui.EndTabItem();
      }
      ImGui.EndTabBar();
    }
    ImGui.End();
  }
}
