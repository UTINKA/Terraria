// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.CustomSky
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
  public abstract class CustomSky : GameEffect
  {
    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth);

    public abstract bool IsActive();

    public abstract void Reset();

    public virtual Color OnTileColor(Color inColor)
    {
      return inColor;
    }

    public virtual float GetCloudAlpha()
    {
      return 1f;
    }

    public override bool IsVisible()
    {
      return true;
    }
  }
}
