// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.EffectManager`1
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Terraria.Graphics.Effects
{
  public abstract class EffectManager<T> where T : GameEffect
  {
    protected Dictionary<string, T> _effects = new Dictionary<string, T>();
    protected bool _isLoaded;

    public bool IsLoaded
    {
      get
      {
        return this._isLoaded;
      }
    }

    public T this[string key]
    {
      get
      {
        T obj;
        if (this._effects.TryGetValue(key, out obj))
          return obj;
        return default (T);
      }
      set
      {
        this.Bind(key, value);
      }
    }

    public void Bind(string name, T effect)
    {
      this._effects[name] = effect;
      if (!this._isLoaded)
        return;
      effect.Load();
    }

    public void Load()
    {
      if (this._isLoaded)
        return;
      this._isLoaded = true;
      foreach (T obj in this._effects.Values)
        obj.Load();
    }

    public T Activate(string name, Vector2 position = null, params object[] args)
    {
      if (!this._effects.ContainsKey(name))
        throw new MissingEffectException("Unable to find effect named: " + name + ". Type: " + (object) typeof (T) + ".");
      T effect = this._effects[name];
      this.OnActivate(effect, position);
      effect.Activate(position, args);
      return effect;
    }

    public void Deactivate(string name, params object[] args)
    {
      if (!this._effects.ContainsKey(name))
        throw new MissingEffectException("Unable to find effect named: " + name + ". Type: " + (object) typeof (T) + ".");
      T effect = this._effects[name];
      this.OnDeactivate(effect);
      effect.Deactivate(args);
    }

    public virtual void OnActivate(T effect, Vector2 position)
    {
    }

    public virtual void OnDeactivate(T effect)
    {
    }
  }
}
