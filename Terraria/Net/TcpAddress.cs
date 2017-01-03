// Decompiled with JetBrains decompiler
// Type: Terraria.Net.TcpAddress
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

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
