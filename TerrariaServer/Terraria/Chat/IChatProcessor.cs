// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.IChatProcessor
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

namespace Terraria.Chat
{
  public interface IChatProcessor
  {
    bool ProcessReceivedMessage(ChatMessage message, int clientId);

    bool ProcessOutgoingMessage(ChatMessage message);
  }
}
