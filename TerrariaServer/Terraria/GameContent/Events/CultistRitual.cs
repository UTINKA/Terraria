// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Events.CultistRitual
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Events
{
  public class CultistRitual
  {
    public const int delayStart = 86400;
    public const int respawnDelay = 43200;
    private const int timePerCultist = 3600;
    private const int recheckStart = 600;
    public static int delay;
    public static int recheck;

    public static void UpdateTime()
    {
      if (Main.netMode == 1)
        return;
      CultistRitual.delay -= Main.dayRate;
      if (CultistRitual.delay < 0)
        CultistRitual.delay = 0;
      CultistRitual.recheck -= Main.dayRate;
      if (CultistRitual.recheck < 0)
        CultistRitual.recheck = 0;
      if (CultistRitual.delay != 0 || CultistRitual.recheck != 0)
        return;
      CultistRitual.recheck = 600;
      if (NPC.AnyDanger())
        CultistRitual.recheck *= 6;
      else
        CultistRitual.TrySpawning(Main.dungeonX, Main.dungeonY);
    }

    public static void CultistSlain()
    {
      CultistRitual.delay -= 3600;
    }

    public static void TabletDestroyed()
    {
      CultistRitual.delay = 43200;
    }

    public static void TrySpawning(int x, int y)
    {
      if (WorldGen.PlayerLOS(x - 6, y) || WorldGen.PlayerLOS(x + 6, y) || !CultistRitual.CheckRitual(x, y))
        return;
      NPC.NewNPC(x * 16 + 8, (y - 4) * 16 - 8, 437, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
    }

    private static bool CheckRitual(int x, int y)
    {
      if (CultistRitual.delay != 0 || !Main.hardMode || (!NPC.downedGolemBoss || !NPC.downedBoss3) || (y < 7 || WorldGen.SolidTile(Main.tile[x, y - 7]) || NPC.AnyNPCs(437)))
        return false;
      Vector2 Center;
      // ISSUE: explicit reference operation
      ((Vector2) @Center).\u002Ector((float) (x * 16 + 8), (float) (y * 16 - 64 - 8 - 27));
      Point[] spawnPoints = (Point[]) null;
      return CultistRitual.CheckFloor(Center, out spawnPoints);
    }

    public static bool CheckFloor(Vector2 Center, out Point[] spawnPoints)
    {
      Point[] pointArray = new Point[4];
      int num1 = 0;
      Point tileCoordinates = Center.ToTileCoordinates();
      int num2 = -5;
      while (num2 <= 5)
      {
        if (num2 != -1 && num2 != 1)
        {
          for (int index = -5; index < 12; ++index)
          {
            int i = tileCoordinates.X + num2 * 2;
            int j = tileCoordinates.Y + index;
            if (WorldGen.SolidTile(i, j) && !Collision.SolidTiles(i - 1, i + 1, j - 3, j - 1))
            {
              pointArray[num1++] = new Point(i, j);
              break;
            }
          }
        }
        num2 += 2;
      }
      if (num1 != 4)
      {
        spawnPoints = (Point[]) null;
        return false;
      }
      spawnPoints = pointArray;
      return true;
    }
  }
}
