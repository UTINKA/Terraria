// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.NPCAimedTarget
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Enums;

namespace Terraria.DataStructures
{
  public struct NPCAimedTarget
  {
    public NPCTargetType Type;
    public Rectangle Hitbox;
    public int Width;
    public int Height;
    public Vector2 Position;
    public Vector2 Velocity;

    public bool Invalid
    {
      get
      {
        return this.Type == NPCTargetType.None;
      }
    }

    public Vector2 Center
    {
      get
      {
        return this.Position + this.Size / 2f;
      }
    }

    public Vector2 Size
    {
      get
      {
        return new Vector2((float) this.Width, (float) this.Height);
      }
    }

    public NPCAimedTarget(NPC npc)
    {
      this.Type = NPCTargetType.NPC;
      this.Hitbox = npc.Hitbox;
      this.Width = npc.width;
      this.Height = npc.height;
      this.Position = npc.position;
      this.Velocity = npc.velocity;
    }

    public NPCAimedTarget(Player player, bool ignoreTank = true)
    {
      this.Type = NPCTargetType.Player;
      this.Hitbox = player.Hitbox;
      this.Width = player.width;
      this.Height = player.height;
      this.Position = player.position;
      this.Velocity = player.velocity;
      if (ignoreTank || player.tankPet <= -1)
        return;
      Projectile projectile = Main.projectile[player.tankPet];
      this.Type = NPCTargetType.PlayerTankPet;
      this.Hitbox = projectile.Hitbox;
      this.Width = projectile.width;
      this.Height = projectile.height;
      this.Position = projectile.position;
      this.Velocity = projectile.velocity;
    }
  }
}
