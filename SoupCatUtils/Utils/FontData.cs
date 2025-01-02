namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;
public readonly struct FontData(string name, string path, float size, bool chinese, bool korean) {
  public string Name { get; } = name;
  public string Path { get; } = path;
  public float Size { get; } = size;
  public bool Chinese { get; } = chinese;
  public bool Korean { get; } = korean;
}
