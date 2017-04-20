// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.NPCUtils
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.Utilities
{
  public static class NPCUtils
  {
    public static NPCUtils.TargetSearchResults SearchForTarget(Vector2 position, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
    {
      return NPCUtils.SearchForTarget((NPC) null, position, flags, playerFilter, npcFilter);
    }

    public static NPCUtils.TargetSearchResults SearchForTarget(NPC searcher, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
    {
      return NPCUtils.SearchForTarget(searcher, searcher.Center, flags, playerFilter, npcFilter);
    }

    public static NPCUtils.TargetSearchResults SearchForTarget(NPC searcher, Vector2 position, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
    {
      float num1 = float.MaxValue;
      int nearestNPCIndex = -1;
      float adjustedTankDistance = float.MaxValue;
      float nearestTankDistance = float.MaxValue;
      int nearestTankIndex = -1;
      NPCUtils.TargetType tankType = NPCUtils.TargetType.Player;
      if (flags.HasFlag((Enum) NPCUtils.TargetSearchFlag.NPCs))
      {
        for (int index = 0; index < 200; ++index)
        {
          NPC entity = Main.npc[index];
          if (entity.active && (npcFilter == null || npcFilter(entity)))
          {
            float num2 = Vector2.DistanceSquared(position, entity.Center);
            if ((double) num2 < (double) num1)
            {
              nearestNPCIndex = index;
              num1 = num2;
            }
          }
        }
      }
      if (flags.HasFlag((Enum) NPCUtils.TargetSearchFlag.Players))
      {
        for (int index = 0; index < (int) byte.MaxValue; ++index)
        {
          Player entity = Main.player[index];
          if (entity.active && !entity.dead && !entity.ghost && (playerFilter == null || playerFilter(entity)))
          {
            float num2 = Vector2.Distance(position, entity.Center);
            float num3 = num2 - (float) entity.aggro;
            bool flag = searcher != null && entity.npcTypeNoAggro[searcher.type];
            if (searcher != null && flag && searcher.direction == 0)
              num3 += 1000f;
            if ((double) num3 < (double) adjustedTankDistance)
            {
              nearestTankIndex = index;
              adjustedTankDistance = num3;
              nearestTankDistance = num2;
              tankType = NPCUtils.TargetType.Player;
            }
            if (entity.tankPet >= 0 && !flag)
            {
              Vector2 center = Main.projectile[entity.tankPet].Center;
              float num4 = Vector2.Distance(position, center);
              float num5 = num4 - 200f;
              if ((double) num5 < (double) adjustedTankDistance && (double) num5 < 200.0 && Collision.CanHit(position, 0, 0, center, 0, 0))
              {
                nearestTankIndex = index;
                adjustedTankDistance = num5;
                nearestTankDistance = num4;
                tankType = NPCUtils.TargetType.TankPet;
              }
            }
          }
        }
      }
      return new NPCUtils.TargetSearchResults(searcher, nearestNPCIndex, (float) Math.Sqrt((double) num1), nearestTankIndex, nearestTankDistance, adjustedTankDistance, tankType);
    }

    public static void TargetClosestOldOnesInvasion(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
    {
      NPCUtils.TargetSearchResults searchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilters.OnlyPlayersInCertainDistance(searcher.Center, 200f), new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.OnlyCrystal));
      if (!searchResults.FoundTarget)
        return;
      searcher.target = searchResults.NearestTargetIndex;
      searcher.targetRect = searchResults.NearestTargetHitbox;
      if (!searcher.ShouldFaceTarget(ref searchResults, new NPCUtils.TargetType?()) || !faceTarget)
        return;
      searcher.FaceTarget();
    }

    public static void TargetClosestBetsy(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
    {
      NPCUtils.TargetSearchResults searchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.All, (NPCUtils.SearchFilter<Player>) null, new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.OnlyCrystal));
      if (!searchResults.FoundTarget)
        return;
      NPCUtils.TargetType targetType = searchResults.NearestTargetType;
      if (searchResults.FoundTank && !searchResults.NearestTankOwner.dead)
        targetType = NPCUtils.TargetType.Player;
      searcher.target = searchResults.NearestTargetIndex;
      searcher.targetRect = searchResults.NearestTargetHitbox;
      if (!searcher.ShouldFaceTarget(ref searchResults, new NPCUtils.TargetType?(targetType)) || !faceTarget)
        return;
      searcher.FaceTarget();
    }

    public delegate bool SearchFilter<T>(T entity) where T : Entity;

    public static class SearchFilters
    {
      public static bool OnlyCrystal(NPC npc)
      {
        if (npc.type == 548)
          return !npc.dontTakeDamageFromHostiles;
        return false;
      }

      public static NPCUtils.SearchFilter<Player> OnlyPlayersInCertainDistance(Vector2 position, float maxDistance)
      {
        return (NPCUtils.SearchFilter<Player>) (player => (double) player.Distance(position) <= (double) maxDistance);
      }
    }

    public enum TargetType
    {
      None,
      NPC,
      Player,
      TankPet,
    }

    public struct TargetSearchResults
    {
      private NPCUtils.TargetType _nearestTargetType;
      private int _nearestNPCIndex;
      private float _nearestNPCDistance;
      private int _nearestTankIndex;
      private float _nearestTankDistance;
      private float _adjustedTankDistance;
      private NPCUtils.TargetType _nearestTankType;

      public int NearestTargetIndex
      {
        get
        {
          switch (this._nearestTargetType)
          {
            case NPCUtils.TargetType.NPC:
              return this.NearestNPC.WhoAmIToTargettingIndex;
            case NPCUtils.TargetType.Player:
            case NPCUtils.TargetType.TankPet:
              return this._nearestTankIndex;
            default:
              return -1;
          }
        }
      }

      public Rectangle NearestTargetHitbox
      {
        get
        {
          switch (this._nearestTargetType)
          {
            case NPCUtils.TargetType.NPC:
              return this.NearestNPC.Hitbox;
            case NPCUtils.TargetType.Player:
              return this.NearestTankOwner.Hitbox;
            case NPCUtils.TargetType.TankPet:
              return Main.projectile[this.NearestTankOwner.tankPet].Hitbox;
            default:
              return Rectangle.get_Empty();
          }
        }
      }

      public NPCUtils.TargetType NearestTargetType
      {
        get
        {
          return this._nearestTargetType;
        }
      }

      public bool FoundTarget
      {
        get
        {
          return this._nearestTargetType != NPCUtils.TargetType.None;
        }
      }

      public NPC NearestNPC
      {
        get
        {
          if (this._nearestNPCIndex != -1)
            return Main.npc[this._nearestNPCIndex];
          return (NPC) null;
        }
      }

      public bool FoundNPC
      {
        get
        {
          return this._nearestNPCIndex != -1;
        }
      }

      public int NearestNPCIndex
      {
        get
        {
          return this._nearestNPCIndex;
        }
      }

      public float NearestNPCDistance
      {
        get
        {
          return this._nearestNPCDistance;
        }
      }

      public Player NearestTankOwner
      {
        get
        {
          if (this._nearestTankIndex != -1)
            return Main.player[this._nearestTankIndex];
          return (Player) null;
        }
      }

      public bool FoundTank
      {
        get
        {
          return this._nearestTankIndex != -1;
        }
      }

      public int NearestTankOwnerIndex
      {
        get
        {
          return this._nearestTankIndex;
        }
      }

      public float NearestTankDistance
      {
        get
        {
          return this._nearestTankDistance;
        }
      }

      public float AdjustedTankDistance
      {
        get
        {
          return this._adjustedTankDistance;
        }
      }

      public NPCUtils.TargetType NearestTankType
      {
        get
        {
          return this._nearestTankType;
        }
      }

      public TargetSearchResults(NPC searcher, int nearestNPCIndex, float nearestNPCDistance, int nearestTankIndex, float nearestTankDistance, float adjustedTankDistance, NPCUtils.TargetType tankType)
      {
        this._nearestNPCIndex = nearestNPCIndex;
        this._nearestNPCDistance = nearestNPCDistance;
        this._nearestTankIndex = nearestTankIndex;
        this._adjustedTankDistance = adjustedTankDistance;
        this._nearestTankDistance = nearestTankDistance;
        this._nearestTankType = tankType;
        if (this._nearestNPCIndex != -1 && this._nearestTankIndex != -1)
        {
          if ((double) this._nearestNPCDistance < (double) this._adjustedTankDistance)
            this._nearestTargetType = NPCUtils.TargetType.NPC;
          else
            this._nearestTargetType = tankType;
        }
        else if (this._nearestNPCIndex != -1)
          this._nearestTargetType = NPCUtils.TargetType.NPC;
        else if (this._nearestTankIndex != -1)
          this._nearestTargetType = tankType;
        else
          this._nearestTargetType = NPCUtils.TargetType.None;
      }
    }

    [Flags]
    public enum TargetSearchFlag
    {
      None = 0,
      NPCs = 1,
      Players = 2,
      All = Players | NPCs,
    }
  }
}
