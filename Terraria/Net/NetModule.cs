// Decompiled with JetBrains decompiler
// Type: Terraria.Net.NetModule
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

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
