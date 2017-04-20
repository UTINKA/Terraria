// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.Overlay
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
