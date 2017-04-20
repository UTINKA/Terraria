// Decompiled with JetBrains decompiler
// Type: Terraria.ID.Colors
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;

namespace Terraria.ID
{
  public static class Colors
  {
    public static readonly Color RarityAmber = new Color((int) byte.MaxValue, 175, 0);
    public static readonly Color RarityTrash = new Color(130, 130, 130);
    public static readonly Color RarityNormal = Color.get_White();
    public static readonly Color RarityBlue = new Color(150, 150, (int) byte.MaxValue);
    public static readonly Color RarityGreen = new Color(150, (int) byte.MaxValue, 150);
    public static readonly Color RarityOrange = new Color((int) byte.MaxValue, 200, 150);
    public static readonly Color RarityRed = new Color((int) byte.MaxValue, 150, 150);
    public static readonly Color RarityPink = new Color((int) byte.MaxValue, 150, (int) byte.MaxValue);
    public static readonly Color RarityPurple = new Color(210, 160, (int) byte.MaxValue);
    public static readonly Color RarityLime = new Color(150, (int) byte.MaxValue, 10);
    public static readonly Color RarityYellow = new Color((int) byte.MaxValue, (int) byte.MaxValue, 10);
    public static readonly Color RarityCyan = new Color(5, 200, (int) byte.MaxValue);
    public static readonly Color CoinPlatinum = new Color(220, 220, 198);
    public static readonly Color CoinGold = new Color(224, 201, 92);
    public static readonly Color CoinSilver = new Color(181, 192, 193);
    public static readonly Color CoinCopper = new Color(246, 138, 96);
    public static readonly Color[] _waterfallColors = new Color[22]
    {
      new Color(9, 61, 191),
      new Color(253, 32, 3),
      new Color(143, 143, 143),
      new Color(59, 29, 131),
      new Color(7, 145, 142),
      new Color(171, 11, 209),
      new Color(9, 137, 191),
      new Color(168, 106, 32),
      new Color(36, 60, 148),
      new Color(65, 59, 101),
      new Color(200, 0, 0),
      (Color) null,
      (Color) null,
      new Color(177, 54, 79),
      new Color((int) byte.MaxValue, 156, 12),
      new Color(91, 34, 104),
      new Color(102, 104, 34),
      new Color(34, 43, 104),
      new Color(34, 104, 38),
      new Color(104, 34, 34),
      new Color(76, 79, 102),
      new Color(104, 61, 34)
    };
    public static readonly Color[] _liquidColors = new Color[12]
    {
      new Color(9, 61, 191),
      new Color(253, 32, 3),
      new Color(59, 29, 131),
      new Color(7, 145, 142),
      new Color(171, 11, 209),
      new Color(9, 137, 191),
      new Color(168, 106, 32),
      new Color(36, 60, 148),
      new Color(65, 59, 101),
      new Color(200, 0, 0),
      new Color(177, 54, 79),
      new Color((int) byte.MaxValue, 156, 12)
    };

    public static Color CurrentLiquidColor
    {
      get
      {
        Color color = Color.get_Transparent();
        bool flag = true;
        for (int index = 0; index < 11; ++index)
        {
          if ((double) Main.liquidAlpha[index] > 0.0)
          {
            if (flag)
            {
              flag = false;
              color = Colors._liquidColors[index];
            }
            else
              color = Color.Lerp(color, Colors._liquidColors[index], Main.liquidAlpha[index]);
          }
        }
        return color;
      }
    }

    public static Color AlphaDarken(Color input)
    {
      return Color.op_Multiply(input, (float) Main.mouseTextColor / (float) byte.MaxValue);
    }
  }
}
