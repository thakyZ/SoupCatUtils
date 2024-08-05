using System.Reflection;
using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;
using ECommons.Schedulers;
using System.Formats.Asn1;
using System.Collections.Concurrent;
using ECommons;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions;

public class ModulesList<TModule> : List<TModule?>, IDisposable where TModule : ModuleBase?, IDisposable? {
  private readonly ConcurrentDictionary<Type, TickScheduler> _tickSchedulers = [];

  private bool MatchAgainstOtherType<TCompare>(TModule? item) where TCompare : TModule {
    return item?.GetType().IsEquivalentTo(typeof(TCompare)) ?? false;
  }

  public NModule? Get<NModule>() where NModule : TModule {
    NModule?[] matches = [..this.FindAll(MatchAgainstOtherType<NModule>).Cast<NModule?>()];
    if (matches.Length == 0) {
      throw new Exception($"No matches found for type {typeof(NModule).FullName} in module list");
    }
    if (matches.Length > 1) {
      throw new Exception($"Too many matches found for type {typeof(NModule).FullName} in module list, found {matches.Length}");
    }
    return matches[0];
  }

  public void Dispose() {
    foreach ((Type _, TickScheduler tickScheduler) in _tickSchedulers) {
      tickScheduler?.Dispose();
    }
    _tickSchedulers.Clear();
    foreach (TModule? module in this) {
      module?.Dispose();
    }
    this.Clear();
    GC.SuppressFinalize(this);
  }
}
