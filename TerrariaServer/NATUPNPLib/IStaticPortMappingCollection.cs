// Decompiled with JetBrains decompiler
// Type: NATUPNPLib.IStaticPortMappingCollection
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.CustomMarshalers;

namespace NATUPNPLib
{
  [CompilerGenerated]
  [Guid("CD1F3E77-66D6-4664-82C7-36DBB641D0F1")]
  [TypeIdentifier]
  [ComImport]
  public interface IStaticPortMappingCollection : IEnumerable
  {
    [DispId(-4)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (EnumeratorToEnumVariantMarshaler))]
    new IEnumerator GetEnumerator();

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_2();

    [DispId(2)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void Remove([In] int lExternalPort, [MarshalAs(UnmanagedType.BStr), In] string bstrProtocol);

    [DispId(3)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IStaticPortMapping Add([In] int lExternalPort, [MarshalAs(UnmanagedType.BStr), In] string bstrProtocol, [In] int lInternalPort, [MarshalAs(UnmanagedType.BStr), In] string bstrInternalClient, [In] bool bEnabled, [MarshalAs(UnmanagedType.BStr), In] string bstrDescription);
  }
}
