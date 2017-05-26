// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.NetSocialModule
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.Diagnostics;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.Base
{
  public abstract class NetSocialModule : ISocialModule
  {
    public abstract void Initialize();

    public abstract void Shutdown();

    public abstract void Close(RemoteAddress address);

    public abstract bool IsConnected(RemoteAddress address);

    public abstract void Connect(RemoteAddress address);

    public abstract bool Send(RemoteAddress address, byte[] data, int length);

    public abstract int Receive(RemoteAddress address, byte[] data, int offset, int length);

    public abstract bool IsDataAvailable(RemoteAddress address);

    public abstract void LaunchLocalServer(Process process, ServerMode mode);

    public abstract bool CanInvite();

    public abstract void OpenInviteInterface();

    public abstract void CancelJoin();

    public abstract bool StartListening(SocketConnectionAccepted callback);

    public abstract void StopListening();

    public abstract ulong GetLobbyId();
  }
}
