using Dalamud.Plugin.Services;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Modules;

public abstract class ModuleBase : IDisposable {
  private bool _isDisposed;

  private readonly bool _hasFrameworkUpdate;

  internal ModuleBase(bool frameworkUpdate) {
    _hasFrameworkUpdate = frameworkUpdate;
    if (_hasFrameworkUpdate) {
      Svc.Framework.Update += Update;
    }
  }

  private void DisposePrivate(bool dispose) {
    if (!_isDisposed) {
      if (dispose) {
        this.DisposeManaged();
      }
      this.DisposeUnmanaged();
      _isDisposed = true;
    }
  }

  internal virtual void DisposeManaged() {
    if (_hasFrameworkUpdate) {
      Svc.Framework.Update -= Update;
    }
  }

  internal virtual void DisposeUnmanaged() { }

  public void Dispose() {
    this.DisposePrivate(true);
    GC.SuppressFinalize(this);
  }

  internal virtual void Update(IFramework framework) { }
}
