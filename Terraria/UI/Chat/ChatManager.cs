// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Chat.ChatManager
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria.Chat;
using Terraria.GameContent.UI.Chat;

namespace Terraria.UI.Chat
{
  public static class ChatManager
  {
    public static readonly ChatCommandProcessor Commands = new ChatCommandProcessor();
    private static ConcurrentDictionary<string, ITagHandler> _handlers = new ConcurrentDictionary<string, ITagHandler>();
    public static readonly Vector2[] ShadowDirections = new Vector2[4]
    {
      Vector2.op_UnaryNegation(Vector2.get_UnitX()),
      Vector2.get_UnitX(),
      Vector2.op_UnaryNegation(Vector2.get_UnitY()),
      Vector2.get_UnitY()
    };

    public static Color WaveColor(Color color)
    {
      float num = (float) Main.mouseTextColor / (float) byte.MaxValue;
      color = Color.Lerp(color, Color.get_Black(), 1f - num);
      // ISSUE: explicit reference operation
      ((Color) @color).set_A(Main.mouseTextColor);
      return color;
    }

    public static void ConvertNormalSnippets(TextSnippet[] snippets)
    {
      for (int index = 0; index < snippets.Length; ++index)
      {
        TextSnippet snippet = snippets[index];
        if (snippets[index].GetType() == typeof (TextSnippet))
        {
          PlainTagHandler.PlainSnippet plainSnippet = new PlainTagHandler.PlainSnippet(snippet.Text, snippet.Color, snippet.Scale);
          snippets[index] = (TextSnippet) plainSnippet;
        }
      }
    }

    public static void Register<T>(params string[] names) where T : ITagHandler, new()
    {
      T obj = new T();
      for (int index = 0; index < names.Length; ++index)
        ChatManager._handlers[names[index].ToLower()] = (ITagHandler) obj;
    }

    private static ITagHandler GetHandler(string tagName)
    {
      string lower = tagName.ToLower();
      if (ChatManager._handlers.ContainsKey(lower))
        return ChatManager._handlers[lower];
      return (ITagHandler) null;
    }

    public static List<TextSnippet> ParseMessage(string text, Color baseColor)
    {
      MatchCollection matchCollection = ChatManager.Regexes.Format.Matches(text);
      List<TextSnippet> textSnippetList = new List<TextSnippet>();
      int startIndex = 0;
      foreach (Match match in matchCollection)
      {
        if (match.Index > startIndex)
          textSnippetList.Add(new TextSnippet(text.Substring(startIndex, match.Index - startIndex), baseColor, 1f));
        startIndex = match.Index + match.Length;
        string tagName = match.Groups["tag"].Value;
        string text1 = match.Groups["text"].Value;
        string options = match.Groups["options"].Value;
        ITagHandler handler = ChatManager.GetHandler(tagName);
        if (handler != null)
        {
          textSnippetList.Add(handler.Parse(text1, baseColor, options));
          textSnippetList[textSnippetList.Count - 1].TextOriginal = match.ToString();
        }
        else
          textSnippetList.Add(new TextSnippet(text1, baseColor, 1f));
      }
      if (text.Length > startIndex)
        textSnippetList.Add(new TextSnippet(text.Substring(startIndex, text.Length - startIndex), baseColor, 1f));
      return textSnippetList;
    }

    public static bool AddChatText(DynamicSpriteFont font, string text, Vector2 baseScale)
    {
      int num = Main.screenWidth - 330;
      if (ChatManager.GetStringSize(font, Main.chatText + text, baseScale, -1f).X > (double) num)
        return false;
      Main.chatText += text;
      return true;
    }

    public static Vector2 GetStringSize(DynamicSpriteFont font, string text, Vector2 baseScale, float maxWidth = -1f)
    {
      TextSnippet[] array = ChatManager.ParseMessage(text, Color.get_White()).ToArray();
      return ChatManager.GetStringSize(font, array, baseScale, maxWidth);
    }

    public static Vector2 GetStringSize(DynamicSpriteFont font, TextSnippet[] snippets, Vector2 baseScale, float maxWidth = -1f)
    {
      Vector2 vec;
      // ISSUE: explicit reference operation
      ((Vector2) @vec).\u002Ector((float) Main.mouseX, (float) Main.mouseY);
      Vector2 zero = Vector2.get_Zero();
      Vector2 minimum = zero;
      Vector2 vector2_1 = minimum;
      float x = (float) font.MeasureString(" ").X;
      float num1 = 0.0f;
      for (int index1 = 0; index1 < snippets.Length; ++index1)
      {
        TextSnippet snippet = snippets[index1];
        snippet.Update();
        float scale = snippet.Scale;
        Vector2 size;
        if (snippet.UniqueDraw(true, out size, (SpriteBatch) null, (Vector2) null, (Color) null, 1f))
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @minimum;
          // ISSUE: explicit reference operation
          double num2 = (^local).X + size.X * baseScale.X * (double) scale;
          // ISSUE: explicit reference operation
          (^local).X = (__Null) num2;
          vector2_1.X = (__Null) (double) Math.Max((float) vector2_1.X, (float) minimum.X);
          vector2_1.Y = (__Null) (double) Math.Max((float) vector2_1.Y, (float) (minimum.Y + size.Y));
        }
        else
        {
          string[] strArray1 = snippet.Text.Split('\n');
          foreach (string str in strArray1)
          {
            char[] chArray = new char[1]{ ' ' };
            string[] strArray2 = str.Split(chArray);
            for (int index2 = 0; index2 < strArray2.Length; ++index2)
            {
              if (index2 != 0)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                Vector2& local = @minimum;
                // ISSUE: explicit reference operation
                double num2 = (^local).X + (double) x * baseScale.X * (double) scale;
                // ISSUE: explicit reference operation
                (^local).X = (__Null) num2;
              }
              if ((double) maxWidth > 0.0)
              {
                float num2 = (float) (font.MeasureString(strArray2[index2]).X * baseScale.X) * scale;
                if (minimum.X - zero.X + (double) num2 > (double) maxWidth)
                {
                  minimum.X = zero.X;
                  // ISSUE: explicit reference operation
                  // ISSUE: variable of a reference type
                  Vector2& local = @minimum;
                  // ISSUE: explicit reference operation
                  double num3 = (^local).Y + (double) font.get_LineSpacing() * (double) num1 * baseScale.Y;
                  // ISSUE: explicit reference operation
                  (^local).Y = (__Null) num3;
                  vector2_1.Y = (__Null) (double) Math.Max((float) vector2_1.Y, (float) minimum.Y);
                  num1 = 0.0f;
                }
              }
              if ((double) num1 < (double) scale)
                num1 = scale;
              Vector2 vector2_2 = font.MeasureString(strArray2[index2]);
              vec.Between(minimum, Vector2.op_Addition(minimum, vector2_2));
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local1 = @minimum;
              // ISSUE: explicit reference operation
              double num4 = (^local1).X + vector2_2.X * baseScale.X * (double) scale;
              // ISSUE: explicit reference operation
              (^local1).X = (__Null) num4;
              vector2_1.X = (__Null) (double) Math.Max((float) vector2_1.X, (float) minimum.X);
              vector2_1.Y = (__Null) (double) Math.Max((float) vector2_1.Y, (float) (minimum.Y + vector2_2.Y));
            }
            if (strArray1.Length > 1)
            {
              minimum.X = zero.X;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local = @minimum;
              // ISSUE: explicit reference operation
              double num2 = (^local).Y + (double) font.get_LineSpacing() * (double) num1 * baseScale.Y;
              // ISSUE: explicit reference operation
              (^local).Y = (__Null) num2;
              vector2_1.Y = (__Null) (double) Math.Max((float) vector2_1.Y, (float) minimum.Y);
              num1 = 0.0f;
            }
          }
        }
      }
      return vector2_1;
    }

    public static void DrawColorCodedStringShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
    {
      for (int index = 0; index < ChatManager.ShadowDirections.Length; ++index)
      {
        int hoveredSnippet;
        ChatManager.DrawColorCodedString(spriteBatch, font, snippets, Vector2.op_Addition(position, Vector2.op_Multiply(ChatManager.ShadowDirections[index], spread)), baseColor, rotation, origin, baseScale, out hoveredSnippet, maxWidth, true);
      }
    }

    public static Vector2 DrawColorCodedString(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth, bool ignoreColors = false)
    {
      int num1 = -1;
      Vector2 vec;
      // ISSUE: explicit reference operation
      ((Vector2) @vec).\u002Ector((float) Main.mouseX, (float) Main.mouseY);
      Vector2 vector2_1 = position;
      Vector2 vector2_2 = vector2_1;
      float x = (float) font.MeasureString(" ").X;
      Color color = baseColor;
      float num2 = 0.0f;
      for (int index1 = 0; index1 < snippets.Length; ++index1)
      {
        TextSnippet snippet = snippets[index1];
        snippet.Update();
        if (!ignoreColors)
          color = snippet.GetVisibleColor();
        float scale = snippet.Scale;
        Vector2 size;
        if (snippet.UniqueDraw(false, out size, spriteBatch, vector2_1, color, scale))
        {
          if (vec.Between(vector2_1, Vector2.op_Addition(vector2_1, size)))
            num1 = index1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @vector2_1;
          // ISSUE: explicit reference operation
          double num3 = (^local).X + size.X * baseScale.X * (double) scale;
          // ISSUE: explicit reference operation
          (^local).X = (__Null) num3;
          vector2_2.X = (__Null) (double) Math.Max((float) vector2_2.X, (float) vector2_1.X);
        }
        else
        {
          string[] strArray1 = snippet.Text.Split('\n');
          foreach (string str in strArray1)
          {
            char[] chArray = new char[1]{ ' ' };
            string[] strArray2 = str.Split(chArray);
            for (int index2 = 0; index2 < strArray2.Length; ++index2)
            {
              if (index2 != 0)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                Vector2& local = @vector2_1;
                // ISSUE: explicit reference operation
                double num3 = (^local).X + (double) x * baseScale.X * (double) scale;
                // ISSUE: explicit reference operation
                (^local).X = (__Null) num3;
              }
              if ((double) maxWidth > 0.0)
              {
                float num3 = (float) (font.MeasureString(strArray2[index2]).X * baseScale.X) * scale;
                if (vector2_1.X - position.X + (double) num3 > (double) maxWidth)
                {
                  vector2_1.X = position.X;
                  // ISSUE: explicit reference operation
                  // ISSUE: variable of a reference type
                  Vector2& local = @vector2_1;
                  // ISSUE: explicit reference operation
                  double num4 = (^local).Y + (double) font.get_LineSpacing() * (double) num2 * baseScale.Y;
                  // ISSUE: explicit reference operation
                  (^local).Y = (__Null) num4;
                  vector2_2.Y = (__Null) (double) Math.Max((float) vector2_2.Y, (float) vector2_1.Y);
                  num2 = 0.0f;
                }
              }
              if ((double) num2 < (double) scale)
                num2 = scale;
              DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, strArray2[index2], vector2_1, color, rotation, origin, Vector2.op_Multiply(Vector2.op_Multiply(baseScale, snippet.Scale), scale), (SpriteEffects) 0, 0.0f);
              Vector2 vector2_3 = font.MeasureString(strArray2[index2]);
              if (vec.Between(vector2_1, Vector2.op_Addition(vector2_1, vector2_3)))
                num1 = index1;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local1 = @vector2_1;
              // ISSUE: explicit reference operation
              double num5 = (^local1).X + vector2_3.X * baseScale.X * (double) scale;
              // ISSUE: explicit reference operation
              (^local1).X = (__Null) num5;
              vector2_2.X = (__Null) (double) Math.Max((float) vector2_2.X, (float) vector2_1.X);
            }
            if (strArray1.Length > 1)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local = @vector2_1;
              // ISSUE: explicit reference operation
              double num3 = (^local).Y + (double) font.get_LineSpacing() * (double) num2 * baseScale.Y;
              // ISSUE: explicit reference operation
              (^local).Y = (__Null) num3;
              vector2_1.X = position.X;
              vector2_2.Y = (__Null) (double) Math.Max((float) vector2_2.Y, (float) vector2_1.Y);
              num2 = 0.0f;
            }
          }
        }
      }
      hoveredSnippet = num1;
      return vector2_2;
    }

    public static Vector2 DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, float rotation, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth = -1f, float spread = 2f)
    {
      ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, Color.get_Black(), rotation, origin, baseScale, maxWidth, spread);
      return ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, Color.get_White(), rotation, origin, baseScale, out hoveredSnippet, maxWidth, false);
    }

    public static void DrawColorCodedStringShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
    {
      for (int index = 0; index < ChatManager.ShadowDirections.Length; ++index)
        ChatManager.DrawColorCodedString(spriteBatch, font, text, Vector2.op_Addition(position, Vector2.op_Multiply(ChatManager.ShadowDirections[index], spread)), baseColor, rotation, origin, baseScale, maxWidth, true);
    }

    public static Vector2 DrawColorCodedString(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, bool ignoreColors = false)
    {
      Vector2 vector2_1 = position;
      Vector2 vector2_2 = vector2_1;
      string[] strArray1 = text.Split('\n');
      float x = (float) font.MeasureString(" ").X;
      Color color = baseColor;
      float num1 = 1f;
      float num2 = 0.0f;
      foreach (string str1 in strArray1)
      {
        char[] chArray = new char[1]{ ':' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (str2.StartsWith("sss"))
          {
            if (str2.StartsWith("sss1"))
            {
              if (!ignoreColors)
                color = Color.get_Red();
            }
            else if (str2.StartsWith("sss2"))
            {
              if (!ignoreColors)
                color = Color.get_Blue();
            }
            else if (str2.StartsWith("sssr") && !ignoreColors)
              color = Color.get_White();
          }
          else
          {
            string[] strArray2 = str2.Split(' ');
            for (int index = 0; index < strArray2.Length; ++index)
            {
              if (index != 0)
              {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                Vector2& local = @vector2_1;
                // ISSUE: explicit reference operation
                double num3 = (^local).X + (double) x * baseScale.X * (double) num1;
                // ISSUE: explicit reference operation
                (^local).X = (__Null) num3;
              }
              if ((double) maxWidth > 0.0)
              {
                float num3 = (float) (font.MeasureString(strArray2[index]).X * baseScale.X) * num1;
                if (vector2_1.X - position.X + (double) num3 > (double) maxWidth)
                {
                  vector2_1.X = position.X;
                  // ISSUE: explicit reference operation
                  // ISSUE: variable of a reference type
                  Vector2& local = @vector2_1;
                  // ISSUE: explicit reference operation
                  double num4 = (^local).Y + (double) font.get_LineSpacing() * (double) num2 * baseScale.Y;
                  // ISSUE: explicit reference operation
                  (^local).Y = (__Null) num4;
                  vector2_2.Y = (__Null) (double) Math.Max((float) vector2_2.Y, (float) vector2_1.Y);
                  num2 = 0.0f;
                }
              }
              if ((double) num2 < (double) num1)
                num2 = num1;
              DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, strArray2[index], vector2_1, color, rotation, origin, Vector2.op_Multiply(baseScale, num1), (SpriteEffects) 0, 0.0f);
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local1 = @vector2_1;
              // ISSUE: explicit reference operation
              double num5 = (^local1).X + font.MeasureString(strArray2[index]).X * baseScale.X * (double) num1;
              // ISSUE: explicit reference operation
              (^local1).X = (__Null) num5;
              vector2_2.X = (__Null) (double) Math.Max((float) vector2_2.X, (float) vector2_1.X);
            }
          }
        }
        vector2_1.X = position.X;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @vector2_1;
        // ISSUE: explicit reference operation
        double num6 = (^local2).Y + (double) font.get_LineSpacing() * (double) num2 * baseScale.Y;
        // ISSUE: explicit reference operation
        (^local2).Y = (__Null) num6;
        vector2_2.Y = (__Null) (double) Math.Max((float) vector2_2.Y, (float) vector2_1.Y);
        num2 = 0.0f;
      }
      return vector2_2;
    }

    public static Vector2 DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
    {
      TextSnippet[] array = ChatManager.ParseMessage(text, baseColor).ToArray();
      ChatManager.ConvertNormalSnippets(array);
      ChatManager.DrawColorCodedStringShadow(spriteBatch, font, array, position, Color.get_Black(), rotation, origin, baseScale, maxWidth, spread);
      int hoveredSnippet;
      return ChatManager.DrawColorCodedString(spriteBatch, font, array, position, Color.get_White(), rotation, origin, baseScale, out hoveredSnippet, maxWidth, false);
    }

    public static class Regexes
    {
      public static readonly Regex Format = new Regex("(?<!\\\\)\\[(?<tag>[a-zA-Z]{1,10})(\\/(?<options>[^:]+))?:(?<text>.+?)(?<!\\\\)\\]", RegexOptions.Compiled);
    }
  }
}
