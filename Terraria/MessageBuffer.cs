using Microsoft.Xna.Framework;
using System;
using System.IO;
namespace Terraria
{
	public class MessageBuffer
	{
		public const int readBufferMax = 65535;
		public const int writeBufferMax = 65535;
		public bool broadcast;
		public byte[] readBuffer = new byte[65535];
		public byte[] writeBuffer = new byte[65535];
		public bool writeLocked;
		public int messageLength;
		public int totalData;
		public int whoAmI;
		public int spamCount;
		public int maxSpam;
		public bool checkBytes;
		public MemoryStream readerStream;
		public MemoryStream writerStream;
		public BinaryReader reader;
		public BinaryWriter writer;
		public void Reset()
		{
			this.readBuffer = new byte[65535];
			this.writeBuffer = new byte[65535];
			this.writeLocked = false;
			this.messageLength = 0;
			this.totalData = 0;
			this.spamCount = 0;
			this.broadcast = false;
			this.checkBytes = false;
			this.ResetReader();
			this.ResetWriter();
		}
		public void ResetReader()
		{
			if (this.readerStream != null)
			{
				this.readerStream.Close();
			}
			this.readerStream = new MemoryStream(this.readBuffer);
			this.reader = new BinaryReader(this.readerStream);
		}
		public void ResetWriter()
		{
			if (this.writerStream != null)
			{
				this.writerStream.Close();
			}
			this.writerStream = new MemoryStream(this.writeBuffer);
			this.writer = new BinaryWriter(this.writerStream);
		}
		public void GetData(int start, int length)
		{
			if (this.whoAmI < 256)
			{
				Netplay.serverSock[this.whoAmI].timeOut = 0;
			}
			else
			{
				Netplay.clientSock.timeOut = 0;
			}
			int num = start + 1;
			byte b = this.readBuffer[start];
			Main.rxMsg++;
			Main.rxData += length;
			Main.rxMsgType[(int)b]++;
			Main.rxDataType[(int)b] += length;
			if (Main.netMode == 1 && Netplay.clientSock.statusMax > 0)
			{
				Netplay.clientSock.statusCount++;
			}
			if (Main.verboseNetplay)
			{
				for (int i = start; i < start + length; i++)
				{
				}
				for (int j = start; j < start + length; j++)
				{
					byte arg_CD_0 = this.readBuffer[j];
				}
			}
			if (Main.netMode == 2 && b != 38 && Netplay.serverSock[this.whoAmI].state == -1)
			{
				NetMessage.SendData(2, this.whoAmI, -1, Lang.mp[1], 0, 0f, 0f, 0f, 0);
				return;
			}
			if (Main.netMode == 2 && Netplay.serverSock[this.whoAmI].state < 10 && b > 12 && b != 16 && b != 42 && b != 50 && b != 38 && b != 68)
			{
				NetMessage.BootPlayer(this.whoAmI, Lang.mp[2]);
			}
			if (this.reader == null)
			{
				this.ResetReader();
			}
			this.reader.BaseStream.Position = (long)num;
			switch (b)
			{
			case 1:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				if (Main.dedServ && Netplay.CheckBan(Netplay.serverSock[this.whoAmI].tcpClient.Client.RemoteEndPoint.ToString()))
				{
					NetMessage.SendData(2, this.whoAmI, -1, Lang.mp[3], 0, 0f, 0f, 0f, 0);
					return;
				}
				if (Netplay.serverSock[this.whoAmI].state != 0)
				{
					return;
				}
				string a = this.reader.ReadString();
				if (!(a == "Terraria" + Main.curRelease))
				{
					NetMessage.SendData(2, this.whoAmI, -1, Lang.mp[4], 0, 0f, 0f, 0f, 0);
					return;
				}
				if (string.IsNullOrEmpty(Netplay.password))
				{
					Netplay.serverSock[this.whoAmI].state = 1;
					NetMessage.SendData(3, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
					return;
				}
				Netplay.serverSock[this.whoAmI].state = -1;
				NetMessage.SendData(37, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
				return;
			}
			case 2:
				if (Main.netMode != 1)
				{
					return;
				}
				Netplay.disconnect = true;
				Main.statusText = this.reader.ReadString();
				return;
			case 3:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				if (Netplay.clientSock.state == 1)
				{
					Netplay.clientSock.state = 2;
				}
				int num2 = (int)this.reader.ReadByte();
				if (num2 != Main.myPlayer)
				{
					Main.player[num2] = (Player)Main.player[Main.myPlayer].Clone();
					Main.player[Main.myPlayer] = new Player();
					Main.player[num2].whoAmi = num2;
					Main.myPlayer = num2;
				}
				Player player = Main.player[num2];
				NetMessage.SendData(4, -1, -1, player.name, num2, 0f, 0f, 0f, 0);
				NetMessage.SendData(68, -1, -1, "", num2, 0f, 0f, 0f, 0);
				NetMessage.SendData(16, -1, -1, "", num2, 0f, 0f, 0f, 0);
				NetMessage.SendData(42, -1, -1, "", num2, 0f, 0f, 0f, 0);
				NetMessage.SendData(50, -1, -1, "", num2, 0f, 0f, 0f, 0);
				for (int k = 0; k < 59; k++)
				{
					NetMessage.SendData(5, -1, -1, player.inventory[k].name, num2, (float)k, (float)player.inventory[k].prefix, 0f, 0);
				}
				for (int l = 0; l < 16; l++)
				{
					NetMessage.SendData(5, -1, -1, player.armor[l].name, num2, (float)(59 + l), (float)player.armor[l].prefix, 0f, 0);
				}
				for (int m = 0; m < 8; m++)
				{
					NetMessage.SendData(5, -1, -1, player.dye[m].name, num2, (float)(75 + m), (float)player.dye[m].prefix, 0f, 0);
				}
				NetMessage.SendData(6, -1, -1, "", 0, 0f, 0f, 0f, 0);
				if (Netplay.clientSock.state == 2)
				{
					Netplay.clientSock.state = 3;
					return;
				}
				return;
			}
			case 4:
			{
				int num3 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num3 = this.whoAmI;
				}
				if (num3 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				Player player2 = Main.player[num3];
				player2.whoAmi = num3;
				if (this.reader.ReadByte() == 0)
				{
					player2.male = true;
				}
				else
				{
					player2.male = false;
				}
				player2.hair = (int)this.reader.ReadByte();
				if (player2.hair >= 123)
				{
					player2.hair = 0;
				}
				player2.name = this.reader.ReadString().Trim().Trim();
				player2.hairDye = this.reader.ReadByte();
				player2.hideVisual = this.reader.ReadByte();
				player2.hairColor = this.reader.ReadRGB();
				player2.skinColor = this.reader.ReadRGB();
				player2.eyeColor = this.reader.ReadRGB();
				player2.shirtColor = this.reader.ReadRGB();
				player2.underShirtColor = this.reader.ReadRGB();
				player2.pantsColor = this.reader.ReadRGB();
				player2.shoeColor = this.reader.ReadRGB();
				player2.difficulty = this.reader.ReadByte();
				if (Main.netMode != 2)
				{
					return;
				}
				bool flag = false;
				if (Netplay.serverSock[this.whoAmI].state < 10)
				{
					for (int n = 0; n < 255; n++)
					{
						if (n != num3 && player2.name == Main.player[n].name && Netplay.serverSock[n].active)
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					NetMessage.SendData(2, this.whoAmI, -1, player2.name + " " + Lang.mp[5], 0, 0f, 0f, 0f, 0);
					return;
				}
				if (player2.name.Length > Player.nameLen)
				{
					NetMessage.SendData(2, this.whoAmI, -1, "Name is too long.", 0, 0f, 0f, 0f, 0);
					return;
				}
				if (player2.name == "")
				{
					NetMessage.SendData(2, this.whoAmI, -1, "Empty name.", 0, 0f, 0f, 0f, 0);
					return;
				}
				Netplay.serverSock[this.whoAmI].oldName = player2.name;
				Netplay.serverSock[this.whoAmI].name = player2.name;
				NetMessage.SendData(4, -1, this.whoAmI, player2.name, num3, 0f, 0f, 0f, 0);
				return;
			}
			case 5:
			{
				int num4 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num4 = this.whoAmI;
				}
				if (num4 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				Player player3 = Main.player[num4];
				lock (player3)
				{
					int num5 = (int)this.reader.ReadByte();
					int stack = (int)this.reader.ReadInt16();
					int num6 = (int)this.reader.ReadByte();
					int type = (int)this.reader.ReadInt16();
					if (num5 < 59)
					{
						player3.inventory[num5] = new Item();
						player3.inventory[num5].netDefaults(type);
						player3.inventory[num5].stack = stack;
						player3.inventory[num5].Prefix(num6);
						if (num4 == Main.myPlayer && num5 == 58)
						{
							Main.mouseItem = player3.inventory[num5].Clone();
						}
					}
					else
					{
						if (num5 >= 75 && num5 <= 82)
						{
							int num7 = num5 - 58 - 17;
							player3.dye[num7] = new Item();
							player3.dye[num7].netDefaults(type);
							player3.dye[num7].stack = stack;
							player3.dye[num7].Prefix(num6);
						}
						else
						{
							int num8 = num5 - 58 - 1;
							player3.armor[num8] = new Item();
							player3.armor[num8].netDefaults(type);
							player3.armor[num8].stack = stack;
							player3.armor[num8].Prefix(num6);
						}
					}
					if (Main.netMode == 2 && num4 == this.whoAmI)
					{
						NetMessage.SendData(5, -1, this.whoAmI, "", num4, (float)num5, (float)num6, 0f, 0);
					}
					return;
				}
				break;
			}
			case 6:
				break;
			case 7:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				Main.time = (double)this.reader.ReadInt32();
				BitsByte bitsByte = this.reader.ReadByte();
				Main.dayTime = bitsByte[0];
				Main.bloodMoon = bitsByte[1];
				Main.eclipse = bitsByte[2];
				Main.moonPhase = (int)this.reader.ReadByte();
				Main.maxTilesX = (int)this.reader.ReadInt16();
				Main.maxTilesY = (int)this.reader.ReadInt16();
				Main.spawnTileX = (int)this.reader.ReadInt16();
				Main.spawnTileY = (int)this.reader.ReadInt16();
				Main.worldSurface = (double)this.reader.ReadInt16();
				Main.rockLayer = (double)this.reader.ReadInt16();
				Main.worldID = this.reader.ReadInt32();
				Main.worldName = this.reader.ReadString();
				Main.moonType = (int)this.reader.ReadByte();
				WorldGen.setBG(0, (int)this.reader.ReadByte());
				WorldGen.setBG(1, (int)this.reader.ReadByte());
				WorldGen.setBG(2, (int)this.reader.ReadByte());
				WorldGen.setBG(3, (int)this.reader.ReadByte());
				WorldGen.setBG(4, (int)this.reader.ReadByte());
				WorldGen.setBG(5, (int)this.reader.ReadByte());
				WorldGen.setBG(6, (int)this.reader.ReadByte());
				WorldGen.setBG(7, (int)this.reader.ReadByte());
				Main.iceBackStyle = (int)this.reader.ReadByte();
				Main.jungleBackStyle = (int)this.reader.ReadByte();
				Main.hellBackStyle = (int)this.reader.ReadByte();
				Main.windSpeedSet = this.reader.ReadSingle();
				Main.numClouds = (int)this.reader.ReadByte();
				for (int num9 = 0; num9 < 3; num9++)
				{
					Main.treeX[num9] = this.reader.ReadInt32();
				}
				for (int num10 = 0; num10 < 4; num10++)
				{
					Main.treeStyle[num10] = (int)this.reader.ReadByte();
				}
				for (int num11 = 0; num11 < 3; num11++)
				{
					Main.caveBackX[num11] = this.reader.ReadInt32();
				}
				for (int num12 = 0; num12 < 4; num12++)
				{
					Main.caveBackStyle[num12] = (int)this.reader.ReadByte();
				}
				Main.maxRaining = this.reader.ReadSingle();
				Main.raining = (Main.maxRaining > 0f);
				BitsByte bitsByte2 = this.reader.ReadByte();
				WorldGen.shadowOrbSmashed = bitsByte2[0];
				NPC.downedBoss1 = bitsByte2[1];
				NPC.downedBoss2 = bitsByte2[2];
				NPC.downedBoss3 = bitsByte2[3];
				Main.hardMode = bitsByte2[4];
				NPC.downedClown = bitsByte2[5];
				Main.ServerSideCharacter = bitsByte2[6];
				NPC.downedPlantBoss = bitsByte2[7];
				BitsByte bitsByte3 = this.reader.ReadByte();
				NPC.downedMechBoss1 = bitsByte3[0];
				NPC.downedMechBoss2 = bitsByte3[1];
				NPC.downedMechBoss3 = bitsByte3[2];
				NPC.downedMechBossAny = bitsByte3[3];
				Main.cloudBGActive = (float)(bitsByte3[4] ? 1 : 0);
				WorldGen.crimson = bitsByte3[5];
				Main.pumpkinMoon = bitsByte3[6];
				Main.snowMoon = bitsByte3[7];
				if (Netplay.clientSock.state == 3)
				{
					Netplay.clientSock.state = 4;
					return;
				}
				return;
			}
			case 8:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num13 = this.reader.ReadInt32();
				int num14 = this.reader.ReadInt32();
				bool flag3 = true;
				if (num13 == -1 || num14 == -1)
				{
					flag3 = false;
				}
				else
				{
					if (num13 < 10 || num13 > Main.maxTilesX - 10)
					{
						flag3 = false;
					}
					else
					{
						if (num14 < 10 || num14 > Main.maxTilesY - 10)
						{
							flag3 = false;
						}
					}
				}
				int num15 = Netplay.GetSectionX(Main.spawnTileX) - 2;
				int num16 = Netplay.GetSectionY(Main.spawnTileY) - 1;
				int num17 = num15 + 5;
				int num18 = num16 + 3;
				if (num15 < 0)
				{
					num15 = 0;
				}
				if (num17 >= Main.maxSectionsX)
				{
					num17 = Main.maxSectionsX - 1;
				}
				if (num16 < 0)
				{
					num16 = 0;
				}
				if (num18 >= Main.maxSectionsY)
				{
					num18 = Main.maxSectionsY - 1;
				}
				int num19 = (num17 - num15) * (num18 - num16);
				int num20 = -1;
				int num21 = -1;
				if (flag3)
				{
					num13 = Netplay.GetSectionX(num13) - 2;
					num14 = Netplay.GetSectionY(num14) - 1;
					num20 = num13 + 5;
					num21 = num14 + 3;
					if (num13 < 0)
					{
						num13 = 0;
					}
					if (num20 >= Main.maxSectionsX)
					{
						num20 = Main.maxSectionsX - 1;
					}
					if (num14 < 0)
					{
						num14 = 0;
					}
					if (num21 >= Main.maxSectionsY)
					{
						num21 = Main.maxSectionsY - 1;
					}
					for (int num22 = num13; num22 < num20; num22++)
					{
						for (int num23 = num14; num23 < num21; num23++)
						{
							if (num22 < num15 || num22 >= num17 || num23 < num16 || num23 >= num18)
							{
								num19++;
							}
						}
					}
				}
				if (Netplay.serverSock[this.whoAmI].state == 2)
				{
					Netplay.serverSock[this.whoAmI].state = 3;
				}
				NetMessage.SendData(9, this.whoAmI, -1, Lang.inter[44], num19, 0f, 0f, 0f, 0);
				Netplay.serverSock[this.whoAmI].statusText2 = "is receiving tile data";
				Netplay.serverSock[this.whoAmI].statusMax += num19;
				for (int num24 = num15; num24 < num17; num24++)
				{
					for (int num25 = num16; num25 < num18; num25++)
					{
						NetMessage.SendSection(this.whoAmI, num24, num25, false);
					}
				}
				if (flag3)
				{
					for (int num26 = num13; num26 < num20; num26++)
					{
						for (int num27 = num14; num27 < num21; num27++)
						{
							NetMessage.SendSection(this.whoAmI, num26, num27, true);
						}
					}
					NetMessage.SendData(11, this.whoAmI, -1, "", num13, (float)num14, (float)(num20 - 1), (float)(num21 - 1), 0);
				}
				NetMessage.SendData(11, this.whoAmI, -1, "", num15, (float)num16, (float)(num17 - 1), (float)(num18 - 1), 0);
				for (int num28 = 0; num28 < 400; num28++)
				{
					if (Main.item[num28].active)
					{
						NetMessage.SendData(21, this.whoAmI, -1, "", num28, 0f, 0f, 0f, 0);
						NetMessage.SendData(22, this.whoAmI, -1, "", num28, 0f, 0f, 0f, 0);
					}
				}
				for (int num29 = 0; num29 < 200; num29++)
				{
					if (Main.npc[num29].active)
					{
						NetMessage.SendData(23, this.whoAmI, -1, "", num29, 0f, 0f, 0f, 0);
					}
				}
				for (int num30 = 0; num30 < 1000; num30++)
				{
					if (Main.projectile[num30].active && (Main.projPet[Main.projectile[num30].type] || Main.projectile[num30].netImportant))
					{
						NetMessage.SendData(27, this.whoAmI, -1, "", num30, 0f, 0f, 0f, 0);
					}
				}
				NetMessage.SendData(49, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
				NetMessage.SendData(57, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
				NetMessage.SendData(7, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
				return;
			}
			case 9:
				if (Main.netMode != 1)
				{
					return;
				}
				Netplay.clientSock.statusMax += this.reader.ReadInt32();
				Netplay.clientSock.statusText = this.reader.ReadString();
				return;
			case 10:
				if (Main.netMode != 1)
				{
					return;
				}
				NetMessage.DecompressTileBlock(this.readBuffer, num, length, true);
				return;
			case 11:
				if (Main.netMode != 1)
				{
					return;
				}
				WorldGen.SectionTileFrame((int)this.reader.ReadInt16(), (int)this.reader.ReadInt16(), (int)this.reader.ReadInt16(), (int)this.reader.ReadInt16());
				return;
			case 12:
			{
				int num31 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num31 = this.whoAmI;
				}
				Player player4 = Main.player[num31];
				player4.SpawnX = (int)this.reader.ReadInt16();
				player4.SpawnY = (int)this.reader.ReadInt16();
				player4.Spawn();
				if (Main.netMode != 2 || Netplay.serverSock[this.whoAmI].state < 3)
				{
					return;
				}
				if (Netplay.serverSock[this.whoAmI].state == 3)
				{
					Netplay.serverSock[this.whoAmI].state = 10;
					NetMessage.greetPlayer(this.whoAmI);
					NetMessage.buffer[this.whoAmI].broadcast = true;
					NetMessage.syncPlayers();
					NetMessage.SendData(12, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f, 0);
					NetMessage.SendData(74, this.whoAmI, -1, Main.player[this.whoAmI].name, Main.anglerQuest, 0f, 0f, 0f, 0);
					return;
				}
				NetMessage.SendData(12, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f, 0);
				return;
			}
			case 13:
			{
				int num32 = (int)this.reader.ReadByte();
				if (num32 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num32 = this.whoAmI;
				}
				Player player5 = Main.player[num32];
				BitsByte bitsByte4 = this.reader.ReadByte();
				player5.controlUp = bitsByte4[0];
				player5.controlDown = bitsByte4[1];
				player5.controlLeft = bitsByte4[2];
				player5.controlRight = bitsByte4[3];
				player5.controlJump = bitsByte4[4];
				player5.controlUseItem = bitsByte4[5];
				player5.direction = (bitsByte4[6] ? 1 : -1);
				BitsByte bitsByte5 = this.reader.ReadByte();
				if (bitsByte5[0])
				{
					player5.pulley = true;
					player5.pulleyDir = (bitsByte5[1] ? 2 : 1);
				}
				else
				{
					player5.pulley = false;
				}
				player5.selectedItem = (int)this.reader.ReadByte();
				player5.position = this.reader.ReadVector2();
				if (bitsByte5[2])
				{
					player5.velocity = this.reader.ReadVector2();
				}
				if (Main.netMode == 2 && Netplay.serverSock[this.whoAmI].state == 10)
				{
					NetMessage.SendData(13, -1, this.whoAmI, "", num32, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 14:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num33 = (int)this.reader.ReadByte();
				int num34 = (int)this.reader.ReadByte();
				if (num34 == 1)
				{
					if (!Main.player[num33].active)
					{
						Main.player[num33] = new Player();
					}
					Main.player[num33].active = true;
					return;
				}
				Main.player[num33].active = false;
				return;
			}
			case 15:
			case 67:
				return;
			case 16:
			{
				int num35 = (int)this.reader.ReadByte();
				if (num35 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num35 = this.whoAmI;
				}
				Player player6 = Main.player[num35];
				player6.statLife = (int)this.reader.ReadInt16();
				player6.statLifeMax = (int)this.reader.ReadInt16();
				if (player6.statLifeMax < 100)
				{
					player6.statLifeMax = 100;
				}
				player6.dead = (player6.statLife <= 0);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(16, -1, this.whoAmI, "", num35, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 17:
			{
				byte b2 = this.reader.ReadByte();
				int num36 = (int)this.reader.ReadInt16();
				int num37 = (int)this.reader.ReadInt16();
				short num38 = this.reader.ReadInt16();
				int num39 = (int)this.reader.ReadByte();
				bool flag4 = num38 == 1;
				if (Main.tile[num36, num37] == null)
				{
					Main.tile[num36, num37] = new Tile();
				}
				if (Main.netMode == 2)
				{
					if (!flag4)
					{
						if (b2 == 0 || b2 == 2 || b2 == 4)
						{
							Netplay.serverSock[this.whoAmI].spamDelBlock += 1f;
						}
						if (b2 == 1 || b2 == 3)
						{
							Netplay.serverSock[this.whoAmI].spamAddBlock += 1f;
						}
					}
					if (!Netplay.serverSock[this.whoAmI].tileSection[Netplay.GetSectionX(num36), Netplay.GetSectionY(num37)])
					{
						flag4 = true;
					}
				}
				if (b2 == 0)
				{
					WorldGen.KillTile(num36, num37, flag4, false, false);
				}
				if (b2 == 1)
				{
					WorldGen.PlaceTile(num36, num37, (int)num38, false, true, -1, num39);
				}
				if (b2 == 2)
				{
					WorldGen.KillWall(num36, num37, flag4);
				}
				if (b2 == 3)
				{
					WorldGen.PlaceWall(num36, num37, (int)num38, false);
				}
				if (b2 == 4)
				{
					WorldGen.KillTile(num36, num37, flag4, false, true);
				}
				if (b2 == 5)
				{
					WorldGen.PlaceWire(num36, num37);
				}
				if (b2 == 6)
				{
					WorldGen.KillWire(num36, num37);
				}
				if (b2 == 7)
				{
					WorldGen.PoundTile(num36, num37);
				}
				if (b2 == 8)
				{
					WorldGen.PlaceActuator(num36, num37);
				}
				if (b2 == 9)
				{
					WorldGen.KillActuator(num36, num37);
				}
				if (b2 == 10)
				{
					WorldGen.PlaceWire2(num36, num37);
				}
				if (b2 == 11)
				{
					WorldGen.KillWire2(num36, num37);
				}
				if (b2 == 12)
				{
					WorldGen.PlaceWire3(num36, num37);
				}
				if (b2 == 13)
				{
					WorldGen.KillWire3(num36, num37);
				}
				if (b2 == 14)
				{
					WorldGen.SlopeTile(num36, num37, (int)num38);
				}
				if (b2 == 15)
				{
					Minecart.FrameTrack(num36, num37, true, false);
				}
				if (Main.netMode != 2)
				{
					return;
				}
				NetMessage.SendData(17, -1, this.whoAmI, "", (int)b2, (float)num36, (float)num37, (float)num38, num39);
				if (b2 == 1 && num38 == 53)
				{
					NetMessage.SendTileSquare(-1, num36, num37, 1);
					return;
				}
				return;
			}
			case 18:
				if (Main.netMode != 1)
				{
					return;
				}
				Main.dayTime = (this.reader.ReadByte() == 1);
				Main.time = (double)this.reader.ReadInt32();
				Main.sunModY = this.reader.ReadInt16();
				Main.moonModY = this.reader.ReadInt16();
				return;
			case 19:
			{
				byte b3 = this.reader.ReadByte();
				int num40 = (int)this.reader.ReadInt16();
				int num41 = (int)this.reader.ReadInt16();
				int num42 = (this.reader.ReadByte() == 0) ? -1 : 1;
				if (b3 == 0)
				{
					WorldGen.OpenDoor(num40, num41, num42);
				}
				else
				{
					if (b3 == 1)
					{
						WorldGen.CloseDoor(num40, num41, true);
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(19, -1, this.whoAmI, "", (int)b3, (float)num40, (float)num41, (float)((num42 == 1) ? 1 : 0), 0);
					return;
				}
				return;
			}
			case 20:
			{
				short num43 = this.reader.ReadInt16();
				int num44 = (int)this.reader.ReadInt16();
				int num45 = (int)this.reader.ReadInt16();
				BitsByte bitsByte6 = 0;
				BitsByte bitsByte7 = 0;
				for (int num46 = num44; num46 < num44 + (int)num43; num46++)
				{
					for (int num47 = num45; num47 < num45 + (int)num43; num47++)
					{
						if (Main.tile[num46, num47] == null)
						{
							Main.tile[num46, num47] = new Tile();
						}
						Tile tile = Main.tile[num46, num47];
						bool flag5 = tile.active();
						bitsByte6 = this.reader.ReadByte();
						bitsByte7 = this.reader.ReadByte();
						tile.active(bitsByte6[0]);
						tile.wall = (bitsByte6[2] ? 1 : 0);
						bool flag6 = bitsByte6[3];
						if (Main.netMode != 2)
						{
							tile.liquid = (flag6 ? 1 : 0);
						}
						tile.wire(bitsByte6[4]);
						tile.halfBrick(bitsByte6[5]);
						tile.actuator(bitsByte6[6]);
						tile.inActive(bitsByte6[7]);
						tile.wire2(bitsByte7[0]);
						tile.wire3(bitsByte7[1]);
						if (bitsByte7[2])
						{
							tile.color(this.reader.ReadByte());
						}
						if (bitsByte7[3])
						{
							tile.wallColor(this.reader.ReadByte());
						}
						if (tile.active())
						{
							int type2 = (int)tile.type;
							tile.type = this.reader.ReadUInt16();
							if (Main.tileFrameImportant[(int)tile.type])
							{
								tile.frameX = this.reader.ReadInt16();
								tile.frameY = this.reader.ReadInt16();
							}
							else
							{
								if (!flag5 || (int)tile.type != type2)
								{
									tile.frameX = -1;
									tile.frameY = -1;
								}
							}
							byte b4 = 0;
							if (bitsByte7[4])
							{
								b4 += 1;
							}
							if (bitsByte7[5])
							{
								b4 += 2;
							}
							if (bitsByte7[6])
							{
								b4 += 4;
							}
							tile.slope(b4);
						}
						if (tile.wall > 0)
						{
							tile.wall = this.reader.ReadByte();
						}
						if (flag6)
						{
							tile.liquid = this.reader.ReadByte();
							tile.liquidType((int)this.reader.ReadByte());
						}
					}
				}
				WorldGen.RangeFrame(num44, num45, num44 + (int)num43, num45 + (int)num43);
				if (Main.netMode == 2)
				{
					NetMessage.SendData((int)b, -1, this.whoAmI, "", (int)num43, (float)num44, (float)num45, 0f, 0);
					return;
				}
				return;
			}
			case 21:
			{
				int num48 = (int)this.reader.ReadInt16();
				Vector2 position = this.reader.ReadVector2();
				Vector2 velocity = this.reader.ReadVector2();
				int stack2 = (int)this.reader.ReadInt16();
				int pre = (int)this.reader.ReadByte();
				int num49 = (int)this.reader.ReadByte();
				int num50 = (int)this.reader.ReadInt16();
				if (Main.netMode == 1)
				{
					if (num50 == 0)
					{
						Main.item[num48].active = false;
						return;
					}
					Item item = Main.item[num48];
					item.netDefaults(num50);
					item.Prefix(pre);
					item.stack = stack2;
					item.position = position;
					item.velocity = velocity;
					item.active = true;
					item.wet = Collision.WetCollision(item.position, item.width, item.height);
					return;
				}
				else
				{
					if (num50 == 0)
					{
						if (num48 < 400)
						{
							Main.item[num48].active = false;
							NetMessage.SendData(21, -1, -1, "", num48, 0f, 0f, 0f, 0);
							return;
						}
						return;
					}
					else
					{
						bool flag7 = false;
						if (num48 == 400)
						{
							flag7 = true;
						}
						if (flag7)
						{
							Item item2 = new Item();
							item2.netDefaults(num50);
							num48 = Item.NewItem((int)position.X, (int)position.Y, item2.width, item2.height, item2.type, stack2, true, 0, false);
						}
						Item item3 = Main.item[num48];
						item3.netDefaults(num50);
						item3.Prefix(pre);
						item3.stack = stack2;
						item3.position = position;
						item3.velocity = velocity;
						item3.active = true;
						item3.owner = Main.myPlayer;
						if (flag7)
						{
							NetMessage.SendData(21, -1, -1, "", num48, 0f, 0f, 0f, 0);
							if (num49 == 0)
							{
								Main.item[num48].ownIgnore = this.whoAmI;
								Main.item[num48].ownTime = 100;
							}
							Main.item[num48].FindOwner(num48);
							return;
						}
						NetMessage.SendData(21, -1, this.whoAmI, "", num48, 0f, 0f, 0f, 0);
						return;
					}
				}
				break;
			}
			case 22:
			{
				int num51 = (int)this.reader.ReadInt16();
				int num52 = (int)this.reader.ReadByte();
				if (Main.netMode == 2 && Main.item[num51].owner != this.whoAmI)
				{
					return;
				}
				Main.item[num51].owner = num52;
				if (num52 == Main.myPlayer)
				{
					Main.item[num51].keepTime = 15;
				}
				else
				{
					Main.item[num51].keepTime = 0;
				}
				if (Main.netMode == 2)
				{
					Main.item[num51].owner = 255;
					Main.item[num51].keepTime = 15;
					NetMessage.SendData(22, -1, -1, "", num51, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 23:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num53 = (int)this.reader.ReadInt16();
				Vector2 position2 = this.reader.ReadVector2();
				Vector2 velocity2 = this.reader.ReadVector2();
				int target = (int)this.reader.ReadByte();
				BitsByte bitsByte8 = this.reader.ReadByte();
				float[] array = new float[NPC.maxAI];
				for (int num54 = 0; num54 < NPC.maxAI; num54++)
				{
					if (bitsByte8[num54 + 2])
					{
						array[num54] = this.reader.ReadSingle();
					}
					else
					{
						array[num54] = 0f;
					}
				}
				int num55 = (int)this.reader.ReadInt16();
				int num56 = 0;
				if (!bitsByte8[7])
				{
					if (Main.npcLifeBytes[num55] == 2)
					{
						num56 = (int)this.reader.ReadInt16();
					}
					else
					{
						if (Main.npcLifeBytes[num55] == 4)
						{
							num56 = this.reader.ReadInt32();
						}
						else
						{
							num56 = (int)this.reader.ReadSByte();
						}
					}
				}
				int num57 = -1;
				NPC nPC = Main.npc[num53];
				if (!nPC.active || nPC.netID != num55)
				{
					if (nPC.active)
					{
						num57 = nPC.type;
					}
					nPC.active = true;
					nPC.netDefaults(num55);
				}
				nPC.position = position2;
				nPC.velocity = velocity2;
				nPC.target = target;
				nPC.direction = (bitsByte8[0] ? 1 : -1);
				nPC.directionY = (bitsByte8[1] ? 1 : -1);
				nPC.spriteDirection = (bitsByte8[6] ? 1 : -1);
				if (bitsByte8[7])
				{
					num56 = (nPC.life = nPC.lifeMax);
				}
				else
				{
					nPC.life = num56;
				}
				if (num56 <= 0)
				{
					nPC.active = false;
				}
				for (int num58 = 0; num58 < NPC.maxAI; num58++)
				{
					nPC.ai[num58] = array[num58];
				}
				if (num57 > -1 && num57 != nPC.type)
				{
					nPC.xForm(num57, nPC.type);
				}
				if (num55 == 262)
				{
					NPC.plantBoss = num53;
				}
				if (num55 == 245)
				{
					NPC.golemBoss = num53;
				}
				if (Main.npcCatchable[nPC.type])
				{
					nPC.releaseOwner = (short)this.reader.ReadByte();
					return;
				}
				return;
			}
			case 24:
			{
				int num59 = (int)this.reader.ReadInt16();
				int num60 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num60 = this.whoAmI;
				}
				Player player7 = Main.player[num60];
				Main.npc[num59].StrikeNPC(player7.inventory[player7.selectedItem].damage, player7.inventory[player7.selectedItem].knockBack, player7.direction, false, false);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(24, -1, this.whoAmI, "", num59, (float)num60, 0f, 0f, 0);
					NetMessage.SendData(23, -1, -1, "", num59, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 25:
			{
				int num61 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num61 = this.whoAmI;
				}
				Color color = this.reader.ReadRGB();
				if (Main.netMode == 2)
				{
					color = new Color(255, 255, 255);
				}
				string text = this.reader.ReadString();
				if (Main.netMode == 1)
				{
					string newText = text;
					if (num61 < 255)
					{
						newText = "<" + Main.player[num61].name + "> " + text;
						Main.player[num61].chatText = text;
						Main.player[num61].chatShowTime = Main.chatLength / 2;
					}
					Main.NewText(newText, color.R, color.G, color.B, false);
					return;
				}
				if (Main.netMode != 2)
				{
					return;
				}
				string text2 = text.ToLower();
				if (text2 == Lang.mp[6] || text2 == Lang.mp[21])
				{
					string text3 = "";
					for (int num62 = 0; num62 < 255; num62++)
					{
						if (Main.player[num62].active)
						{
							if (text3 == "")
							{
								text3 = Main.player[num62].name;
							}
							else
							{
								text3 = text3 + ", " + Main.player[num62].name;
							}
						}
					}
					NetMessage.SendData(25, this.whoAmI, -1, Lang.mp[7] + " " + text3 + ".", 255, 255f, 240f, 20f, 0);
					return;
				}
				if (text2.StartsWith("/me "))
				{
					NetMessage.SendData(25, -1, -1, "*" + Main.player[this.whoAmI].name + " " + text.Substring(4), 255, 200f, 100f, 0f, 0);
					return;
				}
				if (text2 == Lang.mp[8])
				{
					NetMessage.SendData(25, -1, -1, string.Concat(new object[]
					{
						"*",
						Main.player[this.whoAmI].name,
						" ",
						Lang.mp[9],
						" ",
						Main.rand.Next(1, 101)
					}), 255, 255f, 240f, 20f, 0);
					return;
				}
				if (text2.StartsWith("/p "))
				{
					int team = Main.player[this.whoAmI].team;
					color = Main.teamColor[team];
					if (team != 0)
					{
						for (int num63 = 0; num63 < 255; num63++)
						{
							if (Main.player[num63].team == team)
							{
								NetMessage.SendData(25, num63, -1, text.Substring(3), num61, (float)color.R, (float)color.G, (float)color.B, 0);
							}
						}
						return;
					}
					NetMessage.SendData(25, this.whoAmI, -1, Lang.mp[10], 255, 255f, 240f, 20f, 0);
					return;
				}
				else
				{
					if (Main.player[this.whoAmI].difficulty == 2)
					{
						color = Main.hcColor;
					}
					else
					{
						if (Main.player[this.whoAmI].difficulty == 1)
						{
							color = Main.mcColor;
						}
					}
					NetMessage.SendData(25, -1, -1, text, num61, (float)color.R, (float)color.G, (float)color.B, 0);
					if (Main.dedServ)
					{
						Console.WriteLine("<" + Main.player[this.whoAmI].name + "> " + text);
						return;
					}
					return;
				}
				break;
			}
			case 26:
			{
				int num64 = (int)this.reader.ReadByte();
				if (Main.netMode == 2 && this.whoAmI != num64 && (!Main.player[num64].hostile || !Main.player[this.whoAmI].hostile))
				{
					return;
				}
				int num65 = (int)(this.reader.ReadByte() - 1);
				int num66 = (int)this.reader.ReadInt16();
				string text4 = this.reader.ReadString();
				BitsByte bitsByte9 = this.reader.ReadByte();
				bool flag8 = bitsByte9[0];
				bool flag9 = bitsByte9[1];
				Main.player[num64].Hurt(num66, num65, flag8, true, text4, flag9);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(26, -1, this.whoAmI, text4, num64, (float)num65, (float)num66, (float)(flag8 ? 1 : 0), flag9 ? 1 : 0);
					return;
				}
				return;
			}
			case 27:
			{
				int num67 = (int)this.reader.ReadInt16();
				Vector2 position3 = this.reader.ReadVector2();
				Vector2 velocity3 = this.reader.ReadVector2();
				float knockBack = this.reader.ReadSingle();
				int damage = (int)this.reader.ReadInt16();
				int num68 = (int)this.reader.ReadByte();
				int num69 = (int)this.reader.ReadInt16();
				BitsByte bitsByte10 = this.reader.ReadByte();
				float[] array2 = new float[Projectile.maxAI];
				for (int num70 = 0; num70 < Projectile.maxAI; num70++)
				{
					if (bitsByte10[num70])
					{
						array2[num70] = this.reader.ReadSingle();
					}
					else
					{
						array2[num70] = 0f;
					}
				}
				if (Main.netMode == 2)
				{
					num68 = this.whoAmI;
					if (Main.projHostile[num69])
					{
						return;
					}
				}
				int num71 = 1000;
				for (int num72 = 0; num72 < 1000; num72++)
				{
					if (Main.projectile[num72].owner == num68 && Main.projectile[num72].identity == num67 && Main.projectile[num72].active)
					{
						num71 = num72;
						break;
					}
				}
				if (num71 == 1000)
				{
					for (int num73 = 0; num73 < 1000; num73++)
					{
						if (!Main.projectile[num73].active)
						{
							num71 = num73;
							break;
						}
					}
				}
				Projectile projectile = Main.projectile[num71];
				if (!projectile.active || projectile.type != num69)
				{
					projectile.SetDefaults(num69);
					if (Main.netMode == 2)
					{
						Netplay.serverSock[this.whoAmI].spamProjectile += 1f;
					}
				}
				projectile.identity = num67;
				projectile.position = position3;
				projectile.velocity = velocity3;
				projectile.type = num69;
				projectile.damage = damage;
				projectile.knockBack = knockBack;
				projectile.owner = num68;
				for (int num74 = 0; num74 < Projectile.maxAI; num74++)
				{
					projectile.ai[num74] = array2[num74];
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(27, -1, this.whoAmI, "", num71, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 28:
			{
				int num75 = (int)this.reader.ReadInt16();
				int num76 = (int)this.reader.ReadInt16();
				float num77 = this.reader.ReadSingle();
				int num78 = (int)(this.reader.ReadByte() - 1);
				byte b5 = this.reader.ReadByte();
				if (num76 >= 0)
				{
					Main.npc[num75].StrikeNPC(num76, num77, num78, b5 == 1, false);
				}
				else
				{
					Main.npc[num75].life = 0;
					Main.npc[num75].HitEffect(0, 10.0);
					Main.npc[num75].active = false;
				}
				if (Main.netMode != 2)
				{
					return;
				}
				NetMessage.SendData(28, -1, this.whoAmI, "", num75, (float)num76, num77, (float)num78, (int)b5);
				if (Main.npc[num75].life <= 0)
				{
					NetMessage.SendData(23, -1, -1, "", num75, 0f, 0f, 0f, 0);
					return;
				}
				Main.npc[num75].netUpdate = true;
				return;
			}
			case 29:
			{
				int num79 = (int)this.reader.ReadInt16();
				int num80 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num80 = this.whoAmI;
				}
				for (int num81 = 0; num81 < 1000; num81++)
				{
					if (Main.projectile[num81].owner == num80 && Main.projectile[num81].identity == num79 && Main.projectile[num81].active)
					{
						Main.projectile[num81].Kill();
						break;
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(29, -1, this.whoAmI, "", num79, (float)num80, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 30:
			{
				int num82 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num82 = this.whoAmI;
				}
				bool flag10 = this.reader.ReadBoolean();
				Main.player[num82].hostile = flag10;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(30, -1, this.whoAmI, "", num82, 0f, 0f, 0f, 0);
					string str = " " + Lang.mp[flag10 ? 11 : 12];
					Color color2 = Main.teamColor[Main.player[num82].team];
					NetMessage.SendData(25, -1, -1, Main.player[num82].name + str, 255, (float)color2.R, (float)color2.G, (float)color2.B, 0);
					return;
				}
				return;
			}
			case 31:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int x = (int)this.reader.ReadInt16();
				int y = (int)this.reader.ReadInt16();
				int num83 = Chest.FindChest(x, y);
				if (num83 > -1 && Chest.UsingChest(num83) == -1)
				{
					for (int num84 = 0; num84 < Chest.maxItems; num84++)
					{
						NetMessage.SendData(32, this.whoAmI, -1, "", num83, (float)num84, 0f, 0f, 0);
					}
					NetMessage.SendData(33, this.whoAmI, -1, "", num83, 0f, 0f, 0f, 0);
					Main.player[this.whoAmI].chest = num83;
					return;
				}
				return;
			}
			case 32:
			{
				int num85 = (int)this.reader.ReadInt16();
				int num86 = (int)this.reader.ReadByte();
				int stack3 = (int)this.reader.ReadInt16();
				int pre2 = (int)this.reader.ReadByte();
				int type3 = (int)this.reader.ReadInt16();
				if (Main.chest[num85] == null)
				{
					Main.chest[num85] = new Chest(false);
				}
				if (Main.chest[num85].item[num86] == null)
				{
					Main.chest[num85].item[num86] = new Item();
				}
				Main.chest[num85].item[num86].netDefaults(type3);
				Main.chest[num85].item[num86].Prefix(pre2);
				Main.chest[num85].item[num86].stack = stack3;
				return;
			}
			case 33:
			{
				int num87 = (int)this.reader.ReadInt16();
				int chestX = (int)this.reader.ReadInt16();
				int chestY = (int)this.reader.ReadInt16();
				int num88 = (int)this.reader.ReadByte();
				string text5 = string.Empty;
				if (num88 != 0)
				{
					if (num88 <= 20)
					{
						text5 = this.reader.ReadString();
					}
					else
					{
						if (num88 != 255)
						{
							num88 = 0;
						}
					}
				}
				if (Main.netMode == 1)
				{
					Player player8 = Main.player[Main.myPlayer];
					if (player8.chest == -1)
					{
						Main.playerInventory = true;
						Main.PlaySound(10, -1, -1, 1);
					}
					else
					{
						if (player8.chest != num87 && num87 != -1)
						{
							Main.playerInventory = true;
							Main.PlaySound(12, -1, -1, 1);
						}
						else
						{
							if (player8.chest != -1 && num87 == -1)
							{
								Main.PlaySound(11, -1, -1, 1);
							}
						}
					}
					player8.chest = num87;
					player8.chestX = chestX;
					player8.chestY = chestY;
					return;
				}
				if (num88 != 0)
				{
					int chest = Main.player[this.whoAmI].chest;
					Chest chest2 = Main.chest[chest];
					chest2.name = text5;
					NetMessage.SendData(69, -1, this.whoAmI, text5, chest, (float)chest2.x, (float)chest2.y, 0f, 0);
				}
				Main.player[this.whoAmI].chest = num87;
				return;
			}
			case 34:
			{
				byte b6 = this.reader.ReadByte();
				int num89 = (int)this.reader.ReadInt16();
				int num90 = (int)this.reader.ReadInt16();
				int num91 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					if (b6 == 0)
					{
						int num92 = WorldGen.PlaceChest(num89, num90, 21, false, num91);
						if (num92 == -1)
						{
							NetMessage.SendData(34, this.whoAmI, -1, "", (int)b6, (float)num89, (float)num90, (float)num91, num92);
							Item.NewItem(num89 * 16, num90 * 16, 32, 32, Chest.itemSpawn[num91], 1, true, 0, false);
							return;
						}
						NetMessage.SendData(34, -1, -1, "", (int)b6, (float)num89, (float)num90, (float)num91, num92);
						return;
					}
					else
					{
						Tile tile2 = Main.tile[num89, num90];
						if (tile2.type != 21)
						{
							return;
						}
						if (tile2.frameX % 36 != 0)
						{
							num89--;
						}
						if (tile2.frameY % 36 != 0)
						{
							num90--;
						}
						int number = Chest.FindChest(num89, num90);
						WorldGen.KillTile(num89, num90, false, false, false);
						if (!tile2.active())
						{
							NetMessage.SendData(34, -1, -1, "", (int)b6, (float)num89, (float)num90, 0f, number);
							return;
						}
						return;
					}
				}
				else
				{
					int num93 = (int)this.reader.ReadInt16();
					if (b6 != 0)
					{
						Chest.DestroyChestDirect(num89, num90, num93);
						WorldGen.KillTile(num89, num90, false, false, false);
						return;
					}
					if (num93 == -1)
					{
						WorldGen.KillTile(num89, num90, false, false, false);
						return;
					}
					WorldGen.PlaceChestDirect(num89, num90, 21, num91, num93);
					return;
				}
				break;
			}
			case 35:
			{
				int num94 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num94 = this.whoAmI;
				}
				int num95 = (int)this.reader.ReadInt16();
				if (num94 != Main.myPlayer || Main.ServerSideCharacter)
				{
					Main.player[num94].HealEffect(num95, true);
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(35, -1, this.whoAmI, "", num94, (float)num95, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 36:
			{
				int num96 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num96 = this.whoAmI;
				}
				Player player9 = Main.player[num96];
				BitsByte bitsByte11 = this.reader.ReadByte();
				player9.zoneEvil = bitsByte11[0];
				player9.zoneMeteor = bitsByte11[1];
				player9.zoneDungeon = bitsByte11[2];
				player9.zoneJungle = bitsByte11[3];
				player9.zoneHoly = bitsByte11[4];
				player9.zoneSnow = bitsByte11[5];
				player9.zoneBlood = bitsByte11[6];
				player9.zoneCandle = bitsByte11[7];
				if (Main.netMode == 2)
				{
					NetMessage.SendData(36, -1, this.whoAmI, "", num96, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 37:
				if (Main.netMode != 1)
				{
					return;
				}
				if (Main.autoPass)
				{
					NetMessage.SendData(38, -1, -1, Netplay.password, 0, 0f, 0f, 0f, 0);
					Main.autoPass = false;
					return;
				}
				Netplay.password = "";
				Main.menuMode = 31;
				return;
			case 38:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				string a2 = this.reader.ReadString();
				if (a2 == Netplay.password)
				{
					Netplay.serverSock[this.whoAmI].state = 1;
					NetMessage.SendData(3, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
					return;
				}
				NetMessage.SendData(2, this.whoAmI, -1, Lang.mp[1], 0, 0f, 0f, 0f, 0);
				return;
			}
			case 39:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num97 = (int)this.reader.ReadInt16();
				Main.item[num97].owner = 255;
				NetMessage.SendData(22, -1, -1, "", num97, 0f, 0f, 0f, 0);
				return;
			}
			case 40:
			{
				int num98 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num98 = this.whoAmI;
				}
				int talkNPC = (int)this.reader.ReadInt16();
				Main.player[num98].talkNPC = talkNPC;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(40, -1, this.whoAmI, "", num98, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 41:
			{
				int num99 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num99 = this.whoAmI;
				}
				Player player10 = Main.player[num99];
				float itemRotation = this.reader.ReadSingle();
				int itemAnimation = (int)this.reader.ReadInt16();
				player10.itemRotation = itemRotation;
				player10.itemAnimation = itemAnimation;
				player10.channel = player10.inventory[player10.selectedItem].channel;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(41, -1, this.whoAmI, "", num99, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 42:
			{
				int num100 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num100 = this.whoAmI;
				}
				else
				{
					if (Main.myPlayer == num100 && !Main.ServerSideCharacter)
					{
						return;
					}
				}
				int statMana = (int)this.reader.ReadInt16();
				int statManaMax = (int)this.reader.ReadInt16();
				Main.player[num100].statMana = statMana;
				Main.player[num100].statManaMax = statManaMax;
				return;
			}
			case 43:
			{
				int num101 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num101 = this.whoAmI;
				}
				int num102 = (int)this.reader.ReadInt16();
				if (num101 != Main.myPlayer)
				{
					Main.player[num101].ManaEffect(num102);
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(43, -1, this.whoAmI, "", num101, (float)num102, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 44:
			{
				int num103 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num103 = this.whoAmI;
				}
				int num104 = (int)(this.reader.ReadByte() - 1);
				int num105 = (int)this.reader.ReadInt16();
				byte b7 = this.reader.ReadByte();
				string text6 = this.reader.ReadString();
				Main.player[num103].KillMe((double)num105, num104, b7 == 1, text6);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(44, -1, this.whoAmI, text6, num103, (float)num104, (float)num105, (float)b7, 0);
					return;
				}
				return;
			}
			case 45:
			{
				int num106 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num106 = this.whoAmI;
				}
				int num107 = (int)this.reader.ReadByte();
				Player player11 = Main.player[num106];
				int team2 = player11.team;
				player11.team = num107;
				Color color3 = Main.teamColor[num107];
				if (Main.netMode == 2)
				{
					NetMessage.SendData(45, -1, this.whoAmI, "", num106, 0f, 0f, 0f, 0);
					string str2 = " " + Lang.mp[13 + num107];
					for (int num108 = 0; num108 < 255; num108++)
					{
						if (num108 == this.whoAmI || (team2 > 0 && Main.player[num108].team == team2) || (num107 > 0 && Main.player[num108].team == num107))
						{
							NetMessage.SendData(25, num108, -1, player11.name + str2, 255, (float)color3.R, (float)color3.G, (float)color3.B, 0);
						}
					}
					return;
				}
				return;
			}
			case 46:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int i2 = (int)this.reader.ReadInt16();
				int j2 = (int)this.reader.ReadInt16();
				int num109 = Sign.ReadSign(i2, j2);
				if (num109 >= 0)
				{
					NetMessage.SendData(47, this.whoAmI, -1, "", num109, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 47:
			{
				int num110 = (int)this.reader.ReadInt16();
				int x2 = (int)this.reader.ReadInt16();
				int y2 = (int)this.reader.ReadInt16();
				string text7 = this.reader.ReadString();
				Main.sign[num110] = new Sign();
				Main.sign[num110].x = x2;
				Main.sign[num110].y = y2;
				Sign.TextSign(num110, text7);
				if (Main.netMode == 1 && Main.sign[num110] != null)
				{
					Main.playerInventory = false;
					Main.player[Main.myPlayer].talkNPC = -1;
					Main.npcChatCornerItem = 0;
					Main.editSign = false;
					Main.PlaySound(10, -1, -1, 1);
					Main.player[Main.myPlayer].sign = num110;
					Main.npcChatText = Main.sign[num110].text;
					return;
				}
				return;
			}
			case 48:
			{
				int num111 = (int)this.reader.ReadInt16();
				int num112 = (int)this.reader.ReadInt16();
				byte liquid = this.reader.ReadByte();
				byte liquidType = this.reader.ReadByte();
				if (Main.netMode == 2 && Netplay.spamCheck)
				{
					int num113 = this.whoAmI;
					int num114 = (int)(Main.player[num113].position.X + (float)(Main.player[num113].width / 2));
					int num115 = (int)(Main.player[num113].position.Y + (float)(Main.player[num113].height / 2));
					int num116 = 10;
					int num117 = num114 - num116;
					int num118 = num114 + num116;
					int num119 = num115 - num116;
					int num120 = num115 + num116;
					if (num111 < num117 || num111 > num118 || num112 < num119 || num112 > num120)
					{
						NetMessage.BootPlayer(this.whoAmI, "Cheating attempt detected: Liquid spam");
						return;
					}
				}
				if (Main.tile[num111, num112] == null)
				{
					Main.tile[num111, num112] = new Tile();
				}
				lock (Main.tile[num111, num112])
				{
					Main.tile[num111, num112].liquid = liquid;
					Main.tile[num111, num112].liquidType((int)liquidType);
					if (Main.netMode == 2)
					{
						WorldGen.SquareTileFrame(num111, num112, true);
					}
					return;
				}
				goto IL_3A0C;
			}
			case 49:
				goto IL_3A0C;
			case 50:
			{
				int num121 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num121 = this.whoAmI;
				}
				else
				{
					if (num121 == Main.myPlayer && !Main.ServerSideCharacter)
					{
						return;
					}
				}
				Player player12 = Main.player[num121];
				for (int num122 = 0; num122 < 22; num122++)
				{
					player12.buffType[num122] = (int)this.reader.ReadByte();
					if (player12.buffType[num122] > 0)
					{
						player12.buffTime[num122] = 60;
					}
					else
					{
						player12.buffTime[num122] = 0;
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(50, -1, this.whoAmI, "", num121, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 51:
			{
				byte b8 = this.reader.ReadByte();
				byte b9 = this.reader.ReadByte();
				if (b9 == 1)
				{
					NPC.SpawnSkeletron();
					return;
				}
				if (b9 != 2)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(51, -1, this.whoAmI, "", (int)b8, (float)b9, 0f, 0f, 0);
					return;
				}
				Main.PlaySound(2, (int)Main.player[(int)b8].position.X, (int)Main.player[(int)b8].position.Y, 1);
				return;
			}
			case 52:
			{
				int number2 = (int)this.reader.ReadByte();
				int num123 = (int)this.reader.ReadByte();
				int num124 = (int)this.reader.ReadInt16();
				int num125 = (int)this.reader.ReadInt16();
				if (num123 == 1)
				{
					Chest.Unlock(num124, num125);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(52, -1, this.whoAmI, "", number2, (float)num123, (float)num124, (float)num125, 0);
						NetMessage.SendTileSquare(-1, num124, num125, 2);
					}
				}
				if (num123 != 2)
				{
					return;
				}
				WorldGen.UnlockDoor(num124, num125);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(52, -1, this.whoAmI, "", number2, (float)num123, (float)num124, (float)num125, 0);
					NetMessage.SendTileSquare(-1, num124, num125, 2);
					return;
				}
				return;
			}
			case 53:
			{
				int num126 = (int)this.reader.ReadInt16();
				int type4 = (int)this.reader.ReadByte();
				int time = (int)this.reader.ReadInt16();
				Main.npc[num126].AddBuff(type4, time, true);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(54, -1, -1, "", num126, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 54:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num127 = (int)this.reader.ReadInt16();
				NPC nPC2 = Main.npc[num127];
				for (int num128 = 0; num128 < 5; num128++)
				{
					nPC2.buffType[num128] = (int)this.reader.ReadByte();
					nPC2.buffTime[num128] = (int)this.reader.ReadInt16();
				}
				return;
			}
			case 55:
			{
				int num129 = (int)this.reader.ReadByte();
				int num130 = (int)this.reader.ReadByte();
				int num131 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2 && num129 != this.whoAmI && !Main.pvpBuff[num130])
				{
					return;
				}
				if (Main.netMode == 1 && num129 == Main.myPlayer)
				{
					Main.player[num129].AddBuff(num130, num131, true);
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(55, num129, -1, "", num129, (float)num130, (float)num131, 0f, 0);
					return;
				}
				return;
			}
			case 56:
			{
				int num132 = (int)this.reader.ReadInt16();
				if (num132 < 0 || num132 >= 200)
				{
					return;
				}
				if (Main.netMode == 1)
				{
					Main.npc[num132].displayName = this.reader.ReadString();
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(56, this.whoAmI, -1, Main.npc[num132].displayName, num132, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 57:
				if (Main.netMode != 1)
				{
					return;
				}
				WorldGen.tGood = this.reader.ReadByte();
				WorldGen.tEvil = this.reader.ReadByte();
				WorldGen.tBlood = this.reader.ReadByte();
				return;
			case 58:
			{
				int num133 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num133 = this.whoAmI;
				}
				float num134 = this.reader.ReadSingle();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(58, -1, this.whoAmI, "", this.whoAmI, num134, 0f, 0f, 0);
					return;
				}
				Player player13 = Main.player[num133];
				Main.harpNote = num134;
				int style = 26;
				if (player13.inventory[player13.selectedItem].type == 507)
				{
					style = 35;
				}
				Main.PlaySound(2, (int)player13.position.X, (int)player13.position.Y, style);
				return;
			}
			case 59:
			{
				int num135 = (int)this.reader.ReadInt16();
				int num136 = (int)this.reader.ReadInt16();
				Wiring.hitSwitch(num135, num136);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(59, -1, this.whoAmI, "", num135, (float)num136, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 60:
			{
				int num137 = (int)this.reader.ReadInt16();
				int num138 = (int)this.reader.ReadInt16();
				int num139 = (int)this.reader.ReadInt16();
				byte b10 = this.reader.ReadByte();
				if (num137 >= 200)
				{
					NetMessage.BootPlayer(this.whoAmI, "cheating attempt detected: Invalid kick-out");
					return;
				}
				if (Main.netMode == 1)
				{
					Main.npc[num137].homeless = (b10 == 1);
					Main.npc[num137].homeTileX = num138;
					Main.npc[num137].homeTileY = num139;
					return;
				}
				if (b10 == 0)
				{
					WorldGen.kickOut(num137);
					return;
				}
				WorldGen.moveRoom(num138, num139, num137);
				return;
			}
			case 61:
			{
				int plr = this.reader.ReadInt32();
				int num140 = this.reader.ReadInt32();
				if (Main.netMode != 2)
				{
					return;
				}
				if (num140 == 4 || num140 == 13 || num140 == 50 || num140 == 125 || num140 == 126 || num140 == 134 || num140 == 127 || num140 == 128 || num140 == 222 || num140 == 245 || num140 == 266 || num140 == 370)
				{
					bool flag12 = !NPC.AnyNPCs(num140);
					if (flag12)
					{
						NPC.SpawnOnPlayer(plr, num140);
						return;
					}
					return;
				}
				else
				{
					if (num140 == -4)
					{
						if (!Main.dayTime)
						{
							NetMessage.SendData(25, -1, -1, Lang.misc[31], 255, 50f, 255f, 130f, 0);
							Main.startPumpkinMoon();
							NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
							return;
						}
						return;
					}
					else
					{
						if (num140 == -5)
						{
							if (!Main.dayTime)
							{
								NetMessage.SendData(25, -1, -1, Lang.misc[34], 255, 50f, 255f, 130f, 0);
								Main.startSnowMoon();
								NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
								return;
							}
							return;
						}
						else
						{
							if (num140 >= 0)
							{
								return;
							}
							int num141 = 1;
							if (num140 > -4)
							{
								num141 = -num140;
							}
							if (num141 > 0 && Main.invasionType == 0)
							{
								Main.invasionDelay = 0;
								Main.StartInvasion(num141);
								return;
							}
							return;
						}
					}
				}
				break;
			}
			case 62:
			{
				int num142 = (int)this.reader.ReadByte();
				int num143 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num142 = this.whoAmI;
				}
				if (num143 == 1)
				{
					Main.player[num142].NinjaDodge();
				}
				if (num143 == 2)
				{
					Main.player[num142].ShadowDodge();
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(62, -1, this.whoAmI, "", num142, (float)num143, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 63:
			{
				int num144 = (int)this.reader.ReadInt16();
				int num145 = (int)this.reader.ReadInt16();
				byte b11 = this.reader.ReadByte();
				WorldGen.paintTile(num144, num145, b11, false);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(63, -1, this.whoAmI, "", num144, (float)num145, (float)b11, 0f, 0);
					return;
				}
				return;
			}
			case 64:
			{
				int num146 = (int)this.reader.ReadInt16();
				int num147 = (int)this.reader.ReadInt16();
				byte b12 = this.reader.ReadByte();
				WorldGen.paintWall(num146, num147, b12, false);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(64, -1, this.whoAmI, "", num146, (float)num147, (float)b12, 0f, 0);
					return;
				}
				return;
			}
			case 65:
			{
				BitsByte bitsByte12 = this.reader.ReadByte();
				int num148 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					num148 = this.whoAmI;
				}
				Vector2 newPos = this.reader.ReadVector2();
				int num149 = 0;
				int num150 = 0;
				if (bitsByte12[0])
				{
					num149++;
				}
				if (bitsByte12[1])
				{
					num149 += 2;
				}
				if (bitsByte12[2])
				{
					num150++;
				}
				if (bitsByte12[3])
				{
					num150++;
				}
				if (num149 == 0)
				{
					Main.player[num148].Teleport(newPos, num150);
				}
				else
				{
					if (num149 == 1)
					{
						Main.npc[num148].Teleport(newPos, num150);
					}
				}
				if (Main.netMode == 2 && num149 == 0)
				{
					NetMessage.SendData(65, -1, this.whoAmI, "", 0, (float)num148, newPos.X, newPos.Y, num150);
					return;
				}
				return;
			}
			case 66:
			{
				int num151 = (int)this.reader.ReadByte();
				int num152 = (int)this.reader.ReadInt16();
				if (num152 <= 0)
				{
					return;
				}
				Player player14 = Main.player[num151];
				player14.statLife += num152;
				if (player14.statLife > player14.statLifeMax2)
				{
					player14.statLife = player14.statLifeMax2;
				}
				player14.HealEffect(num152, false);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(66, -1, this.whoAmI, "", num151, (float)num152, 0f, 0f, 0);
					return;
				}
				return;
			}
			case 68:
				this.reader.ReadString();
				return;
			case 69:
			{
				int num153 = (int)this.reader.ReadInt16();
				int num154 = (int)this.reader.ReadInt16();
				int num155 = (int)this.reader.ReadInt16();
				if (Main.netMode == 1)
				{
					if (num153 < 0 || num153 >= 1000)
					{
						return;
					}
					Chest chest3 = Main.chest[num153];
					if (chest3 == null)
					{
						chest3 = new Chest(false);
						chest3.x = num154;
						chest3.y = num155;
						Main.chest[num153] = chest3;
					}
					else
					{
						if (chest3.x != num154 || chest3.y != num155)
						{
							return;
						}
					}
					chest3.name = this.reader.ReadString();
					return;
				}
				else
				{
					if (num153 < -1 || num153 >= 1000)
					{
						return;
					}
					if (num153 == -1)
					{
						num153 = Chest.FindChest(num154, num155);
						if (num153 == -1)
						{
							return;
						}
					}
					Chest chest4 = Main.chest[num153];
					if (chest4.x != num154 || chest4.y != num155)
					{
						return;
					}
					NetMessage.SendData(69, this.whoAmI, -1, chest4.name, num153, (float)num154, (float)num155, 0f, 0);
					return;
				}
				break;
			}
			case 70:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int i3 = (int)this.reader.ReadInt16();
				int who = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					who = this.whoAmI;
				}
				NPC.CatchNPC(i3, who);
				return;
			}
			case 71:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int x3 = this.reader.ReadInt32();
				int y3 = this.reader.ReadInt32();
				int type5 = (int)this.reader.ReadInt16();
				byte style2 = this.reader.ReadByte();
				NPC.ReleaseNPC(x3, y3, type5, (int)style2, this.whoAmI);
				return;
			}
			case 72:
				if (Main.netMode != 1)
				{
					return;
				}
				for (int num156 = 0; num156 < Chest.maxItems; num156++)
				{
					Main.travelShop[num156] = (int)this.reader.ReadInt16();
				}
				return;
			case 73:
				Main.player[this.whoAmI].TeleportationPotion();
				return;
			case 74:
				if (Main.netMode != 1)
				{
					return;
				}
				Main.anglerQuest = (int)this.reader.ReadByte();
				Main.anglerQuestFinished = this.reader.ReadBoolean();
				return;
			case 75:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				string name = Main.player[this.whoAmI].name;
				if (!Main.anglerWhoFinishedToday.Contains(name))
				{
					Main.anglerWhoFinishedToday.Add(name);
					return;
				}
				return;
			}
			case 76:
			{
				int num157 = (int)this.reader.ReadByte();
				if (num157 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num157 = this.whoAmI;
				}
				Player player15 = Main.player[num157];
				player15.anglerQuestsFinished = this.reader.ReadInt32();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(76, -1, this.whoAmI, "", num157, 0f, 0f, 0f, 0);
					return;
				}
				return;
			}
			default:
				return;
			}
			if (Main.netMode != 2)
			{
				return;
			}
			if (Netplay.serverSock[this.whoAmI].state == 1)
			{
				Netplay.serverSock[this.whoAmI].state = 2;
			}
			NetMessage.SendData(7, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
			return;
			IL_3A0C:
			if (Netplay.clientSock.state == 6)
			{
				Netplay.clientSock.state = 10;
				Main.player[Main.myPlayer].Spawn();
				return;
			}
		}
	}
}
