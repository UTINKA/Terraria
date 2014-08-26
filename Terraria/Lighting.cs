using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Terraria.DataStructures;
namespace Terraria
{
	public class Lighting
	{
		private class LightingSwipeData
		{
			public int outerLoopStart;
			public int outerLoopEnd;
			public int innerLoop1Start;
			public int innerLoop1End;
			public int innerLoop2Start;
			public int innerLoop2End;
			public Random rand;
			public Action<Lighting.LightingSwipeData> function;
			public Lighting.LightingState[][] jaggedArray;
			public LightingSwipeData()
			{
				this.innerLoop1Start = 0;
				this.outerLoopStart = 0;
				this.innerLoop1End = 0;
				this.outerLoopEnd = 0;
				this.innerLoop2Start = 0;
				this.innerLoop2End = 0;
				this.function = null;
				this.rand = new Random();
			}
			public void CopyFrom(Lighting.LightingSwipeData from)
			{
				this.innerLoop1Start = from.innerLoop1Start;
				this.outerLoopStart = from.outerLoopStart;
				this.innerLoop1End = from.innerLoop1End;
				this.outerLoopEnd = from.outerLoopEnd;
				this.innerLoop2Start = from.innerLoop2Start;
				this.innerLoop2End = from.innerLoop2End;
				this.function = from.function;
				this.jaggedArray = from.jaggedArray;
			}
		}
		private class LightingState
		{
			public float r;
			public float r2;
			public float g;
			public float g2;
			public float b;
			public float b2;
			public bool stopLight;
			public bool wetLight;
			public bool honeyLight;
		}
		private struct ColorTriplet
		{
			public float r;
			public float g;
			public float b;
			public ColorTriplet(float R, float G, float B)
			{
				this.r = R;
				this.g = G;
				this.b = B;
			}
			public ColorTriplet(float averageColor)
			{
				this.b = averageColor;
				this.g = averageColor;
				this.r = averageColor;
			}
		}
		public static int maxRenderCount = 4;
		public static float brightness = 1f;
		public static float defBrightness = 1f;
		public static int lightMode = 0;
		public static bool RGB = true;
		private static float oldSkyColor = 0f;
		private static float skyColor = 0f;
		private static int lightCounter = 0;
		public static int offScreenTiles = 45;
		public static int offScreenTiles2 = 35;
		private static int firstTileX;
		private static int lastTileX;
		private static int firstTileY;
		private static int lastTileY;
		public static int LightingThreads = 0;
		private static Lighting.LightingState[][] states;
		private static Lighting.LightingState[][] axisFlipStates;
		private static Lighting.LightingSwipeData swipe;
		private static Lighting.LightingSwipeData[] threadSwipes;
		private static CountdownEvent countdown;
		public static int scrX;
		public static int scrY;
		public static int minX;
		public static int maxX;
		public static int minY;
		public static int maxY;
		private static int maxTempLights = 2000;
		private static Dictionary<Point16, Lighting.ColorTriplet> tempLights;
		private static int firstToLightX;
		private static int firstToLightY;
		private static int lastToLightX;
		private static int lastToLightY;
		private static float negLight = 0.04f;
		private static float negLight2 = 0.16f;
		private static float wetLightR = 0.16f;
		private static float wetLightG = 0.16f;
		private static float wetLightB = 0.16f;
		private static float honeyLightR = 0.16f;
		private static float honeyLightG = 0.16f;
		private static float honeyLightB = 0.16f;
		private static float blueWave = 1f;
		private static int blueDir = 1;
		private static int minX7;
		private static int maxX7;
		private static int minY7;
		private static int maxY7;
		private static int firstTileX7;
		private static int lastTileX7;
		private static int lastTileY7;
		private static int firstTileY7;
		private static int firstToLightX7;
		private static int lastToLightX7;
		private static int firstToLightY7;
		private static int lastToLightY7;
		private static int firstToLightX27;
		private static int lastToLightX27;
		private static int firstToLightY27;
		private static int lastToLightY27;
		public static void Initialize(bool resize = false)
		{
			if (!resize)
			{
				Lighting.tempLights = new Dictionary<Point16, Lighting.ColorTriplet>();
				Lighting.swipe = new Lighting.LightingSwipeData();
				Lighting.countdown = new CountdownEvent(0);
				Lighting.threadSwipes = new Lighting.LightingSwipeData[Environment.ProcessorCount];
				for (int i = 0; i < Lighting.threadSwipes.Length; i++)
				{
					Lighting.threadSwipes[i] = new Lighting.LightingSwipeData();
				}
			}
			int num = Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10;
			int num2 = Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10;
			if (Lighting.states == null || Lighting.states.Length < num || Lighting.states[0].Length < num2)
			{
				Lighting.states = new Lighting.LightingState[num][];
				Lighting.axisFlipStates = new Lighting.LightingState[num2][];
				for (int j = 0; j < num2; j++)
				{
					Lighting.axisFlipStates[j] = new Lighting.LightingState[num];
				}
				for (int k = 0; k < num; k++)
				{
					Lighting.LightingState[] array = new Lighting.LightingState[num2];
					for (int l = 0; l < num2; l++)
					{
						Lighting.LightingState lightingState = new Lighting.LightingState();
						array[l] = lightingState;
						Lighting.axisFlipStates[l][k] = lightingState;
					}
					Lighting.states[k] = array;
				}
			}
		}
		public static void LightTiles(int firstX, int lastX, int firstY, int lastY)
		{
			Main.render = true;
			Lighting.oldSkyColor = Lighting.skyColor;
			float num = (float)Main.tileColor.R / 255f;
			float num2 = (float)Main.tileColor.G / 255f;
			float num3 = (float)Main.tileColor.B / 255f;
			Lighting.skyColor = (num + num2 + num3) / 3f;
			if (Lighting.lightMode < 2)
			{
				Lighting.brightness = 1.2f;
				Lighting.offScreenTiles2 = 34;
				Lighting.offScreenTiles = 40;
			}
			else
			{
				Lighting.brightness = 1f;
				Lighting.offScreenTiles2 = 18;
				Lighting.offScreenTiles = 23;
			}
			if (Main.player[Main.myPlayer].blind)
			{
				Lighting.brightness = 1f;
			}
			Lighting.defBrightness = Lighting.brightness;
			Lighting.firstTileX = firstX;
			Lighting.lastTileX = lastX;
			Lighting.firstTileY = firstY;
			Lighting.lastTileY = lastY;
			Lighting.firstToLightX = Lighting.firstTileX - Lighting.offScreenTiles;
			Lighting.firstToLightY = Lighting.firstTileY - Lighting.offScreenTiles;
			Lighting.lastToLightX = Lighting.lastTileX + Lighting.offScreenTiles;
			Lighting.lastToLightY = Lighting.lastTileY + Lighting.offScreenTiles;
			if (Lighting.firstToLightX < 0)
			{
				Lighting.firstToLightX = 0;
			}
			if (Lighting.lastToLightX >= Main.maxTilesX)
			{
				Lighting.lastToLightX = Main.maxTilesX - 1;
			}
			if (Lighting.firstToLightY < 0)
			{
				Lighting.firstToLightY = 0;
			}
			if (Lighting.lastToLightY >= Main.maxTilesY)
			{
				Lighting.lastToLightY = Main.maxTilesY - 1;
			}
			Lighting.lightCounter++;
			Main.renderCount++;
			int num4 = Main.screenWidth / 16 + Lighting.offScreenTiles * 2;
			int num5 = Main.screenHeight / 16 + Lighting.offScreenTiles * 2;
			Vector2 vector = Main.screenLastPosition;
			if (Main.renderCount < 3)
			{
				Lighting.doColors();
			}
			if (Main.renderCount == 2)
			{
				vector = Main.screenPosition;
				int num6 = (int)(Main.screenPosition.X / 16f) - Lighting.scrX;
				int num7 = (int)(Main.screenPosition.Y / 16f) - Lighting.scrY;
				if (num6 > 16)
				{
					num6 = 0;
				}
				if (num7 > 16)
				{
					num7 = 0;
				}
				int num8 = 0;
				int num9 = num4;
				int num10 = 0;
				int num11 = num5;
				if (num6 < 0)
				{
					num8 -= num6;
				}
				else
				{
					num9 -= num6;
				}
				if (num7 < 0)
				{
					num10 -= num7;
				}
				else
				{
					num11 -= num7;
				}
				if (Lighting.RGB)
				{
					for (int i = num8; i < num4; i++)
					{
						Lighting.LightingState[] array = Lighting.states[i];
						Lighting.LightingState[] array2 = Lighting.states[i + num6];
						for (int j = num10; j < num11; j++)
						{
							Lighting.LightingState lightingState = array[j];
							Lighting.LightingState lightingState2 = array2[j + num7];
							lightingState.r = lightingState2.r2;
							lightingState.g = lightingState2.g2;
							lightingState.b = lightingState2.b2;
						}
					}
				}
				else
				{
					for (int k = num8; k < num9; k++)
					{
						Lighting.LightingState[] array3 = Lighting.states[k];
						Lighting.LightingState[] array4 = Lighting.states[k + num6];
						for (int l = num10; l < num11; l++)
						{
							Lighting.LightingState lightingState3 = array3[l];
							Lighting.LightingState lightingState4 = array4[l + num7];
							lightingState3.r = lightingState4.r2;
							lightingState3.g = lightingState4.r2;
							lightingState3.b = lightingState4.r2;
						}
					}
				}
			}
			else
			{
				if (!Main.renderNow)
				{
					int num12 = (int)(Main.screenPosition.X / 16f) - (int)(vector.X / 16f);
					if (num12 > 5 || num12 < -5)
					{
						num12 = 0;
					}
					int num13;
					int num14;
					int num15;
					if (num12 < 0)
					{
						num13 = -1;
						num12 *= -1;
						num14 = num4;
						num15 = num12;
					}
					else
					{
						num13 = 1;
						num14 = 0;
						num15 = num4 - num12;
					}
					int num16 = (int)(Main.screenPosition.Y / 16f) - (int)(vector.Y / 16f);
					if (num16 > 5 || num16 < -5)
					{
						num16 = 0;
					}
					int num17;
					int num18;
					int num19;
					if (num16 < 0)
					{
						num17 = -1;
						num16 *= -1;
						num18 = num5;
						num19 = num16;
					}
					else
					{
						num17 = 1;
						num18 = 0;
						num19 = num5 - num16;
					}
					if (num12 != 0 || num16 != 0)
					{
						for (int num20 = num14; num20 != num15; num20 += num13)
						{
							Lighting.LightingState[] array5 = Lighting.states[num20];
							Lighting.LightingState[] array6 = Lighting.states[num20 + num12 * num13];
							for (int num21 = num18; num21 != num19; num21 += num17)
							{
								Lighting.LightingState lightingState5 = array5[num21];
								Lighting.LightingState lightingState6 = array6[num21 + num16 * num17];
								lightingState5.r = lightingState6.r;
								lightingState5.g = lightingState6.g;
								lightingState5.b = lightingState6.b;
							}
						}
					}
					if (Netplay.clientSock.statusMax > 0)
					{
						Main.mapTime = 1;
					}
					if (Main.mapTime == 0 && Main.mapEnabled && Main.renderCount == 3)
					{
						try
						{
							Main.mapTime = Main.mapTimeMax;
							Main.updateMap = true;
							Main.mapMinX = Lighting.firstToLightX + Lighting.offScreenTiles;
							Main.mapMaxX = Lighting.lastToLightX - Lighting.offScreenTiles;
							Main.mapMinY = Lighting.firstToLightY + Lighting.offScreenTiles;
							Main.mapMaxY = Lighting.lastToLightY - Lighting.offScreenTiles;
							for (int m = Main.mapMinX; m < Main.mapMaxX; m++)
							{
								Lighting.LightingState[] array7 = Lighting.states[m - Lighting.firstTileX + Lighting.offScreenTiles];
								for (int n = Main.mapMinY; n < Main.mapMaxY; n++)
								{
									Lighting.LightingState lightingState7 = array7[n - Lighting.firstTileY + Lighting.offScreenTiles];
									Tile tile = Main.tile[m, n];
									Map map = Main.map[m, n];
									if (map == null)
									{
										map = new Map();
										Main.map[m, n] = map;
									}
									float num22 = 0f;
									if (lightingState7.r > num22)
									{
										num22 = lightingState7.r;
									}
									if (lightingState7.g > num22)
									{
										num22 = lightingState7.g;
									}
									if (lightingState7.b > num22)
									{
										num22 = lightingState7.b;
									}
									if (Lighting.lightMode < 2)
									{
										num22 *= 1.5f;
									}
									num22 *= 255f;
									if (num22 > 255f)
									{
										num22 = 255f;
									}
									if ((double)n < Main.worldSurface && !tile.active() && tile.wall == 0 && tile.liquid == 0)
									{
										num22 = 22f;
									}
									if (num22 > 18f || map.light > 0)
									{
										if (num22 < 22f)
										{
											num22 = 22f;
										}
										map.setTile(m, n, (byte)num22);
									}
								}
							}
						}
						catch
						{
						}
					}
					if (Lighting.oldSkyColor != Lighting.skyColor)
					{
						int num23 = Lighting.firstToLightX;
						int num24 = Lighting.firstToLightY;
						int num25 = Lighting.lastToLightX;
						int num26;
						if ((double)Lighting.lastToLightY >= Main.worldSurface)
						{
							num26 = (int)Main.worldSurface - 1;
						}
						else
						{
							num26 = Lighting.lastToLightY;
						}
						if ((double)num24 < Main.worldSurface)
						{
							for (int num27 = num23; num27 < num25; num27++)
							{
								Lighting.LightingState[] array8 = Lighting.states[num27 - Lighting.firstToLightX];
								for (int num28 = num24; num28 < num26; num28++)
								{
									Lighting.LightingState lightingState8 = array8[num28 - Lighting.firstToLightY];
									Tile tile2 = Main.tile[num27, num28];
									if (tile2 == null)
									{
										tile2 = new Tile();
										Main.tile[num27, num28] = tile2;
									}
									if ((!tile2.active() || !Main.tileNoSunLight[(int)tile2.type]) && lightingState8.r < Lighting.skyColor && tile2.liquid < 200 && (Main.wallLight[(int)tile2.wall] || tile2.wall == 73))
									{
										lightingState8.r = num;
										if (lightingState8.g < Lighting.skyColor)
										{
											lightingState8.g = num2;
										}
										if (lightingState8.b < Lighting.skyColor)
										{
											lightingState8.b = num3;
										}
									}
								}
							}
						}
					}
				}
				else
				{
					Lighting.lightCounter = 0;
				}
			}
			if (Main.renderCount > Lighting.maxRenderCount)
			{
				Lighting.PreRenderPhase();
			}
		}
		public static void PreRenderPhase()
		{
			float num = (float)Main.tileColor.R / 255f;
			float num2 = (float)Main.tileColor.G / 255f;
			float num3 = (float)Main.tileColor.B / 255f;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num4 = 0;
			int num5 = Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10;
			int num6 = 0;
			int num7 = Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10;
			Lighting.minX = num5;
			Lighting.maxX = num4;
			Lighting.minY = num7;
			Lighting.maxY = num6;
			if (Lighting.lightMode == 0 || Lighting.lightMode == 3)
			{
				Lighting.RGB = true;
			}
			else
			{
				Lighting.RGB = false;
			}
			for (int i = num4; i < num5; i++)
			{
				Lighting.LightingState[] array = Lighting.states[i];
				for (int j = num6; j < num7; j++)
				{
					Lighting.LightingState lightingState = array[j];
					lightingState.r2 = 0f;
					lightingState.g2 = 0f;
					lightingState.b2 = 0f;
					lightingState.stopLight = false;
					lightingState.wetLight = false;
					lightingState.honeyLight = false;
				}
			}
			if (Main.wof >= 0 && Main.player[Main.myPlayer].gross)
			{
				try
				{
					int num8 = (int)Main.screenPosition.Y / 16 - 10;
					int num9 = (int)(Main.screenPosition.Y + (float)Main.screenHeight) / 16 + 10;
					int num10 = (int)Main.npc[Main.wof].position.X / 16;
					if (Main.npc[Main.wof].direction > 0)
					{
						num10 -= 3;
					}
					else
					{
						num10 += 2;
					}
					int num11 = num10 + 8;
					float num12 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
					float num13 = 0.3f;
					float num14 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
					num12 *= 0.2f;
					num13 *= 0.1f;
					num14 *= 0.3f;
					for (int k = num10; k <= num11; k++)
					{
						Lighting.LightingState[] array2 = Lighting.states[k - num10];
						for (int l = num8; l <= num9; l++)
						{
							Lighting.LightingState lightingState2 = array2[l - Lighting.firstToLightY];
							if (lightingState2.r2 < num12)
							{
								lightingState2.r2 = num12;
							}
							if (lightingState2.g2 < num13)
							{
								lightingState2.g2 = num13;
							}
							if (lightingState2.b2 < num14)
							{
								lightingState2.b2 = num14;
							}
						}
					}
				}
				catch
				{
				}
			}
			Main.sandTiles = 0;
			Main.evilTiles = 0;
			Main.bloodTiles = 0;
			Main.shroomTiles = 0;
			Main.snowTiles = 0;
			Main.holyTiles = 0;
			Main.meteorTiles = 0;
			Main.jungleTiles = 0;
			Main.dungeonTiles = 0;
			Main.campfire = false;
			Main.heartLantern = false;
			Main.musicBox = -1;
			Main.waterCandles = 0;
			int[] tileCounts = WorldGen.tileCounts;
			num4 = Lighting.firstToLightX;
			num5 = Lighting.lastToLightX;
			num6 = Lighting.firstToLightY;
			num7 = Lighting.lastToLightY;
			int num15 = (num5 - num4 - Main.zoneX) / 2;
			int num16 = (num7 - num6 - Main.zoneY) / 2;
			Main.fountainColor = -1;
			for (int m = num4; m < num5; m++)
			{
				Lighting.LightingState[] array3 = Lighting.states[m - Lighting.firstToLightX];
				for (int n = num6; n < num7; n++)
				{
					Lighting.LightingState lightingState3 = array3[n - Lighting.firstToLightY];
					Tile tile = Main.tile[m, n];
					if (tile == null)
					{
						tile = new Tile();
						Main.tile[m, n] = tile;
					}
					float num17 = 0f;
					float num18 = 0f;
					float num19 = 0f;
					if ((double)n < Main.worldSurface)
					{
						if ((!tile.active() || !Main.tileNoSunLight[(int)tile.type]) && lightingState3.r2 < Lighting.skyColor && (Main.wallLight[(int)tile.wall] || tile.wall == 73) && tile.liquid < 200 && (!tile.halfBrick() || Main.tile[m, n - 1].liquid < 200))
						{
							num17 = num;
							num18 = num2;
							num19 = num3;
						}
						if ((!tile.active() || tile.halfBrick() || !Main.tileNoSunLight[(int)tile.type]) && tile.wall >= 88 && tile.wall <= 93 && tile.liquid < 255)
						{
							num17 = num;
							num18 = num2;
							num19 = num3;
							switch (tile.wall)
							{
							case 88:
								num17 *= 0.9f;
								num18 *= 0.15f;
								num19 *= 0.9f;
								break;
							case 89:
								num17 *= 0.9f;
								num18 *= 0.9f;
								num19 *= 0.15f;
								break;
							case 90:
								num17 *= 0.15f;
								num18 *= 0.15f;
								num19 *= 0.9f;
								break;
							case 91:
								num17 *= 0.15f;
								num18 *= 0.9f;
								num19 *= 0.15f;
								break;
							case 92:
								num17 *= 0.9f;
								num18 *= 0.15f;
								num19 *= 0.15f;
								break;
							case 93:
							{
								float num20 = 0.2f;
								float num21 = 0.7f - num20;
								num17 *= num21 + (float)Main.DiscoR / 255f * num20;
								num18 *= num21 + (float)Main.DiscoG / 255f * num20;
								num19 *= num21 + (float)Main.DiscoB / 255f * num20;
								break;
							}
							}
						}
						if (!Lighting.RGB)
						{
							float num22 = (num17 + num18 + num19) / 3f;
							num18 = (num17 = (num19 = num22));
						}
						if (lightingState3.r2 < num17)
						{
							lightingState3.r2 = num17;
						}
						if (lightingState3.g2 < num18)
						{
							lightingState3.g2 = num18;
						}
						if (lightingState3.b2 < num19)
						{
							lightingState3.b2 = num19;
						}
					}
					byte wall = tile.wall;
					if (wall <= 44)
					{
						if (wall != 33)
						{
							if (wall == 44)
							{
								if (!tile.active() || !Main.tileBlockLight[(int)tile.type])
								{
									num17 = (float)Main.DiscoR / 255f * 0.15f;
									num18 = (float)Main.DiscoG / 255f * 0.15f;
									num19 = (float)Main.DiscoB / 255f * 0.15f;
								}
							}
						}
						else
						{
							if (!tile.active() || !Main.tileBlockLight[(int)tile.type])
							{
								num17 = 0.0899999961f;
								num18 = 0.0525000021f;
								num19 = 0.24f;
							}
						}
					}
					else
					{
						if (wall != 137)
						{
							switch (wall)
							{
							case 153:
								num17 = 0.6f;
								num18 = 0.3f;
								break;
							case 154:
								num17 = 0.6f;
								num19 = 0.6f;
								break;
							case 155:
								num17 = 0.6f;
								num18 = 0.6f;
								num19 = 0.6f;
								break;
							case 156:
								num18 = 0.6f;
								break;
							default:
								switch (wall)
								{
								case 164:
									num17 = 0.6f;
									break;
								case 165:
									num19 = 0.6f;
									break;
								case 166:
									num17 = 0.6f;
									num18 = 0.6f;
									break;
								}
								break;
							}
						}
						else
						{
							if (!tile.active() || !Main.tileBlockLight[(int)tile.type])
							{
								float num23 = 0.4f;
								num23 += (float)(270 - (int)Main.mouseTextColor) / 1500f;
								num23 += (float)Main.rand.Next(0, 50) * 0.0005f;
								num17 = 1f * num23;
								num18 = 0.5f * num23;
								num19 = 0.1f * num23;
							}
						}
					}
					if (tile.active())
					{
						if (m > num4 + num15 && m < num5 - num15 && n > num6 + num16 && n < num7 - num16)
						{
							tileCounts[(int)tile.type]++;
							if (tile.type == 42 && tile.frameY >= 324 && tile.frameY <= 358)
							{
								Main.heartLantern = true;
							}
						}
						ushort type = tile.type;
						if (type != 139)
						{
							if (type == 207)
							{
								if (tile.frameY >= 72)
								{
									switch (tile.frameX / 36)
									{
									case 0:
										Main.fountainColor = 0;
										break;
									case 1:
										Main.fountainColor = 6;
										break;
									case 2:
										Main.fountainColor = 3;
										break;
									case 3:
										Main.fountainColor = 5;
										break;
									case 4:
										Main.fountainColor = 2;
										break;
									case 5:
										Main.fountainColor = 10;
										break;
									case 6:
										Main.fountainColor = 4;
										break;
									case 7:
										Main.fountainColor = 9;
										break;
									default:
										Main.fountainColor = -1;
										break;
									}
								}
							}
						}
						else
						{
							if (tile.frameX >= 36)
							{
								Main.musicBox = (int)(tile.frameY / 36);
							}
						}
						if (Main.tileBlockLight[(int)tile.type] && (Lighting.lightMode >= 2 || (tile.type != 131 && !tile.inActive() && tile.slope() == 0)))
						{
							lightingState3.stopLight = true;
						}
						if (Main.tileLighted[(int)tile.type])
						{
							type = tile.type;
							if (type <= 129)
							{
								if (type <= 42)
								{
									if (type <= 22)
									{
										if (type != 4)
										{
											if (type == 17)
											{
												goto IL_1A07;
											}
											if (type != 22)
											{
												goto IL_22D7;
											}
											goto IL_1A55;
										}
										else
										{
											if (tile.frameX >= 66)
											{
												goto IL_22D7;
											}
											switch (tile.frameY / 22)
											{
											case 0:
												num17 = 1f;
												num18 = 0.95f;
												num19 = 0.8f;
												goto IL_22D7;
											case 1:
												num17 = 0f;
												num18 = 0.1f;
												num19 = 1.3f;
												goto IL_22D7;
											case 2:
												num17 = 1f;
												num18 = 0.1f;
												num19 = 0.1f;
												goto IL_22D7;
											case 3:
												num17 = 0f;
												num18 = 1f;
												num19 = 0.1f;
												goto IL_22D7;
											case 4:
												num17 = 0.9f;
												num18 = 0f;
												num19 = 0.9f;
												goto IL_22D7;
											case 5:
												num17 = 1.3f;
												num18 = 1.3f;
												num19 = 1.3f;
												goto IL_22D7;
											case 6:
												num17 = 0.9f;
												num18 = 0.9f;
												num19 = 0f;
												goto IL_22D7;
											case 7:
												num17 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
												num18 = 0.3f;
												num19 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
												goto IL_22D7;
											case 8:
												num17 = 0.85f;
												num18 = 1f;
												num19 = 0.7f;
												goto IL_22D7;
											case 9:
												num17 = 0.7f;
												num18 = 0.85f;
												num19 = 1f;
												goto IL_22D7;
											case 10:
												num17 = 1f;
												num18 = 0.5f;
												num19 = 0f;
												goto IL_22D7;
											case 11:
												num17 = 1.25f;
												num18 = 1.25f;
												num19 = 0.8f;
												goto IL_22D7;
											case 12:
												num17 = 0.75f;
												num18 = 1.28249991f;
												num19 = 1.2f;
												goto IL_22D7;
											default:
												num17 = 1f;
												num18 = 0.95f;
												num19 = 0.8f;
												goto IL_22D7;
											}
										}
									}
									else
									{
										if (type != 26)
										{
											switch (type)
											{
											case 31:
												break;
											case 32:
											case 36:
												goto IL_22D7;
											case 33:
											{
												if (tile.frameX != 0)
												{
													goto IL_22D7;
												}
												int num24 = (int)(tile.frameY / 22);
												switch (num24)
												{
												case 0:
													num17 = 1f;
													num18 = 0.95f;
													num19 = 0.65f;
													goto IL_22D7;
												case 1:
													num17 = 0.55f;
													num18 = 0.85f;
													num19 = 0.35f;
													goto IL_22D7;
												case 2:
													num17 = 0.65f;
													num18 = 0.95f;
													num19 = 0.5f;
													goto IL_22D7;
												case 3:
													num17 = 0.2f;
													num18 = 0.75f;
													num19 = 1f;
													goto IL_22D7;
												default:
													if (num24 != 14)
													{
														switch (num24)
														{
														case 19:
															num17 = 0.37f;
															num18 = 0.8f;
															num19 = 1f;
															goto IL_22D7;
														case 20:
															num17 = 0f;
															num18 = 0.9f;
															num19 = 1f;
															goto IL_22D7;
														case 21:
															num17 = 0.25f;
															num18 = 0.7f;
															num19 = 1f;
															goto IL_22D7;
														case 25:
															num17 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
															num18 = 0.3f;
															num19 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
															goto IL_22D7;
														}
														num17 = 1f;
														num18 = 0.95f;
														num19 = 0.65f;
														goto IL_22D7;
													}
													num17 = 1f;
													num18 = 1f;
													num19 = 0.6f;
													goto IL_22D7;
												}
												break;
											}
											case 34:
												if (tile.frameX < 54)
												{
													switch (tile.frameY / 54)
													{
													case 7:
														num17 = 0.95f;
														num18 = 0.95f;
														num19 = 0.5f;
														goto IL_22D7;
													case 8:
														num17 = 0.85f;
														num18 = 0.6f;
														num19 = 1f;
														goto IL_22D7;
													case 9:
														num17 = 1f;
														num18 = 0.6f;
														num19 = 0.6f;
														goto IL_22D7;
													case 11:
													case 17:
														num17 = 0.75f;
														num18 = 0.9f;
														num19 = 1f;
														goto IL_22D7;
													case 15:
														num17 = 1f;
														num18 = 1f;
														num19 = 0.7f;
														goto IL_22D7;
													case 18:
														num17 = 1f;
														num18 = 1f;
														num19 = 0.6f;
														goto IL_22D7;
													case 24:
														num17 = 0.37f;
														num18 = 0.8f;
														num19 = 1f;
														goto IL_22D7;
													case 25:
														num17 = 0f;
														num18 = 0.9f;
														num19 = 1f;
														goto IL_22D7;
													case 26:
														num17 = 0.25f;
														num18 = 0.7f;
														num19 = 1f;
														goto IL_22D7;
													case 27:
														num17 = 0.55f;
														num18 = 0.85f;
														num19 = 0.35f;
														goto IL_22D7;
													case 28:
														num17 = 0.65f;
														num18 = 0.95f;
														num19 = 0.5f;
														goto IL_22D7;
													case 29:
														num17 = 0.2f;
														num18 = 0.75f;
														num19 = 1f;
														goto IL_22D7;
													case 32:
														num17 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
														num18 = 0.3f;
														num19 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
														goto IL_22D7;
													}
													num17 = 1f;
													num18 = 0.95f;
													num19 = 0.8f;
													goto IL_22D7;
												}
												goto IL_22D7;
											case 35:
												if (tile.frameX < 36)
												{
													num17 = 0.75f;
													num18 = 0.6f;
													num19 = 0.3f;
													goto IL_22D7;
												}
												goto IL_22D7;
											case 37:
												num17 = 0.56f;
												num18 = 0.43f;
												num19 = 0.15f;
												goto IL_22D7;
											default:
											{
												if (type != 42)
												{
													goto IL_22D7;
												}
												if (tile.frameX != 0)
												{
													goto IL_22D7;
												}
												int num25 = (int)(tile.frameY / 36);
												int num24 = num25;
												switch (num24)
												{
												case 0:
													num17 = 0.7f;
													num18 = 0.65f;
													num19 = 0.55f;
													goto IL_22D7;
												case 1:
													num17 = 0.9f;
													num18 = 0.75f;
													num19 = 0.6f;
													goto IL_22D7;
												case 2:
													num17 = 0.8f;
													num18 = 0.6f;
													num19 = 0.6f;
													goto IL_22D7;
												case 3:
													num17 = 0.65f;
													num18 = 0.5f;
													num19 = 0.2f;
													goto IL_22D7;
												case 4:
													num17 = 0.5f;
													num18 = 0.7f;
													num19 = 0.4f;
													goto IL_22D7;
												case 5:
													num17 = 0.9f;
													num18 = 0.4f;
													num19 = 0.2f;
													goto IL_22D7;
												case 6:
													num17 = 0.7f;
													num18 = 0.75f;
													num19 = 0.3f;
													goto IL_22D7;
												case 7:
												{
													float num26 = Main.demonTorch * 0.2f;
													num17 = 0.9f - num26;
													num18 = 0.9f - num26;
													num19 = 0.7f + num26;
													goto IL_22D7;
												}
												case 8:
													num17 = 0.75f;
													num18 = 0.6f;
													num19 = 0.3f;
													goto IL_22D7;
												case 9:
													num17 = 1f;
													num18 = 0.3f;
													num19 = 0.5f;
													num19 += Main.demonTorch * 0.2f;
													num17 -= Main.demonTorch * 0.1f;
													num18 -= Main.demonTorch * 0.2f;
													goto IL_22D7;
												default:
													switch (num24)
													{
													case 28:
														num17 = 0.37f;
														num18 = 0.8f;
														num19 = 1f;
														goto IL_22D7;
													case 29:
														num17 = 0f;
														num18 = 0.9f;
														num19 = 1f;
														goto IL_22D7;
													case 30:
														num17 = 0.25f;
														num18 = 0.7f;
														num19 = 1f;
														goto IL_22D7;
													case 32:
														num17 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
														num18 = 0.3f;
														num19 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
														goto IL_22D7;
													}
													num17 = 1f;
													num18 = 1f;
													num19 = 1f;
													goto IL_22D7;
												}
												break;
											}
											}
										}
										if ((tile.type == 31 && tile.frameX >= 36) || (tile.type == 26 && tile.frameX >= 54))
										{
											float num27 = (float)Main.rand.Next(-5, 6) * 0.0025f;
											num17 = 0.5f + num27 * 2f;
											num18 = 0.2f + num27;
											num19 = 0.1f;
											goto IL_22D7;
										}
										float num28 = (float)Main.rand.Next(-5, 6) * 0.0025f;
										num17 = 0.31f + num28;
										num18 = 0.1f;
										num19 = 0.44f + num28 * 2f;
										goto IL_22D7;
									}
								}
								else
								{
									if (type <= 72)
									{
										if (type == 49)
										{
											num17 = 0f;
											num18 = 0.35f;
											num19 = 0.8f;
											goto IL_22D7;
										}
										if (type != 61)
										{
											switch (type)
											{
											case 70:
											case 71:
											case 72:
												goto IL_1E6B;
											default:
												goto IL_22D7;
											}
										}
										else
										{
											if (tile.frameX == 144)
											{
												float num29 = 1f + (float)(270 - (int)Main.mouseTextColor) / 400f;
												float num30 = 0.8f - (float)(270 - (int)Main.mouseTextColor) / 400f;
												num17 = 0.42f * num30;
												num18 = 0.81f * num29;
												num19 = 0.52f * num30;
												goto IL_22D7;
											}
											goto IL_22D7;
										}
									}
									else
									{
										if (type <= 84)
										{
											if (type == 77)
											{
												num17 = 0.75f;
												num18 = 0.45f;
												num19 = 0.25f;
												goto IL_22D7;
											}
											switch (type)
											{
											case 83:
												if (tile.frameX == 18 && !Main.dayTime)
												{
													num17 = 0.1f;
													num18 = 0.4f;
													num19 = 0.6f;
													goto IL_22D7;
												}
												goto IL_22D7;
											case 84:
											{
												int num31 = (int)(tile.frameX / 18);
												if (num31 == 2)
												{
													float num32 = (float)(270 - (int)Main.mouseTextColor) / 800f;
													if (num32 > 1f)
													{
														num32 = 1f;
													}
													else
													{
														if (num32 < 0f)
														{
															num32 = 0f;
														}
													}
													num17 = num32 * 0.7f;
													num18 = num32;
													num19 = num32 * 0.1f;
													goto IL_22D7;
												}
												if (num31 == 5)
												{
													float num32 = 0.9f;
													num17 = num32;
													num18 = num32 * 0.8f;
													num19 = num32 * 0.2f;
													goto IL_22D7;
												}
												if (num31 == 6)
												{
													float num32 = 0.08f;
													num18 = num32 * 0.8f;
													num19 = num32;
													goto IL_22D7;
												}
												goto IL_22D7;
											}
											default:
												goto IL_22D7;
											}
										}
										else
										{
											switch (type)
											{
											case 92:
												if (tile.frameY <= 18 && tile.frameX == 0)
												{
													num17 = 1f;
													num18 = 1f;
													num19 = 1f;
													goto IL_22D7;
												}
												goto IL_22D7;
											case 93:
												if (tile.frameX == 0)
												{
													switch (tile.frameY / 54)
													{
													case 1:
														num17 = 0.95f;
														num18 = 0.95f;
														num19 = 0.5f;
														goto IL_22D7;
													case 2:
														num17 = 0.85f;
														num18 = 0.6f;
														num19 = 1f;
														goto IL_22D7;
													case 3:
														num17 = 0.75f;
														num18 = 1f;
														num19 = 0.6f;
														goto IL_22D7;
													case 4:
													case 5:
														num17 = 0.75f;
														num18 = 0.9f;
														num19 = 1f;
														goto IL_22D7;
													case 9:
														num17 = 1f;
														num18 = 1f;
														num19 = 0.7f;
														goto IL_22D7;
													case 13:
														num17 = 1f;
														num18 = 1f;
														num19 = 0.6f;
														goto IL_22D7;
													case 19:
														num17 = 0.37f;
														num18 = 0.8f;
														num19 = 1f;
														goto IL_22D7;
													case 20:
														num17 = 0f;
														num18 = 0.9f;
														num19 = 1f;
														goto IL_22D7;
													case 21:
														num17 = 0.25f;
														num18 = 0.7f;
														num19 = 1f;
														goto IL_22D7;
													case 23:
														num17 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
														num18 = 0.3f;
														num19 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
														goto IL_22D7;
													case 24:
														num17 = 0.35f;
														num18 = 0.5f;
														num19 = 0.3f;
														goto IL_22D7;
													case 25:
														num17 = 0.34f;
														num18 = 0.4f;
														num19 = 0.31f;
														goto IL_22D7;
													case 26:
														num17 = 0.25f;
														num18 = 0.32f;
														num19 = 0.5f;
														goto IL_22D7;
													}
													num17 = 1f;
													num18 = 0.97f;
													num19 = 0.85f;
													goto IL_22D7;
												}
												goto IL_22D7;
											case 94:
											case 97:
											case 99:
												goto IL_22D7;
											case 95:
												if (tile.frameX < 36)
												{
													num17 = 1f;
													num18 = 0.95f;
													num19 = 0.8f;
													goto IL_22D7;
												}
												goto IL_22D7;
											case 96:
												if (tile.frameX >= 36)
												{
													num17 = 0.5f;
												}
												num18 = 0.35f;
												num19 = 0.1f;
												goto IL_22D7;
											case 98:
												if (tile.frameY == 0)
												{
													num17 = 1f;
												}
												num18 = 0.97f;
												num19 = 0.85f;
												goto IL_22D7;
											case 100:
												break;
											default:
												switch (type)
												{
												case 125:
												{
													float num33 = (float)Main.rand.Next(28, 42) * 0.01f;
													num33 += (float)(270 - (int)Main.mouseTextColor) / 800f;
													num18 = (lightingState3.g2 = 0.3f * num33);
													num19 = (lightingState3.b2 = 0.6f * num33);
													goto IL_22D7;
												}
												case 126:
													if (tile.frameX < 36)
													{
														num17 = (float)Main.DiscoR / 255f;
														num18 = (float)Main.DiscoG / 255f;
														num19 = (float)Main.DiscoB / 255f;
														goto IL_22D7;
													}
													goto IL_22D7;
												case 127:
												case 128:
													goto IL_22D7;
												case 129:
													switch (tile.frameX / 18 % 3)
													{
													case 0:
														num17 = 0f;
														num18 = 0.05f;
														num19 = 0.25f;
														goto IL_22D7;
													case 1:
														num17 = 0.2f;
														num18 = 0f;
														num19 = 0.15f;
														goto IL_22D7;
													case 2:
														num17 = 0.1f;
														num18 = 0f;
														num19 = 0.2f;
														goto IL_22D7;
													default:
														goto IL_22D7;
													}
													break;
												default:
													goto IL_22D7;
												}
												break;
											}
										}
									}
								}
							}
							else
							{
								if (type <= 204)
								{
									if (type <= 149)
									{
										if (type == 133)
										{
											goto IL_1A07;
										}
										if (type == 140)
										{
											goto IL_1A55;
										}
										if (type != 149)
										{
											goto IL_22D7;
										}
										if (tile.frameX <= 36)
										{
											switch (tile.frameX / 18)
											{
											case 0:
												num17 = 0.1f;
												num18 = 0.2f;
												num19 = 0.5f;
												break;
											case 1:
												num17 = 0.5f;
												num18 = 0.1f;
												num19 = 0.1f;
												break;
											case 2:
												num17 = 0.2f;
												num18 = 0.5f;
												num19 = 0.1f;
												break;
											}
											num17 *= (float)Main.rand.Next(970, 1031) * 0.001f;
											num18 *= (float)Main.rand.Next(970, 1031) * 0.001f;
											num19 *= (float)Main.rand.Next(970, 1031) * 0.001f;
											goto IL_22D7;
										}
										goto IL_22D7;
									}
									else
									{
										if (type <= 174)
										{
											if (type == 160)
											{
												num17 = (float)Main.DiscoR / 255f * 0.25f;
												num18 = (float)Main.DiscoG / 255f * 0.25f;
												num19 = (float)Main.DiscoB / 255f * 0.25f;
												goto IL_22D7;
											}
											switch (type)
											{
											case 171:
											{
												int num34 = m;
												int num35 = n;
												if (tile.frameX < 10)
												{
													num34 -= (int)tile.frameX;
													num35 -= (int)tile.frameY;
												}
												switch ((Main.tile[num34, num35].frameY & 15360) >> 10)
												{
												case 1:
													num17 = 0.1f;
													num18 = 0.1f;
													num19 = 0.1f;
													break;
												case 2:
													num17 = 0.2f;
													break;
												case 3:
													num18 = 0.2f;
													break;
												case 4:
													num19 = 0.2f;
													break;
												case 5:
													num17 = 0.125f;
													num18 = 0.125f;
													break;
												case 6:
													num17 = 0.2f;
													num18 = 0.1f;
													break;
												case 7:
													num17 = 0.125f;
													num18 = 0.125f;
													break;
												case 8:
													num17 = 0.08f;
													num18 = 0.175f;
													break;
												case 9:
													num18 = 0.125f;
													num19 = 0.125f;
													break;
												case 10:
													num17 = 0.125f;
													num19 = 0.125f;
													break;
												case 11:
													num17 = 0.1f;
													num18 = 0.1f;
													num19 = 0.2f;
													break;
												default:
													num18 = (num17 = (num19 = 0f));
													break;
												}
												num17 *= 0.5f;
												num18 *= 0.5f;
												num19 *= 0.5f;
												goto IL_22D7;
											}
											case 172:
												goto IL_22D7;
											case 173:
												break;
											case 174:
												if (tile.frameX == 0)
												{
													num17 = 1f;
													num18 = 0.95f;
													num19 = 0.65f;
													goto IL_22D7;
												}
												goto IL_22D7;
											default:
												goto IL_22D7;
											}
										}
										else
										{
											if (type == 190)
											{
												goto IL_1E6B;
											}
											if (type != 204)
											{
												goto IL_22D7;
											}
											num17 = 0.35f;
											goto IL_22D7;
										}
									}
								}
								else
								{
									if (type <= 271)
									{
										if (type == 215)
										{
											float num36 = (float)Main.rand.Next(28, 42) * 0.005f;
											num36 += (float)(270 - (int)Main.mouseTextColor) / 700f;
											num17 = 0.9f + num36;
											num18 = 0.3f + num36;
											num19 = 0.1f + num36;
											goto IL_22D7;
										}
										switch (type)
										{
										case 235:
											if ((double)lightingState3.r2 < 0.6)
											{
												lightingState3.r2 = 0.6f;
											}
											if ((double)lightingState3.g2 < 0.6)
											{
												lightingState3.g2 = 0.6f;
												goto IL_22D7;
											}
											goto IL_22D7;
										case 236:
											goto IL_22D7;
										case 237:
											num17 = 0.1f;
											num18 = 0.1f;
											goto IL_22D7;
										case 238:
											if ((double)lightingState3.r2 < 0.5)
											{
												lightingState3.r2 = 0.5f;
											}
											if ((double)lightingState3.b2 < 0.5)
											{
												lightingState3.b2 = 0.5f;
												goto IL_22D7;
											}
											goto IL_22D7;
										default:
											switch (type)
											{
											case 262:
												num17 = 0.75f;
												num19 = 0.75f;
												goto IL_22D7;
											case 263:
												num17 = 0.75f;
												num18 = 0.75f;
												goto IL_22D7;
											case 264:
												num19 = 0.75f;
												goto IL_22D7;
											case 265:
												num18 = 0.75f;
												goto IL_22D7;
											case 266:
												num17 = 0.75f;
												goto IL_22D7;
											case 267:
												num17 = 0.75f;
												num18 = 0.75f;
												num19 = 0.75f;
												goto IL_22D7;
											case 268:
												num17 = 0.75f;
												num18 = 0.375f;
												goto IL_22D7;
											case 269:
												goto IL_22D7;
											case 270:
												num17 = 0.73f;
												num18 = 1f;
												num19 = 0.41f;
												goto IL_22D7;
											case 271:
												num17 = 0.45f;
												num18 = 0.95f;
												num19 = 1f;
												goto IL_22D7;
											default:
												goto IL_22D7;
											}
											break;
										}
									}
									else
									{
										if (type <= 318)
										{
											if (type == 286)
											{
												num17 = 1f;
												num18 = 0.2f;
												num19 = 0.7f;
												goto IL_22D7;
											}
											switch (type)
											{
											case 316:
											case 317:
											case 318:
											{
												int num37 = m - (int)(tile.frameX / 18);
												int num38 = n - (int)(tile.frameY / 18);
												int num39 = num37 / 2 * (num38 / 3);
												num39 %= Main.cageFrames;
												bool flag = Main.jellyfishCageMode[(int)(tile.type - 316), num39] == 2;
												if (tile.type == 316)
												{
													if (flag)
													{
														num17 = 0.2f;
														num18 = 0.3f;
														num19 = 0.8f;
													}
													else
													{
														num17 = 0.1f;
														num18 = 0.2f;
														num19 = 0.5f;
													}
												}
												if (tile.type == 317)
												{
													if (flag)
													{
														num17 = 0.2f;
														num18 = 0.7f;
														num19 = 0.3f;
													}
													else
													{
														num17 = 0.05f;
														num18 = 0.45f;
														num19 = 0.1f;
													}
												}
												if (tile.type != 318)
												{
													goto IL_22D7;
												}
												if (flag)
												{
													num17 = 0.7f;
													num18 = 0.2f;
													num19 = 0.5f;
													goto IL_22D7;
												}
												num17 = 0.4f;
												num18 = 0.1f;
												num19 = 0.25f;
												goto IL_22D7;
											}
											default:
												goto IL_22D7;
											}
										}
										else
										{
											if (type == 327)
											{
												float num40 = 0.5f;
												num40 += (float)(270 - (int)Main.mouseTextColor) / 1500f;
												num40 += (float)Main.rand.Next(0, 50) * 0.0005f;
												num17 = 1f * num40;
												num18 = 0.5f * num40;
												num19 = 0.1f * num40;
												goto IL_22D7;
											}
											if (type == 336)
											{
												num17 = 0.85f;
												num18 = 0.5f;
												num19 = 0.3f;
												goto IL_22D7;
											}
											goto IL_22D7;
										}
									}
								}
							}
							if (tile.frameX < 36)
							{
								switch (tile.frameY / 36)
								{
								case 1:
									num17 = 0.95f;
									num18 = 0.95f;
									num19 = 0.5f;
									goto IL_22D7;
								case 3:
									num17 = 1f;
									num18 = 0.6f;
									num19 = 0.6f;
									goto IL_22D7;
								case 6:
								case 9:
									num17 = 0.75f;
									num18 = 0.9f;
									num19 = 1f;
									goto IL_22D7;
								case 11:
									num17 = 1f;
									num18 = 1f;
									num19 = 0.7f;
									goto IL_22D7;
								case 13:
									num17 = 1f;
									num18 = 1f;
									num19 = 0.6f;
									goto IL_22D7;
								case 19:
									num17 = 0.37f;
									num18 = 0.8f;
									num19 = 1f;
									goto IL_22D7;
								case 20:
									num17 = 0f;
									num18 = 0.9f;
									num19 = 1f;
									goto IL_22D7;
								case 21:
									num17 = 0.25f;
									num18 = 0.7f;
									num19 = 1f;
									goto IL_22D7;
								case 22:
									num17 = 0.35f;
									num18 = 0.5f;
									num19 = 0.3f;
									goto IL_22D7;
								case 23:
									num17 = 0.34f;
									num18 = 0.4f;
									num19 = 0.31f;
									goto IL_22D7;
								case 24:
									num17 = 0.25f;
									num18 = 0.32f;
									num19 = 0.5f;
									goto IL_22D7;
								case 25:
									num17 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
									num18 = 0.3f;
									num19 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
									goto IL_22D7;
								}
								num17 = 1f;
								num18 = 0.95f;
								num19 = 0.65f;
								goto IL_22D7;
							}
							goto IL_22D7;
							IL_1A07:
							num17 = 0.83f;
							num18 = 0.6f;
							num19 = 0.5f;
							goto IL_22D7;
							IL_1A55:
							num17 = 0.12f;
							num18 = 0.07f;
							num19 = 0.32f;
							goto IL_22D7;
							IL_1E6B:
							float num41 = (float)Main.rand.Next(28, 42) * 0.005f;
							num41 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
							num17 = 0.1f;
							num18 = 0.2f + num41 / 2f;
							num19 = 0.7f + num41;
						}
					}
					IL_22D7:
					if (Lighting.RGB)
					{
						if (lightingState3.r2 < num17)
						{
							lightingState3.r2 = num17;
						}
						if (lightingState3.g2 < num18)
						{
							lightingState3.g2 = num18;
						}
						if (lightingState3.b2 < num19)
						{
							lightingState3.b2 = num19;
						}
					}
					else
					{
						float num42 = (num17 + num18 + num19) / 3f;
						if (lightingState3.r2 < num42)
						{
							lightingState3.r2 = num42;
						}
					}
					if (tile.lava() && tile.liquid > 0)
					{
						if (Lighting.RGB)
						{
							float num43 = (float)(tile.liquid / 255) * 0.41f + 0.14f;
							num43 = 0.55f;
							num43 += (float)(270 - (int)Main.mouseTextColor) / 900f;
							if (lightingState3.r2 < num43)
							{
								lightingState3.r2 = num43;
							}
							if (lightingState3.g2 < num43)
							{
								lightingState3.g2 = num43 * 0.6f;
							}
							if (lightingState3.b2 < num43)
							{
								lightingState3.b2 = num43 * 0.2f;
							}
						}
						else
						{
							float num44 = (float)(tile.liquid / 255) * 0.38f + 0.08f;
							num44 += (float)(270 - (int)Main.mouseTextColor) / 2000f;
							if (lightingState3.r2 < num44)
							{
								lightingState3.r2 = num44;
							}
						}
					}
					else
					{
						if (tile.liquid > 128)
						{
							lightingState3.wetLight = true;
							if (tile.honey())
							{
								lightingState3.honeyLight = true;
							}
						}
					}
					if (lightingState3.r2 > 0f || (Lighting.RGB && (lightingState3.g2 > 0f || lightingState3.b2 > 0f)))
					{
						int num45 = m - Lighting.firstToLightX;
						int num46 = n - Lighting.firstToLightY;
						if (Lighting.minX > num45)
						{
							Lighting.minX = num45;
						}
						if (Lighting.maxX < num45 + 1)
						{
							Lighting.maxX = num45 + 1;
						}
						if (Lighting.minY > num46)
						{
							Lighting.minY = num46;
						}
						if (Lighting.maxY < num46 + 1)
						{
							Lighting.maxY = num46 + 1;
						}
					}
				}
			}
			foreach (KeyValuePair<Point16, Lighting.ColorTriplet> current in Lighting.tempLights)
			{
				int num47 = (int)current.Key.x - Lighting.firstTileX + Lighting.offScreenTiles;
				int num48 = (int)current.Key.y - Lighting.firstTileY + Lighting.offScreenTiles;
				if (num47 >= 0 && num47 < Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 && num48 >= 0 && num48 < Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10)
				{
					Lighting.LightingState lightingState4 = Lighting.states[num47][num48];
					if (lightingState4.r2 < current.Value.r)
					{
						lightingState4.r2 = current.Value.r;
					}
					if (lightingState4.g2 < current.Value.g)
					{
						lightingState4.g2 = current.Value.g;
					}
					if (lightingState4.b2 < current.Value.b)
					{
						lightingState4.b2 = current.Value.b;
					}
					if (num47 < Lighting.minX)
					{
						Lighting.minX = num47;
					}
					if (num47 > Lighting.maxX)
					{
						Lighting.maxX = num47;
					}
					if (num48 < Lighting.minY)
					{
						Lighting.minY = num48;
					}
					if (num48 > Lighting.maxY)
					{
						Lighting.maxY = num48;
					}
				}
			}
			Lighting.tempLights.Clear();
			if (tileCounts[215] > 0)
			{
				Main.campfire = true;
			}
			Main.holyTiles = tileCounts[109] + tileCounts[110] + tileCounts[113] + tileCounts[117] + tileCounts[116] + tileCounts[164];
			Main.evilTiles = tileCounts[23] + tileCounts[24] + tileCounts[25] + tileCounts[32] + tileCounts[112] + tileCounts[163] - 5 * tileCounts[27];
			Main.bloodTiles = tileCounts[199] + tileCounts[203] + tileCounts[200] + tileCounts[234] - 5 * tileCounts[27];
			Main.snowTiles = tileCounts[147] + tileCounts[148] + tileCounts[161] + tileCounts[162] + tileCounts[164] + tileCounts[163] + tileCounts[200];
			Main.jungleTiles = tileCounts[60] + tileCounts[61] + tileCounts[62] + tileCounts[74] + tileCounts[226];
			Main.shroomTiles = tileCounts[70] + tileCounts[71] + tileCounts[72];
			Main.meteorTiles = tileCounts[37];
			Main.dungeonTiles = tileCounts[41] + tileCounts[43] + tileCounts[44];
			Main.sandTiles = tileCounts[53] + tileCounts[112] + tileCounts[116] + tileCounts[234];
			Main.waterCandles = tileCounts[49];
			Array.Clear(tileCounts, 0, tileCounts.Length);
			if (Main.holyTiles < 0)
			{
				Main.holyTiles = 0;
			}
			if (Main.evilTiles < 0)
			{
				Main.evilTiles = 0;
			}
			if (Main.bloodTiles < 0)
			{
				Main.bloodTiles = 0;
			}
			int holyTiles = Main.holyTiles;
			Main.holyTiles -= Main.evilTiles;
			Main.holyTiles -= Main.bloodTiles;
			Main.evilTiles -= holyTiles;
			Main.bloodTiles -= holyTiles;
			if (Main.holyTiles < 0)
			{
				Main.holyTiles = 0;
			}
			if (Main.evilTiles < 0)
			{
				Main.evilTiles = 0;
			}
			if (Main.bloodTiles < 0)
			{
				Main.bloodTiles = 0;
			}
			Lighting.minX += Lighting.firstToLightX;
			Lighting.maxX += Lighting.firstToLightX;
			Lighting.minY += Lighting.firstToLightY;
			Lighting.maxY += Lighting.firstToLightY;
			Lighting.minX7 = Lighting.minX;
			Lighting.maxX7 = Lighting.maxX;
			Lighting.minY7 = Lighting.minY;
			Lighting.maxY7 = Lighting.maxY;
			Lighting.firstTileX7 = Lighting.firstTileX;
			Lighting.lastTileX7 = Lighting.lastTileX;
			Lighting.lastTileY7 = Lighting.lastTileY;
			Lighting.firstTileY7 = Lighting.firstTileY;
			Lighting.firstToLightX7 = Lighting.firstToLightX;
			Lighting.lastToLightX7 = Lighting.lastToLightX;
			Lighting.firstToLightY7 = Lighting.firstToLightY;
			Lighting.lastToLightY7 = Lighting.lastToLightY;
			Lighting.firstToLightX27 = Lighting.firstTileX - Lighting.offScreenTiles2;
			Lighting.firstToLightY27 = Lighting.firstTileY - Lighting.offScreenTiles2;
			Lighting.lastToLightX27 = Lighting.lastTileX + Lighting.offScreenTiles2;
			Lighting.lastToLightY27 = Lighting.lastTileY + Lighting.offScreenTiles2;
			if (Lighting.firstToLightX27 < 0)
			{
				Lighting.firstToLightX27 = 0;
			}
			if (Lighting.lastToLightX27 >= Main.maxTilesX)
			{
				Lighting.lastToLightX27 = Main.maxTilesX - 1;
			}
			if (Lighting.firstToLightY27 < 0)
			{
				Lighting.firstToLightY27 = 0;
			}
			if (Lighting.lastToLightY27 >= Main.maxTilesY)
			{
				Lighting.lastToLightY27 = Main.maxTilesY - 1;
			}
			Lighting.scrX = (int)Main.screenPosition.X / 16;
			Lighting.scrY = (int)Main.screenPosition.Y / 16;
			Main.renderCount = 0;
			TimeLogger.LightingTime(0, stopwatch.Elapsed.TotalMilliseconds);
			Lighting.doColors();
		}
		public static void doColors()
		{
			if (Lighting.lightMode < 2)
			{
				Lighting.blueWave += (float)Lighting.blueDir * 0.0001f;
				if (Lighting.blueWave > 1f)
				{
					Lighting.blueWave = 1f;
					Lighting.blueDir = -1;
				}
				else
				{
					if (Lighting.blueWave < 0.97f)
					{
						Lighting.blueWave = 0.97f;
						Lighting.blueDir = 1;
					}
				}
				if (Lighting.RGB)
				{
					Lighting.negLight = 0.91f;
					Lighting.negLight2 = 0.56f;
					Lighting.honeyLightG = 0.7f * Lighting.negLight * Lighting.blueWave;
					Lighting.honeyLightR = 0.75f * Lighting.negLight * Lighting.blueWave;
					Lighting.honeyLightB = 0.6f * Lighting.negLight * Lighting.blueWave;
					switch (Main.waterStyle)
					{
					case 0:
					case 1:
					case 7:
					case 8:
						Lighting.wetLightG = 0.96f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightR = 0.88f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightB = 1.015f * Lighting.negLight * Lighting.blueWave;
						break;
					case 2:
						Lighting.wetLightG = 0.85f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightR = 0.94f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightB = 1.01f * Lighting.negLight * Lighting.blueWave;
						break;
					case 3:
						Lighting.wetLightG = 0.95f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightR = 0.84f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightB = 1.015f * Lighting.negLight * Lighting.blueWave;
						break;
					case 4:
						Lighting.wetLightG = 0.86f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightR = 0.9f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightB = 1.01f * Lighting.negLight * Lighting.blueWave;
						break;
					case 5:
						Lighting.wetLightG = 0.99f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightR = 0.84f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightB = 1.01f * Lighting.negLight * Lighting.blueWave;
						break;
					case 6:
						Lighting.wetLightG = 0.98f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightR = 0.95f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightB = 0.85f * Lighting.negLight * Lighting.blueWave;
						break;
					case 9:
						Lighting.wetLightG = 0.88f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightR = 1f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightB = 0.84f * Lighting.negLight * Lighting.blueWave;
						break;
					case 10:
						Lighting.wetLightG = 1f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightR = 0.83f * Lighting.negLight * Lighting.blueWave;
						Lighting.wetLightB = 1f * Lighting.negLight * Lighting.blueWave;
						break;
					default:
						Lighting.wetLightG = 0f;
						Lighting.wetLightR = 0f;
						Lighting.wetLightB = 0f;
						break;
					}
				}
				else
				{
					Lighting.negLight = 0.9f;
					Lighting.negLight2 = 0.54f;
					Lighting.wetLightR = 0.95f * Lighting.negLight * Lighting.blueWave;
				}
				if (Main.player[Main.myPlayer].nightVision)
				{
					Lighting.negLight *= 1.03f;
					Lighting.negLight2 *= 1.03f;
				}
				if (Main.player[Main.myPlayer].blind)
				{
					Lighting.negLight *= 0.95f;
					Lighting.negLight2 *= 0.95f;
				}
				if (Main.player[Main.myPlayer].blackout)
				{
					Lighting.negLight *= 0.85f;
					Lighting.negLight2 *= 0.85f;
				}
			}
			else
			{
				Lighting.negLight = 0.04f;
				Lighting.negLight2 = 0.16f;
				if (Main.player[Main.myPlayer].nightVision)
				{
					Lighting.negLight -= 0.013f;
					Lighting.negLight2 -= 0.04f;
				}
				if (Main.player[Main.myPlayer].blind)
				{
					Lighting.negLight += 0.03f;
					Lighting.negLight2 += 0.06f;
				}
				if (Main.player[Main.myPlayer].blackout)
				{
					Lighting.negLight += 0.09f;
					Lighting.negLight2 += 0.18f;
				}
				Lighting.wetLightR = Lighting.negLight * 1.2f;
				Lighting.wetLightG = Lighting.negLight * 1.1f;
			}
			int num;
			int num2;
			switch (Main.renderCount)
			{
			case 0:
				num = 0;
				num2 = 1;
				break;
			case 1:
				num = 1;
				num2 = 3;
				break;
			case 2:
				num = 3;
				num2 = 4;
				break;
			default:
				num = 0;
				num2 = 0;
				break;
			}
			if (Lighting.LightingThreads < 0)
			{
				Lighting.LightingThreads = 0;
			}
			if (Lighting.LightingThreads >= Environment.ProcessorCount)
			{
				Lighting.LightingThreads = Environment.ProcessorCount - 1;
			}
			int num3 = Lighting.LightingThreads;
			if (num3 > 0)
			{
				num3++;
			}
			Stopwatch stopwatch = new Stopwatch();
			for (int i = num; i < num2; i++)
			{
				stopwatch.Restart();
				switch (i)
				{
				case 0:
					Lighting.swipe.innerLoop1Start = Lighting.minY7 - Lighting.firstToLightY7;
					Lighting.swipe.innerLoop1End = Lighting.lastToLightY27 + Lighting.maxRenderCount - Lighting.firstToLightY7;
					Lighting.swipe.innerLoop2Start = Lighting.maxY7 - Lighting.firstToLightY;
					Lighting.swipe.innerLoop2End = Lighting.firstTileY7 - Lighting.maxRenderCount - Lighting.firstToLightY7;
					Lighting.swipe.outerLoopStart = Lighting.minX7 - Lighting.firstToLightX7;
					Lighting.swipe.outerLoopEnd = Lighting.maxX7 - Lighting.firstToLightX7;
					Lighting.swipe.jaggedArray = Lighting.states;
					break;
				case 1:
					Lighting.swipe.innerLoop1Start = Lighting.minX7 - Lighting.firstToLightX7;
					Lighting.swipe.innerLoop1End = Lighting.lastTileX7 + Lighting.maxRenderCount - Lighting.firstToLightX7;
					Lighting.swipe.innerLoop2Start = Lighting.maxX7 - Lighting.firstToLightX7;
					Lighting.swipe.innerLoop2End = Lighting.firstTileX7 - Lighting.maxRenderCount - Lighting.firstToLightX7;
					Lighting.swipe.outerLoopStart = Lighting.firstToLightY7 - Lighting.firstToLightY7;
					Lighting.swipe.outerLoopEnd = Lighting.lastToLightY7 - Lighting.firstToLightY7;
					Lighting.swipe.jaggedArray = Lighting.axisFlipStates;
					break;
				case 2:
					Lighting.swipe.innerLoop1Start = Lighting.firstToLightY27 - Lighting.firstToLightY7;
					Lighting.swipe.innerLoop1End = Lighting.lastTileY7 + Lighting.maxRenderCount - Lighting.firstToLightY7;
					Lighting.swipe.innerLoop2Start = Lighting.lastToLightY27 - Lighting.firstToLightY;
					Lighting.swipe.innerLoop2End = Lighting.firstTileY7 - Lighting.maxRenderCount - Lighting.firstToLightY7;
					Lighting.swipe.outerLoopStart = Lighting.firstToLightX27 - Lighting.firstToLightX7;
					Lighting.swipe.outerLoopEnd = Lighting.lastToLightX27 - Lighting.firstToLightX7;
					Lighting.swipe.jaggedArray = Lighting.states;
					break;
				case 3:
					Lighting.swipe.innerLoop1Start = Lighting.firstToLightX27 - Lighting.firstToLightX7;
					Lighting.swipe.innerLoop1End = Lighting.lastTileX7 + Lighting.maxRenderCount - Lighting.firstToLightX7;
					Lighting.swipe.innerLoop2Start = Lighting.lastToLightX27 - Lighting.firstToLightX7;
					Lighting.swipe.innerLoop2End = Lighting.firstTileX7 - Lighting.maxRenderCount - Lighting.firstToLightX7;
					Lighting.swipe.outerLoopStart = Lighting.firstToLightY27 - Lighting.firstToLightY7;
					Lighting.swipe.outerLoopEnd = Lighting.lastToLightY27 - Lighting.firstToLightY7;
					Lighting.swipe.jaggedArray = Lighting.axisFlipStates;
					break;
				}
				if (Lighting.swipe.innerLoop1Start > Lighting.swipe.innerLoop1End)
				{
					Lighting.swipe.innerLoop1Start = Lighting.swipe.innerLoop1End;
				}
				if (Lighting.swipe.innerLoop2Start < Lighting.swipe.innerLoop2End)
				{
					Lighting.swipe.innerLoop2Start = Lighting.swipe.innerLoop2End;
				}
				if (Lighting.swipe.outerLoopStart > Lighting.swipe.outerLoopEnd)
				{
					Lighting.swipe.outerLoopStart = Lighting.swipe.outerLoopEnd;
				}
				switch (Lighting.lightMode)
				{
				case 0:
					Lighting.swipe.function = new Action<Lighting.LightingSwipeData>(Lighting.doColors_Mode0_Swipe);
					break;
				case 1:
					Lighting.swipe.function = new Action<Lighting.LightingSwipeData>(Lighting.doColors_Mode1_Swipe);
					break;
				case 2:
					Lighting.swipe.function = new Action<Lighting.LightingSwipeData>(Lighting.doColors_Mode2_Swipe);
					break;
				case 3:
					Lighting.swipe.function = new Action<Lighting.LightingSwipeData>(Lighting.doColors_Mode3_Swipe);
					break;
				default:
					Lighting.swipe.function = null;
					break;
				}
				if (num3 == 0)
				{
					Lighting.swipe.function(Lighting.swipe);
				}
				else
				{
					int num4 = Lighting.swipe.outerLoopEnd - Lighting.swipe.outerLoopStart;
					int num5 = num4 / num3;
					int num6 = num4 % num3;
					int num7 = Lighting.swipe.outerLoopStart;
					Lighting.countdown.Reset(num3);
					for (int j = 0; j < num3; j++)
					{
						Lighting.LightingSwipeData lightingSwipeData = Lighting.threadSwipes[j];
						lightingSwipeData.CopyFrom(Lighting.swipe);
						lightingSwipeData.outerLoopStart = num7;
						num7 += num5;
						if (num6 > 0)
						{
							num7++;
							num6--;
						}
						lightingSwipeData.outerLoopEnd = num7;
						ThreadPool.QueueUserWorkItem(new WaitCallback(Lighting.callback_LightingSwipe), lightingSwipeData);
					}
					Lighting.countdown.Wait();
				}
				TimeLogger.LightingTime(i + 1, stopwatch.Elapsed.TotalMilliseconds);
			}
		}
		private static void callback_LightingSwipe(object obj)
		{
			Lighting.LightingSwipeData lightingSwipeData = obj as Lighting.LightingSwipeData;
			try
			{
				lightingSwipeData.function(lightingSwipeData);
			}
			catch
			{
			}
			Lighting.countdown.Signal();
		}
		private static void doColors_Mode0_Swipe(Lighting.LightingSwipeData swipeData)
		{
			try
			{
				bool flag = true;
				while (true)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = swipeData.innerLoop1Start;
						num3 = swipeData.innerLoop1End;
					}
					else
					{
						num = -1;
						num2 = swipeData.innerLoop2Start;
						num3 = swipeData.innerLoop2End;
					}
					int outerLoopStart = swipeData.outerLoopStart;
					int outerLoopEnd = swipeData.outerLoopEnd;
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						Lighting.LightingState[] array = swipeData.jaggedArray[i];
						float num4 = 0f;
						float num5 = 0f;
						float num6 = 0f;
						int num7 = num2;
						while (num7 != num3)
						{
							Lighting.LightingState lightingState = array[num7];
							Lighting.LightingState lightingState2 = array[num7 + num];
							bool flag3;
							bool flag2 = flag3 = false;
							if (lightingState.r2 > num4)
							{
								num4 = lightingState.r2;
							}
							else
							{
								if ((double)num4 <= 0.0185)
								{
									flag3 = true;
								}
								else
								{
									if (lightingState.r2 < num4)
									{
										lightingState.r2 = num4;
									}
								}
							}
							if (!flag3 && lightingState2.r2 <= num4)
							{
								if (lightingState.stopLight)
								{
									num4 *= Lighting.negLight2;
								}
								else
								{
									if (lightingState.wetLight)
									{
										if (lightingState.honeyLight)
										{
											num4 *= Lighting.honeyLightR * (float)swipeData.rand.Next(98, 100) * 0.01f;
										}
										else
										{
											num4 *= Lighting.wetLightR * (float)swipeData.rand.Next(98, 100) * 0.01f;
										}
									}
									else
									{
										num4 *= Lighting.negLight;
									}
								}
							}
							if (lightingState.g2 > num5)
							{
								num5 = lightingState.g2;
							}
							else
							{
								if ((double)num5 <= 0.0185)
								{
									flag2 = true;
								}
								else
								{
									lightingState.g2 = num5;
								}
							}
							if (!flag2 && lightingState2.g2 <= num5)
							{
								if (lightingState.stopLight)
								{
									num5 *= Lighting.negLight2;
								}
								else
								{
									if (lightingState.wetLight)
									{
										if (lightingState.honeyLight)
										{
											num5 *= Lighting.honeyLightG * (float)swipeData.rand.Next(97, 100) * 0.01f;
										}
										else
										{
											num5 *= Lighting.wetLightG * (float)swipeData.rand.Next(97, 100) * 0.01f;
										}
									}
									else
									{
										num5 *= Lighting.negLight;
									}
								}
							}
							if (lightingState.b2 > num6)
							{
								num6 = lightingState.b2;
								goto IL_22F;
							}
							if ((double)num6 > 0.0185)
							{
								lightingState.b2 = num6;
								goto IL_22F;
							}
							IL_2B1:
							num7 += num;
							continue;
							IL_22F:
							if (lightingState2.b2 >= num6)
							{
								goto IL_2B1;
							}
							if (lightingState.stopLight)
							{
								num6 *= Lighting.negLight2;
								goto IL_2B1;
							}
							if (!lightingState.wetLight)
							{
								num6 *= Lighting.negLight;
								goto IL_2B1;
							}
							if (lightingState.honeyLight)
							{
								num6 *= Lighting.honeyLightB * (float)swipeData.rand.Next(97, 100) * 0.01f;
								goto IL_2B1;
							}
							num6 *= Lighting.wetLightB * (float)swipeData.rand.Next(97, 100) * 0.01f;
							goto IL_2B1;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}
		private static void doColors_Mode1_Swipe(Lighting.LightingSwipeData swipeData)
		{
			try
			{
				bool flag = true;
				while (true)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = swipeData.innerLoop1Start;
						num3 = swipeData.innerLoop1End;
					}
					else
					{
						num = -1;
						num2 = swipeData.innerLoop2Start;
						num3 = swipeData.innerLoop2End;
					}
					int outerLoopStart = swipeData.outerLoopStart;
					int outerLoopEnd = swipeData.outerLoopEnd;
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						Lighting.LightingState[] array = swipeData.jaggedArray[i];
						float num4 = 0f;
						int num5 = num2;
						while (num5 != num3)
						{
							Lighting.LightingState lightingState = array[num5];
							if (lightingState.r2 > num4)
							{
								num4 = lightingState.r2;
								goto IL_9C;
							}
							if ((double)num4 > 0.0185)
							{
								if (lightingState.r2 < num4)
								{
									lightingState.r2 = num4;
									goto IL_9C;
								}
								goto IL_9C;
							}
							IL_123:
							num5 += num;
							continue;
							IL_9C:
							if (array[num5 + num].r2 > num4)
							{
								goto IL_123;
							}
							if (lightingState.stopLight)
							{
								num4 *= Lighting.negLight2;
								goto IL_123;
							}
							if (!lightingState.wetLight)
							{
								num4 *= Lighting.negLight;
								goto IL_123;
							}
							if (lightingState.honeyLight)
							{
								num4 *= Lighting.honeyLightR * (float)swipeData.rand.Next(98, 100) * 0.01f;
								goto IL_123;
							}
							num4 *= Lighting.wetLightR * (float)swipeData.rand.Next(98, 100) * 0.01f;
							goto IL_123;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}
		private static void doColors_Mode2_Swipe(Lighting.LightingSwipeData swipeData)
		{
			try
			{
				bool flag = true;
				while (true)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = swipeData.innerLoop1Start;
						num3 = swipeData.innerLoop1End;
					}
					else
					{
						num = -1;
						num2 = swipeData.innerLoop2Start;
						num3 = swipeData.innerLoop2End;
					}
					int outerLoopStart = swipeData.outerLoopStart;
					int outerLoopEnd = swipeData.outerLoopEnd;
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						Lighting.LightingState[] array = swipeData.jaggedArray[i];
						float num4 = 0f;
						int num5 = num2;
						while (num5 != num3)
						{
							Lighting.LightingState lightingState = array[num5];
							if (lightingState.r2 > num4)
							{
								num4 = lightingState.r2;
								goto IL_86;
							}
							if (num4 > 0f)
							{
								lightingState.r2 = num4;
								goto IL_86;
							}
							IL_BA:
							num5 += num;
							continue;
							IL_86:
							if (lightingState.stopLight)
							{
								num4 -= Lighting.negLight2;
								goto IL_BA;
							}
							if (lightingState.wetLight)
							{
								num4 -= Lighting.wetLightR;
								goto IL_BA;
							}
							num4 -= Lighting.negLight;
							goto IL_BA;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}
		private static void doColors_Mode3_Swipe(Lighting.LightingSwipeData swipeData)
		{
			try
			{
				bool flag = true;
				while (true)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = swipeData.innerLoop1Start;
						num3 = swipeData.innerLoop1End;
					}
					else
					{
						num = -1;
						num2 = swipeData.innerLoop2Start;
						num3 = swipeData.innerLoop2End;
					}
					int outerLoopStart = swipeData.outerLoopStart;
					int outerLoopEnd = swipeData.outerLoopEnd;
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						Lighting.LightingState[] array = swipeData.jaggedArray[i];
						float num4 = 0f;
						float num5 = 0f;
						float num6 = 0f;
						int num7 = num2;
						while (num7 != num3)
						{
							Lighting.LightingState lightingState = array[num7];
							bool flag3;
							bool flag2 = flag3 = false;
							if (lightingState.r2 > num4)
							{
								num4 = lightingState.r2;
							}
							else
							{
								if (num4 <= 0f)
								{
									flag3 = true;
								}
								else
								{
									lightingState.r2 = num4;
								}
							}
							if (!flag3)
							{
								if (lightingState.stopLight)
								{
									num4 -= Lighting.negLight2;
								}
								else
								{
									if (lightingState.wetLight)
									{
										num4 -= Lighting.wetLightR;
									}
									else
									{
										num4 -= Lighting.negLight;
									}
								}
							}
							if (lightingState.g2 > num5)
							{
								num5 = lightingState.g2;
							}
							else
							{
								if (num5 <= 0f)
								{
									flag2 = true;
								}
								else
								{
									lightingState.g2 = num5;
								}
							}
							if (!flag2)
							{
								if (lightingState.stopLight)
								{
									num5 -= Lighting.negLight2;
								}
								else
								{
									if (lightingState.wetLight)
									{
										num5 -= Lighting.wetLightG;
									}
									else
									{
										num5 -= Lighting.negLight;
									}
								}
							}
							if (lightingState.b2 > num6)
							{
								num6 = lightingState.b2;
								goto IL_167;
							}
							if (num6 > 0f)
							{
								lightingState.b2 = num6;
								goto IL_167;
							}
							IL_186:
							num7 += num;
							continue;
							IL_167:
							if (lightingState.stopLight)
							{
								num6 -= Lighting.negLight2;
								goto IL_186;
							}
							num6 -= Lighting.negLight;
							goto IL_186;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}
		public static void addLight(int i, int j, float R, float G, float B)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			if (i - Lighting.firstTileX + Lighting.offScreenTiles >= 0 && i - Lighting.firstTileX + Lighting.offScreenTiles < Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 && j - Lighting.firstTileY + Lighting.offScreenTiles >= 0 && j - Lighting.firstTileY + Lighting.offScreenTiles < Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10)
			{
				if (Lighting.tempLights.Count == Lighting.maxTempLights)
				{
					return;
				}
				Point16 key = new Point16(i, j);
				Lighting.ColorTriplet value;
				if (Lighting.tempLights.TryGetValue(key, out value))
				{
					if (Lighting.RGB)
					{
						if (value.r < R)
						{
							value.r = R;
						}
						if (value.g < G)
						{
							value.g = G;
						}
						if (value.b < B)
						{
							value.b = B;
						}
						Lighting.tempLights[key] = value;
						return;
					}
					float num = (R + G + B) / 3f;
					if (value.r < num)
					{
						Lighting.tempLights[key] = new Lighting.ColorTriplet(num);
						return;
					}
				}
				else
				{
					if (Lighting.RGB)
					{
						value = new Lighting.ColorTriplet(R, G, B);
					}
					else
					{
						value = new Lighting.ColorTriplet((R + G + B) / 3f);
					}
					Lighting.tempLights.Add(key, value);
				}
			}
		}
		public static void NextLightMode()
		{
			Lighting.lightCounter += 100;
			Lighting.lightMode++;
			if (Lighting.lightMode >= 4)
			{
				Lighting.lightMode = 0;
			}
			if (Lighting.lightMode == 2 || Lighting.lightMode == 0)
			{
				Main.renderCount = 0;
				Main.renderNow = true;
				Lighting.BlackOut();
			}
		}
		public static void BlackOut()
		{
			int num = Main.screenWidth / 16 + Lighting.offScreenTiles * 2;
			int num2 = Main.screenHeight / 16 + Lighting.offScreenTiles * 2;
			for (int i = 0; i < num; i++)
			{
				Lighting.LightingState[] array = Lighting.states[i];
				for (int j = 0; j < num2; j++)
				{
					Lighting.LightingState lightingState = array[j];
					lightingState.r = 0f;
					lightingState.g = 0f;
					lightingState.b = 0f;
				}
			}
		}
		public static Color GetColor(int x, int y, Color oldColor)
		{
			int num = x - Lighting.firstTileX + Lighting.offScreenTiles;
			int num2 = y - Lighting.firstTileY + Lighting.offScreenTiles;
			if (Main.gameMenu)
			{
				return oldColor;
			}
			if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 || num2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10)
			{
				return Color.Black;
			}
			Color white = Color.White;
			Lighting.LightingState lightingState = Lighting.states[num][num2];
			int num3 = (int)((float)oldColor.R * lightingState.r * Lighting.brightness);
			int num4 = (int)((float)oldColor.G * lightingState.g * Lighting.brightness);
			int num5 = (int)((float)oldColor.B * lightingState.b * Lighting.brightness);
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			if (num5 > 255)
			{
				num5 = 255;
			}
			white.R = (byte)num3;
			white.G = (byte)num4;
			white.B = (byte)num5;
			return white;
		}
		public static Color GetColor(int x, int y)
		{
			int num = x - Lighting.firstTileX + Lighting.offScreenTiles;
			int num2 = y - Lighting.firstTileY + Lighting.offScreenTiles;
			if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 || num2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2)
			{
				return Color.Black;
			}
			Lighting.LightingState lightingState = Lighting.states[num][num2];
			int num3 = (int)(255f * lightingState.r * Lighting.brightness);
			int num4 = (int)(255f * lightingState.g * Lighting.brightness);
			int num5 = (int)(255f * lightingState.b * Lighting.brightness);
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			if (num5 > 255)
			{
				num5 = 255;
			}
			Color result = new Color((int)((byte)num3), (int)((byte)num4), (int)((byte)num5), 255);
			return result;
		}
		public static void GetColor9Slice(int centerX, int centerY, ref Color[] slices)
		{
			int num = centerX - Lighting.firstTileX + Lighting.offScreenTiles;
			int num2 = centerY - Lighting.firstTileY + Lighting.offScreenTiles;
			if (num <= 0 || num2 <= 0 || num >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 - 1 || num2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 - 1)
			{
				for (int i = 0; i < 9; i++)
				{
					slices[i] = Color.Black;
				}
				return;
			}
			int num3 = 0;
			for (int j = num - 1; j <= num + 1; j++)
			{
				Lighting.LightingState[] array = Lighting.states[j];
				for (int k = num2 - 1; k <= num2 + 1; k++)
				{
					Lighting.LightingState lightingState = array[k];
					int num4 = (int)(255f * lightingState.r * Lighting.brightness);
					int num5 = (int)(255f * lightingState.g * Lighting.brightness);
					int num6 = (int)(255f * lightingState.b * Lighting.brightness);
					if (num4 > 255)
					{
						num4 = 255;
					}
					if (num5 > 255)
					{
						num5 = 255;
					}
					if (num6 > 255)
					{
						num6 = 255;
					}
					slices[num3] = new Color((int)((byte)num4), (int)((byte)num5), (int)((byte)num6), 255);
					num3 += 3;
				}
				num3 -= 8;
			}
		}
		public static void GetColor4Slice(int centerX, int centerY, ref Color[] slices)
		{
			int i = centerX - Lighting.firstTileX + Lighting.offScreenTiles;
			int num = centerY - Lighting.firstTileY + Lighting.offScreenTiles;
			if (i <= 0 || num <= 0 || i >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 - 1 || num >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 - 1)
			{
				for (i = 0; i < 4; i++)
				{
					slices[i] = Color.Black;
				}
				return;
			}
			Lighting.LightingState lightingState = Lighting.states[i][num - 1];
			Lighting.LightingState lightingState2 = Lighting.states[i][num + 1];
			Lighting.LightingState lightingState3 = Lighting.states[i - 1][num];
			Lighting.LightingState lightingState4 = Lighting.states[i + 1][num];
			float num2 = lightingState.r + lightingState.g + lightingState.b;
			float num3 = lightingState2.r + lightingState2.g + lightingState2.b;
			float num4 = lightingState4.r + lightingState4.g + lightingState4.b;
			float num5 = lightingState3.r + lightingState3.g + lightingState3.b;
			if (num2 >= num5)
			{
				int num6 = (int)(255f * lightingState3.r * Lighting.brightness);
				int num7 = (int)(255f * lightingState3.g * Lighting.brightness);
				int num8 = (int)(255f * lightingState3.b * Lighting.brightness);
				if (num6 > 255)
				{
					num6 = 255;
				}
				if (num7 > 255)
				{
					num7 = 255;
				}
				if (num8 > 255)
				{
					num8 = 255;
				}
				slices[0] = new Color((int)((byte)num6), (int)((byte)num7), (int)((byte)num8), 255);
			}
			else
			{
				int num9 = (int)(255f * lightingState.r * Lighting.brightness);
				int num10 = (int)(255f * lightingState.g * Lighting.brightness);
				int num11 = (int)(255f * lightingState.b * Lighting.brightness);
				if (num9 > 255)
				{
					num9 = 255;
				}
				if (num10 > 255)
				{
					num10 = 255;
				}
				if (num11 > 255)
				{
					num11 = 255;
				}
				slices[0] = new Color((int)((byte)num9), (int)((byte)num10), (int)((byte)num11), 255);
			}
			if (num2 >= num4)
			{
				int num12 = (int)(255f * lightingState4.r * Lighting.brightness);
				int num13 = (int)(255f * lightingState4.g * Lighting.brightness);
				int num14 = (int)(255f * lightingState4.b * Lighting.brightness);
				if (num12 > 255)
				{
					num12 = 255;
				}
				if (num13 > 255)
				{
					num13 = 255;
				}
				if (num14 > 255)
				{
					num14 = 255;
				}
				slices[1] = new Color((int)((byte)num12), (int)((byte)num13), (int)((byte)num14), 255);
			}
			else
			{
				int num15 = (int)(255f * lightingState.r * Lighting.brightness);
				int num16 = (int)(255f * lightingState.g * Lighting.brightness);
				int num17 = (int)(255f * lightingState.b * Lighting.brightness);
				if (num15 > 255)
				{
					num15 = 255;
				}
				if (num16 > 255)
				{
					num16 = 255;
				}
				if (num17 > 255)
				{
					num17 = 255;
				}
				slices[1] = new Color((int)((byte)num15), (int)((byte)num16), (int)((byte)num17), 255);
			}
			if (num3 >= num5)
			{
				int num18 = (int)(255f * lightingState3.r * Lighting.brightness);
				int num19 = (int)(255f * lightingState3.g * Lighting.brightness);
				int num20 = (int)(255f * lightingState3.b * Lighting.brightness);
				if (num18 > 255)
				{
					num18 = 255;
				}
				if (num19 > 255)
				{
					num19 = 255;
				}
				if (num20 > 255)
				{
					num20 = 255;
				}
				slices[2] = new Color((int)((byte)num18), (int)((byte)num19), (int)((byte)num20), 255);
			}
			else
			{
				int num21 = (int)(255f * lightingState2.r * Lighting.brightness);
				int num22 = (int)(255f * lightingState2.g * Lighting.brightness);
				int num23 = (int)(255f * lightingState2.b * Lighting.brightness);
				if (num21 > 255)
				{
					num21 = 255;
				}
				if (num22 > 255)
				{
					num22 = 255;
				}
				if (num23 > 255)
				{
					num23 = 255;
				}
				slices[2] = new Color((int)((byte)num21), (int)((byte)num22), (int)((byte)num23), 255);
			}
			if (num3 >= num4)
			{
				int num24 = (int)(255f * lightingState4.r * Lighting.brightness);
				int num25 = (int)(255f * lightingState4.g * Lighting.brightness);
				int num26 = (int)(255f * lightingState4.b * Lighting.brightness);
				if (num24 > 255)
				{
					num24 = 255;
				}
				if (num25 > 255)
				{
					num25 = 255;
				}
				if (num26 > 255)
				{
					num26 = 255;
				}
				slices[3] = new Color((int)((byte)num24), (int)((byte)num25), (int)((byte)num26), 255);
				return;
			}
			int num27 = (int)(255f * lightingState2.r * Lighting.brightness);
			int num28 = (int)(255f * lightingState2.g * Lighting.brightness);
			int num29 = (int)(255f * lightingState2.b * Lighting.brightness);
			if (num27 > 255)
			{
				num27 = 255;
			}
			if (num28 > 255)
			{
				num28 = 255;
			}
			if (num29 > 255)
			{
				num29 = 255;
			}
			slices[3] = new Color((int)((byte)num27), (int)((byte)num28), (int)((byte)num29), 255);
		}
		public static Color GetBlackness(int x, int y)
		{
			int num = x - Lighting.firstTileX + Lighting.offScreenTiles;
			int num2 = y - Lighting.firstTileY + Lighting.offScreenTiles;
			if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 || num2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10)
			{
				return Color.Black;
			}
			Color result = new Color(0, 0, 0, (int)((byte)(255f - 255f * Lighting.states[num][num2].r)));
			return result;
		}
		public static float Brightness(int x, int y)
		{
			int num = x - Lighting.firstTileX + Lighting.offScreenTiles;
			int num2 = y - Lighting.firstTileY + Lighting.offScreenTiles;
			if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + Lighting.offScreenTiles * 2 + 10 || num2 >= Main.screenHeight / 16 + Lighting.offScreenTiles * 2 + 10)
			{
				return 0f;
			}
			Lighting.LightingState lightingState = Lighting.states[num][num2];
			return (lightingState.r + lightingState.g + lightingState.b) / 3f;
		}
	}
}
