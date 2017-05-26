// Decompiled with JetBrains decompiler
// Type: Terraria.Net.SteamAddress
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Steamworks;

namespace Terraria.Net
{
  public class SteamAddress : RemoteAddress
  {
    public readonly CSteamID SteamId;
    private string _friendlyName;

    public SteamAddress(CSteamID steamId)
    {
      this.Type = AddressType.Steam;
      this.SteamId = steamId;
    }

    public override string ToString()
    {
      return "STEAM_0:" + ((ulong) this.SteamId.m_SteamID % 2UL).ToString() + ":" + ((ulong) (this.SteamId.m_SteamID - (76561197960265728L + this.SteamId.m_SteamID % 2L)) / 2UL).ToString();
    }

    public override string GetIdentifier()
    {
      return this.ToString();
    }

    public override bool IsLocalHost()
    {
      if (Program.LaunchParameters.ContainsKey("-localsteamid"))
      {
        // ISSUE: explicit non-virtual call
        return Program.LaunchParameters["-localsteamid"].Equals(__nonvirtual (this.SteamId.m_SteamID.ToString()));
      }
      return false;
    }

    public override string GetFriendlyName()
    {
      if (this._friendlyName == null)
        this._friendlyName = SteamFriends.GetFriendPersonaName(this.SteamId);
      return this._friendlyName;
    }
  }
}
