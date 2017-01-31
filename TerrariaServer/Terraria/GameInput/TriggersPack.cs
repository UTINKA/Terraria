// Decompiled with JetBrains decompiler
// Type: Terraria.GameInput.TriggersPack
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using System.Linq;

namespace Terraria.GameInput
{
  public class TriggersPack
  {
    public TriggersSet Current = new TriggersSet();
    public TriggersSet Old = new TriggersSet();
    public TriggersSet JustPressed = new TriggersSet();
    public TriggersSet JustReleased = new TriggersSet();

    public void Initialize()
    {
      this.Current.SetupKeys();
      this.Old.SetupKeys();
      this.JustPressed.SetupKeys();
      this.JustReleased.SetupKeys();
    }

    public void Reset()
    {
      this.Old = this.Current.Clone();
      this.Current.Reset();
    }

    public void Update()
    {
      this.CompareDiffs(this.JustPressed, this.Old, this.Current);
      this.CompareDiffs(this.JustReleased, this.Current, this.Old);
    }

    public void CompareDiffs(TriggersSet Bearer, TriggersSet oldset, TriggersSet newset)
    {
      Bearer.Reset();
      foreach (string index in Bearer.KeyStatus.Keys.ToList<string>())
        Bearer.KeyStatus[index] = newset.KeyStatus[index] && !oldset.KeyStatus[index];
    }
  }
}
