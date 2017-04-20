// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.WorldUIAnchor
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.GameContent.UI
{
  public class WorldUIAnchor
  {
    public Vector2 pos = Vector2.get_Zero();
    public Vector2 size = Vector2.get_Zero();
    public WorldUIAnchor.AnchorType type;
    public Entity entity;

    public WorldUIAnchor()
    {
      this.type = WorldUIAnchor.AnchorType.None;
    }

    public WorldUIAnchor(Entity anchor)
    {
      this.type = WorldUIAnchor.AnchorType.Entity;
      this.entity = anchor;
    }

    public WorldUIAnchor(Vector2 anchor)
    {
      this.type = WorldUIAnchor.AnchorType.Pos;
      this.pos = anchor;
    }

    public WorldUIAnchor(int topLeftX, int topLeftY, int width, int height)
    {
      this.type = WorldUIAnchor.AnchorType.Tile;
      this.pos = Vector2.op_Multiply(new Vector2((float) topLeftX + (float) width / 2f, (float) topLeftY + (float) height / 2f), 16f);
      this.size = Vector2.op_Multiply(new Vector2((float) width, (float) height), 16f);
    }

    public bool InRange(Vector2 target, float tileRangeX, float tileRangeY)
    {
      switch (this.type)
      {
        case WorldUIAnchor.AnchorType.Entity:
          if ((double) Math.Abs((float) (target.X - this.entity.Center.X)) <= (double) tileRangeX * 16.0 + (double) this.entity.width / 2.0)
            return (double) Math.Abs((float) (target.Y - this.entity.Center.Y)) <= (double) tileRangeY * 16.0 + (double) this.entity.height / 2.0;
          return false;
        case WorldUIAnchor.AnchorType.Tile:
          if ((double) Math.Abs((float) (target.X - this.pos.X)) <= (double) tileRangeX * 16.0 + this.size.X / 2.0)
            return (double) Math.Abs((float) (target.Y - this.pos.Y)) <= (double) tileRangeY * 16.0 + this.size.Y / 2.0;
          return false;
        case WorldUIAnchor.AnchorType.Pos:
          if ((double) Math.Abs((float) (target.X - this.pos.X)) <= (double) tileRangeX * 16.0)
            return (double) Math.Abs((float) (target.Y - this.pos.Y)) <= (double) tileRangeY * 16.0;
          return false;
        default:
          return true;
      }
    }

    public enum AnchorType
    {
      Entity,
      Tile,
      Pos,
      None,
    }
  }
}
