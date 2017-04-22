// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.ThinIceBiome
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.World.Generation;

namespace Terraria.GameContent.Biomes
{
  public class ThinIceBiome : MicroBiome
  {
    public override bool Place(Point origin, StructureMap structures)
    {
      Dictionary<ushort, int> resultsOutput = new Dictionary<ushort, int>();
      WorldUtils.Gen(new Point(origin.X - 25, origin.Y - 25), (GenShape) new Shapes.Rectangle(50, 50), (GenAction) new Actions.TileScanner(new ushort[4]
      {
        (ushort) 0,
        (ushort) 59,
        (ushort) 147,
        (ushort) 1
      }).Output(resultsOutput));
      int num1 = resultsOutput[(ushort) 0] + resultsOutput[(ushort) 1];
      int num2 = resultsOutput[(ushort) 59];
      int num3 = resultsOutput[(ushort) 147];
      if (num3 <= num2 || num3 <= num1)
        return false;
      int num4 = 0;
      for (int radius = GenBase._random.Next(10, 15); radius > 5; --radius)
      {
        int num5 = GenBase._random.Next(-5, 5);
        WorldUtils.Gen(new Point(origin.X + num5, origin.Y + num4), (GenShape) new Shapes.Circle(radius), Actions.Chain((GenAction) new Modifiers.Blotches(4, 0.3), (GenAction) new Modifiers.OnlyTiles(new ushort[5]
        {
          (ushort) 147,
          (ushort) 161,
          (ushort) 224,
          (ushort) 0,
          (ushort) 1
        }), (GenAction) new Actions.SetTile((ushort) 162, true, true)));
        WorldUtils.Gen(new Point(origin.X + num5, origin.Y + num4), (GenShape) new Shapes.Circle(radius), Actions.Chain((GenAction) new Modifiers.Blotches(4, 0.3), (GenAction) new Modifiers.HasLiquid(-1, -1), (GenAction) new Actions.SetTile((ushort) 162, true, true), (GenAction) new Actions.SetLiquid(0, (byte) 0)));
        num4 += radius - 2;
      }
      return true;
    }
  }
}
