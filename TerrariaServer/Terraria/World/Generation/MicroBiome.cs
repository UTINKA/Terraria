// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.MicroBiome
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
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
