// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.Conditions
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

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
