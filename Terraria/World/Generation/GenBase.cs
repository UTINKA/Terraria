// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.GenBase
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

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
