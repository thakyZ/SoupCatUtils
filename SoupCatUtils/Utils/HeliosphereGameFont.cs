using System;
using System.Collections.Generic;

using Dalamud.Interface.GameFonts;
using Dalamud.Logging;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;
/*****************************************************************
 * Code borrowed from https://github.com/heliosphere-xiv/plugin/ *
 *****************************************************************/
class HeliosphereGameFont : IDisposable {
  private Plugin Plugin { get; }
  private readonly Dictionary<(uint, bool), GameFontHandle> _fonts = new();

  internal HeliosphereGameFont(Plugin plugin) {
    Plugin = plugin;
  }

  public void Dispose() {
    foreach (var handle in _fonts.Values) {
      handle.Dispose();
    }
  }

  //internal GameFontHandle? this[uint size, bool italic] {
  //  get {
  //    GameFontHandle handle;
  //    if (_fonts.ContainsKey((size, italic))) {
  //      handle = _fonts[(size, italic)];
  //    } else {
  //      handle = Services.PluginInterface.UiBuilder.GetGameFontHandle(new GameFontStyle(GameFontFamily.Axis, size) {
  //        Italic = italic,
  //      });
  //      _fonts[(size, italic)] = handle;
  //    }
  //
  //    return handle.Available ? handle : null;
  //  }
  //}

  internal GameFontHandle? this[uint size, bool italic] {
    get {
      PluginLog.Information("Loading Get 1");
      GameFontHandle handle;
      PluginLog.Information("Loading Get 2");
      if (_fonts.ContainsKey((size, italic))) {
        PluginLog.Information("Loading Old 1");
        handle = _fonts[(size, italic)];
        PluginLog.Information("Loading Old 2");
      } else {
        PluginLog.Information("Loading New 1");
        handle = Services.PluginInterface.UiBuilder.GetGameFontHandle(new GameFontStyle(GameFontFamily.Axis, size) {
          Italic = italic,
        });
        PluginLog.Information("Loading New 2");
        _fonts.Add((size, italic), handle);
        PluginLog.Information("Loading New 3");
      }
      PluginLog.Information("Loading Get 3");
      PluginLog.Information($"Available: {handle.Available}");
      PluginLog.Information($"handle != null: {handle != null}");
      PluginLog.Information($"handle: {handle}");
      PluginLog.Information($"return: {(handle != null && handle.Available ? "handle" : "null")}");

      return handle.Available ? handle : null;
    }
  }

  internal GameFontHandle? this[uint size] => this[size, false];
}
