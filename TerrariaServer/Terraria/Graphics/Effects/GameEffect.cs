// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.GameEffect
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

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
