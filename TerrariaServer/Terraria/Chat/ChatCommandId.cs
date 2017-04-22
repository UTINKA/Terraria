// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.ChatCommandId
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using ReLogic.Utilities;
using System.IO;
using System.Text;
using Terraria.Chat.Commands;

namespace Terraria.Chat
{
  public struct ChatCommandId
  {
    private readonly string _name;

    private ChatCommandId(string name)
    {
      this._name = name;
    }

    public static ChatCommandId FromType<T>() where T : IChatCommand
    {
      ChatCommandAttribute cacheableAttribute = (ChatCommandAttribute) AttributeUtilities.GetCacheableAttribute<T, ChatCommandAttribute>();
      if (cacheableAttribute != null)
        return new ChatCommandId(cacheableAttribute.Name);
      return new ChatCommandId((string) null);
    }

    public void Serialize(BinaryWriter writer)
    {
      writer.Write(this._name ?? "");
    }

    public static ChatCommandId Deserialize(BinaryReader reader)
    {
      return new ChatCommandId(reader.ReadString());
    }

    public int GetMaxSerializedSize()
    {
      return 4 + Encoding.UTF8.GetByteCount(this._name ?? "");
    }
  }
}
