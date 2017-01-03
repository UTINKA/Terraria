// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.FriendsSocialModule
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

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
