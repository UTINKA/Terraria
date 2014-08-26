using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
namespace Terraria
{
	public static class Utils
	{
		public static Dictionary<SpriteFont, float[]> charLengths = new Dictionary<SpriteFont, float[]>();
		public static bool FloatIntersect(float r1StartX, float r1StartY, float r1Width, float r1Height, float r2StartX, float r2StartY, float r2Width, float r2Height)
		{
			return r1StartX <= r2StartX + r2Width && r1StartY <= r2StartY + r2Height && r1StartX + r1Width >= r2StartX && r1StartY + r1Height >= r2StartY;
		}
		public static string[] WordwrapString(string text, SpriteFont font, int maxWidth, int maxLines, out int lineAmount)
		{
			string[] array = new string[maxLines];
			int num = 0;
			List<string> list = new List<string>(text.Split(new char[]
			{
				'\n'
			}));
			List<string> list2 = new List<string>(list[0].Split(new char[]
			{
				' '
			}));
			for (int i = 1; i < list.Count; i++)
			{
				list2.Add("\n");
				list2.AddRange(list[i].Split(new char[]
				{
					' '
				}));
			}
			bool flag = true;
			while (list2.Count > 0)
			{
				string text2 = list2[0];
				string str = " ";
				if (list2.Count == 1)
				{
					str = "";
				}
				if (text2 == "\n")
				{
					string[] array2;
					IntPtr intPtr;
					(array2 = array)[(int)(intPtr = (IntPtr)(num++))] = array2[(int)intPtr] + text2;
					if (num >= maxLines)
					{
						break;
					}
					list2.RemoveAt(0);
				}
				else
				{
					if (flag)
					{
						if (font.MeasureString(text2).X > (float)maxWidth)
						{
							string text3 = string.Concat(text2[0]);
							int num2 = 1;
							while (font.MeasureString(text3 + text2[num2] + '-').X <= (float)maxWidth)
							{
								text3 += text2[num2++];
							}
							text3 += '-';
							array[num++] = text3 + " ";
							if (num >= maxLines)
							{
								break;
							}
							list2.RemoveAt(0);
							list2.Insert(0, text2.Substring(num2));
						}
						else
						{
							string[] array3;
							IntPtr intPtr2;
							(array3 = array)[(int)(intPtr2 = (IntPtr)num)] = array3[(int)intPtr2] + text2 + str;
							flag = false;
							list2.RemoveAt(0);
						}
					}
					else
					{
						if (font.MeasureString(array[num] + text2).X > (float)maxWidth)
						{
							num++;
							if (num >= maxLines)
							{
								break;
							}
							flag = true;
						}
						else
						{
							string[] array4;
							IntPtr intPtr3;
							(array4 = array)[(int)(intPtr3 = (IntPtr)num)] = array4[(int)intPtr3] + text2 + str;
							flag = false;
							list2.RemoveAt(0);
						}
					}
				}
			}
			lineAmount = num;
			if (lineAmount == maxLines)
			{
				lineAmount--;
			}
			return array;
		}
		public static void DrawBorderStringFourWay(SpriteBatch sb, SpriteFont font, string text, float x, float y, Color textColor, Color borderColor, Vector2 origin, float scale = 1f)
		{
			Color color = borderColor;
			Vector2 zero = Vector2.Zero;
			int i = 0;
			while (i < 5)
			{
				switch (i)
				{
				case 0:
					zero.X = x - 2f;
					zero.Y = y;
					break;
				case 1:
					zero.X = x + 2f;
					zero.Y = y;
					break;
				case 2:
					zero.X = x;
					zero.Y = y - 2f;
					break;
				case 3:
					zero.X = x;
					zero.Y = y + 2f;
					break;
				case 4:
					goto IL_92;
				default:
					goto IL_92;
				}
				IL_A6:
				sb.DrawString(font, text, zero, color, 0f, origin, scale, SpriteEffects.None, 0f);
				i++;
				continue;
				IL_92:
				zero.X = x;
				zero.Y = y;
				color = textColor;
				goto IL_A6;
			}
		}
		public static Vector2 DrawBorderString(SpriteBatch sb, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int stringLimit = -1)
		{
			if (stringLimit != -1 && text.Length > stringLimit)
			{
				text.Substring(0, stringLimit);
			}
			SpriteFont fontMouseText = Main.fontMouseText;
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					sb.DrawString(fontMouseText, text, pos + new Vector2((float)i, (float)j) * 1f, Color.Black, 0f, new Vector2(anchorx, anchory) * fontMouseText.MeasureString(text), scale, SpriteEffects.None, 0f);
				}
			}
			sb.DrawString(fontMouseText, text, pos, color, 0f, new Vector2(anchorx, anchory) * fontMouseText.MeasureString(text), scale, SpriteEffects.None, 0f);
			return fontMouseText.MeasureString(text) * scale;
		}
		public static Vector2 DrawBorderStringBig(SpriteBatch sb, string text, Vector2 pos, Color color, float scale = 1f, float anchorx = 0f, float anchory = 0f, int stringLimit = -1)
		{
			if (stringLimit != -1 && text.Length > stringLimit)
			{
				text.Substring(0, stringLimit);
			}
			SpriteFont fontDeathText = Main.fontDeathText;
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					sb.DrawString(fontDeathText, text, pos + new Vector2((float)i, (float)j) * 1f, Color.Black, 0f, new Vector2(anchorx, anchory) * fontDeathText.MeasureString(text), scale, SpriteEffects.None, 0f);
				}
			}
			sb.DrawString(fontDeathText, text, pos, color, 0f, new Vector2(anchorx, anchory) * fontDeathText.MeasureString(text), scale, SpriteEffects.None, 0f);
			return fontDeathText.MeasureString(text) * scale;
		}
		public static void DrawInvBG(SpriteBatch sb, Rectangle R, Color c = default(Color))
		{
			Utils.DrawInvBG(sb, R.X, R.Y, R.Width, R.Height, c);
		}
		public static void DrawInvBG(SpriteBatch sb, float x, float y, float w, float h, Color c = default(Color))
		{
			Utils.DrawInvBG(sb, (int)x, (int)y, (int)w, (int)h, c);
		}
		public static void DrawInvBG(SpriteBatch sb, int x, int y, int w, int h, Color c = default(Color))
		{
			if (c == default(Color))
			{
				c = new Color(63, 65, 151, 255) * 0.785f;
			}
			Texture2D inventoryBack13Texture = Main.inventoryBack13Texture;
			if (w < 20)
			{
				w = 20;
			}
			if (h < 20)
			{
				h = 20;
			}
			sb.Draw(inventoryBack13Texture, new Rectangle(x, y, 10, 10), new Rectangle?(new Rectangle(0, 0, 10, 10)), c);
			sb.Draw(inventoryBack13Texture, new Rectangle(x + 10, y, w - 20, 10), new Rectangle?(new Rectangle(10, 0, 10, 10)), c);
			sb.Draw(inventoryBack13Texture, new Rectangle(x + w - 10, y, 10, 10), new Rectangle?(new Rectangle(inventoryBack13Texture.Width - 10, 0, 10, 10)), c);
			sb.Draw(inventoryBack13Texture, new Rectangle(x, y + 10, 10, h - 20), new Rectangle?(new Rectangle(0, 10, 10, 10)), c);
			sb.Draw(inventoryBack13Texture, new Rectangle(x + 10, y + 10, w - 20, h - 20), new Rectangle?(new Rectangle(10, 10, 10, 10)), c);
			sb.Draw(inventoryBack13Texture, new Rectangle(x + w - 10, y + 10, 10, h - 20), new Rectangle?(new Rectangle(inventoryBack13Texture.Width - 10, 10, 10, 10)), c);
			sb.Draw(inventoryBack13Texture, new Rectangle(x, y + h - 10, 10, 10), new Rectangle?(new Rectangle(0, inventoryBack13Texture.Height - 10, 10, 10)), c);
			sb.Draw(inventoryBack13Texture, new Rectangle(x + 10, y + h - 10, w - 20, 10), new Rectangle?(new Rectangle(10, inventoryBack13Texture.Height - 10, 10, 10)), c);
			sb.Draw(inventoryBack13Texture, new Rectangle(x + w - 10, y + h - 10, 10, 10), new Rectangle?(new Rectangle(inventoryBack13Texture.Width - 10, inventoryBack13Texture.Height - 10, 10, 10)), c);
		}
		public static void WriteRGB(this BinaryWriter bb, Color c)
		{
			bb.Write(c.R);
			bb.Write(c.G);
			bb.Write(c.B);
		}
		public static void WriteVector2(this BinaryWriter bb, Vector2 v)
		{
			bb.Write(v.X);
			bb.Write(v.Y);
		}
		public static Color ReadRGB(this BinaryReader bb)
		{
			return new Color((int)bb.ReadByte(), (int)bb.ReadByte(), (int)bb.ReadByte());
		}
		public static Vector2 ReadVector2(this BinaryReader bb)
		{
			return new Vector2(bb.ReadSingle(), bb.ReadSingle());
		}
		public static float ToRotation(this Vector2 v)
		{
			return (float)Math.Atan2((double)v.Y, (double)v.X);
		}
		public static Vector2 ToRotationVector2(this float f)
		{
			return new Vector2((float)Math.Cos((double)f), (float)Math.Sin((double)f));
		}
		public static Vector2 Rotate(this Vector2 spinningpoint, double radians, Vector2 center = default(Vector2))
		{
			float num = (float)Math.Cos(radians);
			float num2 = (float)Math.Sin(radians);
			Vector2 vector = spinningpoint - center;
			Vector2 result = center;
			result.X += vector.X * num - vector.Y * num2;
			result.Y += vector.X * num2 + vector.Y * num;
			return result;
		}
		public static Color Multiply(this Color firstColor, Color secondColor)
		{
			return new Color((int)((byte)((float)(firstColor.R * secondColor.R) / 255f)), (int)((byte)((float)(firstColor.G * secondColor.G) / 255f)), (int)((byte)((float)(firstColor.B * secondColor.B) / 255f)));
		}
	}
}
