// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.FriendsSocialModule
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Steamworks;

namespace Terraria.Social.Steam
{
  public class FriendsSocialModule : Terraria.Social.Base.FriendsSocialModule
  {
    public override void Initialize()
    {
    }

    public override void Shutdown()
    {
    }

    public override string GetUsername()
    {
      return SteamFriends.GetPersonaName();
    }

    public override void OpenJoinInterface()
    {
      SteamFriends.ActivateGameOverlay("Friends");
    }
  }
}
