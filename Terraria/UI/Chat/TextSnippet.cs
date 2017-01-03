// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Chat.TextSnippet
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.UI.Chat
{
  public class TextSnippet
  {
    public Color Color = Color.White;
    public float Scale = 1f;
    public string Text;
    public string TextOriginal;
    public bool CheckForHover;
    public bool DeleteWhole;

    public TextSnippet(string text = "")
    {
      this.Text = text;
      this.TextOriginal = text;
    }

    public TextSnippet(string text, Color color, float scale = 1f)
    {
      this.Text = text;
      this.TextOriginal = text;
      this.Color = color;
      this.Scale = scale;
    }

    public virtual void Update()
    {
    }

    public virtual void OnHover()
    {
    }

    public virtual void OnClick()
    {
    }

    public virtual Color GetVisibleColor()
    {
      return ChatManager.WaveColor(this.Color);
    }

    public virtual bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = null, Color color = null, float scale = 1f)
    {
      size = Vector2.Zero;
      return false;
    }

    public virtual TextSnippet CopyMorph(string newText)
    {
      TextSnippet textSnippet = (TextSnippet) this.MemberwiseClone();
      textSnippet.Text = newText;
      return textSnippet;
    }

    public virtual float GetStringLength(SpriteFont font)
    {
      return font.MeasureString(this.Text).X * this.Scale;
    }
  }
}
