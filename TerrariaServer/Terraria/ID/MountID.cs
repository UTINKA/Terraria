// Decompiled with JetBrains decompiler
// Type: Terraria.ID.MountID
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

namespace Terraria.ID
{
  public static class MountID
  {
    public static int Count = 15;

    public static class Sets
    {
      public static SetFactory Factory = new SetFactory(MountID.Count);
      public static bool[] Cart = MountID.Sets.Factory.CreateBoolSet(6, 11, 13);
    }
  }
}
