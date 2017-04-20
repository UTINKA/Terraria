// Decompiled with JetBrains decompiler
// Type: Terraria.World.Generation.Passes
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.World.Generation
{
  public static class Passes
  {
    public class Clear : GenPass
    {
      public Clear()
        : base("clear", 1f)
      {
      }

      public override void Apply(GenerationProgress progress)
      {
        for (int index1 = 0; index1 < GenBase._worldWidth; ++index1)
        {
          for (int index2 = 0; index2 < GenBase._worldHeight; ++index2)
          {
            if (GenBase._tiles[index1, index2] == null)
              GenBase._tiles[index1, index2] = new Tile();
            else
              GenBase._tiles[index1, index2].ClearEverything();
          }
        }
      }
    }

    public class ScatterCustom : GenPass
    {
      private GenBase.CustomPerUnitAction _perUnit;
      private int _count;

      public ScatterCustom(string name, float loadWeight, int count, GenBase.CustomPerUnitAction perUnit = null)
        : base(name, loadWeight)
      {
        this._perUnit = perUnit;
        this._count = count;
      }

      public void SetCustomAction(GenBase.CustomPerUnitAction perUnit)
      {
        this._perUnit = perUnit;
      }

      public override void Apply(GenerationProgress progress)
      {
        int count = this._count;
        while (count > 0)
        {
          if (this._perUnit(GenBase._random.Next(1, GenBase._worldWidth), GenBase._random.Next(1, GenBase._worldHeight), new object[0]))
            --count;
        }
      }
    }
  }
}
