#pragma warning disable IDE0065 // Misplaced using directive
using System.Diagnostics;
using System.Numerics;
using ImGuiNET;

// ReSharper disable once CheckNamespace
namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions.ImGui;

// Place this after the namespace to avoid conflict.
using ImGui = ImGuiNET.ImGui;

/// <inheritdoc cref="ImGuiRaii" />
public static partial class ImGuiRaii {
  /// <summary>
  /// Pushes a style as an <see cref="IDisposable" />. Should be used with the <see langworld="using" /> statement.
  /// </summary>
  /// <param name="idx">The type of ImGui style to modify.</param>
  /// <param name="value">The value of the ImGui style to change.</param>
  /// <param name="condition">Whether to set the value or not.</param>
  /// <returns>An <see cref="IDisposable" /> instance.</returns>
  public static Dalamud.Interface.Utility.Raii.ImRaii.Style PushStyle(ImGuiStyleVar idx, float value, bool condition = true)
    => Dalamud.Interface.Utility.Raii.ImRaii.PushStyle(idx, value, condition);

  /// <inheritdoc cref="ImGuiRaii.PushStyle(ImGuiStyleVar, float, bool)" />
  public static Dalamud.Interface.Utility.Raii.ImRaii.Style PushStyle(ImGuiStyleVar idx, Vector2 value, bool condition = true)
    => Dalamud.Interface.Utility.Raii.ImRaii.PushStyle(idx, value, condition);

  ///// <inheritdoc cref="ImGuiRaii.PushStyle(ImGuiStyleVar, float, bool)" />
  //public static Dalamud.Interface.Utility.Raii.ImRaii.Style PushStyle(ImGuiStyleVar idx, bool value, bool condition = true)
  //  => Dalamud.Interface.Utility.Raii.ImRaii.PushStyle(idx, value, condition);

  /// <inheritdoc cref="ImGuiRaii.PushStyle(ImGuiStyleVar, float, bool)" />
  public static Dalamud.Interface.Utility.Raii.ImRaii.Style PushStyle(ImGuiStyleVar idx, int value, bool condition = true)
    => Dalamud.Interface.Utility.Raii.ImRaii.PushStyle(idx, value, condition);
}
