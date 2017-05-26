// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.GameEffect
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Effects
{
  public abstract class GameEffect
  {
    public float Opacity;
    protected bool _isLoaded;
    protected EffectPriority _priority;

    public bool IsLoaded
    {
      get
      {
        return this._isLoaded;
      }
    }

    public EffectPriority Priority
    {
      get
      {
        return this._priority;
      }
    }

    public void Load()
    {
      if (this._isLoaded)
        return;
      this._isLoaded = true;
      this.OnLoad();
    }

    public virtual void OnLoad()
    {
    }

    public abstract bool IsVisible();

    internal abstract void Activate(Vector2 position, params object[] args);

    internal abstract void Deactivate(params object[] args);
  }
}
