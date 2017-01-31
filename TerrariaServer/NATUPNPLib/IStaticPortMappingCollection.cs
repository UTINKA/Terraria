// Decompiled with JetBrains decompiler
// Type: NATUPNPLib.IStaticPortMappingCollection
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.CustomMarshalers;

namespace NATUPNPLib
{
  [CompilerGenerated]
  [TypeIdentifier]
  [Guid("CD1F3E77-66D6-4664-82C7-36DBB641D0F1")]
  [ComImport]
  public interface IStaticPortMappingCollection : IEnumerable
  {
    [DispId(-4)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (EnumeratorToEnumVariantMarshaler))]
    IEnumerator GetEnumerator();

    [SpecialName]
    void _VtblGap1_2();

    [DispId(2)]
    void Remove([In] int lExternalPort, [MarshalAs(UnmanagedType.BStr), In] string bstrProtocol);

    [DispId(3)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IStaticPortMapping Add([In] int lExternalPort, [MarshalAs(UnmanagedType.BStr), In] string bstrProtocol, [In] int lInternalPort, [MarshalAs(UnmanagedType.BStr), In] string bstrInternalClient, [In] bool bEnabled, [MarshalAs(UnmanagedType.BStr), In] string bstrDescription);
  }
}
