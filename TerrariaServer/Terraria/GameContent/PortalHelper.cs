// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.PortalHelper
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
  public class PortalHelper
  {
    private static int[,] FoundPortals = new int[256, 2];
    private static int[] PortalCooldownForPlayers = new int[256];
    private static int[] PortalCooldownForNPCs = new int[200];
    private static readonly Vector2[] EDGES = new Vector2[4]
    {
      new Vector2(0.0f, 1f),
      new Vector2(0.0f, -1f),
      new Vector2(1f, 0.0f),
      new Vector2(-1f, 0.0f)
    };
    private static readonly Vector2[] SLOPE_EDGES = new Vector2[4]
    {
      new Vector2(1f, -1f),
      new Vector2(-1f, -1f),
      new Vector2(1f, 1f),
      new Vector2(-1f, 1f)
    };
    private static readonly Point[] SLOPE_OFFSETS = new Point[4]
    {
      new Point(1, -1),
      new Point(-1, -1),
      new Point(1, 1),
      new Point(-1, 1)
    };
    public const int PORTALS_PER_PERSON = 2;

    static PortalHelper()
    {
      for (int index = 0; index < PortalHelper.SLOPE_EDGES.Length; ++index)
      {
        // ISSUE: explicit reference operation
        ((Vector2) @PortalHelper.SLOPE_EDGES[index]).Normalize();
      }
      for (int index = 0; index < PortalHelper.FoundPortals.GetLength(0); ++index)
      {
        PortalHelper.FoundPortals[index, 0] = -1;
        PortalHelper.FoundPortals[index, 1] = -1;
      }
    }

    public static void UpdatePortalPoints()
    {
      for (int index = 0; index < PortalHelper.FoundPortals.GetLength(0); ++index)
      {
        PortalHelper.FoundPortals[index, 0] = -1;
        PortalHelper.FoundPortals[index, 1] = -1;
      }
      for (int index = 0; index < PortalHelper.PortalCooldownForPlayers.Length; ++index)
      {
        if (PortalHelper.PortalCooldownForPlayers[index] > 0)
          --PortalHelper.PortalCooldownForPlayers[index];
      }
      for (int index = 0; index < PortalHelper.PortalCooldownForNPCs.Length; ++index)
      {
        if (PortalHelper.PortalCooldownForNPCs[index] > 0)
          --PortalHelper.PortalCooldownForNPCs[index];
      }
      for (int index = 0; index < 1000; ++index)
      {
        Projectile projectile = Main.projectile[index];
        if (projectile.active && projectile.type == 602 && ((double) projectile.ai[1] >= 0.0 && (double) projectile.ai[1] <= 1.0) && (projectile.owner >= 0 && projectile.owner <= (int) byte.MaxValue))
          PortalHelper.FoundPortals[projectile.owner, (int) projectile.ai[1]] = index;
      }
    }

    public static void TryGoingThroughPortals(Entity ent)
    {
      float collisionPoint = 0.0f;
      Vector2 velocity = ent.velocity;
      int width = ent.width;
      int height = ent.height;
      int gravDir = 1;
      if (ent is Player)
        gravDir = (int) ((Player) ent).gravDir;
      for (int index1 = 0; index1 < PortalHelper.FoundPortals.GetLength(0); ++index1)
      {
        if (PortalHelper.FoundPortals[index1, 0] != -1 && PortalHelper.FoundPortals[index1, 1] != -1 && (!(ent is Player) || index1 < PortalHelper.PortalCooldownForPlayers.Length && PortalHelper.PortalCooldownForPlayers[index1] <= 0) && (!(ent is NPC) || index1 < PortalHelper.PortalCooldownForNPCs.Length && PortalHelper.PortalCooldownForNPCs[index1] <= 0))
        {
          for (int index2 = 0; index2 < 2; ++index2)
          {
            Projectile projectile1 = Main.projectile[PortalHelper.FoundPortals[index1, index2]];
            Vector2 start;
            Vector2 end;
            PortalHelper.GetPortalEdges(projectile1.Center, projectile1.ai[0], out start, out end);
            if (Collision.CheckAABBvLineCollision(Vector2.op_Addition(ent.position, ent.velocity), ent.Size, start, end, 2f, ref collisionPoint))
            {
              Projectile projectile2 = Main.projectile[PortalHelper.FoundPortals[index1, 1 - index2]];
              float num1 = ent.Hitbox.Distance(projectile1.Center);
              int bonusX;
              int bonusY;
              Vector2 newPos = Vector2.op_Addition(PortalHelper.GetPortalOutingPoint(ent.Size, projectile2.Center, projectile2.ai[0], out bonusX, out bonusY), Vector2.op_Multiply(Vector2.Normalize(new Vector2((float) bonusX, (float) bonusY)), num1));
              Vector2 Velocity1 = Vector2.op_Multiply(Vector2.get_UnitX(), 16f);
              if (!Vector2.op_Inequality(Collision.TileCollision(Vector2.op_Subtraction(newPos, Velocity1), Velocity1, width, height, true, true, gravDir), Velocity1))
              {
                Vector2 Velocity2 = Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.get_UnitX()), 16f);
                if (!Vector2.op_Inequality(Collision.TileCollision(Vector2.op_Subtraction(newPos, Velocity2), Velocity2, width, height, true, true, gravDir), Velocity2))
                {
                  Vector2 Velocity3 = Vector2.op_Multiply(Vector2.get_UnitY(), 16f);
                  if (!Vector2.op_Inequality(Collision.TileCollision(Vector2.op_Subtraction(newPos, Velocity3), Velocity3, width, height, true, true, gravDir), Velocity3))
                  {
                    Vector2 Velocity4 = Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.get_UnitY()), 16f);
                    if (!Vector2.op_Inequality(Collision.TileCollision(Vector2.op_Subtraction(newPos, Velocity4), Velocity4, width, height, true, true, gravDir), Velocity4))
                    {
                      float num2 = 0.1f;
                      if (bonusY == -gravDir)
                        num2 = 0.1f;
                      if (Vector2.op_Equality(ent.velocity, Vector2.get_Zero()))
                        ent.velocity = Vector2.op_Multiply((projectile1.ai[0] - 1.570796f).ToRotationVector2(), num2);
                      // ISSUE: explicit reference operation
                      if ((double) ((Vector2) @ent.velocity).Length() < (double) num2)
                      {
                        // ISSUE: explicit reference operation
                        ((Vector2) @ent.velocity).Normalize();
                        Entity entity = ent;
                        Vector2 vector2 = Vector2.op_Multiply(entity.velocity, num2);
                        entity.velocity = vector2;
                      }
                      Vector2 vec = Vector2.Normalize(new Vector2((float) bonusX, (float) bonusY));
                      if (vec.HasNaNs() || Vector2.op_Equality(vec, Vector2.get_Zero()))
                        vec = Vector2.op_Multiply(Vector2.get_UnitX(), (float) ent.direction);
                      // ISSUE: explicit reference operation
                      ent.velocity = Vector2.op_Multiply(vec, ((Vector2) @ent.velocity).Length());
                      if (bonusY == -gravDir && Math.Sign((float) ent.velocity.Y) != -gravDir || (double) Math.Abs((float) ent.velocity.Y) < 0.100000001490116)
                        ent.velocity.Y = (__Null) ((double) -gravDir * 0.100000001490116);
                      int extraInfo = (int) ((double) (projectile2.owner * 2) + (double) projectile2.ai[1]);
                      int num3 = extraInfo + (extraInfo % 2 == 0 ? 1 : -1);
                      if (ent is Player)
                      {
                        Player player = (Player) ent;
                        player.lastPortalColorIndex = num3;
                        player.Teleport(newPos, 4, extraInfo);
                        if (Main.netMode == 1)
                        {
                          NetMessage.SendData(96, -1, -1, (NetworkText) null, player.whoAmI, (float) newPos.X, (float) newPos.Y, (float) extraInfo, 0, 0, 0);
                          NetMessage.SendData(13, -1, -1, (NetworkText) null, player.whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                        }
                        PortalHelper.PortalCooldownForPlayers[index1] = 10;
                        return;
                      }
                      if (!(ent is NPC))
                        return;
                      NPC npc = (NPC) ent;
                      npc.lastPortalColorIndex = num3;
                      npc.Teleport(newPos, 4, extraInfo);
                      if (Main.netMode == 1)
                      {
                        NetMessage.SendData(100, -1, -1, (NetworkText) null, npc.whoAmI, (float) newPos.X, (float) newPos.Y, (float) extraInfo, 0, 0, 0);
                        NetMessage.SendData(23, -1, -1, (NetworkText) null, npc.whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                      }
                      PortalHelper.PortalCooldownForPlayers[index1] = 10;
                      return;
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    public static int TryPlacingPortal(Projectile theBolt, Vector2 velocity, Vector2 theCrashVelocity)
    {
      // ISSUE: explicit reference operation
      Vector2 vector2_1 = Vector2.op_Division(velocity, ((Vector2) @velocity).Length());
      Point tileCoordinates = PortalHelper.FindCollision(theBolt.position, Vector2.op_Addition(Vector2.op_Addition(theBolt.position, velocity), Vector2.op_Multiply(vector2_1, 32f))).ToTileCoordinates();
      Tile tile = Main.tile[(int) tileCoordinates.X, (int) tileCoordinates.Y];
      Vector2 position;
      // ISSUE: explicit reference operation
      ((Vector2) @position).\u002Ector((float) (tileCoordinates.X * 16 + 8), (float) (tileCoordinates.Y * 16 + 8));
      if (!WorldGen.SolidOrSlopedTile(tile))
        return -1;
      int num = (int) tile.slope();
      bool flag = tile.halfBrick();
      for (int index = 0; index < (flag ? 2 : PortalHelper.EDGES.Length); ++index)
      {
        Point bestPosition;
        if ((double) Vector2.Dot(PortalHelper.EDGES[index], vector2_1) > 0.0 && PortalHelper.FindValidLine(tileCoordinates, (int) PortalHelper.EDGES[index].Y, (int) -PortalHelper.EDGES[index].X, out bestPosition))
        {
          // ISSUE: explicit reference operation
          ((Vector2) @position).\u002Ector((float) (bestPosition.X * 16 + 8), (float) (bestPosition.Y * 16 + 8));
          return PortalHelper.AddPortal(Vector2.op_Subtraction(position, Vector2.op_Multiply(PortalHelper.EDGES[index], flag ? 0.0f : 8f)), (float) Math.Atan2((double) PortalHelper.EDGES[index].Y, (double) PortalHelper.EDGES[index].X) + 1.570796f, (int) theBolt.ai[0], theBolt.direction);
        }
      }
      if (num != 0)
      {
        Vector2 vector2_2 = PortalHelper.SLOPE_EDGES[num - 1];
        Point bestPosition;
        if ((double) Vector2.Dot(vector2_2, Vector2.op_UnaryNegation(vector2_1)) > 0.0 && PortalHelper.FindValidLine(tileCoordinates, (int) -PortalHelper.SLOPE_OFFSETS[num - 1].Y, (int) PortalHelper.SLOPE_OFFSETS[num - 1].X, out bestPosition))
        {
          // ISSUE: explicit reference operation
          ((Vector2) @position).\u002Ector((float) (bestPosition.X * 16 + 8), (float) (bestPosition.Y * 16 + 8));
          return PortalHelper.AddPortal(position, (float) Math.Atan2((double) vector2_2.Y, (double) vector2_2.X) - 1.570796f, (int) theBolt.ai[0], theBolt.direction);
        }
      }
      return -1;
    }

    private static bool FindValidLine(Point position, int xOffset, int yOffset, out Point bestPosition)
    {
      bestPosition = position;
      if (PortalHelper.IsValidLine(position, xOffset, yOffset))
        return true;
      Point position1;
      // ISSUE: explicit reference operation
      ((Point) @position1).\u002Ector(position.X - xOffset, position.Y - yOffset);
      if (PortalHelper.IsValidLine(position1, xOffset, yOffset))
      {
        bestPosition = position1;
        return true;
      }
      Point position2;
      // ISSUE: explicit reference operation
      ((Point) @position2).\u002Ector(position.X + xOffset, position.Y + yOffset);
      if (!PortalHelper.IsValidLine(position2, xOffset, yOffset))
        return false;
      bestPosition = position2;
      return true;
    }

    private static bool IsValidLine(Point position, int xOffset, int yOffset)
    {
      Tile tile1 = Main.tile[(int) position.X, (int) position.Y];
      Tile tile2 = Main.tile[position.X - xOffset, position.Y - yOffset];
      Tile tile3 = Main.tile[position.X + xOffset, position.Y + yOffset];
      return !PortalHelper.BlockPortals(Main.tile[position.X + yOffset, position.Y - xOffset]) && !PortalHelper.BlockPortals(Main.tile[position.X + yOffset - xOffset, position.Y - xOffset - yOffset]) && (!PortalHelper.BlockPortals(Main.tile[position.X + yOffset + xOffset, position.Y - xOffset + yOffset]) && WorldGen.SolidOrSlopedTile(tile1)) && (WorldGen.SolidOrSlopedTile(tile2) && WorldGen.SolidOrSlopedTile(tile3) && (tile2.HasSameSlope(tile1) && tile3.HasSameSlope(tile1)));
    }

    private static bool BlockPortals(Tile t)
    {
      return t.active() && !Main.tileCut[(int) t.type] && (!TileID.Sets.BreakableWhenPlacing[(int) t.type] && Main.tileSolid[(int) t.type]);
    }

    private static Vector2 FindCollision(Vector2 startPosition, Vector2 stopPosition)
    {
      int lastX = 0;
      int lastY = 0;
      Utils.PlotLine(startPosition.ToTileCoordinates(), stopPosition.ToTileCoordinates(), (Utils.PerLinePoint) ((x, y) =>
      {
        lastX = x;
        lastY = y;
        return !WorldGen.SolidOrSlopedTile(x, y);
      }), false);
      return new Vector2((float) lastX * 16f, (float) lastY * 16f);
    }

    private static int AddPortal(Vector2 position, float angle, int form, int direction)
    {
      if (!PortalHelper.SupportedTilesAreFine(position, angle))
        return -1;
      PortalHelper.RemoveMyOldPortal(form);
      PortalHelper.RemoveIntersectingPortals(position, angle);
      int index = Projectile.NewProjectile((float) position.X, (float) position.Y, 0.0f, 0.0f, 602, 0, 0.0f, Main.myPlayer, angle, (float) form);
      Main.projectile[index].direction = direction;
      Main.projectile[index].netUpdate = true;
      return index;
    }

    private static void RemoveMyOldPortal(int form)
    {
      for (int index = 0; index < 1000; ++index)
      {
        Projectile projectile = Main.projectile[index];
        if (projectile.active && projectile.type == 602 && (projectile.owner == Main.myPlayer && (double) projectile.ai[1] == (double) form))
        {
          projectile.Kill();
          break;
        }
      }
    }

    private static void RemoveIntersectingPortals(Vector2 position, float angle)
    {
      Vector2 start1;
      Vector2 end1;
      PortalHelper.GetPortalEdges(position, angle, out start1, out end1);
      for (int number = 0; number < 1000; ++number)
      {
        Projectile projectile = Main.projectile[number];
        if (projectile.active && projectile.type == 602)
        {
          Vector2 start2;
          Vector2 end2;
          PortalHelper.GetPortalEdges(projectile.Center, projectile.ai[0], out start2, out end2);
          if (Collision.CheckLinevLine(start1, end1, start2, end2).Length > 0)
          {
            if (projectile.owner != Main.myPlayer && Main.netMode != 2)
              NetMessage.SendData(95, -1, -1, (NetworkText) null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            projectile.Kill();
            if (Main.netMode == 2)
              NetMessage.SendData(29, -1, -1, (NetworkText) null, projectile.whoAmI, (float) projectile.owner, 0.0f, 0.0f, 0, 0, 0);
          }
        }
      }
    }

    public static Color GetPortalColor(int colorIndex)
    {
      return PortalHelper.GetPortalColor(colorIndex / 2, colorIndex % 2);
    }

    public static Color GetPortalColor(int player, int portal)
    {
      Color.get_White();
      Color color;
      if (Main.netMode == 0)
      {
        color = portal != 0 ? Main.hslToRgb(0.52f, 1f, 0.6f) : Main.hslToRgb(0.12f, 1f, 0.5f);
      }
      else
      {
        float num = 0.08f;
        color = Main.hslToRgb((float) ((0.5 + (double) player * ((double) num * 2.0) + (double) portal * (double) num) % 1.0), 1f, 0.5f);
      }
      // ISSUE: explicit reference operation
      ((Color) @color).set_A((byte) 66);
      return color;
    }

    private static void GetPortalEdges(Vector2 position, float angle, out Vector2 start, out Vector2 end)
    {
      Vector2 rotationVector2 = angle.ToRotationVector2();
      start = Vector2.op_Addition(position, Vector2.op_Multiply(rotationVector2, -22f));
      end = Vector2.op_Addition(position, Vector2.op_Multiply(rotationVector2, 22f));
    }

    private static Vector2 GetPortalOutingPoint(Vector2 objectSize, Vector2 portalPosition, float portalAngle, out int bonusX, out int bonusY)
    {
      int num = (int) Math.Round((double) MathHelper.WrapAngle(portalAngle) / 0.785398185253143);
      switch (num)
      {
        case 2:
        case -2:
          bonusX = num == 2 ? -1 : 1;
          bonusY = 0;
          return Vector2.op_Addition(portalPosition, new Vector2(num == 2 ? (float) (double) -objectSize.X : 0.0f, (float) (-objectSize.Y / 2.0)));
        case 0:
        case 4:
          bonusX = 0;
          bonusY = num == 0 ? 1 : -1;
          return Vector2.op_Addition(portalPosition, new Vector2((float) (-objectSize.X / 2.0), num == 0 ? 0.0f : (float) (double) -objectSize.Y));
        case -3:
        case 3:
          bonusX = num == -3 ? 1 : -1;
          bonusY = -1;
          return Vector2.op_Addition(portalPosition, new Vector2(num == -3 ? 0.0f : (float) (double) -objectSize.X, (float) -objectSize.Y));
        case 1:
        case -1:
          bonusX = num == -1 ? 1 : -1;
          bonusY = 1;
          return Vector2.op_Addition(portalPosition, new Vector2(num == -1 ? 0.0f : (float) (double) -objectSize.X, 0.0f));
        default:
          Main.NewText("Broken portal! (over4s = " + (object) num + ")", byte.MaxValue, byte.MaxValue, byte.MaxValue, false);
          bonusX = 0;
          bonusY = 0;
          return portalPosition;
      }
    }

    public static void SyncPortalsOnPlayerJoin(int plr, int fluff, List<Point> dontInclude, out List<Point> portals, out List<Point> portalCenters)
    {
      portals = new List<Point>();
      portalCenters = new List<Point>();
      for (int index1 = 0; index1 < 1000; ++index1)
      {
        Projectile projectile = Main.projectile[index1];
        if (projectile.active && (projectile.type == 602 || projectile.type == 601))
        {
          Vector2 center = projectile.Center;
          int sectionX = Netplay.GetSectionX((int) (center.X / 16.0));
          int sectionY = Netplay.GetSectionY((int) (center.Y / 16.0));
          for (int index2 = sectionX - fluff; index2 < sectionX + fluff + 1; ++index2)
          {
            for (int index3 = sectionY - fluff; index3 < sectionY + fluff + 1; ++index3)
            {
              if (index2 >= 0 && index2 < Main.maxSectionsX && (index3 >= 0 && index3 < Main.maxSectionsY) && (!Netplay.Clients[plr].TileSections[index2, index3] && !dontInclude.Contains(new Point(index2, index3))))
              {
                portals.Add(new Point(index2, index3));
                if (!portalCenters.Contains(new Point(sectionX, sectionY)))
                  portalCenters.Add(new Point(sectionX, sectionY));
              }
            }
          }
        }
      }
    }

    public static void SyncPortalSections(Vector2 portalPosition, int fluff)
    {
      for (int playerIndex = 0; playerIndex < (int) byte.MaxValue; ++playerIndex)
      {
        if (Main.player[playerIndex].active)
          RemoteClient.CheckSection(playerIndex, portalPosition, fluff);
      }
    }

    public static bool SupportedTilesAreFine(Vector2 portalCenter, float portalAngle)
    {
      Point tileCoordinates = portalCenter.ToTileCoordinates();
      int num1 = (int) Math.Round((double) MathHelper.WrapAngle(portalAngle) / 0.785398185253143);
      int num2;
      int num3;
      switch (num1)
      {
        case 2:
        case -2:
          num2 = num1 == 2 ? -1 : 1;
          num3 = 0;
          break;
        case 0:
        case 4:
          num2 = 0;
          num3 = num1 == 0 ? 1 : -1;
          break;
        case -3:
        case 3:
          num2 = num1 == -3 ? 1 : -1;
          num3 = -1;
          break;
        case 1:
        case -1:
          num2 = num1 == -1 ? 1 : -1;
          num3 = 1;
          break;
        default:
          Main.NewText("Broken portal! (over4s = " + (object) num1 + " , " + (object) portalAngle + ")", byte.MaxValue, byte.MaxValue, byte.MaxValue, false);
          return false;
      }
      if (num2 != 0 && num3 != 0)
      {
        int num4 = 3;
        if (num2 == -1 && num3 == 1)
          num4 = 5;
        if (num2 == 1 && num3 == -1)
          num4 = 2;
        if (num2 == 1 && num3 == 1)
          num4 = 4;
        int slope = num4 - 1;
        if (PortalHelper.SupportedSlope((int) tileCoordinates.X, (int) tileCoordinates.Y, slope) && PortalHelper.SupportedSlope(tileCoordinates.X + num2, tileCoordinates.Y - num3, slope))
          return PortalHelper.SupportedSlope(tileCoordinates.X - num2, tileCoordinates.Y + num3, slope);
        return false;
      }
      if (num2 != 0)
      {
        if (num2 == 1)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Point& local = @tileCoordinates;
          // ISSUE: explicit reference operation
          int num4 = (^local).X - 1;
          // ISSUE: explicit reference operation
          (^local).X = (__Null) num4;
        }
        if (PortalHelper.SupportedNormal((int) tileCoordinates.X, (int) tileCoordinates.Y) && PortalHelper.SupportedNormal((int) tileCoordinates.X, tileCoordinates.Y - 1))
          return PortalHelper.SupportedNormal((int) tileCoordinates.X, tileCoordinates.Y + 1);
        return false;
      }
      if (num3 == 0)
        return true;
      if (num3 == 1)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Point& local = @tileCoordinates;
        // ISSUE: explicit reference operation
        int num4 = (^local).Y - 1;
        // ISSUE: explicit reference operation
        (^local).Y = (__Null) num4;
      }
      if (PortalHelper.SupportedNormal((int) tileCoordinates.X, (int) tileCoordinates.Y) && PortalHelper.SupportedNormal(tileCoordinates.X + 1, (int) tileCoordinates.Y) && PortalHelper.SupportedNormal(tileCoordinates.X - 1, (int) tileCoordinates.Y))
        return true;
      if (PortalHelper.SupportedHalfbrick((int) tileCoordinates.X, (int) tileCoordinates.Y) && PortalHelper.SupportedHalfbrick(tileCoordinates.X + 1, (int) tileCoordinates.Y))
        return PortalHelper.SupportedHalfbrick(tileCoordinates.X - 1, (int) tileCoordinates.Y);
      return false;
    }

    private static bool SupportedSlope(int x, int y, int slope)
    {
      Tile tile = Main.tile[x, y];
      if (tile != null && tile.nactive() && (!Main.tileCut[(int) tile.type] && !TileID.Sets.BreakableWhenPlacing[(int) tile.type]) && Main.tileSolid[(int) tile.type])
        return (int) tile.slope() == slope;
      return false;
    }

    private static bool SupportedHalfbrick(int x, int y)
    {
      Tile tile = Main.tile[x, y];
      if (tile != null && tile.nactive() && (!Main.tileCut[(int) tile.type] && !TileID.Sets.BreakableWhenPlacing[(int) tile.type]) && Main.tileSolid[(int) tile.type])
        return tile.halfBrick();
      return false;
    }

    private static bool SupportedNormal(int x, int y)
    {
      Tile tile = Main.tile[x, y];
      if (tile != null && tile.nactive() && (!Main.tileCut[(int) tile.type] && !TileID.Sets.BreakableWhenPlacing[(int) tile.type]) && (Main.tileSolid[(int) tile.type] && !TileID.Sets.NotReallySolid[(int) tile.type] && !tile.halfBrick()))
        return (int) tile.slope() == 0;
      return false;
    }
  }
}
