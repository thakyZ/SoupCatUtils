using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

using Dalamud.Game.Gui;
using Dalamud.Interface;

using ImGuiNET;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI;

public abstract class SectionBase {
  public string Name { get { return NameImplementation; } }
  protected abstract string NameImplementation { get; }

  public virtual void Draw() { }

  public virtual void Dispose() {}

  public virtual void FrameWorkUpdate() { }

  public static void CreateTitle(string h1, string h2 = "", string h3 = "") {
    float baseCursorPos = ImGui.GetCursorPosY();
    ImGui.PushFont(Services.FontContainer.GetFont("AXIS", 36));
    ImGui.Text(h1);
    ImGui.PopFont();

    if (h2 != string.Empty) {
      ImGui.SameLine();
      ImGui.Spacing();
      ImGui.SameLine();

      ImGui.PushFont(Services.FontContainer.GetFont("AXIS", 18));
      ImGui.SetCursorPosY(baseCursorPos + (36 - 17));
      ImGui.Text(h2);
      ImGui.PopFont();

      if (h3 != string.Empty) {
        ImGui.SameLine();
        ImGui.Spacing();
        ImGui.SameLine();

        ImGui.PushFont(Services.FontContainer.GetFont("AXIS", 12));
        ImGui.SetCursorPosY(baseCursorPos + (36 - 10));
        ImGui.Text(h3);
        ImGui.PopFont();
      }
    }
  }

  public static void CreateTitle(string h2 = "") {
    ImGui.PushFont(Services.FontContainer.GetFont("AXIS", 18));
    ImGui.Text(h2);
    ImGui.PopFont();
  }

  public static bool InputMixedFilterInt(string label, ref int value, Dictionary<int, string> steps, int step = 1, int step_fast = 5, ImGuiInputTextFlags flags = ImGuiInputTextFlags.None) {
    char[] buf = new char[64];
    float button_size = ImGui.GetFrameHeight();
    ImGui.BeginGroup(); // The only purpose of the group here is to allow the caller to query item data e.g. IsItemActive()
    ImGui.PushID(label);
    /*steps.
    int _value = value.ToString();
    ImGui.SetNextItemWidth(Math.Max(1.0f, ImGui.CalcItemWidth() - ((button_size + ImGui.GetStyle().ItemInnerSpacing.X) * 2)));
    if (ImGui.InputText("", ref _value, 4, _value)) // PushId(label) + "" gives us the expected ID from outside point of view
      value_changed = DataTypeApplyFromText(buf, data_type, p_data, format);
    IMGUI_TEST_ENGINE_ITEM_INFO(g.LastItemData.ID, label, g.LastItemData.StatusFlags | ImGuiItemStatusFlags_Inputable);

    // Step buttons
    const ImVec2 backup_frame_padding = style.FramePadding;
    style.FramePadding.x = style.FramePadding.y;
    ImGuiButtonFlags button_flags = ImGuiButtonFlags_Repeat | ImGuiButtonFlags_DontClosePopups;
    if (flags & ImGuiInputTextFlags_ReadOnly)
      BeginDisabled();
    SameLine(0, style.ItemInnerSpacing.x);
    if (ButtonEx("-", ImVec2(button_size, button_size), button_flags)) {
      DataTypeApplyOp(data_type, '-', p_data, p_data, g.IO.KeyCtrl && p_step_fast ? p_step_fast : p_step);
      value_changed = true;
    }
    SameLine(0, style.ItemInnerSpacing.x);
    if (ButtonEx("+", ImVec2(button_size, button_size), button_flags)) {
      DataTypeApplyOp(data_type, '+', p_data, p_data, g.IO.KeyCtrl && p_step_fast ? p_step_fast : p_step);
      value_changed = true;
    }
    if (flags & ImGuiInputTextFlags_ReadOnly)
      EndDisabled();

    const char* label_end = FindRenderedTextEnd(label);
    if (label != label_end) {
      SameLine(0, style.ItemInnerSpacing.x);
      TextEx(label, label_end);
    }
    style.FramePadding = backup_frame_padding;

    ImGui.PopID();*/
    ImGui.EndGroup();
    return false;
  /*}
    if (value_changed)
        MarkItemEdited(g.LastItemData.ID);

    return value_changed;*/
  }
}
