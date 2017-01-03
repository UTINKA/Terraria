// Decompiled with JetBrains decompiler
// Type: Terraria.Net.NetPacket
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using System.IO;
using Terraria.DataStructures;

namespace Terraria.Net
{
  public struct NetPacket
  {
    public ushort Id;
    public int Length;
    public CachedBuffer Buffer;

    public BinaryWriter Writer
    {
      get
      {
        return this.Buffer.Writer;
      }
    }

    public BinaryReader Reader
    {
      get
      {
        return this.Buffer.Reader;
      }
    }

    public NetPacket(ushort id, int size)
    {
      this.Id = id;
      this.Buffer = BufferPool.Request(size);
      this.Length = size;
    }

    public void Recycle()
    {
      this.Buffer.Recycle();
    }
  }
}
