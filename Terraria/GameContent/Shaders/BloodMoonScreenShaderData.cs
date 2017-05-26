// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.BloodMoonScreenShaderData
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
  public class BloodMoonScreenShaderData : ScreenShaderData
  {
    public BloodMoonScreenShaderData(string passName)
      : base(passName)
    {
    }

    public override void Apply()
    {
      this.UseOpacity((1f - Utils.SmoothStep((float) Main.worldSurface + 50f, (float) Main.rockLayer + 100f, (float) ((Main.screenPosition.Y + (double) (Main.screenHeight / 2)) / 16.0))) * 0.75f);
      base.Apply();
    }
  }
}
