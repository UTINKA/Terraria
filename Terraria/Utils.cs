// Decompiled with JetBrains decompiler
// Type: Terraria.Utils
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.DataStructures;
using Terraria.UI.Chat;
using Terraria.Utilities;

namespace Terraria
{
  public static class Utils
  {
    public static Dictionary<DynamicSpriteFont, float[]> charLengths = new Dictionary<DynamicSpriteFont, float[]>();
    public const long MaxCoins = 999999999;
    private const ulong RANDOM_MULTIPLIER = 25214903917;
    private const ulong RANDOM_ADD = 11;
    private const ulong RANDOM_MASK = 281474976710655;

    public static Color ColorLerp_BlackToWhite(float percent)
    {
      return Color.Lerp(Color.get_Black(), Color.get_White(), percent);
    }

    public static Vector2 Round(Vector2 input)
    {
      return new Vector2((float) Math.Round((double) input.X), (float) Math.Round((double) input.Y));
    }

    public static bool IsPowerOfTwo(int x)
    {
      if (x != 0)
        return (x & x - 1) == 0;
      return false;
    }

    public static float SmoothStep(float min, float max, float x)
    {
      return MathHelper.Clamp((float) (((double) x - (double) min) / ((double) max - (double) min)), 0.0f, 1f);
    }

    public static Dictionary<string, string> ParseArguements(string[] args)
    {
      string str1 = (string) null;
      string str2 = "";
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      string str3;
      for (int index = 0; index < args.Length; ++index)
      {
        if (args[index].Length != 0)
        {
          if ((int) args[index][0] == 45 || (int) args[index][0] == 43)
          {
            if (str1 != null)
            {
              dictionary.Add(str1.ToLower(), str2);
              str3 = "";
            }
            str1 = args[index];
            str2 = "";
          }
          else
          {
            if (str2 != "")
              str2 += " ";
            str2 += args[index];
          }
        }
      }
      if (str1 != null)
      {
        dictionary.Add(str1.ToLower(), str2);
        str3 = "";
      }
      return dictionary;
    }

    public static void Swap<T>(ref T t1, ref T t2)
    {
      T obj = t1;
      t1 = t2;
      t2 = obj;
    }

    public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
    {
      if (value.CompareTo(max) > 0)
        return max;
      if (value.CompareTo(min) < 0)
        return min;
      return value;
    }

    public static float InverseLerp(float from, float to, float t, bool clamped = false)
    {
      if (clamped)
      {
        if ((double) from < (double) to)
        {
          if ((double) t < (double) from)
            return 0.0f;
          if ((double) t > (double) to)
            return 1f;
        }
        else
        {
          if ((double) t < (double) to)
            return 1f;
          if ((double) t > (double) from)
            return 0.0f;
        }
      }
      return (float) (((double) t - (double) from) / ((double) to - (double) from));
    }

    public static string[] ConvertMonoArgsToDotNet(string[] brokenArgs)
    {
      ArrayList arrayList = new ArrayList();
      string str = "";
      for (int index = 0; index < brokenArgs.Length; ++index)
      {
        if (brokenArgs[index].StartsWith("-"))
        {
          if (str != "")
          {
            arrayList.Add((object) str);
            str = "";
          }
          else
            arrayList.Add((object) "");
          arrayList.Add((object) brokenArgs[index]);
        }
        else
        {
          if (str != "")
            str += " ";
          str += brokenArgs[index];
        }
      }
      arrayList.Add((object) str);
      string[] strArray = new string[arrayList.Count];
      arrayList.CopyTo((Array) strArray);
      return strArray;
    }

    public static List<List<TextSnippet>> WordwrapStringSmart(string text, Color c, DynamicSpriteFont font, int maxWidth, int maxLines)
    {
      TextSnippet[] array = ChatManager.ParseMessage(text, c).ToArray();
      List<List<TextSnippet>> textSnippetListList = new List<List<TextSnippet>>();
      List<TextSnippet> textSnippetList1 = new List<TextSnippet>();
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        TextSnippet textSnippet = array[index1];
        string[] strArray = textSnippet.Text.Split('\n');
        for (int index2 = 0; index2 < strArray.Length - 1; ++index2)
        {
          textSnippetList1.Add(textSnippet.CopyMorph(strArray[index2]));
          textSnippetListList.Add(textSnippetList1);
          textSnippetList1 = new List<TextSnippet>();
        }
        textSnippetList1.Add(textSnippet.CopyMorph(strArray[strArray.Length - 1]));
      }
      textSnippetListList.Add(textSnippetList1);
      if (maxWidth != -1)
      {
        for (int index1 = 0; index1 < textSnippetListList.Count; ++index1)
        {
          List<TextSnippet> textSnippetList2 = textSnippetListList[index1];
          float num1 = 0.0f;
          for (int index2 = 0; index2 < textSnippetList2.Count; ++index2)
          {
            float stringLength = textSnippetList2[index2].GetStringLength(font);
            if ((double) stringLength + (double) num1 > (double) maxWidth)
            {
              int num2 = maxWidth - (int) num1;
              if ((double) num1 > 0.0)
                num2 -= 16;
              int num3 = Math.Min(textSnippetList2[index2].Text.Length, num2 / 8);
              if (num3 < 0)
                num3 = 0;
              string[] strArray = textSnippetList2[index2].Text.Split(' ');
              int num4 = num3;
              if (strArray.Length > 1)
              {
                num4 = 0;
                for (int index3 = 0; index3 < strArray.Length && num4 + strArray[index3].Length <= num3; ++index3)
                  num4 += strArray[index3].Length + 1;
                if (num4 > num3)
                  num4 = num3;
              }
              string newText1 = textSnippetList2[index2].Text.Substring(0, num4);
              string newText2 = textSnippetList2[index2].Text.Substring(num4);
              List<TextSnippet> textSnippetList3 = new List<TextSnippet>()
              {
                textSnippetList2[index2].CopyMorph(newText2)
              };
              for (int index3 = index2 + 1; index3 < textSnippetList2.Count; ++index3)
                textSnippetList3.Add(textSnippetList2[index3]);
              textSnippetList2[index2] = textSnippetList2[index2].CopyMorph(newText1);
              textSnippetListList[index1] = textSnippetListList[index1].Take<TextSnippet>(index2 + 1).ToList<TextSnippet>();
              textSnippetListList.Insert(index1 + 1, textSnippetList3);
              break;
            }
            num1 += stringLength;
          }
        }
      }
      if (maxLines != -1)
      {
        while (textSnippetListList.Count > 10)
          textSnippetListList.RemoveAt(10);
      }
      return textSnippetListList;
    }

    public static string[] WordwrapString(string text, DynamicSpriteFont font, int maxWidth, int maxLines, out int lineAmount)
    {
      string[] strArray1 = new string[maxLines];
      int index1 = 0;
      List<string> stringList1 = new List<string>((IEnumerable<string>) text.Split('\n'));
      List<string> stringList2 = new List<string>((IEnumerable<string>) stringList1[0].Split(' '));
      for (int index2 = 1; index2 < stringList1.Count; ++index2)
      {
        stringList2.Add("\n");
        stringList2.AddRange((IEnumerable<string>) stringList1[index2].Split(' '));
      }
      bool flag = true;
      while (stringList2.Count > 0)
      {
        string str1 = stringList2[0];
        string str2 = " ";
        if (stringList2.Count == 1)
          str2 = "";
        if (str1 == "\n")
        {
          string[] strArray2;
          int index2;
          string str3 = (strArray2 = strArray1)[(IntPtr) (index2 = index1++)] + str1;
          strArray2[index2] = str3;
          if (index1 < maxLines)
            stringList2.RemoveAt(0);
          else
            break;
        }
        else if (flag)
        {
          if (font.MeasureString(str1).X > (double) maxWidth)
          {
            string str3 = string.Concat((object) str1[0]);
            int startIndex = 1;
            while (font.MeasureString(str3 + (object) str1[startIndex] + (object) '-').X <= (double) maxWidth)
              str3 += (string) (object) str1[startIndex++];
            string str4 = str3 + (object) '-';
            strArray1[index1++] = str4 + " ";
            if (index1 < maxLines)
            {
              stringList2.RemoveAt(0);
              stringList2.Insert(0, str1.Substring(startIndex));
            }
            else
              break;
          }
          else
          {
            string[] strArray2;
            IntPtr index2;
            (strArray2 = strArray1)[(int) (index2 = (IntPtr) index1)] = strArray2[index2] + str1 + str2;
            flag = false;
            stringList2.RemoveAt(0);
          }
        }
        else if (font.MeasureString(strArray1[index1] + str1).X > (double) maxWidth)
        {
          ++index1;
          if (index1 < maxLines)
            flag = true;
          else
            break;
        }
        else
        {
          string[] strArray2;
          IntPtr index2;
          (strArray2 = strArray1)[(int) (index2 = (IntPtr) index1)] = strArray2[index2] + str1 + str2;
          flag = false;
          stringList2.RemoveAt(0);
        }
      }
      lineAmount = index1;
      if (lineAmount == maxLines)
        --lineAmount;
      return strArray1;
    }

    public static Rectangle CenteredRectangle(Vector2 center, Vector2 size)
    {
      return new Rectangle((int) (center.X - size.X / 2.0), (int) (center.Y - size.Y / 2.0), (int) size.X, (int) size.Y);
    }

    public static Vector2 Vector2FromElipse(Vector2 angleVector, Vector2 elipseSizes)
    {
      if (Vector2.op_Equality(elipseSizes, Vector2.get_Zero()) || Vector2.op_Equality(angleVector, Vector2.get_Zero()))
        return Vector2.get_Zero();
      // ISSUE: explicit reference operation
      ((Vector2) @angleVector).Normalize();
      Vector2 vector2 = Vector2.op_Division(Vector2.get_One(), Vector2.Normalize(elipseSizes));
      angleVector = Vector2.op_Multiply(angleVector, vector2);
      // ISSUE: explicit reference operation
      ((Vector2) @angleVector).Normalize();
      return Vector2.op_Division(Vector2.op_Multiply(angleVector, elipseSizes), 2f);
    }

    public static bool FloatIntersect(float r1StartX, float r1StartY, float r1Width, float r1Height, float r2StartX, float r2StartY, float r2Width, float r2Height)
    {
      return (double) r1StartX <= (double) r2StartX + (double) r2Width && (double) r1StartY <= (double) r2StartY + (double) r2Height && ((double) r1StartX + (double) r1Width >= (double) r2StartX && (double) r1StartY + (double) r1Height >= (double) r2StartY);
    }

    public static long CoinsCount(out bool overFlowing, Item[] inv, params int[] ignoreSlots)
    {
      List<int> intList = new List<int>((IEnumerable<int>) ignoreSlots);
      long num = 0;
      for (int index = 0; index < inv.Length; ++index)
      {
        if (!intList.Contains(index))
        {
          switch (inv[index].type)
          {
            case 71:
              num += (long) inv[index].stack;
              break;
            case 72:
              num += (long) (inv[index].stack * 100);
              break;
            case 73:
              num += (long) (inv[index].stack * 10000);
              break;
            case 74:
              num += (long) (inv[index].stack * 1000000);
              break;
          }
          if (num >= 999999999L)
          {
            overFlowing = true;
            return 999999999;
          }
        }
      }
      overFlowing = false;
      return num;
    }

    public static int[] CoinsSplit(long count)
    {
      int[] numArray = new int[4];
      long num1 = 0;
      long num2 = 1000000;
      for (int index = 3; index >= 0; --index)
      {
        numArray[index] = (int) ((count - num1) / num2);
        num1 += (long) numArray[index] * num2;
        num2 /= 100L;
      }
      return numArray;
    }

    public static long CoinsCombineStacks(out bool overFlowing, params long[] coinCounts)
    {
      long num = 0;
      foreach (long coinCount in coinCounts)
      {
        num += coinCount;
        if (num >= 999999999L)
        {
          overFlowing = true;
          return 999999999;
        }
      }
      overFlowing = false;
      return num;
    }

    public static void PoofOfSmoke(Vector2 position)
    {
      int num = Main.rand.Next(3, 7);
      for (int index1 = 0; index1 < num; ++index1)
      {
        int index2 = Gore.NewGore(position, Vector2.op_Multiply(Vector2.op_Multiply((Main.rand.NextFloat() * 6.283185f).ToRotationVector2(), new Vector2(2f, 0.7f)), 0.7f), Main.rand.Next(11, 14), 1f);
        Main.gore[index2].scale = 0.7f;
      }
      for (int index = 0; index < 10; ++index)
      {
        Dust dust1 = Main.dust[Dust.NewDust(position, 14, 14, 16, 0.0f, 0.0f, 100, (Color) null, 1.5f)];
        Dust dust2 = dust1;
        Vector2 vector2 = Vector2.op_Addition(dust2.position, new Vector2(5f));
        dust2.position = vector2;
        dust1.velocity = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply((Main.rand.NextFloat() * 6.283185f).ToRotationVector2(), new Vector2(2f, 0.7f)), 0.7f), (float) (0.5 + 0.5 * (double) Main.rand.NextFloat()));
      }
    }

    public static byte[] ToByteArray(this string str)
    {
      byte[] numArray = new byte[str.Length * 2];
      Buffer.BlockCopy((Array) str.ToCharArray(), 0, (Array) numArray, 0, numArray.Length);
      return numArray;
    }

    public static float NextFloat(this UnifiedRandom r)
    {
      return (float) r.NextDouble();
    }

    public static float NextFloatDirection(this UnifiedRandom r)
    {
      return (float) (r.NextDouble() * 2.0 - 1.0);
    }

    public static Vector2 NextVector2Square(this UnifiedRandom r, float min, float max)
    {
      return new Vector2((max - min) * (float) r.NextDouble() + min, (max - min) * (float) r.NextDouble() + min);
    }

    public static Vector2 NextVector2Unit(this UnifiedRandom r, float startRotation = 0.0f, float rotationRange = 6.283185f)
    {
      return (startRotation + rotationRange * r.NextFloat()).ToRotationVector2();
    }

    public static Vector2 NextVector2Circular(this UnifiedRandom r, float circleHalfWidth, float circleHalfHeight)
    {
      return Vector2.op_Multiply(Vector2.op_Multiply(r.NextVector2Unit(0.0f, 6.283185f), new Vector2(circleHalfWidth, circleHalfHeight)), r.NextFloat());
    }

    public static Vector2 NextVector2CircularEdge(this UnifiedRandom r, float circleHalfWidth, float circleHalfHeight)
    {
      return Vector2.op_Multiply(r.NextVector2Unit(0.0f, 6.283185f), new Vector2(circleHalfWidth, circleHalfHeight));
    }

    public static Rectangle Frame(this Texture2D tex, int horizontalFrames = 1, int verticalFrames = 1, int frameX = 0, int frameY = 0)
    {
      int num1 = tex.get_Width() / horizontalFrames;
      int num2 = tex.get_Height() / verticalFrames;
      return new Rectangle(num1 * frameX, num2 * frameY, num1, num2);
    }

    public static Vector2 OriginFlip(this Rectangle rect, Vector2 origin, SpriteEffects effects)
    {
      if (((Enum) (object) effects).HasFlag((Enum) (object) (SpriteEffects) 1))
        origin.X = (__Null) ((double) (float) rect.Width - origin.X);
      if (((Enum) (object) effects).HasFlag((Enum) (object) (SpriteEffects) 2))
        origin.Y = (__Null) ((double) (float) rect.Height - origin.Y);
      return origin;
    }

    public static Vector2 Size(this Texture2D tex)
    {
      return new Vector2((float) tex.get_Width(), (float) tex.get_Height());
    }

    public static void WriteRGB(this BinaryWriter bb, Color c)
    {
      // ISSUE: explicit reference operation
      bb.Write(((Color) @c).get_R());
      // ISSUE: explicit reference operation
      bb.Write(((Color) @c).get_G());
      // ISSUE: explicit reference operation
      bb.Write(((Color) @c).get_B());
    }

    public static void WriteVector2(this BinaryWriter bb, Vector2 v)
    {
      bb.Write((float) v.X);
      bb.Write((float) v.Y);
    }

    public static void WritePackedVector2(this BinaryWriter bb, Vector2 v)
    {
      HalfVector2 halfVector2;
      // ISSUE: explicit reference operation
      ((HalfVector2) @halfVector2).\u002Ector((float) v.X, (float) v.Y);
      // ISSUE: explicit reference operation
      bb.Write(((HalfVector2) @halfVector2).get_PackedValue());
    }

    public static Color ReadRGB(this BinaryReader bb)
    {
      return new Color((int) bb.ReadByte(), (int) bb.ReadByte(), (int) bb.ReadByte());
    }

    public static Vector2 ReadVector2(this BinaryReader bb)
    {
      return new Vector2(bb.ReadSingle(), bb.ReadSingle());
    }

    public static Vector2 ReadPackedVector2(this BinaryReader bb)
    {
      HalfVector2 halfVector2 = (HalfVector2) null;
      // ISSUE: explicit reference operation
      ((HalfVector2) @halfVector2).set_PackedValue(bb.ReadUInt32());
      // ISSUE: explicit reference operation
      return ((HalfVector2) @halfVector2).ToVector2();
    }

    public static Vector2 Left(this Rectangle r)
    {
      return new Vector2((float) r.X, (float) (r.Y + r.Height / 2));
    }

    public static Vector2 Right(this Rectangle r)
    {
      return new Vector2((float) (r.X + r.Width), (float) (r.Y + r.Height / 2));
    }

    public static Vector2 Top(this Rectangle r)
    {
      return new Vector2((float) (r.X + r.Width / 2), (float) r.Y);
    }

    public static Vector2 Bottom(this Rectangle r)
    {
      return new Vector2((float) (r.X + r.Width / 2), (float) (r.Y + r.Height));
    }

    public static Vector2 TopLeft(this Rectangle r)
    {
      return new Vector2((float) r.X, (float) r.Y);
    }

    public static Vector2 TopRight(this Rectangle r)
    {
      return new Vector2((float) (r.X + r.Width), (float) r.Y);
    }

    public static Vector2 BottomLeft(this Rectangle r)
    {
      return new Vector2((float) r.X, (float) (r.Y + r.Height));
    }

    public static Vector2 BottomRight(this Rectangle r)
    {
      return new Vector2((float) (r.X + r.Width), (float) (r.Y + r.Height));
    }

    public static Vector2 Center(this Rectangle r)
    {
      return new Vector2((float) (r.X + r.Width / 2), (float) (r.Y + r.Height / 2));
    }

    public static Vector2 Size(this Rectangle r)
    {
      return new Vector2((float) r.Width, (float) r.Height);
    }

    public static float Distance(this Rectangle r, Vector2 point)
    {
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      if (Utils.FloatIntersect((float) ((Rectangle) @r).get_Left(), (float) ((Rectangle) @r).get_Top(), (float) r.Width, (float) r.Height, (float) point.X, (float) point.Y, 0.0f, 0.0f))
        return 0.0f;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      if (point.X >= (double) ((Rectangle) @r).get_Left() && point.X <= (double) ((Rectangle) @r).get_Right())
      {
        // ISSUE: explicit reference operation
        if (point.Y < (double) ((Rectangle) @r).get_Top())
        {
          // ISSUE: explicit reference operation
          return (float) ((Rectangle) @r).get_Top() - (float) point.Y;
        }
        // ISSUE: explicit reference operation
        return (float) point.Y - (float) ((Rectangle) @r).get_Bottom();
      }
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      if (point.Y >= (double) ((Rectangle) @r).get_Top() && point.Y <= (double) ((Rectangle) @r).get_Bottom())
      {
        // ISSUE: explicit reference operation
        if (point.X < (double) ((Rectangle) @r).get_Left())
        {
          // ISSUE: explicit reference operation
          return (float) ((Rectangle) @r).get_Left() - (float) point.X;
        }
        // ISSUE: explicit reference operation
        return (float) point.X - (float) ((Rectangle) @r).get_Right();
      }
      // ISSUE: explicit reference operation
      if (point.X < (double) ((Rectangle) @r).get_Left())
      {
        // ISSUE: explicit reference operation
        if (point.Y < (double) ((Rectangle) @r).get_Top())
          return Vector2.Distance(point, r.TopLeft());
        return Vector2.Distance(point, r.BottomLeft());
      }
      // ISSUE: explicit reference operation
      if (point.Y < (double) ((Rectangle) @r).get_Top())
        return Vector2.Distance(point, r.TopRight());
      return Vector2.Distance(point, r.BottomRight());
    }

    public static float ToRotation(this Vector2 v)
    {
      return (float) Math.Atan2((double) v.Y, (double) v.X);
    }

    public static Vector2 ToRotationVector2(this float f)
    {
      return new Vector2((float) Math.Cos((double) f), (float) Math.Sin((double) f));
    }

    public static Vector2 RotatedBy(this Vector2 spinningpoint, double radians, Vector2 center = null)
    {
      float num1 = (float) Math.Cos(radians);
      float num2 = (float) Math.Sin(radians);
      Vector2 vector2_1 = Vector2.op_Subtraction(spinningpoint, center);
      Vector2 vector2_2 = center;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local1 = @vector2_2;
      // ISSUE: explicit reference operation
      double num3 = (^local1).X + (vector2_1.X * (double) num1 - vector2_1.Y * (double) num2);
      // ISSUE: explicit reference operation
      (^local1).X = (__Null) num3;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local2 = @vector2_2;
      // ISSUE: explicit reference operation
      double num4 = (^local2).Y + (vector2_1.X * (double) num2 + vector2_1.Y * (double) num1);
      // ISSUE: explicit reference operation
      (^local2).Y = (__Null) num4;
      return vector2_2;
    }

    public static Vector2 RotatedByRandom(this Vector2 spinninpoint, double maxRadians)
    {
      return spinninpoint.RotatedBy(Main.rand.NextDouble() * maxRadians - Main.rand.NextDouble() * maxRadians, (Vector2) null);
    }

    public static Vector2 Floor(this Vector2 vec)
    {
      vec.X = (__Null) (double) (int) vec.X;
      vec.Y = (__Null) (double) (int) vec.Y;
      return vec;
    }

    public static bool HasNaNs(this Vector2 vec)
    {
      if (!float.IsNaN((float) vec.X))
        return float.IsNaN((float) vec.Y);
      return true;
    }

    public static bool Between(this Vector2 vec, Vector2 minimum, Vector2 maximum)
    {
      if (vec.X >= minimum.X && vec.X <= maximum.X && vec.Y >= minimum.Y)
        return vec.Y <= maximum.Y;
      return false;
    }

    public static Vector2 ToVector2(this Point p)
    {
      return new Vector2((float) p.X, (float) p.Y);
    }

    public static Vector2 ToWorldCoordinates(this Point p, float autoAddX = 8f, float autoAddY = 8f)
    {
      return Vector2.op_Addition(Vector2.op_Multiply(p.ToVector2(), 16f), new Vector2(autoAddX, autoAddY));
    }

    public static Point16 ToTileCoordinates16(this Vector2 vec)
    {
      return new Point16((int) vec.X >> 4, (int) vec.Y >> 4);
    }

    public static Point ToTileCoordinates(this Vector2 vec)
    {
      return new Point((int) vec.X >> 4, (int) vec.Y >> 4);
    }

    public static Point ToPoint(this Vector2 v)
    {
      return new Point((int) v.X, (int) v.Y);
    }

    public static Vector2 SafeNormalize(this Vector2 v, Vector2 defaultValue)
    {
      if (Vector2.op_Equality(v, Vector2.get_Zero()))
        return defaultValue;
      return Vector2.Normalize(v);
    }

    public static Vector2 ClosestPointOnLine(this Vector2 P, Vector2 A, Vector2 B)
    {
      Vector2 vector2_1 = Vector2.op_Subtraction(P, A);
      Vector2 vector2_2 = Vector2.op_Subtraction(B, A);
      // ISSUE: explicit reference operation
      float num1 = ((Vector2) @vector2_2).LengthSquared();
      float num2 = Vector2.Dot(vector2_1, vector2_2) / num1;
      if ((double) num2 < 0.0)
        return A;
      if ((double) num2 > 1.0)
        return B;
      return Vector2.op_Addition(A, Vector2.op_Multiply(vector2_2, num2));
    }

    public static bool RectangleLineCollision(Vector2 rectTopLeft, Vector2 rectBottomRight, Vector2 lineStart, Vector2 lineEnd)
    {
      if (lineStart.Between(rectTopLeft, rectBottomRight) || lineEnd.Between(rectTopLeft, rectBottomRight))
        return true;
      Vector2 P;
      // ISSUE: explicit reference operation
      ((Vector2) @P).\u002Ector((float) rectBottomRight.X, (float) rectTopLeft.Y);
      Vector2 vector2;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2).\u002Ector((float) rectTopLeft.X, (float) rectBottomRight.Y);
      Vector2[] vector2Array = new Vector2[4]
      {
        rectTopLeft.ClosestPointOnLine(lineStart, lineEnd),
        P.ClosestPointOnLine(lineStart, lineEnd),
        vector2.ClosestPointOnLine(lineStart, lineEnd),
        rectBottomRight.ClosestPointOnLine(lineStart, lineEnd)
      };
      for (int index = 0; index < vector2Array.Length; ++index)
      {
        if (vector2Array[0].Between(rectTopLeft, vector2))
          return true;
      }
      return false;
    }

    public static Vector2 RotateRandom(this Vector2 spinninpoint, double maxRadians)
    {
      return spinninpoint.RotatedBy(Main.rand.NextDouble() * maxRadians - Main.rand.NextDouble() * maxRadians, (Vector2) null);
    }

    public static Vector2 XY(this Vector4 vec)
    {
      return new Vector2((float) vec.X, (float) vec.Y);
    }

    public static Vector2 ZW(this Vector4 vec)
    {
      return new Vector2((float) vec.Z, (float) vec.W);
    }

    public static Vector3 XZW(this Vector4 vec)
    {
      return new Vector3((float) vec.X, (float) vec.Z, (float) vec.W);
    }

    public static Vector3 YZW(this Vector4 vec)
    {
      return new Vector3((float) vec.Y, (float) vec.Z, (float) vec.W);
    }

    public static Color MultiplyRGB(this Color firstColor, Color secondColor)
    {
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      return new Color((int) (byte) ((double) ((int) ((Color) @firstColor).get_R() * (int) ((Color) @secondColor).get_R()) / (double) byte.MaxValue), (int) (byte) ((double) ((int) ((Color) @firstColor).get_G() * (int) ((Color) @secondColor).get_G()) / (double) byte.MaxValue), (int) (byte) ((double) ((int) ((Color) @firstColor).get_B() * (int) ((Color) @secondColor).get_B()) / (double) byte.MaxValue));
    }

    public static Color MultiplyRGBA(this Color firstColor, Color secondColor)
    {
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      return new Color((int) (byte) ((double) ((int) ((Color) @firstColor).get_R() * (int) ((Color) @secondColor).get_R()) / (double) byte.MaxValue), (int) (byte) ((double) ((int) ((Color) @firstColor).get_G() * (int) ((Color) @secondColor).get_G()) / (double) byte.MaxValue), (int) (byte) ((double) ((int) ((Color) @firstColor).get_B() * (int) ((Color) @secondColor).get_B()) / (double) byte.MaxValue), (int) (byte) ((double) ((int) ((Color) @firstColor).get_A() * (int) ((Color) @secondColor).get_A()) / (double) byte.MaxValue));
    }

    public static string Hex3(this Color color)
    {
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      return (((Color) @color).get_R().ToString("X2") + ((Color) @color).get_G().ToString("X2") + ((Color) @color).get_B().ToString("X2")).ToLower();
    }

    public static string Hex4(this Color color)
    {
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      return (((Color) @color).get_R().ToString("X2") + ((Color) @color).get_G().ToString("X2") + ((Color) @color).get_B().ToString("X2") + ((Color) @color).get_A().ToString("X2")).ToLower();
    }

    public static int ToDirectionInt(this bool value)
    {
      return !value ? -1 : 1;
    }

    public static int ToInt(this bool value)
    {
      return !value ? 0 : 1;
    }

    public static float AngleLerp(this float curAngle, float targetAngle, float amount)
    {
      float num1;
      if ((double) targetAngle < (double) curAngle)
      {
        float num2 = targetAngle + 6.283185f;
        num1 = (double) num2 - (double) curAngle > (double) curAngle - (double) targetAngle ? MathHelper.Lerp(curAngle, targetAngle, amount) : MathHelper.Lerp(curAngle, num2, amount);
      }
      else
      {
        if ((double) targetAngle <= (double) curAngle)
          return curAngle;
        float num2 = targetAngle - 6.283185f;
        num1 = (double) targetAngle - (double) curAngle > (double) curAngle - (double) num2 ? MathHelper.Lerp(curAngle, num2, amount) : MathHelper.Lerp(curAngle, targetAngle, amount);
      }
      return MathHelper.WrapAngle(num1);
    }

    public static float AngleTowards(this float curAngle, float targetAngle, float maxChange)
    {
      curAngle = MathHelper.WrapAngle(curAngle);
      targetAngle = MathHelper.WrapAngle(targetAngle);
      if ((double) curAngle < (double) targetAngle)
      {
        if ((double) targetAngle - (double) curAngle > 3.14159274101257)
          curAngle += 6.283185f;
      }
      else if ((double) curAngle - (double) targetAngle > 3.14159274101257)
        curAngle -= 6.283185f;
      curAngle += MathHelper.Clamp(targetAngle - curAngle, -maxChange, maxChange);
      return MathHelper.WrapAngle(curAngle);
    }

    public static bool deepCompare(this int[] firstArray, int[] secondArray)
    {
      if (firstArray == null && secondArray == null)
        return true;
      if (firstArray == null || secondArray == null || firstArray.Length != secondArray.Length)
        return false;
      for (int index = 0; index < firstArray.Length; ++index)
      {
        if (firstArray[index] != secondArray[index])
          return false;
      }
      return true;
    }

    public static bool PressingShift(this KeyboardState kb)
    {
      // ISSUE: explicit reference operation
      if (!((KeyboardState) @kb).IsKeyDown((Keys) 160))
      {
        // ISSUE: explicit reference operation
        return ((KeyboardState) @kb).IsKeyDown((Keys) 161);
      }
      return true;
    }

    public static bool PlotLine(Point16 p0, Point16 p1, Utils.PerLinePoint plot, bool jump = true)
    {
      return Utils.PlotLine((int) p0.X, (int) p0.Y, (int) p1.X, (int) p1.Y, plot, jump);
    }

    public static bool PlotLine(Point p0, Point p1, Utils.PerLinePoint plot, bool jump = true)
    {
      return Utils.PlotLine((int) p0.X, (int) p0.Y, (int) p1.X, (int) p1.Y, plot, jump);
    }

    private static bool PlotLine(int x0, int y0, int x1, int y1, Utils.PerLinePoint plot, bool jump = true)
    {
      if (x0 == x1 && y0 == y1)
        return plot(x0, y0);
      bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
      if (flag)
      {
        Utils.Swap<int>(ref x0, ref y0);
        Utils.Swap<int>(ref x1, ref y1);
      }
      int num1 = Math.Abs(x1 - x0);
      int num2 = Math.Abs(y1 - y0);
      int num3 = num1 / 2;
      int num4 = y0;
      int num5 = x0 < x1 ? 1 : -1;
      int num6 = y0 < y1 ? 1 : -1;
      int num7 = x0;
      while (num7 != x1)
      {
        if (flag)
        {
          if (!plot(num4, num7))
            return false;
        }
        else if (!plot(num7, num4))
          return false;
        num3 -= num2;
        if (num3 < 0)
        {
          num4 += num6;
          if (!jump)
          {
            if (flag)
            {
              if (!plot(num4, num7))
                return false;
            }
            else if (!plot(num7, num4))
              return false;
          }
          num3 += num1;
        }
        num7 += num5;
      }
      return true;
    }

    public static int RandomNext(ref ulong seed, int bits)
    {
      seed = Utils.RandomNextSeed(seed);
      return (int) (seed >> 48 - bits);
    }

    public static ulong RandomNextSeed(ulong seed)
    {
      return (ulong) ((long) seed * 25214903917L + 11L & 281474976710655L);
    }

    public static float RandomFloat(ref ulong seed)
    {
      return (float) Utils.RandomNext(ref seed, 24) / 1.677722E+07f;
    }

    public static int RandomInt(ref ulong seed, int max)
    {
      if ((max & -max) == max)
        return (int) ((long) max * (long) Utils.RandomNext(ref seed, 31) >> 31);
      int num1;
      int num2;
      do
      {
        num1 = Utils.RandomNext(ref seed, 31);
        num2 = num1 % max;
      }
      while (num1 - num2 + (max - 1) < 0);
      return num2;
    }

    public static int RandomInt(ref ulong seed, int min, int max)
    {
      return Utils.RandomInt(ref seed, max - min) + min;
    }

    public static bool PlotTileLine(Vector2 start, Vector2 end, float width, Utils.PerLinePoint plot)
    {
      float num = width / 2f;
      Vector2 vector2_1 = Vector2.op_Subtraction(end, start);
      // ISSUE: explicit reference operation
      Vector2 vector2_2 = Vector2.op_Division(vector2_1, ((Vector2) @vector2_1).Length());
      Vector2 vector2_3 = Vector2.op_Multiply(new Vector2((float) -vector2_2.Y, (float) vector2_2.X), num);
      Point tileCoordinates1 = Vector2.op_Subtraction(start, vector2_3).ToTileCoordinates();
      Point tileCoordinates2 = Vector2.op_Addition(start, vector2_3).ToTileCoordinates();
      Point tileCoordinates3 = start.ToTileCoordinates();
      Point tileCoordinates4 = end.ToTileCoordinates();
      Point lineMinOffset = new Point((int) (tileCoordinates1.X - tileCoordinates3.X), (int) (tileCoordinates1.Y - tileCoordinates3.Y));
      Point lineMaxOffset = new Point((int) (tileCoordinates2.X - tileCoordinates3.X), (int) (tileCoordinates2.Y - tileCoordinates3.Y));
      return Utils.PlotLine((int) tileCoordinates3.X, (int) tileCoordinates3.Y, (int) tileCoordinates4.X, (int) tileCoordinates4.Y, (Utils.PerLinePoint) ((x, y) => Utils.PlotLine(x + lineMinOffset.X, y + lineMinOffset.Y, x + lineMaxOffset.X, y + lineMaxOffset.Y, plot, false)), true);
    }

    public static bool PlotTileTale(Vector2 start, Vector2 end, float width, Utils.PerLinePoint plot)
    {
      float halfWidth = width / 2f;
      Vector2 vector2_1 = Vector2.op_Subtraction(end, start);
      // ISSUE: explicit reference operation
      Vector2 vector2_2 = Vector2.op_Division(vector2_1, ((Vector2) @vector2_1).Length());
      Vector2 perpOffset = new Vector2((float) -vector2_2.Y, (float) vector2_2.X);
      Point pointStart = start.ToTileCoordinates();
      Point tileCoordinates1 = end.ToTileCoordinates();
      int length = 0;
      Utils.PlotLine((int) pointStart.X, (int) pointStart.Y, (int) tileCoordinates1.X, (int) tileCoordinates1.Y, (Utils.PerLinePoint) ((x, y) =>
      {
        ++length;
        return true;
      }), true);
      --length;
      int curLength = 0;
      return Utils.PlotLine((int) pointStart.X, (int) pointStart.Y, (int) tileCoordinates1.X, (int) tileCoordinates1.Y, (Utils.PerLinePoint) ((x, y) =>
      {
        float num = (float) (1.0 - (double) curLength / (double) length);
        ++curLength;
        Point tileCoordinates2 = Vector2.op_Subtraction(start, Vector2.op_Multiply(Vector2.op_Multiply(perpOffset, halfWidth), num)).ToTileCoordinates();
        Point tileCoordinates3 = Vector2.op_Addition(start, Vector2.op_Multiply(Vector2.op_Multiply(perpOffset, halfWidth), num)).ToTileCoordinates();
        Point point1;
        // ISSUE: explicit reference operation
        ((Point) @point1).\u002Ector((int) (tileCoordinates2.X - pointStart.X), (int) (tileCoordinates2.Y - pointStart.Y));
        Point point2;
        // ISSUE: explicit reference operation
        ((Point) @point2).\u002Ector((int) (tileCoordinates3.X - pointStart.X), (int) (tileCoordinates3.Y - pointStart.Y));
        return Utils.PlotLine(x + point1.X, y + point1.Y, x + point2.X, y + point2.Y, plot, false);
      }), true);
    }

    public static int RandomConsecutive(double random, int odds)
    {
      return (int) Math.Log(1.0 - random, 1.0 / (double) odds);
    }

    public static Vector2 RandomVector2(UnifiedRandom random, float min, float max)
    {
      return new Vector2((max - min) * (float) random.NextDouble() + min, (max - min) * (float) random.NextDouble() + min);
    }

    public static bool IndexInRange<T>(this T[] t, int index)
    {
      if (index >= 0)
        return index < t.Length;
      return false;
    }

    public static bool IndexInRange<T>(this List<T> t, int index)
    {
      if (index >= 0)
        return index < t.Count;
      return false;
    }

    public static T SelectRandom<T>(UnifiedRandom random, params T[] choices)
    {
      return choices[random.Next(choices.Length)];
    }

    public static void DrawBorderStringFourWay(SpriteBatch sb, DynamicSpriteFont font, string text, float x, float y, Color textColor, Color borderColor, Vector2 origin, float scale = 1f)
    {
      Color color = borderColor;
      Vector2 zero = Vector2.get_Zero();
      for (int index = 0; index < 5; ++index)
      {
        switch (index)
        {
          case 0:
            zero.X = (__Null) ((double) x - 2.0);
            zero.Y = (__Null) (double) y;
            break;
          case 1:
            zero.X = (__Null) ((double) x + 2.0);
            zero.Y = (__Null) (double) y;
            break;
          case 2:
            zero.X = (__Null) (double) x;
            zero.Y = (__Null) ((double) y - 2.0);
            break;
          case 3:
            zero.X = (__Null) (double) x;
            zero.Y = (__Null) ((double) y + 2.0);
            break;
          default:
            zero.X = (__Null) (double) x;
            zero.Y = (__Null) (double) y;
            color = textColor;
            break;
        }
        DynamicSpriteFontExtensionMethods.DrawString(sb, font, text, zero, color, 0.0f, origin, scale, (SpriteEffects) 0, 0.0f);
      }
    }

    public static Vector2 DrawBorderString(SpriteBatch sb, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0.0f, float anchory = 0.0f, int maxCharactersDisplayed = -1)
    {
      if (maxCharactersDisplayed != -1 && text.Length > maxCharactersDisplayed)
        text.Substring(0, maxCharactersDisplayed);
      DynamicSpriteFont fontMouseText = Main.fontMouseText;
      Vector2 vector2 = fontMouseText.MeasureString(text);
      ChatManager.DrawColorCodedStringWithShadow(sb, fontMouseText, text, pos, color, 0.0f, Vector2.op_Multiply(new Vector2(anchorx, anchory), vector2), new Vector2(scale), -1f, 1.5f);
      return Vector2.op_Multiply(vector2, scale);
    }

    public static Vector2 DrawBorderStringBig(SpriteBatch spriteBatch, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0.0f, float anchory = 0.0f, int maxCharactersDisplayed = -1)
    {
      if (maxCharactersDisplayed != -1 && text.Length > maxCharactersDisplayed)
        text.Substring(0, maxCharactersDisplayed);
      DynamicSpriteFont fontDeathText = Main.fontDeathText;
      for (int index1 = -1; index1 < 2; ++index1)
      {
        for (int index2 = -1; index2 < 2; ++index2)
          DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, fontDeathText, text, Vector2.op_Addition(pos, new Vector2((float) index1, (float) index2)), Color.get_Black(), 0.0f, Vector2.op_Multiply(new Vector2(anchorx, anchory), fontDeathText.MeasureString(text)), scale, (SpriteEffects) 0, 0.0f);
      }
      DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, fontDeathText, text, pos, color, 0.0f, Vector2.op_Multiply(new Vector2(anchorx, anchory), fontDeathText.MeasureString(text)), scale, (SpriteEffects) 0, 0.0f);
      return Vector2.op_Multiply(fontDeathText.MeasureString(text), scale);
    }

    public static void DrawInvBG(SpriteBatch sb, Rectangle R, Color c = null)
    {
      Utils.DrawInvBG(sb, (int) R.X, (int) R.Y, (int) R.Width, (int) R.Height, c);
    }

    public static void DrawInvBG(SpriteBatch sb, float x, float y, float w, float h, Color c = null)
    {
      Utils.DrawInvBG(sb, (int) x, (int) y, (int) w, (int) h, c);
    }

    public static void DrawInvBG(SpriteBatch sb, int x, int y, int w, int h, Color c = null)
    {
      if (Color.op_Equality(c, (Color) null))
        c = Color.op_Multiply(new Color(63, 65, 151, (int) byte.MaxValue), 0.785f);
      Texture2D inventoryBack13Texture = Main.inventoryBack13Texture;
      if (w < 20)
        w = 20;
      if (h < 20)
        h = 20;
      sb.Draw(inventoryBack13Texture, new Rectangle(x, y, 10, 10), new Rectangle?(new Rectangle(0, 0, 10, 10)), c);
      sb.Draw(inventoryBack13Texture, new Rectangle(x + 10, y, w - 20, 10), new Rectangle?(new Rectangle(10, 0, 10, 10)), c);
      sb.Draw(inventoryBack13Texture, new Rectangle(x + w - 10, y, 10, 10), new Rectangle?(new Rectangle(inventoryBack13Texture.get_Width() - 10, 0, 10, 10)), c);
      sb.Draw(inventoryBack13Texture, new Rectangle(x, y + 10, 10, h - 20), new Rectangle?(new Rectangle(0, 10, 10, 10)), c);
      sb.Draw(inventoryBack13Texture, new Rectangle(x + 10, y + 10, w - 20, h - 20), new Rectangle?(new Rectangle(10, 10, 10, 10)), c);
      sb.Draw(inventoryBack13Texture, new Rectangle(x + w - 10, y + 10, 10, h - 20), new Rectangle?(new Rectangle(inventoryBack13Texture.get_Width() - 10, 10, 10, 10)), c);
      sb.Draw(inventoryBack13Texture, new Rectangle(x, y + h - 10, 10, 10), new Rectangle?(new Rectangle(0, inventoryBack13Texture.get_Height() - 10, 10, 10)), c);
      sb.Draw(inventoryBack13Texture, new Rectangle(x + 10, y + h - 10, w - 20, 10), new Rectangle?(new Rectangle(10, inventoryBack13Texture.get_Height() - 10, 10, 10)), c);
      sb.Draw(inventoryBack13Texture, new Rectangle(x + w - 10, y + h - 10, 10, 10), new Rectangle?(new Rectangle(inventoryBack13Texture.get_Width() - 10, inventoryBack13Texture.get_Height() - 10, 10, 10)), c);
    }

    public static void DrawSettingsPanel(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
    {
      Utils.DrawPanel(Main.settingsPanelTexture, 2, 0, spriteBatch, position, width, color);
    }

    public static void DrawSettings2Panel(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
    {
      Utils.DrawPanel(Main.settingsPanelTexture, 2, 0, spriteBatch, position, width, color);
    }

    public static void DrawPanel(Texture2D texture, int edgeWidth, int edgeShove, SpriteBatch spriteBatch, Vector2 position, float width, Color color)
    {
      spriteBatch.Draw(texture, position, new Rectangle?(new Rectangle(0, 0, edgeWidth, texture.get_Height())), color);
      spriteBatch.Draw(texture, new Vector2((float) position.X + (float) edgeWidth, (float) position.Y), new Rectangle?(new Rectangle(edgeWidth + edgeShove, 0, texture.get_Width() - (edgeWidth + edgeShove) * 2, texture.get_Height())), color, 0.0f, Vector2.get_Zero(), new Vector2((width - (float) (edgeWidth * 2)) / (float) (texture.get_Width() - (edgeWidth + edgeShove) * 2), 1f), (SpriteEffects) 0, 0.0f);
      spriteBatch.Draw(texture, new Vector2((float) position.X + width - (float) edgeWidth, (float) position.Y), new Rectangle?(new Rectangle(texture.get_Width() - edgeWidth, 0, edgeWidth, texture.get_Height())), color);
    }

    public static void DrawRectangle(SpriteBatch sb, Vector2 start, Vector2 end, Color colorStart, Color colorEnd, float width)
    {
      Utils.DrawLine(sb, start, new Vector2((float) start.X, (float) end.Y), colorStart, colorEnd, width);
      Utils.DrawLine(sb, start, new Vector2((float) end.X, (float) start.Y), colorStart, colorEnd, width);
      Utils.DrawLine(sb, end, new Vector2((float) start.X, (float) end.Y), colorStart, colorEnd, width);
      Utils.DrawLine(sb, end, new Vector2((float) end.X, (float) start.Y), colorStart, colorEnd, width);
    }

    public static void DrawLaser(SpriteBatch sb, Texture2D tex, Vector2 start, Vector2 end, Vector2 scale, Utils.LaserLineFraming framing)
    {
      Vector2 currentPosition1 = start;
      Vector2 vector2_1 = Vector2.Normalize(Vector2.op_Subtraction(end, start));
      Vector2 vector2_2 = Vector2.op_Subtraction(end, start);
      // ISSUE: explicit reference operation
      float distanceLeft1 = ((Vector2) @vector2_2).Length();
      float num1 = vector2_1.ToRotation() - 1.570796f;
      if (vector2_1.HasNaNs())
        return;
      float distanceCovered;
      Rectangle frame;
      Vector2 origin;
      Color color;
      framing(0, currentPosition1, distanceLeft1, (Rectangle) null, out distanceCovered, out frame, out origin, out color);
      sb.Draw(tex, currentPosition1, new Rectangle?(frame), color, num1, Vector2.op_Division(frame.Size(), 2f), scale, (SpriteEffects) 0, 0.0f);
      float distanceLeft2 = distanceLeft1 - distanceCovered * (float) scale.Y;
      Vector2 currentPosition2 = Vector2.op_Addition(currentPosition1, Vector2.op_Multiply(Vector2.op_Multiply(vector2_1, (float) frame.Height - (float) origin.Y), (float) scale.Y));
      if ((double) distanceLeft2 > 0.0)
      {
        float num2 = 0.0f;
        while ((double) num2 + 1.0 < (double) distanceLeft2)
        {
          framing(1, currentPosition2, distanceLeft2 - num2, frame, out distanceCovered, out frame, out origin, out color);
          if ((double) distanceLeft2 - (double) num2 < (double) (float) frame.Height)
          {
            distanceCovered *= (distanceLeft2 - num2) / (float) frame.Height;
            frame.Height = (__Null) (int) ((double) distanceLeft2 - (double) num2);
          }
          sb.Draw(tex, currentPosition2, new Rectangle?(frame), color, num1, origin, scale, (SpriteEffects) 0, 0.0f);
          num2 += distanceCovered * (float) scale.Y;
          currentPosition2 = Vector2.op_Addition(currentPosition2, Vector2.op_Multiply(Vector2.op_Multiply(vector2_1, distanceCovered), (float) scale.Y));
        }
      }
      framing(2, currentPosition2, distanceLeft2, (Rectangle) null, out distanceCovered, out frame, out origin, out color);
      sb.Draw(tex, currentPosition2, new Rectangle?(frame), color, num1, origin, scale, (SpriteEffects) 0, 0.0f);
    }

    public static void DrawLine(SpriteBatch spriteBatch, Point start, Point end, Color color)
    {
      Utils.DrawLine(spriteBatch, new Vector2((float) (start.X << 4), (float) (start.Y << 4)), new Vector2((float) (end.X << 4), (float) (end.Y << 4)), color);
    }

    public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
    {
      float num1 = Vector2.Distance(start, end);
      Vector2 v = Vector2.op_Division(Vector2.op_Subtraction(end, start), num1);
      Vector2 vector2 = start;
      Vector2 screenPosition = Main.screenPosition;
      float rotation = v.ToRotation();
      float num2 = 0.0f;
      while ((double) num2 <= (double) num1)
      {
        float num3 = num2 / num1;
        // ISSUE: explicit reference operation
        spriteBatch.Draw(Main.blackTileTexture, Vector2.op_Subtraction(vector2, screenPosition), new Rectangle?(), new Color(Vector4.op_Multiply(new Vector4(num3, num3, num3, 1f), ((Color) @color).ToVector4())), rotation, Vector2.get_Zero(), 0.25f, (SpriteEffects) 0, 0.0f);
        vector2 = Vector2.op_Addition(start, Vector2.op_Multiply(num2, v));
        num2 += 4f;
      }
    }

    public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color colorStart, Color colorEnd, float width)
    {
      float num1 = Vector2.Distance(start, end);
      Vector2 v = Vector2.op_Division(Vector2.op_Subtraction(end, start), num1);
      Vector2 vector2 = start;
      Vector2 screenPosition = Main.screenPosition;
      float rotation = v.ToRotation();
      float num2 = width / 16f;
      float num3 = 0.0f;
      while ((double) num3 <= (double) num1)
      {
        float num4 = num3 / num1;
        spriteBatch.Draw(Main.blackTileTexture, Vector2.op_Subtraction(vector2, screenPosition), new Rectangle?(), Color.Lerp(colorStart, colorEnd, num4), rotation, Vector2.get_Zero(), num2, (SpriteEffects) 0, 0.0f);
        vector2 = Vector2.op_Addition(start, Vector2.op_Multiply(num3, v));
        num3 += width;
      }
    }

    public static void DrawRect(SpriteBatch spriteBatch, Rectangle rect, Color color)
    {
      Utils.DrawRect(spriteBatch, new Point((int) rect.X, (int) rect.Y), new Point((int) (rect.X + rect.Width), (int) (rect.Y + rect.Height)), color);
    }

    public static void DrawRect(SpriteBatch spriteBatch, Point start, Point end, Color color)
    {
      Utils.DrawRect(spriteBatch, new Vector2((float) (start.X << 4), (float) (start.Y << 4)), new Vector2((float) ((end.X << 4) - 4), (float) ((end.Y << 4) - 4)), color);
    }

    public static void DrawRect(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
    {
      Utils.DrawLine(spriteBatch, start, new Vector2((float) start.X, (float) end.Y), color);
      Utils.DrawLine(spriteBatch, start, new Vector2((float) end.X, (float) start.Y), color);
      Utils.DrawLine(spriteBatch, end, new Vector2((float) start.X, (float) end.Y), color);
      Utils.DrawLine(spriteBatch, end, new Vector2((float) end.X, (float) start.Y), color);
    }

    public static void DrawRect(SpriteBatch spriteBatch, Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft, Color color)
    {
      Utils.DrawLine(spriteBatch, topLeft, topRight, color);
      Utils.DrawLine(spriteBatch, topRight, bottomRight, color);
      Utils.DrawLine(spriteBatch, bottomRight, bottomLeft, color);
      Utils.DrawLine(spriteBatch, bottomLeft, topLeft, color);
    }

    public static void DrawCursorSingle(SpriteBatch sb, Color color, float rot = float.NaN, float scale = 1f, Vector2 manualPosition = null, int cursorSlot = 0, int specialMode = 0)
    {
      bool flag1 = false;
      bool flag2 = true;
      bool flag3 = true;
      Vector2 zero = Vector2.get_Zero();
      Vector2 vector2_1;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).\u002Ector((float) Main.mouseX, (float) Main.mouseY);
      if (Vector2.op_Inequality(manualPosition, Vector2.get_Zero()))
        vector2_1 = manualPosition;
      if (float.IsNaN(rot))
      {
        rot = 0.0f;
      }
      else
      {
        flag1 = true;
        rot -= 2.356194f;
      }
      if (cursorSlot == 4 || cursorSlot == 5)
      {
        flag2 = false;
        // ISSUE: explicit reference operation
        ((Vector2) @zero).\u002Ector(8f);
        if (flag1 && specialMode == 0)
        {
          float num1 = rot;
          if ((double) num1 < 0.0)
            num1 += 6.283185f;
          for (float num2 = 0.0f; (double) num2 < 4.0; ++num2)
          {
            if ((double) Math.Abs(num1 - 1.570796f * num2) <= 0.785398185253143)
            {
              rot = 1.570796f * num2;
              break;
            }
          }
        }
      }
      Vector2 vector2_2 = Vector2.get_One();
      if (Main.ThickMouse && cursorSlot == 0 || cursorSlot == 1)
        vector2_2 = Main.DrawThickCursor(cursorSlot == 1);
      if (flag2)
        sb.Draw(Main.cursorTextures[cursorSlot], Vector2.op_Addition(Vector2.op_Addition(vector2_1, vector2_2), Vector2.get_One()), new Rectangle?(), color.MultiplyRGB(new Color(0.2f, 0.2f, 0.2f, 0.5f)), rot, zero, scale * 1.1f, (SpriteEffects) 0, 0.0f);
      if (!flag3)
        return;
      sb.Draw(Main.cursorTextures[cursorSlot], Vector2.op_Addition(vector2_1, vector2_2), new Rectangle?(), color, rot, zero, scale, (SpriteEffects) 0, 0.0f);
    }

    public delegate bool PerLinePoint(int x, int y);

    public delegate void LaserLineFraming(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distanceCovered, out Rectangle frame, out Vector2 origin, out Color color);

    public delegate Color ColorLerpMethod(float percent);
  }
}
