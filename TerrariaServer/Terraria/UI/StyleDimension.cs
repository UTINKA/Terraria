// Decompiled with JetBrains decompiler
// Type: Terraria.UI.StyleDimension
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

namespace Terraria.UI
{
  public struct StyleDimension
  {
    public static StyleDimension Fill = new StyleDimension(0.0f, 1f);
    public static StyleDimension Empty = new StyleDimension(0.0f, 0.0f);
    public float Pixels;
    public float Precent;

    public StyleDimension(float pixels, float precent)
    {
      this.Pixels = pixels;
      this.Precent = precent;
    }

    public void Set(float pixels, float precent)
    {
      this.Pixels = pixels;
      this.Precent = precent;
    }

    public float GetValue(float containerSize)
    {
      return this.Pixels + this.Precent * containerSize;
    }
  }
}
