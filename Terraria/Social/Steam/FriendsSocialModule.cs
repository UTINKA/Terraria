// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.FriendsSocialModule
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

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
