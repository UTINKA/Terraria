using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
namespace Terraria
{
	public class Map
	{
		private struct OldMapHelper
		{
			public byte misc;
			public byte misc2;
			public bool active()
			{
				return (this.misc & 1) == 1;
			}
			public bool water()
			{
				return (this.misc & 2) == 2;
			}
			public bool lava()
			{
				return (this.misc & 4) == 4;
			}
			public bool honey()
			{
				return (this.misc2 & 64) == 64;
			}
			public bool changed()
			{
				return (this.misc & 8) == 8;
			}
			public bool wall()
			{
				return (this.misc & 16) == 16;
			}
			public byte option()
			{
				byte b = 0;
				if ((this.misc & 32) == 32)
				{
					b += 1;
				}
				if ((this.misc & 64) == 64)
				{
					b += 2;
				}
				if ((this.misc & 128) == 128)
				{
					b += 4;
				}
				if ((this.misc2 & 1) == 1)
				{
					b += 8;
				}
				return b;
			}
			public byte color()
			{
				return (byte)((this.misc2 & 30) >> 1);
			}
		}
		public const int drawLoopMilliseconds = 5;
		private const int HeaderEmpty = 0;
		private const int HeaderTile = 1;
		private const int HeaderWall = 2;
		private const int HeaderWater = 3;
		private const int HeaderLava = 4;
		private const int HeaderHoney = 5;
		private const int HeaderHeavenAndHell = 6;
		private const int HeaderBackground = 7;
		private const int maxTileOptions = 10;
		private const int maxWallOptions = 2;
		private const int maxLiquidTypes = 3;
		private const int maxSkyGradients = 256;
		private const int maxDirtGradients = 256;
		private const int maxRockGradients = 256;
		public static int maxUpdateTile = 1000;
		public static int numUpdateTile = 0;
		public static short[] updateTileX = new short[Map.maxUpdateTile];
		public static short[] updateTileY = new short[Map.maxUpdateTile];
		public static bool saveLock = false;
		private static object padlock = new object();
		public static int[] tileOptionCounts;
		public static int[] wallOptionCounts;
		public static ushort[] tileLookup;
		public static ushort[] wallLookup;
		public static ushort tilePosition;
		public static ushort wallPosition;
		public static ushort liquidPosition;
		public static ushort skyPosition;
		public static ushort dirtPosition;
		public static ushort rockPosition;
		public static ushort hellPosition;
		public static Color[] colorLookup;
		public static ushort[] snowTypes;
		public static ushort wallRangeStart;
		public static ushort wallRangeEnd;
		public ushort type;
		public byte light;
		public byte misc;
		public bool changed
		{
			get
			{
				return (this.misc & 32) == 32;
			}
			set
			{
				if (value)
				{
					this.misc |= 32;
					return;
				}
				this.misc = (byte)((int)this.misc & -33);
			}
		}
		public byte color
		{
			get
			{
				return this.misc & 31;
			}
			set
			{
				if (value > 30)
				{
					value = 30;
				}
				this.misc = ((this.misc & 224) | value);
			}
		}
		public Map()
		{
			this.type = 0;
			this.light = 0;
			this.misc = 0;
		}
		public Map(Map copyFrom)
		{
			this.type = copyFrom.type;
			this.light = copyFrom.light;
			this.misc = copyFrom.misc;
		}
		public Map(Map copyFrom, byte newLight)
		{
			this.type = copyFrom.type;
			this.light = newLight;
			this.misc = copyFrom.misc;
		}
		public bool isTheSameAs(Map compMap)
		{
			return compMap != null && this.type == compMap.type && this.light == compMap.light && this.color == compMap.color;
		}
		public bool isTheSameAs_NoLight(Map compMap)
		{
			return compMap != null && this.type == compMap.type && this.color == compMap.color;
		}
		public void mapColor(ref Color oldColor, byte colorType)
		{
			Color color = WorldGen.paintColor((int)colorType);
			float num = (float)oldColor.R / 255f;
			float num2 = (float)oldColor.G / 255f;
			float num3 = (float)oldColor.B / 255f;
			if (num2 > num)
			{
				num = num2;
			}
			if (num3 > num)
			{
				float num4 = num;
				num = num3;
				num3 = num4;
			}
			if (colorType == 29)
			{
				float num5 = num3 * 0.3f;
				oldColor.R = (byte)((float)color.R * num5);
				oldColor.G = (byte)((float)color.G * num5);
				oldColor.B = (byte)((float)color.B * num5);
				return;
			}
			if (colorType != 30)
			{
				float num6 = num;
				oldColor.R = (byte)((float)color.R * num6);
				oldColor.G = (byte)((float)color.G * num6);
				oldColor.B = (byte)((float)color.B * num6);
				return;
			}
			if (this.type >= Map.wallRangeStart && this.type <= Map.wallRangeEnd)
			{
				oldColor.R = (byte)((float)(255 - oldColor.R) * 0.5f);
				oldColor.G = (byte)((float)(255 - oldColor.G) * 0.5f);
				oldColor.B = (byte)((float)(255 - oldColor.B) * 0.5f);
				return;
			}
			oldColor.R = 255 - oldColor.R;
			oldColor.G = 255 - oldColor.G;
			oldColor.B = 255 - oldColor.B;
		}
		public void getColor(out Color retColor, int j)
		{
			retColor = Map.colorLookup[(int)this.type];
			byte color = this.color;
			if (color > 0)
			{
				this.mapColor(ref retColor, color);
			}
			if (this.light == 255)
			{
				return;
			}
			float num = (float)this.light / 255f;
			retColor.R = (byte)((float)retColor.R * num);
			retColor.G = (byte)((float)retColor.G * num);
			retColor.B = (byte)((float)retColor.B * num);
		}
		public void setTile(int i, int j, byte Light)
		{
			Tile tile = Main.tile[i, j];
			if (tile == null)
			{
				return;
			}
			int num = (int)this.type;
			int num2 = (int)this.light;
			int color = (int)this.color;
			int num3 = 0;
			int num4 = (int)Light;
			int num5 = 0;
			int num6 = 0;
			if (tile.active())
			{
				int num7 = (int)tile.type;
				num5 = (int)Map.tileLookup[num7];
				if (num7 == 51 && (i + j) % 2 == 0)
				{
					num5 = 0;
				}
				if (num5 != 0)
				{
					if (num7 == 160)
					{
						num3 = 0;
					}
					else
					{
						num3 = (int)tile.color();
					}
					int num8 = num7;
					if (num8 <= 137)
					{
						if (num8 <= 31)
						{
							if (num8 == 4)
							{
								if (tile.frameX < 66)
								{
								}
								num6 = 0;
								goto IL_850;
							}
							if (num8 != 21)
							{
								switch (num8)
								{
								case 26:
									if (tile.frameX >= 54)
									{
										num6 = 1;
										goto IL_850;
									}
									num6 = 0;
									goto IL_850;
								case 27:
									if (tile.frameY < 34)
									{
										num6 = 1;
										goto IL_850;
									}
									num6 = 0;
									goto IL_850;
								case 28:
									if (tile.frameY < 144)
									{
										num6 = 0;
										goto IL_850;
									}
									if (tile.frameY < 252)
									{
										num6 = 1;
										goto IL_850;
									}
									if (tile.frameY < 360 || (tile.frameY > 900 && tile.frameY < 1008))
									{
										num6 = 2;
										goto IL_850;
									}
									if (tile.frameY < 468)
									{
										num6 = 3;
										goto IL_850;
									}
									if (tile.frameY < 576)
									{
										num6 = 4;
										goto IL_850;
									}
									if (tile.frameY < 684)
									{
										num6 = 5;
										goto IL_850;
									}
									if (tile.frameY < 792)
									{
										num6 = 6;
										goto IL_850;
									}
									if (tile.frameY < 898)
									{
										num6 = 8;
										goto IL_850;
									}
									num6 = 7;
									goto IL_850;
								case 31:
									if (tile.frameX >= 36)
									{
										num6 = 1;
										goto IL_850;
									}
									num6 = 0;
									goto IL_850;
								}
							}
							else
							{
								int num9 = (int)(tile.frameX / 36);
								if (num9 == 1 || num9 == 2 || num9 == 10 || num9 == 13 || num9 == 15)
								{
									num6 = 1;
									goto IL_850;
								}
								if (num9 == 3 || num9 == 4)
								{
									num6 = 2;
									goto IL_850;
								}
								if (num9 == 6)
								{
									num6 = 3;
									goto IL_850;
								}
								if (num9 == 11 || num9 == 17)
								{
									num6 = 4;
									goto IL_850;
								}
								num6 = 0;
								goto IL_850;
							}
						}
						else
						{
							switch (num8)
							{
							case 82:
							case 83:
							case 84:
								if (tile.frameX < 18)
								{
									num6 = 0;
									goto IL_850;
								}
								if (tile.frameX < 36)
								{
									num6 = 1;
									goto IL_850;
								}
								if (tile.frameX < 54)
								{
									num6 = 2;
									goto IL_850;
								}
								if (tile.frameX < 72)
								{
									num6 = 3;
									goto IL_850;
								}
								if (tile.frameX < 90)
								{
									num6 = 4;
									goto IL_850;
								}
								if (tile.frameX < 108)
								{
									num6 = 5;
									goto IL_850;
								}
								num6 = 6;
								goto IL_850;
							default:
								if (num8 != 105)
								{
									switch (num8)
									{
									case 133:
										if (tile.frameX < 52)
										{
											num6 = 0;
											goto IL_850;
										}
										num6 = 1;
										goto IL_850;
									case 134:
										if (tile.frameX < 28)
										{
											num6 = 0;
											goto IL_850;
										}
										num6 = 1;
										goto IL_850;
									case 137:
										if (tile.frameY == 0)
										{
											num6 = 0;
											goto IL_850;
										}
										num6 = 1;
										goto IL_850;
									}
								}
								else
								{
									if (tile.frameX >= 1548 && tile.frameX <= 1654)
									{
										num6 = 1;
										goto IL_850;
									}
									if (tile.frameX >= 1656 && tile.frameX <= 1798)
									{
										num6 = 2;
										goto IL_850;
									}
									num6 = 0;
									goto IL_850;
								}
								break;
							}
						}
					}
					else
					{
						if (num8 <= 165)
						{
							if (num8 == 149)
							{
								num6 = j % 3;
								goto IL_850;
							}
							if (num8 == 160)
							{
								num6 = j % 3;
								goto IL_850;
							}
							if (num8 == 165)
							{
								if (tile.frameX < 54)
								{
									num6 = 0;
									goto IL_850;
								}
								if (tile.frameX < 106)
								{
									num6 = 1;
									goto IL_850;
								}
								if (tile.frameX >= 216)
								{
									num6 = 1;
									goto IL_850;
								}
								if (tile.frameX < 162)
								{
									num6 = 2;
									goto IL_850;
								}
								num6 = 3;
								goto IL_850;
							}
						}
						else
						{
							if (num8 <= 187)
							{
								if (num8 != 178)
								{
									switch (num8)
									{
									case 184:
										if (tile.frameX < 22)
										{
											num6 = 0;
											goto IL_850;
										}
										if (tile.frameX < 44)
										{
											num6 = 1;
											goto IL_850;
										}
										if (tile.frameX < 66)
										{
											num6 = 2;
											goto IL_850;
										}
										if (tile.frameX < 88)
										{
											num6 = 3;
											goto IL_850;
										}
										num6 = 4;
										goto IL_850;
									case 185:
										if (tile.frameY < 18)
										{
											int num9 = (int)(tile.frameX / 18);
											if (num9 < 6 || num9 == 28 || num9 == 29 || num9 == 30 || num9 == 31 || num9 == 32)
											{
												num6 = 0;
												goto IL_850;
											}
											if (num9 < 12 || num9 == 33 || num9 == 34 || num9 == 35)
											{
												num6 = 1;
												goto IL_850;
											}
											if (num9 < 28)
											{
												num6 = 2;
												goto IL_850;
											}
											if (num9 < 48)
											{
												num6 = 3;
												goto IL_850;
											}
											if (num9 < 54)
											{
												num6 = 4;
												goto IL_850;
											}
											goto IL_850;
										}
										else
										{
											int num9 = (int)(tile.frameX / 36);
											if (num9 < 6 || num9 == 19 || num9 == 20 || num9 == 21 || num9 == 22 || num9 == 23 || num9 == 24 || num9 == 33 || num9 == 38 || num9 == 39 || num9 == 40)
											{
												num6 = 0;
												goto IL_850;
											}
											if (num9 < 16)
											{
												num6 = 2;
												goto IL_850;
											}
											if (num9 < 19 || num9 == 31 || num9 == 32)
											{
												num6 = 1;
												goto IL_850;
											}
											if (num9 < 31)
											{
												num6 = 3;
												goto IL_850;
											}
											if (num9 < 38)
											{
												num6 = 4;
												goto IL_850;
											}
											goto IL_850;
										}
										break;
									case 186:
									{
										int num9 = (int)(tile.frameX / 54);
										if (num9 < 7)
										{
											num6 = 2;
											goto IL_850;
										}
										if (num9 < 22 || num9 == 33 || num9 == 34 || num9 == 35)
										{
											num6 = 0;
											goto IL_850;
										}
										if (num9 < 25)
										{
											num6 = 1;
											goto IL_850;
										}
										if (num9 == 25)
										{
											num6 = 5;
											goto IL_850;
										}
										if (num9 < 32)
										{
											num6 = 3;
											goto IL_850;
										}
										goto IL_850;
									}
									case 187:
									{
										int num9 = (int)(tile.frameX / 54);
										if (num9 < 3 || num9 == 14 || num9 == 15 || num9 == 16)
										{
											num6 = 0;
											goto IL_850;
										}
										if (num9 < 6)
										{
											num6 = 6;
											goto IL_850;
										}
										if (num9 < 9)
										{
											num6 = 7;
											goto IL_850;
										}
										if (num9 < 14)
										{
											num6 = 4;
											goto IL_850;
										}
										if (num9 < 18)
										{
											num6 = 4;
											goto IL_850;
										}
										if (num9 < 23)
										{
											num6 = 8;
											goto IL_850;
										}
										if (num9 < 25)
										{
											num6 = 0;
											goto IL_850;
										}
										if (num9 < 29)
										{
											num6 = 1;
											goto IL_850;
										}
										goto IL_850;
									}
									}
								}
								else
								{
									if (tile.frameX < 18)
									{
										num6 = 0;
										goto IL_850;
									}
									if (tile.frameX < 36)
									{
										num6 = 1;
										goto IL_850;
									}
									if (tile.frameX < 54)
									{
										num6 = 2;
										goto IL_850;
									}
									if (tile.frameX < 72)
									{
										num6 = 3;
										goto IL_850;
									}
									if (tile.frameX < 90)
									{
										num6 = 4;
										goto IL_850;
									}
									if (tile.frameX < 108)
									{
										num6 = 5;
										goto IL_850;
									}
									num6 = 6;
									goto IL_850;
								}
							}
							else
							{
								if (num8 == 227)
								{
									num6 = (int)(tile.frameX / 34);
									goto IL_850;
								}
								switch (num8)
								{
								case 240:
								{
									int num9 = (int)(tile.frameX / 54);
									int num10 = (int)(tile.frameY / 54);
									num9 += num10 * 36;
									if ((num9 >= 0 && num9 <= 11) || (num9 >= 47 && num9 <= 53))
									{
										num6 = 0;
										goto IL_850;
									}
									if (num9 >= 12 && num9 <= 15)
									{
										num6 = 1;
										goto IL_850;
									}
									if (num9 == 16 || num9 == 17)
									{
										num6 = 2;
										goto IL_850;
									}
									if (num9 >= 18 && num9 <= 35)
									{
										num6 = 1;
										goto IL_850;
									}
									if (num9 >= 41 && num9 <= 45)
									{
										num6 = 3;
										goto IL_850;
									}
									if (num9 == 46)
									{
										num6 = 4;
										goto IL_850;
									}
									goto IL_850;
								}
								case 242:
								{
									int num9 = (int)(tile.frameY / 72);
									if (num9 >= 22 && num9 <= 24)
									{
										num6 = 1;
										goto IL_850;
									}
									num6 = 0;
									goto IL_850;
								}
								}
							}
						}
					}
					num6 = 0;
				}
			}
			IL_850:
			if (num5 == 0)
			{
				if (tile.liquid > 32)
				{
					int num11 = (int)tile.liquidType();
					num5 = (int)Map.liquidPosition + num11;
				}
				else
				{
					if (tile.wall > 0)
					{
						int wall = (int)tile.wall;
						num5 = (int)Map.wallLookup[wall];
						num3 = (int)tile.wallColor();
						int num12 = wall;
						if (num12 <= 27)
						{
							if (num12 != 21)
							{
								if (num12 != 27)
								{
									goto IL_8E7;
								}
								num6 = i % 2;
								goto IL_8EA;
							}
						}
						else
						{
							switch (num12)
							{
							case 88:
							case 89:
							case 90:
							case 91:
							case 92:
							case 93:
								break;
							default:
								if (num12 != 168)
								{
									goto IL_8E7;
								}
								break;
							}
						}
						num3 = 0;
						goto IL_8EA;
						IL_8E7:
						num6 = 0;
					}
				}
			}
			IL_8EA:
			if (num5 == 0)
			{
				if ((double)j < Main.worldSurface)
				{
					int num13 = (int)((byte)(255.0 * ((double)j / Main.worldSurface)));
					num5 = (int)Map.skyPosition + num13;
					num4 = 255;
					num3 = 0;
				}
				else
				{
					if (j < Main.maxTilesY - 200)
					{
						num3 = 0;
						bool flag = num < (int)Map.dirtPosition || num >= (int)Map.hellPosition;
						byte b = 0;
						float num14 = Main.screenPosition.X / 16f - 5f;
						float num15 = (Main.screenPosition.X + (float)Main.screenWidth) / 16f + 5f;
						float num16 = Main.screenPosition.Y / 16f - 5f;
						float num17 = (Main.screenPosition.Y + (float)Main.screenHeight) / 16f + 5f;
						if (((float)i < num14 || (float)i > num15 || (float)j < num16 || (float)j > num17) && i > 40 && i < Main.maxTilesX - 40 && j > 40 && j < Main.maxTilesY - 40)
						{
							if (flag)
							{
								for (int k = i - 36; k <= i + 30; k += 10)
								{
									for (int l = j - 36; l <= j + 30; l += 10)
									{
										if (Main.map[k, l] != null)
										{
											int num18 = (int)Main.map[k, l].type;
											for (int m = 0; m < Map.snowTypes.Length; m++)
											{
												if ((int)Map.snowTypes[m] == num18)
												{
													b = 255;
													k = i + 31;
													l = j + 31;
													break;
												}
											}
										}
									}
								}
							}
						}
						else
						{
							float num19 = (float)Main.snowTiles / 1000f;
							num19 *= 255f;
							if (num19 > 255f)
							{
								num19 = 255f;
							}
							b = (byte)num19;
						}
						if ((double)j < Main.rockLayer)
						{
							num5 = (int)(Map.dirtPosition + (ushort)b);
						}
						else
						{
							num5 = (int)(Map.rockPosition + (ushort)b);
						}
					}
					else
					{
						num5 = (int)Map.hellPosition;
					}
				}
			}
			int num20 = num5 + num6;
			bool flag2 = false;
			if (num20 != num)
			{
				this.type = (ushort)num20;
				flag2 = true;
			}
			if (num3 != color)
			{
				this.color = (byte)num3;
				flag2 = true;
			}
			if (num4 > num2)
			{
				this.light = (byte)num4;
				flag2 = true;
			}
			if (flag2)
			{
				this.changed = true;
			}
		}
		public static void Initialize()
		{
			Color[][] array = new Color[340][];
			for (int i = 0; i < 340; i++)
			{
				array[i] = new Color[10];
			}
			Color color = new Color(151, 107, 75);
			array[0][0] = color;
			array[5][0] = color;
			array[30][0] = color;
			array[191][0] = color;
			array[272][0] = new Color(121, 119, 101);
			color = new Color(128, 128, 128);
			array[1][0] = color;
			array[38][0] = color;
			array[48][0] = color;
			array[130][0] = color;
			array[138][0] = color;
			array[273][0] = color;
			array[283][0] = color;
			array[2][0] = new Color(28, 216, 94);
			color = new Color(26, 196, 84);
			array[3][0] = color;
			array[192][0] = color;
			array[73][0] = new Color(27, 197, 109);
			array[52][0] = new Color(23, 177, 76);
			array[20][0] = new Color(163, 116, 81);
			array[6][0] = new Color(140, 101, 80);
			color = new Color(150, 67, 22);
			array[7][0] = color;
			array[47][0] = color;
			array[284][0] = color;
			color = new Color(185, 164, 23);
			array[8][0] = color;
			array[45][0] = color;
			color = new Color(185, 194, 195);
			array[9][0] = color;
			array[46][0] = color;
			color = new Color(98, 95, 167);
			array[22][0] = color;
			array[140][0] = color;
			array[23][0] = new Color(141, 137, 223);
			array[24][0] = new Color(122, 116, 218);
			array[25][0] = new Color(109, 90, 128);
			array[37][0] = new Color(104, 86, 84);
			array[39][0] = new Color(181, 62, 59);
			array[40][0] = new Color(146, 81, 68);
			array[41][0] = new Color(66, 84, 109);
			array[43][0] = new Color(84, 100, 63);
			array[44][0] = new Color(107, 68, 99);
			array[53][0] = new Color(186, 168, 84);
			color = new Color(190, 171, 94);
			array[151][0] = color;
			array[154][0] = color;
			array[274][0] = color;
			array[328][0] = new Color(200, 246, 254);
			array[329][0] = new Color(15, 15, 15);
			array[54][0] = new Color(200, 246, 254);
			array[56][0] = new Color(43, 40, 84);
			array[75][0] = new Color(26, 26, 26);
			array[57][0] = new Color(68, 68, 76);
			color = new Color(142, 66, 66);
			array[58][0] = color;
			array[76][0] = color;
			color = new Color(92, 68, 73);
			array[59][0] = color;
			array[120][0] = color;
			array[60][0] = new Color(143, 215, 29);
			array[61][0] = new Color(135, 196, 26);
			array[74][0] = new Color(96, 197, 27);
			array[62][0] = new Color(121, 176, 24);
			array[233][0] = new Color(107, 182, 29);
			array[63][0] = new Color(110, 140, 182);
			array[64][0] = new Color(196, 96, 114);
			array[65][0] = new Color(56, 150, 97);
			array[66][0] = new Color(160, 118, 58);
			array[67][0] = new Color(140, 58, 166);
			array[68][0] = new Color(125, 191, 197);
			array[70][0] = new Color(93, 127, 255);
			color = new Color(182, 175, 130);
			array[71][0] = color;
			array[72][0] = color;
			array[190][0] = color;
			color = new Color(73, 120, 17);
			array[80][0] = color;
			array[188][0] = color;
			color = new Color(11, 80, 143);
			array[107][0] = color;
			array[121][0] = color;
			color = new Color(91, 169, 169);
			array[108][0] = color;
			array[122][0] = color;
			color = new Color(128, 26, 52);
			array[111][0] = color;
			array[150][0] = color;
			array[109][0] = new Color(78, 193, 227);
			array[110][0] = new Color(48, 186, 135);
			array[113][0] = new Color(48, 208, 234);
			array[115][0] = new Color(33, 171, 207);
			array[112][0] = new Color(103, 98, 122);
			color = new Color(238, 225, 218);
			array[116][0] = color;
			array[118][0] = color;
			array[117][0] = new Color(181, 172, 190);
			array[119][0] = new Color(107, 92, 108);
			array[123][0] = new Color(106, 107, 118);
			array[124][0] = new Color(73, 51, 36);
			array[131][0] = new Color(52, 52, 52);
			array[145][0] = new Color(192, 30, 30);
			array[146][0] = new Color(43, 192, 30);
			color = new Color(211, 236, 241);
			array[147][0] = color;
			array[148][0] = color;
			array[152][0] = new Color(128, 133, 184);
			array[153][0] = new Color(239, 141, 126);
			array[155][0] = new Color(131, 162, 161);
			array[156][0] = new Color(170, 171, 157);
			array[157][0] = new Color(104, 100, 126);
			color = new Color(145, 81, 85);
			array[158][0] = color;
			array[232][0] = color;
			array[159][0] = new Color(148, 133, 98);
			array[160][0] = new Color(200, 0, 0);
			array[160][1] = new Color(0, 200, 0);
			array[160][2] = new Color(0, 0, 200);
			array[161][0] = new Color(144, 195, 232);
			array[162][0] = new Color(184, 219, 240);
			array[163][0] = new Color(174, 145, 214);
			array[164][0] = new Color(218, 182, 204);
			array[170][0] = new Color(27, 109, 69);
			array[171][0] = new Color(33, 135, 85);
			color = new Color(129, 125, 93);
			array[166][0] = color;
			array[175][0] = color;
			array[167][0] = new Color(62, 82, 114);
			color = new Color(132, 157, 127);
			array[168][0] = color;
			array[176][0] = color;
			color = new Color(152, 171, 198);
			array[169][0] = color;
			array[177][0] = color;
			array[179][0] = new Color(49, 134, 114);
			array[180][0] = new Color(126, 134, 49);
			array[181][0] = new Color(134, 59, 49);
			array[182][0] = new Color(43, 86, 140);
			array[183][0] = new Color(121, 49, 134);
			array[189][0] = new Color(223, 255, 255);
			array[193][0] = new Color(56, 121, 255);
			array[194][0] = new Color(157, 157, 107);
			array[195][0] = new Color(134, 22, 34);
			array[196][0] = new Color(147, 144, 178);
			array[197][0] = new Color(97, 200, 225);
			array[198][0] = new Color(62, 61, 52);
			array[199][0] = new Color(208, 80, 80);
			array[201][0] = new Color(203, 61, 64);
			array[205][0] = new Color(186, 50, 52);
			array[200][0] = new Color(216, 152, 144);
			array[202][0] = new Color(213, 178, 28);
			array[203][0] = new Color(128, 44, 45);
			array[204][0] = new Color(125, 55, 65);
			array[206][0] = new Color(124, 175, 201);
			array[208][0] = new Color(88, 105, 118);
			array[211][0] = new Color(191, 233, 115);
			array[213][0] = new Color(137, 120, 67);
			array[214][0] = new Color(103, 103, 103);
			array[221][0] = new Color(239, 90, 50);
			array[222][0] = new Color(231, 96, 228);
			array[223][0] = new Color(57, 85, 101);
			array[224][0] = new Color(107, 132, 139);
			array[225][0] = new Color(227, 125, 22);
			array[226][0] = new Color(141, 56, 0);
			array[229][0] = new Color(255, 156, 12);
			array[230][0] = new Color(131, 79, 13);
			array[234][0] = new Color(53, 44, 41);
			array[235][0] = new Color(214, 184, 46);
			array[236][0] = new Color(149, 232, 87);
			array[237][0] = new Color(255, 241, 51);
			array[238][0] = new Color(225, 128, 206);
			array[243][0] = new Color(198, 196, 170);
			array[248][0] = new Color(219, 71, 38);
			array[249][0] = new Color(235, 38, 231);
			array[250][0] = new Color(86, 85, 92);
			array[251][0] = new Color(235, 150, 23);
			array[252][0] = new Color(153, 131, 44);
			array[253][0] = new Color(57, 48, 97);
			array[254][0] = new Color(248, 158, 92);
			array[255][0] = new Color(107, 49, 154);
			array[256][0] = new Color(154, 148, 49);
			array[257][0] = new Color(49, 49, 154);
			array[258][0] = new Color(49, 154, 68);
			array[259][0] = new Color(154, 49, 77);
			array[260][0] = new Color(85, 89, 118);
			array[261][0] = new Color(154, 83, 49);
			array[262][0] = new Color(221, 79, 255);
			array[263][0] = new Color(250, 255, 79);
			array[264][0] = new Color(79, 102, 255);
			array[265][0] = new Color(79, 255, 89);
			array[266][0] = new Color(255, 79, 79);
			array[267][0] = new Color(240, 240, 247);
			array[268][0] = new Color(255, 145, 79);
			array[287][0] = new Color(79, 128, 17);
			color = new Color(122, 217, 232);
			array[275][0] = color;
			array[276][0] = color;
			array[277][0] = color;
			array[278][0] = color;
			array[279][0] = color;
			array[280][0] = color;
			array[281][0] = color;
			array[282][0] = color;
			array[285][0] = color;
			array[286][0] = color;
			array[288][0] = color;
			array[289][0] = color;
			array[290][0] = color;
			array[291][0] = color;
			array[292][0] = color;
			array[293][0] = color;
			array[294][0] = color;
			array[295][0] = color;
			array[296][0] = color;
			array[297][0] = color;
			array[298][0] = color;
			array[299][0] = color;
			array[309][0] = color;
			array[310][0] = color;
			array[339][0] = color;
			array[311][0] = new Color(117, 61, 25);
			array[312][0] = new Color(204, 93, 73);
			array[313][0] = new Color(87, 150, 154);
			array[4][0] = new Color(169, 125, 93);
			array[4][1] = new Color(253, 221, 3);
			color = new Color(253, 221, 3);
			array[93][0] = color;
			array[33][0] = color;
			array[174][0] = color;
			array[100][0] = color;
			array[98][0] = color;
			array[173][0] = color;
			color = new Color(119, 105, 79);
			array[11][0] = color;
			array[10][0] = color;
			color = new Color(191, 142, 111);
			array[14][0] = color;
			array[15][0] = color;
			array[18][0] = color;
			array[19][0] = color;
			array[55][0] = color;
			array[79][0] = color;
			array[86][0] = color;
			array[87][0] = color;
			array[88][0] = color;
			array[89][0] = color;
			array[94][0] = color;
			array[101][0] = color;
			array[104][0] = color;
			array[106][0] = color;
			array[114][0] = color;
			array[128][0] = color;
			array[139][0] = color;
			array[216][0] = color;
			array[269][0] = color;
			array[334][0] = color;
			array[12][0] = new Color(174, 24, 69);
			array[13][0] = new Color(133, 213, 247);
			color = new Color(144, 148, 144);
			array[17][0] = color;
			array[90][0] = color;
			array[96][0] = color;
			array[97][0] = color;
			array[99][0] = color;
			array[132][0] = color;
			array[142][0] = color;
			array[143][0] = color;
			array[144][0] = color;
			array[207][0] = color;
			array[209][0] = color;
			array[212][0] = color;
			array[217][0] = color;
			array[218][0] = color;
			array[219][0] = color;
			array[220][0] = color;
			array[228][0] = color;
			array[300][0] = color;
			array[301][0] = color;
			array[302][0] = color;
			array[303][0] = color;
			array[304][0] = color;
			array[305][0] = color;
			array[306][0] = color;
			array[307][0] = color;
			array[308][0] = color;
			array[105][0] = new Color(144, 148, 144);
			array[105][1] = new Color(177, 92, 31);
			array[105][2] = new Color(201, 188, 170);
			array[137][0] = new Color(144, 148, 144);
			array[137][1] = new Color(141, 56, 0);
			array[16][0] = new Color(140, 130, 116);
			array[26][0] = new Color(119, 101, 125);
			array[26][1] = new Color(214, 127, 133);
			array[36][0] = new Color(230, 89, 92);
			array[28][0] = new Color(151, 79, 80);
			array[28][1] = new Color(90, 139, 140);
			array[28][2] = new Color(192, 136, 70);
			array[28][3] = new Color(203, 185, 151);
			array[28][4] = new Color(73, 56, 41);
			array[28][5] = new Color(148, 159, 67);
			array[28][6] = new Color(138, 172, 67);
			array[28][7] = new Color(226, 122, 47);
			array[28][8] = new Color(198, 87, 93);
			array[29][0] = new Color(175, 105, 128);
			array[51][0] = new Color(192, 202, 203);
			array[31][0] = new Color(141, 120, 168);
			array[31][1] = new Color(212, 105, 105);
			array[32][0] = new Color(151, 135, 183);
			array[42][0] = new Color(251, 235, 127);
			array[50][0] = new Color(170, 48, 114);
			array[85][0] = new Color(192, 192, 192);
			array[69][0] = new Color(190, 150, 92);
			array[77][0] = new Color(238, 85, 70);
			array[81][0] = new Color(245, 133, 191);
			array[78][0] = new Color(121, 110, 97);
			array[141][0] = new Color(192, 59, 59);
			array[129][0] = new Color(255, 117, 224);
			array[126][0] = new Color(159, 209, 229);
			array[125][0] = new Color(141, 175, 255);
			array[103][0] = new Color(141, 98, 77);
			array[95][0] = new Color(255, 162, 31);
			array[92][0] = new Color(213, 229, 237);
			array[91][0] = new Color(13, 88, 130);
			array[215][0] = new Color(254, 121, 2);
			array[316][0] = new Color(157, 176, 226);
			array[317][0] = new Color(118, 227, 129);
			array[318][0] = new Color(227, 118, 215);
			array[319][0] = new Color(96, 68, 48);
			array[320][0] = new Color(203, 185, 151);
			array[321][0] = new Color(96, 77, 64);
			array[322][0] = new Color(198, 170, 104);
			array[149][0] = new Color(220, 50, 50);
			array[149][1] = new Color(0, 220, 50);
			array[149][2] = new Color(50, 50, 220);
			array[133][0] = new Color(231, 53, 56);
			array[133][1] = new Color(192, 189, 221);
			array[134][0] = new Color(166, 187, 153);
			array[134][1] = new Color(241, 129, 249);
			array[102][0] = new Color(229, 212, 73);
			array[49][0] = new Color(89, 201, 255);
			array[35][0] = new Color(226, 145, 30);
			array[34][0] = new Color(235, 166, 135);
			array[136][0] = new Color(213, 203, 204);
			array[231][0] = new Color(224, 194, 101);
			array[239][0] = new Color(224, 194, 101);
			array[240][0] = new Color(120, 85, 60);
			array[240][1] = new Color(99, 50, 30);
			array[240][2] = new Color(153, 153, 117);
			array[240][3] = new Color(112, 84, 56);
			array[240][4] = new Color(234, 231, 226);
			array[241][0] = new Color(77, 74, 72);
			array[244][0] = new Color(200, 245, 253);
			color = new Color(99, 50, 30);
			array[242][0] = color;
			array[245][0] = color;
			array[246][0] = color;
			array[242][1] = new Color(185, 142, 97);
			array[247][0] = new Color(140, 150, 150);
			array[271][0] = new Color(107, 250, 255);
			array[270][0] = new Color(187, 255, 107);
			array[314][0] = new Color(181, 164, 125);
			array[324][0] = new Color(228, 213, 173);
			array[21][0] = new Color(174, 129, 92);
			array[21][1] = new Color(233, 207, 94);
			array[21][2] = new Color(137, 128, 200);
			array[21][3] = new Color(160, 160, 160);
			array[21][4] = new Color(106, 210, 255);
			array[27][0] = new Color(54, 154, 54);
			array[27][1] = new Color(226, 196, 49);
			color = new Color(246, 197, 26);
			array[82][0] = color;
			array[83][0] = color;
			array[84][0] = color;
			color = new Color(76, 150, 216);
			array[82][1] = color;
			array[83][1] = color;
			array[84][1] = color;
			color = new Color(185, 214, 42);
			array[82][2] = color;
			array[83][2] = color;
			array[84][2] = color;
			color = new Color(167, 203, 37);
			array[82][3] = color;
			array[83][3] = color;
			array[84][3] = color;
			color = new Color(72, 145, 125);
			array[82][4] = color;
			array[83][4] = color;
			array[84][4] = color;
			color = new Color(177, 69, 49);
			array[82][5] = color;
			array[83][5] = color;
			array[84][5] = color;
			color = new Color(40, 152, 240);
			array[82][6] = color;
			array[83][6] = color;
			array[84][6] = color;
			array[165][0] = new Color(115, 173, 229);
			array[165][1] = new Color(100, 100, 100);
			array[165][2] = new Color(152, 152, 152);
			array[165][3] = new Color(227, 125, 22);
			array[178][0] = new Color(208, 94, 201);
			array[178][1] = new Color(233, 146, 69);
			array[178][2] = new Color(71, 146, 251);
			array[178][3] = new Color(60, 226, 133);
			array[178][4] = new Color(250, 30, 71);
			array[178][5] = new Color(166, 176, 204);
			array[178][6] = new Color(255, 217, 120);
			array[184][0] = new Color(29, 106, 88);
			array[184][1] = new Color(94, 100, 36);
			array[184][2] = new Color(96, 44, 40);
			array[184][3] = new Color(34, 63, 102);
			array[184][4] = new Color(79, 35, 95);
			color = new Color(99, 99, 99);
			array[185][0] = color;
			array[186][0] = color;
			array[187][0] = color;
			color = new Color(114, 81, 56);
			array[185][1] = color;
			array[186][1] = color;
			array[187][1] = color;
			color = new Color(133, 133, 101);
			array[185][2] = color;
			array[186][2] = color;
			array[187][2] = color;
			color = new Color(151, 200, 211);
			array[185][3] = color;
			array[186][3] = color;
			array[187][3] = color;
			color = new Color(177, 183, 161);
			array[185][4] = color;
			array[186][4] = color;
			array[187][4] = color;
			color = new Color(134, 114, 38);
			array[185][5] = color;
			array[186][5] = color;
			array[187][5] = color;
			color = new Color(82, 62, 66);
			array[185][6] = color;
			array[186][6] = color;
			array[187][6] = color;
			color = new Color(143, 117, 121);
			array[185][7] = color;
			array[186][7] = color;
			array[187][7] = color;
			color = new Color(177, 92, 31);
			array[185][8] = color;
			array[186][8] = color;
			array[187][8] = color;
			color = new Color(85, 73, 87);
			array[185][9] = color;
			array[186][9] = color;
			array[187][9] = color;
			array[227][0] = new Color(74, 197, 155);
			array[227][1] = new Color(54, 153, 88);
			array[227][2] = new Color(63, 126, 207);
			array[227][3] = new Color(240, 180, 4);
			array[227][4] = new Color(45, 68, 168);
			array[227][5] = new Color(61, 92, 0);
			array[227][6] = new Color(216, 112, 152);
			array[227][7] = new Color(200, 40, 24);
			array[323][0] = new Color(182, 141, 86);
			array[325][0] = new Color(129, 125, 93);
			array[326][0] = new Color(9, 61, 191);
			array[327][0] = new Color(253, 32, 3);
			array[330][0] = new Color(226, 118, 76);
			array[331][0] = new Color(161, 172, 173);
			array[332][0] = new Color(204, 181, 72);
			array[333][0] = new Color(190, 190, 178);
			array[335][0] = new Color(217, 174, 137);
			array[336][0] = new Color(253, 62, 3);
			array[337][0] = new Color(144, 148, 144);
			array[338][0] = new Color(85, 255, 160);
			array[315][0] = new Color(235, 114, 80);
			Color[] array2 = new Color[]
			{
				new Color(9, 61, 191),
				new Color(253, 32, 3),
				new Color(254, 194, 20)
			};
			Color[][] array3 = new Color[172][];
			for (int j = 0; j < 172; j++)
			{
				array3[j] = new Color[2];
			}
			array3[158][0] = new Color(107, 49, 154);
			array3[163][0] = new Color(154, 148, 49);
			array3[162][0] = new Color(49, 49, 154);
			array3[160][0] = new Color(49, 154, 68);
			array3[161][0] = new Color(154, 49, 77);
			array3[159][0] = new Color(85, 89, 118);
			array3[157][0] = new Color(154, 83, 49);
			array3[154][0] = new Color(221, 79, 255);
			array3[166][0] = new Color(250, 255, 79);
			array3[165][0] = new Color(79, 102, 255);
			array3[156][0] = new Color(79, 255, 89);
			array3[164][0] = new Color(255, 79, 79);
			array3[155][0] = new Color(240, 240, 247);
			array3[153][0] = new Color(255, 145, 79);
			array3[169][0] = new Color(5, 5, 5);
			array3[170][0] = new Color(59, 39, 22);
			array3[171][0] = new Color(59, 39, 22);
			color = new Color(52, 52, 52);
			array3[1][0] = color;
			array3[53][0] = color;
			array3[52][0] = color;
			array3[51][0] = color;
			array3[50][0] = color;
			array3[49][0] = color;
			array3[48][0] = color;
			array3[44][0] = color;
			array3[5][0] = color;
			color = new Color(88, 61, 46);
			array3[2][0] = color;
			array3[16][0] = color;
			array3[59][0] = color;
			array3[3][0] = new Color(61, 58, 78);
			array3[4][0] = new Color(73, 51, 36);
			array3[6][0] = new Color(91, 30, 30);
			color = new Color(27, 31, 42);
			array3[7][0] = color;
			array3[17][0] = color;
			color = new Color(32, 40, 45);
			array3[94][0] = color;
			array3[100][0] = color;
			color = new Color(44, 41, 50);
			array3[95][0] = color;
			array3[101][0] = color;
			color = new Color(31, 39, 26);
			array3[8][0] = color;
			array3[18][0] = color;
			color = new Color(36, 45, 44);
			array3[98][0] = color;
			array3[104][0] = color;
			color = new Color(38, 49, 50);
			array3[99][0] = color;
			array3[105][0] = color;
			color = new Color(41, 28, 36);
			array3[9][0] = color;
			array3[19][0] = color;
			color = new Color(72, 50, 77);
			array3[96][0] = color;
			array3[102][0] = color;
			color = new Color(78, 50, 69);
			array3[97][0] = color;
			array3[103][0] = color;
			array3[10][0] = new Color(74, 62, 12);
			array3[11][0] = new Color(46, 56, 59);
			array3[12][0] = new Color(75, 32, 11);
			array3[13][0] = new Color(67, 37, 37);
			color = new Color(15, 15, 15);
			array3[14][0] = color;
			array3[20][0] = color;
			array3[15][0] = new Color(52, 43, 45);
			array3[22][0] = new Color(113, 99, 99);
			array3[23][0] = new Color(38, 38, 43);
			array3[24][0] = new Color(53, 39, 41);
			array3[25][0] = new Color(11, 35, 62);
			array3[26][0] = new Color(21, 63, 70);
			array3[27][0] = new Color(88, 61, 46);
			array3[27][1] = new Color(52, 52, 52);
			array3[28][0] = new Color(81, 84, 101);
			array3[29][0] = new Color(88, 23, 23);
			array3[30][0] = new Color(28, 88, 23);
			array3[31][0] = new Color(78, 87, 99);
			color = new Color(69, 67, 41);
			array3[34][0] = color;
			array3[37][0] = color;
			array3[32][0] = new Color(86, 17, 40);
			array3[33][0] = new Color(49, 47, 83);
			array3[35][0] = new Color(51, 51, 70);
			array3[36][0] = new Color(87, 59, 55);
			array3[38][0] = new Color(49, 57, 49);
			array3[39][0] = new Color(78, 79, 73);
			array3[45][0] = new Color(60, 59, 51);
			array3[46][0] = new Color(48, 57, 47);
			array3[47][0] = new Color(71, 77, 85);
			array3[40][0] = new Color(85, 102, 103);
			array3[41][0] = new Color(52, 50, 62);
			array3[42][0] = new Color(71, 42, 44);
			array3[43][0] = new Color(73, 66, 50);
			array3[54][0] = new Color(40, 56, 50);
			array3[55][0] = new Color(49, 48, 36);
			array3[56][0] = new Color(43, 33, 32);
			array3[57][0] = new Color(31, 40, 49);
			array3[58][0] = new Color(48, 35, 52);
			array3[60][0] = new Color(1, 52, 20);
			array3[61][0] = new Color(55, 39, 26);
			array3[62][0] = new Color(39, 33, 26);
			array3[69][0] = new Color(43, 42, 68);
			array3[70][0] = new Color(30, 70, 80);
			color = new Color(30, 80, 48);
			array3[63][0] = color;
			array3[65][0] = color;
			array3[66][0] = color;
			array3[68][0] = color;
			color = new Color(53, 80, 30);
			array3[64][0] = color;
			array3[67][0] = color;
			array3[78][0] = new Color(63, 39, 26);
			array3[71][0] = new Color(78, 105, 135);
			array3[72][0] = new Color(52, 84, 12);
			array3[73][0] = new Color(190, 204, 223);
			color = new Color(64, 62, 80);
			array3[74][0] = color;
			array3[80][0] = color;
			array3[75][0] = new Color(65, 65, 35);
			array3[76][0] = new Color(20, 46, 104);
			array3[77][0] = new Color(61, 13, 16);
			array3[79][0] = new Color(51, 47, 96);
			array3[81][0] = new Color(101, 51, 51);
			array3[82][0] = new Color(77, 64, 34);
			array3[83][0] = new Color(62, 38, 41);
			array3[84][0] = new Color(48, 78, 93);
			array3[85][0] = new Color(54, 63, 69);
			color = new Color(138, 73, 38);
			array3[86][0] = color;
			array3[108][0] = color;
			color = new Color(50, 15, 8);
			array3[87][0] = color;
			array3[112][0] = color;
			array3[109][0] = new Color(94, 25, 17);
			array3[110][0] = new Color(125, 36, 122);
			array3[111][0] = new Color(51, 35, 27);
			array3[113][0] = new Color(135, 58, 0);
			array3[114][0] = new Color(65, 52, 15);
			array3[115][0] = new Color(39, 42, 51);
			array3[116][0] = new Color(89, 26, 27);
			array3[117][0] = new Color(126, 123, 115);
			array3[118][0] = new Color(8, 50, 19);
			array3[119][0] = new Color(95, 21, 24);
			array3[120][0] = new Color(17, 31, 65);
			array3[121][0] = new Color(192, 173, 143);
			array3[122][0] = new Color(114, 114, 131);
			array3[123][0] = new Color(136, 119, 7);
			array3[124][0] = new Color(8, 72, 3);
			array3[125][0] = new Color(117, 132, 82);
			array3[126][0] = new Color(100, 102, 114);
			array3[127][0] = new Color(30, 118, 226);
			array3[128][0] = new Color(93, 6, 102);
			array3[129][0] = new Color(64, 40, 169);
			array3[130][0] = new Color(39, 34, 180);
			array3[131][0] = new Color(87, 94, 125);
			array3[132][0] = new Color(6, 6, 6);
			array3[133][0] = new Color(69, 72, 186);
			array3[134][0] = new Color(130, 62, 16);
			array3[135][0] = new Color(22, 123, 163);
			array3[136][0] = new Color(40, 86, 151);
			array3[137][0] = new Color(183, 75, 15);
			array3[138][0] = new Color(83, 80, 100);
			array3[139][0] = new Color(115, 65, 68);
			array3[140][0] = new Color(119, 108, 81);
			array3[141][0] = new Color(59, 67, 71);
			array3[142][0] = new Color(17, 172, 143);
			array3[143][0] = new Color(90, 112, 105);
			array3[144][0] = new Color(62, 28, 87);
			array3[146][0] = new Color(120, 59, 19);
			array3[147][0] = new Color(59, 59, 59);
			array3[148][0] = new Color(229, 218, 161);
			array3[149][0] = new Color(73, 59, 50);
			array3[151][0] = new Color(102, 75, 34);
			array3[167][0] = new Color(70, 68, 51);
			Color[] array4 = new Color[256];
			Color color2 = new Color(50, 40, 255);
			Color color3 = new Color(145, 185, 255);
			for (int k = 0; k < array4.Length; k++)
			{
				float num = (float)k / (float)array4.Length;
				float num2 = 1f - num;
				array4[k] = new Color((int)((byte)((float)color2.R * num2 + (float)color3.R * num)), (int)((byte)((float)color2.G * num2 + (float)color3.G * num)), (int)((byte)((float)color2.B * num2 + (float)color3.B * num)));
			}
			Color[] array5 = new Color[256];
			Color color4 = new Color(88, 61, 46);
			Color color5 = new Color(37, 78, 123);
			for (int l = 0; l < array5.Length; l++)
			{
				float num3 = (float)l / 255f;
				float num4 = 1f - num3;
				array5[l] = new Color((int)((byte)((float)color4.R * num4 + (float)color5.R * num3)), (int)((byte)((float)color4.G * num4 + (float)color5.G * num3)), (int)((byte)((float)color4.B * num4 + (float)color5.B * num3)));
			}
			Color[] array6 = new Color[256];
			Color color6 = new Color(74, 67, 60);
			color5 = new Color(53, 70, 97);
			for (int m = 0; m < array6.Length; m++)
			{
				float num5 = (float)m / 255f;
				float num6 = 1f - num5;
				array6[m] = new Color((int)((byte)((float)color6.R * num6 + (float)color5.R * num5)), (int)((byte)((float)color6.G * num6 + (float)color5.G * num5)), (int)((byte)((float)color6.B * num6 + (float)color5.B * num5)));
			}
			Color color7 = new Color(50, 44, 38);
			int num7 = 0;
			Map.tileOptionCounts = new int[340];
			for (int n = 0; n < 340; n++)
			{
				Color[] array7 = array[n];
				int num8 = 0;
				while (num8 < 10 && !(array7[num8] == Color.Transparent))
				{
					num8++;
				}
				Map.tileOptionCounts[n] = num8;
				num7 += num8;
			}
			Map.wallOptionCounts = new int[172];
			for (int num9 = 0; num9 < 172; num9++)
			{
				Color[] array8 = array3[num9];
				int num10 = 0;
				while (num10 < 2 && !(array8[num10] == Color.Transparent))
				{
					num10++;
				}
				Map.wallOptionCounts[num9] = num10;
				num7 += num10;
			}
			num7 += 773;
			Map.colorLookup = new Color[num7];
			Map.colorLookup[0] = Color.Transparent;
			ushort num11 = 1;
			Map.tilePosition = num11;
			Map.tileLookup = new ushort[340];
			for (int num12 = 0; num12 < 340; num12++)
			{
				if (Map.tileOptionCounts[num12] > 0)
				{
					Color[] arg_43EC_0 = array[num12];
					Map.tileLookup[num12] = num11;
					for (int num13 = 0; num13 < Map.tileOptionCounts[num12]; num13++)
					{
						Map.colorLookup[(int)num11] = array[num12][num13];
						num11 += 1;
					}
				}
				else
				{
					Map.tileLookup[num12] = 0;
				}
			}
			Map.wallPosition = num11;
			Map.wallLookup = new ushort[172];
			Map.wallRangeStart = num11;
			for (int num14 = 0; num14 < 172; num14++)
			{
				if (Map.wallOptionCounts[num14] > 0)
				{
					Color[] arg_4481_0 = array3[num14];
					Map.wallLookup[num14] = num11;
					for (int num15 = 0; num15 < Map.wallOptionCounts[num14]; num15++)
					{
						Map.colorLookup[(int)num11] = array3[num14][num15];
						num11 += 1;
					}
				}
				else
				{
					Map.wallLookup[num14] = 0;
				}
			}
			Map.wallRangeEnd = num11;
			Map.liquidPosition = num11;
			for (int num16 = 0; num16 < 3; num16++)
			{
				Map.colorLookup[(int)num11] = array2[num16];
				num11 += 1;
			}
			Map.skyPosition = num11;
			for (int num17 = 0; num17 < 256; num17++)
			{
				Map.colorLookup[(int)num11] = array4[num17];
				num11 += 1;
			}
			Map.dirtPosition = num11;
			for (int num18 = 0; num18 < 256; num18++)
			{
				Map.colorLookup[(int)num11] = array5[num18];
				num11 += 1;
			}
			Map.rockPosition = num11;
			for (int num19 = 0; num19 < 256; num19++)
			{
				Map.colorLookup[(int)num11] = array6[num19];
				num11 += 1;
			}
			Map.hellPosition = num11;
			Map.colorLookup[(int)num11] = color7;
			Map.snowTypes = new ushort[6];
			Map.snowTypes[0] = Map.tileLookup[147];
			Map.snowTypes[1] = Map.tileLookup[161];
			Map.snowTypes[2] = Map.tileLookup[162];
			Map.snowTypes[3] = Map.tileLookup[163];
			Map.snowTypes[4] = Map.tileLookup[164];
			Map.snowTypes[5] = Map.tileLookup[200];
		}
		public static int TileToLookup(int tileType, int option)
		{
			if (tileType < 0 || tileType > 340)
			{
				throw new ArgumentOutOfRangeException("Map.TileToLookup passed a bad tileType: " + tileType);
			}
			int num = (int)Map.tileLookup[tileType];
			int num2 = Map.tileOptionCounts[tileType];
			if (option < 0)
			{
				throw new ArgumentOutOfRangeException("Map.TileToLookup passed a bad option: " + option);
			}
			if (option >= num2)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Map.TileToLookup passed an option for tileType ",
					tileType,
					" that does not exist: ",
					option
				}));
			}
			return num + option;
		}
		public static void clearMap()
		{
			try
			{
				for (int i = 0; i < Main.maxTilesX; i++)
				{
					float num = (float)i / (float)Main.maxTilesX;
					Main.statusText = string.Concat(new object[]
					{
						Lang.gen[65],
						" ",
						(int)(num * 100f + 1f),
						"%"
					});
					for (int j = 0; j < Main.maxTilesY; j++)
					{
						if (Main.map[i, j] != null)
						{
							Main.map[i, j] = null;
						}
					}
				}
			}
			catch
			{
			}
		}
		public static void loadMap()
		{
			Map.saveLock = false;
			if (!Main.mapEnabled)
			{
				return;
			}
			string text = Main.playerPathName.Substring(0, Main.playerPathName.Length - 4);
			string text2 = string.Concat(new object[]
			{
				text,
				Path.DirectorySeparatorChar,
				Main.worldID,
				".map"
			});
			if (!File.Exists(text2))
			{
				return;
			}
			using (FileStream fileStream = new FileStream(text2, FileMode.Open))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					try
					{
						int num = binaryReader.ReadInt32();
						if (num > Main.curRelease)
						{
							try
							{
								binaryReader.Close();
								fileStream.Close();
							}
							catch
							{
							}
						}
						else
						{
							if (num <= 91)
							{
								Map.loadMap_Version1(binaryReader, num);
							}
							else
							{
								Map.loadMap_Version2(binaryReader, num);
							}
							binaryReader.Close();
							fileStream.Close();
							Main.clearMap = true;
							Main.loadMap = true;
							Main.loadMapLock = true;
							Main.refreshMap = false;
						}
					}
					catch (Exception value)
					{
						using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
						{
							streamWriter.WriteLine(DateTime.Now);
							streamWriter.WriteLine(value);
							streamWriter.WriteLine("");
						}
						File.Copy(text2, text2 + ".bad", true);
						Map.clearMap();
					}
				}
			}
		}
		private static void loadMap_Version1(BinaryReader fileIO, int release)
		{
			string a = fileIO.ReadString();
			int num = fileIO.ReadInt32();
			int num2 = fileIO.ReadInt32();
			int num3 = fileIO.ReadInt32();
			if (a != Main.worldName || num != Main.worldID || num3 != Main.maxTilesX || num2 != Main.maxTilesY)
			{
				throw new Exception("Map meta-data is invalid.");
			}
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				float num4 = (float)i / (float)Main.maxTilesX;
				Main.statusText = string.Concat(new object[]
				{
					Lang.gen[67],
					" ",
					(int)(num4 * 100f + 1f),
					"%"
				});
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					bool flag = fileIO.ReadBoolean();
					if (flag)
					{
						Map map = new Map();
						Main.map[i, j] = map;
						int num5;
						if (release > 77)
						{
							num5 = (int)fileIO.ReadUInt16();
						}
						else
						{
							num5 = (int)fileIO.ReadByte();
						}
						map.light = fileIO.ReadByte();
						Map.OldMapHelper oldMapHelper;
						oldMapHelper.misc = fileIO.ReadByte();
						if (release >= 50)
						{
							oldMapHelper.misc2 = fileIO.ReadByte();
						}
						else
						{
							oldMapHelper.misc2 = 0;
						}
						bool flag2 = false;
						int num6 = (int)oldMapHelper.option();
						int num7;
						if (oldMapHelper.active())
						{
							num7 = num6 + (int)Map.tileLookup[num5];
						}
						else
						{
							if (oldMapHelper.water())
							{
								num7 = (int)Map.liquidPosition;
							}
							else
							{
								if (oldMapHelper.lava())
								{
									num7 = (int)(Map.liquidPosition + 1);
								}
								else
								{
									if (oldMapHelper.honey())
									{
										num7 = (int)(Map.liquidPosition + 2);
									}
									else
									{
										if (oldMapHelper.wall())
										{
											num7 = num6 + (int)Map.wallLookup[num5];
										}
										else
										{
											if ((double)j < Main.worldSurface)
											{
												flag2 = true;
												int num8 = (int)((byte)(256.0 * ((double)j / Main.worldSurface)));
												num7 = (int)Map.skyPosition + num8;
											}
											else
											{
												if ((double)j < Main.rockLayer)
												{
													flag2 = true;
													if (num5 > 255)
													{
														num5 = 255;
													}
													num7 = num5 + (int)Map.dirtPosition;
												}
												else
												{
													if (j < Main.maxTilesY - 200)
													{
														flag2 = true;
														if (num5 > 255)
														{
															num5 = 255;
														}
														num7 = num5 + (int)Map.rockPosition;
													}
													else
													{
														num7 = (int)Map.hellPosition;
													}
												}
											}
										}
									}
								}
							}
						}
						map.type = (ushort)num7;
						map.changed = true;
						int k = (int)fileIO.ReadInt16();
						if (map.light == 255)
						{
							while (k > 0)
							{
								k--;
								j++;
								Map map2 = new Map(map);
								if (flag2)
								{
									if ((double)j < Main.worldSurface)
									{
										flag2 = true;
										int num9 = (int)((byte)(256.0 * ((double)j / Main.worldSurface)));
										num7 = (int)Map.skyPosition + num9;
									}
									else
									{
										if ((double)j < Main.rockLayer)
										{
											flag2 = true;
											num7 = num5 + (int)Map.dirtPosition;
										}
										else
										{
											if (j < Main.maxTilesY - 200)
											{
												flag2 = true;
												num7 = num5 + (int)Map.rockPosition;
											}
											else
											{
												flag2 = true;
												num7 = (int)Map.hellPosition;
											}
										}
									}
									map2.type = (ushort)num7;
								}
								Main.map[i, j] = map2;
							}
						}
						else
						{
							while (k > 0)
							{
								j++;
								k--;
								byte b = fileIO.ReadByte();
								if (b > 18)
								{
									Map map3 = new Map(map, b);
									if (flag2)
									{
										if ((double)j < Main.worldSurface)
										{
											flag2 = true;
											int num10 = (int)((byte)(256.0 * ((double)j / Main.worldSurface)));
											num7 = (int)Map.skyPosition + num10;
										}
										else
										{
											if ((double)j < Main.rockLayer)
											{
												flag2 = true;
												num7 = num5 + (int)Map.dirtPosition;
											}
											else
											{
												if (j < Main.maxTilesY - 200)
												{
													flag2 = true;
													num7 = num5 + (int)Map.rockPosition;
												}
												else
												{
													flag2 = true;
													num7 = (int)Map.hellPosition;
												}
											}
										}
										map3.type = (ushort)num7;
									}
									Main.map[i, j] = map3;
								}
							}
						}
					}
					else
					{
						int num11 = (int)fileIO.ReadInt16();
						j += num11;
					}
				}
			}
		}
		private static void loadMap_Version2(BinaryReader fileIO, int release)
		{
			string a = fileIO.ReadString();
			int num = fileIO.ReadInt32();
			int num2 = fileIO.ReadInt32();
			int num3 = fileIO.ReadInt32();
			if (a != Main.worldName || num != Main.worldID || num3 != Main.maxTilesX || num2 != Main.maxTilesY)
			{
				throw new Exception("Map meta-data is invalid.");
			}
			short num4 = fileIO.ReadInt16();
			short num5 = fileIO.ReadInt16();
			short num6 = fileIO.ReadInt16();
			short num7 = fileIO.ReadInt16();
			short num8 = fileIO.ReadInt16();
			short num9 = fileIO.ReadInt16();
			bool[] array = new bool[(int)num4];
			byte b = 0;
			byte b2 = 128;
			for (int i = 0; i < (int)num4; i++)
			{
				if (b2 == 128)
				{
					b = fileIO.ReadByte();
					b2 = 1;
				}
				else
				{
					b2 = (byte)(b2 << 1);
				}
				if ((b & b2) == b2)
				{
					array[i] = true;
				}
			}
			bool[] array2 = new bool[(int)num5];
			b = 0;
			b2 = 128;
			for (int i = 0; i < (int)num5; i++)
			{
				if (b2 == 128)
				{
					b = fileIO.ReadByte();
					b2 = 1;
				}
				else
				{
					b2 = (byte)(b2 << 1);
				}
				if ((b & b2) == b2)
				{
					array2[i] = true;
				}
			}
			byte[] array3 = new byte[(int)num4];
			ushort num10 = 0;
			for (int i = 0; i < (int)num4; i++)
			{
				if (array[i])
				{
					array3[i] = fileIO.ReadByte();
				}
				else
				{
					array3[i] = 1;
				}
				num10 += (ushort)array3[i];
			}
			byte[] array4 = new byte[(int)num5];
			ushort num11 = 0;
			for (int i = 0; i < (int)num5; i++)
			{
				if (array2[i])
				{
					array4[i] = fileIO.ReadByte();
				}
				else
				{
					array4[i] = 1;
				}
				num11 += (ushort)array4[i];
			}
			int num12 = (int)(num10 + num11 + (ushort)num6 + (ushort)num7 + (ushort)num8 + (ushort)num9 + 2);
			ushort[] array5 = new ushort[num12];
			array5[0] = 0;
			ushort num13 = 1;
			ushort num14 = 1;
			ushort num15 = num14;
			for (int i = 0; i < 340; i++)
			{
				if (i < (int)num4)
				{
					int num16 = (int)array3[i];
					int num17 = Map.tileOptionCounts[i];
					for (int j = 0; j < num17; j++)
					{
						if (j < num16)
						{
							array5[(int)num14] = num13;
							num14 += 1;
						}
						num13 += 1;
					}
				}
				else
				{
					num13 += (ushort)Map.tileOptionCounts[i];
				}
			}
			ushort num18 = num14;
			for (int i = 0; i < 172; i++)
			{
				if (i < (int)num5)
				{
					int num19 = (int)array4[i];
					int num20 = Map.wallOptionCounts[i];
					for (int k = 0; k < num20; k++)
					{
						if (k < num19)
						{
							array5[(int)num14] = num13;
							num14 += 1;
						}
						num13 += 1;
					}
				}
				else
				{
					num13 += (ushort)Map.wallOptionCounts[i];
				}
			}
			ushort num21 = num14;
			for (int i = 0; i < 3; i++)
			{
				if (i < (int)num6)
				{
					array5[(int)num14] = num13;
					num14 += 1;
				}
				num13 += 1;
			}
			ushort num22 = num14;
			for (int i = 0; i < 256; i++)
			{
				if (i < (int)num7)
				{
					array5[(int)num14] = num13;
					num14 += 1;
				}
				num13 += 1;
			}
			ushort num23 = num14;
			for (int i = 0; i < 256; i++)
			{
				if (i < (int)num8)
				{
					array5[(int)num14] = num13;
					num14 += 1;
				}
				num13 += 1;
			}
			ushort num24 = num14;
			for (int i = 0; i < 256; i++)
			{
				if (i < (int)num9)
				{
					array5[(int)num14] = num13;
					num14 += 1;
				}
				num13 += 1;
			}
			ushort num25 = num14;
			array5[(int)num14] = num13;
			BinaryReader binaryReader;
			if (release >= 93)
			{
				DeflateStream input = new DeflateStream(fileIO.BaseStream, CompressionMode.Decompress);
				binaryReader = new BinaryReader(input);
			}
			else
			{
				binaryReader = new BinaryReader(fileIO.BaseStream);
			}
			for (int l = 0; l < Main.maxTilesY; l++)
			{
				float num26 = (float)l / (float)Main.maxTilesY;
				Main.statusText = string.Concat(new object[]
				{
					Lang.gen[67],
					" ",
					(int)(num26 * 100f + 1f),
					"%"
				});
				for (int m = 0; m < Main.maxTilesX; m++)
				{
					byte b3 = binaryReader.ReadByte();
					byte b4;
					if ((b3 & 1) == 1)
					{
						b4 = binaryReader.ReadByte();
					}
					else
					{
						b4 = 0;
					}
					byte b5 = (byte)((b3 & 14) >> 1);
					bool flag;
					switch (b5)
					{
					case 0:
						flag = false;
						break;
					case 1:
					case 2:
					case 7:
						flag = true;
						break;
					case 3:
					case 4:
					case 5:
						flag = false;
						break;
					case 6:
						flag = false;
						break;
					default:
						flag = false;
						break;
					}
					ushort num27;
					if (flag)
					{
						if ((b3 & 16) == 16)
						{
							num27 = binaryReader.ReadUInt16();
						}
						else
						{
							num27 = (ushort)binaryReader.ReadByte();
						}
					}
					else
					{
						num27 = 0;
					}
					byte b6;
					if ((b3 & 32) == 32)
					{
						b6 = binaryReader.ReadByte();
					}
					else
					{
						b6 = 255;
					}
					int n;
					switch ((byte)((b3 & 192) >> 6))
					{
					case 0:
						n = 0;
						break;
					case 1:
						n = (int)binaryReader.ReadByte();
						break;
					case 2:
						n = (int)binaryReader.ReadInt16();
						break;
					default:
						n = 0;
						break;
					}
					if (b5 == 0)
					{
						m += n;
					}
					else
					{
						switch (b5)
						{
						case 1:
							num27 += num15;
							break;
						case 2:
							num27 += num18;
							break;
						case 3:
						case 4:
						case 5:
							num27 += num21 + (ushort)(b5 - 3);
							break;
						case 6:
							if ((double)l < Main.worldSurface)
							{
								ushort num28 = (ushort)((double)num7 * ((double)l / Main.worldSurface));
								num27 += num22 + num28;
							}
							else
							{
								num27 = num25;
							}
							break;
						case 7:
							if ((double)l < Main.rockLayer)
							{
								num27 += num23;
							}
							else
							{
								num27 += num24;
							}
							break;
						}
						Map map = new Map();
						map.light = b6;
						map.color = (byte)(b4 >> 1 & 31);
						map.type = array5[(int)num27];
						Main.map[m, l] = map;
						if (b6 == 255)
						{
							while (n > 0)
							{
								m++;
								Main.map[m, l] = new Map(map);
								n--;
							}
						}
						else
						{
							while (n > 0)
							{
								m++;
								Main.map[m, l] = new Map(map, binaryReader.ReadByte());
								n--;
							}
						}
					}
				}
			}
			binaryReader.Close();
		}
		public static void saveMap()
		{
			if (!Main.mapEnabled)
			{
				return;
			}
			if (Map.saveLock)
			{
				return;
			}
			string text = Main.playerPathName.Substring(0, Main.playerPathName.Length - 4);
			DeflateStream deflateStream = null;
			lock (Map.padlock)
			{
				try
				{
					Map.saveLock = true;
					try
					{
						Directory.CreateDirectory(text);
					}
					catch
					{
					}
					text = string.Concat(new object[]
					{
						text,
						Path.DirectorySeparatorChar,
						Main.worldID,
						".map"
					});
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					string text2 = text + ".sav";
					bool flag2 = false;
					if (!Main.gameMenu)
					{
						flag2 = true;
					}
					using (FileStream fileStream = new FileStream(text2, FileMode.Create))
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
						{
							int num = 0;
							byte[] array = new byte[16384];
							binaryWriter.Write(Main.curRelease);
							binaryWriter.Write(Main.worldName);
							binaryWriter.Write(Main.worldID);
							binaryWriter.Write(Main.maxTilesY);
							binaryWriter.Write(Main.maxTilesX);
							binaryWriter.Write(340);
							binaryWriter.Write(172);
							binaryWriter.Write(3);
							binaryWriter.Write(256);
							binaryWriter.Write(256);
							binaryWriter.Write(256);
							byte b = 1;
							byte b2 = 0;
							int i;
							for (i = 0; i < 340; i++)
							{
								if (Map.tileOptionCounts[i] != 1)
								{
									b2 |= b;
								}
								if (b == 128)
								{
									binaryWriter.Write(b2);
									b2 = 0;
									b = 1;
								}
								else
								{
									b = (byte)(b << 1);
								}
							}
							if (b != 1)
							{
								binaryWriter.Write(b2);
							}
							i = 0;
							b = 1;
							b2 = 0;
							while (i < 172)
							{
								if (Map.wallOptionCounts[i] != 1)
								{
									b2 |= b;
								}
								if (b == 128)
								{
									binaryWriter.Write(b2);
									b2 = 0;
									b = 1;
								}
								else
								{
									b = (byte)(b << 1);
								}
								i++;
							}
							if (b != 1)
							{
								binaryWriter.Write(b2);
							}
							for (i = 0; i < 340; i++)
							{
								if (Map.tileOptionCounts[i] != 1)
								{
									binaryWriter.Write((byte)Map.tileOptionCounts[i]);
								}
							}
							for (i = 0; i < 172; i++)
							{
								if (Map.wallOptionCounts[i] != 1)
								{
									binaryWriter.Write((byte)Map.wallOptionCounts[i]);
								}
							}
							deflateStream = new DeflateStream(binaryWriter.BaseStream, CompressionMode.Compress);
							for (int j = 0; j < Main.maxTilesY; j++)
							{
								if (!flag2)
								{
									float num2 = (float)j / (float)Main.maxTilesY;
									Main.statusText = string.Concat(new object[]
									{
										Lang.gen[66],
										" ",
										(int)(num2 * 100f + 1f),
										"%"
									});
								}
								for (int k = 0; k < Main.maxTilesX; k++)
								{
									Map map = Main.map[k, j];
									byte b4;
									byte b3 = b4 = 0;
									bool flag3 = true;
									bool flag4 = true;
									int num3 = 0;
									int num4 = 0;
									byte b5 = 0;
									int num5;
									ushort num6;
									int num7;
									if (map == null || map.light <= 18)
									{
										flag4 = false;
										flag3 = false;
										num5 = 0;
										num6 = 0;
										num7 = 0;
										int num8 = k + 1;
										int l = Main.maxTilesX - k - 1;
										while (l > 0)
										{
											Map map2 = Main.map[num8, j];
											if (map2 != null && map2.light > 18)
											{
												break;
											}
											num7++;
											l--;
											num8++;
										}
									}
									else
									{
										b5 = map.color;
										num6 = map.type;
										if (num6 < Map.wallPosition)
										{
											num5 = 1;
											num6 -= Map.tilePosition;
										}
										else
										{
											if (num6 < Map.liquidPosition)
											{
												num5 = 2;
												num6 -= Map.wallPosition;
											}
											else
											{
												if (num6 < Map.skyPosition)
												{
													num5 = (int)(3 + (num6 - Map.liquidPosition));
													flag3 = false;
												}
												else
												{
													if (num6 < Map.dirtPosition)
													{
														num5 = 6;
														flag4 = false;
														flag3 = false;
													}
													else
													{
														if (num6 < Map.hellPosition)
														{
															num5 = 7;
															if (num6 < Map.rockPosition)
															{
																num6 -= Map.dirtPosition;
															}
															else
															{
																num6 -= Map.rockPosition;
															}
														}
														else
														{
															num5 = 6;
															flag3 = false;
														}
													}
												}
											}
										}
										if (map.light == 255)
										{
											flag4 = false;
										}
										if (flag4)
										{
											num7 = 0;
											int num8 = k + 1;
											int l = Main.maxTilesX - k - 1;
											num3 = num8;
											while (l > 0)
											{
												if (!map.isTheSameAs_NoLight(Main.map[num8, j]))
												{
													num4 = num8;
													break;
												}
												l--;
												num7++;
												num8++;
											}
										}
										else
										{
											num7 = 0;
											int num8 = k + 1;
											int l = Main.maxTilesX - k - 1;
											while (l > 0 && map.isTheSameAs(Main.map[num8, j]))
											{
												l--;
												num7++;
												num8++;
											}
										}
									}
									if (b5 > 0)
									{
										b3 |= (byte)(b5 << 1);
									}
									if (b3 != 0)
									{
										b4 |= 1;
									}
									b4 |= (byte)(num5 << 1);
									if (flag3 && num6 > 255)
									{
										b4 |= 16;
									}
									if (flag4)
									{
										b4 |= 32;
									}
									if (num7 > 0)
									{
										if (num7 > 255)
										{
											b4 |= 128;
										}
										else
										{
											b4 |= 64;
										}
									}
									array[num] = b4;
									num++;
									if (b3 != 0)
									{
										array[num] = b3;
										num++;
									}
									if (flag3)
									{
										array[num] = (byte)num6;
										num++;
										if (num6 > 255)
										{
											array[num] = (byte)(num6 >> 8);
											num++;
										}
									}
									if (flag4)
									{
										array[num] = map.light;
										num++;
									}
									if (num7 > 0)
									{
										array[num] = (byte)num7;
										num++;
										if (num7 > 255)
										{
											array[num] = (byte)(num7 >> 8);
											num++;
										}
									}
									for (int m = num3; m < num4; m++)
									{
										array[num] = Main.map[m, j].light;
										num++;
									}
									k += num7;
									if (num >= 4096)
									{
										deflateStream.Write(array, 0, num);
										num = 0;
									}
								}
							}
							if (num > 0)
							{
								deflateStream.Write(array, 0, num);
							}
							deflateStream.Close();
							binaryWriter.Close();
							fileStream.Close();
							File.Copy(text2, text, true);
							File.Delete(text2);
						}
					}
				}
				catch (Exception value)
				{
					using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
					{
						streamWriter.WriteLine(DateTime.Now);
						streamWriter.WriteLine(value);
						streamWriter.WriteLine("");
					}
					if (deflateStream != null)
					{
						deflateStream.Close();
					}
				}
				Map.saveLock = false;
			}
		}
	}
}
