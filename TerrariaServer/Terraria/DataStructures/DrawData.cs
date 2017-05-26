// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.DrawData
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
  public struct DrawData
  {
    public Texture2D texture;
    public Vector2 position;
    public Rectangle destinationRectangle;
    public Rectangle? sourceRect;
    public Color color;
    public float rotation;
    public Vector2 origin;
    public Vector2 scale;
    public SpriteEffects effect;
    public int shader;
    public bool ignorePlayerRotation;
    public readonly bool useDestinationRectangle;
    public static Rectangle? nullRectangle;

    public DrawData(Texture2D texture, Vector2 position, Color color)
    {
      this.texture = texture;
      this.position = position;
      this.color = color;
      this.destinationRectangle = (Rectangle) null;
      this.sourceRect = DrawData.nullRectangle;
      this.rotation = 0.0f;
      this.origin = Vector2.get_Zero();
      this.scale = Vector2.get_One();
      this.effect = (SpriteEffects) 0;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color)
    {
      this.texture = texture;
      this.position = position;
      this.color = color;
      this.destinationRectangle = (Rectangle) null;
      this.sourceRect = sourceRect;
      this.rotation = 0.0f;
      this.origin = Vector2.get_Zero();
      this.scale = Vector2.get_One();
      this.effect = (SpriteEffects) 0;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effect, int inactiveLayerDepth)
    {
      this.texture = texture;
      this.position = position;
      this.sourceRect = sourceRect;
      this.color = color;
      this.rotation = rotation;
      this.origin = origin;
      this.scale = new Vector2(scale, scale);
      this.effect = effect;
      this.destinationRectangle = (Rectangle) null;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effect, int inactiveLayerDepth)
    {
      this.texture = texture;
      this.position = position;
      this.sourceRect = sourceRect;
      this.color = color;
      this.rotation = rotation;
      this.origin = origin;
      this.scale = scale;
      this.effect = effect;
      this.destinationRectangle = (Rectangle) null;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(Texture2D texture, Rectangle destinationRectangle, Color color)
    {
      this.texture = texture;
      this.destinationRectangle = destinationRectangle;
      this.color = color;
      this.position = Vector2.get_Zero();
      this.sourceRect = DrawData.nullRectangle;
      this.rotation = 0.0f;
      this.origin = Vector2.get_Zero();
      this.scale = Vector2.get_One();
      this.effect = (SpriteEffects) 0;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRect, Color color)
    {
      this.texture = texture;
      this.destinationRectangle = destinationRectangle;
      this.color = color;
      this.position = Vector2.get_Zero();
      this.sourceRect = sourceRect;
      this.rotation = 0.0f;
      this.origin = Vector2.get_Zero();
      this.scale = Vector2.get_One();
      this.effect = (SpriteEffects) 0;
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public DrawData(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, SpriteEffects effect, int inactiveLayerDepth)
    {
      this.texture = texture;
      this.destinationRectangle = destinationRectangle;
      this.sourceRect = sourceRect;
      this.color = color;
      this.rotation = rotation;
      this.origin = origin;
      this.effect = effect;
      this.position = Vector2.get_Zero();
      this.scale = Vector2.get_One();
      this.shader = 0;
      this.ignorePlayerRotation = false;
      this.useDestinationRectangle = false;
    }

    public void Draw(SpriteBatch sb)
    {
      if (this.useDestinationRectangle)
        sb.Draw(this.texture, this.destinationRectangle, this.sourceRect, this.color, this.rotation, this.origin, this.effect, 0.0f);
      else
        sb.Draw(this.texture, this.position, this.sourceRect, this.color, this.rotation, this.origin, this.scale, this.effect, 0.0f);
    }
  }
}
