using System.ComponentModel;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

/* Territory Type Key
  * 339 => Mist
  * 340 => The Lavender Beds
  * 341 => The Goblet
  * 641 => Shirogane
  * 979 => The Empyreum
  */
/// <summary>
/// Territory Type Key.
/// <remarks>Can be passed as a uint</remarks>
/// <list type="table">
///   <listheader>
///     <term>ID</term>
///     <description>Correlation</description>
///   </listheader>
///   <item>
///     <term>339</term>
///     <description>Mist</description>
///   </item>
///   <item>
///     <term>340</term>
///     <description>The Lavender Beds</description>
///   </item>
///   <item>
///     <term>341</term>
///     <description>The Goblet</description>
///   </item>
///   <item>
///     <term>641</term>
///     <description>Shirogane</description>
///   </item>
///   <item>
///     <term>979</term>
///     <description>The Empyreum</description>
///   </item>
/// </list>
/// </summary>
public enum TerritoryTypes {
  [Description("Mist")]
  Mist = 339,
  [Description("The Lavender Beds")]
  LavenderBeds = 340,
  [Description("The Goblet")]
  Goblet = 341,
  [Description("Shirogane")]
  Shirogane = 641,
  [Description("The Empyreum")]
  Empyreum = 979
}
