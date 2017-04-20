// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.ScreenShaderData
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Shaders
{
  public class ScreenShaderData : ShaderData
  {
    private Vector3 _uColor = Vector3.get_One();
    private Vector3 _uSecondaryColor = Vector3.get_One();
    private float _uOpacity = 1f;
    private float _globalOpacity = 1f;
    private float _uIntensity = 1f;
    private Vector2 _uTargetPosition = Vector2.get_One();
    private Vector2 _uDirection = new Vector2(0.0f, 1f);
    private Vector2 _uImageOffset = Vector2.get_Zero();
    private Ref<Texture2D>[] _uImages = new Ref<Texture2D>[3];
    private SamplerState[] _samplerStates = new SamplerState[3];
    private Vector2[] _imageScales = new Vector2[3]
    {
      Vector2.get_One(),
      Vector2.get_One(),
      Vector2.get_One()
    };
    private float _uProgress;

    public float Intensity
    {
      get
      {
        return this._uIntensity;
      }
    }

    public float CombinedOpacity
    {
      get
      {
        return this._uOpacity * this._globalOpacity;
      }
    }

    public ScreenShaderData(string passName)
      : base(Main.ScreenShaderRef, passName)
    {
    }

    public ScreenShaderData(Ref<Effect> shader, string passName)
      : base(shader, passName)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
    }

    public new virtual void Apply()
    {
      Vector2 vector2_1;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).\u002Ector((float) Main.offScreenRange, (float) Main.offScreenRange);
      Vector2 vector2_2 = Vector2.op_Division(new Vector2((float) Main.screenWidth, (float) Main.screenHeight), Main.GameViewMatrix.Zoom);
      Vector2 vector2_3 = Vector2.op_Multiply(new Vector2((float) Main.screenWidth, (float) Main.screenHeight), 0.5f);
      Vector2 vector2_4 = Vector2.op_Addition(Main.screenPosition, Vector2.op_Multiply(vector2_3, Vector2.op_Subtraction(Vector2.get_One(), Vector2.op_Division(Vector2.get_One(), Main.GameViewMatrix.Zoom))));
      this.Shader.get_Parameters().get_Item("uColor").SetValue(this._uColor);
      this.Shader.get_Parameters().get_Item("uOpacity").SetValue(this.CombinedOpacity);
      this.Shader.get_Parameters().get_Item("uSecondaryColor").SetValue(this._uSecondaryColor);
      this.Shader.get_Parameters().get_Item("uTime").SetValue(Main.GlobalTime);
      this.Shader.get_Parameters().get_Item("uScreenResolution").SetValue(vector2_2);
      this.Shader.get_Parameters().get_Item("uScreenPosition").SetValue(Vector2.op_Subtraction(vector2_4, vector2_1));
      this.Shader.get_Parameters().get_Item("uTargetPosition").SetValue(Vector2.op_Subtraction(this._uTargetPosition, vector2_1));
      this.Shader.get_Parameters().get_Item("uImageOffset").SetValue(this._uImageOffset);
      this.Shader.get_Parameters().get_Item("uIntensity").SetValue(this._uIntensity);
      this.Shader.get_Parameters().get_Item("uProgress").SetValue(this._uProgress);
      this.Shader.get_Parameters().get_Item("uDirection").SetValue(this._uDirection);
      this.Shader.get_Parameters().get_Item("uZoom").SetValue(Main.GameViewMatrix.Zoom);
      for (int index = 0; index < this._uImages.Length; ++index)
      {
        if (this._uImages[index] != null && this._uImages[index].Value != null)
        {
          Main.graphics.get_GraphicsDevice().get_Textures().set_Item(index + 1, (Texture) this._uImages[index].Value);
          int width = this._uImages[index].Value.get_Width();
          int height = this._uImages[index].Value.get_Height();
          if (this._samplerStates[index] != null)
            Main.graphics.get_GraphicsDevice().get_SamplerStates().set_Item(index + 1, this._samplerStates[index]);
          else if (Utils.IsPowerOfTwo(width) && Utils.IsPowerOfTwo(height))
            Main.graphics.get_GraphicsDevice().get_SamplerStates().set_Item(index + 1, (SamplerState) SamplerState.LinearWrap);
          else
            Main.graphics.get_GraphicsDevice().get_SamplerStates().set_Item(index + 1, (SamplerState) SamplerState.AnisotropicClamp);
          this.Shader.get_Parameters().get_Item("uImageSize" + (object) (index + 1)).SetValue(Vector2.op_Multiply(new Vector2((float) width, (float) height), this._imageScales[index]));
        }
      }
      base.Apply();
    }

    public ScreenShaderData UseImageOffset(Vector2 offset)
    {
      this._uImageOffset = offset;
      return this;
    }

    public ScreenShaderData UseIntensity(float intensity)
    {
      this._uIntensity = intensity;
      return this;
    }

    public ScreenShaderData UseColor(float r, float g, float b)
    {
      return this.UseColor(new Vector3(r, g, b));
    }

    public ScreenShaderData UseProgress(float progress)
    {
      this._uProgress = progress;
      return this;
    }

    public ScreenShaderData UseImage(Texture2D image, int index = 0, SamplerState samplerState = null)
    {
      this._samplerStates[index] = samplerState;
      if (this._uImages[index] == null)
        this._uImages[index] = new Ref<Texture2D>(image);
      else
        this._uImages[index].Value = image;
      return this;
    }

    public ScreenShaderData UseImage(string path, int index = 0, SamplerState samplerState = null)
    {
      this._uImages[index] = TextureManager.AsyncLoad(path);
      this._samplerStates[index] = samplerState;
      return this;
    }

    public ScreenShaderData UseColor(Color color)
    {
      // ISSUE: explicit reference operation
      return this.UseColor(((Color) @color).ToVector3());
    }

    public ScreenShaderData UseColor(Vector3 color)
    {
      this._uColor = color;
      return this;
    }

    public ScreenShaderData UseDirection(Vector2 direction)
    {
      this._uDirection = direction;
      return this;
    }

    public ScreenShaderData UseGlobalOpacity(float opacity)
    {
      this._globalOpacity = opacity;
      return this;
    }

    public ScreenShaderData UseTargetPosition(Vector2 position)
    {
      this._uTargetPosition = position;
      return this;
    }

    public ScreenShaderData UseSecondaryColor(float r, float g, float b)
    {
      return this.UseSecondaryColor(new Vector3(r, g, b));
    }

    public ScreenShaderData UseSecondaryColor(Color color)
    {
      // ISSUE: explicit reference operation
      return this.UseSecondaryColor(((Color) @color).ToVector3());
    }

    public ScreenShaderData UseSecondaryColor(Vector3 color)
    {
      this._uSecondaryColor = color;
      return this;
    }

    public ScreenShaderData UseOpacity(float opacity)
    {
      this._uOpacity = opacity;
      return this;
    }

    public ScreenShaderData UseImageScale(Vector2 scale, int index = 0)
    {
      this._imageScales[index] = scale;
      return this;
    }

    public virtual ScreenShaderData GetSecondaryShader(Player player)
    {
      return this;
    }
  }
}
