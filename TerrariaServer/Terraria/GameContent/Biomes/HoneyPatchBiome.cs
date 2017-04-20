// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.HoneyPatchBiome
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Terraria.World.Generation;

namespace Terraria.GameContent.Biomes
{
  public class HoneyPatchBiome : MicroBiome
  {
    public override bool Place(Point origin, StructureMap structures)
    {
      if (GenBase._tiles[(int) origin.X, (int) origin.Y].active() && WorldGen.SolidTile((int) origin.X, (int) origin.Y))
        return false;
      Point result;
      if (!WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Down(80), (GenCondition) new Conditions.IsSolid()), out result))
        return false;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Point& local = @result;
      // ISSUE: explicit reference operation
      int num = (^local).Y + 2;
      // ISSUE: explicit reference operation
      (^local).Y = (__Null) num;
      Ref<int> count = new Ref<int>(0);
      WorldUtils.Gen(result, (GenShape) new Shapes.Circle(8), Actions.Chain((GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.Scanner(count)));
      if (count.Value < 20 || !structures.CanPlace(new Rectangle(result.X - 8, result.Y - 8, 16, 16), 0))
        return false;
      WorldUtils.Gen(result, (GenShape) new Shapes.Circle(8), Actions.Chain((GenAction) new Modifiers.RadialDither(0.0f, 10f), (GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.SetTile((ushort) 229, true, true)));
      ShapeData data = new ShapeData();
      WorldUtils.Gen(result, (GenShape) new Shapes.Circle(4, 3), Actions.Chain((GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.ClearTile(true), new Modifiers.RectangleMask(-6, 6, 0, 3).Output(data), (GenAction) new Actions.SetLiquid(2, byte.MaxValue)));
      WorldUtils.Gen(new Point((int) result.X, result.Y + 1), (GenShape) new ModShapes.InnerOutline(data, true), Actions.Chain((GenAction) new Modifiers.IsEmpty(), (GenAction) new Modifiers.RectangleMask(-6, 6, 1, 3), (GenAction) new Actions.SetTile((ushort) 59, true, true)));
      structures.AddStructure(new Rectangle(result.X - 8, result.Y - 8, 16, 16), 0);
      return true;
    }
  }
}
