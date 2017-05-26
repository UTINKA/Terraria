// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.DrawAnimation
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
  public class DrawAnimation
  {
    public int Frame;
    public int FrameCount;
    public int TicksPerFrame;
    public int FrameCounter;

    public virtual void Update()
    {
    }

    public virtual Rectangle GetFrame(Texture2D texture)
    {
      return texture.Frame(1, 1, 0, 0);
    }
  }
}
