// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.CloudSocialModule
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Steamworks;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

    public override List<string> GetFiles(string matchPattern)
    {
      lock (this.ioLock)
      {
        matchPattern = "^" + matchPattern + "$";
        List<string> local_0 = new List<string>();
        int local_1 = SteamRemoteStorage.GetFileCount();
        Regex local_2 = new Regex(matchPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        for (int local_4 = 0; local_4 < local_1; ++local_4)
        {
          int local_3;
          string local_5 = SteamRemoteStorage.GetFileNameAndSize(local_4, ref local_3);
          if (local_2.Match(local_5).Length > 0)
            local_0.Add(local_5);
        }
        return local_0;
      }
    }

    public override bool Write(string path, byte[] data, int length)
    {
      lock (this.ioLock)
      {
        UGCFileWriteStreamHandle_t local_0 = SteamRemoteStorage.FileWriteStreamOpen(path);
        uint local_1 = 0;
        while ((long) local_1 < (long) length)
        {
          int local_2 = (int) Math.Min(1024L, (long) length - (long) local_1);
          Array.Copy((Array) data, (long) local_1, (Array) this.writeBuffer, 0L, (long) local_2);
          SteamRemoteStorage.FileWriteStreamWriteChunk(local_0, this.writeBuffer, local_2);
          local_1 += 1024U;
        }
        return SteamRemoteStorage.FileWriteStreamClose(local_0);
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
