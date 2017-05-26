// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Chat.GlyphTagHandler
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
  public class GlyphTagHandler : ITagHandler
  {
    public static float GlyphsScale = 1f;
    private static Dictionary<string, int> GlyphIndexes = new Dictionary<string, int>()
    {
      {
        ((Buttons) 4096).ToString(),
        0
      },
      {
        ((Buttons) 8192).ToString(),
        1
      },
      {
        ((Buttons) 32).ToString(),
        4
      },
      {
        ((Buttons) 2).ToString(),
        15
      },
      {
        ((Buttons) 4).ToString(),
        14
      },
      {
        ((Buttons) 8).ToString(),
        13
      },
      {
        ((Buttons) 1).ToString(),
        16
      },
      {
        ((Buttons) 256).ToString(),
        6
      },
      {
        ((Buttons) 64).ToString(),
        10
      },
      {
        ((Buttons) 536870912).ToString(),
        20
      },
      {
        ((Buttons) 2097152).ToString(),
        17
      },
      {
        ((Buttons) 1073741824).ToString(),
        18
      },
      {
        ((Buttons) 268435456).ToString(),
        19
      },
      {
        ((Buttons) 8388608).ToString(),
        8
      },
      {
        ((Buttons) 512).ToString(),
        7
      },
      {
        ((Buttons) 128).ToString(),
        11
      },
      {
        ((Buttons) 33554432).ToString(),
        24
      },
      {
        ((Buttons) 134217728).ToString(),
        21
      },
      {
        ((Buttons) 67108864).ToString(),
        22
      },
      {
        ((Buttons) 16777216).ToString(),
        23
      },
      {
        ((Buttons) 4194304).ToString(),
        9
      },
      {
        ((Buttons) 16).ToString(),
        5
      },
      {
        ((Buttons) 16384).ToString(),
        2
      },
      {
        ((Buttons) 32768).ToString(),
        3
      },
      {
        "LR",
        25
      }
    };
    private const int GlyphsPerLine = 25;
    private const int MaxGlyphs = 26;

    TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
    {
      int result;
      if (!int.TryParse(text, out result) || result >= 26)
        return new TextSnippet(text);
      GlyphTagHandler.GlyphSnippet glyphSnippet = new GlyphTagHandler.GlyphSnippet(result);
      int num = 1;
      glyphSnippet.DeleteWhole = num != 0;
      string str = "[g:" + (object) result + "]";
      glyphSnippet.Text = str;
      return (TextSnippet) glyphSnippet;
    }

    public static string GenerateTag(int index)
    {
      return "[g" + ":" + (object) index + "]";
    }

    public static string GenerateTag(string keyname)
    {
      int index;
      if (GlyphTagHandler.GlyphIndexes.TryGetValue(keyname, out index))
        return GlyphTagHandler.GenerateTag(index);
      return keyname;
    }

    private class GlyphSnippet : TextSnippet
    {
      private int _glyphIndex;

      public GlyphSnippet(int index)
        : base("")
      {
        this._glyphIndex = index;
        this.Color = Color.get_White();
      }

      public override bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = null, Color color = null, float scale = 1f)
      {
        if (!justCheckingString && Color.op_Inequality(color, Color.get_Black()))
        {
          int frameX = this._glyphIndex;
          if (this._glyphIndex == 25)
            frameX = (double) Main.GlobalTime % 0.600000023841858 < 0.300000011920929 ? 17 : 18;
          Texture2D tex = Main.textGlyphTexture[0];
          spriteBatch.Draw(tex, position, new Rectangle?(tex.Frame(25, 1, frameX, frameX / 25)), color, 0.0f, Vector2.get_Zero(), GlyphTagHandler.GlyphsScale, (SpriteEffects) 0, 0.0f);
        }
        size = Vector2.op_Multiply(new Vector2(26f), GlyphTagHandler.GlyphsScale);
        return true;
      }

      public override float GetStringLength(DynamicSpriteFont font)
      {
        return 26f * GlyphTagHandler.GlyphsScale;
      }
    }
  }
}
