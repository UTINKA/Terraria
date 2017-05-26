// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.PressurePlateHelper
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.GameContent
{
  public class PressurePlateHelper
  {
    public static Dictionary<Point, bool[]> PressurePlatesPressed = new Dictionary<Point, bool[]>();
    public static bool NeedsFirstUpdate = false;
    private static Vector2[] PlayerLastPosition = new Vector2[(int) byte.MaxValue];
    private static Rectangle pressurePlateBounds = new Rectangle(0, 0, 16, 10);

    public static void Update()
    {
      if (!PressurePlateHelper.NeedsFirstUpdate)
        return;
      using (Dictionary<Point, bool[]>.KeyCollection.Enumerator enumerator = PressurePlateHelper.PressurePlatesPressed.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
          PressurePlateHelper.PokeLocation(enumerator.Current);
      }
      PressurePlateHelper.PressurePlatesPressed.Clear();
      PressurePlateHelper.NeedsFirstUpdate = false;
    }

    public static void Reset()
    {
      PressurePlateHelper.PressurePlatesPressed.Clear();
      for (int index = 0; index < PressurePlateHelper.PlayerLastPosition.Length; ++index)
        PressurePlateHelper.PlayerLastPosition[index] = Vector2.get_Zero();
    }

    public static void ResetPlayer(int player)
    {
      using (Dictionary<Point, bool[]>.ValueCollection.Enumerator enumerator = PressurePlateHelper.PressurePlatesPressed.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current[player] = false;
      }
    }

    public static void UpdatePlayerPosition(Player player)
    {
      Point p;
      // ISSUE: explicit reference operation
      ((Point) @p).\u002Ector(1, 1);
      Vector2 vector2 = p.ToVector2();
      List<Point> tilesIn1 = Collision.GetTilesIn(Vector2.op_Addition(PressurePlateHelper.PlayerLastPosition[player.whoAmI], vector2), Vector2.op_Subtraction(Vector2.op_Addition(PressurePlateHelper.PlayerLastPosition[player.whoAmI], player.Size), Vector2.op_Multiply(vector2, 2f)));
      List<Point> tilesIn2 = Collision.GetTilesIn(Vector2.op_Addition(player.TopLeft, vector2), Vector2.op_Subtraction(player.BottomRight, Vector2.op_Multiply(vector2, 2f)));
      Rectangle hitbox1 = player.Hitbox;
      Rectangle hitbox2 = player.Hitbox;
      // ISSUE: explicit reference operation
      ((Rectangle) @hitbox1).Inflate((int) -p.X, (int) -p.Y);
      // ISSUE: explicit reference operation
      ((Rectangle) @hitbox2).Inflate((int) -p.X, (int) -p.Y);
      hitbox2.X = (__Null) (int) PressurePlateHelper.PlayerLastPosition[player.whoAmI].X;
      hitbox2.Y = (__Null) (int) PressurePlateHelper.PlayerLastPosition[player.whoAmI].Y;
      for (int index = 0; index < tilesIn1.Count; ++index)
      {
        Point location = tilesIn1[index];
        Tile tile = Main.tile[(int) location.X, (int) location.Y];
        if (tile.active() && (int) tile.type == 428)
        {
          PressurePlateHelper.pressurePlateBounds.X = (__Null) (location.X * 16);
          PressurePlateHelper.pressurePlateBounds.Y = (__Null) (location.Y * 16 + 16 - PressurePlateHelper.pressurePlateBounds.Height);
          // ISSUE: explicit reference operation
          if (!((Rectangle) @hitbox1).Intersects(PressurePlateHelper.pressurePlateBounds) && !tilesIn2.Contains(location))
            PressurePlateHelper.MoveAwayFrom(location, player.whoAmI);
        }
      }
      for (int index = 0; index < tilesIn2.Count; ++index)
      {
        Point location = tilesIn2[index];
        Tile tile = Main.tile[(int) location.X, (int) location.Y];
        if (tile.active() && (int) tile.type == 428)
        {
          PressurePlateHelper.pressurePlateBounds.X = (__Null) (location.X * 16);
          PressurePlateHelper.pressurePlateBounds.Y = (__Null) (location.Y * 16 + 16 - PressurePlateHelper.pressurePlateBounds.Height);
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if (((Rectangle) @hitbox1).Intersects(PressurePlateHelper.pressurePlateBounds) && (!tilesIn1.Contains(location) || !((Rectangle) @hitbox2).Intersects(PressurePlateHelper.pressurePlateBounds)))
            PressurePlateHelper.MoveInto(location, player.whoAmI);
        }
      }
      PressurePlateHelper.PlayerLastPosition[player.whoAmI] = player.position;
    }

    public static void DestroyPlate(Point location)
    {
      bool[] flagArray;
      if (!PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out flagArray))
        return;
      PressurePlateHelper.PressurePlatesPressed.Remove(location);
      PressurePlateHelper.PokeLocation(location);
    }

    private static void UpdatePlatePosition(Point location, int player, bool onIt)
    {
      if (onIt)
        PressurePlateHelper.MoveInto(location, player);
      else
        PressurePlateHelper.MoveAwayFrom(location, player);
    }

    private static void MoveInto(Point location, int player)
    {
      bool[] flagArray;
      if (PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out flagArray))
      {
        flagArray[player] = true;
      }
      else
      {
        PressurePlateHelper.PressurePlatesPressed[location] = new bool[(int) byte.MaxValue];
        PressurePlateHelper.PressurePlatesPressed[location][player] = true;
        PressurePlateHelper.PokeLocation(location);
      }
    }

    private static void MoveAwayFrom(Point location, int player)
    {
      bool[] flagArray;
      if (!PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out flagArray))
        return;
      flagArray[player] = false;
      bool flag = false;
      for (int index = 0; index < flagArray.Length; ++index)
      {
        if (flagArray[index])
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      PressurePlateHelper.PressurePlatesPressed.Remove(location);
      PressurePlateHelper.PokeLocation(location);
    }

    private static void PokeLocation(Point location)
    {
      if (Main.netMode == 1)
        return;
      Wiring.blockPlayerTeleportationForOneIteration = true;
      Wiring.HitSwitch((int) location.X, (int) location.Y);
      NetMessage.SendData(59, -1, -1, (NetworkText) null, (int) location.X, (float) location.Y, 0.0f, 0.0f, 0, 0, 0);
    }
  }
}
