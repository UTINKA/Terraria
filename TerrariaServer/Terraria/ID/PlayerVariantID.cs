// Decompiled with JetBrains decompiler
// Type: Terraria.ID.PlayerVariantID
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

namespace Terraria.ID
{
  public static class PlayerVariantID
  {
    public static SetFactory Factory = new SetFactory(10);
    public const int MaleStarter = 0;
    public const int MaleSticker = 1;
    public const int MaleGangster = 2;
    public const int MaleCoat = 3;
    public const int FemaleStarter = 4;
    public const int FemaleSticker = 5;
    public const int FemaleGangster = 6;
    public const int FemaleCoat = 7;
    public const int MaleDress = 8;
    public const int FemaleDress = 9;
    public const int Count = 10;

    public class Sets
    {
      public static bool[] Male = PlayerVariantID.Factory.CreateBoolSet(0, 1, 2, 3, 8);
      public static int[] AltGenderReference = PlayerVariantID.Factory.CreateIntSet(0, 0, 4, 4, 0, 1, 5, 5, 1, 2, 6, 6, 2, 3, 7, 7, 3, 8, 9, 9, 8);
      public static int[] VariantOrderMale = new int[5]
      {
        0,
        1,
        2,
        3,
        8
      };
      public static int[] VariantOrderFemale = new int[5]
      {
        4,
        5,
        6,
        7,
        9
      };
    }
  }
}
