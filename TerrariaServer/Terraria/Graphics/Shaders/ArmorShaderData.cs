// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.ArmorShaderData
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
  public class ArmorShaderData : ShaderData
  {
    private Vector3 _uColor = Vector3.get_One();
    private Vector3 _uSecondaryColor = Vector3.get_One();
    private float _uSaturation = 1f;
    private float _uOpacity = 1f;
    private Ref<Texture2D> _uImage;

    public ArmorShaderData(Ref<Effect> shader, string passName)
      : base(shader, passName)
    {
    }

    public virtual void Apply(Entity entity, DrawData? drawData = null)
    {
      this.Shader.get_Parameters().get_Item("uColor").SetValue(this._uColor);
      this.Shader.get_Parameters().get_Item("uSaturation").SetValue(this._uSaturation);
      this.Shader.get_Parameters().get_Item("uSecondaryColor").SetValue(this._uSecondaryColor);
      this.Shader.get_Parameters().get_Item("uTime").SetValue(Main.GlobalTime);
      this.Shader.get_Parameters().get_Item("uOpacity").SetValue(this._uOpacity);
      if (drawData.HasValue)
      {
        DrawData drawData1 = drawData.Value;
        Vector4 vector4;
        if (drawData1.sourceRect.HasValue)
        {
          // ISSUE: explicit reference operation
          ((Vector4) @vector4).\u002Ector((float) drawData1.sourceRect.Value.X, (float) drawData1.sourceRect.Value.Y, (float) drawData1.sourceRect.Value.Width, (float) drawData1.sourceRect.Value.Height);
        }
        else
        {
          // ISSUE: explicit reference operation
          ((Vector4) @vector4).\u002Ector(0.0f, 0.0f, (float) drawData1.texture.get_Width(), (float) drawData1.texture.get_Height());
        }
        this.Shader.get_Parameters().get_Item("uSourceRect").SetValue(vector4);
        this.Shader.get_Parameters().get_Item("uWorldPosition").SetValue(Vector2.op_Addition(Main.screenPosition, drawData1.position));
        this.Shader.get_Parameters().get_Item("uImageSize0").SetValue(new Vector2((float) drawData1.texture.get_Width(), (float) drawData1.texture.get_Height()));
        this.Shader.get_Parameters().get_Item("uRotation").SetValue(drawData1.rotation * (((Enum) (object) drawData1.effect).HasFlag((Enum) (object) (SpriteEffects) 1) ? -1f : 1f));
        this.Shader.get_Parameters().get_Item("uDirection").SetValue(((Enum) (object) drawData1.effect).HasFlag((Enum) (object) (SpriteEffects) 1) ? -1 : 1);
      }
      else
      {
        this.Shader.get_Parameters().get_Item("uSourceRect").SetValue(new Vector4(0.0f, 0.0f, 4f, 4f));
        this.Shader.get_Parameters().get_Item("uRotation").SetValue(0.0f);
      }
      if (this._uImage != null)
      {
        Main.graphics.get_GraphicsDevice().get_Textures().set_Item(1, (Texture) this._uImage.Value);
        this.Shader.get_Parameters().get_Item("uImageSize1").SetValue(new Vector2((float) this._uImage.Value.get_Width(), (float) this._uImage.Value.get_Height()));
      }
      if (entity != null)
        this.Shader.get_Parameters().get_Item("uDirection").SetValue((float) entity.direction);
      this.Apply();
    }

    public ArmorShaderData UseColor(float r, float g, float b)
    {
      return this.UseColor(new Vector3(r, g, b));
    }

    public ArmorShaderData UseColor(Color color)
    {
      // ISSUE: explicit reference operation
      return this.UseColor(((Color) @color).ToVector3());
    }

    public ArmorShaderData UseColor(Vector3 color)
    {
      this._uColor = color;
      return this;
    }

    public ArmorShaderData UseImage(string path)
    {
      this._uImage = TextureManager.AsyncLoad(path);
      return this;
    }

    public ArmorShaderData UseOpacity(float alpha)
    {
      this._uOpacity = alpha;
      return this;
    }

    public ArmorShaderData UseSecondaryColor(float r, float g, float b)
    {
      return this.UseSecondaryColor(new Vector3(r, g, b));
    }

    public ArmorShaderData UseSecondaryColor(Color color)
    {
      // ISSUE: explicit reference operation
      return this.UseSecondaryColor(((Color) @color).ToVector3());
    }

    public ArmorShaderData UseSecondaryColor(Vector3 color)
    {
      this._uSecondaryColor = color;
      return this;
    }

    public ArmorShaderData UseSaturation(float saturation)
    {
      this._uSaturation = saturation;
      return this;
    }

    public virtual ArmorShaderData GetSecondaryShader(Entity entity)
    {
      return this;
    }
  }
}
