// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.BloodMoonScreenShaderData
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
      this.UseOpacity((1f - Utils.SmoothStep((float) Main.worldSurface + 50f, (float) Main.rockLayer + 100f, (float) (((double) Main.screenPosition.Y + (double) (Main.screenHeight / 2)) / 16.0))) * 0.75f);
      base.Apply();
    }
  }
}
