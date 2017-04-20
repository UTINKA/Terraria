// Decompiled with JetBrains decompiler
// Type: Terraria.Net.ServerMode
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
