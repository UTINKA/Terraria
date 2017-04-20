// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.CloudSocialModule
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Steamworks;
using System;
using System.Collections.Generic;

namespace Terraria.Social.Steam
{
  public class CloudSocialModule : Terraria.Social.Base.CloudSocialModule
  {
    private object ioLock = new object();
    private byte[] writeBuffer = new byte[new IntPtr(1024)];
    private const uint WRITE_CHUNK_SIZE = 1024;

    public override void Initialize()
    {
      base.Initialize();
    }

    public override void Shutdown()
    {
    }

    public override IEnumerable<string> GetFiles()
    {
      lock (this.ioLock)
      {
        int fileCount = SteamRemoteStorage.GetFileCount();
        List<string> stringList = new List<string>(fileCount);
        for (int index = 0; index < fileCount; ++index)
        {
          int num;
          stringList.Add(SteamRemoteStorage.GetFileNameAndSize(index, ref num));
        }
        return (IEnumerable<string>) stringList;
      }
    }

    public override bool Write(string path, byte[] data, int length)
    {
      lock (this.ioLock)
      {
        UGCFileWriteStreamHandle_t writeStreamHandleT = SteamRemoteStorage.FileWriteStreamOpen(path);
        uint num1 = 0;
        while ((long) num1 < (long) length)
        {
          int num2 = (int) Math.Min(1024L, (long) length - (long) num1);
          Array.Copy((Array) data, (long) num1, (Array) this.writeBuffer, 0L, (long) num2);
          SteamRemoteStorage.FileWriteStreamWriteChunk(writeStreamHandleT, this.writeBuffer, num2);
          num1 += 1024U;
        }
        return SteamRemoteStorage.FileWriteStreamClose(writeStreamHandleT);
      }
    }

    public override int GetFileSize(string path)
    {
      lock (this.ioLock)
        return SteamRemoteStorage.GetFileSize(path);
    }

    public override void Read(string path, byte[] buffer, int size)
    {
      lock (this.ioLock)
        SteamRemoteStorage.FileRead(path, buffer, size);
    }

    public override bool HasFile(string path)
    {
      lock (this.ioLock)
        return SteamRemoteStorage.FileExists(path);
    }

    public override bool Delete(string path)
    {
      lock (this.ioLock)
        return SteamRemoteStorage.FileDelete(path);
    }
  }
}
