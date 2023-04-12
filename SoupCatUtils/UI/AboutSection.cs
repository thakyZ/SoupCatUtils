using System;

using Dalamud.Interface;
using Dalamud.Interface.Windowing;

using FFXIVClientStructs.FFXIV.Common.Math;

using ImGuiNET;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;

public class AboutSection : SectionBase, IDisposable {
  public new string Name { get; set; } = "About##SoupCatUtils";
  protected override string NameImpl {
    get { return Name; }
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed = false;

  protected virtual void Dispose(bool disposing) {
    if (!_isDisposed && disposing) {
      _isDisposed = true;
    }
  }

  public override void Draw() {
    CreateTitle("About", Plugin.StaticName, $"by: {Plugin.StaticAuthor}");
  }
}
