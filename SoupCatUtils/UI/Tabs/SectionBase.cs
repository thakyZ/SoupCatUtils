using Dalamud.Interface.Windowing;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI.Tabs;

public abstract class SectionBase : IDisposable {
  internal abstract string Name { get; }
  internal string DisplayName => Name.Split("##")[0];
  internal Window Parent { get; }

  protected SectionBase(Window parent) {
    this.Parent = parent;
  }

  public virtual void Draw() {
    CreateTitle(h2: DisplayName);
  }

  public abstract void Dispose();

  public abstract void FrameworkUpdate();

  public static void CreateTitle(string h1 = "", string h2 = "", string h3 = "") {
    float baseCursorPos = ImGui.GetCursorPosY();
    if (h1 != string.Empty) {
      using (var _ = FontContainer.PushFont("AXIS_36")) {
        ImGui.Text(h1);
      }
    }

    if (h2 != string.Empty) {
      if (h1 != string.Empty) {
        ImGui.SameLine();
        ImGui.Spacing();
        ImGui.SameLine();
      }

      using (var _ = FontContainer.PushFont("AXIS_18")) {
        ImGui.SetCursorPosY(baseCursorPos + (36 - 17));
        ImGui.Text(h2);
      }
    }

    if (h3 != string.Empty) {
      if (h2 != string.Empty || h1 != string.Empty) {
        ImGui.SameLine();
        ImGui.Spacing();
        ImGui.SameLine();
      }

      using (var _ = FontContainer.PushFont("AXIS_12")) {
        ImGui.SetCursorPosY(baseCursorPos + (36 - 10));
        ImGui.Text(h3);
      }
    }
  }
}
