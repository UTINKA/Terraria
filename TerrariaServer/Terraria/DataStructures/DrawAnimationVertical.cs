// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.DrawAnimationVertical
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
  public class DrawAnimationVertical : DrawAnimation
  {
    public DrawAnimationVertical(int ticksperframe, int frameCount)
    {
      this.Frame = 0;
      this.FrameCounter = 0;
      this.FrameCount = frameCount;
      this.TicksPerFrame = ticksperframe;
    }

    public override void Update()
    {
      int num1 = this.FrameCounter + 1;
      this.FrameCounter = num1;
      if (num1 < this.TicksPerFrame)
        return;
      this.FrameCounter = 0;
      int num2 = this.Frame + 1;
      this.Frame = num2;
      if (num2 < this.FrameCount)
        return;
      this.Frame = 0;
    }

    public override Rectangle GetFrame(Texture2D texture)
    {
      return texture.Frame(1, this.FrameCount, 0, this.Frame);
    }
  }
}
