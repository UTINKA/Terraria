// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.CachedBuffer
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using System.IO;

namespace Terraria.DataStructures
{
  public class CachedBuffer
  {
    private bool _isActive = true;
    public readonly byte[] Data;
    public readonly BinaryWriter Writer;
    public readonly BinaryReader Reader;
    private readonly MemoryStream _memoryStream;

    public int Length
    {
      get
      {
        return this.Data.Length;
      }
    }

    public bool IsActive
    {
      get
      {
        return this._isActive;
      }
    }

    public CachedBuffer(byte[] data)
    {
      this.Data = data;
      this._memoryStream = new MemoryStream(data);
      this.Writer = new BinaryWriter((Stream) this._memoryStream);
      this.Reader = new BinaryReader((Stream) this._memoryStream);
    }

    internal CachedBuffer Activate()
    {
      this._isActive = true;
      this._memoryStream.Position = 0L;
      return this;
    }

    public void Recycle()
    {
      if (!this._isActive)
        return;
      this._isActive = false;
      BufferPool.Recycle(this);
    }
  }
}
