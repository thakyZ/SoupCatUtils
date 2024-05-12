using System;

using ImGuiNET;

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
    Services.FontContainer.PushFont("AXIS", 36);
    ImGui.Text(h1);
    ImGui.PopFont();

    if (h2 != string.Empty) {
      ImGui.SameLine();
      ImGui.Spacing();
      ImGui.SameLine();

      Services.FontContainer.PushFont("AXIS", 18);
      ImGui.SetCursorPosY(baseCursorPos + (36 - 17));
      ImGui.Text(h2);
      ImGui.PopFont();

      if (h3 != string.Empty) {
        ImGui.SameLine();
        ImGui.Spacing();
        ImGui.SameLine();

        Services.FontContainer.PushFont("AXIS", 12);
        ImGui.SetCursorPosY(baseCursorPos + (36 - 10));
        ImGui.Text(h3);
        ImGui.PopFont();
      }
    }
  }

  public static void CreateTitle(string h2 = "") {
    Services.FontContainer.PushFont("AXIS", 18);
    ImGui.Text(h2);
    ImGui.PopFont();
  }
}
