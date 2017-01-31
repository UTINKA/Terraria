// Decompiled with JetBrains decompiler
// Type: NATUPNPLib.IStaticPortMapping
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NATUPNPLib
{
  [Guid("6F10711F-729B-41E5-93B8-F21D0F818DF1")]
  [CompilerGenerated]
  [TypeIdentifier]
  [ComImport]
  public interface IStaticPortMapping
  {
    int InternalPort { [DispId(3)] get; }

    string Protocol { [DispId(4)] get; }

    string InternalClient { [DispId(5)] get; }

    [SpecialName]
    void _VtblGap1_2();
  }
}
