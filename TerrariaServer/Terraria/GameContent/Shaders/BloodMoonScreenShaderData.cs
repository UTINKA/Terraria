// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.BloodMoonScreenShaderData
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

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
