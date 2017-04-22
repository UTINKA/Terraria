// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PlayerDeathReason
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.IO;
using Terraria.Localization;

namespace Terraria.DataStructures
{
  public class PlayerDeathReason
  {
    private int SourcePlayerIndex = -1;
    private int SourceNPCIndex = -1;
    private int SourceProjectileIndex = -1;
    private int SourceOtherIndex = -1;
    private int SourceProjectileType;
    private int SourceItemType;
    private int SourceItemPrefix;
    private string SourceCustomReason;

    public static PlayerDeathReason LegacyEmpty()
    {
      return new PlayerDeathReason()
      {
        SourceOtherIndex = 254
      };
    }

    public static PlayerDeathReason LegacyDefault()
    {
      return new PlayerDeathReason()
      {
        SourceOtherIndex = (int) byte.MaxValue
      };
    }

    public static PlayerDeathReason ByNPC(int index)
    {
      return new PlayerDeathReason()
      {
        SourceNPCIndex = index
      };
    }

    public static PlayerDeathReason ByCustomReason(string reasonInEnglish)
    {
      return new PlayerDeathReason()
      {
        SourceCustomReason = reasonInEnglish
      };
    }

    public static PlayerDeathReason ByPlayer(int index)
    {
      return new PlayerDeathReason()
      {
        SourcePlayerIndex = index,
        SourceItemType = Main.player[index].inventory[Main.player[index].selectedItem].type,
        SourceItemPrefix = (int) Main.player[index].inventory[Main.player[index].selectedItem].prefix
      };
    }

    public static PlayerDeathReason ByOther(int type)
    {
      return new PlayerDeathReason()
      {
        SourceOtherIndex = type
      };
    }

    public static PlayerDeathReason ByProjectile(int playerIndex, int projectileIndex)
    {
      PlayerDeathReason playerDeathReason = new PlayerDeathReason()
      {
        SourcePlayerIndex = playerIndex,
        SourceProjectileIndex = projectileIndex,
        SourceProjectileType = Main.projectile[projectileIndex].type
      };
      if (playerIndex >= 0 && playerIndex <= (int) byte.MaxValue)
      {
        playerDeathReason.SourceItemType = Main.player[playerIndex].inventory[Main.player[playerIndex].selectedItem].type;
        playerDeathReason.SourceItemPrefix = (int) Main.player[playerIndex].inventory[Main.player[playerIndex].selectedItem].prefix;
      }
      return playerDeathReason;
    }

    public NetworkText GetDeathText(string deadPlayerName)
    {
      if (this.SourceCustomReason != null)
        return NetworkText.FromLiteral(this.SourceCustomReason);
      return Lang.CreateDeathMessage(deadPlayerName, this.SourcePlayerIndex, this.SourceNPCIndex, this.SourceProjectileIndex, this.SourceOtherIndex, this.SourceProjectileType, this.SourceItemType);
    }

    public void WriteSelfTo(BinaryWriter writer)
    {
      BitsByte bitsByte = (BitsByte) (byte) 0;
      bitsByte[0] = this.SourcePlayerIndex != -1;
      bitsByte[1] = this.SourceNPCIndex != -1;
      bitsByte[2] = this.SourceProjectileIndex != -1;
      bitsByte[3] = this.SourceOtherIndex != -1;
      bitsByte[4] = this.SourceProjectileType != 0;
      bitsByte[5] = this.SourceItemType != 0;
      bitsByte[6] = this.SourceItemPrefix != 0;
      bitsByte[7] = this.SourceCustomReason != null;
      writer.Write((byte) bitsByte);
      if (bitsByte[0])
        writer.Write((short) this.SourcePlayerIndex);
      if (bitsByte[1])
        writer.Write((short) this.SourceNPCIndex);
      if (bitsByte[2])
        writer.Write((short) this.SourceProjectileIndex);
      if (bitsByte[3])
        writer.Write((byte) this.SourceOtherIndex);
      if (bitsByte[4])
        writer.Write((short) this.SourceProjectileType);
      if (bitsByte[5])
        writer.Write((short) this.SourceItemType);
      if (bitsByte[6])
        writer.Write((byte) this.SourceItemPrefix);
      if (!bitsByte[7])
        return;
      writer.Write(this.SourceCustomReason);
    }

    public static PlayerDeathReason FromReader(BinaryReader reader)
    {
      PlayerDeathReason playerDeathReason = new PlayerDeathReason();
      BitsByte bitsByte = (BitsByte) reader.ReadByte();
      if (bitsByte[0])
        playerDeathReason.SourcePlayerIndex = (int) reader.ReadInt16();
      if (bitsByte[1])
        playerDeathReason.SourceNPCIndex = (int) reader.ReadInt16();
      if (bitsByte[2])
        playerDeathReason.SourceProjectileIndex = (int) reader.ReadInt16();
      if (bitsByte[3])
        playerDeathReason.SourceOtherIndex = (int) reader.ReadByte();
      if (bitsByte[4])
        playerDeathReason.SourceProjectileType = (int) reader.ReadInt16();
      if (bitsByte[5])
        playerDeathReason.SourceItemType = (int) reader.ReadInt16();
      if (bitsByte[6])
        playerDeathReason.SourceItemPrefix = (int) reader.ReadByte();
      if (bitsByte[7])
        playerDeathReason.SourceCustomReason = reader.ReadString();
      return playerDeathReason;
    }
  }
}
