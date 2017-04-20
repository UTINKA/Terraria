// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Tile_Entities.TEItemFrame
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Tile_Entities
{
  public class TEItemFrame : TileEntity
  {
    public Item item;

    public TEItemFrame()
    {
      this.item = new Item();
    }

    public static void Initialize()
    {
      TileEntity._NetPlaceEntity += new Action<int, int, int>(TEItemFrame.NetPlaceEntity);
    }

    public static void NetPlaceEntity(int x, int y, int type)
    {
      if (type != 1 || !TEItemFrame.ValidTile(x, y))
        return;
      NetMessage.SendData(86, -1, -1, (NetworkText) null, TEItemFrame.Place(x, y), (float) x, (float) y, 0.0f, 0, 0, 0);
    }

    public static int Place(int x, int y)
    {
      TEItemFrame teItemFrame = new TEItemFrame();
      teItemFrame.Position = new Point16(x, y);
      teItemFrame.ID = TileEntity.AssignNewID();
      teItemFrame.type = (byte) 1;
      TileEntity.ByID[teItemFrame.ID] = (TileEntity) teItemFrame;
      TileEntity.ByPosition[teItemFrame.Position] = (TileEntity) teItemFrame;
      return teItemFrame.ID;
    }

    public static int Hook_AfterPlacement(int x, int y, int type = 395, int style = 0, int direction = 1)
    {
      if (Main.netMode != 1)
        return TEItemFrame.Place(x, y);
      NetMessage.SendTileSquare(Main.myPlayer, x, y, 2, TileChangeType.None);
      NetMessage.SendData(87, -1, -1, (NetworkText) null, x, (float) y, 1f, 0.0f, 0, 0, 0);
      return -1;
    }

    public static void Kill(int x, int y)
    {
      TileEntity tileEntity;
      if (!TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) || (int) tileEntity.type != 1)
        return;
      TileEntity.ByID.Remove(tileEntity.ID);
      TileEntity.ByPosition.Remove(new Point16(x, y));
    }

    public static int Find(int x, int y)
    {
      TileEntity tileEntity;
      if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && (int) tileEntity.type == 1)
        return tileEntity.ID;
      return -1;
    }

    public static bool ValidTile(int x, int y)
    {
      return Main.tile[x, y].active() && (int) Main.tile[x, y].type == 395 && ((int) Main.tile[x, y].frameY == 0 && (int) Main.tile[x, y].frameX % 36 == 0);
    }

    public override void WriteExtraData(BinaryWriter writer, bool networkSend)
    {
      writer.Write((short) this.item.netID);
      writer.Write(this.item.prefix);
      writer.Write((short) this.item.stack);
    }

    public override void ReadExtraData(BinaryReader reader, bool networkSend)
    {
      this.item = new Item();
      this.item.netDefaults((int) reader.ReadInt16());
      this.item.Prefix((int) reader.ReadByte());
      this.item.stack = (int) reader.ReadInt16();
    }

    public override string ToString()
    {
      return ((int) this.Position.X).ToString() + "x  " + (object) this.Position.Y + "y item: " + this.item.ToString();
    }

    public void DropItem()
    {
      if (Main.netMode != 1)
        Item.NewItem((int) this.Position.X * 16, (int) this.Position.Y * 16, 32, 32, this.item.netID, 1, false, (int) this.item.prefix, false, false);
      this.item = new Item();
    }

    public static void TryPlacing(int x, int y, int netid, int prefix, int stack)
    {
      int index = TEItemFrame.Find(x, y);
      if (index == -1)
      {
        int number = Item.NewItem(x * 16, y * 16, 32, 32, 1, 1, false, 0, false, false);
        Main.item[number].netDefaults(netid);
        Main.item[number].Prefix(prefix);
        Main.item[number].stack = stack;
        NetMessage.SendData(21, -1, -1, (NetworkText) null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      else
      {
        TEItemFrame teItemFrame = (TEItemFrame) TileEntity.ByID[index];
        if (teItemFrame.item.stack > 0)
          teItemFrame.DropItem();
        teItemFrame.item = new Item();
        teItemFrame.item.netDefaults(netid);
        teItemFrame.item.Prefix(prefix);
        teItemFrame.item.stack = stack;
        NetMessage.SendData(86, -1, -1, (NetworkText) null, teItemFrame.ID, (float) x, (float) y, 0.0f, 0, 0, 0);
      }
    }
  }
}
