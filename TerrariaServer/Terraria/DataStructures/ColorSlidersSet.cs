// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.ColorSlidersSet
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
  public class ColorSlidersSet
  {
    public float Alpha = 1f;
    public float Hue;
    public float Saturation;
    public float Luminance;

    public void SetHSL(Color color)
    {
      Vector3 hsl = Main.rgbToHsl(color);
      this.Hue = (float) hsl.X;
      this.Saturation = (float) hsl.Y;
      this.Luminance = (float) hsl.Z;
    }

    public void SetHSL(Vector3 vector)
    {
      this.Hue = (float) vector.X;
      this.Saturation = (float) vector.Y;
      this.Luminance = (float) vector.Z;
    }

    public Color GetColor()
    {
      Color rgb = Main.hslToRgb(this.Hue, this.Saturation, this.Luminance);
      // ISSUE: explicit reference operation
      ((Color) @rgb).set_A((byte) ((double) this.Alpha * (double) byte.MaxValue));
      return rgb;
    }

    public Vector3 GetHSLVector()
    {
      return new Vector3(this.Hue, this.Saturation, this.Luminance);
    }

    public void ApplyToMainLegacyBars()
    {
      Main.hBar = this.Hue;
      Main.sBar = this.Saturation;
      Main.lBar = this.Luminance;
      Main.aBar = this.Alpha;
    }
  }
}
