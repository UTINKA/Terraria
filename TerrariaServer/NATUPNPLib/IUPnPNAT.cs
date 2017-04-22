// Decompiled with JetBrains decompiler
// Type: NATUPNPLib.IUPnPNAT
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NATUPNPLib
{
  [Guid("B171C812-CC76-485A-94D8-B6B3A2794E99")]
  [TypeIdentifier]
  [CompilerGenerated]
  [ComImport]
  public interface IUPnPNAT
  {
    IStaticPortMappingCollection StaticPortMappingCollection { [DispId(1)] get; }
  }
}
