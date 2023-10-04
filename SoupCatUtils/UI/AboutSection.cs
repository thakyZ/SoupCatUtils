using System;

using Dalamud.Interface;
using Dalamud.Interface.Windowing;

using FFXIVClientStructs.FFXIV.Common.Math;

using ImGuiNET;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;

public class AboutSection : SectionBase, IDisposable {
  public new string Name { get; set; } = "About##SoupCatUtils";
  protected override string NameImplementation {
    get { return Name; }
  }

  protected override void DisposeImpl() {
  }

  public override void Draw() {
    CreateTitle("About", Plugin.StaticName, $"by: {Plugin.StaticAuthor}");
  }
}
