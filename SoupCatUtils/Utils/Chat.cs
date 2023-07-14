using Dalamud.Game.Gui;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

internal sealed class Chat {
  private readonly ChatGui _chatGui;

  public Chat(ChatGui chatGui) {
    _chatGui = chatGui;
  }

  public void Error(string e) {
    _chatGui.PrintChat(new XivChatEntry {
      Message = new SeStringBuilder()
            .AddUiForeground($"[{Plugin.StaticName}] ", 16)
            .AddText(e).Build(),
      Type = XivChatType.Urgent
    });
  }

  public void Message(string message) {
    _chatGui.Print(new SeStringBuilder()
        .AddUiForeground($"[{Plugin.StaticName}] ", 57)
        .AddText(message).Build());
  }

  public void UnformattedMessage(string message) {
    _chatGui.Print(message);
  }
}
