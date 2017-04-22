// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.Conditions
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.World.Generation
{
  public static class Conditions
  {
    public class IsTile : GenCondition
    {
      private ushort[] _types;

      public IsTile(params ushort[] types)
      {
        this._types = types;
      }

      protected override bool CheckValidity(int x, int y)
      {
        if (GenBase._tiles[x, y].active())
        {
          for (int index = 0; index < this._types.Length; ++index)
          {
            if ((int) GenBase._tiles[x, y].type == (int) this._types[index])
              return true;
          }
        }
        return false;
      }
    }

    public class Continue : GenCondition
    {
      protected override bool CheckValidity(int x, int y)
      {
        return false;
      }
    }

    public class IsSolid : GenCondition
    {
      protected override bool CheckValidity(int x, int y)
      {
        if (GenBase._tiles[x, y].active())
          return Main.tileSolid[(int) GenBase._tiles[x, y].type];
        return false;
      }
    }

    public class HasLava : GenCondition
    {
      protected override bool CheckValidity(int x, int y)
      {
        if ((int) GenBase._tiles[x, y].liquid > 0)
          return (int) GenBase._tiles[x, y].liquidType() == 1;
        return false;
      }
    }
  }
}
