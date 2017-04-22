// Decompiled with JetBrains decompiler
// Type: Terraria.Net.NetPacket
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.IO;
using Terraria.DataStructures;

namespace Terraria.Net
{
  public struct NetPacket
  {
    private const int HEADER_SIZE = 5;
    public readonly ushort Id;
    public readonly CachedBuffer Buffer;

    public int Length { get; private set; }

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
      this = new NetPacket();
      this.Id = id;
      this.Buffer = BufferPool.Request(size + 5);
      this.Length = size + 5;
      this.Writer.Write((ushort) (size + 5));
      this.Writer.Write((byte) 82);
      this.Writer.Write(id);
    }

    public void Recycle()
    {
      this.Buffer.Recycle();
    }

    public void ShrinkToFit()
    {
      if (this.Length == (int) this.Writer.BaseStream.Position)
        return;
      this.Length = (int) this.Writer.BaseStream.Position;
      this.Writer.Seek(0, SeekOrigin.Begin);
      this.Writer.Write((ushort) this.Length);
      this.Writer.Seek(this.Length, SeekOrigin.Begin);
    }
  }
}
