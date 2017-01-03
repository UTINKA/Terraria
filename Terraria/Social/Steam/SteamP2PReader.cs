// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.SteamP2PReader
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Steamworks;
using System;
using System.Collections.Generic;

namespace Terraria.Social.Steam
{
  public class SteamP2PReader
  {
    public object SteamLock = new object();
    private Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> _pendingReadBuffers = new Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>>();
    private Queue<CSteamID> _deletionQueue = new Queue<CSteamID>();
    private Queue<byte[]> _bufferPool = new Queue<byte[]>();
    private const int BUFFER_SIZE = 4096;
    private int _channel;
    private SteamP2PReader.OnReadEvent _readEvent;

    public SteamP2PReader(int channel)
    {
      this._channel = channel;
    }

    public void ClearUser(CSteamID id)
    {
      lock (this._pendingReadBuffers)
        this._deletionQueue.Enqueue(id);
    }

    public bool IsDataAvailable(CSteamID id)
    {
      lock (this._pendingReadBuffers)
      {
        if (!this._pendingReadBuffers.ContainsKey(id))
          return false;
        Queue<SteamP2PReader.ReadResult> local_0 = this._pendingReadBuffers[id];
        return local_0.Count != 0 && (int) local_0.Peek().Size != 0;
      }
    }

    public void SetReadEvent(SteamP2PReader.OnReadEvent method)
    {
      this._readEvent = method;
    }

    private bool IsPacketAvailable(out uint size)
    {
      lock (this.SteamLock)
        return SteamNetworking.IsP2PPacketAvailable(ref size, this._channel);
    }

    public void ReadTick()
    {
      lock (this._pendingReadBuffers)
      {
        while (this._deletionQueue.Count > 0)
          this._pendingReadBuffers.Remove(this._deletionQueue.Dequeue());
        uint local_0;
        while (this.IsPacketAvailable(out local_0))
        {
          byte[] local_1 = this._bufferPool.Count != 0 ? this._bufferPool.Dequeue() : new byte[(IntPtr) Math.Max(local_0, 4096U)];
          uint local_3;
          CSteamID local_2;
          bool local_4;
          lock (this.SteamLock)
            local_4 = SteamNetworking.ReadP2PPacket(local_1, (uint) local_1.Length, ref local_3, ref local_2, this._channel);
          if (local_4)
          {
            if (this._readEvent == null || this._readEvent(local_1, (int) local_3, local_2))
            {
              if (!this._pendingReadBuffers.ContainsKey(local_2))
                this._pendingReadBuffers[local_2] = new Queue<SteamP2PReader.ReadResult>();
              this._pendingReadBuffers[local_2].Enqueue(new SteamP2PReader.ReadResult(local_1, local_3));
            }
            else
              this._bufferPool.Enqueue(local_1);
          }
        }
      }
    }

    public int Receive(CSteamID user, byte[] buffer, int bufferOffset, int bufferSize)
    {
      uint num = 0;
      lock (this._pendingReadBuffers)
      {
        if (!this._pendingReadBuffers.ContainsKey(user))
          return 0;
        Queue<SteamP2PReader.ReadResult> local_1 = this._pendingReadBuffers[user];
        while (local_1.Count > 0)
        {
          SteamP2PReader.ReadResult local_2 = local_1.Peek();
          uint local_3 = Math.Min((uint) bufferSize - num, local_2.Size - local_2.Offset);
          if ((int) local_3 == 0)
            return (int) num;
          Array.Copy((Array) local_2.Data, (long) local_2.Offset, (Array) buffer, (long) bufferOffset + (long) num, (long) local_3);
          if ((int) local_3 == (int) local_2.Size - (int) local_2.Offset)
            this._bufferPool.Enqueue(local_1.Dequeue().Data);
          else
            local_2.Offset += local_3;
          num += local_3;
        }
      }
      return (int) num;
    }

    public class ReadResult
    {
      public byte[] Data;
      public uint Size;
      public uint Offset;

      public ReadResult(byte[] data, uint size)
      {
        this.Data = data;
        this.Size = size;
        this.Offset = 0U;
      }
    }

    public delegate bool OnReadEvent(byte[] data, int size, CSteamID user);
  }
}
