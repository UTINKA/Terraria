// Decompiled with JetBrains decompiler
// Type: Terraria.Net.RemoteAddress
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Net
{
  public abstract class RemoteAddress
  {
    public AddressType Type;

    public abstract string GetIdentifier();

    public abstract string GetFriendlyName();

    public abstract bool IsLocalHost();
  }
}
