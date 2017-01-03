// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.ShaderData
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Shaders
{
  public class ShaderData
  {
    protected Ref<Effect> _shader;
    protected string _passName;
    private EffectPass _effectPass;
    private Effect _lastEffect;

    public Effect Shader
    {
      get
      {
        if (this._shader != null)
          return this._shader.Value;
        return (Effect) null;
      }
    }

    public ShaderData(Ref<Effect> shader, string passName)
    {
      this._passName = passName;
      this._shader = shader;
    }

    public void SwapProgram(string passName)
    {
      this._passName = passName;
      if (passName == null)
        return;
      this._effectPass = this.Shader.CurrentTechnique.Passes[passName];
    }

    protected virtual void Apply()
    {
      if (this._shader != null && this._lastEffect != this._shader.Value && (this.Shader != null && this._passName != null))
        this._effectPass = this.Shader.CurrentTechnique.Passes[this._passName];
      this._effectPass.Apply();
    }
  }
}
