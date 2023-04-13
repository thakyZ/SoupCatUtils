using System;
using System.Collections.Generic;
using System.Linq;

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
      GameFontHandle handle;
      if (_fonts.ContainsKey((size, italic))) {
        handle = _fonts[(size, italic)];
      } else {
        handle = Services.PluginInterface.UiBuilder.GetGameFontHandle(new GameFontStyle(GameFontFamily.Axis, size) {
          Italic = italic,
        });
        _fonts.Add((size, italic), handle);
      }

      #region Debugging
      PluginLog.Information($"Available: {handle.Available}");
      PluginLog.Information($"handle != null: {handle != null}");
      PluginLog.Information($"handle: {handle}");
      PluginLog.Information($"return: {(handle != null && handle.Available ? "handle" : "null")}");
      PluginLog.Information($"_fonts.Keys.Count: {_fonts.Keys.Count}");
      PluginLog.Information($"_fonts.Count: {_fonts.Count}");
      try {
        PluginLog.Information($"_fonts.Keys[0]: ({_fonts.Keys.ElementAt(0).Item1}, {_fonts.Keys.ElementAt(0).Item2})");
      } catch {
        PluginLog.Information($"_fonts.Keys[0]: Failed!");
      }
      try {
        PluginLog.Information($"_fonts[0]: {_fonts[_fonts.Keys.ElementAt(0)]}");
      } catch {
        PluginLog.Information($"_fonts[0]: Failed!");
      }
      PluginLog.Information($"_fonts[0] != null: {_fonts[_fonts.Keys.ElementAt(0)] != null}");
      try {
        if (_fonts[_fonts.Keys.ElementAt(0)] != null) {
          PluginLog.Information($"_fonts[0].Available: {_fonts[_fonts.Keys.ElementAt(0)].Available}");
        }
      } catch {
        PluginLog.Information($"_fonts[0].Available: Failed!");
      }
      #endregion

      return handle.Available ? handle : null;
    }
  }

  internal GameFontHandle? this[uint size] => this[size, false];
}
