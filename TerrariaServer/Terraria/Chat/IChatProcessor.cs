// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.IChatProcessor
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

namespace Terraria.Chat
{
  public interface IChatProcessor
  {
    bool ProcessReceivedMessage(ChatMessage message, int clientId);

    bool ProcessOutgoingMessage(ChatMessage message);
  }
}
