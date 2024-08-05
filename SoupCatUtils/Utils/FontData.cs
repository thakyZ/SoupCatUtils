namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;
public struct FontData(string name, string path, float size, bool chinese, bool korean) {
  public string Name = name;
  public string Path = path;
  public float Size = size;
  public bool Chinese = chinese;
  public bool Korean = korean;
}
