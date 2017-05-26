// Decompiled with JetBrains decompiler
// Type: Terraria.Net.NetModule
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

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
