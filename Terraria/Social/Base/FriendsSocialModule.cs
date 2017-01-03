// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.FriendsSocialModule
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

namespace Terraria.Social.Base
{
  public abstract class FriendsSocialModule : ISocialModule
  {
    public abstract string GetUsername();

    public abstract void OpenJoinInterface();

    public abstract void Initialize();

    public abstract void Shutdown();
  }
}
