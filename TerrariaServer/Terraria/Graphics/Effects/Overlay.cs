// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.Overlay
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
