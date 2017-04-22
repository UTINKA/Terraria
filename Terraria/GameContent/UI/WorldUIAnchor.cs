// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.WorldUIAnchor
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.GameContent.UI
{
  public class WorldUIAnchor
  {
    public Vector2 pos = Vector2.Zero;
    public Vector2 size = Vector2.Zero;
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
      this.pos = new Vector2((float) topLeftX + (float) width / 2f, (float) topLeftY + (float) height / 2f) * 16f;
      this.size = new Vector2((float) width, (float) height) * 16f;
    }

    public bool InRange(Vector2 target, float tileRangeX, float tileRangeY)
    {
      switch (this.type)
      {
        case WorldUIAnchor.AnchorType.Entity:
          if ((double) Math.Abs(target.X - this.entity.Center.X) <= (double) tileRangeX * 16.0 + (double) this.entity.width / 2.0)
            return (double) Math.Abs(target.Y - this.entity.Center.Y) <= (double) tileRangeY * 16.0 + (double) this.entity.height / 2.0;
          return false;
        case WorldUIAnchor.AnchorType.Tile:
          if ((double) Math.Abs(target.X - this.pos.X) <= (double) tileRangeX * 16.0 + (double) this.size.X / 2.0)
            return (double) Math.Abs(target.Y - this.pos.Y) <= (double) tileRangeY * 16.0 + (double) this.size.Y / 2.0;
          return false;
        case WorldUIAnchor.AnchorType.Pos:
          if ((double) Math.Abs(target.X - this.pos.X) <= (double) tileRangeX * 16.0)
            return (double) Math.Abs(target.Y - this.pos.Y) <= (double) tileRangeY * 16.0;
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
