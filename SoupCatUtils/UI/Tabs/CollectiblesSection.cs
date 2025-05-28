using System.IO;

using static ECommons.GenericHelpers;

using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;

using ImGuiNET;

using Newtonsoft.Json;

using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Exceptions;
using Dalamud.Interface.Windowing;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.UI.Tabs;

public class CollectiblesSection : SectionBase {
  public CollectiblesSection(Window parent) : base(parent) { }

  internal override string Name => "Collectibles##SoupCatUtils";

  internal enum WindowType {
    None = 0,
    CraftingLog = 1,
    GatherWindow = 2,
    FishingLog = 3,
  }

  public override void Dispose() {
    GC.SuppressFinalize(this);
  }

  public override void FrameworkUpdate() {
  }

  /// <summary>
  /// Writes an object that is convertible to a json document to a file.
  /// </summary>
  /// <param name="jsonObject">The json object to write to file.</param>
  /// <param name="filePath">Path to the output file.</param>
  /// <param name="overwrite">Overwrite the file if it exists otherwise skip.</param>
  /// <typeparam name="T">A type convertible to a json document.</typeparam>
  private void WriteJsonFile<T>(T jsonObject, string filePath, bool overwrite) where T : notnull {
    if (File.Exists(filePath) && !overwrite) {
      return;
    }
    if (!File.Exists(filePath)) {
      File.Create(filePath);
    }

    var json = JsonConvert.SerializeObject(jsonObject);
    using var fs = new FileStream(filePath, FileMode.Truncate, FileAccess.ReadWrite);
    using var sr = new StreamWriter(fs);
    sr.Write(json);
  }

  private unsafe uint[] GetCraftingLog() {
    if (!(System.States?[State.IsCraftingLogOpen] ?? false)) {
      return [];
    }
    List<uint> output = [];
    try {
      if (TryGetAddonByName<AddonRecipeNote>("RecipeNote", out var addon)) {
        if (!addon->AtkUnitBase.IsVisible) return [];
        var treeListRaw = addon->AtkUnitBase.GetNodeById(45);
        if (treeListRaw == null) return [];
        var treeList = treeListRaw->GetAsAtkComponentList();
        if (treeList == null) return [];
        var json = treeList->JSONClone();
        WriteJsonFile(json, Path.Join("E:", "FFXIV", "CraftingLog.json"), false);
        return [];
        /*
        var treeListCount = treeList->ListLength;
        var treeListArray = treeList->CreateArray((uint)treeListCount);

        for (int i = 0; i < treeListCount; i++) {
          return [];
        }
        */
      }
    } catch (Exception exception) {
      Svc.Log.Error(exception, "Failed to process the add-on crafting log.");
    }
    return [.. output];
  }

  private unsafe uint[] GetGatheringLog() {
    if (!(System.States?[State.IsGatheringLogOpen] ?? false)) {
      return [];
    }
    List<uint> output = [];
    try {
      var addon = (AtkUnitBase*)Svc.GameGui.GetAddonByName("GatheringNote");
    } catch (Exception exception) {
      Svc.Log.Error(exception, "Failed to get crafting log data.");
      return [];
    }
    return [..output];
  }

  private unsafe uint[] GetFishingLog() {
    if (!(System.States?[State.IsFishingLogOpen] ?? false)) {
      return [];
    }
    List<uint> output = [];
    try {
      var addon = (AtkUnitBase*)Svc.GameGui.GetAddonByName("FishingGuide2");
    } catch (Exception exception) {
      Svc.Log.Error(exception, "Failed to get crafting log data.");
      return [];
    }
    return [..output];
  }

  private uint[] GetDataFromWindow(WindowType windowType) {
    return windowType switch {
      WindowType.CraftingLog => GetCraftingLog(),
      WindowType.GatherWindow => GetGatheringLog(),
      WindowType.FishingLog => GetFishingLog(),
      _ => throw new InvalidArgumentException(nameof(windowType), "WindowType of \"None\" or 0 is invalid."),
    };
  }

  private string? SetupTemplate(string templateName, uint[] data) {
    if (data.Length == 0) {
      return null;
    }
    string? template = System.ResourceManager.GetString(templateName);
    if (template is null) {
      return null;
    }
    string jsonStringifyData = JsonConvert.SerializeObject(data);
    if (string.IsNullOrEmpty(jsonStringifyData)) {
      return null;
    }
    return template.Replace("%@", jsonStringifyData);
  }

  public override void Draw() {
    base.Draw();

    #region Miner / Botanist
    ImGui.Text("Miner / Botanist");
    ImGui.SameLine();
    if (ImGui.Button("Copy FFXIV Collect JS")) {
      var data = GetDataFromWindow(WindowType.GatherWindow);
      if (data is not null) {
        var processedTemplate = SetupTemplate("Collectibles_FFXIVCollectJS-Gather_Template", data);
        if (processedTemplate is not null) {
          ImGui.SetClipboardText(processedTemplate);
        }
      }
    }
    ImGui.SameLine();
    if (ImGui.Button("Copy FFXIV TeamCraft JS")) {
      var data = GetDataFromWindow(WindowType.GatherWindow);
      if (data is not null) {
        var processedTemplate = SetupTemplate("Collectibles_FFXIVTeamCraft-Gather_Template", data);
        if (processedTemplate is not null) {
          ImGui.SetClipboardText(processedTemplate);
        }
      }
    }
    #endregion

    #region Fisher
    ImGui.Text("Fisher");
    ImGui.SameLine();
    if (ImGui.Button("Copy FFXIV Collect JS")) {
      var data = GetDataFromWindow(WindowType.FishingLog);
      if (data is not null) {
        var processedTemplate = SetupTemplate("Collectibles_FFXIVCollectJS-Fisher_Template", data);
        if (processedTemplate is not null) {
          ImGui.SetClipboardText(processedTemplate);
        }
      }
    }
    ImGui.SameLine();
    if (ImGui.Button("Copy Lalachievements JS")) {
      var data = GetDataFromWindow(WindowType.FishingLog);
      if (data is not null) {
        var processedTemplate = SetupTemplate("Collectibles_LalachievementsJS_Template", data);
        if (processedTemplate is not null) {
          ImGui.SetClipboardText(processedTemplate);
        }
      }
    }
    ImGui.SameLine();
    if (ImGui.Button("Copy FFXIV TeamCraft JS")) {
      var data = GetDataFromWindow(WindowType.FishingLog);
      if (data is not null) {
        var processedTemplate = SetupTemplate("Collectibles_FFXIVTeamCraft-Gather_Template", data);
        if (processedTemplate is not null) {
          ImGui.SetClipboardText(processedTemplate);
        }
      }
    }
    ImGui.SameLine();
    if (ImGui.Button("Copy Carbuncle Plushy JS")) {
      var data = GetDataFromWindow(WindowType.FishingLog);
      if (data is not null) {
        var processedTemplate = SetupTemplate("Collectibles_CarbunclePlushyJS_Template", data);
        if (processedTemplate is not null) {
          ImGui.SetClipboardText(processedTemplate);
        }
      }
    }
    #endregion

    #region Crafter
    ImGui.Text("Crafter");
    ImGui.SameLine();
    if (ImGui.Button("Copy FFXIV Collect JS")) {
      var data = GetDataFromWindow(WindowType.CraftingLog);
      if (data is not null) {
        var processedTemplate = SetupTemplate("Collectibles_FFXIVCollectJS-Crafter_Template", data);
        if (processedTemplate is not null) {
          ImGui.SetClipboardText(processedTemplate);
        }
      }
    }
    ImGui.SameLine();
    if (ImGui.Button("Copy FFXIV TeamCraft JS")) {
      var data = GetDataFromWindow(WindowType.CraftingLog);
      if (data is not null) {
        var processedTemplate = SetupTemplate("Collectibles_FFXIVTeamCraft-Crafter_Template", data);
        if (processedTemplate is not null) {
          ImGui.SetClipboardText(processedTemplate);
        }
      }
    }
    #endregion
  }
}
