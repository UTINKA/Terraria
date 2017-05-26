// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Chat.TextSnippet
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace Terraria.UI.Chat
{
  public class TextSnippet
  {
    public Color Color = Color.get_White();
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
      size = Vector2.get_Zero();
      return false;
    }

    public virtual TextSnippet CopyMorph(string newText)
    {
      TextSnippet textSnippet = (TextSnippet) this.MemberwiseClone();
      string str = newText;
      textSnippet.Text = str;
      return textSnippet;
    }

    public virtual float GetStringLength(DynamicSpriteFont font)
    {
      return (float) font.MeasureString(this.Text).X * this.Scale;
    }
  }
}
