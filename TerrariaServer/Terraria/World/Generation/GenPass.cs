// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenPass
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
