// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.MicroBiome
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

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
