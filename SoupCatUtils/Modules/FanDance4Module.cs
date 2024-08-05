using System.Numerics;

using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin.Services;

using ECommons.SplatoonAPI;

using Serilog.Events;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

internal sealed class FanDance4Module : ModuleBase {
  internal static DebugState DebugState { get; } = new();

  private const float MaxDistance = 15f;
  private const long OnTerritoryChange = -2;

  private static class Buffs {
    public const ushort FourFoldFanDance = 2699;
  }

  public FanDance4Module() : base(true) {
    Svc.Log.Information("Initializing splatoon");
    Splatoon.SetOnConnect(SplatoonOnConnect);
  }

  internal void SplatoonOnConnect() {
    Update(Svc.Framework);
    Svc.Framework.Update += this.Update;
  }

  private static bool AreInRange(float range, Vector3 v1, Vector3 v2, float v1H = 0.0f, float v2H = 0.0f) {
    return range > Vector3.Distance(new Vector3(v1.X, v1.Z, v1.Y), new Vector3(v2.X, v2.Z, v2.Y)) - v1H - v2H;
  }

  private static uint Vector4FromRGBA(uint rgba) {
		var array = rgba.ToString("X4").SplitInParts(2).ToArray();
		string hex = array[3] + array[2] + array[1] + array[0];
		return Convert.ToUInt32(hex, 16);
  }

  internal override void Update(IFramework framework) {
    try {
      Splatoon.RemoveDynamicElements(ToLayerName());
      if (System.PluginConfig.SplatoonFanDanceIV) {
        IPlayerCharacter? localPlayer = Svc.ClientState.LocalPlayer;
        if (localPlayer is not null) {
          IGameObject? target = Svc.Targets.Target;
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
              }, (long[])[ -1L, OnTerritoryChange ]);
              DebugState.DebugMessage = "Working...";
            } else {
              DebugState.DebugMessage = $"pl:({playerPos.X},{playerPos.Z},{playerPos.Y})\nta:({targetPos.X},{targetPos.Z},{playerPos.Y})";
            }
          } else {
            DebugState.DebugMessage = "target is null";
          }
        } else {
          DebugState.DebugMessage = "localPlayer is null";
        }
      } else {
        DebugState.DebugMessage = "Not Enabled";
      }
    } catch (Exception e) {
      // not relevant
      DebugState.DebugMessage = $"Could not create splatoon element {ToLayerName()}";
      DebugState.SetFromException(e);
    }
  }

  private string ToLayerName() {
    return $"SoupCatUtils.{this.GetType().Name}";
  }

  private void Disconnect() {
    Svc.Framework.Update -= Update;
  }

  public void CheckConnected() {
    if (!Splatoon.IsConnected()) {
      Disconnect();
    }
  }

  internal override void DisposeManaged() {
    Svc.Log.Information("Disposing splatoon rendered");
    try {
      Splatoon.RemoveDynamicElements(ToLayerName());
      Disconnect();
    } catch (Exception exception) {
      if (Svc.Log.MinimumLogLevel == LogEventLevel.Debug) {
        Svc.Log.Error(exception, "Failed to dispose of splatoon.");
      }
    }
  }
}
