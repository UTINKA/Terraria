// Decompiled with JetBrains decompiler
// Type: Terraria.Net.Sockets.ISocket
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

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
