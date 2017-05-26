// Decompiled with JetBrains decompiler
// Type: Terraria.Net.TcpAddress
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.Net;

namespace Terraria.Net
{
  public class TcpAddress : RemoteAddress
  {
    public IPAddress Address;
    public int Port;

    public TcpAddress(IPAddress address, int port)
    {
      this.Type = AddressType.Tcp;
      this.Address = address;
      this.Port = port;
    }

    public override string GetIdentifier()
    {
      return this.Address.ToString();
    }

    public override bool IsLocalHost()
    {
      return this.Address.Equals((object) IPAddress.Loopback);
    }

    public override string ToString()
    {
      return new IPEndPoint(this.Address, this.Port).ToString();
    }

    public override string GetFriendlyName()
    {
      return this.ToString();
    }
  }
}
