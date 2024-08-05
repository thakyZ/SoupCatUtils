using Dalamud.Interface.ManagedFontAtlas;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

public class FontScope : IDisposable {
  private readonly IFontHandle? _handle;

  public FontScope(IFontHandle? handle = null) {
    _handle = handle;
    _handle?.Push();
  }

  public void Dispose() {
    _handle?.Pop();
    GC.SuppressFinalize(this);
  }
}
