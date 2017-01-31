// Decompiled with JetBrains decompiler
// Type: Terraria.Net.NetModule
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using System.IO;

namespace Terraria.Net
{
  public abstract class NetModule
  {
    protected const int HEADER_SIZE = 5;
    public ushort Id;

    public abstract bool Deserialize(BinaryReader reader, int userId);

    protected static NetPacket CreatePacket<T>(int size) where T : NetModule
    {
      ushort id = NetManager.Instance.GetId<T>();
      NetPacket netPacket = new NetPacket(id, size + 5);
      netPacket.Writer.Write((ushort) (size + 5));
      netPacket.Writer.Write((byte) 82);
      netPacket.Writer.Write(id);
      return netPacket;
    }
  }
}
