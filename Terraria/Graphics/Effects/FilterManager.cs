// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.FilterManager
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.IO;

namespace Terraria.Graphics.Effects
{
  public class FilterManager : EffectManager<Filter>
  {
    private LinkedList<Filter> _activeFilters = new LinkedList<Filter>();
    private int _filterLimit = 16;
    private const float OPACITY_RATE = 1f;
    private EffectPriority _priorityThreshold;
    private int _activeFilterCount;
    private bool _captureThisFrame;

    public event Action OnPostDraw;

    public FilterManager()
    {
      Main.Configuration.OnLoad += (Action<Preferences>) (preferences =>
      {
        this._filterLimit = preferences.Get<int>("FilterLimit", 16);
        EffectPriority result;
        if (!Enum.TryParse<EffectPriority>(preferences.Get<string>("FilterPriorityThreshold", "VeryLow"), out result))
          return;
        this._priorityThreshold = result;
      });
      Main.Configuration.OnSave += (Action<Preferences>) (preferences =>
      {
        preferences.Put("FilterLimit", (object) this._filterLimit);
        preferences.Put("FilterPriorityThreshold", (object) Enum.GetName(typeof (EffectPriority), (object) this._priorityThreshold));
      });
    }

    public override void OnActivate(Filter effect, Vector2 position)
    {
      if (this._activeFilters.Contains(effect))
      {
        if (effect.Active)
          return;
        if (effect.Priority >= this._priorityThreshold)
          this._activeFilterCount = this._activeFilterCount - 1;
        this._activeFilters.Remove(effect);
      }
      else
        effect.Opacity = 0.0f;
      if (effect.Priority >= this._priorityThreshold)
        this._activeFilterCount = this._activeFilterCount + 1;
      if (this._activeFilters.Count == 0)
      {
        this._activeFilters.AddLast(effect);
      }
      else
      {
        for (LinkedListNode<Filter> node = this._activeFilters.First; node != null; node = node.Next)
        {
          Filter filter = node.Value;
          if (effect.Priority <= filter.Priority)
          {
            this._activeFilters.AddAfter(node, effect);
            return;
          }
        }
        this._activeFilters.AddLast(effect);
      }
    }

    public void BeginCapture()
    {
      // ISSUE: reference to a compiler-generated field
      if (this._activeFilterCount == 0 && this.OnPostDraw == null)
      {
        this._captureThisFrame = false;
      }
      else
      {
        this._captureThisFrame = true;
        Main.instance.get_GraphicsDevice().SetRenderTarget(Main.screenTarget);
        Main.instance.get_GraphicsDevice().Clear(Color.get_Black());
      }
    }

    public void Update(GameTime gameTime)
    {
      LinkedListNode<Filter> node = this._activeFilters.First;
      int count = this._activeFilters.Count;
      int num = 0;
      LinkedListNode<Filter> next;
      for (; node != null; node = next)
      {
        Filter filter = node.Value;
        next = node.Next;
        bool flag = false;
        if (filter.Priority >= this._priorityThreshold)
        {
          ++num;
          if (num > this._activeFilterCount - this._filterLimit)
          {
            filter.Update(gameTime);
            flag = true;
          }
        }
        if (filter.Active & flag)
          filter.Opacity = Math.Min(filter.Opacity + (float) (gameTime.get_ElapsedGameTime().TotalSeconds * 1.0), 1f);
        else
          filter.Opacity = Math.Max(filter.Opacity - (float) (gameTime.get_ElapsedGameTime().TotalSeconds * 1.0), 0.0f);
        if (!filter.Active && (double) filter.Opacity == 0.0)
        {
          if (filter.Priority >= this._priorityThreshold)
            this._activeFilterCount = this._activeFilterCount - 1;
          this._activeFilters.Remove(node);
        }
      }
    }

    public void EndCapture()
    {
      if (!this._captureThisFrame)
        return;
      LinkedListNode<Filter> linkedListNode = this._activeFilters.First;
      int count = this._activeFilters.Count;
      Filter filter1 = (Filter) null;
      RenderTarget2D renderTarget2D1 = Main.screenTarget;
      GraphicsDevice graphicsDevice = Main.instance.get_GraphicsDevice();
      int num = 0;
      if ((double) Main.player[Main.myPlayer].gravDir == -1.0)
      {
        RenderTarget2D screenTargetSwap = Main.screenTargetSwap;
        graphicsDevice.SetRenderTarget(screenTargetSwap);
        graphicsDevice.Clear(Color.get_Black());
        Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) BlendState.AlphaBlend, (SamplerState) SamplerState.LinearClamp, (DepthStencilState) DepthStencilState.Default, (RasterizerState) RasterizerState.CullNone, (Effect) null, Matrix.Invert(Main.GameViewMatrix.EffectMatrix));
        Main.spriteBatch.Draw((Texture2D) renderTarget2D1, Vector2.get_Zero(), Color.get_White());
        Main.spriteBatch.End();
        renderTarget2D1 = Main.screenTargetSwap;
      }
      LinkedListNode<Filter> next;
      for (; linkedListNode != null; linkedListNode = next)
      {
        Filter filter2 = linkedListNode.Value;
        next = linkedListNode.Next;
        if (filter2.Priority >= this._priorityThreshold)
        {
          ++num;
          if (num > this._activeFilterCount - this._filterLimit && filter2.IsVisible())
          {
            if (filter1 != null)
            {
              RenderTarget2D renderTarget2D2 = renderTarget2D1 != Main.screenTarget ? Main.screenTarget : Main.screenTargetSwap;
              graphicsDevice.SetRenderTarget(renderTarget2D2);
              graphicsDevice.Clear(Color.get_Black());
              Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) BlendState.AlphaBlend);
              filter1.Apply();
              Main.spriteBatch.Draw((Texture2D) renderTarget2D1, Vector2.get_Zero(), Main.bgColor);
              Main.spriteBatch.End();
              renderTarget2D1 = renderTarget2D1 != Main.screenTarget ? Main.screenTarget : Main.screenTargetSwap;
            }
            filter1 = filter2;
          }
        }
      }
      graphicsDevice.SetRenderTarget((RenderTarget2D) null);
      graphicsDevice.Clear(Color.get_Black());
      if ((double) Main.player[Main.myPlayer].gravDir == -1.0)
        Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) BlendState.AlphaBlend, (SamplerState) SamplerState.LinearClamp, (DepthStencilState) DepthStencilState.Default, (RasterizerState) RasterizerState.CullNone, (Effect) null, Main.GameViewMatrix.EffectMatrix);
      else
        Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) BlendState.AlphaBlend);
      if (filter1 != null)
      {
        filter1.Apply();
        Main.spriteBatch.Draw((Texture2D) renderTarget2D1, Vector2.get_Zero(), Main.bgColor);
      }
      else
        Main.spriteBatch.Draw((Texture2D) renderTarget2D1, Vector2.get_Zero(), Color.get_White());
      Main.spriteBatch.End();
      for (int index = 0; index < 8; ++index)
        graphicsDevice.get_Textures().set_Item(index, (Texture) null);
      // ISSUE: reference to a compiler-generated field
      if (this.OnPostDraw == null)
        return;
      // ISSUE: reference to a compiler-generated field
      this.OnPostDraw();
    }

    public bool HasActiveFilter()
    {
      return (uint) this._activeFilters.Count > 0U;
    }

    public bool CanCapture()
    {
      if (!this.HasActiveFilter())
      {
        // ISSUE: reference to a compiler-generated field
        return this.OnPostDraw != null;
      }
      return true;
    }
  }
}
