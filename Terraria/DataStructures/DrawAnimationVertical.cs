// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.DrawAnimationVertical
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

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
