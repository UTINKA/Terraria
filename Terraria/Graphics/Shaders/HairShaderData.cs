// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.HairShaderData
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
  public class HairShaderData : ShaderData
  {
    protected Vector3 _uColor = Vector3.get_One();
    protected Vector3 _uSecondaryColor = Vector3.get_One();
    protected float _uSaturation = 1f;
    protected float _uOpacity = 1f;
    protected Ref<Texture2D> _uImage;
    protected bool _shaderDisabled;

    public bool ShaderDisabled
    {
      get
      {
        return this._shaderDisabled;
      }
    }

    public HairShaderData(Ref<Effect> shader, string passName)
      : base(shader, passName)
    {
    }

    public virtual void Apply(Player player, DrawData? drawData = null)
    {
      if (this._shaderDisabled)
        return;
      this.Shader.get_Parameters().get_Item("uColor").SetValue(this._uColor);
      this.Shader.get_Parameters().get_Item("uSaturation").SetValue(this._uSaturation);
      this.Shader.get_Parameters().get_Item("uSecondaryColor").SetValue(this._uSecondaryColor);
      this.Shader.get_Parameters().get_Item("uTime").SetValue(Main.GlobalTime);
      this.Shader.get_Parameters().get_Item("uOpacity").SetValue(this._uOpacity);
      if (drawData.HasValue)
      {
        DrawData drawData1 = drawData.Value;
        Vector4 vector4;
        // ISSUE: explicit reference operation
        ((Vector4) @vector4).\u002Ector((float) drawData1.sourceRect.Value.X, (float) drawData1.sourceRect.Value.Y, (float) drawData1.sourceRect.Value.Width, (float) drawData1.sourceRect.Value.Height);
        this.Shader.get_Parameters().get_Item("uSourceRect").SetValue(vector4);
        this.Shader.get_Parameters().get_Item("uWorldPosition").SetValue(Vector2.op_Addition(Main.screenPosition, drawData1.position));
        this.Shader.get_Parameters().get_Item("uImageSize0").SetValue(new Vector2((float) drawData1.texture.get_Width(), (float) drawData1.texture.get_Height()));
      }
      else
        this.Shader.get_Parameters().get_Item("uSourceRect").SetValue(new Vector4(0.0f, 0.0f, 4f, 4f));
      if (this._uImage != null)
      {
        Main.graphics.get_GraphicsDevice().get_Textures().set_Item(1, (Texture) this._uImage.Value);
        this.Shader.get_Parameters().get_Item("uImageSize1").SetValue(new Vector2((float) this._uImage.Value.get_Width(), (float) this._uImage.Value.get_Height()));
      }
      if (player != null)
        this.Shader.get_Parameters().get_Item("uDirection").SetValue((float) player.direction);
      this.Apply();
    }

    public virtual Color GetColor(Player player, Color lightColor)
    {
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      return new Color(Vector4.op_Multiply(((Color) @lightColor).ToVector4(), ((Color) @player.hairColor).ToVector4()));
    }

    public HairShaderData UseColor(float r, float g, float b)
    {
      return this.UseColor(new Vector3(r, g, b));
    }

    public HairShaderData UseColor(Color color)
    {
      // ISSUE: explicit reference operation
      return this.UseColor(((Color) @color).ToVector3());
    }

    public HairShaderData UseColor(Vector3 color)
    {
      this._uColor = color;
      return this;
    }

    public HairShaderData UseImage(string path)
    {
      this._uImage = TextureManager.AsyncLoad(path);
      return this;
    }

    public HairShaderData UseOpacity(float alpha)
    {
      this._uOpacity = alpha;
      return this;
    }

    public HairShaderData UseSecondaryColor(float r, float g, float b)
    {
      return this.UseSecondaryColor(new Vector3(r, g, b));
    }

    public HairShaderData UseSecondaryColor(Color color)
    {
      // ISSUE: explicit reference operation
      return this.UseSecondaryColor(((Color) @color).ToVector3());
    }

    public HairShaderData UseSecondaryColor(Vector3 color)
    {
      this._uSecondaryColor = color;
      return this;
    }

    public HairShaderData UseSaturation(float saturation)
    {
      this._uSaturation = saturation;
      return this;
    }
  }
}
