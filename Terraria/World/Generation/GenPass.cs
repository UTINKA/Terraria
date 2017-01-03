// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenPass
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

namespace Terraria.World.Generation
{
  public abstract class GenPass : GenBase
  {
    public string Name;
    public float Weight;

    public GenPass(string name, float loadWeight)
    {
      this.Name = name;
      this.Weight = loadWeight;
    }

    public abstract void Apply(GenerationProgress progress);
  }
}
