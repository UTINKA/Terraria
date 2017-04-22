// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.MicroBiome
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

namespace Terraria.World.Generation
{
  public abstract class MicroBiome : GenStructure
  {
    public virtual void Reset()
    {
    }

    public static void ResetAll()
    {
      foreach (MicroBiome biome in BiomeCollection.Biomes)
        biome.Reset();
    }
  }
}
