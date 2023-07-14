using System;
using System.Linq;
using System.Numerics;

using Dalamud.Game;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Logging;

using ECommons.SplatoonAPI;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;
internal sealed class FanDanceIV_Module : IDisposable {
  private readonly DebugState _debugState;

  private const float MaxDistance = 15f;
  private const long OnTerritoryChange = -2;

  private static class Buffs {
    public const ushort FourFoldFanDance = 2699;
  }

  public FanDanceIV_Module() {
    _debugState = Services.FanDanceIV_DebugState;

    PluginLog.Information("Initializing splatoon");
    //ECommonsMain.Init(pluginInterface, dalamudPlugin, ECommons.Module.SplatoonAPI);
    Splatoon.SetOnConnect(SplatoonOnConnect);
  }

  void SplatoonOnConnect() {
    Update(Services.Framework);
    Services.Framework.Update += Update;
  }

  static bool AreInRange(float range, Vector3 v1, Vector3 v2, float v1H = 0.0f, float v2H = 0.0f) {
    return range > Vector3.Distance(new Vector3(v1.X, v1.Z, v1.Y), new Vector3(v2.X, v2.Z, v2.Y)) - v1H - v2H;
  }

  UInt32 Vector4FromRGBA(UInt32 rgba) {
		string hex = "";
		var array = rgba.ToString("X4").SplitInParts(2).ToArray();
		hex = array[3] + array[2] + array[1] + array[0];
		return Convert.ToUInt32(hex, 16);
  }

  internal void Update(Framework framework) {
    //if (Environment.TickCount64 > long.MaxValue) {
    try {
      Splatoon.RemoveDynamicElements(ToLayerName());
      if (Services.PluginConfig.SplatoonFanDanceIV) {
        PlayerCharacter? localPlayer = Services.ClientState.LocalPlayer;
        if (localPlayer is not null) {
          GameObject? target = Services.TargetManager.Target;
          if (target is not null) {
            Vector3 playerPos = localPlayer.Position;
            Vector3 targetPos = target.Position;
            if (localPlayer.StatusList.Any(x => x.StatusId == Buffs.FourFoldFanDance) && AreInRange(MaxDistance, playerPos, targetPos, localPlayer.HitboxRadius, target.HitboxRadius)) {
              Splatoon.AddDynamicElement(ToLayerName(), new Element(ElementType.LineBetweenTwoFixedCoordinates) {
                refX = playerPos.X,
                refY = playerPos.Z, // z and y are swapped
                refZ = playerPos.Y,
                offX = targetPos.X,
                offY = targetPos.Z,
                offZ = playerPos.Y,
                color = Vector4FromRGBA(0x563396C8),
                thicc = 10,
                radius = 0
              }, new[] { -1, OnTerritoryChange });
              _debugState.DebugMessage = $"Working...";
            } else {
              _debugState.DebugMessage = $"pl:({playerPos.X},{playerPos.Z},{playerPos.Y})\nta:({targetPos.X},{targetPos.Z},{playerPos.Y})";
            }
          } else {
            _debugState.DebugMessage = "target is null";
          }
        } else {
          _debugState.DebugMessage = "localPlayer is null";
        }
      } else {
        _debugState.DebugMessage = "Not Enabled";
      }
    } catch (Exception e) {
      // not relevant
      _debugState.DebugMessage = $"Could not create splatoon element {ToLayerName()}";
      _debugState.SetFromException(e);
    }
    //}
  }

  private string ToLayerName() {
    return $"SoupCatUtils.{this.GetType().Name}";
  }

  private void Disconnect() {
    Services.Framework.Update -= Update;
  }

  public void CheckConnected() {
    if (!Splatoon.IsConnected()) {
      Disconnect();
    }
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private bool _isDisposed;

  private void Dispose(bool disposing) {
    if (disposing && !_isDisposed) {
      PluginLog.Information("Disposing splatoon rendered");
      try {
        Splatoon.RemoveDynamicElements(ToLayerName());
        Disconnect();
      } catch (Exception) { }
      _isDisposed = true;

      //ECommonsMain.Dispose();
    }
  }
}
