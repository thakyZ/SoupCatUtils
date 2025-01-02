using System.ComponentModel;
using Lumina.Excel.Sheets;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

/// <summary>
/// Proxy class with description for the enum <see cref="GameFontFamilyAndSize" />.
/// </summary>
internal enum GameFontFamilyAndSizeProxy {
  /// <summary>
  /// Placeholder meaning unused.
  /// </summary>
  [Description("Placeholder meaning unused.")]
  Undefined,

  /// <summary>
  /// <para>AXIS (9.6pt)</para>
  /// <para>Contains Japanese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("AXIS_9.6")]
  Axis96,

  /// <summary>
  /// <para>AXIS (12pt)</para>
  /// <para>Contains Japanese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("AXIS_12")]
  Axis12,

  /// <summary>
  /// <para>AXIS (14pt)</para>
  /// <para>Contains Japanese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("AXIS_14")]
  Axis14,

  /// <summary>
  /// <para>AXIS (18pt)</para>
  /// <para>Contains Japanese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("AXIS_18")]
  Axis18,

  /// <summary>
  /// <para>AXIS (36pt)</para>
  /// <para>Contains Japanese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("AXIS_36")]
  Axis36,

  /// <summary>
  /// <para>CHNAXIS (120pt)</para>
  /// <para>Contains Chinese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("CHNAXIS_120")]
  ChnAxis120,

  /// <summary>
  /// <para>CHNAXIS (140pt)</para>
  /// <para>Contains Chinese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("CHNAXIS_140")]
  ChnAxis140,

  /// <summary>
  /// <para>CHNAXIS (180pt)</para>
  /// <para>Contains Chinese characters in addition to Latin characters. Used in game for the whole UI.</para>
  /// </summary>
  [Description("CHNAXIS_180")]
  ChnAxis180,

  /// <summary>
  /// <para>Jupiter (16pt)</para>
  /// <para>Serif font. Contains mostly ASCII range. Used in game for job names.</para>
  /// </summary>
  [Description("Jupiter_16")]
  Jupiter16,

  /// <summary>
  /// <para>Jupiter (20pt)</para>
  /// <para>Serif font. Contains mostly ASCII range. Used in game for job names.</para>
  /// </summary>
  [Description("Jupiter_20")]
  Jupiter20,

  /// <summary>
  /// <para>Jupiter (23pt)</para>
  /// <para>Serif font. Contains mostly ASCII range. Used in game for job names.</para>
  /// </summary>
  [Description("Jupiter_23")]
  Jupiter23,

  /// <summary>
  /// <para>Jupiter (45pt)</para>
  /// <para>Serif font. Contains mostly numbers. Used in game for flying texts.</para>
  /// </summary>
  [Description("Jupiter_45")]
  Jupiter45,

  /// <summary>
  /// <para>Jupiter (46pt)</para>
  /// <para>Serif font. Contains mostly ASCII range. Used in game for job names.</para>
  /// </summary>
  [Description("Jupiter_46")]
  Jupiter46,

  /// <summary>
  /// <para>Jupiter (90pt)</para>
  /// <para>Serif font. Contains mostly numbers. Used in game for flying texts.</para>
  /// </summary>
  [Description("Jupiter_90")]
  Jupiter90,

  /// <summary>
  /// <para>Meidinger (16pt)</para>
  /// <para>Horizontally wide. Contains mostly numbers. Used in game for HP/MP/IL stuff.</para>
  /// </summary>
  [Description("Meidinger_16")]
  Meidinger16,

  /// <summary>
  /// <para>Meidinger (20pt)</para>
  /// <para>Horizontally wide. Contains mostly numbers. Used in game for HP/MP/IL stuff.</para>
  /// </summary>
  [Description("Meidinger_20")]
  Meidinger20,

  /// <summary>
  /// <para>Meidinger (40pt)</para>
  /// <para>Horizontally wide. Contains mostly numbers. Used in game for HP/MP/IL stuff.</para>
  /// </summary>
  [Description("Meidinger_40")]
  Meidinger40,

  /// <summary>
  /// <para>MiedingerMid (10pt)</para>
  /// <para>Horizontally wide. Contains mostly ASCII range.</para>
  /// </summary>
  [Description("MiedingerMid_10")]
  MiedingerMid10,

  /// <summary>
  /// <para>MiedingerMid (12pt)</para>
  /// <para>Horizontally wide. Contains mostly ASCII range.</para>
  /// </summary>
  [Description("MiedingerMid_12")]
  MiedingerMid12,

  /// <summary>
  /// <para>MiedingerMid (14pt)</para>
  /// <para>Horizontally wide. Contains mostly ASCII range.</para>
  /// </summary>
  [Description("MiedingerMid_14")]
  MiedingerMid14,

  /// <summary>
  /// <para>MiedingerMid (18pt)</para>
  /// <para>Horizontally wide. Contains mostly ASCII range.</para>
  /// </summary>
  [Description("MiedingerMid_18")]
  MiedingerMid18,

  /// <summary>
  /// <para>MiedingerMid (36pt)</para>
  /// <para>Horizontally wide. Contains mostly ASCII range.</para>
  /// </summary>
  [Description("MiedingerMid_36")]
  MiedingerMid36,

  /// <summary>
  /// <para>TrumpGothic (18.4pt)</para>
  /// <para>Horizontally narrow. Contains mostly ASCII range. Used for addon titles.</para>
  /// </summary>
  [Description("TrumpGothic_18.4")]
  TrumpGothic184,

  /// <summary>
  /// <para>TrumpGothic (23pt)</para>
  /// <para>Horizontally narrow. Contains mostly ASCII range. Used for addon titles.</para>
  /// </summary>
  [Description("TrumpGothic_23")]
  TrumpGothic23,

  /// <summary>
  /// <para>TrumpGothic (34pt)</para>
  /// <para>Horizontally narrow. Contains mostly ASCII range. Used for addon titles.</para>
  /// </summary>
  [Description("TrumpGothic_34")]
  TrumpGothic34,

  /// <summary>
  /// <para>TrumpGothic (688pt)</para>
  /// <para>Horizontally narrow. Contains mostly ASCII range. Used for ad don titles.</para>
  /// </summary>
  [Description("TrumpGothic_688")]
  TrumpGothic68,
}
