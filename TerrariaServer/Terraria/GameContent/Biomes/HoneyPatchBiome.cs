// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.HoneyPatchBiome
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Terraria.World.Generation;

namespace Terraria.GameContent.Biomes
{
  public class HoneyPatchBiome : MicroBiome
  {
    public override bool Place(Point origin, StructureMap structures)
    {
      if (GenBase._tiles[origin.X, origin.Y].active() && WorldGen.SolidTile(origin.X, origin.Y))
        return false;
      Point result;
      if (!WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Down(80), (GenCondition) new Conditions.IsSolid()), out result))
        return false;
      result.Y += 2;
      Ref<int> count = new Ref<int>(0);
      WorldUtils.Gen(result, (GenShape) new Shapes.Circle(8), Actions.Chain((GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.Scanner(count)));
      if (count.Value < 20 || !structures.CanPlace(new Microsoft.Xna.Framework.Rectangle(result.X - 8, result.Y - 8, 16, 16), 0))
        return false;
      WorldUtils.Gen(result, (GenShape) new Shapes.Circle(8), Actions.Chain((GenAction) new Modifiers.RadialDither(0.0f, 10f), (GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.SetTile((ushort) 229, true, true)));
      ShapeData data = new ShapeData();
      WorldUtils.Gen(result, (GenShape) new Shapes.Circle(4, 3), Actions.Chain((GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.ClearTile(true), new Modifiers.RectangleMask(-6, 6, 0, 3).Output(data), (GenAction) new Actions.SetLiquid(2, byte.MaxValue)));
      WorldUtils.Gen(new Point(result.X, result.Y + 1), (GenShape) new ModShapes.InnerOutline(data, true), Actions.Chain((GenAction) new Modifiers.IsEmpty(), (GenAction) new Modifiers.RectangleMask(-6, 6, 1, 3), (GenAction) new Actions.SetTile((ushort) 59, true, true)));
      structures.AddStructure(new Microsoft.Xna.Framework.Rectangle(result.X - 8, result.Y - 8, 16, 16), 0);
      return true;
    }
  }
}
