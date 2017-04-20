// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.GameEffect
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
