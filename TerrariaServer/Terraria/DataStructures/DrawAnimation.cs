// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.DrawAnimation
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

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
