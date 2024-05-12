using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

using ECommons.DalamudServices.Legacy;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

internal sealed class Chat {
  public Chat() {
  }

  public void Error(string e) {
    Services.ChatGui.PrintChat(new XivChatEntry {
      Message = new SeStringBuilder()
            .AddUiForeground($"[{Plugin.StaticName}] ", 16)
            .AddText(e).Build(),
      Type = XivChatType.Urgent
    });
  }

  public void Message(string message) {
    Services.ChatGui.Print(new SeStringBuilder()
        .AddUiForeground($"[{Plugin.StaticName}] ", 57)
        .AddText(message).Build());
  }

  public void UnformattedMessage(string message) {
    Services.ChatGui.Print(message);
  }
}
