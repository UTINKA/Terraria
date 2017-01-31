// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.ArmorShaderData
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
  public class ArmorShaderData : ShaderData
  {
    private Vector3 _uColor = Vector3.One;
    private Vector3 _uSecondaryColor = Vector3.One;
    private float _uSaturation = 1f;
    private float _uOpacity = 1f;
    private Ref<Texture2D> _uImage;

    public ArmorShaderData(Ref<Effect> shader, string passName)
      : base(shader, passName)
    {
    }

    public virtual void Apply(Entity entity, DrawData? drawData = null)
    {
      this.Shader.Parameters["uColor"].SetValue(this._uColor);
      this.Shader.Parameters["uSaturation"].SetValue(this._uSaturation);
      this.Shader.Parameters["uSecondaryColor"].SetValue(this._uSecondaryColor);
      this.Shader.Parameters["uTime"].SetValue(Main.GlobalTime);
      this.Shader.Parameters["uOpacity"].SetValue(this._uOpacity);
      if (drawData.HasValue)
      {
        DrawData drawData1 = drawData.Value;
        this.Shader.Parameters["uSourceRect"].SetValue(!drawData1.sourceRect.HasValue ? new Vector4(0.0f, 0.0f, (float) drawData1.texture.Width, (float) drawData1.texture.Height) : new Vector4((float) drawData1.sourceRect.Value.X, (float) drawData1.sourceRect.Value.Y, (float) drawData1.sourceRect.Value.Width, (float) drawData1.sourceRect.Value.Height));
        this.Shader.Parameters["uWorldPosition"].SetValue(Main.screenPosition + drawData1.position);
        this.Shader.Parameters["uImageSize0"].SetValue(new Vector2((float) drawData1.texture.Width, (float) drawData1.texture.Height));
        this.Shader.Parameters["uRotation"].SetValue(drawData1.rotation * (drawData1.effect.HasFlag((Enum) SpriteEffects.FlipHorizontally) ? -1f : 1f));
        this.Shader.Parameters["uDirection"].SetValue(drawData1.effect.HasFlag((Enum) SpriteEffects.FlipHorizontally) ? -1 : 1);
      }
      else
      {
        this.Shader.Parameters["uSourceRect"].SetValue(new Vector4(0.0f, 0.0f, 4f, 4f));
        this.Shader.Parameters["uRotation"].SetValue(0.0f);
      }
      if (this._uImage != null)
      {
        Main.graphics.GraphicsDevice.Textures[1] = (Texture) this._uImage.Value;
        this.Shader.Parameters["uImageSize1"].SetValue(new Vector2((float) this._uImage.Value.Width, (float) this._uImage.Value.Height));
      }
      if (entity != null)
        this.Shader.Parameters["uDirection"].SetValue((float) entity.direction);
      this.Apply();
    }

    public ArmorShaderData UseColor(float r, float g, float b)
    {
      return this.UseColor(new Vector3(r, g, b));
    }

    public ArmorShaderData UseColor(Color color)
    {
      return this.UseColor(color.ToVector3());
    }

    public ArmorShaderData UseColor(Vector3 color)
    {
      this._uColor = color;
      return this;
    }

    public ArmorShaderData UseImage(string path)
    {
      this._uImage = TextureManager.Retrieve(path);
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
      return this.UseSecondaryColor(color.ToVector3());
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
