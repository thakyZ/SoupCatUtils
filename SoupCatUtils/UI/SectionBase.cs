using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

using Dalamud.Game.Gui;
using Dalamud.Interface;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;

public abstract class SectionBase : IDisposable {
  public string Name { get { return NameImplementation; } }
  protected abstract string NameImplementation { get; }

  public virtual void Draw() {

  }

  protected abstract void DisposeImpl();

  private bool _isDisposed;

  public void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      DisposeImpl();
      _isDisposed = true;
    }
  }

  public void Dispose() {
    this.Dispose(true);
    GC.SuppressFinalize(this);
  }

  public virtual void FrameworkUpdate() {

  }

  public static void CreateTitle(string h1, string h2 = "", string h3 = "") {
    float baseCursorPos = ImGui.GetCursorPosY();
    ImGui.PushFont(Services.FontContainer.GetFont("AXIS", 36));
    ImGui.Text(h1);
    ImGui.PopFont();

    if (h2 != string.Empty) {
      ImGui.SameLine();
      ImGui.Spacing();
      ImGui.SameLine();

      ImGui.PushFont(Services.FontContainer.GetFont("AXIS", 18));
      ImGui.SetCursorPosY(baseCursorPos + (36 - 17));
      ImGui.Text(h2);
      ImGui.PopFont();

      if (h3 != string.Empty) {
        ImGui.SameLine();
        ImGui.Spacing();
        ImGui.SameLine();

        ImGui.PushFont(Services.FontContainer.GetFont("AXIS", 12));
        ImGui.SetCursorPosY(baseCursorPos + (36 - 10));
        ImGui.Text(h3);
        ImGui.PopFont();
      }
    }
  }

  public static void CreateTitle(string h2 = "") {
    ImGui.PushFont(Services.FontContainer.GetFont("AXIS", 18));
    ImGui.Text(h2);
    ImGui.PopFont();
  }
}
