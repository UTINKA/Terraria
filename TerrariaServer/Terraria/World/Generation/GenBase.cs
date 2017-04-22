// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenBase
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Terraria.Utilities;

namespace Terraria.World.Generation
{
  public class GenBase
  {
    protected static UnifiedRandom _random
    {
      get
      {
        return WorldGen.genRand;
      }
    }

    protected static Tile[,] _tiles
    {
      get
      {
        return Main.tile;
      }
    }

    protected static int _worldWidth
    {
      get
      {
        return Main.maxTilesX;
      }
    }

    protected static int _worldHeight
    {
      get
      {
        return Main.maxTilesY;
      }
    }

    public delegate bool CustomPerUnitAction(int x, int y, params object[] args);
  }
}
