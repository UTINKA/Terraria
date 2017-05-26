// Decompiled with JetBrains decompiler
// Type: NATUPNPLib.IStaticPortMapping
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NATUPNPLib
{
  [CompilerGenerated]
  [Guid("6F10711F-729B-41E5-93B8-F21D0F818DF1")]
  [TypeIdentifier]
  [ComImport]
  public interface IStaticPortMapping
  {
    [DispId(3)]
    int InternalPort { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

    [DispId(4)]
    string Protocol { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

    [DispId(5)]
    string InternalClient { [DispId(5), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_2();
  }
}
