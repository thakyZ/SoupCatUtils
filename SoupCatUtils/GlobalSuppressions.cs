// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors",
  Justification = "This doesn't need to be marked static as it is passed as a type and instanced.", Scope = "type", Target = "~T:NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Services")]
[assembly: SuppressMessage("Roslynator", "RCS1170:Use read-only auto-implemented property",
  Justification = "Cannot pass auto-implemented properties as a refrence", Scope = "member", Target = "~T:NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Services")]
[assembly: SuppressMessage("Roslynator", "RCS1085:Use auto-implemented property",
  Justification = "Cannot pass auto-implemented properties as a refrence", Scope = "member", Target = "~T:NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.PluginUI")]
[assembly: SuppressMessage("Roslynator", "RCS1085:Use auto-implemented property",
  Justification = "Cannot pass auto-implemented properties as a refrence", Scope = "member", Target = "~T:NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI.AboutSection")]
[assembly: SuppressMessage("Roslynator", "RCS1085:Use auto-implemented property",
  Justification = "Cannot pass auto-implemented properties as a refrence", Scope = "member", Target = "~T:NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI.HousingSection")]
