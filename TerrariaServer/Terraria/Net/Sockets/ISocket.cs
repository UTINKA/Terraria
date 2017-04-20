// Decompiled with JetBrains decompiler
// Type: Terraria.Net.Sockets.ISocket
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

namespace Terraria.Net.Sockets
{
  public interface ISocket
  {
    void Close();

    bool IsConnected();

    void Connect(RemoteAddress address);

    void AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state = null);

    void AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state = null);

    bool IsDataAvailable();

    void SendQueuedPackets();

    bool StartListening(SocketConnectionAccepted callback);

    void StopListening();

    RemoteAddress GetRemoteAddress();
  }
}
