using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
namespace Terraria
{
	public class WaterfallManager
	{
		public struct WaterfallData
		{
			public int x;
			public int y;
			public int type;
			public int stopAtStep;
		}
		private const int minWet = 160;
		private const int maxCount = 200;
		private const int maxLength = 100;
		private const int maxTypes = 22;
		private int qualityMax;
		private int currentMax;
		private WaterfallManager.WaterfallData[] waterfalls;
		public Texture2D[] waterfallTexture = new Texture2D[22];
		private int wFallFrCounter;
		private int regularFrame;
		private int wFallFrCounter2;
		private int slowFrame;
		private int rainFrameCounter;
		private int rainFrameForeground;
		private int rainFrameBackground;
		private int findWaterfallCount;
		private int waterfallDist = 100;
		public WaterfallManager()
		{
			this.waterfalls = new WaterfallManager.WaterfallData[200];
		}
		public void LoadContent(ContentManager manager)
		{
			for (int i = 0; i < 22; i++)
			{
				this.waterfallTexture[i] = manager.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Waterfall_",
					i
				}));
			}
		}
		public bool CheckForWaterfall(int i, int j)
		{
			for (int k = 0; k < this.currentMax; k++)
			{
				if (this.waterfalls[k].x == i && this.waterfalls[k].y == j)
				{
					return true;
				}
			}
			return false;
		}
		public void FindWaterfalls()
		{
			this.findWaterfallCount++;
			if (this.findWaterfallCount < 30)
			{
				return;
			}
			this.findWaterfallCount = 0;
			this.waterfallDist = (int)(75f * Main.gfxQuality) + 25;
			this.qualityMax = (int)(175f * Main.gfxQuality) + 25;
			this.currentMax = 0;
			int num = (int)(Main.screenPosition.X / 16f - 1f);
			int num2 = (int)((Main.screenPosition.X + (float)Main.screenWidth) / 16f) + 2;
			int num3 = (int)(Main.screenPosition.Y / 16f - 1f);
			int num4 = (int)((Main.screenPosition.Y + (float)Main.screenHeight) / 16f) + 2;
			num -= this.waterfallDist;
			num2 += this.waterfallDist;
			num3 -= this.waterfallDist;
			num4 += 20;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile == null)
					{
						tile = new Tile();
						Main.tile[i, j] = tile;
					}
					if (tile.active())
					{
						if (tile.halfBrick())
						{
							Tile tile2 = Main.tile[i, j - 1];
							if (tile2 == null)
							{
								tile2 = new Tile();
								Main.tile[i, j - 1] = tile2;
							}
							if (tile2.liquid < 16 || WorldGen.SolidTile(tile2))
							{
								Tile tile3 = Main.tile[i + 1, j];
								if (tile3 == null)
								{
									tile3 = new Tile();
									Main.tile[i - 1, j] = tile3;
								}
								Tile tile4 = Main.tile[i - 1, j];
								if (tile4 == null)
								{
									tile4 = new Tile();
									Main.tile[i + 1, j] = tile4;
								}
								if ((tile3.liquid > 160 || tile4.liquid > 160) && ((tile3.liquid == 0 && !WorldGen.SolidTile(tile3) && tile3.slope() == 0) || (tile4.liquid == 0 && !WorldGen.SolidTile(tile4) && tile4.slope() == 0)) && this.currentMax < this.qualityMax)
								{
									this.waterfalls[this.currentMax].type = 0;
									if (tile2.lava() || tile4.lava() || tile3.lava())
									{
										this.waterfalls[this.currentMax].type = 1;
									}
									else
									{
										if (tile2.honey() || tile4.honey() || tile3.honey())
										{
											this.waterfalls[this.currentMax].type = 14;
										}
										else
										{
											this.waterfalls[this.currentMax].type = 0;
										}
									}
									this.waterfalls[this.currentMax].x = i;
									this.waterfalls[this.currentMax].y = j;
									this.currentMax++;
								}
							}
						}
						if (tile.type == 196)
						{
							Tile tile5 = Main.tile[i, j + 1];
							if (tile5 == null)
							{
								tile5 = new Tile();
								Main.tile[i, j + 1] = tile5;
							}
							if (!WorldGen.SolidTile(tile5) && tile5.slope() == 0 && this.currentMax < this.qualityMax)
							{
								this.waterfalls[this.currentMax].type = 11;
								this.waterfalls[this.currentMax].x = i;
								this.waterfalls[this.currentMax].y = j + 1;
								this.currentMax++;
							}
						}
					}
				}
			}
		}
		public void UpdateFrame()
		{
			this.wFallFrCounter++;
			if (this.wFallFrCounter > 2)
			{
				this.wFallFrCounter = 0;
				this.regularFrame++;
				if (this.regularFrame > 15)
				{
					this.regularFrame = 0;
				}
			}
			this.wFallFrCounter2++;
			if (this.wFallFrCounter2 > 6)
			{
				this.wFallFrCounter2 = 0;
				this.slowFrame++;
				if (this.slowFrame > 15)
				{
					this.slowFrame = 0;
				}
			}
			this.rainFrameCounter++;
			if (this.rainFrameCounter > 0)
			{
				this.rainFrameForeground++;
				if (this.rainFrameForeground > 7)
				{
					this.rainFrameForeground -= 8;
				}
				if (this.rainFrameCounter > 2)
				{
					this.rainFrameCounter = 0;
					this.rainFrameBackground--;
					if (this.rainFrameBackground < 0)
					{
						this.rainFrameBackground = 7;
					}
				}
			}
		}
		private void DrawWaterfall(SpriteBatch spriteBatch, int Style = 0, float Alpha = 1f)
		{
			float num = 0f;
			float num2 = 99999f;
			float num3 = 99999f;
			int num4 = -1;
			int num5 = -1;
			float num6 = 0f;
			float num7 = 99999f;
			float num8 = 99999f;
			int num9 = -1;
			int num10 = -1;
			int i = 0;
			while (i < this.currentMax)
			{
				int num11 = 0;
				int num12 = this.waterfalls[i].type;
				int num13 = this.waterfalls[i].x;
				int num14 = this.waterfalls[i].y;
				int num15 = 0;
				int num16 = 0;
				int num17 = 0;
				int num18 = 0;
				int num19 = 0;
				int num20 = 0;
				int num22;
				if (num12 == 1 || num12 == 14)
				{
					if (!Main.drewLava && this.waterfalls[i].stopAtStep != 0)
					{
						int num21 = 32 * this.slowFrame;
						goto IL_3FA;
					}
				}
				else
				{
					if (num12 != 11)
					{
						if (num12 == 0)
						{
							num12 = Style;
						}
						else
						{
							if (num12 == 2 && Main.drewLava)
							{
								goto IL_1879;
							}
						}
						int num21 = 32 * this.regularFrame;
						goto IL_3FA;
					}
					if (!Main.drewLava)
					{
						num22 = this.waterfallDist / 4;
						if (this.waterfalls[i].stopAtStep > num22)
						{
							this.waterfalls[i].stopAtStep = num22;
						}
						if (this.waterfalls[i].stopAtStep != 0 && (float)(num14 + num22) >= Main.screenPosition.Y / 16f && (float)num13 >= Main.screenPosition.X / 16f - 1f && (float)num13 <= (Main.screenPosition.X + (float)Main.screenWidth) / 16f + 1f)
						{
							int num23;
							int num24;
							if (num13 % 2 == 0)
							{
								num23 = this.rainFrameForeground + 3;
								if (num23 > 7)
								{
									num23 -= 8;
								}
								num24 = this.rainFrameBackground + 2;
								if (num24 > 7)
								{
									num24 -= 8;
								}
							}
							else
							{
								num23 = this.rainFrameForeground;
								num24 = this.rainFrameBackground;
							}
							Rectangle value = new Rectangle(num24 * 18, 0, 16, 16);
							Rectangle value2 = new Rectangle(num23 * 18, 0, 16, 16);
							Vector2 origin = new Vector2(8f, 8f);
							Vector2 position;
							if (num14 % 2 == 0)
							{
								position = new Vector2((float)(num13 * 16 + 9), (float)(num14 * 16 + 8)) - Main.screenPosition;
							}
							else
							{
								position = new Vector2((float)(num13 * 16 + 8), (float)(num14 * 16 + 8)) - Main.screenPosition;
							}
							bool flag = false;
							for (int j = 0; j < num22; j++)
							{
								Color color = Lighting.GetColor(num13, num14);
								float num25 = 0.6f;
								float num26 = 0.3f;
								if (j > num22 - 8)
								{
									float num27 = (float)(num22 - j) / 8f;
									num25 *= num27;
									num26 *= num27;
								}
								Color color2 = color * num25;
								Color color3 = color * num26;
								spriteBatch.Draw(this.waterfallTexture[12], position, new Rectangle?(value), color3, 0f, origin, 1f, SpriteEffects.None, 0f);
								spriteBatch.Draw(this.waterfallTexture[11], position, new Rectangle?(value2), color2, 0f, origin, 1f, SpriteEffects.None, 0f);
								if (flag)
								{
									break;
								}
								num14++;
								Tile tile = Main.tile[num13, num14];
								if (WorldGen.SolidTile(tile))
								{
									flag = true;
								}
								if (tile.liquid > 0)
								{
									int num28 = (int)(16f * ((float)tile.liquid / 255f)) & 254;
									if (num28 >= 15)
									{
										break;
									}
									value2.Height -= num28;
									value.Height -= num28;
								}
								if (num14 % 2 == 0)
								{
									position.X += 1f;
								}
								else
								{
									position.X -= 1f;
								}
								position.Y += 16f;
							}
							this.waterfalls[i].stopAtStep = 0;
						}
					}
				}
				IL_1879:
				i++;
				continue;
				IL_3FA:
				int num29 = 0;
				num22 = this.waterfallDist;
				Color color4 = Color.White;
				for (int k = 0; k < num22; k++)
				{
					if (num29 < 2)
					{
						int num30 = num12;
						switch (num30)
						{
						case 1:
						{
							float num31 = 0.55f;
							num31 += (float)(270 - (int)Main.mouseTextColor) / 900f;
							num31 *= 0.4f;
							float num32 = num31;
							float num33 = num31 * 0.3f;
							float num34 = num31 * 0.1f;
							Lighting.addLight(num13, num14, num32, num33, num34);
							break;
						}
						case 2:
						{
							float num32 = (float)Main.DiscoR / 255f;
							float num33 = (float)Main.DiscoG / 255f;
							float num34 = (float)Main.DiscoB / 255f;
							num32 *= 0.2f;
							num33 *= 0.2f;
							num34 *= 0.2f;
							Lighting.addLight(num13, num14, num32, num33, num34);
							break;
						}
						default:
							switch (num30)
							{
							case 15:
							{
								float num32 = 0f;
								float num33 = 0f;
								float num34 = 0.2f;
								Lighting.addLight(num13, num14, num32, num33, num34);
								break;
							}
							case 16:
							{
								float num32 = 0f;
								float num33 = 0.2f;
								float num34 = 0f;
								Lighting.addLight(num13, num14, num32, num33, num34);
								break;
							}
							case 17:
							{
								float num32 = 0f;
								float num33 = 0f;
								float num34 = 0.2f;
								Lighting.addLight(num13, num14, num32, num33, num34);
								break;
							}
							case 18:
							{
								float num32 = 0f;
								float num33 = 0.2f;
								float num34 = 0f;
								Lighting.addLight(num13, num14, num32, num33, num34);
								break;
							}
							case 19:
							{
								float num32 = 0.2f;
								float num33 = 0f;
								float num34 = 0f;
								Lighting.addLight(num13, num14, num32, num33, num34);
								break;
							}
							case 20:
								Lighting.addLight(num13, num14, 0.2f, 0.2f, 0.2f);
								break;
							case 21:
							{
								float num32 = 0.2f;
								float num33 = 0f;
								float num34 = 0f;
								Lighting.addLight(num13, num14, num32, num33, num34);
								break;
							}
							}
							break;
						}
						Tile tile2 = Main.tile[num13, num14];
						if (tile2 == null)
						{
							tile2 = new Tile();
							Main.tile[num13, num14] = tile2;
						}
						Tile tile3 = Main.tile[num13 - 1, num14];
						if (tile3 == null)
						{
							tile3 = new Tile();
							Main.tile[num13 - 1, num14] = tile3;
						}
						Tile tile4 = Main.tile[num13, num14 + 1];
						if (tile4 == null)
						{
							tile4 = new Tile();
							Main.tile[num13, num14 + 1] = tile4;
						}
						Tile tile5 = Main.tile[num13 + 1, num14];
						if (tile5 == null)
						{
							tile5 = new Tile();
							Main.tile[num13 + 1, num14] = tile5;
						}
						int num35 = (int)(tile2.liquid / 16);
						int num36 = 0;
						int num37 = num18;
						int num38;
						int num39;
						if (tile4.topSlope())
						{
							if (tile4.slope() == 1)
							{
								num36 = 1;
								num38 = 1;
								num17 = 1;
								num18 = num17;
							}
							else
							{
								num36 = -1;
								num38 = -1;
								num17 = -1;
								num18 = num17;
							}
							num39 = 1;
						}
						else
						{
							if ((((!WorldGen.SolidTile(tile4) && !tile4.bottomSlope()) || tile4.type == 162) && !tile2.halfBrick()) || (!tile4.active() && !tile2.halfBrick()))
							{
								num29 = 0;
								num39 = 1;
								num38 = 0;
							}
							else
							{
								if ((WorldGen.SolidTile(tile3) || tile3.topSlope() || tile3.liquid > 0) && !WorldGen.SolidTile(tile5) && tile5.liquid == 0)
								{
									if (num17 == -1)
									{
										num29++;
									}
									num38 = 1;
									num39 = 0;
									num17 = 1;
								}
								else
								{
									if ((WorldGen.SolidTile(tile5) || tile5.topSlope() || tile5.liquid > 0) && !WorldGen.SolidTile(tile3) && tile3.liquid == 0)
									{
										if (num17 == 1)
										{
											num29++;
										}
										num38 = -1;
										num39 = 0;
										num17 = -1;
									}
									else
									{
										if (((!WorldGen.SolidTile(tile5) && !tile2.topSlope()) || tile5.liquid == 0) && !WorldGen.SolidTile(tile3) && !tile2.topSlope() && tile3.liquid == 0)
										{
											num39 = 0;
											num38 = num17;
										}
										else
										{
											num29++;
											num39 = 0;
											num38 = 0;
										}
									}
								}
							}
						}
						if (num29 >= 2)
						{
							num17 *= -1;
							num38 *= -1;
						}
						int num40 = -1;
						if (num12 != 1 && num12 != 14)
						{
							if (tile4.active())
							{
								num40 = (int)tile4.type;
							}
							if (tile2.active())
							{
								num40 = (int)tile2.type;
							}
						}
						if (num40 != -1)
						{
							if (num40 == 160)
							{
								num12 = 2;
							}
							else
							{
								if (num40 >= 262 && num40 <= 268)
								{
									num12 = 15 + num40 - 262;
								}
							}
						}
						if (WorldGen.SolidTile(tile4) && !tile2.halfBrick())
						{
							num11 = 8;
						}
						else
						{
							if (num16 != 0)
							{
								num11 = 0;
							}
						}
						Color color5 = Lighting.GetColor(num13, num14);
						Color color6 = color5;
						float num41;
						if (num12 == 1)
						{
							num41 = 1f;
						}
						else
						{
							if (num12 == 14)
							{
								num41 = 0.8f;
							}
							else
							{
								if (tile2.wall == 0 && (double)num14 < Main.worldSurface)
								{
									num41 = Alpha;
								}
								else
								{
									num41 = 0.6f * Alpha;
								}
							}
						}
						if (k > num22 - 10)
						{
							num41 *= (float)(num22 - k) / 10f;
						}
						float num42 = (float)color5.R * num41;
						float num43 = (float)color5.G * num41;
						float num44 = (float)color5.B * num41;
						float num45 = (float)color5.A * num41;
						if (num12 == 1)
						{
							if (num42 < 190f * num41)
							{
								num42 = 190f * num41;
							}
							if (num43 < 190f * num41)
							{
								num43 = 190f * num41;
							}
							if (num44 < 190f * num41)
							{
								num44 = 190f * num41;
							}
						}
						else
						{
							if (num12 == 2)
							{
								num42 = (float)Main.DiscoR * num41;
								num43 = (float)Main.DiscoG * num41;
								num44 = (float)Main.DiscoB * num41;
							}
							else
							{
								if (num12 >= 15 && num12 <= 21)
								{
									num42 = 255f * num41;
									num43 = 255f * num41;
									num44 = 255f * num41;
								}
							}
						}
						color5 = new Color((int)num42, (int)num43, (int)num44, (int)num45);
						if (num12 == 1)
						{
							float num46 = Math.Abs((float)(num13 * 16 + 8) - (Main.screenPosition.X + (float)(Main.screenWidth / 2)));
							float num47 = Math.Abs((float)(num14 * 16 + 8) - (Main.screenPosition.Y + (float)(Main.screenHeight / 2)));
							if (num46 < (float)(Main.screenWidth * 2) && num47 < (float)(Main.screenHeight * 2))
							{
								float num48 = (float)Math.Sqrt((double)(num46 * num46 + num47 * num47));
								float num49 = 1f - num48 / ((float)Main.screenWidth * 0.75f);
								if (num49 > 0f)
								{
									num6 += num49;
								}
							}
							if (num46 < num7)
							{
								num7 = num46;
								num9 = num13 * 16 + 8;
							}
							if (num47 < num8)
							{
								num8 = num46;
								num10 = num14 * 16 + 8;
							}
						}
						else
						{
							if (num12 != 1 && num12 != 14 && num12 != 11 && num12 != 12)
							{
								float num50 = Math.Abs((float)(num13 * 16 + 8) - (Main.screenPosition.X + (float)(Main.screenWidth / 2)));
								float num51 = Math.Abs((float)(num14 * 16 + 8) - (Main.screenPosition.Y + (float)(Main.screenHeight / 2)));
								if (num50 < (float)(Main.screenWidth * 2) && num51 < (float)(Main.screenHeight * 2))
								{
									float num52 = (float)Math.Sqrt((double)(num50 * num50 + num51 * num51));
									float num53 = 1f - num52 / ((float)Main.screenWidth * 0.75f);
									if (num53 > 0f)
									{
										num += num53;
									}
								}
								if (num50 < num2)
								{
									num2 = num50;
									num4 = num13 * 16 + 8;
								}
								if (num51 < num3)
								{
									num3 = num50;
									num5 = num14 * 16 + 8;
								}
							}
						}
						if (k > 50 && (color6.R > 20 || color6.B > 20 || color6.G > 20))
						{
							float num54 = (float)color6.R;
							if ((float)color6.G > num54)
							{
								num54 = (float)color6.G;
							}
							if ((float)color6.B > num54)
							{
								num54 = (float)color6.B;
							}
							if ((float)Main.rand.Next(20000) < num54 / 30f)
							{
								int num55 = Dust.NewDust(new Vector2((float)(num13 * 16 - num17 * 7), (float)(num14 * 16 + 6)), 10, 8, 43, 0f, 0f, 254, Color.White, 0.5f);
								Main.dust[num55].velocity *= 0f;
							}
						}
						int num21;
						if (num15 == 0 && num36 != 0 && num16 == 1 && num17 != num18)
						{
							num36 = 0;
							num17 = num18;
							color5 = Color.White;
							if (num17 == 1)
							{
								spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 16 - num35)), color5, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
							}
							else
							{
								spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 8)), color5, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
							}
						}
						if (num19 != 0 && num38 == 0 && num39 == 1)
						{
							if (num17 == 1)
							{
								if (num20 != num12)
								{
									spriteBatch.Draw(this.waterfallTexture[num20], new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 0, 16, 16 - num35 - 8)), color4, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
								}
								else
								{
									spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 0, 16, 16 - num35 - 8)), color5, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
								}
							}
							else
							{
								spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 0, 16, 16 - num35 - 8)), color5, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
							}
						}
						if (num11 == 8 && num16 == 1 && num19 == 0)
						{
							if (num18 == -1)
							{
								if (num20 != num12)
								{
									spriteBatch.Draw(this.waterfallTexture[num20], new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 8)), color4, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
								else
								{
									spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 8)), color5, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
							}
							else
							{
								if (num20 != num12)
								{
									spriteBatch.Draw(this.waterfallTexture[num20], new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 8)), color4, 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
								}
								else
								{
									spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 8)), color5, 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
								}
							}
						}
						if (num36 != 0 && num15 == 0)
						{
							if (num37 == 1)
							{
								if (num20 != num12)
								{
									spriteBatch.Draw(this.waterfallTexture[num20], new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 16 - num35)), color4, 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
								}
								else
								{
									spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 16 - num35)), color5, 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
								}
							}
							else
							{
								if (num20 != num12)
								{
									spriteBatch.Draw(this.waterfallTexture[num20], new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 16 - num35)), color4, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
								else
								{
									spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 16 - num35)), color5, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
							}
						}
						if (num39 == 1 && num36 == 0 && num19 == 0)
						{
							if (num17 == -1)
							{
								if (num16 == 0)
								{
									spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 0, 16, 16 - num35)), color5, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
								else
								{
									if (num20 != num12)
									{
										spriteBatch.Draw(this.waterfallTexture[num20], new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 16 - num35)), color4, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
									}
									else
									{
										spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 16 - num35)), color5, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
									}
								}
							}
							else
							{
								if (num16 == 0)
								{
									spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 0, 16, 16 - num35)), color5, 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
								}
								else
								{
									if (num20 != num12)
									{
										spriteBatch.Draw(this.waterfallTexture[num20], new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 16 - num35)), color4, 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
									}
									else
									{
										spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle?(new Rectangle(num21, 24, 32, 16 - num35)), color5, 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
									}
								}
							}
						}
						else
						{
							if (num38 == 1)
							{
								if (Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
								{
									if (num36 == 1)
									{
										for (int l = 0; l < 8; l++)
										{
											int num56 = l * 2;
											int num57 = 14 - l * 2;
											int num58 = num56;
											num11 = 8;
											if (num15 == 0 && l < 2)
											{
												num58 = 4;
											}
											spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16 + num56), (float)(num14 * 16 + num11 + num58)) - Main.screenPosition, new Rectangle?(new Rectangle(16 + num21 + num57, 0, 2, 16 - num11)), color5, 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
										}
									}
									else
									{
										spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle?(new Rectangle(16 + num21, 0, 16, 16)), color5, 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
									}
								}
							}
							else
							{
								if (num38 == -1)
								{
									if (Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
									{
										if (num36 == -1)
										{
											for (int m = 0; m < 8; m++)
											{
												int num59 = m * 2;
												int num60 = m * 2;
												int num61 = 14 - m * 2;
												num11 = 8;
												if (num15 == 0 && m > 5)
												{
													num61 = 4;
												}
												spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16 + num59), (float)(num14 * 16 + num11 + num61)) - Main.screenPosition, new Rectangle?(new Rectangle(16 + num21 + num60, 0, 2, 16 - num11)), color5, 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
											}
										}
										else
										{
											spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle?(new Rectangle(16 + num21, 0, 16, 16)), color5, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
										}
									}
								}
								else
								{
									if (num38 == 0 && num39 == 0)
									{
										if (Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
										{
											spriteBatch.Draw(this.waterfallTexture[num12], new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle?(new Rectangle(16 + num21, 0, 16, 16)), color5, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
										}
										k = 1000;
									}
								}
							}
						}
						if (tile2.liquid > 0 && !tile2.halfBrick())
						{
							k = 1000;
						}
						num16 = num39;
						num18 = num17;
						num15 = num38;
						num13 += num38;
						num14 += num39;
						num19 = num36;
						color4 = color5;
						if (num20 != num12)
						{
							num20 = num12;
						}
						if ((tile3.active() && (tile3.type == 189 || tile3.type == 196)) || (tile5.active() && (tile5.type == 189 || tile5.type == 196)) || (tile4.active() && (tile4.type == 189 || tile4.type == 196)))
						{
							num22 = (int)((float)(40 * (Main.maxTilesX / 4200)) * Main.gfxQuality);
						}
					}
				}
				goto IL_1879;
			}
			Main.ambientWaterfallX = (float)num4;
			Main.ambientWaterfallY = (float)num5;
			Main.ambientWaterfallStrength = num;
			Main.ambientLavafallX = (float)num9;
			Main.ambientLavafallY = (float)num10;
			Main.ambientLavafallStrength = num6;
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < this.currentMax; i++)
			{
				this.waterfalls[i].stopAtStep = this.waterfallDist;
			}
			Main.drewLava = false;
			if (Main.liquidAlpha[0] > 0f)
			{
				this.DrawWaterfall(spriteBatch, 0, Main.liquidAlpha[0]);
			}
			if (Main.liquidAlpha[2] > 0f)
			{
				this.DrawWaterfall(spriteBatch, 3, Main.liquidAlpha[2]);
			}
			if (Main.liquidAlpha[3] > 0f)
			{
				this.DrawWaterfall(spriteBatch, 4, Main.liquidAlpha[3]);
			}
			if (Main.liquidAlpha[4] > 0f)
			{
				this.DrawWaterfall(spriteBatch, 5, Main.liquidAlpha[4]);
			}
			if (Main.liquidAlpha[5] > 0f)
			{
				this.DrawWaterfall(spriteBatch, 6, Main.liquidAlpha[5]);
			}
			if (Main.liquidAlpha[6] > 0f)
			{
				this.DrawWaterfall(spriteBatch, 7, Main.liquidAlpha[6]);
			}
			if (Main.liquidAlpha[7] > 0f)
			{
				this.DrawWaterfall(spriteBatch, 8, Main.liquidAlpha[7]);
			}
			if (Main.liquidAlpha[8] > 0f)
			{
				this.DrawWaterfall(spriteBatch, 9, Main.liquidAlpha[8]);
			}
			if (Main.liquidAlpha[9] > 0f)
			{
				this.DrawWaterfall(spriteBatch, 10, Main.liquidAlpha[9]);
			}
			if (Main.liquidAlpha[10] > 0f)
			{
				this.DrawWaterfall(spriteBatch, 13, Main.liquidAlpha[10]);
			}
		}
	}
}
