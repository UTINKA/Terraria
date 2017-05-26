// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.ChatCommandAttribute
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.Chat.Commands
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
  public sealed class ChatCommandAttribute : Attribute
  {
    public readonly string Name;

    public ChatCommandAttribute(string name)
    {
      this.Name = name;
    }
  }
}
