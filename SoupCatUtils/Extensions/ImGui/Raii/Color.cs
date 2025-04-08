#pragma warning disable IDE0065 // Misplaced using directive
using System;
using System.Numerics;
using ImGuiNET;

// ReSharper disable once CheckNamespace
namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions.ImGui;

// Place this after the namespace to avoid conflict.
using ImGui = ImGuiNET.ImGui;

/// <inheritdoc cref="ImGuiRaii" />
public static partial class ImGuiRaii {
  /// <summary>
  /// Pushes a color as an <see cref="IDisposable" />. Should be used with the <see langworld="using" /> statement.
  /// </summary>
  /// <param name="idx">The type of ImGui color to modify.</param>
  /// <param name="color">The value of the ImGui color to change.</param>
  /// <param name="condition">Whether to set the value or not.</param>
  /// <returns>An <see cref="IDisposable" /> instance.</returns>
  public static Dalamud.Interface.Utility.Raii.ImRaii.Color PushColor(ImGuiCol idx, uint color, bool condition = true)
    => Dalamud.Interface.Utility.Raii.ImRaii.PushColor(idx, color, condition);

  /// <inheritdoc cref="ImGuiRaii.PushColor(ImGuiCol, uint, bool)" />
  public static Dalamud.Interface.Utility.Raii.ImRaii.Color PushColor(ImGuiCol idx, Vector4 color, bool condition = true)
    => Dalamud.Interface.Utility.Raii.ImRaii.PushColor(idx, color, condition);
}
