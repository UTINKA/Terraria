// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.NPCAimedTarget
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
        return Vector2.op_Addition(this.Position, Vector2.op_Division(this.Size, 2f));
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
