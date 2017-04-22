// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.Overlay
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
  public abstract class Overlay : GameEffect
  {
    public OverlayMode Mode = OverlayMode.Inactive;
    private RenderLayers _layer = RenderLayers.All;

    public RenderLayers Layer
    {
      get
      {
        return this._layer;
      }
    }

    public Overlay(EffectPriority priority, RenderLayers layer)
    {
      this._priority = priority;
      this._layer = layer;
    }

    public abstract void Draw(SpriteBatch spriteBatch);

    public abstract void Update(GameTime gameTime);
  }
}
