// Decompiled with JetBrains decompiler
// Type: Terraria.NetMessage
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Ionic.Zlib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Net.Sockets;
using Terraria.Social;

namespace Terraria
{
  public class NetMessage
  {
    public static MessageBuffer[] buffer = new MessageBuffer[257];
    private static PlayerDeathReason _currentPlayerDeathReason;

    public static void SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, string text = "", int number = 0, float number2 = 0.0f, float number3 = 0.0f, float number4 = 0.0f, int number5 = 0, int number6 = 0, int number7 = 0)
    {
      if (Main.netMode == 0)
        return;
      int whoAmi = 256;
      if (Main.netMode == 2 && remoteClient >= 0)
        whoAmi = remoteClient;
      lock (NetMessage.buffer[whoAmi])
      {
        BinaryWriter local_1 = NetMessage.buffer[whoAmi].writer;
        if (local_1 == null)
        {
          NetMessage.buffer[whoAmi].ResetWriter();
          local_1 = NetMessage.buffer[whoAmi].writer;
        }
        local_1.BaseStream.Position = 0L;
        long local_2 = local_1.BaseStream.Position;
        local_1.BaseStream.Position += 2L;
        local_1.Write((byte) msgType);
        switch (msgType)
        {
          case 1:
            local_1.Write("Terraria" + (object) 188);
            break;
          case 2:
            local_1.Write(text);
            if (Main.dedServ)
            {
              Console.WriteLine(Language.GetTextValue("CLI.ClientWasBooted", (object) Netplay.Clients[whoAmi].Socket.GetRemoteAddress().ToString(), (object) text));
              break;
            }
            break;
          case 3:
            local_1.Write((byte) remoteClient);
            break;
          case 4:
            Player local_3 = Main.player[number];
            local_1.Write((byte) number);
            local_1.Write((byte) local_3.skinVariant);
            local_1.Write((byte) local_3.hair);
            local_1.Write(text);
            local_1.Write(local_3.hairDye);
            BitsByte local_4 = (BitsByte) (byte) 0;
            for (int local_5 = 0; local_5 < 8; ++local_5)
              local_4[local_5] = local_3.hideVisual[local_5];
            local_1.Write((byte) local_4);
            BitsByte local_4_1 = (BitsByte) (byte) 0;
            for (int local_6 = 0; local_6 < 2; ++local_6)
              local_4_1[local_6] = local_3.hideVisual[local_6 + 8];
            local_1.Write((byte) local_4_1);
            local_1.Write((byte) local_3.hideMisc);
            local_1.WriteRGB(local_3.hairColor);
            local_1.WriteRGB(local_3.skinColor);
            local_1.WriteRGB(local_3.eyeColor);
            local_1.WriteRGB(local_3.shirtColor);
            local_1.WriteRGB(local_3.underShirtColor);
            local_1.WriteRGB(local_3.pantsColor);
            local_1.WriteRGB(local_3.shoeColor);
            BitsByte local_7 = (BitsByte) (byte) 0;
            if ((int) local_3.difficulty == 1)
              local_7[0] = true;
            else if ((int) local_3.difficulty == 2)
              local_7[1] = true;
            local_7[2] = local_3.extraAccessory;
            local_1.Write((byte) local_7);
            break;
          case 5:
            local_1.Write((byte) number);
            local_1.Write((byte) number2);
            Player local_8 = Main.player[number];
            Item local_9_1 = (double) number2 <= (double) (58 + local_8.armor.Length + local_8.dye.Length + local_8.miscEquips.Length + local_8.miscDyes.Length + local_8.bank.item.Length + local_8.bank2.item.Length + 1) ? ((double) number2 <= (double) (58 + local_8.armor.Length + local_8.dye.Length + local_8.miscEquips.Length + local_8.miscDyes.Length + local_8.bank.item.Length + local_8.bank2.item.Length) ? ((double) number2 <= (double) (58 + local_8.armor.Length + local_8.dye.Length + local_8.miscEquips.Length + local_8.miscDyes.Length + local_8.bank.item.Length) ? ((double) number2 <= (double) (58 + local_8.armor.Length + local_8.dye.Length + local_8.miscEquips.Length + local_8.miscDyes.Length) ? ((double) number2 <= (double) (58 + local_8.armor.Length + local_8.dye.Length + local_8.miscEquips.Length) ? ((double) number2 <= (double) (58 + local_8.armor.Length + local_8.dye.Length) ? ((double) number2 <= (double) (58 + local_8.armor.Length) ? ((double) number2 <= 58.0 ? local_8.inventory[(int) number2] : local_8.armor[(int) number2 - 58 - 1]) : local_8.dye[(int) number2 - 58 - local_8.armor.Length - 1]) : local_8.miscEquips[(int) number2 - 58 - (local_8.armor.Length + local_8.dye.Length) - 1]) : local_8.miscDyes[(int) number2 - 58 - (local_8.armor.Length + local_8.dye.Length + local_8.miscEquips.Length) - 1]) : local_8.bank.item[(int) number2 - 58 - (local_8.armor.Length + local_8.dye.Length + local_8.miscEquips.Length + local_8.miscDyes.Length) - 1]) : local_8.bank2.item[(int) number2 - 58 - (local_8.armor.Length + local_8.dye.Length + local_8.miscEquips.Length + local_8.miscDyes.Length + local_8.bank.item.Length) - 1]) : local_8.trashItem) : local_8.bank3.item[(int) number2 - 58 - (local_8.armor.Length + local_8.dye.Length + local_8.miscEquips.Length + local_8.miscDyes.Length + local_8.bank.item.Length + local_8.bank2.item.Length + 1) - 1];
            if (local_9_1.name == "" || local_9_1.stack == 0 || local_9_1.type == 0)
              local_9_1.SetDefaults(0, false);
            int local_10_1 = local_9_1.stack;
            int local_11_1 = local_9_1.netID;
            if (local_10_1 < 0)
              local_10_1 = 0;
            local_1.Write((short) local_10_1);
            local_1.Write((byte) number3);
            local_1.Write((short) local_11_1);
            break;
          case 7:
            local_1.Write((int) Main.time);
            BitsByte local_12 = (BitsByte) (byte) 0;
            local_12[0] = Main.dayTime;
            local_12[1] = Main.bloodMoon;
            local_12[2] = Main.eclipse;
            local_1.Write((byte) local_12);
            local_1.Write((byte) Main.moonPhase);
            local_1.Write((short) Main.maxTilesX);
            local_1.Write((short) Main.maxTilesY);
            local_1.Write((short) Main.spawnTileX);
            local_1.Write((short) Main.spawnTileY);
            local_1.Write((short) Main.worldSurface);
            local_1.Write((short) Main.rockLayer);
            local_1.Write(Main.worldID);
            local_1.Write(Main.worldName);
            local_1.Write(Main.ActiveWorldFileData.UniqueId.ToString());
            local_1.Write((byte) Main.moonType);
            local_1.Write((byte) WorldGen.treeBG);
            local_1.Write((byte) WorldGen.corruptBG);
            local_1.Write((byte) WorldGen.jungleBG);
            local_1.Write((byte) WorldGen.snowBG);
            local_1.Write((byte) WorldGen.hallowBG);
            local_1.Write((byte) WorldGen.crimsonBG);
            local_1.Write((byte) WorldGen.desertBG);
            local_1.Write((byte) WorldGen.oceanBG);
            local_1.Write((byte) Main.iceBackStyle);
            local_1.Write((byte) Main.jungleBackStyle);
            local_1.Write((byte) Main.hellBackStyle);
            local_1.Write(Main.windSpeedSet);
            local_1.Write((byte) Main.numClouds);
            for (int local_13 = 0; local_13 < 3; ++local_13)
              local_1.Write(Main.treeX[local_13]);
            for (int local_14 = 0; local_14 < 4; ++local_14)
              local_1.Write((byte) Main.treeStyle[local_14]);
            for (int local_15 = 0; local_15 < 3; ++local_15)
              local_1.Write(Main.caveBackX[local_15]);
            for (int local_16 = 0; local_16 < 4; ++local_16)
              local_1.Write((byte) Main.caveBackStyle[local_16]);
            if (!Main.raining)
              Main.maxRaining = 0.0f;
            local_1.Write(Main.maxRaining);
            BitsByte local_17 = (BitsByte) (byte) 0;
            local_17[0] = WorldGen.shadowOrbSmashed;
            local_17[1] = NPC.downedBoss1;
            local_17[2] = NPC.downedBoss2;
            local_17[3] = NPC.downedBoss3;
            local_17[4] = Main.hardMode;
            local_17[5] = NPC.downedClown;
            local_17[7] = NPC.downedPlantBoss;
            local_1.Write((byte) local_17);
            BitsByte local_18 = (BitsByte) (byte) 0;
            local_18[0] = NPC.downedMechBoss1;
            local_18[1] = NPC.downedMechBoss2;
            local_18[2] = NPC.downedMechBoss3;
            local_18[3] = NPC.downedMechBossAny;
            local_18[4] = (double) Main.cloudBGActive >= 1.0;
            local_18[5] = WorldGen.crimson;
            local_18[6] = Main.pumpkinMoon;
            local_18[7] = Main.snowMoon;
            local_1.Write((byte) local_18);
            BitsByte local_19 = (BitsByte) (byte) 0;
            local_19[0] = Main.expertMode;
            local_19[1] = Main.fastForwardTime;
            local_19[2] = Main.slimeRain;
            local_19[3] = NPC.downedSlimeKing;
            local_19[4] = NPC.downedQueenBee;
            local_19[5] = NPC.downedFishron;
            local_19[6] = NPC.downedMartians;
            local_19[7] = NPC.downedAncientCultist;
            local_1.Write((byte) local_19);
            BitsByte local_20 = (BitsByte) (byte) 0;
            local_20[0] = NPC.downedMoonlord;
            local_20[1] = NPC.downedHalloweenKing;
            local_20[2] = NPC.downedHalloweenTree;
            local_20[3] = NPC.downedChristmasIceQueen;
            local_20[4] = NPC.downedChristmasSantank;
            local_20[5] = NPC.downedChristmasTree;
            local_20[6] = NPC.downedGolemBoss;
            local_20[7] = BirthdayParty.PartyIsUp;
            local_1.Write((byte) local_20);
            BitsByte local_21 = (BitsByte) (byte) 0;
            local_21[0] = NPC.downedPirates;
            local_21[1] = NPC.downedFrost;
            local_21[2] = NPC.downedGoblins;
            local_21[3] = Sandstorm.Happening;
            local_21[4] = DD2Event.Ongoing;
            local_21[5] = DD2Event.DownedInvasionT1;
            local_21[6] = DD2Event.DownedInvasionT2;
            local_21[7] = DD2Event.DownedInvasionT3;
            local_1.Write((byte) local_21);
            local_1.Write((sbyte) Main.invasionType);
            if (SocialAPI.Network != null)
              local_1.Write(SocialAPI.Network.GetLobbyId());
            else
              local_1.Write(0UL);
            local_1.Write(Sandstorm.IntendedSeverity);
            break;
          case 8:
            local_1.Write(number);
            local_1.Write((int) number2);
            break;
          case 9:
            local_1.Write(number);
            local_1.Write(text);
            break;
          case 10:
            int local_22 = NetMessage.CompressTileBlock(number, (int) number2, (short) number3, (short) number4, NetMessage.buffer[whoAmi].writeBuffer, (int) local_1.BaseStream.Position);
            local_1.BaseStream.Position += (long) local_22;
            break;
          case 11:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write((short) number3);
            local_1.Write((short) number4);
            break;
          case 12:
            local_1.Write((byte) number);
            local_1.Write((short) Main.player[number].SpawnX);
            local_1.Write((short) Main.player[number].SpawnY);
            break;
          case 13:
            Player local_23 = Main.player[number];
            local_1.Write((byte) number);
            BitsByte local_24 = (BitsByte) (byte) 0;
            local_24[0] = local_23.controlUp;
            local_24[1] = local_23.controlDown;
            local_24[2] = local_23.controlLeft;
            local_24[3] = local_23.controlRight;
            local_24[4] = local_23.controlJump;
            local_24[5] = local_23.controlUseItem;
            local_24[6] = local_23.direction == 1;
            local_1.Write((byte) local_24);
            BitsByte local_25 = (BitsByte) (byte) 0;
            local_25[0] = local_23.pulley;
            local_25[1] = local_23.pulley && (int) local_23.pulleyDir == 2;
            local_25[2] = local_23.velocity != Vector2.Zero;
            local_25[3] = local_23.vortexStealthActive;
            local_25[4] = (double) local_23.gravDir == 1.0;
            local_25[5] = local_23.shieldRaised;
            local_1.Write((byte) local_25);
            local_1.Write((byte) local_23.selectedItem);
            local_1.WriteVector2(local_23.position);
            if (local_25[2])
            {
              local_1.WriteVector2(local_23.velocity);
              break;
            }
            break;
          case 14:
            local_1.Write((byte) number);
            local_1.Write((byte) number2);
            break;
          case 16:
            local_1.Write((byte) number);
            local_1.Write((short) Main.player[number].statLife);
            local_1.Write((short) Main.player[number].statLifeMax);
            break;
          case 17:
            local_1.Write((byte) number);
            local_1.Write((short) number2);
            local_1.Write((short) number3);
            local_1.Write((short) number4);
            local_1.Write((byte) number5);
            break;
          case 18:
            local_1.Write(Main.dayTime ? (byte) 1 : (byte) 0);
            local_1.Write((int) Main.time);
            local_1.Write(Main.sunModY);
            local_1.Write(Main.moonModY);
            break;
          case 19:
            local_1.Write((byte) number);
            local_1.Write((short) number2);
            local_1.Write((short) number3);
            local_1.Write((double) number4 == 1.0 ? (byte) 1 : (byte) 0);
            break;
          case 20:
            int local_26 = number;
            int local_27 = (int) number2;
            int local_28 = (int) number3;
            if (local_26 < 0)
              local_26 = 0;
            if (local_27 < local_26)
              local_27 = local_26;
            if (local_27 >= Main.maxTilesX + local_26)
              local_27 = Main.maxTilesX - local_26 - 1;
            if (local_28 < local_26)
              local_28 = local_26;
            if (local_28 >= Main.maxTilesY + local_26)
              local_28 = Main.maxTilesY - local_26 - 1;
            if (number5 == 0)
            {
              local_1.Write((ushort) (local_26 & (int) short.MaxValue));
            }
            else
            {
              local_1.Write((ushort) (local_26 & (int) short.MaxValue | 32768));
              local_1.Write((byte) number5);
            }
            local_1.Write((short) local_27);
            local_1.Write((short) local_28);
            for (int local_29 = local_27; local_29 < local_27 + local_26; ++local_29)
            {
              for (int local_30 = local_28; local_30 < local_28 + local_26; ++local_30)
              {
                BitsByte local_31 = (BitsByte) (byte) 0;
                BitsByte local_32 = (BitsByte) (byte) 0;
                byte local_33 = 0;
                byte local_34 = 0;
                Tile local_35 = Main.tile[local_29, local_30];
                local_31[0] = local_35.active();
                local_31[2] = (int) local_35.wall > 0;
                local_31[3] = (int) local_35.liquid > 0 && Main.netMode == 2;
                local_31[4] = local_35.wire();
                local_31[5] = local_35.halfBrick();
                local_31[6] = local_35.actuator();
                local_31[7] = local_35.inActive();
                local_32[0] = local_35.wire2();
                local_32[1] = local_35.wire3();
                if (local_35.active() && (int) local_35.color() > 0)
                {
                  local_32[2] = true;
                  local_33 = local_35.color();
                }
                if ((int) local_35.wall > 0 && (int) local_35.wallColor() > 0)
                {
                  local_32[3] = true;
                  local_34 = local_35.wallColor();
                }
                local_32 = (BitsByte) ((byte) ((uint) (byte) local_32 + (uint) (byte) ((uint) local_35.slope() << 4)));
                local_32[7] = local_35.wire4();
                local_1.Write((byte) local_31);
                local_1.Write((byte) local_32);
                if ((int) local_33 > 0)
                  local_1.Write(local_33);
                if ((int) local_34 > 0)
                  local_1.Write(local_34);
                if (local_35.active())
                {
                  local_1.Write(local_35.type);
                  if (Main.tileFrameImportant[(int) local_35.type])
                  {
                    local_1.Write(local_35.frameX);
                    local_1.Write(local_35.frameY);
                  }
                }
                if ((int) local_35.wall > 0)
                  local_1.Write(local_35.wall);
                if ((int) local_35.liquid > 0 && Main.netMode == 2)
                {
                  local_1.Write(local_35.liquid);
                  local_1.Write(local_35.liquidType());
                }
              }
            }
            break;
          case 21:
          case 90:
            Item local_36 = Main.item[number];
            local_1.Write((short) number);
            local_1.WriteVector2(local_36.position);
            local_1.WriteVector2(local_36.velocity);
            local_1.Write((short) local_36.stack);
            local_1.Write(local_36.prefix);
            local_1.Write((byte) number2);
            short local_37 = 0;
            if (local_36.active && local_36.stack > 0)
              local_37 = (short) local_36.netID;
            local_1.Write(local_37);
            break;
          case 22:
            local_1.Write((short) number);
            local_1.Write((byte) Main.item[number].owner);
            break;
          case 23:
            NPC local_38 = Main.npc[number];
            local_1.Write((short) number);
            local_1.WriteVector2(local_38.position);
            local_1.WriteVector2(local_38.velocity);
            local_1.Write((ushort) local_38.target);
            int local_39 = local_38.life;
            if (!local_38.active)
              local_39 = 0;
            if (!local_38.active || local_38.life <= 0)
              local_38.netSkip = 0;
            if (local_38.name == null)
              local_38.name = "";
            short local_40 = (short) local_38.netID;
            bool[] local_41 = new bool[4];
            BitsByte local_42 = (BitsByte) (byte) 0;
            local_42[0] = local_38.direction > 0;
            local_42[1] = local_38.directionY > 0;
            local_42[2] = local_41[0] = (double) local_38.ai[0] != 0.0;
            local_42[3] = local_41[1] = (double) local_38.ai[1] != 0.0;
            local_42[4] = local_41[2] = (double) local_38.ai[2] != 0.0;
            local_42[5] = local_41[3] = (double) local_38.ai[3] != 0.0;
            local_42[6] = local_38.spriteDirection > 0;
            local_42[7] = local_39 == local_38.lifeMax;
            local_1.Write((byte) local_42);
            for (int local_43 = 0; local_43 < NPC.maxAI; ++local_43)
            {
              if (local_41[local_43])
                local_1.Write(local_38.ai[local_43]);
            }
            local_1.Write(local_40);
            if (!local_42[7])
            {
              byte local_44 = Main.npcLifeBytes[local_38.netID];
              local_1.Write(local_44);
              if ((int) local_44 == 2)
                local_1.Write((short) local_39);
              else if ((int) local_44 == 4)
                local_1.Write(local_39);
              else
                local_1.Write((sbyte) local_39);
            }
            if (local_38.type >= 0 && local_38.type < 580 && Main.npcCatchable[local_38.type])
            {
              local_1.Write((byte) local_38.releaseOwner);
              break;
            }
            break;
          case 24:
            local_1.Write((short) number);
            local_1.Write((byte) number2);
            break;
          case 25:
            local_1.Write((byte) number);
            local_1.Write((byte) number2);
            local_1.Write((byte) number3);
            local_1.Write((byte) number4);
            local_1.Write(text);
            break;
          case 26:
            local_1.Write((byte) number);
            local_1.Write((byte) ((double) number2 + 1.0));
            local_1.Write((short) number3);
            local_1.Write(text);
            BitsByte local_45 = (BitsByte) (byte) 0;
            local_45[0] = (double) number4 == 1.0;
            local_45[1] = number5 == 1;
            local_45[2] = number6 == 0;
            local_45[3] = number6 == 1;
            local_45[4] = number6 == 2;
            local_1.Write((byte) local_45);
            break;
          case 27:
            Projectile local_46 = Main.projectile[number];
            local_1.Write((short) local_46.identity);
            local_1.WriteVector2(local_46.position);
            local_1.WriteVector2(local_46.velocity);
            local_1.Write(local_46.knockBack);
            local_1.Write((short) local_46.damage);
            local_1.Write((byte) local_46.owner);
            local_1.Write((short) local_46.type);
            BitsByte local_47 = (BitsByte) (byte) 0;
            for (int local_48 = 0; local_48 < Projectile.maxAI; ++local_48)
            {
              if ((double) local_46.ai[local_48] != 0.0)
                local_47[local_48] = true;
            }
            if (local_46.type > 0 && local_46.type < 714 && ProjectileID.Sets.NeedsUUID[local_46.type])
              local_47[Projectile.maxAI] = true;
            local_1.Write((byte) local_47);
            for (int local_49 = 0; local_49 < Projectile.maxAI; ++local_49)
            {
              if (local_47[local_49])
                local_1.Write(local_46.ai[local_49]);
            }
            if (local_47[Projectile.maxAI])
            {
              local_1.Write((short) local_46.projUUID);
              break;
            }
            break;
          case 28:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write(number3);
            local_1.Write((byte) ((double) number4 + 1.0));
            local_1.Write((byte) number5);
            break;
          case 29:
            local_1.Write((short) number);
            local_1.Write((byte) number2);
            break;
          case 30:
            local_1.Write((byte) number);
            local_1.Write(Main.player[number].hostile);
            break;
          case 31:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            break;
          case 32:
            Item local_50 = Main.chest[number].item[(int) (byte) number2];
            local_1.Write((short) number);
            local_1.Write((byte) number2);
            short local_51 = (short) local_50.netID;
            if (local_50.name == null)
              local_51 = (short) 0;
            local_1.Write((short) local_50.stack);
            local_1.Write(local_50.prefix);
            local_1.Write(local_51);
            break;
          case 33:
            int local_52 = 0;
            int local_53 = 0;
            int local_54 = 0;
            string local_55 = (string) null;
            if (number > -1)
            {
              local_52 = Main.chest[number].x;
              local_53 = Main.chest[number].y;
            }
            if ((double) number2 == 1.0)
            {
              local_54 = (int) (byte) text.Length;
              if (local_54 == 0 || local_54 > 20)
                local_54 = (int) byte.MaxValue;
              else
                local_55 = text;
            }
            local_1.Write((short) number);
            local_1.Write((short) local_52);
            local_1.Write((short) local_53);
            local_1.Write((byte) local_54);
            if (local_55 != null)
            {
              local_1.Write(local_55);
              break;
            }
            break;
          case 34:
            local_1.Write((byte) number);
            local_1.Write((short) number2);
            local_1.Write((short) number3);
            local_1.Write((short) number4);
            if (Main.netMode == 2)
            {
              Netplay.GetSectionX((int) number2);
              Netplay.GetSectionY((int) number3);
              local_1.Write((short) number5);
              break;
            }
            local_1.Write((short) 0);
            break;
          case 35:
          case 66:
            local_1.Write((byte) number);
            local_1.Write((short) number2);
            break;
          case 36:
            Player local_56 = Main.player[number];
            local_1.Write((byte) number);
            local_1.Write((byte) local_56.zone1);
            local_1.Write((byte) local_56.zone2);
            local_1.Write((byte) local_56.zone3);
            local_1.Write((byte) local_56.zone4);
            break;
          case 38:
            local_1.Write(text);
            break;
          case 39:
            local_1.Write((short) number);
            break;
          case 40:
            local_1.Write((byte) number);
            local_1.Write((short) Main.player[number].talkNPC);
            break;
          case 41:
            local_1.Write((byte) number);
            local_1.Write(Main.player[number].itemRotation);
            local_1.Write((short) Main.player[number].itemAnimation);
            break;
          case 42:
            local_1.Write((byte) number);
            local_1.Write((short) Main.player[number].statMana);
            local_1.Write((short) Main.player[number].statManaMax);
            break;
          case 43:
            local_1.Write((byte) number);
            local_1.Write((short) number2);
            break;
          case 44:
            local_1.Write((byte) number);
            local_1.Write((byte) ((double) number2 + 1.0));
            local_1.Write((short) number3);
            local_1.Write((byte) number4);
            local_1.Write(text);
            break;
          case 45:
            local_1.Write((byte) number);
            local_1.Write((byte) Main.player[number].team);
            break;
          case 46:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            break;
          case 47:
            local_1.Write((short) number);
            local_1.Write((short) Main.sign[number].x);
            local_1.Write((short) Main.sign[number].y);
            local_1.Write(Main.sign[number].text);
            local_1.Write((byte) number2);
            break;
          case 48:
            Tile local_57 = Main.tile[number, (int) number2];
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write(local_57.liquid);
            local_1.Write(local_57.liquidType());
            break;
          case 50:
            local_1.Write((byte) number);
            for (int local_58 = 0; local_58 < 22; ++local_58)
              local_1.Write((byte) Main.player[number].buffType[local_58]);
            break;
          case 51:
            local_1.Write((byte) number);
            local_1.Write((byte) number2);
            break;
          case 52:
            local_1.Write((byte) number2);
            local_1.Write((short) number3);
            local_1.Write((short) number4);
            break;
          case 53:
            local_1.Write((short) number);
            local_1.Write((byte) number2);
            local_1.Write((short) number3);
            break;
          case 54:
            local_1.Write((short) number);
            for (int local_59 = 0; local_59 < 5; ++local_59)
            {
              local_1.Write((byte) Main.npc[number].buffType[local_59]);
              local_1.Write((short) Main.npc[number].buffTime[local_59]);
            }
            break;
          case 55:
            local_1.Write((byte) number);
            local_1.Write((byte) number2);
            local_1.Write((int) number3);
            break;
          case 56:
            string local_60 = (string) null;
            if (Main.netMode == 2)
              local_60 = Main.npc[number].displayName;
            else if (Main.netMode == 1)
              local_60 = text;
            local_1.Write((short) number);
            local_1.Write(local_60);
            break;
          case 57:
            local_1.Write(WorldGen.tGood);
            local_1.Write(WorldGen.tEvil);
            local_1.Write(WorldGen.tBlood);
            break;
          case 58:
            local_1.Write((byte) number);
            local_1.Write(number2);
            break;
          case 59:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            break;
          case 60:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write((short) number3);
            local_1.Write((byte) number4);
            break;
          case 61:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            break;
          case 62:
            local_1.Write((byte) number);
            local_1.Write((byte) number2);
            break;
          case 63:
          case 64:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write((byte) number3);
            break;
          case 65:
            BitsByte local_61 = (BitsByte) (byte) 0;
            local_61[0] = (number & 1) == 1;
            local_61[1] = (number & 2) == 2;
            local_61[2] = (number5 & 1) == 1;
            local_61[3] = (number5 & 2) == 2;
            local_1.Write((byte) local_61);
            local_1.Write((short) number2);
            local_1.Write(number3);
            local_1.Write(number4);
            break;
          case 68:
            local_1.Write(Main.clientUUID);
            break;
          case 69:
            Netplay.GetSectionX((int) number2);
            Netplay.GetSectionY((int) number3);
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write((short) number3);
            local_1.Write(text);
            break;
          case 70:
            local_1.Write((short) number);
            local_1.Write((byte) number2);
            break;
          case 71:
            local_1.Write(number);
            local_1.Write((int) number2);
            local_1.Write((short) number3);
            local_1.Write((byte) number4);
            break;
          case 72:
            for (int local_62 = 0; local_62 < 40; ++local_62)
              local_1.Write((short) Main.travelShop[local_62]);
            break;
          case 74:
            local_1.Write((byte) Main.anglerQuest);
            bool local_63 = Main.anglerWhoFinishedToday.Contains(text);
            local_1.Write(local_63);
            break;
          case 76:
            local_1.Write((byte) number);
            local_1.Write(Main.player[number].anglerQuestsFinished);
            break;
          case 77:
            if (Main.netMode != 2)
              return;
            local_1.Write((short) number);
            local_1.Write((ushort) number2);
            local_1.Write((short) number3);
            local_1.Write((short) number4);
            break;
          case 78:
            local_1.Write(number);
            local_1.Write((int) number2);
            local_1.Write((sbyte) number3);
            local_1.Write((sbyte) number4);
            break;
          case 79:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write((short) number3);
            local_1.Write((short) number4);
            local_1.Write((byte) number5);
            local_1.Write((sbyte) number6);
            local_1.Write(number7 == 1);
            break;
          case 80:
            local_1.Write((byte) number);
            local_1.Write((short) number2);
            break;
          case 81:
            local_1.Write(number2);
            local_1.Write(number3);
            local_1.WriteRGB(new Color()
            {
              PackedValue = (uint) number
            });
            local_1.Write(text);
            break;
          case 83:
            int local_65 = number;
            if (local_65 < 0 && local_65 >= 267)
              local_65 = 1;
            int local_66 = NPC.killCount[local_65];
            local_1.Write((short) local_65);
            local_1.Write(local_66);
            break;
          case 84:
            byte local_67 = (byte) number;
            float local_68 = Main.player[(int) local_67].stealth;
            local_1.Write(local_67);
            local_1.Write(local_68);
            break;
          case 85:
            byte local_69 = (byte) number;
            local_1.Write(local_69);
            break;
          case 86:
            local_1.Write(number);
            bool local_70 = TileEntity.ByID.ContainsKey(number);
            local_1.Write(local_70);
            if (local_70)
            {
              TileEntity.Write(local_1, TileEntity.ByID[number], true);
              break;
            }
            break;
          case 87:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write((byte) number3);
            break;
          case 88:
            BitsByte local_71 = (BitsByte) ((byte) number2);
            BitsByte local_72 = (BitsByte) ((byte) number3);
            local_1.Write((short) number);
            local_1.Write((byte) local_71);
            Item local_73 = Main.item[number];
            if (local_71[0])
              local_1.Write(local_73.color.PackedValue);
            if (local_71[1])
              local_1.Write((ushort) local_73.damage);
            if (local_71[2])
              local_1.Write(local_73.knockBack);
            if (local_71[3])
              local_1.Write((ushort) local_73.useAnimation);
            if (local_71[4])
              local_1.Write((ushort) local_73.useTime);
            if (local_71[5])
              local_1.Write((short) local_73.shoot);
            if (local_71[6])
              local_1.Write(local_73.shootSpeed);
            if (local_71[7])
            {
              local_1.Write((byte) local_72);
              if (local_72[0])
                local_1.Write((ushort) local_73.width);
              if (local_72[1])
                local_1.Write((ushort) local_73.height);
              if (local_72[2])
                local_1.Write(local_73.scale);
              if (local_72[3])
                local_1.Write((short) local_73.ammo);
              if (local_72[4])
                local_1.Write((short) local_73.useAmmo);
              if (local_72[5])
              {
                local_1.Write(local_73.notAmmo);
                break;
              }
              break;
            }
            break;
          case 89:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            Item local_74 = Main.player[(int) number4].inventory[(int) number3];
            local_1.Write((short) local_74.netID);
            local_1.Write(local_74.prefix);
            local_1.Write((short) local_74.stack);
            break;
          case 91:
            local_1.Write(number);
            local_1.Write((byte) number2);
            if ((double) number2 != (double) byte.MaxValue)
            {
              local_1.Write((ushort) number3);
              local_1.Write((byte) number4);
              local_1.Write((byte) number5);
              if (number5 < 0)
              {
                local_1.Write((short) number6);
                break;
              }
              break;
            }
            break;
          case 92:
            local_1.Write((short) number);
            local_1.Write(number2);
            local_1.Write(number3);
            local_1.Write(number4);
            break;
          case 95:
            local_1.Write((ushort) number);
            break;
          case 96:
            local_1.Write((byte) number);
            Player local_75 = Main.player[number];
            local_1.Write((short) number4);
            local_1.Write(number2);
            local_1.Write(number3);
            local_1.WriteVector2(local_75.velocity);
            break;
          case 97:
            local_1.Write((short) number);
            break;
          case 98:
            local_1.Write((short) number);
            break;
          case 99:
            local_1.Write((byte) number);
            local_1.WriteVector2(Main.player[number].MinionRestTargetPoint);
            break;
          case 100:
            local_1.Write((ushort) number);
            NPC local_76 = Main.npc[number];
            local_1.Write((short) number4);
            local_1.Write(number2);
            local_1.Write(number3);
            local_1.WriteVector2(local_76.velocity);
            break;
          case 101:
            local_1.Write((ushort) NPC.ShieldStrengthTowerSolar);
            local_1.Write((ushort) NPC.ShieldStrengthTowerVortex);
            local_1.Write((ushort) NPC.ShieldStrengthTowerNebula);
            local_1.Write((ushort) NPC.ShieldStrengthTowerStardust);
            break;
          case 102:
            local_1.Write((byte) number);
            local_1.Write((byte) number2);
            local_1.Write(number3);
            local_1.Write(number4);
            break;
          case 103:
            local_1.Write(NPC.MoonLordCountdown);
            break;
          case 104:
            local_1.Write((byte) number);
            local_1.Write((short) number2);
            local_1.Write((int) (short) number3 < 0 ? 0.0f : number3);
            local_1.Write((byte) number4);
            local_1.Write(number5);
            local_1.Write((byte) number6);
            break;
          case 105:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write((double) number3 == 1.0);
            break;
          case 106:
            HalfVector2 local_77 = new HalfVector2((float) number, number2);
            local_1.Write(local_77.PackedValue);
            break;
          case 107:
            local_1.Write((byte) number);
            local_1.Write((byte) number2);
            local_1.Write((byte) number3);
            local_1.Write((byte) number4);
            local_1.Write(text);
            local_1.Write((short) number5);
            break;
          case 108:
            local_1.Write((short) number);
            local_1.Write(number2);
            local_1.Write((short) number3);
            local_1.Write((short) number4);
            local_1.Write((short) number5);
            local_1.Write((short) number6);
            local_1.Write((byte) number7);
            break;
          case 109:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write((short) number3);
            local_1.Write((short) number4);
            local_1.Write((byte) number5);
            break;
          case 110:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            local_1.Write((byte) number3);
            break;
          case 112:
            local_1.Write((byte) number);
            local_1.Write((short) number2);
            local_1.Write((short) number3);
            local_1.Write((byte) number4);
            local_1.Write((short) number5);
            break;
          case 113:
            local_1.Write((short) number);
            local_1.Write((short) number2);
            break;
          case 115:
            local_1.Write((byte) number);
            local_1.Write((short) Main.player[number].MinionAttackTargetNPC);
            break;
          case 116:
            local_1.Write(number);
            break;
          case 117:
            local_1.Write((byte) number);
            NetMessage._currentPlayerDeathReason.WriteSelfTo(local_1);
            local_1.Write((short) number2);
            local_1.Write((byte) ((double) number3 + 1.0));
            local_1.Write((byte) number4);
            local_1.Write((sbyte) number5);
            break;
          case 118:
            local_1.Write((byte) number);
            NetMessage._currentPlayerDeathReason.WriteSelfTo(local_1);
            local_1.Write((short) number2);
            local_1.Write((byte) ((double) number3 + 1.0));
            local_1.Write((byte) number4);
            break;
        }
        int local_78 = (int) local_1.BaseStream.Position;
        local_1.BaseStream.Position = local_2;
        local_1.Write((short) local_78);
        local_1.BaseStream.Position = (long) local_78;
        if (Main.netMode == 1)
        {
          if (Netplay.Connection.Socket.IsConnected())
          {
            try
            {
              ++NetMessage.buffer[whoAmi].spamCount;
              ++Main.txMsg;
              Main.txData += local_78;
              ++Main.txMsgType[msgType];
              Main.txDataType[msgType] += local_78;
              Netplay.Connection.Socket.AsyncSend(NetMessage.buffer[whoAmi].writeBuffer, 0, local_78, new SocketSendCallback(Netplay.Connection.ClientWriteCallBack), (object) null);
            }
            catch
            {
            }
          }
        }
        else if (remoteClient == -1)
        {
          if (msgType == 34 || msgType == 69)
          {
            for (int local_79 = 0; local_79 < 256; ++local_79)
            {
              if (local_79 != ignoreClient && NetMessage.buffer[local_79].broadcast)
              {
                if (Netplay.Clients[local_79].IsConnected())
                {
                  try
                  {
                    ++NetMessage.buffer[local_79].spamCount;
                    ++Main.txMsg;
                    Main.txData += local_78;
                    ++Main.txMsgType[msgType];
                    Main.txDataType[msgType] += local_78;
                    Netplay.Clients[local_79].Socket.AsyncSend(NetMessage.buffer[whoAmi].writeBuffer, 0, local_78, new SocketSendCallback(Netplay.Clients[local_79].ServerWriteCallBack), (object) null);
                  }
                  catch
                  {
                  }
                }
              }
            }
          }
          else if (msgType == 20)
          {
            for (int local_80 = 0; local_80 < 256; ++local_80)
            {
              if (local_80 != ignoreClient && NetMessage.buffer[local_80].broadcast && Netplay.Clients[local_80].IsConnected())
              {
                if (Netplay.Clients[local_80].SectionRange(number, (int) number2, (int) number3))
                {
                  try
                  {
                    ++NetMessage.buffer[local_80].spamCount;
                    ++Main.txMsg;
                    Main.txData += local_78;
                    ++Main.txMsgType[msgType];
                    Main.txDataType[msgType] += local_78;
                    Netplay.Clients[local_80].Socket.AsyncSend(NetMessage.buffer[whoAmi].writeBuffer, 0, local_78, new SocketSendCallback(Netplay.Clients[local_80].ServerWriteCallBack), (object) null);
                  }
                  catch
                  {
                  }
                }
              }
            }
          }
          else if (msgType == 23)
          {
            NPC local_81 = Main.npc[number];
            for (int local_82 = 0; local_82 < 256; ++local_82)
            {
              if (local_82 != ignoreClient && NetMessage.buffer[local_82].broadcast && Netplay.Clients[local_82].IsConnected())
              {
                bool local_83 = false;
                if (local_81.boss || local_81.netAlways || (local_81.townNPC || !local_81.active))
                  local_83 = true;
                else if (local_81.netSkip <= 0)
                {
                  Rectangle local_84 = Main.player[local_82].getRect();
                  Rectangle local_85 = local_81.getRect();
                  local_85.X -= 2500;
                  local_85.Y -= 2500;
                  local_85.Width += 5000;
                  local_85.Height += 5000;
                  if (local_84.Intersects(local_85))
                    local_83 = true;
                }
                else
                  local_83 = true;
                if (local_83)
                {
                  try
                  {
                    ++NetMessage.buffer[local_82].spamCount;
                    ++Main.txMsg;
                    Main.txData += local_78;
                    ++Main.txMsgType[msgType];
                    Main.txDataType[msgType] += local_78;
                    Netplay.Clients[local_82].Socket.AsyncSend(NetMessage.buffer[whoAmi].writeBuffer, 0, local_78, new SocketSendCallback(Netplay.Clients[local_82].ServerWriteCallBack), (object) null);
                  }
                  catch
                  {
                  }
                }
              }
            }
            ++local_81.netSkip;
            if (local_81.netSkip > 4)
              local_81.netSkip = 0;
          }
          else if (msgType == 28)
          {
            NPC local_86 = Main.npc[number];
            for (int local_87 = 0; local_87 < 256; ++local_87)
            {
              if (local_87 != ignoreClient && NetMessage.buffer[local_87].broadcast && Netplay.Clients[local_87].IsConnected())
              {
                bool local_88 = false;
                if (local_86.life <= 0)
                {
                  local_88 = true;
                }
                else
                {
                  Rectangle local_89 = Main.player[local_87].getRect();
                  Rectangle local_90 = local_86.getRect();
                  local_90.X -= 3000;
                  local_90.Y -= 3000;
                  local_90.Width += 6000;
                  local_90.Height += 6000;
                  if (local_89.Intersects(local_90))
                    local_88 = true;
                }
                if (local_88)
                {
                  try
                  {
                    ++NetMessage.buffer[local_87].spamCount;
                    ++Main.txMsg;
                    Main.txData += local_78;
                    ++Main.txMsgType[msgType];
                    Main.txDataType[msgType] += local_78;
                    Netplay.Clients[local_87].Socket.AsyncSend(NetMessage.buffer[whoAmi].writeBuffer, 0, local_78, new SocketSendCallback(Netplay.Clients[local_87].ServerWriteCallBack), (object) null);
                  }
                  catch
                  {
                  }
                }
              }
            }
          }
          else if (msgType == 13)
          {
            for (int local_91 = 0; local_91 < 256; ++local_91)
            {
              if (local_91 != ignoreClient && NetMessage.buffer[local_91].broadcast)
              {
                if (Netplay.Clients[local_91].IsConnected())
                {
                  try
                  {
                    ++NetMessage.buffer[local_91].spamCount;
                    ++Main.txMsg;
                    Main.txData += local_78;
                    ++Main.txMsgType[msgType];
                    Main.txDataType[msgType] += local_78;
                    Netplay.Clients[local_91].Socket.AsyncSend(NetMessage.buffer[whoAmi].writeBuffer, 0, local_78, new SocketSendCallback(Netplay.Clients[local_91].ServerWriteCallBack), (object) null);
                  }
                  catch
                  {
                  }
                }
              }
            }
            ++Main.player[number].netSkip;
            if (Main.player[number].netSkip > 2)
              Main.player[number].netSkip = 0;
          }
          else if (msgType == 27)
          {
            Projectile local_92 = Main.projectile[number];
            for (int local_93 = 0; local_93 < 256; ++local_93)
            {
              if (local_93 != ignoreClient && NetMessage.buffer[local_93].broadcast && Netplay.Clients[local_93].IsConnected())
              {
                bool local_94 = false;
                if (local_92.type == 12 || Main.projPet[local_92.type] || (local_92.aiStyle == 11 || local_92.netImportant))
                {
                  local_94 = true;
                }
                else
                {
                  Rectangle local_95 = Main.player[local_93].getRect();
                  Rectangle local_96 = local_92.getRect();
                  local_96.X -= 5000;
                  local_96.Y -= 5000;
                  local_96.Width += 10000;
                  local_96.Height += 10000;
                  if (local_95.Intersects(local_96))
                    local_94 = true;
                }
                if (local_94)
                {
                  try
                  {
                    ++NetMessage.buffer[local_93].spamCount;
                    ++Main.txMsg;
                    Main.txData += local_78;
                    ++Main.txMsgType[msgType];
                    Main.txDataType[msgType] += local_78;
                    Netplay.Clients[local_93].Socket.AsyncSend(NetMessage.buffer[whoAmi].writeBuffer, 0, local_78, new SocketSendCallback(Netplay.Clients[local_93].ServerWriteCallBack), (object) null);
                  }
                  catch
                  {
                  }
                }
              }
            }
          }
          else
          {
            for (int local_97 = 0; local_97 < 256; ++local_97)
            {
              if (local_97 != ignoreClient && (NetMessage.buffer[local_97].broadcast || Netplay.Clients[local_97].State >= 3 && msgType == 10))
              {
                if (Netplay.Clients[local_97].IsConnected())
                {
                  try
                  {
                    ++NetMessage.buffer[local_97].spamCount;
                    ++Main.txMsg;
                    Main.txData += local_78;
                    ++Main.txMsgType[msgType];
                    Main.txDataType[msgType] += local_78;
                    Netplay.Clients[local_97].Socket.AsyncSend(NetMessage.buffer[whoAmi].writeBuffer, 0, local_78, new SocketSendCallback(Netplay.Clients[local_97].ServerWriteCallBack), (object) null);
                  }
                  catch
                  {
                  }
                }
              }
            }
          }
        }
        else if (Netplay.Clients[remoteClient].IsConnected())
        {
          try
          {
            ++NetMessage.buffer[remoteClient].spamCount;
            ++Main.txMsg;
            Main.txData += local_78;
            ++Main.txMsgType[msgType];
            Main.txDataType[msgType] += local_78;
            Netplay.Clients[remoteClient].Socket.AsyncSend(NetMessage.buffer[whoAmi].writeBuffer, 0, local_78, new SocketSendCallback(Netplay.Clients[remoteClient].ServerWriteCallBack), (object) null);
          }
          catch
          {
          }
        }
        if (Main.verboseNetplay)
        {
          int local_98 = 0;
          while (local_98 < local_78)
            ++local_98;
          for (int local_99 = 0; local_99 < local_78; ++local_99)
          {
            int temp_101 = (int) NetMessage.buffer[whoAmi].writeBuffer[local_99];
          }
        }
        NetMessage.buffer[whoAmi].writeLocked = false;
        if (msgType == 19 && Main.netMode == 1)
          NetMessage.SendTileSquare(whoAmi, (int) number2, (int) number3, 5, TileChangeType.None);
        if (msgType != 2 || Main.netMode != 2)
          return;
        Netplay.Clients[whoAmi].PendingTermination = true;
      }
    }

    public static int CompressTileBlock(int xStart, int yStart, short width, short height, byte[] buffer, int bufferStart)
    {
      using (MemoryStream memoryStream1 = new MemoryStream())
      {
        using (BinaryWriter writer = new BinaryWriter((Stream) memoryStream1))
        {
          writer.Write(xStart);
          writer.Write(yStart);
          writer.Write(width);
          writer.Write(height);
          NetMessage.CompressTileBlock_Inner(writer, xStart, yStart, (int) width, (int) height);
          int length = buffer.Length;
          if ((long) bufferStart + memoryStream1.Length > (long) length)
            return (int) ((long) (length - bufferStart) + memoryStream1.Length);
          memoryStream1.Position = 0L;
          MemoryStream memoryStream2 = new MemoryStream();
          using (DeflateStream deflateStream = new DeflateStream((Stream) memoryStream2, (CompressionMode) 0, true))
          {
            memoryStream1.CopyTo((Stream) deflateStream);
            ((Stream) deflateStream).Flush();
            ((Stream) deflateStream).Close();
            ((Stream) deflateStream).Dispose();
          }
          if (memoryStream1.Length <= memoryStream2.Length)
          {
            memoryStream1.Position = 0L;
            buffer[bufferStart] = (byte) 0;
            ++bufferStart;
            memoryStream1.Read(buffer, bufferStart, (int) memoryStream1.Length);
            return (int) memoryStream1.Length + 1;
          }
          memoryStream2.Position = 0L;
          buffer[bufferStart] = (byte) 1;
          ++bufferStart;
          memoryStream2.Read(buffer, bufferStart, (int) memoryStream2.Length);
          return (int) memoryStream2.Length + 1;
        }
      }
    }

    public static void CompressTileBlock_Inner(BinaryWriter writer, int xStart, int yStart, int width, int height)
    {
      short[] numArray1 = new short[1000];
      short[] numArray2 = new short[1000];
      short[] numArray3 = new short[1000];
      short num1 = 0;
      short num2 = 0;
      short num3 = 0;
      short num4 = 0;
      int index1 = 0;
      int index2 = 0;
      byte num5 = 0;
      byte[] buffer = new byte[13];
      Tile compTile = (Tile) null;
      for (int index3 = yStart; index3 < yStart + height; ++index3)
      {
        for (int index4 = xStart; index4 < xStart + width; ++index4)
        {
          Tile tile = Main.tile[index4, index3];
          if (tile.isTheSameAs(compTile))
          {
            ++num4;
          }
          else
          {
            if (compTile != null)
            {
              if ((int) num4 > 0)
              {
                buffer[index1] = (byte) ((uint) num4 & (uint) byte.MaxValue);
                ++index1;
                if ((int) num4 > (int) byte.MaxValue)
                {
                  num5 |= (byte) 128;
                  buffer[index1] = (byte) (((int) num4 & 65280) >> 8);
                  ++index1;
                }
                else
                  num5 |= (byte) 64;
              }
              buffer[index2] = num5;
              writer.Write(buffer, index2, index1 - index2);
              num4 = (short) 0;
            }
            index1 = 3;
            int num6;
            byte num7 = (byte) (num6 = 0);
            byte num8 = (byte) num6;
            num5 = (byte) num6;
            if (tile.active())
            {
              num5 |= (byte) 2;
              buffer[index1] = (byte) tile.type;
              ++index1;
              if ((int) tile.type > (int) byte.MaxValue)
              {
                buffer[index1] = (byte) ((uint) tile.type >> 8);
                ++index1;
                num5 |= (byte) 32;
              }
              if ((int) tile.type == 21 && (int) tile.frameX % 36 == 0 && (int) tile.frameY % 36 == 0)
              {
                short chest = (short) Chest.FindChest(index4, index3);
                if ((int) chest != -1)
                {
                  numArray1[(int) num1] = chest;
                  ++num1;
                }
              }
              if ((int) tile.type == 88 && (int) tile.frameX % 54 == 0 && (int) tile.frameY % 36 == 0)
              {
                short chest = (short) Chest.FindChest(index4, index3);
                if ((int) chest != -1)
                {
                  numArray1[(int) num1] = chest;
                  ++num1;
                }
              }
              if ((int) tile.type == 85 && (int) tile.frameX % 36 == 0 && (int) tile.frameY % 36 == 0)
              {
                short num9 = (short) Sign.ReadSign(index4, index3, true);
                if ((int) num9 != -1)
                  numArray2[(int) num2++] = num9;
              }
              if ((int) tile.type == 55 && (int) tile.frameX % 36 == 0 && (int) tile.frameY % 36 == 0)
              {
                short num9 = (short) Sign.ReadSign(index4, index3, true);
                if ((int) num9 != -1)
                  numArray2[(int) num2++] = num9;
              }
              if ((int) tile.type == 425 && (int) tile.frameX % 36 == 0 && (int) tile.frameY % 36 == 0)
              {
                short num9 = (short) Sign.ReadSign(index4, index3, true);
                if ((int) num9 != -1)
                  numArray2[(int) num2++] = num9;
              }
              if ((int) tile.type == 378 && (int) tile.frameX % 36 == 0 && (int) tile.frameY == 0)
              {
                int num9 = TETrainingDummy.Find(index4, index3);
                if (num9 != -1)
                  numArray3[(int) num3++] = (short) num9;
              }
              if ((int) tile.type == 395 && (int) tile.frameX % 36 == 0 && (int) tile.frameY == 0)
              {
                int num9 = TEItemFrame.Find(index4, index3);
                if (num9 != -1)
                  numArray3[(int) num3++] = (short) num9;
              }
              if (Main.tileFrameImportant[(int) tile.type])
              {
                buffer[index1] = (byte) ((uint) tile.frameX & (uint) byte.MaxValue);
                int index5 = index1 + 1;
                buffer[index5] = (byte) (((int) tile.frameX & 65280) >> 8);
                int index6 = index5 + 1;
                buffer[index6] = (byte) ((uint) tile.frameY & (uint) byte.MaxValue);
                int index7 = index6 + 1;
                buffer[index7] = (byte) (((int) tile.frameY & 65280) >> 8);
                index1 = index7 + 1;
              }
              if ((int) tile.color() != 0)
              {
                num7 |= (byte) 8;
                buffer[index1] = tile.color();
                ++index1;
              }
            }
            if ((int) tile.wall != 0)
            {
              num5 |= (byte) 4;
              buffer[index1] = tile.wall;
              ++index1;
              if ((int) tile.wallColor() != 0)
              {
                num7 |= (byte) 16;
                buffer[index1] = tile.wallColor();
                ++index1;
              }
            }
            if ((int) tile.liquid != 0)
            {
              if (tile.lava())
                num5 |= (byte) 16;
              else if (tile.honey())
                num5 |= (byte) 24;
              else
                num5 |= (byte) 8;
              buffer[index1] = tile.liquid;
              ++index1;
            }
            if (tile.wire())
              num8 |= (byte) 2;
            if (tile.wire2())
              num8 |= (byte) 4;
            if (tile.wire3())
              num8 |= (byte) 8;
            int num10 = !tile.halfBrick() ? ((int) tile.slope() == 0 ? 0 : (int) tile.slope() + 1 << 4) : 16;
            byte num11 = (byte) ((uint) num8 | (uint) (byte) num10);
            if (tile.actuator())
              num7 |= (byte) 2;
            if (tile.inActive())
              num7 |= (byte) 4;
            if (tile.wire4())
              num7 |= (byte) 32;
            index2 = 2;
            if ((int) num7 != 0)
            {
              num11 |= (byte) 1;
              buffer[index2] = num7;
              --index2;
            }
            if ((int) num11 != 0)
            {
              num5 |= (byte) 1;
              buffer[index2] = num11;
              --index2;
            }
            compTile = tile;
          }
        }
      }
      if ((int) num4 > 0)
      {
        buffer[index1] = (byte) ((uint) num4 & (uint) byte.MaxValue);
        ++index1;
        if ((int) num4 > (int) byte.MaxValue)
        {
          num5 |= (byte) 128;
          buffer[index1] = (byte) (((int) num4 & 65280) >> 8);
          ++index1;
        }
        else
          num5 |= (byte) 64;
      }
      buffer[index2] = num5;
      writer.Write(buffer, index2, index1 - index2);
      writer.Write(num1);
      for (int index3 = 0; index3 < (int) num1; ++index3)
      {
        Chest chest = Main.chest[(int) numArray1[index3]];
        writer.Write(numArray1[index3]);
        writer.Write((short) chest.x);
        writer.Write((short) chest.y);
        writer.Write(chest.name);
      }
      writer.Write(num2);
      for (int index3 = 0; index3 < (int) num2; ++index3)
      {
        Sign sign = Main.sign[(int) numArray2[index3]];
        writer.Write(numArray2[index3]);
        writer.Write((short) sign.x);
        writer.Write((short) sign.y);
        writer.Write(sign.text);
      }
      writer.Write(num3);
      for (int index3 = 0; index3 < (int) num3; ++index3)
        TileEntity.Write(writer, TileEntity.ByID[(int) numArray3[index3]], false);
    }

    public static void DecompressTileBlock(byte[] buffer, int bufferStart, int bufferLength)
    {
      using (MemoryStream memoryStream1 = new MemoryStream())
      {
        memoryStream1.Write(buffer, bufferStart, bufferLength);
        memoryStream1.Position = 0L;
        MemoryStream memoryStream2;
        if (memoryStream1.ReadByte() != 0)
        {
          MemoryStream memoryStream3 = new MemoryStream();
          using (DeflateStream deflateStream = new DeflateStream((Stream) memoryStream1, (CompressionMode) 1, true))
          {
            ((Stream) deflateStream).CopyTo((Stream) memoryStream3);
            ((Stream) deflateStream).Close();
          }
          memoryStream2 = memoryStream3;
          memoryStream2.Position = 0L;
        }
        else
        {
          memoryStream2 = memoryStream1;
          memoryStream2.Position = 1L;
        }
        using (BinaryReader reader = new BinaryReader((Stream) memoryStream2))
        {
          int xStart = reader.ReadInt32();
          int yStart = reader.ReadInt32();
          short num1 = reader.ReadInt16();
          short num2 = reader.ReadInt16();
          NetMessage.DecompressTileBlock_Inner(reader, xStart, yStart, (int) num1, (int) num2);
        }
      }
    }

    public static void DecompressTileBlock_Inner(BinaryReader reader, int xStart, int yStart, int width, int height)
    {
      Tile tile = (Tile) null;
      int num1 = 0;
      for (int index1 = yStart; index1 < yStart + height; ++index1)
      {
        for (int index2 = xStart; index2 < xStart + width; ++index2)
        {
          if (num1 != 0)
          {
            --num1;
            if (Main.tile[index2, index1] == null)
              Main.tile[index2, index1] = new Tile(tile);
            else
              Main.tile[index2, index1].CopyFrom(tile);
          }
          else
          {
            byte num2;
            byte num3 = num2 = (byte) 0;
            tile = Main.tile[index2, index1];
            if (tile == null)
            {
              tile = new Tile();
              Main.tile[index2, index1] = tile;
            }
            else
              tile.ClearEverything();
            byte num4 = reader.ReadByte();
            if (((int) num4 & 1) == 1)
            {
              num3 = reader.ReadByte();
              if (((int) num3 & 1) == 1)
                num2 = reader.ReadByte();
            }
            bool flag = tile.active();
            if (((int) num4 & 2) == 2)
            {
              tile.active(true);
              ushort type = tile.type;
              int index3;
              if (((int) num4 & 32) == 32)
              {
                byte num5 = reader.ReadByte();
                index3 = (int) reader.ReadByte() << 8 | (int) num5;
              }
              else
                index3 = (int) reader.ReadByte();
              tile.type = (ushort) index3;
              if (Main.tileFrameImportant[index3])
              {
                tile.frameX = reader.ReadInt16();
                tile.frameY = reader.ReadInt16();
              }
              else if (!flag || (int) tile.type != (int) type)
              {
                tile.frameX = (short) -1;
                tile.frameY = (short) -1;
              }
              if (((int) num2 & 8) == 8)
                tile.color(reader.ReadByte());
            }
            if (((int) num4 & 4) == 4)
            {
              tile.wall = reader.ReadByte();
              if (((int) num2 & 16) == 16)
                tile.wallColor(reader.ReadByte());
            }
            byte num6 = (byte) (((int) num4 & 24) >> 3);
            if ((int) num6 != 0)
            {
              tile.liquid = reader.ReadByte();
              if ((int) num6 > 1)
              {
                if ((int) num6 == 2)
                  tile.lava(true);
                else
                  tile.honey(true);
              }
            }
            if ((int) num3 > 1)
            {
              if (((int) num3 & 2) == 2)
                tile.wire(true);
              if (((int) num3 & 4) == 4)
                tile.wire2(true);
              if (((int) num3 & 8) == 8)
                tile.wire3(true);
              byte num5 = (byte) (((int) num3 & 112) >> 4);
              if ((int) num5 != 0 && Main.tileSolid[(int) tile.type])
              {
                if ((int) num5 == 1)
                  tile.halfBrick(true);
                else
                  tile.slope((byte) ((uint) num5 - 1U));
              }
            }
            if ((int) num2 > 0)
            {
              if (((int) num2 & 2) == 2)
                tile.actuator(true);
              if (((int) num2 & 4) == 4)
                tile.inActive(true);
              if (((int) num2 & 32) == 32)
                tile.wire4(true);
            }
            switch ((byte) (((int) num4 & 192) >> 6))
            {
              case 0:
                num1 = 0;
                continue;
              case 1:
                num1 = (int) reader.ReadByte();
                continue;
              default:
                num1 = (int) reader.ReadInt16();
                continue;
            }
          }
        }
      }
      short num7 = reader.ReadInt16();
      for (int index = 0; index < (int) num7; ++index)
      {
        short num2 = reader.ReadInt16();
        short num3 = reader.ReadInt16();
        short num4 = reader.ReadInt16();
        string str = reader.ReadString();
        if ((int) num2 >= 0 && (int) num2 < 1000)
        {
          if (Main.chest[(int) num2] == null)
            Main.chest[(int) num2] = new Chest(false);
          Main.chest[(int) num2].name = str;
          Main.chest[(int) num2].x = (int) num3;
          Main.chest[(int) num2].y = (int) num4;
        }
      }
      short num8 = reader.ReadInt16();
      for (int index = 0; index < (int) num8; ++index)
      {
        short num2 = reader.ReadInt16();
        short num3 = reader.ReadInt16();
        short num4 = reader.ReadInt16();
        string str = reader.ReadString();
        if ((int) num2 >= 0 && (int) num2 < 1000)
        {
          if (Main.sign[(int) num2] == null)
            Main.sign[(int) num2] = new Sign();
          Main.sign[(int) num2].text = str;
          Main.sign[(int) num2].x = (int) num3;
          Main.sign[(int) num2].y = (int) num4;
        }
      }
      short num9 = reader.ReadInt16();
      for (int index = 0; index < (int) num9; ++index)
      {
        TileEntity tileEntity = TileEntity.Read(reader, false);
        TileEntity.ByID[tileEntity.ID] = tileEntity;
        TileEntity.ByPosition[tileEntity.Position] = tileEntity;
      }
    }

    public static void RecieveBytes(byte[] bytes, int streamLength, int i = 256)
    {
      lock (NetMessage.buffer[i])
      {
        try
        {
          Buffer.BlockCopy((Array) bytes, 0, (Array) NetMessage.buffer[i].readBuffer, NetMessage.buffer[i].totalData, streamLength);
          NetMessage.buffer[i].totalData += streamLength;
          NetMessage.buffer[i].checkBytes = true;
        }
        catch
        {
          if (Main.netMode == 1)
          {
            Main.menuMode = 15;
            Main.statusText = Language.GetTextValue("Error.BadHeaderBufferOverflow");
            Netplay.disconnect = true;
          }
          else
            Netplay.Clients[i].PendingTermination = true;
        }
      }
    }

    public static void CheckBytes(int bufferIndex = 256)
    {
      lock (NetMessage.buffer[bufferIndex])
      {
        int local_0 = 0;
        int local_1 = NetMessage.buffer[bufferIndex].totalData;
        try
        {
          while (local_1 >= 2)
          {
            int local_2 = (int) BitConverter.ToUInt16(NetMessage.buffer[bufferIndex].readBuffer, local_0);
            if (local_1 >= local_2)
            {
              long local_3 = NetMessage.buffer[bufferIndex].reader.BaseStream.Position;
              int local_4;
              NetMessage.buffer[bufferIndex].GetData(local_0 + 2, local_2 - 2, out local_4);
              NetMessage.buffer[bufferIndex].reader.BaseStream.Position = local_3 + (long) local_2;
              local_1 -= local_2;
              local_0 += local_2;
            }
            else
              break;
          }
        }
        catch
        {
          local_1 = 0;
          local_0 = 0;
        }
        if (local_1 != NetMessage.buffer[bufferIndex].totalData)
        {
          for (int local_5 = 0; local_5 < local_1; ++local_5)
            NetMessage.buffer[bufferIndex].readBuffer[local_5] = NetMessage.buffer[bufferIndex].readBuffer[local_5 + local_0];
          NetMessage.buffer[bufferIndex].totalData = local_1;
        }
        NetMessage.buffer[bufferIndex].checkBytes = false;
      }
    }

    public static void BootPlayer(int plr, string msg)
    {
      NetMessage.SendData(2, plr, -1, msg, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public static void SendObjectPlacment(int whoAmi, int x, int y, int type, int style, int alternative, int random, int direction)
    {
      int remoteClient;
      int ignoreClient;
      if (Main.netMode == 2)
      {
        remoteClient = -1;
        ignoreClient = whoAmi;
      }
      else
      {
        remoteClient = whoAmi;
        ignoreClient = -1;
      }
      NetMessage.SendData(79, remoteClient, ignoreClient, "", x, (float) y, (float) type, (float) style, alternative, random, direction);
    }

    public static void SendTemporaryAnimation(int whoAmi, int animationType, int tileType, int xCoord, int yCoord)
    {
      NetMessage.SendData(77, whoAmi, -1, "", animationType, (float) tileType, (float) xCoord, (float) yCoord, 0, 0, 0);
    }

    public static void SendPlayerHurt(int playerTargetIndex, PlayerDeathReason reason, int damage, int direction, bool critical, bool pvp, int hitContext, int remoteClient = -1, int ignoreClient = -1)
    {
      NetMessage._currentPlayerDeathReason = reason;
      BitsByte bitsByte = (BitsByte) (byte) 0;
      bitsByte[0] = critical;
      bitsByte[1] = pvp;
      NetMessage.SendData(117, remoteClient, ignoreClient, "", playerTargetIndex, (float) damage, (float) direction, (float) (byte) bitsByte, hitContext, 0, 0);
    }

    public static void SendPlayerDeath(int playerTargetIndex, PlayerDeathReason reason, int damage, int direction, bool pvp, int remoteClient = -1, int ignoreClient = -1)
    {
      NetMessage._currentPlayerDeathReason = reason;
      BitsByte bitsByte = (BitsByte) (byte) 0;
      bitsByte[0] = pvp;
      NetMessage.SendData(118, remoteClient, ignoreClient, "", playerTargetIndex, (float) damage, (float) direction, (float) (byte) bitsByte, 0, 0, 0);
    }

    public static void SendTileRange(int whoAmi, int tileX, int tileY, int xSize, int ySize, TileChangeType changeType = TileChangeType.None)
    {
      int number = xSize >= ySize ? xSize : ySize;
      NetMessage.SendData(20, whoAmi, -1, "", number, (float) tileX, (float) tileY, 0.0f, (int) changeType, 0, 0);
    }

    public static void SendTileSquare(int whoAmi, int tileX, int tileY, int size, TileChangeType changeType = TileChangeType.None)
    {
      int num = (size - 1) / 2;
      NetMessage.SendData(20, whoAmi, -1, "", size, (float) (tileX - num), (float) (tileY - num), 0.0f, (int) changeType, 0, 0);
    }

    public static void SendTravelShop(int remoteClient)
    {
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(72, remoteClient, -1, "", 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public static void SendAnglerQuest(int remoteClient)
    {
      if (Main.netMode != 2)
        return;
      if (remoteClient == -1)
      {
        for (int remoteClient1 = 0; remoteClient1 < (int) byte.MaxValue; ++remoteClient1)
        {
          if (Netplay.Clients[remoteClient1].State == 10)
            NetMessage.SendData(74, remoteClient1, -1, Main.player[remoteClient1].name, Main.anglerQuest, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
      }
      else
      {
        if (Netplay.Clients[remoteClient].State != 10)
          return;
        NetMessage.SendData(74, remoteClient, -1, Main.player[remoteClient].name, Main.anglerQuest, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
    }

    public static void SendSection(int whoAmi, int sectionX, int sectionY, bool skipSent = false)
    {
      if (Main.netMode != 2)
        return;
      try
      {
        if (sectionX < 0 || sectionY < 0 || (sectionX >= Main.maxSectionsX || sectionY >= Main.maxSectionsY) || skipSent && Netplay.Clients[whoAmi].TileSections[sectionX, sectionY])
          return;
        Netplay.Clients[whoAmi].TileSections[sectionX, sectionY] = true;
        int number1 = sectionX * 200;
        int num1 = sectionY * 150;
        int num2 = 150;
        int num3 = num1;
        while (num3 < num1 + 150)
        {
          NetMessage.SendData(10, whoAmi, -1, "", number1, (float) num3, 200f, (float) num2, 0, 0, 0);
          num3 += num2;
        }
        for (int number2 = 0; number2 < 200; ++number2)
        {
          if (Main.npc[number2].active && Main.npc[number2].townNPC)
          {
            int sectionX1 = Netplay.GetSectionX((int) ((double) Main.npc[number2].position.X / 16.0));
            int sectionY1 = Netplay.GetSectionY((int) ((double) Main.npc[number2].position.Y / 16.0));
            if (sectionX1 == sectionX && sectionY1 == sectionY)
              NetMessage.SendData(23, whoAmi, -1, "", number2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
      }
      catch
      {
      }
    }

    public static void greetPlayer(int plr)
    {
      if (Main.motd == "")
        NetMessage.SendData(25, plr, -1, Lang.mp[18] + " " + Main.worldName + "!", (int) byte.MaxValue, (float) byte.MaxValue, 240f, 20f, 0, 0, 0);
      else
        NetMessage.SendData(25, plr, -1, Main.motd, (int) byte.MaxValue, (float) byte.MaxValue, 240f, 20f, 0, 0, 0);
      string str = "";
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (Main.player[index].active)
          str = !(str == "") ? str + ", " + Main.player[index].name : str + Main.player[index].name;
      }
      NetMessage.SendData(25, plr, -1, Language.GetTextValue("Game.JoinGreeting", (object) str), (int) byte.MaxValue, (float) byte.MaxValue, 240f, 20f, 0, 0, 0);
    }

    public static void sendWater(int x, int y)
    {
      if (Main.netMode == 1)
      {
        NetMessage.SendData(48, -1, -1, "", x, (float) y, 0.0f, 0.0f, 0, 0, 0);
      }
      else
      {
        for (int remoteClient = 0; remoteClient < 256; ++remoteClient)
        {
          if ((NetMessage.buffer[remoteClient].broadcast || Netplay.Clients[remoteClient].State >= 3) && Netplay.Clients[remoteClient].IsConnected())
          {
            int index1 = x / 200;
            int index2 = y / 150;
            if (Netplay.Clients[remoteClient].TileSections[index1, index2])
              NetMessage.SendData(48, remoteClient, -1, "", x, (float) y, 0.0f, 0.0f, 0, 0, 0);
          }
        }
      }
    }

    public static void SyncDisconnectedPlayer(int plr)
    {
      NetMessage.SyncOnePlayer(plr, -1, plr);
      NetMessage.EnsureLocalPlayerIsPresent();
    }

    public static void SyncConnectedPlayer(int plr)
    {
      NetMessage.SyncOnePlayer(plr, -1, plr);
      for (int plr1 = 0; plr1 < (int) byte.MaxValue; ++plr1)
      {
        if (plr != plr1 && Main.player[plr1].active)
          NetMessage.SyncOnePlayer(plr1, plr, -1);
      }
      NetMessage.SendNPCHousesAndTravelShop(plr);
      NetMessage.SendAnglerQuest(plr);
      NetMessage.EnsureLocalPlayerIsPresent();
    }

    private static void SendNPCHousesAndTravelShop(int plr)
    {
      bool flag = false;
      for (int number = 0; number < 200; ++number)
      {
        if (Main.npc[number].active && Main.npc[number].townNPC && NPC.TypeToHeadIndex(Main.npc[number].type) != -1)
        {
          if (!flag && Main.npc[number].type == 368)
            flag = true;
          int num = 0;
          if (Main.npc[number].homeless)
            num = 1;
          NetMessage.SendData(60, plr, -1, "", number, (float) Main.npc[number].homeTileX, (float) Main.npc[number].homeTileY, (float) num, 0, 0, 0);
        }
      }
      if (!flag)
        return;
      NetMessage.SendTravelShop(plr);
    }

    private static void EnsureLocalPlayerIsPresent()
    {
      if (!Main.autoShutdown)
        return;
      bool flag = false;
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (Netplay.Clients[index].State == 10 && Netplay.Clients[index].Socket.GetRemoteAddress().IsLocalHost())
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      Console.WriteLine(Language.GetTextValue("Net.ServerAutoShutdown"));
      WorldFile.saveWorld();
      Netplay.disconnect = true;
    }

    private static void SyncOnePlayer(int plr, int toWho, int fromWho)
    {
      int num1 = 0;
      if (Main.player[plr].active)
        num1 = 1;
      if (Netplay.Clients[plr].State == 10)
      {
        NetMessage.SendData(14, toWho, fromWho, "", plr, (float) num1, 0.0f, 0.0f, 0, 0, 0);
        NetMessage.SendData(4, toWho, fromWho, Main.player[plr].name, plr, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        NetMessage.SendData(13, toWho, fromWho, "", plr, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        NetMessage.SendData(16, toWho, fromWho, "", plr, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        NetMessage.SendData(30, toWho, fromWho, "", plr, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        NetMessage.SendData(45, toWho, fromWho, "", plr, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        NetMessage.SendData(42, toWho, fromWho, "", plr, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        NetMessage.SendData(50, toWho, fromWho, "", plr, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        for (int index = 0; index < 59; ++index)
          NetMessage.SendData(5, toWho, fromWho, Main.player[plr].inventory[index].name, plr, (float) index, (float) Main.player[plr].inventory[index].prefix, 0.0f, 0, 0, 0);
        for (int index = 0; index < Main.player[plr].armor.Length; ++index)
          NetMessage.SendData(5, toWho, fromWho, Main.player[plr].armor[index].name, plr, (float) (59 + index), (float) Main.player[plr].armor[index].prefix, 0.0f, 0, 0, 0);
        for (int index = 0; index < Main.player[plr].dye.Length; ++index)
          NetMessage.SendData(5, toWho, fromWho, Main.player[plr].dye[index].name, plr, (float) (58 + Main.player[plr].armor.Length + 1 + index), (float) Main.player[plr].dye[index].prefix, 0.0f, 0, 0, 0);
        for (int index = 0; index < Main.player[plr].miscEquips.Length; ++index)
          NetMessage.SendData(5, toWho, fromWho, "", plr, (float) (58 + Main.player[plr].armor.Length + Main.player[plr].dye.Length + 1 + index), (float) Main.player[plr].miscEquips[index].prefix, 0.0f, 0, 0, 0);
        for (int index = 0; index < Main.player[plr].miscDyes.Length; ++index)
          NetMessage.SendData(5, toWho, fromWho, "", plr, (float) (58 + Main.player[plr].armor.Length + Main.player[plr].dye.Length + Main.player[plr].miscEquips.Length + 1 + index), (float) Main.player[plr].miscDyes[index].prefix, 0.0f, 0, 0, 0);
        if (Netplay.Clients[plr].IsAnnouncementCompleted)
          return;
        Netplay.Clients[plr].IsAnnouncementCompleted = true;
        NetMessage.SendData(25, -1, plr, Main.player[plr].name + " " + Lang.mp[19], (int) byte.MaxValue, (float) byte.MaxValue, 240f, 20f, 0, 0, 0);
        if (!Main.dedServ)
          return;
        Console.WriteLine(Main.player[plr].name + " " + Lang.mp[19]);
      }
      else
      {
        int num2 = 0;
        NetMessage.SendData(14, -1, plr, "", plr, (float) num2, 0.0f, 0.0f, 0, 0, 0);
        if (!Netplay.Clients[plr].IsAnnouncementCompleted)
          return;
        Netplay.Clients[plr].IsAnnouncementCompleted = false;
        NetMessage.SendData(25, -1, plr, Netplay.Clients[plr].Name + " " + Lang.mp[20], (int) byte.MaxValue, (float) byte.MaxValue, 240f, 20f, 0, 0, 0);
        if (Main.dedServ)
          Console.WriteLine(Netplay.Clients[plr].Name + " " + Lang.mp[20]);
        Netplay.Clients[plr].Name = "Anonymous";
      }
    }
  }
}
