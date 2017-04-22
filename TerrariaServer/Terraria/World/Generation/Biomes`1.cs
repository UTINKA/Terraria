// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.Biomes`1
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.World.Generation
{
  public static class Biomes<T> where T : MicroBiome, new()
  {
    private static T _microBiome = Biomes<T>.CreateInstance();

    public static bool Place(int x, int y, StructureMap structures)
    {
      return Biomes<T>._microBiome.Place(new Point(x, y), structures);
    }

    public static bool Place(Point origin, StructureMap structures)
    {
      return Biomes<T>._microBiome.Place(origin, structures);
    }

    public static T Get()
    {
      return Biomes<T>._microBiome;
    }

    private static T CreateInstance()
    {
      T instance = Activator.CreateInstance<T>();
      BiomeCollection.Biomes.Add((MicroBiome) instance);
      return instance;
    }
  }
}
