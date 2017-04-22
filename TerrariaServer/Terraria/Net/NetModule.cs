// Decompiled with JetBrains decompiler
// Type: Terraria.Net.NetModule
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.IO;

namespace Terraria.Net
{
  public abstract class NetModule
  {
    public abstract bool Deserialize(BinaryReader reader, int userId);

    protected static NetPacket CreatePacket<T>(int maxSize) where T : NetModule
    {
      return new NetPacket(NetManager.Instance.GetId<T>(), maxSize);
    }
  }
}
