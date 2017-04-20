// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.OverlayManager
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Terraria.Graphics.Effects
{
  public class OverlayManager : EffectManager<Overlay>
  {
    private LinkedList<Overlay>[] _activeOverlays = new LinkedList<Overlay>[Enum.GetNames(typeof (EffectPriority)).Length];
    private const float OPACITY_RATE = 1f;
    private int _overlayCount;

    public OverlayManager()
    {
      for (int index = 0; index < this._activeOverlays.Length; ++index)
        this._activeOverlays[index] = new LinkedList<Overlay>();
    }

    public override void OnActivate(Overlay overlay, Vector2 position)
    {
      LinkedList<Overlay> activeOverlay = this._activeOverlays[(int) overlay.Priority];
      if (overlay.Mode == OverlayMode.FadeIn || overlay.Mode == OverlayMode.Active)
        return;
      if (overlay.Mode == OverlayMode.FadeOut)
      {
        activeOverlay.Remove(overlay);
        --this._overlayCount;
      }
      else
        overlay.Opacity = 0.0f;
      if (activeOverlay.Count != 0)
      {
        foreach (Overlay overlay1 in activeOverlay)
          overlay1.Mode = OverlayMode.FadeOut;
      }
      activeOverlay.AddLast(overlay);
      ++this._overlayCount;
    }

    public void Update(GameTime gameTime)
    {
      LinkedListNode<Overlay> next;
      for (int index = 0; index < this._activeOverlays.Length; ++index)
      {
        for (LinkedListNode<Overlay> node = this._activeOverlays[index].First; node != null; node = next)
        {
          Overlay overlay = node.Value;
          next = node.Next;
          overlay.Update(gameTime);
          switch (overlay.Mode)
          {
            case OverlayMode.FadeIn:
              overlay.Opacity += (float) (gameTime.get_ElapsedGameTime().TotalSeconds * 1.0);
              if ((double) overlay.Opacity >= 1.0)
              {
                overlay.Opacity = 1f;
                overlay.Mode = OverlayMode.Active;
                break;
              }
              break;
            case OverlayMode.Active:
              overlay.Opacity = Math.Min(1f, overlay.Opacity + (float) (gameTime.get_ElapsedGameTime().TotalSeconds * 1.0));
              break;
            case OverlayMode.FadeOut:
              overlay.Opacity -= (float) (gameTime.get_ElapsedGameTime().TotalSeconds * 1.0);
              if ((double) overlay.Opacity <= 0.0)
              {
                overlay.Opacity = 0.0f;
                overlay.Mode = OverlayMode.Inactive;
                this._activeOverlays[index].Remove(node);
                --this._overlayCount;
                break;
              }
              break;
          }
        }
      }
    }

    public void Draw(SpriteBatch spriteBatch, RenderLayers layer)
    {
      if (this._overlayCount == 0)
        return;
      bool flag = false;
      for (int index = 0; index < this._activeOverlays.Length; ++index)
      {
        for (LinkedListNode<Overlay> linkedListNode = this._activeOverlays[index].First; linkedListNode != null; linkedListNode = linkedListNode.Next)
        {
          Overlay overlay = linkedListNode.Value;
          if (overlay.Layer == layer && overlay.IsVisible())
          {
            if (!flag)
            {
              spriteBatch.Begin((SpriteSortMode) 1, (BlendState) BlendState.AlphaBlend, (SamplerState) SamplerState.LinearClamp, (DepthStencilState) DepthStencilState.Default, (RasterizerState) RasterizerState.CullNone, (Effect) null, Main.Transform);
              flag = true;
            }
            overlay.Draw(spriteBatch);
          }
        }
      }
      if (!flag)
        return;
      spriteBatch.End();
    }
  }
}
