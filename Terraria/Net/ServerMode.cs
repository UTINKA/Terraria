// Decompiled with JetBrains decompiler
// Type: Terraria.Net.ServerMode
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.Net
{
  [Flags]
  public enum ServerMode : byte
  {
    None = 0,
    Lobby = 1,
    FriendsCanJoin = 2,
    FriendsOfFriends = 4,
  }
}
