// Decompiled with JetBrains decompiler
// Type: Terraria.Net.NetManager
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Terraria.Localization;
using Terraria.Net.Sockets;

namespace Terraria.Net
{
  public class NetManager
  {
    public static NetManager Instance = new NetManager();
    private static long _trafficTotal = 0;
    private static Stopwatch _trafficTimer = NetManager.CreateStopwatch();
    private Dictionary<ushort, NetModule> _modules = new Dictionary<ushort, NetModule>();
    private ushort ModuleCount;

    private static Stopwatch CreateStopwatch()
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      return stopwatch;
    }

    public void Register<T>() where T : NetModule, new()
    {
      T instance = Activator.CreateInstance<T>();
      instance.Id = this.ModuleCount;
      NetManager.PacketTypeStorage<T>.Module = instance;
      this._modules[this.ModuleCount] = (NetModule) instance;
      ++this.ModuleCount;
    }

    public NetModule GetModule<T>() where T : NetModule
    {
      return (NetModule) NetManager.PacketTypeStorage<T>.Module;
    }

    public ushort GetId<T>() where T : NetModule
    {
      return NetManager.PacketTypeStorage<T>.Module.Id;
    }

    public void Read(BinaryReader reader, int userId)
    {
      ushort key = reader.ReadUInt16();
      if (!this._modules.ContainsKey(key))
        return;
      this._modules[key].Deserialize(reader, userId);
    }

    public void Broadcast(NetPacket packet, int ignoreClient = -1)
    {
      for (int index = 0; index < 256; ++index)
      {
        if (index != ignoreClient && Netplay.Clients[index].IsConnected())
          NetManager.SendData(Netplay.Clients[index].Socket, packet);
      }
    }

    public void SendToServer(NetPacket packet)
    {
      NetManager.SendData(Netplay.Connection.Socket, packet);
    }

    public static void SendData(ISocket socket, NetPacket packet)
    {
      try
      {
        socket.AsyncSend(packet.Buffer.Data, 0, packet.Length, new SocketSendCallback(NetManager.SendCallback), (object) packet);
      }
      catch
      {
        Console.WriteLine(Language.GetTextValue("Error.ExceptionNormal", (object) Language.GetTextValue("Error.DataSentAfterConnectionLost")));
      }
    }

    public static void SendCallback(object state)
    {
      ((NetPacket) state).Recycle();
    }

    private static void UpdateStats(int length)
    {
      NetManager._trafficTotal += (long) length;
      double totalSeconds = NetManager._trafficTimer.Elapsed.TotalSeconds;
      if (totalSeconds <= 5.0)
        return;
      Console.WriteLine("NetManager :: Sending at " + (object) (Math.Floor((double) NetManager._trafficTotal / totalSeconds) / 1000.0) + " kbps.");
      NetManager._trafficTimer.Restart();
      NetManager._trafficTotal = 0L;
    }

    private class PacketTypeStorage<T> where T : NetModule
    {
      public static T Module;
    }
  }
}
