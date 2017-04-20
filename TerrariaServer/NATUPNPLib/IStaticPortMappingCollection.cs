// Decompiled with JetBrains decompiler
// Type: NATUPNPLib.IStaticPortMappingCollection
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.CustomMarshalers;

namespace NATUPNPLib
{
  [TypeIdentifier]
  [CompilerGenerated]
  [Guid("CD1F3E77-66D6-4664-82C7-36DBB641D0F1")]
  [ComImport]
  public interface IStaticPortMappingCollection : IEnumerable
  {
    [DispId(-4)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (EnumeratorToEnumVariantMarshaler))]
    new IEnumerator GetEnumerator();

    [SpecialName]
    extern void _VtblGap1_2();

    [DispId(2)]
    void Remove([In] int lExternalPort, [MarshalAs(UnmanagedType.BStr), In] string bstrProtocol);

    [DispId(3)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IStaticPortMapping Add([In] int lExternalPort, [MarshalAs(UnmanagedType.BStr), In] string bstrProtocol, [In] int lInternalPort, [MarshalAs(UnmanagedType.BStr), In] string bstrInternalClient, [In] bool bEnabled, [MarshalAs(UnmanagedType.BStr), In] string bstrDescription);
  }
}
