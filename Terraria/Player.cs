using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Terraria
{
	public class Player
	{
		public const int maxBuffs = 22;
		public const int defaultWidth = 20;
		public const int defaultHeight = 42;
		public int beetleOrbs;
		public float beetleCounter;
		public int beetleCountdown;
		public bool beetleDefense;
		public bool beetleOffense;
		public bool beetleBuff;
		public bool manaMagnet;
		public bool lifeMagnet;
		public bool lifeForce;
		public bool calmed;
		public bool inferno;
		public float flameRingRot;
		public float flameRingScale = 1f;
		public byte flameRingFrame;
		public byte flameRingAlpha;
		public int netManaTime;
		public int netLifeTime;
		public bool netMana;
		public bool netLife;
		public Vector2[] beetlePos = new Vector2[3];
		public Vector2[] beetleVel = new Vector2[3];
		public int beetleFrame;
		public int beetleFrameCounter;
		public static int manaSickTime = 300;
		public static int manaSickTimeMax = 600;
		public static float manaSickLessDmg = 0.25f;
		public float manaSickReduction;
		public bool manaSick;
		public bool stairFall;
		public int loadStatus;
		public Vector2[] itemFlamePos = new Vector2[7];
		public int itemFlameCount;
		public bool outOfRange;
		public float lifeSteal = 99999f;
		public float ghostDmg;
		public bool teleporting;
		public float teleportTime;
		public int teleportStyle;
		public bool sloping;
		public bool chilled;
		public bool frozen;
		public bool ichor;
		public int ropeCount;
		public int manaRegenBonus;
		public int manaRegenDelayBonus;
		public int dash;
		public int dashTime;
		public int dashDelay;
		public float accRunSpeed;
		public int gem = -1;
		public int gemCount;
		public byte meleeEnchant;
		public byte pulleyDir;
		public bool pulley;
		public int pulleyFrame;
		public float pulleyFrameCounter;
		public bool blackBelt;
		public bool sliding;
		public int slideDir;
		public int launcherWait;
		public bool iceSkate;
		public bool carpet;
		public int spikedBoots;
		public int carpetFrame = -1;
		public float carpetFrameCounter;
		public bool canCarpet;
		public int carpetTime;
		public int miscCounter;
		public int infernoCounter;
		public bool sandStorm;
		public bool crimsonRegen;
		public bool ghostHeal;
		public bool ghostHurt;
		public bool sticky;
		public bool slippy;
		public bool slippy2;
		public bool powerrun;
		public bool flapSound;
		public bool iceBarrier;
		public bool dangerSense;
		public float endurance;
		public bool loveStruck;
		public bool stinky;
		public bool resistCold;
		public bool panic;
		public byte iceBarrierFrame;
		public byte iceBarrierFrameCounter;
		public bool shadowDodge;
		public float shadowDodgeCount;
		public bool palladiumRegen;
		public bool onHitDodge;
		public bool onHitRegen;
		public bool onHitPetal;
		public int petalTimer;
		public int shadowDodgeTimer;
		public int fishingSkill;
		public bool cratePotion;
		public bool sonarPotion;
		public bool accFishingLine;
		public bool accTackleBox;
		public int maxMinions = 1;
		public int numMinions;
		public float slotsMinions;
		public bool pygmy;
		public bool raven;
		public bool slime;
		public bool hornetMinion;
		public bool impMinion;
		public bool twinsMinion;
		public bool spiderMinion;
		public bool pirateMinion;
		public bool sharknadoMinion;
		public float wingTime;
		public int wings;
		public int wingsLogic;
		public int wingTimeMax;
		public int wingFrame;
		public int wingFrameCounter;
		public bool male = true;
		public bool ghost;
		public int ghostFrame;
		public int ghostFrameCounter;
		public int miscTimer;
		public bool pvpDeath;
		public bool zoneDungeon;
		public bool zoneEvil;
		public bool zoneHoly;
		public bool zoneMeteor;
		public bool zoneJungle;
		public bool zoneSnow;
		public bool zoneBlood;
		public bool zoneCandle;
		public bool boneArmor;
		public bool frostArmor;
		public bool honey;
		public bool crystalLeaf;
		public bool paladinBuff;
		public bool paladinGive;
		public float townNPCs;
		public Vector2 position;
		public Vector2 oldPosition;
		public Vector2 velocity;
		public Vector2 oldVelocity;
		public double headFrameCounter;
		public double bodyFrameCounter;
		public double legFrameCounter;
		public int netSkip;
		public int oldSelectItem;
		public bool immune;
		public int immuneTime;
		public int immuneAlphaDirection;
		public int immuneAlpha;
		public int team;
		public bool hbLocked;
		public static int nameLen = 20;
		private float maxRegenDelay;
		public string chatText = "";
		public int sign = -1;
		public bool editedChestName;
		public int chatShowTime;
		public int reuseDelay;
		public int aggro;
		public float activeNPCs;
		public bool mouseInterface;
		public bool lastMouseInterface;
		public int noThrow;
		public int changeItem = -1;
		public int selectedItem;
		public Item[] armor = new Item[16];
		public Item[] dye = new Item[8];
		public int itemAnimation;
		public int itemAnimationMax;
		public int itemTime;
		public int toolTime;
		public float itemRotation;
		public int itemWidth;
		public int itemHeight;
		public Vector2 itemLocation;
		public bool poundRelease;
		public float ghostFade;
		public float ghostDir = 1f;
		public int[] buffType = new int[22];
		public int[] buffTime = new int[22];
		public bool[] buffImmune = new bool[140];
		public int heldProj = -1;
		public int breathCD;
		public int breathMax = 200;
		public int breath = 200;
		public int lavaCD;
		public int lavaMax;
		public int lavaTime;
		public bool ignoreWater;
		public bool socialShadow;
		public bool socialGhost;
		public bool armorSteath;
		public int stealthTimer;
		public float stealth = 1f;
		public string setBonus = "";
		public Item[] inventory = new Item[59];
		public Chest bank = new Chest(true);
		public Chest bank2 = new Chest(true);
		public float headRotation;
		public float bodyRotation;
		public float legRotation;
		public Vector2 headPosition;
		public Vector2 bodyPosition;
		public Vector2 legPosition;
		public Vector2 headVelocity;
		public Vector2 bodyVelocity;
		public Vector2 legVelocity;
		public float fullRotation;
		public Vector2 fullRotationOrigin = Vector2.Zero;
		public int nonTorch = -1;
		public float gfxOffY;
		public float stepSpeed = 1f;
		public static bool deadForGood = false;
		public bool dead;
		public int respawnTimer;
		public string name = "";
		public int attackCD;
		public int potionDelay;
		public byte difficulty;
		public bool wet;
		public byte wetCount;
		public bool lavaWet;
		public bool honeyWet;
		public byte wetSlime;
		public HitTile hitTile;
		public int jump;
		public int head = -1;
		public int body = -1;
		public int legs = -1;
		public sbyte handon = -1;
		public sbyte handoff = -1;
		public sbyte back = -1;
		public sbyte front = -1;
		public sbyte shoe = -1;
		public sbyte waist = -1;
		public sbyte shield = -1;
		public sbyte neck = -1;
		public sbyte face = -1;
		public sbyte balloon = -1;
		public BitsByte hideVisual = 0;
		public Rectangle headFrame;
		public Rectangle bodyFrame;
		public Rectangle legFrame;
		public Rectangle hairFrame;
		public bool controlLeft;
		public bool controlRight;
		public bool controlUp;
		public bool controlDown;
		public bool controlJump;
		public bool controlUseItem;
		public bool controlUseTile;
		public bool controlThrow;
		public bool controlInv;
		public bool controlHook;
		public bool controlTorch;
		public bool controlMap;
		public bool controlSmart;
		public bool releaseJump;
		public bool releaseUp;
		public bool releaseUseItem;
		public bool releaseUseTile;
		public bool releaseInventory;
		public bool releaseHook;
		public bool releaseThrow;
		public bool releaseQuickMana;
		public bool releaseQuickHeal;
		public bool releaseLeft;
		public bool releaseRight;
		public bool releaseSmart;
		public bool mapZoomIn;
		public bool mapZoomOut;
		public bool mapAlphaUp;
		public bool mapAlphaDown;
		public bool mapFullScreen;
		public bool mapStyle;
		public bool releaseMapFullscreen;
		public bool releaseMapStyle;
		public int leftTimer;
		public int rightTimer;
		public bool delayUseItem;
		public bool active;
		public int width = 20;
		public int height = 42;
		public int direction = 1;
		public bool showItemIcon;
		public bool showItemIconR;
		public int showItemIcon2;
		public string showItemIconText = "";
		public int whoAmi;
		public int runSoundDelay;
		public float shadow;
		public Vector2[] shadowPos = new Vector2[3];
		public float[] shadowRotation = new float[3];
		public Vector2[] shadowOrigin = new Vector2[3];
		public int shadowCount;
		public float manaCost = 1f;
		public bool fireWalk;
		public bool channel;
		public int step = -1;
		public int anglerQuestsFinished;
		public int statDefense;
		public int statLifeMax = 100;
		public int statLifeMax2 = 100;
		public int statLife = 100;
		public int statMana;
		public int statManaMax;
		public int statManaMax2;
		public int lifeRegen;
		public int lifeRegenCount;
		public int lifeRegenTime;
		public int manaRegen;
		public int manaRegenCount;
		public int manaRegenDelay;
		public bool manaRegenBuff;
		public bool noKnockback;
		public bool spaceGun;
		public float gravDir = 1f;
		public bool ammoCost80;
		public bool ammoCost75;
		public int stickyBreak;
		public bool magicQuiver;
		public bool magmaStone;
		public bool lavaRose;
		public bool ammoBox;
		public bool ammoPotion;
		public bool chaosState;
		public bool lightOrb;
		public bool blueFairy;
		public bool redFairy;
		public bool greenFairy;
		public bool bunny;
		public bool turtle;
		public bool eater;
		public bool penguin;
		public bool puppy;
		public bool grinch;
		public bool miniMinotaur;
		public bool wearsRobe;
		public bool minecartLeft;
		public bool onWrongGround;
		public bool onTrack;
		public int cartRampTime;
		public bool cartFlip;
		public float trackBoost;
		public Vector2 lastBoost = Vector2.Zero;
		public Mount mount;
		public bool blackCat;
		public bool spider;
		public bool squashling;
		public bool magicCuffs;
		public bool coldDash;
		public bool eyeSpring;
		public bool snowman;
		public bool scope;
		public bool dino;
		public bool skeletron;
		public bool hornet;
		public bool zephyrfish;
		public bool tiki;
		public bool parrot;
		public bool truffle;
		public bool sapling;
		public bool cSapling;
		public bool wisp;
		public bool lizard;
		public bool archery;
		public bool poisoned;
		public bool venom;
		public bool blind;
		public bool blackout;
		public bool frostBurn;
		public bool onFrostBurn;
		public bool burned;
		public bool suffocating;
		public byte suffocateDelay;
		public bool dripping;
		public bool drippingSlime;
		public bool onFire;
		public bool onFire2;
		public bool noItems;
		public bool wereWolf;
		public bool wolfAcc;
		public bool rulerAcc;
		public bool bleed;
		public bool confused;
		public bool accMerman;
		public bool merman;
		public bool brokenArmor;
		public bool silence;
		public bool slow;
		public bool gross;
		public bool tongued;
		public bool kbGlove;
		public bool kbBuff;
		public bool starCloak;
		public bool longInvince;
		public bool pStone;
		public bool manaFlower;
		public int meleeCrit = 4;
		public int rangedCrit = 4;
		public int magicCrit = 4;
		public float meleeDamage = 1f;
		public float rangedDamage = 1f;
		public float bulletDamage = 1f;
		public float arrowDamage = 1f;
		public float rocketDamage = 1f;
		public float magicDamage = 1f;
		public float minionDamage = 1f;
		public float minionKB;
		public float meleeSpeed = 1f;
		public float moveSpeed = 1f;
		public float pickSpeed = 1f;
		public float wallSpeed = 1f;
		public float tileSpeed = 1f;
		public bool autoPaint;
		public int SpawnX = -1;
		public int SpawnY = -1;
		public int[] spX = new int[200];
		public int[] spY = new int[200];
		public string[] spN = new string[200];
		public int[] spI = new int[200];
		public static int tileRangeX = 5;
		public static int tileRangeY = 4;
		public static int tileTargetX;
		public static int tileTargetY;
		public static float defaultGravity = 0.4f;
		private static int jumpHeight = 15;
		private static float jumpSpeed = 5.01f;
		public float gravity = Player.defaultGravity;
		public float maxFallSpeed = 10f;
		public float maxRunSpeed = 3f;
		public float runAcceleration = 0.08f;
		public float runSlowdown = 0.2f;
		public bool adjWater;
		public bool adjHoney;
		public bool adjLava;
		public bool oldAdjWater;
		public bool oldAdjHoney;
		public bool oldAdjLava;
		public bool[] adjTile = new bool[340];
		public bool[] oldAdjTile = new bool[340];
		private static int defaultItemGrabRange = 38;
		private static float itemGrabSpeed = 0.45f;
		private static float itemGrabSpeedMax = 4f;
		public byte hairDye;
		public Color hairDyeColor = Color.Transparent;
		public float hairDyeVar;
		public Color hairColor = new Color(215, 90, 55);
		public Color skinColor = new Color(255, 125, 90);
		public Color eyeColor = new Color(105, 90, 75);
		public Color shirtColor = new Color(175, 165, 140);
		public Color underShirtColor = new Color(160, 180, 215);
		public Color pantsColor = new Color(255, 230, 175);
		public Color shoeColor = new Color(160, 105, 60);
		public int hair;
		public bool hostile;
		public int accCompass;
		public int accWatch;
		public int accDepthMeter;
		public bool discount;
		public bool coins;
		public bool accDivingHelm;
		public bool accFlipper;
		public bool doubleJump;
		public bool jumpAgain;
		public bool dJumpEffect;
		public bool doubleJump2;
		public bool jumpAgain2;
		public bool dJumpEffect2;
		public bool doubleJump3;
		public bool jumpAgain3;
		public bool dJumpEffect3;
		public bool autoJump;
		public bool justJumped;
		public float jumpSpeedBoost;
		public int extraFall;
		public bool doubleJump4;
		public bool jumpAgain4;
		public bool dJumpEffect4;
		public bool spawnMax;
		public int blockRange;
		public int[] grappling = new int[20];
		public int grapCount;
		public int rocketTime;
		public int rocketTimeMax = 7;
		public int rocketDelay;
		public int rocketDelay2;
		public bool rocketRelease;
		public bool rocketFrame;
		public int rocketBoots;
		public bool canRocket;
		public bool jumpBoost;
		public bool noFallDmg;
		public int swimTime;
		public bool killGuide;
		public bool killClothier;
		public bool lavaImmune;
		public bool gills;
		public bool slowFall;
		public bool findTreasure;
		public bool invis;
		public bool detectCreature;
		public bool nightVision;
		public bool enemySpawns;
		public bool thorns;
		public bool turtleArmor;
		public bool turtleThorns;
		public bool spiderArmor;
		public bool waterWalk;
		public bool waterWalk2;
		public bool gravControl;
		public bool gravControl2;
		public bool bee;
		public int chest = -1;
		public int chestX;
		public int chestY;
		public int talkNPC = -1;
		public int fallStart;
		public int fallStart2;
		public int slowCount;
		public int potionDelayTime = Item.potionDelay;
		public static bool lastPound = true;
		public void HealEffect(int healAmount, bool broadcast = true)
		{
			CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(100, 255, 100, 255), string.Concat(healAmount), false, false);
			if (broadcast && Main.netMode == 1 && this.whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(35, -1, -1, "", this.whoAmi, (float)healAmount, 0f, 0f, 0);
			}
		}
		public void ManaEffect(int manaAmount)
		{
			CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(100, 100, 255, 255), string.Concat(manaAmount), false, false);
			if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(43, -1, -1, "", this.whoAmi, (float)manaAmount, 0f, 0f, 0);
			}
		}
		public static byte FindClosest(Vector2 Position, int Width, int Height)
		{
			byte result = 0;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					result = (byte)i;
					break;
				}
			}
			float num = -1f;
			for (int j = 0; j < 255; j++)
			{
				if (Main.player[j].active && !Main.player[j].dead)
				{
					float num2 = Math.Abs(Main.player[j].position.X + (float)(Main.player[j].width / 2) - (Position.X + (float)(Width / 2))) + Math.Abs(Main.player[j].position.Y + (float)(Main.player[j].height / 2) - (Position.Y + (float)(Height / 2)));
					if (num == -1f || num2 < num)
					{
						num = num2;
						result = (byte)j;
					}
				}
			}
			return result;
		}
		public void checkArmor()
		{
		}
		public void toggleInv()
		{
			if (Main.ingameOptionsWindow)
			{
				IngameOptions.Close();
				return;
			}
			if (this.talkNPC >= 0)
			{
				this.talkNPC = -1;
				Main.npcChatCornerItem = 0;
				Main.npcChatText = "";
				Main.PlaySound(11, -1, -1, 1);
				return;
			}
			if (this.sign >= 0)
			{
				this.sign = -1;
				Main.editSign = false;
				Main.npcChatText = "";
				Main.PlaySound(11, -1, -1, 1);
				return;
			}
			if (Main.clothesWindow)
			{
				Main.CancelClothesWindow();
				return;
			}
			if (!Main.playerInventory)
			{
				Recipe.FindRecipes();
				Main.playerInventory = true;
				Main.PlaySound(10, -1, -1, 1);
				return;
			}
			Main.playerInventory = false;
			Main.PlaySound(11, -1, -1, 1);
		}
		public void dropItemCheck()
		{
			if (!Main.playerInventory)
			{
				this.noThrow = 0;
			}
			if (this.noThrow > 0)
			{
				this.noThrow--;
			}
			if (!Main.craftGuide && Main.guideItem.type > 0)
			{
				int num = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, Main.guideItem.type, 1, false, 0, false);
				Main.guideItem.position = Main.item[num].position;
				Main.item[num] = Main.guideItem;
				Main.guideItem = new Item();
				if (Main.netMode == 0)
				{
					Main.item[num].noGrabDelay = 100;
				}
				Main.item[num].velocity.Y = -2f;
				Main.item[num].velocity.X = (float)(4 * this.direction) + this.velocity.X;
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f, 0);
				}
			}
			if (!Main.reforge && Main.reforgeItem.type > 0)
			{
				int num2 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, Main.reforgeItem.type, 1, false, 0, false);
				Main.reforgeItem.position = Main.item[num2].position;
				Main.item[num2] = Main.reforgeItem;
				Main.reforgeItem = new Item();
				if (Main.netMode == 0)
				{
					Main.item[num2].noGrabDelay = 100;
				}
				Main.item[num2].velocity.Y = -2f;
				Main.item[num2].velocity.X = (float)(4 * this.direction) + this.velocity.X;
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", num2, 0f, 0f, 0f, 0);
				}
			}
			if (Main.myPlayer == this.whoAmi)
			{
				this.inventory[58] = Main.mouseItem.Clone();
			}
			bool flag = true;
			if (Main.mouseItem.type > 0 && Main.mouseItem.stack > 0 && !Main.gamePaused)
			{
				Player.tileTargetX = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
				Player.tileTargetY = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
				if (this.gravDir == -1f)
				{
					Player.tileTargetY = (int)((Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16f);
				}
				if (this.selectedItem != 58)
				{
					this.oldSelectItem = this.selectedItem;
				}
				this.selectedItem = 58;
				flag = false;
			}
			if (flag && this.selectedItem == 58)
			{
				this.selectedItem = this.oldSelectItem;
			}
			if (Main.tile[Player.tileTargetX, Player.tileTargetY] != null && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 334 && this.ItemFitsWeaponRack(this.inventory[this.selectedItem]))
			{
				this.noThrow = 2;
			}
			if (((this.controlThrow && this.releaseThrow && this.inventory[this.selectedItem].type > 0 && !Main.chatMode) || (((Main.mouseRight && !this.mouseInterface && Main.mouseRightRelease) || !Main.playerInventory) && Main.mouseItem.type > 0 && Main.mouseItem.stack > 0)) && this.noThrow <= 0)
			{
				Item item = new Item();
				bool flag2 = false;
				if (((Main.mouseRight && !this.mouseInterface && Main.mouseRightRelease) || !Main.playerInventory) && Main.mouseItem.type > 0 && Main.mouseItem.stack > 0)
				{
					item = this.inventory[this.selectedItem];
					this.inventory[this.selectedItem] = Main.mouseItem;
					this.delayUseItem = true;
					this.controlUseItem = false;
					flag2 = true;
				}
				int num3 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.inventory[this.selectedItem].type, 1, false, 0, false);
				if (!flag2 && this.inventory[this.selectedItem].type == 8 && this.inventory[this.selectedItem].stack > 1)
				{
					this.inventory[this.selectedItem].stack--;
				}
				else
				{
					this.inventory[this.selectedItem].position = Main.item[num3].position;
					Main.item[num3] = this.inventory[this.selectedItem];
					this.inventory[this.selectedItem] = new Item();
				}
				if (Main.netMode == 0)
				{
					Main.item[num3].noGrabDelay = 100;
				}
				Main.item[num3].velocity.Y = -2f;
				Main.item[num3].velocity.X = (float)(4 * this.direction) + this.velocity.X;
				if (((Main.mouseRight && !this.mouseInterface) || !Main.playerInventory) && Main.mouseItem.type > 0)
				{
					this.inventory[this.selectedItem] = item;
					Main.mouseItem = new Item();
				}
				else
				{
					this.itemAnimation = 10;
					this.itemAnimationMax = 10;
				}
				Recipe.FindRecipes();
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", num3, 0f, 0f, 0f, 0);
				}
			}
		}
		public int HasBuff(int type)
		{
			if (this.buffImmune[type])
			{
				return -1;
			}
			for (int i = 0; i < 22; i++)
			{
				if (this.buffTime[i] >= 1 && this.buffType[i] == type)
				{
					return i;
				}
			}
			return -1;
		}
		public void AddBuff(int type, int time, bool quiet = true)
		{
			if (this.buffImmune[type])
			{
				return;
			}
			if (!quiet && Main.netMode == 1)
			{
				bool flag = true;
				for (int i = 0; i < 22; i++)
				{
					if (this.buffType[i] == type)
					{
						flag = false;
					}
				}
				if (flag)
				{
					NetMessage.SendData(55, -1, -1, "", this.whoAmi, (float)type, (float)time, 0f, 0);
				}
			}
			int num = -1;
			for (int j = 0; j < 22; j++)
			{
				if (this.buffType[j] == type)
				{
					if (type == 94)
					{
						this.buffTime[j] += time;
						if (this.buffTime[j] > Player.manaSickTimeMax)
						{
							this.buffTime[j] = Player.manaSickTimeMax;
							return;
						}
					}
					else
					{
						if (this.buffTime[j] < time)
						{
							this.buffTime[j] = time;
						}
					}
					return;
				}
			}
			if (Main.vanityPet[type] || Main.lightPet[type])
			{
				for (int k = 0; k < 22; k++)
				{
					if (Main.vanityPet[type] && Main.vanityPet[this.buffType[k]])
					{
						this.DelBuff(k);
					}
					if (Main.lightPet[type] && Main.lightPet[this.buffType[k]])
					{
						this.DelBuff(k);
					}
				}
			}
			while (num == -1)
			{
				int num2 = -1;
				for (int l = 0; l < 22; l++)
				{
					if (!Main.debuff[this.buffType[l]])
					{
						num2 = l;
						break;
					}
				}
				if (num2 == -1)
				{
					return;
				}
				for (int m = num2; m < 22; m++)
				{
					if (this.buffType[m] == 0)
					{
						num = m;
						break;
					}
				}
				if (num == -1)
				{
					this.DelBuff(num2);
				}
			}
			this.buffType[num] = type;
			this.buffTime[num] = time;
			if (Main.meleeBuff[type])
			{
				for (int n = 0; n < 22; n++)
				{
					if (n != num && Main.meleeBuff[this.buffType[n]])
					{
						this.DelBuff(n);
					}
				}
			}
		}
		public void DelBuff(int b)
		{
			this.buffTime[b] = 0;
			this.buffType[b] = 0;
			for (int i = 0; i < 21; i++)
			{
				if (this.buffTime[i] == 0 || this.buffType[i] == 0)
				{
					for (int j = i + 1; j < 22; j++)
					{
						this.buffTime[j - 1] = this.buffTime[j];
						this.buffType[j - 1] = this.buffType[j];
						this.buffTime[j] = 0;
						this.buffType[j] = 0;
					}
				}
			}
		}
		public void ClearBuff(int type)
		{
			for (int i = 0; i < 22; i++)
			{
				if (this.buffType[i] == type)
				{
					this.DelBuff(i);
				}
			}
		}
		public void QuickHeal()
		{
			if (this.noItems)
			{
				return;
			}
			if (this.statLife == this.statLifeMax2 || this.potionDelay > 0)
			{
				return;
			}
			int num = this.statLifeMax2 - this.statLife;
			Item item = null;
			int num2 = -this.statLifeMax2;
			for (int i = 0; i < 58; i++)
			{
				Item item2 = this.inventory[i];
				if (item2.stack > 0 && item2.type > 0 && item2.potion && item2.healLife > 0)
				{
					int num3 = item2.healLife - num;
					if (num2 < 0)
					{
						if (num3 > num2)
						{
							item = item2;
							num2 = num3;
						}
					}
					else
					{
						if (num3 < num2 && num3 >= 0)
						{
							item = item2;
							num2 = num3;
						}
					}
				}
			}
			if (item == null)
			{
				return;
			}
			Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, item.useSound);
			if (item.potion)
			{
				this.potionDelay = this.potionDelayTime;
				this.AddBuff(21, this.potionDelay, true);
			}
			this.statLife += item.healLife;
			this.statMana += item.healMana;
			if (this.statLife > this.statLifeMax2)
			{
				this.statLife = this.statLifeMax2;
			}
			if (this.statMana > this.statManaMax2)
			{
				this.statMana = this.statManaMax2;
			}
			if (item.healLife > 0 && Main.myPlayer == this.whoAmi)
			{
				this.HealEffect(item.healLife, true);
			}
			if (item.healMana > 0 && Main.myPlayer == this.whoAmi)
			{
				this.ManaEffect(item.healMana);
			}
			item.stack--;
			if (item.stack <= 0)
			{
				item.type = 0;
				item.name = "";
			}
			Recipe.FindRecipes();
		}
		public void QuickMana()
		{
			if (this.noItems)
			{
				return;
			}
			if (this.statMana == this.statManaMax2)
			{
				return;
			}
			for (int i = 0; i < 58; i++)
			{
				if (this.inventory[i].stack > 0 && this.inventory[i].type > 0 && this.inventory[i].healMana > 0 && (this.potionDelay == 0 || !this.inventory[i].potion))
				{
					Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, this.inventory[i].useSound);
					if (this.inventory[i].potion)
					{
						this.potionDelay = this.potionDelayTime;
						this.AddBuff(21, this.potionDelay, true);
					}
					this.statLife += this.inventory[i].healLife;
					this.statMana += this.inventory[i].healMana;
					if (this.statLife > this.statLifeMax2)
					{
						this.statLife = this.statLifeMax2;
					}
					if (this.statMana > this.statManaMax2)
					{
						this.statMana = this.statManaMax2;
					}
					if (this.inventory[i].healLife > 0 && Main.myPlayer == this.whoAmi)
					{
						this.HealEffect(this.inventory[i].healLife, true);
					}
					if (this.inventory[i].healMana > 0)
					{
						this.AddBuff(94, Player.manaSickTime, true);
						if (Main.myPlayer == this.whoAmi)
						{
							this.ManaEffect(this.inventory[i].healMana);
						}
					}
					this.inventory[i].stack--;
					if (this.inventory[i].stack <= 0)
					{
						this.inventory[i].type = 0;
						this.inventory[i].name = "";
					}
					Recipe.FindRecipes();
					return;
				}
			}
		}
		public int countBuffs()
		{
			int num = 0;
			for (int i = 0; i < 22; i++)
			{
				if (this.buffType[num] > 0)
				{
					num++;
				}
			}
			return num;
		}
		public void QuickBuff()
		{
			if (this.noItems)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < 58; i++)
			{
				if (this.countBuffs() == 22)
				{
					return;
				}
				if (this.inventory[i].stack > 0 && this.inventory[i].type > 0 && this.inventory[i].buffType > 0 && !this.inventory[i].summon && this.inventory[i].buffType != 90)
				{
					int num2 = this.inventory[i].buffType;
					bool flag = true;
					for (int j = 0; j < 22; j++)
					{
						if (num2 == 27 && (this.buffType[j] == num2 || this.buffType[j] == 101 || this.buffType[j] == 102))
						{
							flag = false;
							break;
						}
						if (this.buffType[j] == num2)
						{
							flag = false;
							break;
						}
						if (Main.meleeBuff[num2] && Main.meleeBuff[this.buffType[j]])
						{
							flag = false;
							break;
						}
					}
					if (Main.lightPet[this.inventory[i].buffType] || Main.vanityPet[this.inventory[i].buffType])
					{
						for (int k = 0; k < 22; k++)
						{
							if (Main.lightPet[this.buffType[k]] && Main.lightPet[this.inventory[i].buffType])
							{
								flag = false;
							}
							if (Main.vanityPet[this.buffType[k]] && Main.vanityPet[this.inventory[i].buffType])
							{
								flag = false;
							}
						}
					}
					if (this.inventory[i].mana > 0 && flag)
					{
						if (this.statMana >= (int)((float)this.inventory[i].mana * this.manaCost))
						{
							this.manaRegenDelay = (int)this.maxRegenDelay;
							this.statMana -= (int)((float)this.inventory[i].mana * this.manaCost);
						}
						else
						{
							flag = false;
						}
					}
					if (this.whoAmi == Main.myPlayer && this.inventory[i].type == 603 && !Main.cEd)
					{
						flag = false;
					}
					if (num2 == 27)
					{
						num2 = Main.rand.Next(3);
						if (num2 == 0)
						{
							num2 = 27;
						}
						if (num2 == 1)
						{
							num2 = 101;
						}
						if (num2 == 2)
						{
							num2 = 102;
						}
					}
					if (flag)
					{
						num = this.inventory[i].useSound;
						int num3 = this.inventory[i].buffTime;
						if (num3 == 0)
						{
							num3 = 3600;
						}
						this.AddBuff(num2, num3, true);
						if (this.inventory[i].consumable)
						{
							this.inventory[i].stack--;
							if (this.inventory[i].stack <= 0)
							{
								this.inventory[i].type = 0;
								this.inventory[i].name = "";
							}
						}
					}
				}
			}
			if (num > 0)
			{
				Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, num);
				Recipe.FindRecipes();
			}
		}
		public void StatusNPC(int type, int i)
		{
			if (this.meleeEnchant > 0)
			{
				if (this.meleeEnchant == 1)
				{
					Main.npc[i].AddBuff(70, 60 * Main.rand.Next(5, 10), false);
				}
				if (this.meleeEnchant == 2)
				{
					Main.npc[i].AddBuff(39, 60 * Main.rand.Next(3, 7), false);
				}
				if (this.meleeEnchant == 3)
				{
					Main.npc[i].AddBuff(24, 60 * Main.rand.Next(3, 7), false);
				}
				if (this.meleeEnchant == 5)
				{
					Main.npc[i].AddBuff(69, 60 * Main.rand.Next(10, 20), false);
				}
				if (this.meleeEnchant == 6)
				{
					Main.npc[i].AddBuff(31, 60 * Main.rand.Next(1, 4), false);
				}
				if (this.meleeEnchant == 8)
				{
					Main.npc[i].AddBuff(20, 60 * Main.rand.Next(5, 10), false);
				}
				if (this.meleeEnchant == 4)
				{
					Main.npc[i].AddBuff(72, 120, false);
				}
			}
			if (this.frostBurn)
			{
				Main.npc[i].AddBuff(44, 60 * Main.rand.Next(5, 15), false);
			}
			if (this.magmaStone)
			{
				if (Main.rand.Next(7) == 0)
				{
					Main.npc[i].AddBuff(24, 360, false);
				}
				else
				{
					if (Main.rand.Next(3) == 0)
					{
						Main.npc[i].AddBuff(24, 120, false);
					}
					else
					{
						Main.npc[i].AddBuff(24, 60, false);
					}
				}
			}
			if (type == 121)
			{
				if (Main.rand.Next(2) == 0)
				{
					Main.npc[i].AddBuff(24, 180, false);
					return;
				}
			}
			else
			{
				if (type == 122)
				{
					if (Main.rand.Next(10) == 0)
					{
						Main.npc[i].AddBuff(24, 180, false);
						return;
					}
				}
				else
				{
					if (type == 190)
					{
						if (Main.rand.Next(4) == 0)
						{
							Main.npc[i].AddBuff(20, 420, false);
							return;
						}
					}
					else
					{
						if (type == 217)
						{
							if (Main.rand.Next(5) == 0)
							{
								Main.npc[i].AddBuff(24, 180, false);
								return;
							}
						}
						else
						{
							if (type == 1123 && Main.rand.Next(10) != 0)
							{
								Main.npc[i].AddBuff(31, 120, false);
							}
						}
					}
				}
			}
		}
		public void StatusPvP(int type, int i)
		{
			if (this.meleeEnchant > 0)
			{
				if (this.meleeEnchant == 1)
				{
					Main.player[i].AddBuff(70, 60 * Main.rand.Next(5, 10), true);
				}
				if (this.meleeEnchant == 2)
				{
					Main.player[i].AddBuff(39, 60 * Main.rand.Next(3, 7), true);
				}
				if (this.meleeEnchant == 3)
				{
					Main.player[i].AddBuff(24, 60 * Main.rand.Next(3, 7), true);
				}
				if (this.meleeEnchant == 5)
				{
					Main.player[i].AddBuff(69, 60 * Main.rand.Next(10, 20), true);
				}
				if (this.meleeEnchant == 6)
				{
					Main.player[i].AddBuff(31, 60 * Main.rand.Next(1, 4), true);
				}
				if (this.meleeEnchant == 8)
				{
					Main.player[i].AddBuff(20, 60 * Main.rand.Next(5, 10), true);
				}
			}
			if (this.frostBurn)
			{
				Main.player[i].AddBuff(44, 60 * Main.rand.Next(1, 8), true);
			}
			if (this.magmaStone)
			{
				if (Main.rand.Next(7) == 0)
				{
					Main.player[i].AddBuff(24, 360, true);
				}
				else
				{
					if (Main.rand.Next(3) == 0)
					{
						Main.player[i].AddBuff(24, 120, true);
					}
					else
					{
						Main.player[i].AddBuff(24, 60, true);
					}
				}
			}
			if (type == 121)
			{
				if (Main.rand.Next(2) == 0)
				{
					Main.player[i].AddBuff(24, 180, false);
					return;
				}
			}
			else
			{
				if (type == 122)
				{
					if (Main.rand.Next(10) == 0)
					{
						Main.player[i].AddBuff(24, 180, false);
						return;
					}
				}
				else
				{
					if (type == 190)
					{
						if (Main.rand.Next(4) == 0)
						{
							Main.player[i].AddBuff(20, 420, false);
							return;
						}
					}
					else
					{
						if (type == 217)
						{
							if (Main.rand.Next(5) == 0)
							{
								Main.player[i].AddBuff(24, 180, false);
								return;
							}
						}
						else
						{
							if (type == 1123 && Main.rand.Next(9) != 0)
							{
								Main.player[i].AddBuff(31, 120, false);
							}
						}
					}
				}
			}
		}
		public void Ghost()
		{
			this.immune = false;
			this.immuneAlpha = 0;
			this.controlUp = false;
			this.controlLeft = false;
			this.controlDown = false;
			this.controlRight = false;
			this.controlJump = false;
			if (Main.hasFocus && !Main.chatMode && !Main.editSign && !Main.editChest && !Main.blockInput)
			{
				Keys[] pressedKeys = Main.keyState.GetPressedKeys();
				if (Main.blockKey != Keys.None)
				{
					bool flag = false;
					for (int i = 0; i < pressedKeys.Length; i++)
					{
						if (pressedKeys[i] == Main.blockKey)
						{
							pressedKeys[i] = Keys.None;
							flag = true;
						}
					}
					if (!flag)
					{
						Main.blockKey = Keys.None;
					}
				}
				for (int j = 0; j < pressedKeys.Length; j++)
				{
					string a = string.Concat(pressedKeys[j]);
					if (a == Main.cUp)
					{
						this.controlUp = true;
					}
					if (a == Main.cLeft)
					{
						this.controlLeft = true;
					}
					if (a == Main.cDown)
					{
						this.controlDown = true;
					}
					if (a == Main.cRight)
					{
						this.controlRight = true;
					}
					if (a == Main.cJump)
					{
						this.controlJump = true;
					}
				}
			}
			if (this.controlUp || this.controlJump)
			{
				if (this.velocity.Y > 0f)
				{
					this.velocity.Y = this.velocity.Y * 0.9f;
				}
				this.velocity.Y = this.velocity.Y - 0.1f;
				if (this.velocity.Y < -3f)
				{
					this.velocity.Y = -3f;
				}
			}
			else
			{
				if (this.controlDown)
				{
					if (this.velocity.Y < 0f)
					{
						this.velocity.Y = this.velocity.Y * 0.9f;
					}
					this.velocity.Y = this.velocity.Y + 0.1f;
					if (this.velocity.Y > 3f)
					{
						this.velocity.Y = 3f;
					}
				}
				else
				{
					if ((double)this.velocity.Y < -0.1 || (double)this.velocity.Y > 0.1)
					{
						this.velocity.Y = this.velocity.Y * 0.9f;
					}
					else
					{
						this.velocity.Y = 0f;
					}
				}
			}
			if (this.controlLeft && !this.controlRight)
			{
				if (this.velocity.X > 0f)
				{
					this.velocity.X = this.velocity.X * 0.9f;
				}
				this.velocity.X = this.velocity.X - 0.1f;
				if (this.velocity.X < -3f)
				{
					this.velocity.X = -3f;
				}
			}
			else
			{
				if (this.controlRight && !this.controlLeft)
				{
					if (this.velocity.X < 0f)
					{
						this.velocity.X = this.velocity.X * 0.9f;
					}
					this.velocity.X = this.velocity.X + 0.1f;
					if (this.velocity.X > 3f)
					{
						this.velocity.X = 3f;
					}
				}
				else
				{
					if ((double)this.velocity.X < -0.1 || (double)this.velocity.X > 0.1)
					{
						this.velocity.X = this.velocity.X * 0.9f;
					}
					else
					{
						this.velocity.X = 0f;
					}
				}
			}
			this.position += this.velocity;
			this.ghostFrameCounter++;
			if (this.velocity.X < 0f)
			{
				this.direction = -1;
			}
			else
			{
				if (this.velocity.X > 0f)
				{
					this.direction = 1;
				}
			}
			if (this.ghostFrameCounter >= 8)
			{
				this.ghostFrameCounter = 0;
				this.ghostFrame++;
				if (this.ghostFrame >= 4)
				{
					this.ghostFrame = 0;
				}
			}
			if (this.position.X < Main.leftWorld + (float)(Lighting.offScreenTiles * 16) + 16f)
			{
				this.position.X = Main.leftWorld + (float)(Lighting.offScreenTiles * 16) + 16f;
				this.velocity.X = 0f;
			}
			if (this.position.X + (float)this.width > Main.rightWorld - (float)(Lighting.offScreenTiles * 16) - 32f)
			{
				this.position.X = Main.rightWorld - (float)(Lighting.offScreenTiles * 16) - 32f - (float)this.width;
				this.velocity.X = 0f;
			}
			if (this.position.Y < Main.topWorld + (float)(Lighting.offScreenTiles * 16) + 16f)
			{
				this.position.Y = Main.topWorld + (float)(Lighting.offScreenTiles * 16) + 16f;
				if ((double)this.velocity.Y < -0.1)
				{
					this.velocity.Y = -0.1f;
				}
			}
			if (this.position.Y > Main.bottomWorld - (float)(Lighting.offScreenTiles * 16) - 32f - (float)this.height)
			{
				this.position.Y = Main.bottomWorld - (float)(Lighting.offScreenTiles * 16) - 32f - (float)this.height;
				this.velocity.Y = 0f;
			}
		}
		public Vector2 Center()
		{
			return new Vector2(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2));
		}
		public void onHit(float x, float y)
		{
			if (Main.myPlayer != this.whoAmi)
			{
				return;
			}
			if (this.onHitDodge && this.shadowDodgeTimer == 0 && Main.rand.Next(4) == 0)
			{
				if (!this.shadowDodge)
				{
					this.shadowDodgeTimer = 1200;
				}
				this.AddBuff(59, 1200, true);
			}
			if (this.onHitRegen)
			{
				this.AddBuff(58, 300, true);
			}
			if (this.onHitPetal && this.petalTimer == 0)
			{
				this.petalTimer = 20;
				if (x < this.position.X + (float)(this.width / 2))
				{
				}
				int num = this.direction;
				float num2 = Main.screenPosition.X;
				if (num < 0)
				{
					num2 += (float)Main.screenWidth;
				}
				float num3 = Main.screenPosition.Y;
				num3 += (float)Main.rand.Next(Main.screenHeight);
				Vector2 vector = new Vector2(num2, num3);
				float num4 = x - vector.X;
				float num5 = y - vector.Y;
				num4 += (float)Main.rand.Next(-50, 51) * 0.1f;
				num5 += (float)Main.rand.Next(-50, 51) * 0.1f;
				int num6 = 24;
				float num7 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
				num7 = (float)num6 / num7;
				num4 *= num7;
				num5 *= num7;
				Projectile.NewProjectile(num2, num3, num4, num5, 221, 36, 0f, this.whoAmi, 0f, 0f);
			}
			if (this.crystalLeaf && this.petalTimer == 0)
			{
				int arg_1AA_0 = this.inventory[this.selectedItem].type;
				for (int i = 0; i < 1000; i++)
				{
					if (Main.projectile[i].owner == this.whoAmi && Main.projectile[i].type == 226)
					{
						this.petalTimer = 50;
						float num8 = 12f;
						Vector2 vector2 = new Vector2(Main.projectile[i].position.X + (float)this.width * 0.5f, Main.projectile[i].position.Y + (float)this.height * 0.5f);
						float num9 = x - vector2.X;
						float num10 = y - vector2.Y;
						float num11 = (float)Math.Sqrt((double)(num9 * num9 + num10 * num10));
						num11 = num8 / num11;
						num9 *= num11;
						num10 *= num11;
						Projectile.NewProjectile(Main.projectile[i].center().X - 4f, Main.projectile[i].center().Y, num9, num10, 227, 50, 5f, this.whoAmi, 0f, 0f);
						return;
					}
				}
			}
		}
		public void openPresent()
		{
			if (Main.rand.Next(15) == 0 && Main.hardMode)
			{
				int number = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 602, 1, false, 0, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", number, 1f, 0f, 0f, 0);
					return;
				}
			}
			else
			{
				if (Main.rand.Next(400) == 0)
				{
					int number2 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1927, 1, false, 0, false);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", number2, 1f, 0f, 0f, 0);
						return;
					}
				}
				else
				{
					if (Main.rand.Next(150) == 0)
					{
						int number3 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1870, 1, false, 0, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number3, 1f, 0f, 0f, 0);
						}
						number3 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 97, Main.rand.Next(30, 61), false, 0, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number3, 1f, 0f, 0f, 0);
							return;
						}
					}
					else
					{
						if (Main.rand.Next(150) == 0)
						{
							int number4 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1909, 1, false, 0, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", number4, 1f, 0f, 0f, 0);
								return;
							}
						}
						else
						{
							if (Main.rand.Next(150) == 0)
							{
								int number5 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1917, 1, false, 0, false);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(21, -1, -1, "", number5, 1f, 0f, 0f, 0);
									return;
								}
							}
							else
							{
								if (Main.rand.Next(150) == 0)
								{
									int number6 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1915, 1, false, 0, false);
									if (Main.netMode == 1)
									{
										NetMessage.SendData(21, -1, -1, "", number6, 1f, 0f, 0f, 0);
										return;
									}
								}
								else
								{
									if (Main.rand.Next(150) == 0)
									{
										int number7 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1918, 1, false, 0, false);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(21, -1, -1, "", number7, 1f, 0f, 0f, 0);
											return;
										}
									}
									else
									{
										if (Main.rand.Next(150) == 0)
										{
											int number8 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1921, 1, false, 0, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(21, -1, -1, "", number8, 1f, 0f, 0f, 0);
												return;
											}
										}
										else
										{
											if (Main.rand.Next(300) == 0)
											{
												int number9 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1923, 1, false, 0, false);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(21, -1, -1, "", number9, 1f, 0f, 0f, 0);
													return;
												}
											}
											else
											{
												if (Main.rand.Next(40) == 0)
												{
													int number10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1907, 1, false, 0, false);
													if (Main.netMode == 1)
													{
														NetMessage.SendData(21, -1, -1, "", number10, 1f, 0f, 0f, 0);
														return;
													}
												}
												else
												{
													if (Main.rand.Next(10) == 0)
													{
														int number11 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1908, 1, false, 0, false);
														if (Main.netMode == 1)
														{
															NetMessage.SendData(21, -1, -1, "", number11, 1f, 0f, 0f, 0);
															return;
														}
													}
													else
													{
														if (Main.rand.Next(15) == 0)
														{
															int num = Main.rand.Next(5);
															if (num == 0)
															{
																int number12 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1932, 1, false, 0, false);
																if (Main.netMode == 1)
																{
																	NetMessage.SendData(21, -1, -1, "", number12, 1f, 0f, 0f, 0);
																}
																number12 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1933, 1, false, 0, false);
																if (Main.netMode == 1)
																{
																	NetMessage.SendData(21, -1, -1, "", number12, 1f, 0f, 0f, 0);
																}
																number12 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1934, 1, false, 0, false);
																if (Main.netMode == 1)
																{
																	NetMessage.SendData(21, -1, -1, "", number12, 1f, 0f, 0f, 0);
																	return;
																}
															}
															else
															{
																if (num == 1)
																{
																	int number13 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1935, 1, false, 0, false);
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(21, -1, -1, "", number13, 1f, 0f, 0f, 0);
																	}
																	number13 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1936, 1, false, 0, false);
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(21, -1, -1, "", number13, 1f, 0f, 0f, 0);
																	}
																	number13 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1937, 1, false, 0, false);
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(21, -1, -1, "", number13, 1f, 0f, 0f, 0);
																		return;
																	}
																}
																else
																{
																	if (num == 2)
																	{
																		int number14 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1940, 1, false, 0, false);
																		if (Main.netMode == 1)
																		{
																			NetMessage.SendData(21, -1, -1, "", number14, 1f, 0f, 0f, 0);
																		}
																		number14 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1941, 1, false, 0, false);
																		if (Main.netMode == 1)
																		{
																			NetMessage.SendData(21, -1, -1, "", number14, 1f, 0f, 0f, 0);
																		}
																		number14 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1942, 1, false, 0, false);
																		if (Main.netMode == 1)
																		{
																			NetMessage.SendData(21, -1, -1, "", number14, 1f, 0f, 0f, 0);
																			return;
																		}
																	}
																	else
																	{
																		if (num == 3)
																		{
																			int number15 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1938, 1, false, 0, false);
																			if (Main.netMode == 1)
																			{
																				NetMessage.SendData(21, -1, -1, "", number15, 1f, 0f, 0f, 0);
																				return;
																			}
																		}
																		else
																		{
																			if (num == 4)
																			{
																				int number16 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1939, 1, false, 0, false);
																				if (Main.netMode == 1)
																				{
																					NetMessage.SendData(21, -1, -1, "", number16, 1f, 0f, 0f, 0);
																					return;
																				}
																			}
																		}
																	}
																}
															}
														}
														else
														{
															if (Main.rand.Next(7) == 0)
															{
																int num2 = Main.rand.Next(3);
																if (num2 == 0)
																{
																	num2 = 1911;
																}
																if (num2 == 1)
																{
																	num2 = 1919;
																}
																if (num2 == 2)
																{
																	num2 = 1920;
																}
																int number17 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num2, 1, false, 0, false);
																if (Main.netMode == 1)
																{
																	NetMessage.SendData(21, -1, -1, "", number17, 1f, 0f, 0f, 0);
																	return;
																}
															}
															else
															{
																if (Main.rand.Next(8) == 0)
																{
																	int number18 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1912, Main.rand.Next(1, 4), false, 0, false);
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(21, -1, -1, "", number18, 1f, 0f, 0f, 0);
																		return;
																	}
																}
																else
																{
																	if (Main.rand.Next(9) == 0)
																	{
																		int number19 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1913, Main.rand.Next(20, 41), false, 0, false);
																		if (Main.netMode == 1)
																		{
																			NetMessage.SendData(21, -1, -1, "", number19, 1f, 0f, 0f, 0);
																			return;
																		}
																	}
																	else
																	{
																		int num3 = Main.rand.Next(3);
																		if (num3 == 0)
																		{
																			int number20 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1872, Main.rand.Next(20, 50), false, 0, false);
																			if (Main.netMode == 1)
																			{
																				NetMessage.SendData(21, -1, -1, "", number20, 1f, 0f, 0f, 0);
																				return;
																			}
																		}
																		else
																		{
																			if (num3 == 1)
																			{
																				int number21 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 586, Main.rand.Next(20, 50), false, 0, false);
																				if (Main.netMode == 1)
																				{
																					NetMessage.SendData(21, -1, -1, "", number21, 1f, 0f, 0f, 0);
																					return;
																				}
																			}
																			else
																			{
																				int number22 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 591, Main.rand.Next(20, 50), false, 0, false);
																				if (Main.netMode == 1)
																				{
																					NetMessage.SendData(21, -1, -1, "", number22, 1f, 0f, 0f, 0);
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		public void openCrate(int type)
		{
			int num = type - 2334;
			if (num == 0)
			{
				bool flag = true;
				while (flag)
				{
					if (Main.hardMode && flag && Main.rand.Next(25) == 0)
					{
						int type2 = 2424;
						int stack = 1;
						int number = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type2, stack, false, 0, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number, 1f, 0f, 0f, 0);
						}
						flag = false;
					}
					if (Main.rand.Next(7) == 0)
					{
						int type3;
						int stack2;
						if (Main.rand.Next(3) == 0)
						{
							type3 = 73;
							stack2 = Main.rand.Next(1, 6);
						}
						else
						{
							type3 = 72;
							stack2 = Main.rand.Next(20, 91);
						}
						int number2 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type3, stack2, false, 0, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number2, 1f, 0f, 0f, 0);
						}
						flag = false;
					}
					if (Main.rand.Next(7) == 0)
					{
						int num2 = Main.rand.Next(8);
						if (num2 == 0)
						{
							num2 = 12;
						}
						else
						{
							if (num2 == 1)
							{
								num2 = 11;
							}
							else
							{
								if (num2 == 2)
								{
									num2 = 14;
								}
								else
								{
									if (num2 == 3)
									{
										num2 = 13;
									}
									else
									{
										if (num2 == 4)
										{
											num2 = 699;
										}
										else
										{
											if (num2 == 5)
											{
												num2 = 700;
											}
											else
											{
												if (num2 == 6)
												{
													num2 = 701;
												}
												else
												{
													if (num2 == 7)
													{
														num2 = 702;
													}
												}
											}
										}
									}
								}
							}
						}
						if (Main.hardMode && Main.rand.Next(2) == 0)
						{
							num2 = Main.rand.Next(6);
							if (num2 == 0)
							{
								num2 = 364;
							}
							else
							{
								if (num2 == 1)
								{
									num2 = 365;
								}
								else
								{
									if (num2 == 2)
									{
										num2 = 366;
									}
									else
									{
										if (num2 == 3)
										{
											num2 = 1104;
										}
										else
										{
											if (num2 == 4)
											{
												num2 = 1105;
											}
											else
											{
												if (num2 == 5)
												{
													num2 = 1106;
												}
											}
										}
									}
								}
							}
						}
						int stack3 = Main.rand.Next(8, 21);
						int number3 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num2, stack3, false, 0, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number3, 1f, 0f, 0f, 0);
						}
						flag = false;
					}
					if (Main.rand.Next(8) == 0)
					{
						int num3 = Main.rand.Next(8);
						if (num3 == 0)
						{
							num3 = 20;
						}
						else
						{
							if (num3 == 1)
							{
								num3 = 22;
							}
							else
							{
								if (num3 == 2)
								{
									num3 = 21;
								}
								else
								{
									if (num3 == 3)
									{
										num3 = 19;
									}
									else
									{
										if (num3 == 4)
										{
											num3 = 703;
										}
										else
										{
											if (num3 == 5)
											{
												num3 = 704;
											}
											else
											{
												if (num3 == 6)
												{
													num3 = 705;
												}
												else
												{
													if (num3 == 7)
													{
														num3 = 706;
													}
												}
											}
										}
									}
								}
							}
						}
						int num4 = Main.rand.Next(2, 8);
						if (Main.hardMode && Main.rand.Next(2) == 0)
						{
							num3 = Main.rand.Next(6);
							if (num3 == 0)
							{
								num3 = 381;
							}
							else
							{
								if (num3 == 1)
								{
									num3 = 382;
								}
								else
								{
									if (num3 == 2)
									{
										num3 = 391;
									}
									else
									{
										if (num3 == 3)
										{
											num3 = 1184;
										}
										else
										{
											if (num3 == 4)
											{
												num3 = 1191;
											}
											else
											{
												if (num3 == 5)
												{
													num3 = 1198;
												}
											}
										}
									}
								}
							}
							num4 -= Main.rand.Next(2);
						}
						int number4 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num3, num4, false, 0, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number4, 1f, 0f, 0f, 0);
						}
						flag = false;
					}
					if (Main.rand.Next(7) == 0)
					{
						int num5 = Main.rand.Next(10);
						if (num5 == 0)
						{
							num5 = 288;
						}
						else
						{
							if (num5 == 1)
							{
								num5 = 290;
							}
							else
							{
								if (num5 == 2)
								{
									num5 = 292;
								}
								else
								{
									if (num5 == 3)
									{
										num5 = 299;
									}
									else
									{
										if (num5 == 4)
										{
											num5 = 298;
										}
										else
										{
											if (num5 == 5)
											{
												num5 = 304;
											}
											else
											{
												if (num5 == 6)
												{
													num5 = 291;
												}
												else
												{
													if (num5 == 7)
													{
														num5 = 2322;
													}
													else
													{
														if (num5 == 8)
														{
															num5 = 2323;
														}
														else
														{
															if (num5 == 9)
															{
																num5 = 2329;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						int stack4 = Main.rand.Next(1, 4);
						int number5 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num5, stack4, false, 0, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number5, 1f, 0f, 0f, 0);
						}
						flag = false;
					}
				}
				if (Main.rand.Next(3) == 0)
				{
					int num6 = Main.rand.Next(2);
					if (num6 == 0)
					{
						num6 = 28;
					}
					else
					{
						if (num6 == 1)
						{
							num6 = 110;
						}
					}
					int stack5 = Main.rand.Next(5, 16);
					int number6 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num6, stack5, false, 0, false);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", number6, 1f, 0f, 0f, 0);
						return;
					}
				}
			}
			else
			{
				if (num == 1)
				{
					bool flag2 = true;
					while (flag2)
					{
						if (flag2 && Main.rand.Next(25) == 0)
						{
							int type4 = 2501;
							int stack6 = 1;
							int number7 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type4, stack6, false, 0, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", number7, 1f, 0f, 0f, 0);
							}
							flag2 = false;
						}
						if (flag2 && Main.rand.Next(20) == 0)
						{
							int type5 = 2587;
							int stack7 = 1;
							int number8 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type5, stack7, false, 0, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", number8, 1f, 0f, 0f, 0);
							}
							flag2 = false;
						}
						if (flag2 && Main.rand.Next(15) == 0)
						{
							int type6 = 2608;
							int stack8 = 1;
							int number9 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type6, stack8, false, 0, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", number9, 1f, 0f, 0f, 0);
							}
							flag2 = false;
						}
						if (Main.rand.Next(4) == 0)
						{
							int type7 = 73;
							int stack9 = Main.rand.Next(5, 11);
							int number10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type7, stack9, false, 0, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", number10, 1f, 0f, 0f, 0);
							}
							flag2 = false;
						}
						if (Main.rand.Next(4) == 0)
						{
							int num7 = Main.rand.Next(8);
							if (num7 == 0)
							{
								num7 = 20;
							}
							else
							{
								if (num7 == 1)
								{
									num7 = 22;
								}
								else
								{
									if (num7 == 2)
									{
										num7 = 21;
									}
									else
									{
										if (num7 == 3)
										{
											num7 = 19;
										}
										else
										{
											if (num7 == 4)
											{
												num7 = 703;
											}
											else
											{
												if (num7 == 5)
												{
													num7 = 704;
												}
												else
												{
													if (num7 == 6)
													{
														num7 = 705;
													}
													else
													{
														if (num7 == 7)
														{
															num7 = 706;
														}
													}
												}
											}
										}
									}
								}
							}
							int num8 = Main.rand.Next(6, 15);
							if (Main.hardMode && Main.rand.Next(3) != 0)
							{
								num7 = Main.rand.Next(6);
								if (num7 == 0)
								{
									num7 = 381;
								}
								else
								{
									if (num7 == 1)
									{
										num7 = 382;
									}
									else
									{
										if (num7 == 2)
										{
											num7 = 391;
										}
										else
										{
											if (num7 == 3)
											{
												num7 = 1184;
											}
											else
											{
												if (num7 == 4)
												{
													num7 = 1191;
												}
												else
												{
													if (num7 == 5)
													{
														num7 = 1198;
													}
												}
											}
										}
									}
								}
								num8 -= Main.rand.Next(2);
							}
							int number11 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num7, num8, false, 0, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", number11, 1f, 0f, 0f, 0);
							}
							flag2 = false;
						}
						if (Main.rand.Next(4) == 0)
						{
							int num9 = Main.rand.Next(8);
							if (num9 == 0)
							{
								num9 = 288;
							}
							else
							{
								if (num9 == 1)
								{
									num9 = 296;
								}
								else
								{
									if (num9 == 2)
									{
										num9 = 304;
									}
									else
									{
										if (num9 == 3)
										{
											num9 = 305;
										}
										else
										{
											if (num9 == 4)
											{
												num9 = 2322;
											}
											else
											{
												if (num9 == 5)
												{
													num9 = 2323;
												}
												else
												{
													if (num9 == 6)
													{
														num9 = 2324;
													}
													else
													{
														if (num9 == 7)
														{
															num9 = 2327;
														}
													}
												}
											}
										}
									}
								}
							}
							int stack10 = Main.rand.Next(2, 5);
							int number12 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num9, stack10, false, 0, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", number12, 1f, 0f, 0f, 0);
							}
							flag2 = false;
						}
					}
					if (Main.rand.Next(2) == 0)
					{
						int type8 = Main.rand.Next(188, 190);
						int stack11 = Main.rand.Next(5, 16);
						int number13 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type8, stack11, false, 0, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number13, 1f, 0f, 0f, 0);
							return;
						}
					}
				}
				else
				{
					if (num == 2)
					{
						bool flag3 = true;
						while (flag3)
						{
							if (flag3 && Main.rand.Next(10) == 0)
							{
								int type9 = 2491;
								int stack12 = 1;
								int number14 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type9, stack12, false, 0, false);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(21, -1, -1, "", number14, 1f, 0f, 0f, 0);
								}
								flag3 = false;
							}
							if (Main.rand.Next(3) == 0)
							{
								int type10 = 73;
								int stack13 = Main.rand.Next(8, 21);
								int number15 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type10, stack13, false, 0, false);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(21, -1, -1, "", number15, 1f, 0f, 0f, 0);
								}
								flag3 = false;
							}
							if (Main.rand.Next(3) == 0)
							{
								int num10 = Main.rand.Next(4);
								if (num10 == 0)
								{
									num10 = 21;
								}
								else
								{
									if (num10 == 1)
									{
										num10 = 19;
									}
									else
									{
										if (num10 == 2)
										{
											num10 = 705;
										}
										else
										{
											if (num10 == 3)
											{
												num10 = 706;
											}
										}
									}
								}
								if (Main.hardMode && Main.rand.Next(3) != 0)
								{
									num10 = Main.rand.Next(4);
									if (num10 == 0)
									{
										num10 = 382;
									}
									else
									{
										if (num10 == 1)
										{
											num10 = 391;
										}
										else
										{
											if (num10 == 2)
											{
												num10 = 1191;
											}
											else
											{
												if (num10 == 3)
												{
													num10 = 1198;
												}
											}
										}
									}
								}
								int stack14 = Main.rand.Next(15, 31);
								int number16 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num10, stack14, false, 0, false);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(21, -1, -1, "", number16, 1f, 0f, 0f, 0);
								}
								flag3 = false;
							}
						}
						if (Main.rand.Next(3) == 0)
						{
							int num11 = Main.rand.Next(5);
							if (num11 == 0)
							{
								num11 = 288;
							}
							else
							{
								if (num11 == 1)
								{
									num11 = 296;
								}
								else
								{
									if (num11 == 2)
									{
										num11 = 305;
									}
									else
									{
										if (num11 == 3)
										{
											num11 = 2322;
										}
										else
										{
											if (num11 == 4)
											{
												num11 = 2323;
											}
										}
									}
								}
							}
							int stack15 = Main.rand.Next(2, 6);
							int number17 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num11, stack15, false, 0, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", number17, 1f, 0f, 0f, 0);
							}
						}
						if (Main.rand.Next(2) == 0)
						{
							int type11 = Main.rand.Next(499, 501);
							int stack16 = Main.rand.Next(5, 21);
							int number18 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type11, stack16, false, 0, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", number18, 1f, 0f, 0f, 0);
							}
						}
					}
				}
			}
		}
		public void openGoodieBag()
		{
			if (Main.rand.Next(150) == 0)
			{
				int number = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1810, 1, false, 0, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", number, 1f, 0f, 0f, 0);
					return;
				}
			}
			else
			{
				if (Main.rand.Next(150) == 0)
				{
					int number2 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1800, 1, false, 0, false);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", number2, 1f, 0f, 0f, 0);
						return;
					}
				}
				else
				{
					if (Main.rand.Next(3) == 1)
					{
						int number3 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1809, Main.rand.Next(10, 41), false, 0, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number3, 1f, 0f, 0f, 0);
							return;
						}
					}
					else
					{
						if (Main.rand.Next(10) == 0)
						{
							int number4 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, Main.rand.Next(1846, 1851), 1, false, 0, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", number4, 1f, 0f, 0f, 0);
								return;
							}
						}
						else
						{
							int num = Main.rand.Next(19);
							if (num == 0)
							{
								int number5 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1749, 1, false, 0, false);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(21, -1, -1, "", number5, 1f, 0f, 0f, 0);
								}
								number5 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1750, 1, false, 0, false);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(21, -1, -1, "", number5, 1f, 0f, 0f, 0);
								}
								number5 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1751, 1, false, 0, false);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(21, -1, -1, "", number5, 1f, 0f, 0f, 0);
									return;
								}
							}
							else
							{
								if (num == 1)
								{
									int number6 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1746, 1, false, 0, false);
									if (Main.netMode == 1)
									{
										NetMessage.SendData(21, -1, -1, "", number6, 1f, 0f, 0f, 0);
									}
									number6 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1747, 1, false, 0, false);
									if (Main.netMode == 1)
									{
										NetMessage.SendData(21, -1, -1, "", number6, 1f, 0f, 0f, 0);
									}
									number6 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1748, 1, false, 0, false);
									if (Main.netMode == 1)
									{
										NetMessage.SendData(21, -1, -1, "", number6, 1f, 0f, 0f, 0);
										return;
									}
								}
								else
								{
									if (num == 2)
									{
										int number7 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1752, 1, false, 0, false);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(21, -1, -1, "", number7, 1f, 0f, 0f, 0);
										}
										number7 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1753, 1, false, 0, false);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(21, -1, -1, "", number7, 1f, 0f, 0f, 0);
											return;
										}
									}
									else
									{
										if (num == 3)
										{
											int number8 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1767, 1, false, 0, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(21, -1, -1, "", number8, 1f, 0f, 0f, 0);
											}
											number8 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1768, 1, false, 0, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(21, -1, -1, "", number8, 1f, 0f, 0f, 0);
											}
											number8 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1769, 1, false, 0, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(21, -1, -1, "", number8, 1f, 0f, 0f, 0);
												return;
											}
										}
										else
										{
											if (num == 4)
											{
												int number9 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1770, 1, false, 0, false);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(21, -1, -1, "", number9, 1f, 0f, 0f, 0);
												}
												number9 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1771, 1, false, 0, false);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(21, -1, -1, "", number9, 1f, 0f, 0f, 0);
													return;
												}
											}
											else
											{
												if (num == 5)
												{
													int number10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1772, 1, false, 0, false);
													if (Main.netMode == 1)
													{
														NetMessage.SendData(21, -1, -1, "", number10, 1f, 0f, 0f, 0);
													}
													number10 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1773, 1, false, 0, false);
													if (Main.netMode == 1)
													{
														NetMessage.SendData(21, -1, -1, "", number10, 1f, 0f, 0f, 0);
														return;
													}
												}
												else
												{
													if (num == 6)
													{
														int number11 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1754, 1, false, 0, false);
														if (Main.netMode == 1)
														{
															NetMessage.SendData(21, -1, -1, "", number11, 1f, 0f, 0f, 0);
														}
														number11 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1755, 1, false, 0, false);
														if (Main.netMode == 1)
														{
															NetMessage.SendData(21, -1, -1, "", number11, 1f, 0f, 0f, 0);
														}
														number11 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1756, 1, false, 0, false);
														if (Main.netMode == 1)
														{
															NetMessage.SendData(21, -1, -1, "", number11, 1f, 0f, 0f, 0);
															return;
														}
													}
													else
													{
														if (num == 7)
														{
															int number12 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1757, 1, false, 0, false);
															if (Main.netMode == 1)
															{
																NetMessage.SendData(21, -1, -1, "", number12, 1f, 0f, 0f, 0);
															}
															number12 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1758, 1, false, 0, false);
															if (Main.netMode == 1)
															{
																NetMessage.SendData(21, -1, -1, "", number12, 1f, 0f, 0f, 0);
															}
															number12 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1759, 1, false, 0, false);
															if (Main.netMode == 1)
															{
																NetMessage.SendData(21, -1, -1, "", number12, 1f, 0f, 0f, 0);
																return;
															}
														}
														else
														{
															if (num == 8)
															{
																int number13 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1760, 1, false, 0, false);
																if (Main.netMode == 1)
																{
																	NetMessage.SendData(21, -1, -1, "", number13, 1f, 0f, 0f, 0);
																}
																number13 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1761, 1, false, 0, false);
																if (Main.netMode == 1)
																{
																	NetMessage.SendData(21, -1, -1, "", number13, 1f, 0f, 0f, 0);
																}
																number13 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1762, 1, false, 0, false);
																if (Main.netMode == 1)
																{
																	NetMessage.SendData(21, -1, -1, "", number13, 1f, 0f, 0f, 0);
																	return;
																}
															}
															else
															{
																if (num == 9)
																{
																	int number14 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1763, 1, false, 0, false);
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(21, -1, -1, "", number14, 1f, 0f, 0f, 0);
																	}
																	number14 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1764, 1, false, 0, false);
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(21, -1, -1, "", number14, 1f, 0f, 0f, 0);
																	}
																	number14 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1765, 1, false, 0, false);
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(21, -1, -1, "", number14, 1f, 0f, 0f, 0);
																		return;
																	}
																}
																else
																{
																	if (num == 10)
																	{
																		int number15 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1766, 1, false, 0, false);
																		if (Main.netMode == 1)
																		{
																			NetMessage.SendData(21, -1, -1, "", number15, 1f, 0f, 0f, 0);
																		}
																		number15 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1775, 1, false, 0, false);
																		if (Main.netMode == 1)
																		{
																			NetMessage.SendData(21, -1, -1, "", number15, 1f, 0f, 0f, 0);
																		}
																		number15 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1776, 1, false, 0, false);
																		if (Main.netMode == 1)
																		{
																			NetMessage.SendData(21, -1, -1, "", number15, 1f, 0f, 0f, 0);
																			return;
																		}
																	}
																	else
																	{
																		if (num == 11)
																		{
																			int number16 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1777, 1, false, 0, false);
																			if (Main.netMode == 1)
																			{
																				NetMessage.SendData(21, -1, -1, "", number16, 1f, 0f, 0f, 0);
																			}
																			number16 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1778, 1, false, 0, false);
																			if (Main.netMode == 1)
																			{
																				NetMessage.SendData(21, -1, -1, "", number16, 1f, 0f, 0f, 0);
																				return;
																			}
																		}
																		else
																		{
																			if (num == 12)
																			{
																				int number17 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1779, 1, false, 0, false);
																				if (Main.netMode == 1)
																				{
																					NetMessage.SendData(21, -1, -1, "", number17, 1f, 0f, 0f, 0);
																				}
																				number17 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1780, 1, false, 0, false);
																				if (Main.netMode == 1)
																				{
																					NetMessage.SendData(21, -1, -1, "", number17, 1f, 0f, 0f, 0);
																				}
																				number17 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1781, 1, false, 0, false);
																				if (Main.netMode == 1)
																				{
																					NetMessage.SendData(21, -1, -1, "", number17, 1f, 0f, 0f, 0);
																					return;
																				}
																			}
																			else
																			{
																				if (num == 13)
																				{
																					int number18 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1819, 1, false, 0, false);
																					if (Main.netMode == 1)
																					{
																						NetMessage.SendData(21, -1, -1, "", number18, 1f, 0f, 0f, 0);
																					}
																					number18 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1820, 1, false, 0, false);
																					if (Main.netMode == 1)
																					{
																						NetMessage.SendData(21, -1, -1, "", number18, 1f, 0f, 0f, 0);
																						return;
																					}
																				}
																				else
																				{
																					if (num == 14)
																					{
																						int number19 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1821, 1, false, 0, false);
																						if (Main.netMode == 1)
																						{
																							NetMessage.SendData(21, -1, -1, "", number19, 1f, 0f, 0f, 0);
																						}
																						number19 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1822, 1, false, 0, false);
																						if (Main.netMode == 1)
																						{
																							NetMessage.SendData(21, -1, -1, "", number19, 1f, 0f, 0f, 0);
																						}
																						number19 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1823, 1, false, 0, false);
																						if (Main.netMode == 1)
																						{
																							NetMessage.SendData(21, -1, -1, "", number19, 1f, 0f, 0f, 0);
																							return;
																						}
																					}
																					else
																					{
																						if (num == 15)
																						{
																							int number20 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1824, 1, false, 0, false);
																							if (Main.netMode == 1)
																							{
																								NetMessage.SendData(21, -1, -1, "", number20, 1f, 0f, 0f, 0);
																								return;
																							}
																						}
																						else
																						{
																							if (num == 16)
																							{
																								int number21 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1838, 1, false, 0, false);
																								if (Main.netMode == 1)
																								{
																									NetMessage.SendData(21, -1, -1, "", number21, 1f, 0f, 0f, 0);
																								}
																								number21 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1839, 1, false, 0, false);
																								if (Main.netMode == 1)
																								{
																									NetMessage.SendData(21, -1, -1, "", number21, 1f, 0f, 0f, 0);
																								}
																								number21 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1840, 1, false, 0, false);
																								if (Main.netMode == 1)
																								{
																									NetMessage.SendData(21, -1, -1, "", number21, 1f, 0f, 0f, 0);
																									return;
																								}
																							}
																							else
																							{
																								if (num == 17)
																								{
																									int number22 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1841, 1, false, 0, false);
																									if (Main.netMode == 1)
																									{
																										NetMessage.SendData(21, -1, -1, "", number22, 1f, 0f, 0f, 0);
																									}
																									number22 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1842, 1, false, 0, false);
																									if (Main.netMode == 1)
																									{
																										NetMessage.SendData(21, -1, -1, "", number22, 1f, 0f, 0f, 0);
																									}
																									number22 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1843, 1, false, 0, false);
																									if (Main.netMode == 1)
																									{
																										NetMessage.SendData(21, -1, -1, "", number22, 1f, 0f, 0f, 0);
																										return;
																									}
																								}
																								else
																								{
																									if (num == 18)
																									{
																										int number23 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1851, 1, false, 0, false);
																										if (Main.netMode == 1)
																										{
																											NetMessage.SendData(21, -1, -1, "", number23, 1f, 0f, 0f, 0);
																										}
																										number23 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 1852, 1, false, 0, false);
																										if (Main.netMode == 1)
																										{
																											NetMessage.SendData(21, -1, -1, "", number23, 1f, 0f, 0f, 0);
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		public void UpdateBuffs(int i)
		{
			int[] array = new int[423];
			for (int j = 0; j < 1000; j++)
			{
				if (Main.projectile[j].active && Main.projectile[j].owner == i)
				{
					array[Main.projectile[j].type]++;
				}
			}
			for (int k = 0; k < 22; k++)
			{
				if (this.buffType[k] > 0 && this.buffTime[k] > 0)
				{
					if (this.whoAmi == Main.myPlayer && this.buffType[k] != 28)
					{
						this.buffTime[k]--;
					}
					if (this.buffType[k] == 1)
					{
						this.lavaImmune = true;
						this.fireWalk = true;
					}
					else
					{
						if (this.buffType[k] == 2)
						{
							this.lifeRegen += 2;
						}
						else
						{
							if (this.buffType[k] == 3)
							{
								this.moveSpeed += 0.25f;
							}
							else
							{
								if (this.buffType[k] == 4)
								{
									this.gills = true;
								}
								else
								{
									if (this.buffType[k] == 5)
									{
										this.statDefense += 8;
									}
									else
									{
										if (this.buffType[k] == 6)
										{
											this.manaRegenBuff = true;
										}
										else
										{
											if (this.buffType[k] == 7)
											{
												this.magicDamage += 0.2f;
											}
											else
											{
												if (this.buffType[k] == 8)
												{
													this.slowFall = true;
												}
												else
												{
													if (this.buffType[k] == 9)
													{
														this.findTreasure = true;
													}
													else
													{
														if (this.buffType[k] == 10)
														{
															this.invis = true;
														}
														else
														{
															if (this.buffType[k] == 11)
															{
																Lighting.addLight((int)(this.position.X + (float)(this.width / 2)) / 16, (int)(this.position.Y + (float)(this.height / 2)) / 16, 0.8f, 0.95f, 1f);
															}
															else
															{
																if (this.buffType[k] == 12)
																{
																	this.nightVision = true;
																}
																else
																{
																	if (this.buffType[k] == 13)
																	{
																		this.enemySpawns = true;
																	}
																	else
																	{
																		if (this.buffType[k] == 14)
																		{
																			this.thorns = true;
																		}
																		else
																		{
																			if (this.buffType[k] == 15)
																			{
																				this.waterWalk = true;
																			}
																			else
																			{
																				if (this.buffType[k] == 16)
																				{
																					this.archery = true;
																				}
																				else
																				{
																					if (this.buffType[k] == 17)
																					{
																						this.detectCreature = true;
																					}
																					else
																					{
																						if (this.buffType[k] == 18)
																						{
																							this.gravControl = true;
																						}
																						else
																						{
																							if (this.buffType[k] == 30)
																							{
																								this.bleed = true;
																							}
																							else
																							{
																								if (this.buffType[k] == 31)
																								{
																									this.confused = true;
																								}
																								else
																								{
																									if (this.buffType[k] == 32)
																									{
																										this.slow = true;
																									}
																									else
																									{
																										if (this.buffType[k] == 35)
																										{
																											this.silence = true;
																										}
																										else
																										{
																											if (this.buffType[k] == 46)
																											{
																												this.chilled = true;
																											}
																											else
																											{
																												if (this.buffType[k] == 47)
																												{
																													this.frozen = true;
																												}
																												else
																												{
																													if (this.buffType[k] == 69)
																													{
																														this.ichor = true;
																														this.statDefense -= 20;
																													}
																													else
																													{
																														if (this.buffType[k] == 36)
																														{
																															this.brokenArmor = true;
																														}
																														else
																														{
																															if (this.buffType[k] == 48)
																															{
																																this.honey = true;
																															}
																															else
																															{
																																if (this.buffType[k] == 59)
																																{
																																	this.shadowDodge = true;
																																}
																																else
																																{
																																	if (this.buffType[k] == 93)
																																	{
																																		this.ammoBox = true;
																																	}
																																	else
																																	{
																																		if (this.buffType[k] == 58)
																																		{
																																			this.palladiumRegen = true;
																																		}
																																		else
																																		{
																																			if (this.buffType[k] == 88)
																																			{
																																				this.chaosState = true;
																																			}
																																			else
																																			{
																																				if (this.buffType[k] == 63)
																																				{
																																					this.moveSpeed += 1f;
																																				}
																																				else
																																				{
																																					if (this.buffType[k] == 104)
																																					{
																																						this.pickSpeed -= 0.25f;
																																					}
																																					else
																																					{
																																						if (this.buffType[k] == 105)
																																						{
																																							this.lifeMagnet = true;
																																						}
																																						else
																																						{
																																							if (this.buffType[k] == 106)
																																							{
																																								this.calmed = true;
																																							}
																																							else
																																							{
																																								if (this.buffType[k] == 121)
																																								{
																																									this.fishingSkill += 15;
																																								}
																																								else
																																								{
																																									if (this.buffType[k] == 122)
																																									{
																																										this.sonarPotion = true;
																																									}
																																									else
																																									{
																																										if (this.buffType[k] == 123)
																																										{
																																											this.cratePotion = true;
																																										}
																																										else
																																										{
																																											if (this.buffType[k] == 107)
																																											{
																																												this.tileSpeed += 0.25f;
																																												this.wallSpeed += 0.25f;
																																												this.blockRange++;
																																											}
																																											else
																																											{
																																												if (this.buffType[k] == 108)
																																												{
																																													this.kbBuff = true;
																																												}
																																												else
																																												{
																																													if (this.buffType[k] == 109)
																																													{
																																														this.ignoreWater = true;
																																														this.accFlipper = true;
																																													}
																																													else
																																													{
																																														if (this.buffType[k] == 110)
																																														{
																																															this.maxMinions++;
																																														}
																																														else
																																														{
																																															if (this.buffType[k] == 111)
																																															{
																																																this.dangerSense = true;
																																															}
																																															else
																																															{
																																																if (this.buffType[k] == 112)
																																																{
																																																	this.ammoPotion = true;
																																																}
																																																else
																																																{
																																																	if (this.buffType[k] == 113)
																																																	{
																																																		this.lifeForce = true;
																																																		this.statLifeMax2 += this.statLifeMax / 5 / 20 * 20;
																																																	}
																																																	else
																																																	{
																																																		if (this.buffType[k] == 114)
																																																		{
																																																			this.endurance += 0.1f;
																																																		}
																																																		else
																																																		{
																																																			if (this.buffType[k] == 115)
																																																			{
																																																				this.meleeCrit += 10;
																																																				this.rangedCrit += 10;
																																																				this.magicCrit += 10;
																																																			}
																																																			else
																																																			{
																																																				if (this.buffType[k] == 116)
																																																				{
																																																					this.inferno = true;
																																																					Lighting.addLight((int)(this.Center().X / 16f), (int)(this.Center().Y / 16f), 0.65f, 0.4f, 0.1f);
																																																					int num = 24;
																																																					float num2 = 200f;
																																																					bool flag = this.infernoCounter % 60 == 0;
																																																					int num3 = 10;
																																																					if (this.whoAmi == Main.myPlayer)
																																																					{
																																																						for (int l = 0; l < 200; l++)
																																																						{
																																																							NPC nPC = Main.npc[l];
																																																							if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[num] && Vector2.Distance(this.center(), nPC.center()) <= num2)
																																																							{
																																																								if (nPC.HasBuff(num) == -1)
																																																								{
																																																									nPC.AddBuff(num, 120, false);
																																																								}
																																																								if (flag)
																																																								{
																																																									nPC.StrikeNPC(num3, 0f, 0, false, false);
																																																									if (Main.netMode != 0)
																																																									{
																																																										NetMessage.SendData(28, -1, -1, "", l, (float)num3, 0f, 0f, 0);
																																																									}
																																																								}
																																																							}
																																																						}
																																																						if (this.hostile)
																																																						{
																																																							for (int m = 0; m < 255; m++)
																																																							{
																																																								Player player = Main.player[m];
																																																								if (player != this && player.active && !player.dead && player.hostile && !player.buffImmune[num] && (player.team != this.team || player.team == 0) && Vector2.Distance(this.center(), player.center()) <= num2)
																																																								{
																																																									if (player.HasBuff(num) == -1)
																																																									{
																																																										player.AddBuff(num, 120, true);
																																																									}
																																																									if (flag)
																																																									{
																																																										player.Hurt(num3, 0, true, false, "", false);
																																																										if (Main.netMode != 0)
																																																										{
																																																											NetMessage.SendData(26, -1, -1, Lang.deathMsg(this.whoAmi, -1, -1, -1), m, 0f, (float)num3, 1f, 0);
																																																										}
																																																									}
																																																								}
																																																							}
																																																						}
																																																					}
																																																				}
																																																				else
																																																				{
																																																					if (this.buffType[k] == 117)
																																																					{
																																																						this.meleeDamage += 0.1f;
																																																						this.rangedDamage += 0.1f;
																																																						this.magicDamage += 0.1f;
																																																						this.minionDamage += 0.1f;
																																																					}
																																																					else
																																																					{
																																																						if (this.buffType[k] == 119)
																																																						{
																																																							this.loveStruck = true;
																																																						}
																																																						else
																																																						{
																																																							if (this.buffType[k] == 120)
																																																							{
																																																								this.stinky = true;
																																																							}
																																																							else
																																																							{
																																																								if (this.buffType[k] == 124)
																																																								{
																																																									this.resistCold = true;
																																																								}
																																																								else
																																																								{
																																																									if (this.buffType[k] == 94)
																																																									{
																																																										this.manaSick = true;
																																																										this.manaSickReduction = Player.manaSickLessDmg * ((float)this.buffTime[k] / (float)Player.manaSickTime);
																																																									}
																																																									else
																																																									{
																																																										if (this.buffType[k] >= 95 && this.buffType[k] <= 97)
																																																										{
																																																											this.buffTime[k] = 5;
																																																											int num4 = (int)((byte)(1 + this.buffType[k] - 95));
																																																											if (this.beetleOrbs > 0 && this.beetleOrbs != num4)
																																																											{
																																																												if (this.beetleOrbs > num4)
																																																												{
																																																													this.DelBuff(k);
																																																													k--;
																																																												}
																																																												else
																																																												{
																																																													for (int n = 0; n < 22; n++)
																																																													{
																																																														if (this.buffType[n] >= 95 && this.buffType[n] <= 95 + num4 - 1)
																																																														{
																																																															this.DelBuff(n);
																																																															n--;
																																																														}
																																																													}
																																																												}
																																																											}
																																																											this.beetleOrbs = num4;
																																																											if (!this.beetleDefense)
																																																											{
																																																												this.beetleOrbs = 0;
																																																												this.DelBuff(k);
																																																												k--;
																																																											}
																																																											else
																																																											{
																																																												this.beetleBuff = true;
																																																											}
																																																										}
																																																										else
																																																										{
																																																											if (this.buffType[k] >= 98 && this.buffType[k] <= 100)
																																																											{
																																																												int num5 = (int)((byte)(1 + this.buffType[k] - 98));
																																																												if (this.beetleOrbs > 0 && this.beetleOrbs != num5)
																																																												{
																																																													if (this.beetleOrbs > num5)
																																																													{
																																																														this.DelBuff(k);
																																																														k--;
																																																													}
																																																													else
																																																													{
																																																														for (int num6 = 0; num6 < 22; num6++)
																																																														{
																																																															if (this.buffType[num6] >= 98 && this.buffType[num6] <= 98 + num5 - 1)
																																																															{
																																																																this.DelBuff(num6);
																																																																num6--;
																																																															}
																																																														}
																																																													}
																																																												}
																																																												this.beetleOrbs = num5;
																																																												this.meleeDamage += 0.1f * (float)this.beetleOrbs;
																																																												this.meleeSpeed += 0.1f * (float)this.beetleOrbs;
																																																												if (!this.beetleOffense)
																																																												{
																																																													this.beetleOrbs = 0;
																																																													this.DelBuff(k);
																																																													k--;
																																																												}
																																																												else
																																																												{
																																																													this.beetleBuff = true;
																																																												}
																																																											}
																																																											else
																																																											{
																																																												if (this.buffType[k] == 62)
																																																												{
																																																													if ((double)this.statLife <= (double)this.statLifeMax2 * 0.25)
																																																													{
																																																														Lighting.addLight((int)(this.Center().X / 16f), (int)(this.Center().Y / 16f), 0.1f, 0.2f, 0.45f);
																																																														this.iceBarrier = true;
																																																														this.statDefense += 30;
																																																														this.iceBarrierFrameCounter += 1;
																																																														if (this.iceBarrierFrameCounter > 2)
																																																														{
																																																															this.iceBarrierFrameCounter = 0;
																																																															this.iceBarrierFrame += 1;
																																																															if (this.iceBarrierFrame >= 12)
																																																															{
																																																																this.iceBarrierFrame = 0;
																																																															}
																																																														}
																																																													}
																																																													else
																																																													{
																																																														this.DelBuff(k);
																																																														k--;
																																																													}
																																																												}
																																																												else
																																																												{
																																																													if (this.buffType[k] == 49)
																																																													{
																																																														for (int num7 = 191; num7 <= 194; num7++)
																																																														{
																																																															if (array[num7] > 0)
																																																															{
																																																																this.pygmy = true;
																																																															}
																																																														}
																																																														if (!this.pygmy)
																																																														{
																																																															this.DelBuff(k);
																																																															k--;
																																																														}
																																																														else
																																																														{
																																																															this.buffTime[k] = 18000;
																																																														}
																																																													}
																																																													else
																																																													{
																																																														if (this.buffType[k] == 83)
																																																														{
																																																															if (array[317] > 0)
																																																															{
																																																																this.raven = true;
																																																															}
																																																															if (!this.raven)
																																																															{
																																																																this.DelBuff(k);
																																																																k--;
																																																															}
																																																															else
																																																															{
																																																																this.buffTime[k] = 18000;
																																																															}
																																																														}
																																																														else
																																																														{
																																																															if (this.buffType[k] == 64)
																																																															{
																																																																if (array[266] > 0)
																																																																{
																																																																	this.slime = true;
																																																																}
																																																																if (!this.slime)
																																																																{
																																																																	this.DelBuff(k);
																																																																	k--;
																																																																}
																																																																else
																																																																{
																																																																	this.buffTime[k] = 18000;
																																																																}
																																																															}
																																																															else
																																																															{
																																																																if (this.buffType[k] == 125)
																																																																{
																																																																	if (array[373] > 0)
																																																																	{
																																																																		this.hornetMinion = true;
																																																																	}
																																																																	if (!this.hornetMinion)
																																																																	{
																																																																		this.DelBuff(k);
																																																																		k--;
																																																																	}
																																																																	else
																																																																	{
																																																																		this.buffTime[k] = 18000;
																																																																	}
																																																																}
																																																																else
																																																																{
																																																																	if (this.buffType[k] == 126)
																																																																	{
																																																																		if (array[375] > 0)
																																																																		{
																																																																			this.impMinion = true;
																																																																		}
																																																																		if (!this.impMinion)
																																																																		{
																																																																			this.DelBuff(k);
																																																																			k--;
																																																																		}
																																																																		else
																																																																		{
																																																																			this.buffTime[k] = 18000;
																																																																		}
																																																																	}
																																																																	else
																																																																	{
																																																																		if (this.buffType[k] == 133)
																																																																		{
																																																																			if (array[390] > 0 || array[391] > 0 || array[392] > 0)
																																																																			{
																																																																				this.spiderMinion = true;
																																																																			}
																																																																			if (!this.spiderMinion)
																																																																			{
																																																																				this.DelBuff(k);
																																																																				k--;
																																																																			}
																																																																			else
																																																																			{
																																																																				this.buffTime[k] = 18000;
																																																																			}
																																																																		}
																																																																		else
																																																																		{
																																																																			if (this.buffType[k] == 134)
																																																																			{
																																																																				if (array[387] > 0 || array[388] > 0)
																																																																				{
																																																																					this.twinsMinion = true;
																																																																				}
																																																																				if (!this.twinsMinion)
																																																																				{
																																																																					this.DelBuff(k);
																																																																					k--;
																																																																				}
																																																																				else
																																																																				{
																																																																					this.buffTime[k] = 18000;
																																																																				}
																																																																			}
																																																																			else
																																																																			{
																																																																				if (this.buffType[k] == 135)
																																																																				{
																																																																					if (array[393] > 0 || array[394] > 0 || array[395] > 0)
																																																																					{
																																																																						this.pirateMinion = true;
																																																																					}
																																																																					if (!this.pirateMinion)
																																																																					{
																																																																						this.DelBuff(k);
																																																																						k--;
																																																																					}
																																																																					else
																																																																					{
																																																																						this.buffTime[k] = 18000;
																																																																					}
																																																																				}
																																																																				else
																																																																				{
																																																																					if (this.buffType[k] == 139)
																																																																					{
																																																																						if (array[407] > 0)
																																																																						{
																																																																							this.sharknadoMinion = true;
																																																																						}
																																																																						if (!this.sharknadoMinion)
																																																																						{
																																																																							this.DelBuff(k);
																																																																							k--;
																																																																						}
																																																																						else
																																																																						{
																																																																							this.buffTime[k] = 18000;
																																																																						}
																																																																					}
																																																																					else
																																																																					{
																																																																						if (this.buffType[k] == 90)
																																																																						{
																																																																							this.mount.SetMount(0, this, false);
																																																																							this.buffTime[k] = 10;
																																																																						}
																																																																						else
																																																																						{
																																																																							if (this.buffType[k] == 128)
																																																																							{
																																																																								this.mount.SetMount(1, this, false);
																																																																								this.buffTime[k] = 10;
																																																																							}
																																																																							else
																																																																							{
																																																																								if (this.buffType[k] == 129)
																																																																								{
																																																																									this.mount.SetMount(2, this, false);
																																																																									this.buffTime[k] = 10;
																																																																								}
																																																																								else
																																																																								{
																																																																									if (this.buffType[k] == 130)
																																																																									{
																																																																										this.mount.SetMount(3, this, false);
																																																																										this.buffTime[k] = 10;
																																																																									}
																																																																									else
																																																																									{
																																																																										if (this.buffType[k] == 118)
																																																																										{
																																																																											this.mount.SetMount(6, this, true);
																																																																											this.buffTime[k] = 10;
																																																																										}
																																																																										else
																																																																										{
																																																																											if (this.buffType[k] == 138)
																																																																											{
																																																																												this.mount.SetMount(6, this, false);
																																																																												this.buffTime[k] = 10;
																																																																											}
																																																																											else
																																																																											{
																																																																												if (this.buffType[k] == 131)
																																																																												{
																																																																													this.ignoreWater = true;
																																																																													this.accFlipper = true;
																																																																													this.mount.SetMount(4, this, false);
																																																																													this.buffTime[k] = 10;
																																																																												}
																																																																												else
																																																																												{
																																																																													if (this.buffType[k] == 132)
																																																																													{
																																																																														this.mount.SetMount(5, this, false);
																																																																														this.buffTime[k] = 10;
																																																																													}
																																																																													else
																																																																													{
																																																																														if (this.buffType[k] == 37)
																																																																														{
																																																																															if (Main.wof >= 0 && Main.npc[Main.wof].type == 113)
																																																																															{
																																																																																this.gross = true;
																																																																																this.buffTime[k] = 10;
																																																																															}
																																																																															else
																																																																															{
																																																																																this.DelBuff(k);
																																																																																k--;
																																																																															}
																																																																														}
																																																																														else
																																																																														{
																																																																															if (this.buffType[k] == 38)
																																																																															{
																																																																																this.buffTime[k] = 10;
																																																																																this.tongued = true;
																																																																															}
																																																																															else
																																																																															{
																																																																																if (this.buffType[k] == 19)
																																																																																{
																																																																																	this.buffTime[k] = 18000;
																																																																																	this.lightOrb = true;
																																																																																	bool flag2 = true;
																																																																																	if (array[18] > 0)
																																																																																	{
																																																																																		flag2 = false;
																																																																																	}
																																																																																	if (flag2)
																																																																																	{
																																																																																		Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 18, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																	}
																																																																																}
																																																																																else
																																																																																{
																																																																																	if (this.buffType[k] == 27 || this.buffType[k] == 101 || this.buffType[k] == 102)
																																																																																	{
																																																																																		this.buffTime[k] = 18000;
																																																																																		bool flag3 = true;
																																																																																		int num8 = 72;
																																																																																		if (this.buffType[k] == 27)
																																																																																		{
																																																																																			this.blueFairy = true;
																																																																																		}
																																																																																		if (this.buffType[k] == 101)
																																																																																		{
																																																																																			num8 = 86;
																																																																																			this.redFairy = true;
																																																																																		}
																																																																																		if (this.buffType[k] == 102)
																																																																																		{
																																																																																			num8 = 87;
																																																																																			this.greenFairy = true;
																																																																																		}
																																																																																		if (this.head == 45 && this.body == 26 && this.legs == 25)
																																																																																		{
																																																																																			num8 = 72;
																																																																																		}
																																																																																		if (array[num8] > 0)
																																																																																		{
																																																																																			flag3 = false;
																																																																																		}
																																																																																		if (flag3)
																																																																																		{
																																																																																			Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, num8, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																		}
																																																																																	}
																																																																																	else
																																																																																	{
																																																																																		if (this.buffType[k] == 40)
																																																																																		{
																																																																																			this.buffTime[k] = 18000;
																																																																																			this.bunny = true;
																																																																																			bool flag4 = true;
																																																																																			if (array[111] > 0)
																																																																																			{
																																																																																				flag4 = false;
																																																																																			}
																																																																																			if (flag4)
																																																																																			{
																																																																																				Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 111, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																			}
																																																																																		}
																																																																																		else
																																																																																		{
																																																																																			if (this.buffType[k] == 41)
																																																																																			{
																																																																																				this.buffTime[k] = 18000;
																																																																																				this.penguin = true;
																																																																																				bool flag5 = true;
																																																																																				if (array[112] > 0)
																																																																																				{
																																																																																					flag5 = false;
																																																																																				}
																																																																																				if (flag5)
																																																																																				{
																																																																																					Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 112, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																				}
																																																																																			}
																																																																																			else
																																																																																			{
																																																																																				if (this.buffType[k] == 91)
																																																																																				{
																																																																																					this.buffTime[k] = 18000;
																																																																																					this.puppy = true;
																																																																																					bool flag6 = true;
																																																																																					if (array[334] > 0)
																																																																																					{
																																																																																						flag6 = false;
																																																																																					}
																																																																																					if (flag6)
																																																																																					{
																																																																																						Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 334, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																					}
																																																																																				}
																																																																																				else
																																																																																				{
																																																																																					if (this.buffType[k] == 92)
																																																																																					{
																																																																																						this.buffTime[k] = 18000;
																																																																																						this.grinch = true;
																																																																																						bool flag7 = true;
																																																																																						if (array[353] > 0)
																																																																																						{
																																																																																							flag7 = false;
																																																																																						}
																																																																																						if (flag7)
																																																																																						{
																																																																																							Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 353, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																						}
																																																																																					}
																																																																																					else
																																																																																					{
																																																																																						if (this.buffType[k] == 84)
																																																																																						{
																																																																																							this.buffTime[k] = 18000;
																																																																																							this.blackCat = true;
																																																																																							bool flag8 = true;
																																																																																							if (array[319] > 0)
																																																																																							{
																																																																																								flag8 = false;
																																																																																							}
																																																																																							if (flag8)
																																																																																							{
																																																																																								Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 319, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																							}
																																																																																						}
																																																																																						else
																																																																																						{
																																																																																							if (this.buffType[k] == 61)
																																																																																							{
																																																																																								this.buffTime[k] = 18000;
																																																																																								this.dino = true;
																																																																																								bool flag9 = true;
																																																																																								if (array[236] > 0)
																																																																																								{
																																																																																									flag9 = false;
																																																																																								}
																																																																																								if (flag9)
																																																																																								{
																																																																																									Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 236, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																								}
																																																																																							}
																																																																																							else
																																																																																							{
																																																																																								if (this.buffType[k] == 65)
																																																																																								{
																																																																																									this.buffTime[k] = 18000;
																																																																																									this.eyeSpring = true;
																																																																																									bool flag10 = true;
																																																																																									if (array[268] > 0)
																																																																																									{
																																																																																										flag10 = false;
																																																																																									}
																																																																																									if (flag10)
																																																																																									{
																																																																																										Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 268, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																									}
																																																																																								}
																																																																																								else
																																																																																								{
																																																																																									if (this.buffType[k] == 66)
																																																																																									{
																																																																																										this.buffTime[k] = 18000;
																																																																																										this.snowman = true;
																																																																																										bool flag11 = true;
																																																																																										if (array[269] > 0)
																																																																																										{
																																																																																											flag11 = false;
																																																																																										}
																																																																																										if (flag11)
																																																																																										{
																																																																																											Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 269, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																										}
																																																																																									}
																																																																																									else
																																																																																									{
																																																																																										if (this.buffType[k] == 42)
																																																																																										{
																																																																																											this.buffTime[k] = 18000;
																																																																																											this.turtle = true;
																																																																																											bool flag12 = true;
																																																																																											if (array[127] > 0)
																																																																																											{
																																																																																												flag12 = false;
																																																																																											}
																																																																																											if (flag12)
																																																																																											{
																																																																																												Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 127, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																											}
																																																																																										}
																																																																																										else
																																																																																										{
																																																																																											if (this.buffType[k] == 45)
																																																																																											{
																																																																																												this.buffTime[k] = 18000;
																																																																																												this.eater = true;
																																																																																												bool flag13 = true;
																																																																																												if (array[175] > 0)
																																																																																												{
																																																																																													flag13 = false;
																																																																																												}
																																																																																												if (flag13)
																																																																																												{
																																																																																													Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 175, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																												}
																																																																																											}
																																																																																											else
																																																																																											{
																																																																																												if (this.buffType[k] == 50)
																																																																																												{
																																																																																													this.buffTime[k] = 18000;
																																																																																													this.skeletron = true;
																																																																																													bool flag14 = true;
																																																																																													if (array[197] > 0)
																																																																																													{
																																																																																														flag14 = false;
																																																																																													}
																																																																																													if (flag14)
																																																																																													{
																																																																																														Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 197, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																													}
																																																																																												}
																																																																																												else
																																																																																												{
																																																																																													if (this.buffType[k] == 51)
																																																																																													{
																																																																																														this.buffTime[k] = 18000;
																																																																																														this.hornet = true;
																																																																																														bool flag15 = true;
																																																																																														if (array[198] > 0)
																																																																																														{
																																																																																															flag15 = false;
																																																																																														}
																																																																																														if (flag15)
																																																																																														{
																																																																																															Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 198, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																														}
																																																																																													}
																																																																																													else
																																																																																													{
																																																																																														if (this.buffType[k] == 52)
																																																																																														{
																																																																																															this.buffTime[k] = 18000;
																																																																																															this.tiki = true;
																																																																																															bool flag16 = true;
																																																																																															if (array[199] > 0)
																																																																																															{
																																																																																																flag16 = false;
																																																																																															}
																																																																																															if (flag16)
																																																																																															{
																																																																																																Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 199, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																															}
																																																																																														}
																																																																																														else
																																																																																														{
																																																																																															if (this.buffType[k] == 53)
																																																																																															{
																																																																																																this.buffTime[k] = 18000;
																																																																																																this.lizard = true;
																																																																																																bool flag17 = true;
																																																																																																if (array[200] > 0)
																																																																																																{
																																																																																																	flag17 = false;
																																																																																																}
																																																																																																if (flag17)
																																																																																																{
																																																																																																	Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 200, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																}
																																																																																															}
																																																																																															else
																																																																																															{
																																																																																																if (this.buffType[k] == 54)
																																																																																																{
																																																																																																	this.buffTime[k] = 18000;
																																																																																																	this.parrot = true;
																																																																																																	bool flag18 = true;
																																																																																																	if (array[208] > 0)
																																																																																																	{
																																																																																																		flag18 = false;
																																																																																																	}
																																																																																																	if (flag18)
																																																																																																	{
																																																																																																		Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 208, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																	}
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	if (this.buffType[k] == 55)
																																																																																																	{
																																																																																																		this.buffTime[k] = 18000;
																																																																																																		this.truffle = true;
																																																																																																		bool flag19 = true;
																																																																																																		if (array[209] > 0)
																																																																																																		{
																																																																																																			flag19 = false;
																																																																																																		}
																																																																																																		if (flag19)
																																																																																																		{
																																																																																																			Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 209, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																		}
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		if (this.buffType[k] == 56)
																																																																																																		{
																																																																																																			this.buffTime[k] = 18000;
																																																																																																			this.sapling = true;
																																																																																																			bool flag20 = true;
																																																																																																			if (array[210] > 0)
																																																																																																			{
																																																																																																				flag20 = false;
																																																																																																			}
																																																																																																			if (flag20)
																																																																																																			{
																																																																																																				Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 210, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																			}
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			if (this.buffType[k] == 85)
																																																																																																			{
																																																																																																				this.buffTime[k] = 18000;
																																																																																																				this.cSapling = true;
																																																																																																				bool flag21 = true;
																																																																																																				if (array[324] > 0)
																																																																																																				{
																																																																																																					flag21 = false;
																																																																																																				}
																																																																																																				if (flag21)
																																																																																																				{
																																																																																																					Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 324, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																				}
																																																																																																			}
																																																																																																			else
																																																																																																			{
																																																																																																				if (this.buffType[k] == 81)
																																																																																																				{
																																																																																																					this.buffTime[k] = 18000;
																																																																																																					this.spider = true;
																																																																																																					bool flag22 = true;
																																																																																																					if (array[313] > 0)
																																																																																																					{
																																																																																																						flag22 = false;
																																																																																																					}
																																																																																																					if (flag22)
																																																																																																					{
																																																																																																						Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 313, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																					}
																																																																																																				}
																																																																																																				else
																																																																																																				{
																																																																																																					if (this.buffType[k] == 82)
																																																																																																					{
																																																																																																						this.buffTime[k] = 18000;
																																																																																																						this.squashling = true;
																																																																																																						bool flag23 = true;
																																																																																																						if (array[314] > 0)
																																																																																																						{
																																																																																																							flag23 = false;
																																																																																																						}
																																																																																																						if (flag23)
																																																																																																						{
																																																																																																							Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 314, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																						}
																																																																																																					}
																																																																																																					else
																																																																																																					{
																																																																																																						if (this.buffType[k] == 57)
																																																																																																						{
																																																																																																							this.buffTime[k] = 18000;
																																																																																																							this.wisp = true;
																																																																																																							bool flag24 = true;
																																																																																																							if (array[211] > 0)
																																																																																																							{
																																																																																																								flag24 = false;
																																																																																																							}
																																																																																																							if (flag24)
																																																																																																							{
																																																																																																								Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 211, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																							}
																																																																																																						}
																																																																																																						else
																																																																																																						{
																																																																																																							if (this.buffType[k] == 60)
																																																																																																							{
																																																																																																								this.buffTime[k] = 18000;
																																																																																																								this.crystalLeaf = true;
																																																																																																								bool flag25 = true;
																																																																																																								for (int num9 = 0; num9 < 1000; num9++)
																																																																																																								{
																																																																																																									if (Main.projectile[num9].active && Main.projectile[num9].owner == this.whoAmi && Main.projectile[num9].type == 226)
																																																																																																									{
																																																																																																										if (!flag25)
																																																																																																										{
																																																																																																											Main.projectile[num9].Kill();
																																																																																																										}
																																																																																																										flag25 = false;
																																																																																																									}
																																																																																																								}
																																																																																																								if (flag25)
																																																																																																								{
																																																																																																									Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 226, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																								}
																																																																																																							}
																																																																																																							else
																																																																																																							{
																																																																																																								if (this.buffType[k] == 127)
																																																																																																								{
																																																																																																									this.buffTime[k] = 18000;
																																																																																																									this.zephyrfish = true;
																																																																																																									bool flag26 = true;
																																																																																																									if (array[380] > 0)
																																																																																																									{
																																																																																																										flag26 = false;
																																																																																																									}
																																																																																																									if (flag26)
																																																																																																									{
																																																																																																										Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 380, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																									}
																																																																																																								}
																																																																																																								else
																																																																																																								{
																																																																																																									if (this.buffType[k] == 136)
																																																																																																									{
																																																																																																										this.buffTime[k] = 18000;
																																																																																																										this.miniMinotaur = true;
																																																																																																										bool flag27 = true;
																																																																																																										if (array[398] > 0)
																																																																																																										{
																																																																																																											flag27 = false;
																																																																																																										}
																																																																																																										if (flag27)
																																																																																																										{
																																																																																																											Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 398, 0, 0f, this.whoAmi, 0f, 0f);
																																																																																																										}
																																																																																																									}
																																																																																																									else
																																																																																																									{
																																																																																																										if (this.buffType[k] == 70)
																																																																																																										{
																																																																																																											this.venom = true;
																																																																																																										}
																																																																																																										else
																																																																																																										{
																																																																																																											if (this.buffType[k] == 20)
																																																																																																											{
																																																																																																												this.poisoned = true;
																																																																																																											}
																																																																																																											else
																																																																																																											{
																																																																																																												if (this.buffType[k] == 21)
																																																																																																												{
																																																																																																													this.potionDelay = this.buffTime[k];
																																																																																																												}
																																																																																																												else
																																																																																																												{
																																																																																																													if (this.buffType[k] == 22)
																																																																																																													{
																																																																																																														this.blind = true;
																																																																																																													}
																																																																																																													else
																																																																																																													{
																																																																																																														if (this.buffType[k] == 80)
																																																																																																														{
																																																																																																															this.blackout = true;
																																																																																																														}
																																																																																																														else
																																																																																																														{
																																																																																																															if (this.buffType[k] == 23)
																																																																																																															{
																																																																																																																this.noItems = true;
																																																																																																															}
																																																																																																															else
																																																																																																															{
																																																																																																																if (this.buffType[k] == 24)
																																																																																																																{
																																																																																																																	this.onFire = true;
																																																																																																																}
																																																																																																																else
																																																																																																																{
																																																																																																																	if (this.buffType[k] == 103)
																																																																																																																	{
																																																																																																																		this.dripping = true;
																																																																																																																	}
																																																																																																																	else
																																																																																																																	{
																																																																																																																		if (this.buffType[k] == 137)
																																																																																																																		{
																																																																																																																			this.drippingSlime = true;
																																																																																																																		}
																																																																																																																		else
																																																																																																																		{
																																																																																																																			if (this.buffType[k] == 67)
																																																																																																																			{
																																																																																																																				this.burned = true;
																																																																																																																			}
																																																																																																																			else
																																																																																																																			{
																																																																																																																				if (this.buffType[k] == 68)
																																																																																																																				{
																																																																																																																					this.suffocating = true;
																																																																																																																				}
																																																																																																																				else
																																																																																																																				{
																																																																																																																					if (this.buffType[k] == 39)
																																																																																																																					{
																																																																																																																						this.onFire2 = true;
																																																																																																																					}
																																																																																																																					else
																																																																																																																					{
																																																																																																																						if (this.buffType[k] == 44)
																																																																																																																						{
																																																																																																																							this.onFrostBurn = true;
																																																																																																																						}
																																																																																																																						else
																																																																																																																						{
																																																																																																																							if (this.buffType[k] == 43)
																																																																																																																							{
																																																																																																																								this.paladinBuff = true;
																																																																																																																							}
																																																																																																																							else
																																																																																																																							{
																																																																																																																								if (this.buffType[k] == 29)
																																																																																																																								{
																																																																																																																									this.magicCrit += 2;
																																																																																																																									this.magicDamage += 0.05f;
																																																																																																																									this.statManaMax2 += 20;
																																																																																																																									this.manaCost -= 0.02f;
																																																																																																																								}
																																																																																																																								else
																																																																																																																								{
																																																																																																																									if (this.buffType[k] == 28)
																																																																																																																									{
																																																																																																																										if (!Main.dayTime && this.wolfAcc && !this.merman)
																																																																																																																										{
																																																																																																																											this.lifeRegen++;
																																																																																																																											this.wereWolf = true;
																																																																																																																											this.meleeCrit += 2;
																																																																																																																											this.meleeDamage += 0.051f;
																																																																																																																											this.meleeSpeed += 0.051f;
																																																																																																																											this.statDefense += 3;
																																																																																																																											this.moveSpeed += 0.05f;
																																																																																																																										}
																																																																																																																										else
																																																																																																																										{
																																																																																																																											this.DelBuff(k);
																																																																																																																											k--;
																																																																																																																										}
																																																																																																																									}
																																																																																																																									else
																																																																																																																									{
																																																																																																																										if (this.buffType[k] == 33)
																																																																																																																										{
																																																																																																																											this.meleeDamage -= 0.051f;
																																																																																																																											this.meleeSpeed -= 0.051f;
																																																																																																																											this.statDefense -= 4;
																																																																																																																											this.moveSpeed -= 0.1f;
																																																																																																																										}
																																																																																																																										else
																																																																																																																										{
																																																																																																																											if (this.buffType[k] == 25)
																																																																																																																											{
																																																																																																																												this.statDefense -= 4;
																																																																																																																												this.meleeCrit += 2;
																																																																																																																												this.meleeDamage += 0.1f;
																																																																																																																												this.meleeSpeed += 0.1f;
																																																																																																																											}
																																																																																																																											else
																																																																																																																											{
																																																																																																																												if (this.buffType[k] == 26)
																																																																																																																												{
																																																																																																																													this.statDefense += 2;
																																																																																																																													this.meleeCrit += 2;
																																																																																																																													this.meleeDamage += 0.05f;
																																																																																																																													this.meleeSpeed += 0.05f;
																																																																																																																													this.magicCrit += 2;
																																																																																																																													this.magicDamage += 0.05f;
																																																																																																																													this.rangedCrit += 2;
																																																																																																																													this.rangedDamage += 0.05f;
																																																																																																																													this.minionDamage += 0.05f;
																																																																																																																													this.minionKB += 0.5f;
																																																																																																																													this.moveSpeed += 0.2f;
																																																																																																																												}
																																																																																																																												else
																																																																																																																												{
																																																																																																																													if (this.buffType[k] == 71)
																																																																																																																													{
																																																																																																																														this.meleeEnchant = 1;
																																																																																																																													}
																																																																																																																													else
																																																																																																																													{
																																																																																																																														if (this.buffType[k] == 73)
																																																																																																																														{
																																																																																																																															this.meleeEnchant = 2;
																																																																																																																														}
																																																																																																																														else
																																																																																																																														{
																																																																																																																															if (this.buffType[k] == 74)
																																																																																																																															{
																																																																																																																																this.meleeEnchant = 3;
																																																																																																																															}
																																																																																																																															else
																																																																																																																															{
																																																																																																																																if (this.buffType[k] == 75)
																																																																																																																																{
																																																																																																																																	this.meleeEnchant = 4;
																																																																																																																																}
																																																																																																																																else
																																																																																																																																{
																																																																																																																																	if (this.buffType[k] == 76)
																																																																																																																																	{
																																																																																																																																		this.meleeEnchant = 5;
																																																																																																																																	}
																																																																																																																																	else
																																																																																																																																	{
																																																																																																																																		if (this.buffType[k] == 77)
																																																																																																																																		{
																																																																																																																																			this.meleeEnchant = 6;
																																																																																																																																		}
																																																																																																																																		else
																																																																																																																																		{
																																																																																																																																			if (this.buffType[k] == 78)
																																																																																																																																			{
																																																																																																																																				this.meleeEnchant = 7;
																																																																																																																																			}
																																																																																																																																			else
																																																																																																																																			{
																																																																																																																																				if (this.buffType[k] == 79)
																																																																																																																																				{
																																																																																																																																					this.meleeEnchant = 8;
																																																																																																																																				}
																																																																																																																																			}
																																																																																																																																		}
																																																																																																																																	}
																																																																																																																																}
																																																																																																																															}
																																																																																																																														}
																																																																																																																													}
																																																																																																																												}
																																																																																																																											}
																																																																																																																										}
																																																																																																																									}
																																																																																																																								}
																																																																																																																							}
																																																																																																																						}
																																																																																																																					}
																																																																																																																				}
																																																																																																																			}
																																																																																																																		}
																																																																																																																	}
																																																																																																																}
																																																																																																															}
																																																																																																														}
																																																																																																													}
																																																																																																												}
																																																																																																											}
																																																																																																										}
																																																																																																									}
																																																																																																								}
																																																																																																							}
																																																																																																						}
																																																																																																					}
																																																																																																				}
																																																																																																			}
																																																																																																		}
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																														}
																																																																																													}
																																																																																												}
																																																																																											}
																																																																																										}
																																																																																									}
																																																																																								}
																																																																																							}
																																																																																						}
																																																																																					}
																																																																																				}
																																																																																			}
																																																																																		}
																																																																																	}
																																																																																}
																																																																															}
																																																																														}
																																																																													}
																																																																												}
																																																																											}
																																																																										}
																																																																									}
																																																																								}
																																																																							}
																																																																						}
																																																																					}
																																																																				}
																																																																			}
																																																																		}
																																																																	}
																																																																}
																																																															}
																																																														}
																																																													}
																																																												}
																																																											}
																																																										}
																																																									}
																																																								}
																																																							}
																																																						}
																																																					}
																																																				}
																																																			}
																																																		}
																																																	}
																																																}
																																															}
																																														}
																																													}
																																												}
																																											}
																																										}
																																									}
																																								}
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		public void UpdateEquips(int i)
		{
			for (int j = 0; j < 8; j++)
			{
				this.statDefense += this.armor[j].defense;
				this.lifeRegen += this.armor[j].lifeRegen;
				if (this.armor[j].type == 268)
				{
					this.accDivingHelm = true;
				}
				if (this.armor[j].type == 238)
				{
					this.magicDamage += 0.15f;
				}
				if (this.armor[j].type == 2277)
				{
					this.magicDamage += 0.05f;
					this.meleeDamage += 0.05f;
					this.rangedDamage += 0.05f;
					this.magicCrit += 5;
					this.rangedCrit += 5;
					this.meleeCrit += 5;
					this.meleeSpeed += 0.1f;
					this.moveSpeed += 0.1f;
				}
				if (this.armor[j].type == 2279)
				{
					this.magicDamage += 0.06f;
					this.magicCrit += 6;
					this.manaCost -= 0.1f;
				}
				if (this.armor[j].type == 2275)
				{
					this.magicDamage += 0.07f;
					this.magicCrit += 7;
				}
				if (this.armor[j].type == 123 || this.armor[j].type == 124 || this.armor[j].type == 125)
				{
					this.magicDamage += 0.07f;
				}
				if (this.armor[j].type == 151 || this.armor[j].type == 152 || this.armor[j].type == 153 || this.armor[j].type == 959)
				{
					this.rangedDamage += 0.05f;
				}
				if (this.armor[j].type == 111 || this.armor[j].type == 228 || this.armor[j].type == 229 || this.armor[j].type == 230 || this.armor[j].type == 960 || this.armor[j].type == 961 || this.armor[j].type == 962)
				{
					this.statManaMax2 += 20;
				}
				if (this.armor[j].type == 228 || this.armor[j].type == 229 || this.armor[j].type == 230 || this.armor[j].type == 960 || this.armor[j].type == 961 || this.armor[j].type == 962)
				{
					this.magicCrit += 3;
				}
				if (this.armor[j].type == 100 || this.armor[j].type == 101 || this.armor[j].type == 102)
				{
					this.meleeSpeed += 0.07f;
				}
				if (this.armor[j].type == 956 || this.armor[j].type == 957 || this.armor[j].type == 958)
				{
					this.meleeSpeed += 0.07f;
				}
				if (this.armor[j].type == 791 || this.armor[j].type == 792 || this.armor[j].type == 793)
				{
					this.meleeDamage += 0.02f;
					this.rangedDamage += 0.02f;
					this.magicDamage += 0.02f;
				}
				if (this.armor[j].type == 371)
				{
					this.magicCrit += 9;
					this.statManaMax2 += 40;
				}
				if (this.armor[j].type == 372)
				{
					this.moveSpeed += 0.07f;
					this.meleeSpeed += 0.12f;
				}
				if (this.armor[j].type == 373)
				{
					this.rangedDamage += 0.1f;
					this.rangedCrit += 6;
				}
				if (this.armor[j].type == 374)
				{
					this.magicCrit += 3;
					this.meleeCrit += 3;
					this.rangedCrit += 3;
				}
				if (this.armor[j].type == 375)
				{
					this.moveSpeed += 0.1f;
				}
				if (this.armor[j].type == 376)
				{
					this.magicDamage += 0.15f;
					this.statManaMax2 += 60;
				}
				if (this.armor[j].type == 377)
				{
					this.meleeCrit += 5;
					this.meleeDamage += 0.1f;
				}
				if (this.armor[j].type == 378)
				{
					this.rangedDamage += 0.12f;
					this.rangedCrit += 7;
				}
				if (this.armor[j].type == 379)
				{
					this.rangedDamage += 0.05f;
					this.meleeDamage += 0.05f;
					this.magicDamage += 0.05f;
				}
				if (this.armor[j].type == 380)
				{
					this.magicCrit += 3;
					this.meleeCrit += 3;
					this.rangedCrit += 3;
				}
				if (this.armor[j].type >= 2367 && this.armor[j].type <= 2369)
				{
					this.fishingSkill += 5;
				}
				if (this.armor[j].type == 400)
				{
					this.magicDamage += 0.11f;
					this.magicCrit += 11;
					this.statManaMax2 += 80;
				}
				if (this.armor[j].type == 401)
				{
					this.meleeCrit += 7;
					this.meleeDamage += 0.14f;
				}
				if (this.armor[j].type == 402)
				{
					this.rangedDamage += 0.14f;
					this.rangedCrit += 8;
				}
				if (this.armor[j].type == 403)
				{
					this.rangedDamage += 0.06f;
					this.meleeDamage += 0.06f;
					this.magicDamage += 0.06f;
				}
				if (this.armor[j].type == 404)
				{
					this.magicCrit += 4;
					this.meleeCrit += 4;
					this.rangedCrit += 4;
					this.moveSpeed += 0.05f;
				}
				if (this.armor[j].type == 1205)
				{
					this.meleeDamage += 0.08f;
					this.meleeSpeed += 0.12f;
				}
				if (this.armor[j].type == 1206)
				{
					this.rangedDamage += 0.09f;
					this.rangedCrit += 9;
				}
				if (this.armor[j].type == 1207)
				{
					this.magicDamage += 0.07f;
					this.magicCrit += 7;
					this.statManaMax2 += 60;
				}
				if (this.armor[j].type == 1208)
				{
					this.meleeDamage += 0.03f;
					this.rangedDamage += 0.03f;
					this.magicDamage += 0.03f;
					this.magicCrit += 2;
					this.meleeCrit += 2;
					this.rangedCrit += 2;
				}
				if (this.armor[j].type == 1209)
				{
					this.meleeDamage += 0.02f;
					this.rangedDamage += 0.02f;
					this.magicDamage += 0.02f;
					this.magicCrit++;
					this.meleeCrit++;
					this.rangedCrit++;
				}
				if (this.armor[j].type == 1210)
				{
					this.meleeDamage += 0.07f;
					this.meleeSpeed += 0.07f;
					this.moveSpeed += 0.07f;
				}
				if (this.armor[j].type == 1211)
				{
					this.rangedCrit += 15;
					this.moveSpeed += 0.08f;
				}
				if (this.armor[j].type == 1212)
				{
					this.magicCrit += 18;
					this.statManaMax2 += 80;
				}
				if (this.armor[j].type == 1213)
				{
					this.magicCrit += 6;
					this.meleeCrit += 6;
					this.rangedCrit += 6;
				}
				if (this.armor[j].type == 1214)
				{
					this.moveSpeed += 0.11f;
				}
				if (this.armor[j].type == 1215)
				{
					this.meleeDamage += 0.08f;
					this.meleeCrit += 8;
					this.meleeSpeed += 0.08f;
				}
				if (this.armor[j].type == 1216)
				{
					this.rangedDamage += 0.16f;
					this.rangedCrit += 7;
				}
				if (this.armor[j].type == 1217)
				{
					this.magicDamage += 0.16f;
					this.magicCrit += 7;
					this.statManaMax2 += 100;
				}
				if (this.armor[j].type == 1218)
				{
					this.meleeDamage += 0.04f;
					this.rangedDamage += 0.04f;
					this.magicDamage += 0.04f;
					this.magicCrit += 3;
					this.meleeCrit += 3;
					this.rangedCrit += 3;
				}
				if (this.armor[j].type == 1219)
				{
					this.meleeDamage += 0.03f;
					this.rangedDamage += 0.03f;
					this.magicDamage += 0.03f;
					this.magicCrit += 3;
					this.meleeCrit += 3;
					this.rangedCrit += 3;
					this.moveSpeed += 0.06f;
				}
				if (this.armor[j].type == 558)
				{
					this.magicDamage += 0.12f;
					this.magicCrit += 12;
					this.statManaMax2 += 100;
				}
				if (this.armor[j].type == 559)
				{
					this.meleeCrit += 10;
					this.meleeDamage += 0.1f;
					this.meleeSpeed += 0.1f;
				}
				if (this.armor[j].type == 553)
				{
					this.rangedDamage += 0.15f;
					this.rangedCrit += 8;
				}
				if (this.armor[j].type == 551)
				{
					this.magicCrit += 7;
					this.meleeCrit += 7;
					this.rangedCrit += 7;
				}
				if (this.armor[j].type == 552)
				{
					this.rangedDamage += 0.07f;
					this.meleeDamage += 0.07f;
					this.magicDamage += 0.07f;
					this.moveSpeed += 0.08f;
				}
				if (this.armor[j].type == 1001)
				{
					this.meleeDamage += 0.16f;
					this.meleeCrit += 6;
				}
				if (this.armor[j].type == 1002)
				{
					this.rangedDamage += 0.16f;
					this.ammoCost80 = true;
				}
				if (this.armor[j].type == 1003)
				{
					this.statManaMax2 += 80;
					this.manaCost -= 0.17f;
					this.magicDamage += 0.16f;
				}
				if (this.armor[j].type == 1004)
				{
					this.meleeDamage += 0.05f;
					this.magicDamage += 0.05f;
					this.rangedDamage += 0.05f;
					this.magicCrit += 7;
					this.meleeCrit += 7;
					this.rangedCrit += 7;
				}
				if (this.armor[j].type == 1005)
				{
					this.magicCrit += 8;
					this.meleeCrit += 8;
					this.rangedCrit += 8;
					this.moveSpeed += 0.05f;
				}
				if (this.armor[j].type == 2189)
				{
					this.statManaMax2 += 60;
					this.manaCost -= 0.13f;
					this.magicDamage += 0.05f;
					this.magicCrit += 5;
				}
				if (this.armor[j].type == 1503)
				{
					this.magicDamage -= 0.4f;
				}
				if (this.armor[j].type == 1504)
				{
					this.magicDamage += 0.07f;
					this.magicCrit += 7;
				}
				if (this.armor[j].type == 1505)
				{
					this.magicDamage += 0.08f;
					this.moveSpeed += 0.08f;
				}
				if (this.armor[j].type == 1546)
				{
					this.rangedCrit += 5;
					this.arrowDamage += 0.15f;
				}
				if (this.armor[j].type == 1547)
				{
					this.rangedCrit += 5;
					this.bulletDamage += 0.15f;
				}
				if (this.armor[j].type == 1548)
				{
					this.rangedCrit += 5;
					this.rocketDamage += 0.15f;
				}
				if (this.armor[j].type == 1549)
				{
					this.rangedCrit += 13;
					this.rangedDamage += 0.13f;
					this.ammoCost80 = true;
				}
				if (this.armor[j].type == 1550)
				{
					this.rangedCrit += 7;
					this.moveSpeed += 0.12f;
				}
				if (this.armor[j].type == 1282)
				{
					this.statManaMax2 += 20;
					this.manaCost -= 0.05f;
				}
				if (this.armor[j].type == 1283)
				{
					this.statManaMax2 += 40;
					this.manaCost -= 0.07f;
				}
				if (this.armor[j].type == 1284)
				{
					this.statManaMax2 += 40;
					this.manaCost -= 0.09f;
				}
				if (this.armor[j].type == 1285)
				{
					this.statManaMax2 += 60;
					this.manaCost -= 0.11f;
				}
				if (this.armor[j].type == 1286)
				{
					this.statManaMax2 += 60;
					this.manaCost -= 0.13f;
				}
				if (this.armor[j].type == 1287)
				{
					this.statManaMax2 += 80;
					this.manaCost -= 0.15f;
				}
				if (this.armor[j].type == 1316 || this.armor[j].type == 1317 || this.armor[j].type == 1318)
				{
					this.aggro += 250;
				}
				if (this.armor[j].type == 1316)
				{
					this.meleeDamage += 0.06f;
				}
				if (this.armor[j].type == 1317)
				{
					this.meleeDamage += 0.08f;
					this.meleeCrit += 8;
				}
				if (this.armor[j].type == 1318)
				{
					this.meleeCrit += 4;
				}
				if (this.armor[j].type == 2199 || this.armor[j].type == 2202)
				{
					this.aggro += 250;
				}
				if (this.armor[j].type == 2201)
				{
					this.aggro += 400;
				}
				if (this.armor[j].type == 2199)
				{
					this.meleeDamage += 0.06f;
				}
				if (this.armor[j].type == 2200)
				{
					this.meleeDamage += 0.08f;
					this.meleeCrit += 8;
					this.meleeSpeed += 0.06f;
					this.moveSpeed += 0.06f;
				}
				if (this.armor[j].type == 2201)
				{
					this.meleeDamage += 0.05f;
					this.meleeCrit += 5;
				}
				if (this.armor[j].type == 2202)
				{
					this.meleeSpeed += 0.06f;
					this.moveSpeed += 0.06f;
				}
				if (this.armor[j].type == 684)
				{
					this.rangedDamage += 0.16f;
					this.meleeDamage += 0.16f;
				}
				if (this.armor[j].type == 685)
				{
					this.meleeCrit += 11;
					this.rangedCrit += 11;
				}
				if (this.armor[j].type == 686)
				{
					this.moveSpeed += 0.08f;
					this.meleeSpeed += 0.07f;
				}
				if (this.armor[j].type == 2361)
				{
					this.maxMinions++;
					this.minionDamage += 0.04f;
				}
				if (this.armor[j].type == 2362)
				{
					this.maxMinions++;
					this.minionDamage += 0.04f;
				}
				if (this.armor[j].type == 2363)
				{
					this.minionDamage += 0.05f;
				}
				if (this.armor[j].type >= 1158 && this.armor[j].type <= 1161)
				{
					this.maxMinions++;
				}
				if (this.armor[j].type >= 1159 && this.armor[j].type <= 1161)
				{
					this.minionDamage += 0.1f;
				}
				if (this.armor[j].type >= 2370 && this.armor[j].type <= 2371)
				{
					this.minionDamage += 0.05f;
					this.maxMinions++;
				}
				if (this.armor[j].type == 2372)
				{
					this.minionDamage += 0.06f;
					this.maxMinions++;
				}
				if (this.armor[j].type >= 1832 && this.armor[j].type <= 1834)
				{
					this.maxMinions++;
				}
				if (this.armor[j].type >= 1832 && this.armor[j].type <= 1834)
				{
					this.minionDamage += 0.11f;
				}
				if (this.armor[j].prefix == 62)
				{
					this.statDefense++;
				}
				if (this.armor[j].prefix == 63)
				{
					this.statDefense += 2;
				}
				if (this.armor[j].prefix == 64)
				{
					this.statDefense += 3;
				}
				if (this.armor[j].prefix == 65)
				{
					this.statDefense += 4;
				}
				if (this.armor[j].prefix == 66)
				{
					this.statManaMax2 += 20;
				}
				if (this.armor[j].prefix == 67)
				{
					this.meleeCrit += 2;
					this.rangedCrit += 2;
					this.magicCrit += 2;
				}
				if (this.armor[j].prefix == 68)
				{
					this.meleeCrit += 4;
					this.rangedCrit += 4;
					this.magicCrit += 4;
				}
				if (this.armor[j].prefix == 69)
				{
					this.meleeDamage += 0.01f;
					this.rangedDamage += 0.01f;
					this.magicDamage += 0.01f;
					this.minionDamage += 0.01f;
				}
				if (this.armor[j].prefix == 70)
				{
					this.meleeDamage += 0.02f;
					this.rangedDamage += 0.02f;
					this.magicDamage += 0.02f;
					this.minionDamage += 0.02f;
				}
				if (this.armor[j].prefix == 71)
				{
					this.meleeDamage += 0.03f;
					this.rangedDamage += 0.03f;
					this.magicDamage += 0.03f;
					this.minionDamage += 0.03f;
				}
				if (this.armor[j].prefix == 72)
				{
					this.meleeDamage += 0.04f;
					this.rangedDamage += 0.04f;
					this.magicDamage += 0.04f;
					this.minionDamage += 0.04f;
				}
				if (this.armor[j].prefix == 73)
				{
					this.moveSpeed += 0.01f;
				}
				if (this.armor[j].prefix == 74)
				{
					this.moveSpeed += 0.02f;
				}
				if (this.armor[j].prefix == 75)
				{
					this.moveSpeed += 0.03f;
				}
				if (this.armor[j].prefix == 76)
				{
					this.moveSpeed += 0.04f;
				}
				if (this.armor[j].prefix == 77)
				{
					this.meleeSpeed += 0.01f;
				}
				if (this.armor[j].prefix == 78)
				{
					this.meleeSpeed += 0.02f;
				}
				if (this.armor[j].prefix == 79)
				{
					this.meleeSpeed += 0.03f;
				}
				if (this.armor[j].prefix == 80)
				{
					this.meleeSpeed += 0.04f;
				}
			}
			for (int k = 3; k < 8; k++)
			{
				if (this.armor[k].type == 2373)
				{
					this.accFishingLine = true;
				}
				if (this.armor[k].type == 2374)
				{
					this.fishingSkill += 10;
				}
				if (this.armor[k].type == 2375)
				{
					this.accTackleBox = true;
				}
				if (this.armor[k].type == 2423)
				{
					this.autoJump = true;
					this.jumpSpeedBoost += 2.4f;
					this.extraFall += 15;
				}
				if (this.armor[k].type == 15 && this.accWatch < 1)
				{
					this.accWatch = 1;
				}
				if (this.armor[k].type == 16 && this.accWatch < 2)
				{
					this.accWatch = 2;
				}
				if (this.armor[k].type == 17 && this.accWatch < 3)
				{
					this.accWatch = 3;
				}
				if (this.armor[k].type == 707 && this.accWatch < 1)
				{
					this.accWatch = 1;
				}
				if (this.armor[k].type == 708 && this.accWatch < 2)
				{
					this.accWatch = 2;
				}
				if (this.armor[k].type == 709 && this.accWatch < 3)
				{
					this.accWatch = 3;
				}
				if (this.armor[k].type == 18 && this.accDepthMeter < 1)
				{
					this.accDepthMeter = 1;
				}
				if (this.armor[k].type == 857)
				{
					this.doubleJump2 = true;
				}
				if (this.armor[k].type == 983)
				{
					this.doubleJump2 = true;
					this.jumpBoost = true;
				}
				if (this.armor[k].type == 987)
				{
					this.doubleJump3 = true;
				}
				if (this.armor[k].type == 1163)
				{
					this.doubleJump3 = true;
					this.jumpBoost = true;
				}
				if (this.armor[k].type == 1724)
				{
					this.doubleJump4 = true;
				}
				if (this.armor[k].type == 1863)
				{
					this.doubleJump4 = true;
					this.jumpBoost = true;
				}
				if (this.armor[k].type == 1164)
				{
					this.doubleJump = true;
					this.doubleJump2 = true;
					this.doubleJump3 = true;
					this.jumpBoost = true;
				}
				if (this.armor[k].type == 1250)
				{
					this.jumpBoost = true;
					this.doubleJump = true;
					this.noFallDmg = true;
				}
				if (this.armor[k].type == 1252)
				{
					this.doubleJump2 = true;
					this.jumpBoost = true;
					this.noFallDmg = true;
				}
				if (this.armor[k].type == 1251)
				{
					this.doubleJump3 = true;
					this.jumpBoost = true;
					this.noFallDmg = true;
				}
				if (this.armor[k].type == 1249)
				{
					this.jumpBoost = true;
					this.bee = true;
				}
				if (this.armor[k].type == 1253 && (double)this.statLife <= (double)this.statLifeMax2 * 0.25)
				{
					this.AddBuff(62, 5, true);
				}
				if (this.armor[k].type == 1290)
				{
					this.panic = true;
				}
				if ((this.armor[k].type == 1300 || this.armor[k].type == 1858) && (this.inventory[this.selectedItem].useAmmo == 14 || this.inventory[this.selectedItem].useAmmo == 311 || this.inventory[this.selectedItem].useAmmo == 323))
				{
					this.scope = true;
				}
				if (this.armor[k].type == 1858)
				{
					this.rangedCrit += 10;
					this.rangedDamage += 0.1f;
				}
				if (this.armor[k].type == 1303 && this.wet)
				{
					Lighting.addLight((int)this.center().X / 16, (int)this.center().Y / 16, 0.9f, 0.2f, 0.6f);
				}
				if (this.armor[k].type == 1301)
				{
					this.meleeCrit += 8;
					this.rangedCrit += 8;
					this.magicCrit += 8;
					this.meleeDamage += 0.1f;
					this.rangedDamage += 0.1f;
					this.magicDamage += 0.1f;
					this.minionDamage += 0.1f;
				}
				if (this.armor[k].type == 982)
				{
					this.statManaMax2 += 20;
					this.manaRegenDelayBonus++;
					this.manaRegenBonus += 25;
				}
				if (this.armor[k].type == 1595)
				{
					this.statManaMax2 += 20;
					this.magicCuffs = true;
				}
				if (this.armor[k].type == 2219)
				{
					this.manaMagnet = true;
				}
				if (this.armor[k].type == 2220)
				{
					this.manaMagnet = true;
					this.magicDamage += 0.15f;
				}
				if (this.armor[k].type == 2221)
				{
					this.manaMagnet = true;
					this.magicCuffs = true;
				}
				if (this.whoAmi == Main.myPlayer && this.armor[k].type == 1923)
				{
					Player.tileRangeX++;
					Player.tileRangeY++;
				}
				if (this.armor[k].type == 1247)
				{
					this.starCloak = true;
					this.bee = true;
				}
				if (this.armor[k].type == 1248)
				{
					this.meleeCrit += 10;
					this.rangedCrit += 10;
					this.magicCrit += 10;
				}
				if (this.armor[k].type == 854)
				{
					this.discount = true;
				}
				if (this.armor[k].type == 855)
				{
					this.coins = true;
				}
				if (this.armor[k].type == 53)
				{
					this.doubleJump = true;
				}
				if (this.armor[k].type == 54)
				{
					this.accRunSpeed = 6f;
				}
				if (this.armor[k].type == 1579)
				{
					this.accRunSpeed = 6f;
					this.coldDash = true;
				}
				if (this.armor[k].type == 128)
				{
					this.rocketBoots = 1;
				}
				if (this.armor[k].type == 156)
				{
					this.noKnockback = true;
				}
				if (this.armor[k].type == 158)
				{
					this.noFallDmg = true;
				}
				if (this.armor[k].type == 934)
				{
					this.carpet = true;
				}
				if (this.armor[k].type == 953)
				{
					this.spikedBoots++;
				}
				if (this.armor[k].type == 975)
				{
					this.spikedBoots++;
				}
				if (this.armor[k].type == 976)
				{
					this.spikedBoots += 2;
				}
				if (this.armor[k].type == 977)
				{
					this.dash = 1;
				}
				if (this.armor[k].type == 963)
				{
					this.blackBelt = true;
				}
				if (this.armor[k].type == 984)
				{
					this.blackBelt = true;
					this.dash = 1;
					this.spikedBoots = 2;
				}
				if (this.armor[k].type == 1131)
				{
					this.gravControl2 = true;
				}
				if (this.armor[k].type == 1132)
				{
					this.bee = true;
				}
				if (this.armor[k].type == 1578)
				{
					this.bee = true;
					this.panic = true;
				}
				if (this.armor[k].type == 950)
				{
					this.iceSkate = true;
				}
				if (this.armor[k].type == 159)
				{
					this.jumpBoost = true;
				}
				if (this.armor[k].type == 187)
				{
					this.accFlipper = true;
				}
				if (this.armor[k].type == 211)
				{
					this.meleeSpeed += 0.12f;
				}
				if (this.armor[k].type == 223)
				{
					this.manaCost -= 0.06f;
				}
				if (this.armor[k].type == 285)
				{
					this.moveSpeed += 0.05f;
				}
				if (this.armor[k].type == 212)
				{
					this.moveSpeed += 0.1f;
				}
				if (this.armor[k].type == 267)
				{
					this.killGuide = true;
				}
				if (this.armor[k].type == 1307)
				{
					this.killClothier = true;
				}
				if (this.armor[k].type == 193)
				{
					this.fireWalk = true;
				}
				if (this.armor[k].type == 861)
				{
					this.accMerman = true;
					this.wolfAcc = true;
				}
				if (this.armor[k].type == 862)
				{
					this.starCloak = true;
					this.longInvince = true;
				}
				if (this.armor[k].type == 860)
				{
					this.pStone = true;
				}
				if (this.armor[k].type == 863)
				{
					this.waterWalk2 = true;
				}
				if (this.armor[k].type == 907)
				{
					this.waterWalk2 = true;
					this.fireWalk = true;
				}
				if (this.armor[k].type == 908)
				{
					this.waterWalk = true;
					this.fireWalk = true;
					this.lavaMax += 420;
				}
				if (this.armor[k].type == 906)
				{
					this.lavaMax += 420;
				}
				if (this.armor[k].type == 485)
				{
					this.wolfAcc = true;
				}
				if (this.armor[k].type == 486)
				{
					this.rulerAcc = true;
				}
				if (this.armor[k].type == 393)
				{
					this.accCompass = 1;
				}
				if (this.armor[k].type == 394)
				{
					this.accFlipper = true;
					this.accDivingHelm = true;
				}
				if (this.armor[k].type == 395)
				{
					this.accWatch = 3;
					this.accDepthMeter = 1;
					this.accCompass = 1;
				}
				if (this.armor[k].type == 396)
				{
					this.noFallDmg = true;
					this.fireWalk = true;
				}
				if (this.armor[k].type == 397)
				{
					this.noKnockback = true;
					this.fireWalk = true;
				}
				if (this.armor[k].type == 399)
				{
					this.jumpBoost = true;
					this.doubleJump = true;
				}
				if (this.armor[k].type == 405)
				{
					this.accRunSpeed = 6f;
					this.rocketBoots = 2;
				}
				if (this.armor[k].type == 1860)
				{
					this.accFlipper = true;
					this.accDivingHelm = true;
					if (this.wet)
					{
						Lighting.addLight((int)this.center().X / 16, (int)this.center().Y / 16, 0.9f, 0.2f, 0.6f);
					}
				}
				if (this.armor[k].type == 1861)
				{
					this.accFlipper = true;
					this.accDivingHelm = true;
					this.iceSkate = true;
					if (this.wet)
					{
						Lighting.addLight((int)this.center().X / 16, (int)this.center().Y / 16, 0.2f, 0.8f, 0.9f);
					}
				}
				if (this.armor[k].type == 2214)
				{
					this.tileSpeed += 0.5f;
				}
				if (this.whoAmi == Main.myPlayer && this.armor[k].type == 2215)
				{
					Player.tileRangeX += 3;
					Player.tileRangeY += 2;
				}
				if (this.armor[k].type == 2216)
				{
					this.autoPaint = true;
				}
				if (this.armor[k].type == 2217)
				{
					this.wallSpeed += 0.5f;
				}
				if (this.armor[k].type == 897)
				{
					this.kbGlove = true;
					this.meleeSpeed += 0.12f;
				}
				if (this.armor[k].type == 1343)
				{
					this.kbGlove = true;
					this.meleeSpeed += 0.09f;
					this.meleeDamage += 0.09f;
					this.magmaStone = true;
				}
				if (this.armor[k].type == 1167)
				{
					this.minionKB += 2f;
					this.minionDamage += 0.15f;
				}
				if (this.armor[k].type == 1864)
				{
					this.minionKB += 2f;
					this.minionDamage += 0.15f;
					this.maxMinions++;
				}
				if (this.armor[k].type == 1845)
				{
					this.minionDamage += 0.1f;
					this.maxMinions++;
				}
				if (this.armor[k].type == 1321)
				{
					this.magicQuiver = true;
					this.arrowDamage += 0.1f;
				}
				if (this.armor[k].type == 1322)
				{
					this.magmaStone = true;
				}
				if (this.armor[k].type == 1323)
				{
					this.lavaRose = true;
				}
				if (this.armor[k].type == 938)
				{
					this.noKnockback = true;
					if ((double)this.statLife > (double)this.statLifeMax2 * 0.25)
					{
						if (i == Main.myPlayer)
						{
							this.paladinGive = true;
						}
						else
						{
							if (this.miscCounter % 5 == 0)
							{
								int myPlayer = Main.myPlayer;
								if (Main.player[myPlayer].team == this.team && this.team != 0)
								{
									float num = this.position.X - Main.player[myPlayer].position.X;
									float num2 = this.position.Y - Main.player[myPlayer].position.Y;
									float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
									if (num3 < 800f)
									{
										Main.player[myPlayer].AddBuff(43, 10, true);
									}
								}
							}
						}
					}
				}
				if (this.armor[k].type == 936)
				{
					this.kbGlove = true;
					this.meleeSpeed += 0.12f;
					this.meleeDamage += 0.12f;
				}
				if (this.armor[k].type == 898)
				{
					this.accRunSpeed = 6.75f;
					this.rocketBoots = 2;
					this.moveSpeed += 0.08f;
				}
				if (this.armor[k].type == 1862)
				{
					this.accRunSpeed = 6.75f;
					this.rocketBoots = 3;
					this.moveSpeed += 0.08f;
					this.iceSkate = true;
				}
				if (this.armor[k].type == 1865)
				{
					this.lifeRegen += 2;
					this.statDefense += 4;
					this.meleeSpeed += 0.1f;
					this.meleeDamage += 0.1f;
					this.meleeCrit += 2;
					this.rangedDamage += 0.1f;
					this.rangedCrit += 2;
					this.magicDamage += 0.1f;
					this.magicCrit += 2;
					this.pickSpeed -= 0.15f;
					this.minionDamage += 0.1f;
					this.minionKB += 0.5f;
				}
				if (this.armor[k].type == 899 && Main.dayTime)
				{
					this.lifeRegen += 2;
					this.statDefense += 4;
					this.meleeSpeed += 0.1f;
					this.meleeDamage += 0.1f;
					this.meleeCrit += 2;
					this.rangedDamage += 0.1f;
					this.rangedCrit += 2;
					this.magicDamage += 0.1f;
					this.magicCrit += 2;
					this.pickSpeed -= 0.15f;
					this.minionDamage += 0.1f;
					this.minionKB += 0.5f;
				}
				if (this.armor[k].type == 900 && !Main.dayTime)
				{
					this.lifeRegen += 2;
					this.statDefense += 4;
					this.meleeSpeed += 0.1f;
					this.meleeDamage += 0.1f;
					this.meleeCrit += 2;
					this.rangedDamage += 0.1f;
					this.rangedCrit += 2;
					this.magicDamage += 0.1f;
					this.magicCrit += 2;
					this.pickSpeed -= 0.15f;
					this.minionDamage += 0.1f;
					this.minionKB += 0.5f;
				}
				if (this.armor[k].type == 407)
				{
					this.blockRange = 1;
				}
				if (this.armor[k].type == 489)
				{
					this.magicDamage += 0.15f;
				}
				if (this.armor[k].type == 490)
				{
					this.meleeDamage += 0.15f;
				}
				if (this.armor[k].type == 491)
				{
					this.rangedDamage += 0.15f;
				}
				if (this.armor[k].type == 935)
				{
					this.magicDamage += 0.12f;
					this.meleeDamage += 0.12f;
					this.rangedDamage += 0.12f;
					this.minionDamage += 0.12f;
				}
				if (this.armor[k].type == 492)
				{
					this.wingTimeMax = 100;
				}
				if (this.armor[k].type == 493)
				{
					this.wingTimeMax = 100;
				}
				if (this.armor[k].type == 665)
				{
					this.wingTimeMax = 220;
				}
				if (this.armor[k].type == 748)
				{
					this.wingTimeMax = 115;
				}
				if (this.armor[k].type == 749)
				{
					this.wingTimeMax = 130;
				}
				if (this.armor[k].type == 761)
				{
					this.wingTimeMax = 130;
				}
				if (this.armor[k].type == 785)
				{
					this.wingTimeMax = 140;
				}
				if (this.armor[k].type == 786)
				{
					this.wingTimeMax = 140;
				}
				if (this.armor[k].type == 821)
				{
					this.wingTimeMax = 160;
				}
				if (this.armor[k].type == 822)
				{
					this.wingTimeMax = 160;
				}
				if (this.armor[k].type == 823)
				{
					this.wingTimeMax = 160;
				}
				if (this.armor[k].type == 2280)
				{
					this.wingTimeMax = 160;
				}
				if (this.armor[k].type == 2494)
				{
					this.wingTimeMax = 100;
				}
				if (this.armor[k].type == 2609)
				{
					this.wingTimeMax = 180;
					this.ignoreWater = true;
				}
				if (this.armor[k].type == 948)
				{
					this.wingTimeMax = 180;
				}
				if (this.armor[k].type == 1162)
				{
					this.wingTimeMax = 160;
				}
				if (this.armor[k].type == 1165)
				{
					this.wingTimeMax = 140;
				}
				if (this.armor[k].type == 1515)
				{
					this.wingTimeMax = 130;
				}
				if (this.armor[k].type == 1583)
				{
					this.wingTimeMax = 190;
				}
				if (this.armor[k].type == 1584)
				{
					this.wingTimeMax = 190;
				}
				if (this.armor[k].type == 1585)
				{
					this.wingTimeMax = 190;
				}
				if (this.armor[k].type == 1586)
				{
					this.wingTimeMax = 190;
				}
				if (this.armor[k].type == 1797)
				{
					this.wingTimeMax = 180;
				}
				if (this.armor[k].type == 1830)
				{
					this.wingTimeMax = 180;
				}
				if (this.armor[k].type == 1866)
				{
					this.wingTimeMax = 170;
				}
				if (this.armor[k].type == 1871)
				{
					this.wingTimeMax = 170;
				}
				if (this.armor[k].type == 885)
				{
					this.buffImmune[30] = true;
				}
				if (this.armor[k].type == 886)
				{
					this.buffImmune[36] = true;
				}
				if (this.armor[k].type == 887)
				{
					this.buffImmune[20] = true;
				}
				if (this.armor[k].type == 888)
				{
					this.buffImmune[22] = true;
				}
				if (this.armor[k].type == 889)
				{
					this.buffImmune[32] = true;
				}
				if (this.armor[k].type == 890)
				{
					this.buffImmune[35] = true;
				}
				if (this.armor[k].type == 891)
				{
					this.buffImmune[23] = true;
				}
				if (this.armor[k].type == 892)
				{
					this.buffImmune[33] = true;
				}
				if (this.armor[k].type == 893)
				{
					this.buffImmune[31] = true;
				}
				if (this.armor[k].type == 901)
				{
					this.buffImmune[33] = true;
					this.buffImmune[36] = true;
				}
				if (this.armor[k].type == 902)
				{
					this.buffImmune[30] = true;
					this.buffImmune[20] = true;
				}
				if (this.armor[k].type == 903)
				{
					this.buffImmune[32] = true;
					this.buffImmune[31] = true;
				}
				if (this.armor[k].type == 904)
				{
					this.buffImmune[35] = true;
					this.buffImmune[23] = true;
				}
				if (this.armor[k].type == 1921)
				{
					this.buffImmune[46] = true;
					this.buffImmune[47] = true;
				}
				if (this.armor[k].type == 1612)
				{
					this.buffImmune[33] = true;
					this.buffImmune[36] = true;
					this.buffImmune[30] = true;
					this.buffImmune[20] = true;
					this.buffImmune[32] = true;
					this.buffImmune[31] = true;
					this.buffImmune[35] = true;
					this.buffImmune[23] = true;
					this.buffImmune[22] = true;
				}
				if (this.armor[k].type == 1613)
				{
					this.noKnockback = true;
					this.fireWalk = true;
					this.buffImmune[33] = true;
					this.buffImmune[36] = true;
					this.buffImmune[30] = true;
					this.buffImmune[20] = true;
					this.buffImmune[32] = true;
					this.buffImmune[31] = true;
					this.buffImmune[35] = true;
					this.buffImmune[23] = true;
					this.buffImmune[22] = true;
				}
				if (this.armor[k].type == 497)
				{
					this.accMerman = true;
				}
				if (this.armor[k].type == 535)
				{
					this.pStone = true;
				}
				if (this.armor[k].type == 536)
				{
					this.kbGlove = true;
				}
				if (this.armor[k].type == 532)
				{
					this.starCloak = true;
				}
				if (this.armor[k].type == 554)
				{
					this.longInvince = true;
				}
				if (this.armor[k].type == 555)
				{
					this.manaFlower = true;
					this.manaCost -= 0.08f;
				}
				if (Main.myPlayer == this.whoAmi)
				{
					if (this.armor[k].type == 576 && Main.rand.Next(18000) == 0 && Main.curMusic > 0 && Main.curMusic <= 33)
					{
						int num4 = 0;
						if (Main.curMusic == 1)
						{
							num4 = 0;
						}
						if (Main.curMusic == 2)
						{
							num4 = 1;
						}
						if (Main.curMusic == 3)
						{
							num4 = 2;
						}
						if (Main.curMusic == 4)
						{
							num4 = 4;
						}
						if (Main.curMusic == 5)
						{
							num4 = 5;
						}
						if (Main.curMusic == 6)
						{
							num4 = 3;
						}
						if (Main.curMusic == 7)
						{
							num4 = 6;
						}
						if (Main.curMusic == 8)
						{
							num4 = 7;
						}
						if (Main.curMusic == 9)
						{
							num4 = 9;
						}
						if (Main.curMusic == 10)
						{
							num4 = 8;
						}
						if (Main.curMusic == 11)
						{
							num4 = 11;
						}
						if (Main.curMusic == 12)
						{
							num4 = 10;
						}
						if (Main.curMusic == 13)
						{
							num4 = 12;
						}
						if (Main.curMusic == 29)
						{
							this.armor[k].SetDefaults(1610, false);
						}
						else
						{
							if (Main.curMusic == 30)
							{
								this.armor[k].SetDefaults(1963, false);
							}
							else
							{
								if (Main.curMusic == 31)
								{
									this.armor[k].SetDefaults(1964, false);
								}
								else
								{
									if (Main.curMusic == 32)
									{
										this.armor[k].SetDefaults(1965, false);
									}
									else
									{
										if (Main.curMusic == 33)
										{
											this.armor[k].SetDefaults(2742, false);
										}
										else
										{
											if (Main.curMusic > 13)
											{
												this.armor[k].SetDefaults(1596 + Main.curMusic - 14, false);
											}
											else
											{
												this.armor[k].SetDefaults(num4 + 562, false);
											}
										}
									}
								}
							}
						}
					}
					if (this.armor[k].type >= 562 && this.armor[k].type <= 574)
					{
						Main.musicBox2 = this.armor[k].type - 562;
					}
					if (this.armor[k].type >= 1596 && this.armor[k].type <= 1609)
					{
						Main.musicBox2 = this.armor[k].type - 1596 + 13;
					}
					if (this.armor[k].type == 1610)
					{
						Main.musicBox2 = 27;
					}
					if (this.armor[k].type == 1963)
					{
						Main.musicBox2 = 28;
					}
					if (this.armor[k].type == 1964)
					{
						Main.musicBox2 = 29;
					}
					if (this.armor[k].type == 1965)
					{
						Main.musicBox2 = 30;
					}
					if (this.armor[k].type == 2742)
					{
						Main.musicBox2 = 31;
					}
				}
			}
			for (int l = 3; l < 8; l++)
			{
				if (this.armor[l].wingSlot > 0)
				{
					if (!this.hideVisual[l] || (this.velocity.Y != 0f && !this.mount.Active))
					{
						this.wings = (int)this.armor[l].wingSlot;
					}
					this.wingsLogic = (int)this.armor[l].wingSlot;
				}
			}
			for (int m = 11; m < 16; m++)
			{
				if (this.armor[m].wingSlot > 0)
				{
					this.wings = (int)this.armor[m].wingSlot;
				}
			}
		}
		public void UpdateArmorSets(int i)
		{
			this.setBonus = "";
			if (this.body == 67 && this.legs == 56 && this.head >= 103 && this.head <= 105)
			{
				this.setBonus = Lang.setBonus(31, false);
				this.armorSteath = true;
			}
			if ((this.head == 52 && this.body == 32 && this.legs == 31) || (this.head == 53 && this.body == 33 && this.legs == 32) || (this.head == 54 && this.body == 34 && this.legs == 33) || (this.head == 55 && this.body == 35 && this.legs == 34) || (this.head == 70 && this.body == 46 && this.legs == 42) || (this.head == 71 && this.body == 47 && this.legs == 43) || (this.head == 166 && this.body == 173 && this.legs == 108) || (this.head == 167 && this.body == 174 && this.legs == 109))
			{
				this.setBonus = Lang.setBonus(20, false);
				this.statDefense++;
			}
			if ((this.head == 1 && this.body == 1 && this.legs == 1) || ((this.head == 72 || this.head == 2) && this.body == 2 && this.legs == 2) || (this.head == 47 && this.body == 28 && this.legs == 27))
			{
				this.setBonus = Lang.setBonus(0, false);
				this.statDefense += 2;
			}
			if ((this.head == 3 && this.body == 3 && this.legs == 3) || ((this.head == 73 || this.head == 4) && this.body == 4 && this.legs == 4) || (this.head == 48 && this.body == 29 && this.legs == 28) || (this.head == 49 && this.body == 30 && this.legs == 29))
			{
				this.setBonus = Lang.setBonus(1, false);
				this.statDefense += 3;
			}
			if (this.head == 50 && this.body == 31 && this.legs == 30)
			{
				this.setBonus = Lang.setBonus(32, false);
				this.statDefense += 4;
			}
			if (this.head == 112 && this.body == 75 && this.legs == 64)
			{
				this.setBonus = Lang.setBonus(33, false);
				this.meleeDamage += 0.1f;
				this.magicDamage += 0.1f;
				this.rangedDamage += 0.1f;
			}
			if (this.head == 157 && this.body == 105 && this.legs == 98)
			{
				int num = 0;
				this.setBonus = Lang.setBonus(38, false);
				this.beetleOffense = true;
				this.beetleCounter -= 3f;
				this.beetleCounter -= (float)(this.beetleCountdown / 10);
				this.beetleCountdown++;
				if (this.beetleCounter < 0f)
				{
					this.beetleCounter = 0f;
				}
				int num2 = 400;
				int num3 = 1200;
				int num4 = 4600;
				if (this.beetleCounter > (float)(num2 + num3 + num4 + num3))
				{
					this.beetleCounter = (float)(num2 + num3 + num4 + num3);
				}
				if (this.beetleCounter > (float)(num2 + num3 + num4))
				{
					this.AddBuff(100, 5, false);
					num = 3;
				}
				else
				{
					if (this.beetleCounter > (float)(num2 + num3))
					{
						this.AddBuff(99, 5, false);
						num = 2;
					}
					else
					{
						if (this.beetleCounter > (float)num2)
						{
							this.AddBuff(98, 5, false);
							num = 1;
						}
					}
				}
				if (num < this.beetleOrbs)
				{
					this.beetleCountdown = 0;
				}
				else
				{
					if (num > this.beetleOrbs)
					{
						this.beetleCounter += 200f;
					}
				}
				if (num != this.beetleOrbs && this.beetleOrbs > 0)
				{
					for (int j = 0; j < 22; j++)
					{
						if (this.buffType[j] >= 98 && this.buffType[j] <= 100 && this.buffType[j] != 97 + num)
						{
							this.DelBuff(j);
						}
					}
				}
			}
			else
			{
				if (this.head == 157 && this.body == 106 && this.legs == 98)
				{
					this.setBonus = Lang.setBonus(37, false);
					this.beetleDefense = true;
					this.beetleCounter += 1f;
					int num5 = 180;
					if (this.beetleCounter >= (float)num5)
					{
						if (this.beetleOrbs > 0 && this.beetleOrbs < 3)
						{
							for (int k = 0; k < 22; k++)
							{
								if (this.buffType[k] >= 95 && this.buffType[k] <= 96)
								{
									this.DelBuff(k);
								}
							}
						}
						if (this.beetleOrbs < 3)
						{
							this.AddBuff(95 + this.beetleOrbs, 5, false);
							this.beetleCounter = 0f;
						}
						else
						{
							this.beetleCounter = (float)num5;
						}
					}
				}
			}
			if (!this.beetleDefense && !this.beetleOffense)
			{
				this.beetleCounter = 0f;
			}
			else
			{
				this.beetleFrameCounter++;
				if (this.beetleFrameCounter >= 1)
				{
					this.beetleFrameCounter = 0;
					this.beetleFrame++;
					if (this.beetleFrame > 2)
					{
						this.beetleFrame = 0;
					}
				}
				for (int l = this.beetleOrbs; l < 3; l++)
				{
					this.beetlePos[l].X = 0f;
					this.beetlePos[l].Y = 0f;
				}
				for (int m = 0; m < this.beetleOrbs; m++)
				{
					this.beetlePos[m] += this.beetleVel[m];
					Vector2[] expr_66B_cp_0 = this.beetleVel;
					int expr_66B_cp_1 = m;
					expr_66B_cp_0[expr_66B_cp_1].X = expr_66B_cp_0[expr_66B_cp_1].X + (float)Main.rand.Next(-100, 101) * 0.005f;
					Vector2[] expr_699_cp_0 = this.beetleVel;
					int expr_699_cp_1 = m;
					expr_699_cp_0[expr_699_cp_1].Y = expr_699_cp_0[expr_699_cp_1].Y + (float)Main.rand.Next(-100, 101) * 0.005f;
					float num6 = this.beetlePos[m].X;
					float num7 = this.beetlePos[m].Y;
					float num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
					if (num8 > 100f)
					{
						num8 = 20f / num8;
						num6 *= -num8;
						num7 *= -num8;
						int num9 = 10;
						this.beetleVel[m].X = (this.beetleVel[m].X * (float)(num9 - 1) + num6) / (float)num9;
						this.beetleVel[m].Y = (this.beetleVel[m].Y * (float)(num9 - 1) + num7) / (float)num9;
					}
					else
					{
						if (num8 > 30f)
						{
							num8 = 10f / num8;
							num6 *= -num8;
							num7 *= -num8;
							int num10 = 20;
							this.beetleVel[m].X = (this.beetleVel[m].X * (float)(num10 - 1) + num6) / (float)num10;
							this.beetleVel[m].Y = (this.beetleVel[m].Y * (float)(num10 - 1) + num7) / (float)num10;
						}
					}
					num6 = this.beetleVel[m].X;
					num7 = this.beetleVel[m].Y;
					num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
					if (num8 > 2f)
					{
						this.beetleVel[m] *= 0.9f;
					}
					this.beetlePos[m] -= this.velocity * 0.25f;
				}
			}
			if (this.head == 14 && ((this.body >= 58 && this.body <= 63) || this.body == 167))
			{
				this.setBonus = Lang.setBonus(28, false);
				this.magicCrit += 10;
			}
			if (this.head == 159 && ((this.body >= 58 && this.body <= 63) || this.body == 167))
			{
				this.setBonus = Lang.setBonus(36, false);
				this.statManaMax2 += 60;
			}
			if ((this.head == 5 || this.head == 74) && (this.body == 5 || this.body == 48) && (this.legs == 5 || this.legs == 44))
			{
				this.setBonus = Lang.setBonus(2, false);
				this.moveSpeed += 0.15f;
			}
			if (this.head == 57 && this.body == 37 && this.legs == 35)
			{
				this.setBonus = Lang.setBonus(21, false);
				this.crimsonRegen = true;
			}
			if (this.head == 101 && this.body == 66 && this.legs == 55)
			{
				this.setBonus = Lang.setBonus(30, false);
				this.ghostHeal = true;
			}
			if (this.head == 156 && this.body == 66 && this.legs == 55)
			{
				this.setBonus = Lang.setBonus(35, false);
				this.ghostHurt = true;
			}
			if (this.head == 6 && this.body == 6 && this.legs == 6)
			{
				this.setBonus = Lang.setBonus(3, false);
				this.spaceGun = true;
			}
			if (this.head == 46 && this.body == 27 && this.legs == 26)
			{
				this.frostArmor = true;
				this.setBonus = Lang.setBonus(22, false);
				this.frostBurn = true;
			}
			if ((this.head == 75 || this.head == 7) && this.body == 7 && this.legs == 7)
			{
				this.boneArmor = true;
				this.setBonus = Lang.setBonus(4, false);
				this.ammoCost80 = true;
			}
			if ((this.head == 76 || this.head == 8) && (this.body == 49 || this.body == 8) && (this.legs == 45 || this.legs == 8))
			{
				this.setBonus = Lang.setBonus(5, false);
				this.manaCost -= 0.16f;
			}
			if (this.head == 9 && this.body == 9 && this.legs == 9)
			{
				this.setBonus = Lang.setBonus(6, false);
				this.meleeDamage += 0.17f;
			}
			if (this.head == 11 && this.body == 20 && this.legs == 19)
			{
				this.setBonus = Lang.setBonus(7, false);
				this.pickSpeed -= 0.3f;
			}
			if ((this.head == 78 || this.head == 79 || this.head == 80) && this.body == 51 && this.legs == 47)
			{
				this.setBonus = Lang.setBonus(27, false);
				this.AddBuff(60, 18000, true);
			}
			else
			{
				if (this.crystalLeaf)
				{
					for (int n = 0; n < 22; n++)
					{
						if (this.buffType[n] == 60)
						{
							this.DelBuff(n);
						}
					}
				}
			}
			if (this.head == 99 && this.body == 65 && this.legs == 54)
			{
				this.setBonus = Lang.setBonus(29, false);
				this.thorns = true;
				this.turtleThorns = true;
			}
			if (this.body == 17 && this.legs == 16)
			{
				if (this.head == 29)
				{
					this.setBonus = Lang.setBonus(8, false);
					this.manaCost -= 0.14f;
				}
				else
				{
					if (this.head == 30)
					{
						this.setBonus = Lang.setBonus(9, false);
						this.meleeSpeed += 0.15f;
					}
					else
					{
						if (this.head == 31)
						{
							this.setBonus = Lang.setBonus(10, false);
							this.ammoCost80 = true;
						}
					}
				}
			}
			if (this.body == 18 && this.legs == 17)
			{
				if (this.head == 32)
				{
					this.setBonus = Lang.setBonus(11, false);
					this.manaCost -= 0.17f;
				}
				else
				{
					if (this.head == 33)
					{
						this.setBonus = Lang.setBonus(12, false);
						this.meleeCrit += 5;
					}
					else
					{
						if (this.head == 34)
						{
							this.setBonus = Lang.setBonus(13, false);
							this.ammoCost80 = true;
						}
					}
				}
			}
			if (this.body == 19 && this.legs == 18)
			{
				if (this.head == 35)
				{
					this.setBonus = Lang.setBonus(14, false);
					this.manaCost -= 0.19f;
				}
				else
				{
					if (this.head == 36)
					{
						this.setBonus = Lang.setBonus(15, false);
						this.meleeSpeed += 0.18f;
						this.moveSpeed += 0.18f;
					}
					else
					{
						if (this.head == 37)
						{
							this.setBonus = Lang.setBonus(16, false);
							this.ammoCost75 = true;
						}
					}
				}
			}
			if (this.body == 54 && this.legs == 49 && (this.head == 83 || this.head == 84 || this.head == 85))
			{
				this.setBonus = Lang.setBonus(24, false);
				this.onHitRegen = true;
			}
			if (this.body == 55 && this.legs == 50 && (this.head == 86 || this.head == 87 || this.head == 88))
			{
				this.setBonus = Lang.setBonus(25, false);
				this.onHitPetal = true;
			}
			if (this.body == 56 && this.legs == 51 && (this.head == 89 || this.head == 90 || this.head == 91))
			{
				this.setBonus = Lang.setBonus(26, false);
				this.onHitDodge = true;
			}
			if (this.body == 24 && this.legs == 23)
			{
				if (this.head == 42)
				{
					this.setBonus = Lang.setBonus(17, false);
					this.manaCost -= 0.2f;
				}
				else
				{
					if (this.head == 43)
					{
						this.setBonus = Lang.setBonus(18, false);
						this.meleeSpeed += 0.19f;
						this.moveSpeed += 0.19f;
					}
					else
					{
						if (this.head == 41)
						{
							this.setBonus = Lang.setBonus(19, false);
							this.ammoCost75 = true;
						}
					}
				}
			}
			if (this.head == 82 && this.body == 53 && this.legs == 48)
			{
				this.setBonus = Lang.setBonus(23, false);
				this.maxMinions++;
			}
			if (this.head == 134 && this.body == 95 && this.legs == 79)
			{
				this.setBonus = Lang.setBonus(34, false);
				this.minionDamage += 0.25f;
			}
			if (this.head == 160 && this.body == 168 && this.legs == 103)
			{
				this.setBonus = Lang.setBonus(39, false);
				this.minionDamage += 0.1f;
			}
			if (this.head == 162 && this.body == 170 && this.legs == 105)
			{
				this.setBonus = Lang.setBonus(40, false);
				this.minionDamage += 0.12f;
			}
		}
		public void UpdateSocialShadow()
		{
			this.shadowCount++;
			if (this.shadowCount == 1)
			{
				this.shadowPos[2] = this.shadowPos[1];
				this.shadowRotation[2] = this.shadowRotation[1];
				this.shadowOrigin[2] = this.shadowOrigin[1];
				return;
			}
			if (this.shadowCount == 2)
			{
				this.shadowPos[1] = this.shadowPos[0];
				this.shadowRotation[1] = this.shadowRotation[0];
				this.shadowOrigin[1] = this.shadowOrigin[0];
				return;
			}
			if (this.shadowCount >= 3)
			{
				this.shadowCount = 0;
				this.shadowPos[0] = this.position;
				Vector2[] expr_FD_cp_0 = this.shadowPos;
				int expr_FD_cp_1 = 0;
				expr_FD_cp_0[expr_FD_cp_1].Y = expr_FD_cp_0[expr_FD_cp_1].Y + this.gfxOffY;
				this.shadowRotation[0] = this.fullRotation;
				this.shadowOrigin[0] = this.fullRotationOrigin;
			}
		}
		public void UpdateTeleportVisuals()
		{
			if (this.teleportTime > 0f)
			{
				if (this.teleportStyle == 1)
				{
					if ((float)Main.rand.Next(100) <= 100f * this.teleportTime)
					{
						int num = Dust.NewDust(new Vector2((float)this.getRect().X, (float)this.getRect().Y), this.getRect().Width, this.getRect().Height, 164, 0f, 0f, 0, default(Color), 1f);
						Main.dust[num].scale = this.teleportTime * 1.5f;
						Main.dust[num].noGravity = true;
						Main.dust[num].velocity *= 1.1f;
					}
				}
				else
				{
					if (this.teleportStyle == 2)
					{
						this.teleportTime = 0.005f;
					}
					else
					{
						if (this.teleportStyle == 0 && (float)Main.rand.Next(100) <= 100f * this.teleportTime * 2f)
						{
							int num2 = Dust.NewDust(new Vector2((float)this.getRect().X, (float)this.getRect().Y), this.getRect().Width, this.getRect().Height, 159, 0f, 0f, 0, default(Color), 1f);
							Main.dust[num2].scale = this.teleportTime * 1.5f;
							Main.dust[num2].noGravity = true;
							Main.dust[num2].velocity *= 1.1f;
						}
					}
				}
				this.teleportTime -= 0.005f;
			}
		}
		public void UpdateBiomes()
		{
			this.zoneEvil = false;
			if (Main.evilTiles >= 200)
			{
				this.zoneEvil = true;
			}
			this.zoneHoly = false;
			if (Main.holyTiles >= 100)
			{
				this.zoneHoly = true;
			}
			this.zoneMeteor = false;
			if (Main.meteorTiles >= 50)
			{
				this.zoneMeteor = true;
			}
			this.zoneDungeon = false;
			if (Main.dungeonTiles >= 250 && (double)this.position.Y > Main.worldSurface * 16.0)
			{
				int num = (int)this.position.X / 16;
				int num2 = (int)this.position.Y / 16;
				if (Main.wallDungeon[(int)Main.tile[num, num2].wall])
				{
					this.zoneDungeon = true;
				}
			}
			this.zoneJungle = false;
			if (Main.jungleTiles >= 80)
			{
				this.zoneJungle = true;
			}
			this.zoneSnow = false;
			if (Main.snowTiles >= 300)
			{
				this.zoneSnow = true;
			}
			this.zoneBlood = false;
			if (Main.bloodTiles >= 200)
			{
				this.zoneBlood = true;
			}
			if (Main.waterCandles > 0)
			{
				this.zoneCandle = true;
				return;
			}
			this.zoneCandle = false;
		}
		public void UpdateDead()
		{
			this.gem = -1;
			this.slippy = false;
			this.slippy2 = false;
			this.powerrun = false;
			this.wings = 0;
			this.wingsLogic = 0;
			this.face = (this.neck = (this.back = (this.front = (this.handoff = (this.handon = (this.shoe = (this.waist = (this.balloon = (this.shield = 0)))))))));
			this.poisoned = false;
			this.venom = false;
			this.onFire = false;
			this.dripping = false;
			this.drippingSlime = false;
			this.burned = false;
			this.suffocating = false;
			this.onFire2 = false;
			this.onFrostBurn = false;
			this.blind = false;
			this.blackout = false;
			this.gravDir = 1f;
			for (int i = 0; i < 22; i++)
			{
				this.buffTime[i] = 0;
				this.buffType[i] = 0;
			}
			if (this.whoAmi == Main.myPlayer)
			{
				Main.npcChatText = "";
				Main.editSign = false;
			}
			this.grappling[0] = -1;
			this.grappling[1] = -1;
			this.grappling[2] = -1;
			this.sign = -1;
			this.talkNPC = -1;
			Main.npcChatCornerItem = 0;
			this.statLife = 0;
			this.channel = false;
			this.potionDelay = 0;
			this.chest = -1;
			this.changeItem = -1;
			this.itemAnimation = 0;
			this.immuneAlpha += 2;
			if (this.immuneAlpha > 255)
			{
				this.immuneAlpha = 255;
			}
			this.headPosition += this.headVelocity;
			this.bodyPosition += this.bodyVelocity;
			this.legPosition += this.legVelocity;
			this.headRotation += this.headVelocity.X * 0.1f;
			this.bodyRotation += this.bodyVelocity.X * 0.1f;
			this.legRotation += this.legVelocity.X * 0.1f;
			this.headVelocity.Y = this.headVelocity.Y + 0.1f;
			this.bodyVelocity.Y = this.bodyVelocity.Y + 0.1f;
			this.legVelocity.Y = this.legVelocity.Y + 0.1f;
			this.headVelocity.X = this.headVelocity.X * 0.99f;
			this.bodyVelocity.X = this.bodyVelocity.X * 0.99f;
			this.legVelocity.X = this.legVelocity.X * 0.99f;
			if (this.difficulty == 2)
			{
				if (this.respawnTimer > 0)
				{
					this.respawnTimer--;
					return;
				}
				if (this.whoAmi == Main.myPlayer || Main.netMode == 2)
				{
					this.ghost = true;
					return;
				}
			}
			else
			{
				this.respawnTimer--;
				if (this.respawnTimer <= 0 && Main.myPlayer == this.whoAmi)
				{
					if (Main.mouseItem.type > 0)
					{
						Main.playerInventory = true;
					}
					this.Spawn();
				}
			}
		}
		public void SmartCursorLookup()
		{
			if (this.whoAmi != Main.myPlayer)
			{
				return;
			}
			Main.smartDigShowing = false;
			if (!Main.smartDigEnabled)
			{
				return;
			}
			Item item = this.inventory[this.selectedItem];
			Vector2 vector = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			if (this.gravDir == -1f)
			{
				vector.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
			}
			int arg_77_0 = Player.tileTargetX;
			int arg_7D_0 = Player.tileTargetY;
			int tileBoost = item.tileBoost;
			int num = (int)(this.position.X / 16f) - Player.tileRangeX - tileBoost + 1;
			int num2 = (int)((this.position.X + (float)this.width) / 16f) + Player.tileRangeX + tileBoost - 1;
			int num3 = (int)(this.position.Y / 16f) - Player.tileRangeY - tileBoost + 1;
			int num4 = (int)((this.position.Y + (float)this.height) / 16f) + Player.tileRangeY + tileBoost - 2;
			if (num < 10)
			{
				num = 10;
			}
			if (num2 > Main.maxTilesX - 10)
			{
				num2 = Main.maxTilesX - 10;
			}
			if (num3 < 10)
			{
				num3 = 10;
			}
			if (num4 > Main.maxTilesY - 10)
			{
				num4 = Main.maxTilesY - 10;
			}
			List<Tuple<int, int>> list = new List<Tuple<int, int>>();
			for (int i = 0; i < this.grapCount; i++)
			{
				Projectile projectile = Main.projectile[this.grappling[i]];
				int item2 = (int)projectile.center().X / 16;
				int item3 = (int)projectile.center().Y / 16;
				list.Add(new Tuple<int, int>(item2, item3));
			}
			int num5 = -1;
			int num6 = -1;
			if (item.axe > 0 && num5 == -1 && num6 == -1)
			{
				float num7 = -1f;
				for (int j = num; j <= num2; j++)
				{
					for (int k = num3; k <= num4; k++)
					{
						if (Main.tile[j, k].active())
						{
							Tile tile = Main.tile[j, k];
							if (Main.tileAxe[(int)tile.type])
							{
								int num8 = j;
								int num9 = k;
								if (tile.type == 5)
								{
									if (Collision.InTileBounds(num8 + 1, num9, num, num3, num2, num4))
									{
										if (Main.tile[num8, num9].frameY >= 198 && Main.tile[num8, num9].frameX == 44)
										{
											num8++;
										}
										if (Main.tile[num8, num9].frameX == 66 && Main.tile[num8, num9].frameY <= 44)
										{
											num8++;
										}
										if (Main.tile[num8, num9].frameX == 44 && Main.tile[num8, num9].frameY >= 132 && Main.tile[num8, num9].frameY <= 176)
										{
											num8++;
										}
									}
									if (Collision.InTileBounds(num8 - 1, num9, num, num3, num2, num4))
									{
										if (Main.tile[num8, num9].frameY >= 198 && Main.tile[num8, num9].frameX == 66)
										{
											num8--;
										}
										if (Main.tile[num8, num9].frameX == 88 && Main.tile[num8, num9].frameY >= 66 && Main.tile[num8, num9].frameY <= 110)
										{
											num8--;
										}
										if (Main.tile[num8, num9].frameX == 22 && Main.tile[num8, num9].frameY >= 132 && Main.tile[num8, num9].frameY <= 176)
										{
											num8--;
										}
									}
									while (Main.tile[num8, num9].active() && Main.tile[num8, num9].type == 5 && Main.tile[num8, num9 + 1].type == 5 && Collision.InTileBounds(num8, num9 + 1, num, num3, num2, num4))
									{
										num9++;
									}
								}
								if (tile.type == 80)
								{
									if (Collision.InTileBounds(num8 + 1, num9, num, num3, num2, num4))
									{
										if (Main.tile[num8, num9].frameX == 54)
										{
											num8++;
										}
										if (Main.tile[num8, num9].frameX == 108 && Main.tile[num8, num9].frameY == 36)
										{
											num8++;
										}
									}
									if (Collision.InTileBounds(num8 - 1, num9, num, num3, num2, num4))
									{
										if (Main.tile[num8, num9].frameX == 36)
										{
											num8--;
										}
										if (Main.tile[num8, num9].frameX == 108 && Main.tile[num8, num9].frameY == 18)
										{
											num8--;
										}
									}
									while (Main.tile[num8, num9].active() && Main.tile[num8, num9].type == 80 && Main.tile[num8, num9 + 1].type == 80 && Collision.InTileBounds(num8, num9 + 1, num, num3, num2, num4))
									{
										num9++;
									}
								}
								if (tile.type != 323)
								{
									if (tile.type != 72)
									{
										goto IL_63F;
									}
								}
								while (Main.tile[num8, num9].active() && ((Main.tile[num8, num9].type == 323 && Main.tile[num8, num9 + 1].type == 323) || (Main.tile[num8, num9].type == 72 && Main.tile[num8, num9 + 1].type == 72)) && Collision.InTileBounds(num8, num9 + 1, num, num3, num2, num4))
								{
									num9++;
								}
								IL_63F:
								float num10 = Vector2.Distance(new Vector2((float)num8, (float)num9) * 16f + Vector2.One * 8f, vector);
								if (num7 == -1f || num10 < num7)
								{
									num7 = num10;
									num5 = num8;
									num6 = num9;
								}
							}
						}
					}
				}
			}
			if (item.pick > 0 && num5 == -1 && num6 == -1)
			{
				Vector2 vector2 = vector - this.center();
				int num11 = Math.Sign(vector2.X);
				int num12 = Math.Sign(vector2.Y);
				if (Math.Abs(vector2.X) > Math.Abs(vector2.Y) * 3f)
				{
					num12 = 0;
					vector.Y = this.center().Y;
				}
				if (Math.Abs(vector2.Y) > Math.Abs(vector2.X) * 3f)
				{
					num11 = 0;
					vector.X = this.center().X;
				}
				int arg_768_0 = (int)this.center().X / 16;
				int arg_778_0 = (int)this.center().Y / 16;
				List<Tuple<int, int>> list2 = new List<Tuple<int, int>>();
				List<Tuple<int, int>> list3 = new List<Tuple<int, int>>();
				int num13 = 1;
				if (num12 == -1 && num11 != 0)
				{
					num13 = -1;
				}
				int num14 = (int)((this.position.X + (float)(this.width / 2) + (float)((this.width / 2 - 1) * num11)) / 16f);
				int num15 = (int)(((double)this.position.Y + 0.1) / 16.0);
				if (num13 == -1)
				{
					num15 = (int)((this.position.Y + (float)this.height - 1f) / 16f);
				}
				int num16 = this.width / 16 + ((this.width % 16 == 0) ? 0 : 1);
				int num17 = this.height / 16 + ((this.height % 16 == 0) ? 0 : 1);
				if (num11 != 0)
				{
					for (int l = 0; l < num17; l++)
					{
						if (Main.tile[num14, num15 + l * num13] == null)
						{
							return;
						}
						list2.Add(new Tuple<int, int>(num14, num15 + l * num13));
					}
				}
				if (num12 != 0)
				{
					for (int m = 0; m < num16; m++)
					{
						if (Main.tile[(int)(this.position.X / 16f) + m, num15] == null)
						{
							return;
						}
						list2.Add(new Tuple<int, int>((int)(this.position.X / 16f) + m, num15));
					}
				}
				int num18 = (int)((vector.X + (float)((this.width / 2 - 1) * num11)) / 16f);
				int num19 = (int)(((double)vector.Y + 0.1 - (double)(this.height / 2 + 1)) / 16.0);
				if (num13 == -1)
				{
					num19 = (int)((vector.Y + (float)(this.height / 2) - 1f) / 16f);
				}
				if (this.gravDir == -1f && num12 == 0)
				{
					num19++;
				}
				if (num19 < 10)
				{
					num19 = 10;
				}
				if (num19 > Main.maxTilesY - 10)
				{
					num19 = Main.maxTilesY - 10;
				}
				int num20 = this.width / 16 + ((this.width % 16 == 0) ? 0 : 1);
				int num21 = this.height / 16 + ((this.height % 16 == 0) ? 0 : 1);
				if (num11 != 0)
				{
					for (int n = 0; n < num21; n++)
					{
						if (Main.tile[num18, num19 + n * num13] == null)
						{
							return;
						}
						list3.Add(new Tuple<int, int>(num18, num19 + n * num13));
					}
				}
				if (num12 != 0)
				{
					for (int num22 = 0; num22 < num20; num22++)
					{
						if (Main.tile[(int)((vector.X - (float)(this.width / 2)) / 16f) + num22, num19] == null)
						{
							return;
						}
						list3.Add(new Tuple<int, int>((int)((vector.X - (float)(this.width / 2)) / 16f) + num22, num19));
					}
				}
				List<Tuple<int, int>> list4 = new List<Tuple<int, int>>();
				while (list2.Count > 0)
				{
					Tuple<int, int> tuple = list2[0];
					Tuple<int, int> tuple2 = list3[0];
					Tuple<int, int> tuple3 = Collision.TupleHitLine(tuple.Item1, tuple.Item2, tuple2.Item1, tuple2.Item2, num11 * (int)this.gravDir, -num12 * (int)this.gravDir, list);
					if (tuple3.Item1 == -1 || tuple3.Item2 == -1)
					{
						list2.Remove(tuple);
						list3.Remove(tuple2);
					}
					else
					{
						if (tuple3.Item1 != tuple2.Item1 || tuple3.Item2 != tuple2.Item2)
						{
							list4.Add(tuple3);
						}
						Tile tile2 = Main.tile[tuple3.Item1, tuple3.Item2];
						if (!tile2.inActive() && tile2.active() && Main.tileSolid[(int)tile2.type] && !Main.tileSolidTop[(int)tile2.type] && !list.Contains(tuple3))
						{
							list4.Add(tuple3);
						}
						list2.Remove(tuple);
						list3.Remove(tuple2);
					}
				}
				if (list4.Count > 0)
				{
					float num23 = -1f;
					Tuple<int, int> tuple4 = list4[0];
					for (int num24 = 0; num24 < list4.Count; num24++)
					{
						float num25 = Vector2.Distance(new Vector2((float)list4[num24].Item1, (float)list4[num24].Item2) * 16f + Vector2.One * 8f, this.center());
						if (num23 == -1f || num25 < num23)
						{
							num23 = num25;
							tuple4 = list4[num24];
						}
					}
					if (Collision.InTileBounds(tuple4.Item1, tuple4.Item2, num, num3, num2, num4))
					{
						num5 = tuple4.Item1;
						num6 = tuple4.Item2;
					}
				}
				list2.Clear();
				list3.Clear();
				list4.Clear();
			}
			if ((item.type == 509 || item.type == 850 || item.type == 851) && num5 == -1 && num6 == -1)
			{
				List<Tuple<int, int>> list5 = new List<Tuple<int, int>>();
				int num26 = 0;
				if (item.type == 509)
				{
					num26 = 1;
				}
				if (item.type == 850)
				{
					num26 = 2;
				}
				if (item.type == 851)
				{
					num26 = 3;
				}
				bool flag = false;
				if (Main.tile[Player.tileTargetX, Player.tileTargetY].wire() && num26 == 1)
				{
					flag = true;
				}
				if (Main.tile[Player.tileTargetX, Player.tileTargetY].wire2() && num26 == 2)
				{
					flag = true;
				}
				if (Main.tile[Player.tileTargetX, Player.tileTargetY].wire3() && num26 == 3)
				{
					flag = true;
				}
				if (!flag)
				{
					for (int num27 = num; num27 <= num2; num27++)
					{
						for (int num28 = num3; num28 <= num4; num28++)
						{
							Tile tile3 = Main.tile[num27, num28];
							if ((tile3.wire() && num26 == 1) || (tile3.wire2() && num26 == 2) || (tile3.wire3() && num26 == 3))
							{
								if (num26 == 1)
								{
									if (!Main.tile[num27 - 1, num28].wire())
									{
										list5.Add(new Tuple<int, int>(num27 - 1, num28));
									}
									if (!Main.tile[num27 + 1, num28].wire())
									{
										list5.Add(new Tuple<int, int>(num27 + 1, num28));
									}
									if (!Main.tile[num27, num28 - 1].wire())
									{
										list5.Add(new Tuple<int, int>(num27, num28 - 1));
									}
									if (!Main.tile[num27, num28 + 1].wire())
									{
										list5.Add(new Tuple<int, int>(num27, num28 + 1));
									}
								}
								if (num26 == 2)
								{
									if (!Main.tile[num27 - 1, num28].wire2())
									{
										list5.Add(new Tuple<int, int>(num27 - 1, num28));
									}
									if (!Main.tile[num27 + 1, num28].wire2())
									{
										list5.Add(new Tuple<int, int>(num27 + 1, num28));
									}
									if (!Main.tile[num27, num28 - 1].wire2())
									{
										list5.Add(new Tuple<int, int>(num27, num28 - 1));
									}
									if (!Main.tile[num27, num28 + 1].wire2())
									{
										list5.Add(new Tuple<int, int>(num27, num28 + 1));
									}
								}
								if (num26 == 3)
								{
									if (!Main.tile[num27 - 1, num28].wire3())
									{
										list5.Add(new Tuple<int, int>(num27 - 1, num28));
									}
									if (!Main.tile[num27 + 1, num28].wire3())
									{
										list5.Add(new Tuple<int, int>(num27 + 1, num28));
									}
									if (!Main.tile[num27, num28 - 1].wire3())
									{
										list5.Add(new Tuple<int, int>(num27, num28 - 1));
									}
									if (!Main.tile[num27, num28 + 1].wire3())
									{
										list5.Add(new Tuple<int, int>(num27, num28 + 1));
									}
								}
							}
						}
					}
				}
				if (list5.Count > 0)
				{
					float num29 = -1f;
					Tuple<int, int> tuple5 = list5[0];
					for (int num30 = 0; num30 < list5.Count; num30++)
					{
						float num31 = Vector2.Distance(new Vector2((float)list5[num30].Item1, (float)list5[num30].Item2) * 16f + Vector2.One * 8f, vector);
						if (num29 == -1f || num31 < num29)
						{
							num29 = num31;
							tuple5 = list5[num30];
						}
					}
					if (Collision.InTileBounds(tuple5.Item1, tuple5.Item2, num, num3, num2, num4))
					{
						num5 = tuple5.Item1;
						num6 = tuple5.Item2;
					}
				}
				list5.Clear();
			}
			if (item.hammer > 0 && num5 == -1 && num6 == -1)
			{
				Vector2 vector3 = vector - this.center();
				int num32 = Math.Sign(vector3.X);
				int num33 = Math.Sign(vector3.Y);
				if (Math.Abs(vector3.X) > Math.Abs(vector3.Y) * 3f)
				{
					num33 = 0;
					vector.Y = this.center().Y;
				}
				if (Math.Abs(vector3.Y) > Math.Abs(vector3.X) * 3f)
				{
					num32 = 0;
					vector.X = this.center().X;
				}
				int arg_1162_0 = (int)this.center().X / 16;
				int arg_1172_0 = (int)this.center().Y / 16;
				List<Tuple<int, int>> list6 = new List<Tuple<int, int>>();
				List<Tuple<int, int>> list7 = new List<Tuple<int, int>>();
				int num34 = 1;
				if (num33 == -1 && num32 != 0)
				{
					num34 = -1;
				}
				int num35 = (int)((this.position.X + (float)(this.width / 2) + (float)((this.width / 2 - 1) * num32)) / 16f);
				int num36 = (int)(((double)this.position.Y + 0.1) / 16.0);
				if (num34 == -1)
				{
					num36 = (int)((this.position.Y + (float)this.height - 1f) / 16f);
				}
				int num37 = this.width / 16 + ((this.width % 16 == 0) ? 0 : 1);
				int num38 = this.height / 16 + ((this.height % 16 == 0) ? 0 : 1);
				if (num32 != 0)
				{
					for (int num39 = 0; num39 < num38; num39++)
					{
						if (Main.tile[num35, num36 + num39 * num34] == null)
						{
							return;
						}
						list6.Add(new Tuple<int, int>(num35, num36 + num39 * num34));
					}
				}
				if (num33 != 0)
				{
					for (int num40 = 0; num40 < num37; num40++)
					{
						if (Main.tile[(int)(this.position.X / 16f) + num40, num36] == null)
						{
							return;
						}
						list6.Add(new Tuple<int, int>((int)(this.position.X / 16f) + num40, num36));
					}
				}
				int num41 = (int)((vector.X + (float)((this.width / 2 - 1) * num32)) / 16f);
				int num42 = (int)(((double)vector.Y + 0.1 - (double)(this.height / 2 + 1)) / 16.0);
				if (num34 == -1)
				{
					num42 = (int)((vector.Y + (float)(this.height / 2) - 1f) / 16f);
				}
				if (this.gravDir == -1f && num33 == 0)
				{
					num42++;
				}
				if (num42 < 10)
				{
					num42 = 10;
				}
				if (num42 > Main.maxTilesY - 10)
				{
					num42 = Main.maxTilesY - 10;
				}
				int num43 = this.width / 16 + ((this.width % 16 == 0) ? 0 : 1);
				int num44 = this.height / 16 + ((this.height % 16 == 0) ? 0 : 1);
				if (num32 != 0)
				{
					for (int num45 = 0; num45 < num44; num45++)
					{
						if (Main.tile[num41, num42 + num45 * num34] == null)
						{
							return;
						}
						list7.Add(new Tuple<int, int>(num41, num42 + num45 * num34));
					}
				}
				if (num33 != 0)
				{
					for (int num46 = 0; num46 < num43; num46++)
					{
						if (Main.tile[(int)((vector.X - (float)(this.width / 2)) / 16f) + num46, num42] == null)
						{
							return;
						}
						list7.Add(new Tuple<int, int>((int)((vector.X - (float)(this.width / 2)) / 16f) + num46, num42));
					}
				}
				List<Tuple<int, int>> list8 = new List<Tuple<int, int>>();
				while (list6.Count > 0)
				{
					Tuple<int, int> tuple6 = list6[0];
					Tuple<int, int> tuple7 = list7[0];
					Tuple<int, int> tuple8 = Collision.TupleHitLineWall(tuple6.Item1, tuple6.Item2, tuple7.Item1, tuple7.Item2);
					if (tuple8.Item1 == -1 || tuple8.Item2 == -1)
					{
						list6.Remove(tuple6);
						list7.Remove(tuple7);
					}
					else
					{
						if (tuple8.Item1 != tuple7.Item1 || tuple8.Item2 != tuple7.Item2)
						{
							list8.Add(tuple8);
						}
						Main.tile[tuple8.Item1, tuple8.Item2];
						if (Collision.HitWallSubstep(tuple8.Item1, tuple8.Item2))
						{
							list8.Add(tuple8);
						}
						list6.Remove(tuple6);
						list7.Remove(tuple7);
					}
				}
				if (list8.Count > 0)
				{
					float num47 = -1f;
					Tuple<int, int> tuple9 = list8[0];
					for (int num48 = 0; num48 < list8.Count; num48++)
					{
						float num49 = Vector2.Distance(new Vector2((float)list8[num48].Item1, (float)list8[num48].Item2) * 16f + Vector2.One * 8f, this.center());
						if (num47 == -1f || num49 < num47)
						{
							num47 = num49;
							tuple9 = list8[num48];
						}
					}
					if (Collision.InTileBounds(tuple9.Item1, tuple9.Item2, num, num3, num2, num4))
					{
						this.poundRelease = false;
						num5 = tuple9.Item1;
						num6 = tuple9.Item2;
					}
				}
				list8.Clear();
				list6.Clear();
				list7.Clear();
			}
			if (item.hammer > 0 && num5 == -1 && num6 == -1)
			{
				List<Tuple<int, int>> list9 = new List<Tuple<int, int>>();
				for (int num50 = num; num50 <= num2; num50++)
				{
					for (int num51 = num3; num51 <= num4; num51++)
					{
						if (Main.tile[num50, num51].wall > 0 && Collision.HitWallSubstep(num50, num51))
						{
							list9.Add(new Tuple<int, int>(num50, num51));
						}
					}
				}
				if (list9.Count > 0)
				{
					float num52 = -1f;
					Tuple<int, int> tuple10 = list9[0];
					for (int num53 = 0; num53 < list9.Count; num53++)
					{
						float num54 = Vector2.Distance(new Vector2((float)list9[num53].Item1, (float)list9[num53].Item2) * 16f + Vector2.One * 8f, vector);
						if (num52 == -1f || num54 < num52)
						{
							num52 = num54;
							tuple10 = list9[num53];
						}
					}
					if (Collision.InTileBounds(tuple10.Item1, tuple10.Item2, num, num3, num2, num4))
					{
						this.poundRelease = false;
						num5 = tuple10.Item1;
						num6 = tuple10.Item2;
					}
				}
				list9.Clear();
			}
			if (item.type == 510 && num5 == -1 && num6 == -1)
			{
				List<Tuple<int, int>> list10 = new List<Tuple<int, int>>();
				for (int num55 = num; num55 <= num2; num55++)
				{
					for (int num56 = num3; num56 <= num4; num56++)
					{
						Tile tile4 = Main.tile[num55, num56];
						if (tile4.wire() || tile4.wire2() || tile4.wire3())
						{
							list10.Add(new Tuple<int, int>(num55, num56));
						}
					}
				}
				if (list10.Count > 0)
				{
					float num57 = -1f;
					Tuple<int, int> tuple11 = list10[0];
					for (int num58 = 0; num58 < list10.Count; num58++)
					{
						float num59 = Vector2.Distance(new Vector2((float)list10[num58].Item1, (float)list10[num58].Item2) * 16f + Vector2.One * 8f, vector);
						if (num57 == -1f || num59 < num57)
						{
							num57 = num59;
							tuple11 = list10[num58];
						}
					}
					if (Collision.InTileBounds(tuple11.Item1, tuple11.Item2, num, num3, num2, num4))
					{
						num5 = tuple11.Item1;
						num6 = tuple11.Item2;
					}
				}
				list10.Clear();
			}
			if (item.createTile == 19 && num5 == -1 && num6 == -1)
			{
				List<Tuple<int, int>> list11 = new List<Tuple<int, int>>();
				bool flag2 = false;
				if (Main.tile[Player.tileTargetX, Player.tileTargetY].active() && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 19)
				{
					flag2 = true;
				}
				if (!flag2)
				{
					for (int num60 = num; num60 <= num2; num60++)
					{
						for (int num61 = num3; num61 <= num4; num61++)
						{
							Tile tile5 = Main.tile[num60, num61];
							if (tile5.active() && tile5.type == 19)
							{
								if (!Main.tile[num60 - 1, num61 - 1].active())
								{
									list11.Add(new Tuple<int, int>(num60 - 1, num61 - 1));
								}
								if (!Main.tile[num60 - 1, num61].active())
								{
									list11.Add(new Tuple<int, int>(num60 - 1, num61));
								}
								if (!Main.tile[num60 - 1, num61 + 1].active())
								{
									list11.Add(new Tuple<int, int>(num60 - 1, num61 + 1));
								}
								if (!Main.tile[num60 + 1, num61 - 1].active())
								{
									list11.Add(new Tuple<int, int>(num60 + 1, num61 - 1));
								}
								if (!Main.tile[num60 + 1, num61].active())
								{
									list11.Add(new Tuple<int, int>(num60 + 1, num61));
								}
								if (!Main.tile[num60 + 1, num61 + 1].active())
								{
									list11.Add(new Tuple<int, int>(num60 + 1, num61 + 1));
								}
							}
						}
					}
				}
				if (list11.Count > 0)
				{
					float num62 = -1f;
					Tuple<int, int> tuple12 = list11[0];
					for (int num63 = 0; num63 < list11.Count; num63++)
					{
						float num64 = Vector2.Distance(new Vector2((float)list11[num63].Item1, (float)list11[num63].Item2) * 16f + Vector2.One * 8f, vector);
						if (num62 == -1f || num64 < num62)
						{
							num62 = num64;
							tuple12 = list11[num63];
						}
					}
					if (Collision.InTileBounds(tuple12.Item1, tuple12.Item2, num, num3, num2, num4))
					{
						num5 = tuple12.Item1;
						num6 = tuple12.Item2;
					}
				}
				list11.Clear();
			}
			if ((item.type == 2340 || item.type == 2739) && num5 == -1 && num6 == -1)
			{
				List<Tuple<int, int>> list12 = new List<Tuple<int, int>>();
				bool flag3 = false;
				if (Main.tile[Player.tileTargetX, Player.tileTargetY].active() && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 314)
				{
					flag3 = true;
				}
				if (!flag3)
				{
					for (int num65 = num; num65 <= num2; num65++)
					{
						for (int num66 = num3; num66 <= num4; num66++)
						{
							Tile tile6 = Main.tile[num65, num66];
							if (tile6.active() && tile6.type == 314)
							{
								bool flag4 = Main.tile[num65 + 1, num66 + 1].active() && Main.tile[num65 + 1, num66 + 1].type == 314;
								bool flag5 = Main.tile[num65 + 1, num66 - 1].active() && Main.tile[num65 + 1, num66 - 1].type == 314;
								bool flag6 = Main.tile[num65 - 1, num66 + 1].active() && Main.tile[num65 - 1, num66 + 1].type == 314;
								bool flag7 = Main.tile[num65 - 1, num66 - 1].active() && Main.tile[num65 - 1, num66 - 1].type == 314;
								if ((!Main.tile[num65 - 1, num66 - 1].active() || Main.tileCut[(int)Main.tile[num65 - 1, num66 - 1].type]) && (flag4 || !flag5))
								{
									list12.Add(new Tuple<int, int>(num65 - 1, num66 - 1));
								}
								if (!Main.tile[num65 - 1, num66].active() || Main.tileCut[(int)Main.tile[num65 - 1, num66].type])
								{
									list12.Add(new Tuple<int, int>(num65 - 1, num66));
								}
								if ((!Main.tile[num65 - 1, num66 + 1].active() || Main.tileCut[(int)Main.tile[num65 - 1, num66 + 1].type]) && (flag5 || !flag4))
								{
									list12.Add(new Tuple<int, int>(num65 - 1, num66 + 1));
								}
								if ((!Main.tile[num65 + 1, num66 - 1].active() || Main.tileCut[(int)Main.tile[num65 + 1, num66 - 1].type]) && (flag6 || !flag7))
								{
									list12.Add(new Tuple<int, int>(num65 + 1, num66 - 1));
								}
								if (!Main.tile[num65 + 1, num66].active() || Main.tileCut[(int)Main.tile[num65 + 1, num66].type])
								{
									list12.Add(new Tuple<int, int>(num65 + 1, num66));
								}
								if ((!Main.tile[num65 + 1, num66 + 1].active() || Main.tileCut[(int)Main.tile[num65 + 1, num66 + 1].type]) && (flag7 || !flag6))
								{
									list12.Add(new Tuple<int, int>(num65 + 1, num66 + 1));
								}
							}
						}
					}
				}
				if (list12.Count > 0)
				{
					float num67 = -1f;
					Tuple<int, int> tuple13 = list12[0];
					for (int num68 = 0; num68 < list12.Count; num68++)
					{
						if ((!Main.tile[list12[num68].Item1, list12[num68].Item2 - 1].active() || Main.tile[list12[num68].Item1, list12[num68].Item2 - 1].type != 314) && (!Main.tile[list12[num68].Item1, list12[num68].Item2 + 1].active() || Main.tile[list12[num68].Item1, list12[num68].Item2 + 1].type != 314))
						{
							float num69 = Vector2.Distance(new Vector2((float)list12[num68].Item1, (float)list12[num68].Item2) * 16f + Vector2.One * 8f, vector);
							if (num67 == -1f || num69 < num67)
							{
								num67 = num69;
								tuple13 = list12[num68];
							}
						}
					}
					if (Collision.InTileBounds(tuple13.Item1, tuple13.Item2, num, num3, num2, num4) && num67 != -1f)
					{
						num5 = tuple13.Item1;
						num6 = tuple13.Item2;
					}
				}
				list12.Clear();
			}
			if (item.type == 2492 && num5 == -1 && num6 == -1)
			{
				List<Tuple<int, int>> list13 = new List<Tuple<int, int>>();
				bool flag8 = false;
				if (Main.tile[Player.tileTargetX, Player.tileTargetY].active() && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 314)
				{
					flag8 = true;
				}
				if (!flag8)
				{
					for (int num70 = num; num70 <= num2; num70++)
					{
						for (int num71 = num3; num71 <= num4; num71++)
						{
							Tile tile7 = Main.tile[num70, num71];
							if (tile7.active() && tile7.type == 314)
							{
								if (!Main.tile[num70 - 1, num71].active() || Main.tileCut[(int)Main.tile[num70 - 1, num71].type])
								{
									list13.Add(new Tuple<int, int>(num70 - 1, num71));
								}
								if (!Main.tile[num70 + 1, num71].active() || Main.tileCut[(int)Main.tile[num70 + 1, num71].type])
								{
									list13.Add(new Tuple<int, int>(num70 + 1, num71));
								}
							}
						}
					}
				}
				if (list13.Count > 0)
				{
					float num72 = -1f;
					Tuple<int, int> tuple14 = list13[0];
					for (int num73 = 0; num73 < list13.Count; num73++)
					{
						if ((!Main.tile[list13[num73].Item1, list13[num73].Item2 - 1].active() || Main.tile[list13[num73].Item1, list13[num73].Item2 - 1].type != 314) && (!Main.tile[list13[num73].Item1, list13[num73].Item2 + 1].active() || Main.tile[list13[num73].Item1, list13[num73].Item2 + 1].type != 314))
						{
							float num74 = Vector2.Distance(new Vector2((float)list13[num73].Item1, (float)list13[num73].Item2) * 16f + Vector2.One * 8f, vector);
							if (num72 == -1f || num74 < num72)
							{
								num72 = num74;
								tuple14 = list13[num73];
							}
						}
					}
					if (Collision.InTileBounds(tuple14.Item1, tuple14.Item2, num, num3, num2, num4) && num72 != -1f)
					{
						num5 = tuple14.Item1;
						num6 = tuple14.Item2;
					}
				}
				list13.Clear();
			}
			if (item.createWall > 0 && num5 == -1 && num6 == -1)
			{
				List<Tuple<int, int>> list14 = new List<Tuple<int, int>>();
				for (int num75 = num; num75 <= num2; num75++)
				{
					for (int num76 = num3; num76 <= num4; num76++)
					{
						Tile tile8 = Main.tile[num75, num76];
						if (tile8.wall == 0 && (!tile8.active() || !Main.tileSolid[(int)tile8.type] || Main.tileSolidTop[(int)tile8.type]) && Collision.CanHit(this.position, this.width, this.height, new Vector2((float)num75, (float)num76) * 16f, 16, 16))
						{
							bool flag9 = false;
							if (Main.tile[num75 - 1, num76].active() || Main.tile[num75 - 1, num76].wall > 0)
							{
								flag9 = true;
							}
							if (Main.tile[num75 + 1, num76].active() || Main.tile[num75 + 1, num76].wall > 0)
							{
								flag9 = true;
							}
							if (Main.tile[num75, num76 - 1].active() || Main.tile[num75, num76 - 1].wall > 0)
							{
								flag9 = true;
							}
							if (Main.tile[num75, num76 + 1].active() || Main.tile[num75, num76 + 1].wall > 0)
							{
								flag9 = true;
							}
							if (Main.tile[num75, num76].active() && Main.tile[num75, num76].type == 11 && (Main.tile[num75, num76].frameX < 18 || Main.tile[num75, num76].frameX > 54))
							{
								flag9 = false;
							}
							if (flag9)
							{
								list14.Add(new Tuple<int, int>(num75, num76));
							}
						}
					}
				}
				if (list14.Count > 0)
				{
					float num77 = -1f;
					Tuple<int, int> tuple15 = list14[0];
					for (int num78 = 0; num78 < list14.Count; num78++)
					{
						float num79 = Vector2.Distance(new Vector2((float)list14[num78].Item1, (float)list14[num78].Item2) * 16f + Vector2.One * 8f, vector);
						if (num77 == -1f || num79 < num77)
						{
							num77 = num79;
							tuple15 = list14[num78];
						}
					}
					if (Collision.InTileBounds(tuple15.Item1, tuple15.Item2, num, num3, num2, num4))
					{
						num5 = tuple15.Item1;
						num6 = tuple15.Item2;
					}
				}
				list14.Clear();
			}
			if (item.createTile > -1 && Main.tileSolid[item.createTile] && !Main.tileSolidTop[item.createTile] && !Main.tileFrameImportant[item.createTile] && num5 == -1 && num6 == -1)
			{
				List<Tuple<int, int>> list15 = new List<Tuple<int, int>>();
				bool flag10 = false;
				if (Main.tile[Player.tileTargetX, Player.tileTargetY].active())
				{
					flag10 = true;
				}
				if (!Collision.InTileBounds(Player.tileTargetX, Player.tileTargetY, num, num3, num2, num4))
				{
					flag10 = true;
				}
				if (!flag10)
				{
					for (int num80 = num; num80 <= num2; num80++)
					{
						for (int num81 = num3; num81 <= num4; num81++)
						{
							Tile tile9 = Main.tile[num80, num81];
							if (!tile9.active() || Main.tileCut[(int)tile9.type])
							{
								bool flag11 = false;
								if (Main.tile[num80 - 1, num81].active() && Main.tileSolid[(int)Main.tile[num80 - 1, num81].type] && !Main.tileSolidTop[(int)Main.tile[num80 - 1, num81].type])
								{
									flag11 = true;
								}
								if (Main.tile[num80 + 1, num81].active() && Main.tileSolid[(int)Main.tile[num80 + 1, num81].type] && !Main.tileSolidTop[(int)Main.tile[num80 + 1, num81].type])
								{
									flag11 = true;
								}
								if (Main.tile[num80, num81 - 1].active() && Main.tileSolid[(int)Main.tile[num80, num81 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num80, num81 - 1].type])
								{
									flag11 = true;
								}
								if (Main.tile[num80, num81 + 1].active() && Main.tileSolid[(int)Main.tile[num80, num81 + 1].type] && !Main.tileSolidTop[(int)Main.tile[num80, num81 + 1].type])
								{
									flag11 = true;
								}
								if (flag11)
								{
									list15.Add(new Tuple<int, int>(num80, num81));
								}
							}
						}
					}
				}
				if (list15.Count > 0)
				{
					float num82 = -1f;
					Tuple<int, int> tuple16 = list15[0];
					for (int num83 = 0; num83 < list15.Count; num83++)
					{
						if (Collision.EmptyTile(list15[num83].Item1, list15[num83].Item2, false))
						{
							float num84 = Vector2.Distance(new Vector2((float)list15[num83].Item1, (float)list15[num83].Item2) * 16f + Vector2.One * 8f, vector);
							if (num82 == -1f || num84 < num82)
							{
								num82 = num84;
								tuple16 = list15[num83];
							}
						}
					}
					if (Collision.InTileBounds(tuple16.Item1, tuple16.Item2, num, num3, num2, num4) && num82 != -1f)
					{
						num5 = tuple16.Item1;
						num6 = tuple16.Item2;
					}
				}
				list15.Clear();
			}
			if (num5 != -1 && num6 != -1)
			{
				Main.smartDigX = (Player.tileTargetX = num5);
				Main.smartDigY = (Player.tileTargetY = num6);
				Main.smartDigShowing = true;
			}
			list.Clear();
		}
		public void SmartitemLookup()
		{
			if (this.controlTorch && this.itemAnimation == 0)
			{
				int num = 0;
				int num2 = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
				int num3 = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
				if (this.gravDir == -1f)
				{
					num3 = (int)((Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16f);
				}
				int num4 = -10;
				int num5 = -10;
				int num6 = -10;
				int num7 = -10;
				int num8 = -10;
				for (int i = 0; i < 50; i++)
				{
					if (this.inventory[i].pick > 0 && num4 == -10)
					{
						num4 = this.inventory[i].tileBoost;
					}
					if (this.inventory[i].axe > 0 && num5 == -10)
					{
						num5 = this.inventory[i].tileBoost;
					}
					if (this.inventory[i].hammer > 0 && num6 == -10)
					{
						num6 = this.inventory[i].tileBoost;
					}
					if ((this.inventory[i].type == 929 || this.inventory[i].type == 1338) && num7 == -10)
					{
						num7 = this.inventory[i].tileBoost;
					}
					if ((this.inventory[i].type == 424 || this.inventory[i].type == 1103) && num8 == -10)
					{
						num8 = this.inventory[i].tileBoost;
					}
				}
				int num9 = 0;
				int num10 = 0;
				if (this.position.X / 16f >= (float)num2)
				{
					num9 = (int)(this.position.X / 16f) - num2;
				}
				if ((this.position.X + (float)this.width) / 16f <= (float)num2)
				{
					num9 = num2 - (int)((this.position.X + (float)this.width) / 16f);
				}
				if (this.position.Y / 16f >= (float)num3)
				{
					num10 = (int)(this.position.Y / 16f) - num3;
				}
				if ((this.position.Y + (float)this.height) / 16f <= (float)num3)
				{
					num10 = num3 - (int)((this.position.Y + (float)this.height) / 16f);
				}
				bool flag = false;
				bool flag2 = false;
				try
				{
					flag2 = (Main.tile[num2, num3].liquid > 0);
					if (Main.tile[num2, num3].active())
					{
						int type = (int)Main.tile[num2, num3].type;
						if (type == 219 && num9 <= num8 + Player.tileRangeX && num10 <= num8 + Player.tileRangeY)
						{
							num = 7;
							flag = true;
						}
						else
						{
							if (type == 209 && num9 <= num7 + Player.tileRangeX && num10 <= num7 + Player.tileRangeY)
							{
								num = 6;
								flag = true;
							}
							else
							{
								if (Main.tileHammer[type] && num9 <= num6 + Player.tileRangeX && num10 <= num6 + Player.tileRangeY)
								{
									num = 1;
									flag = true;
								}
								else
								{
									if (Main.tileAxe[type] && num9 <= num5 + Player.tileRangeX && num10 <= num5 + Player.tileRangeY)
									{
										num = 2;
										flag = true;
									}
									else
									{
										if (num9 <= num4 + Player.tileRangeX && num10 <= num4 + Player.tileRangeY)
										{
											num = 3;
											flag = true;
										}
									}
								}
							}
						}
					}
					else
					{
						if (flag2 && this.wet)
						{
							num = 4;
							flag = true;
						}
					}
				}
				catch
				{
				}
				if (!flag && this.wet)
				{
					num = 4;
				}
				if (num == 0 || num == 4)
				{
					float num11 = Math.Abs((float)Main.mouseX + Main.screenPosition.X - (this.position.X + (float)(this.width / 2)));
					float num12 = Math.Abs((float)Main.mouseY + Main.screenPosition.Y - (this.position.Y + (float)(this.height / 2))) * 1.3f;
					float num13 = (float)Math.Sqrt((double)(num11 * num11 + num12 * num12));
					if (num13 > 200f)
					{
						num = 5;
					}
				}
				for (int j = 0; j < 50; j++)
				{
					int type2 = this.inventory[j].type;
					if (num == 0)
					{
						if (type2 == 8 || type2 == 427 || type2 == 428 || type2 == 429 || type2 == 430 || type2 == 431 || type2 == 432 || type2 == 433 || type2 == 523 || type2 == 974 || type2 == 1245 || type2 == 1333 || type2 == 2274)
						{
							if (this.nonTorch == -1)
							{
								this.nonTorch = this.selectedItem;
							}
							this.selectedItem = j;
							return;
						}
						if (type2 == 282 || type2 == 286)
						{
							if (this.nonTorch == -1)
							{
								this.nonTorch = this.selectedItem;
							}
							this.selectedItem = j;
						}
					}
					else
					{
						if (num == 1)
						{
							if (this.inventory[j].hammer > 0)
							{
								if (this.nonTorch == -1)
								{
									this.nonTorch = this.selectedItem;
								}
								this.selectedItem = j;
								return;
							}
						}
						else
						{
							if (num == 2)
							{
								if (this.inventory[j].axe > 0)
								{
									if (this.nonTorch == -1)
									{
										this.nonTorch = this.selectedItem;
									}
									this.selectedItem = j;
									return;
								}
							}
							else
							{
								if (num == 3)
								{
									if (this.inventory[j].pick > 0)
									{
										if (this.nonTorch == -1)
										{
											this.nonTorch = this.selectedItem;
										}
										this.selectedItem = j;
										return;
									}
								}
								else
								{
									if (num == 4)
									{
										if (this.inventory[j].type != 282 && this.inventory[j].type != 286 && this.inventory[j].type != 930 && (type2 == 8 || type2 == 427 || type2 == 428 || type2 == 429 || type2 == 430 || type2 == 431 || type2 == 432 || type2 == 433 || type2 == 974 || type2 == 1245 || type2 == 2274))
										{
											if (this.nonTorch == -1)
											{
												this.nonTorch = this.selectedItem;
											}
											if (this.inventory[this.selectedItem].createTile != 4)
											{
												this.selectedItem = j;
											}
										}
										else
										{
											if ((type2 == 282 || type2 == 286) && flag2)
											{
												if (this.nonTorch == -1)
												{
													this.nonTorch = this.selectedItem;
												}
												this.selectedItem = j;
												return;
											}
											if (type2 == 930 && flag2)
											{
												bool flag3 = false;
												for (int k = 57; k >= 0; k--)
												{
													if (this.inventory[k].ammo == this.inventory[j].useAmmo)
													{
														flag3 = true;
														break;
													}
												}
												if (flag3)
												{
													if (this.nonTorch == -1)
													{
														this.nonTorch = this.selectedItem;
													}
													this.selectedItem = j;
													return;
												}
											}
											else
											{
												if (type2 == 1333 || type2 == 523)
												{
													if (this.nonTorch == -1)
													{
														this.nonTorch = this.selectedItem;
													}
													this.selectedItem = j;
													return;
												}
											}
										}
									}
									else
									{
										if (num == 5)
										{
											if (type2 == 8 || type2 == 427 || type2 == 428 || type2 == 429 || type2 == 430 || type2 == 431 || type2 == 432 || type2 == 433 || type2 == 523 || type2 == 974 || type2 == 1245 || type2 == 1333 || type2 == 2274)
											{
												if (this.nonTorch == -1)
												{
													this.nonTorch = this.selectedItem;
												}
												if (this.inventory[this.selectedItem].createTile != 4)
												{
													this.selectedItem = j;
												}
											}
											else
											{
												if (type2 == 930)
												{
													bool flag4 = false;
													for (int l = 57; l >= 0; l--)
													{
														if (this.inventory[l].ammo == this.inventory[j].useAmmo)
														{
															flag4 = true;
															break;
														}
													}
													if (flag4)
													{
														if (this.nonTorch == -1)
														{
															this.nonTorch = this.selectedItem;
														}
														this.selectedItem = j;
														return;
													}
												}
												else
												{
													if (type2 == 282 || type2 == 286)
													{
														if (this.nonTorch == -1)
														{
															this.nonTorch = this.selectedItem;
														}
														this.selectedItem = j;
														return;
													}
												}
											}
										}
										else
										{
											if (num == 6)
											{
												int num14 = 929;
												if (Main.tile[num2, num3].frameX >= 72)
												{
													num14 = 1338;
												}
												if (type2 == num14)
												{
													if (this.nonTorch == -1)
													{
														this.nonTorch = this.selectedItem;
													}
													this.selectedItem = j;
													return;
												}
											}
											else
											{
												if (num == 7 && (type2 == 424 || type2 == 1103))
												{
													if (this.nonTorch == -1)
													{
														this.nonTorch = this.selectedItem;
													}
													this.selectedItem = j;
													return;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				return;
			}
			if (this.nonTorch > -1 && this.itemAnimation == 0)
			{
				this.selectedItem = this.nonTorch;
				this.nonTorch = -1;
			}
		}
		public void ResetEffects()
		{
			this.armorSteath = false;
			this.statDefense = 0;
			this.accWatch = 0;
			this.accCompass = 0;
			this.accDepthMeter = 0;
			this.accDivingHelm = false;
			this.lifeRegen = 0;
			this.manaCost = 1f;
			this.meleeSpeed = 1f;
			this.meleeDamage = 1f;
			this.rangedDamage = 1f;
			this.magicDamage = 1f;
			this.minionDamage = 1f;
			this.minionKB = 0f;
			this.moveSpeed = 1f;
			this.boneArmor = false;
			this.honey = false;
			this.frostArmor = false;
			this.rocketBoots = 0;
			this.fireWalk = false;
			this.noKnockback = false;
			this.jumpBoost = false;
			this.noFallDmg = false;
			this.accFlipper = false;
			this.spawnMax = false;
			this.spaceGun = false;
			this.killGuide = false;
			this.killClothier = false;
			this.lavaImmune = false;
			this.gills = false;
			this.slowFall = false;
			this.findTreasure = false;
			this.invis = false;
			this.nightVision = false;
			this.enemySpawns = false;
			this.thorns = false;
			this.aggro = 0;
			this.waterWalk = false;
			this.waterWalk2 = false;
			this.detectCreature = false;
			this.gravControl = false;
			this.bee = false;
			this.gravControl2 = false;
			this.statLifeMax2 = this.statLifeMax;
			this.statManaMax2 = this.statManaMax;
			this.ammoCost80 = false;
			this.ammoCost75 = false;
			this.manaRegenBuff = false;
			this.meleeCrit = 4;
			this.rangedCrit = 4;
			this.magicCrit = 4;
			this.arrowDamage = 1f;
			this.bulletDamage = 1f;
			this.rocketDamage = 1f;
			this.lightOrb = false;
			this.blueFairy = false;
			this.redFairy = false;
			this.greenFairy = false;
			this.wisp = false;
			this.bunny = false;
			this.turtle = false;
			this.eater = false;
			this.skeletron = false;
			this.hornet = false;
			this.zephyrfish = false;
			this.tiki = false;
			this.lizard = false;
			this.parrot = false;
			this.sapling = false;
			this.cSapling = false;
			this.truffle = false;
			this.shadowDodge = false;
			this.palladiumRegen = false;
			this.chaosState = false;
			this.onHitDodge = false;
			this.onHitRegen = false;
			this.onHitPetal = false;
			this.iceBarrier = false;
			this.maxMinions = 1;
			this.ammoBox = false;
			this.ammoPotion = false;
			this.penguin = false;
			this.inferno = false;
			this.manaMagnet = false;
			this.lifeMagnet = false;
			this.lifeForce = false;
			this.dangerSense = false;
			this.endurance = 0f;
			this.calmed = false;
			this.beetleOrbs = 0;
			this.beetleBuff = false;
			this.miniMinotaur = false;
			this.fishingSkill = 0;
			this.cratePotion = false;
			this.sonarPotion = false;
			this.accTackleBox = false;
			this.accFishingLine = false;
			this.wallSpeed = 1f;
			this.tileSpeed = 1f;
			this.autoPaint = false;
			this.manaSick = false;
			this.puppy = false;
			this.grinch = false;
			this.blackCat = false;
			this.spider = false;
			this.squashling = false;
			this.magicCuffs = false;
			this.coldDash = false;
			this.magicQuiver = false;
			this.magmaStone = false;
			this.lavaRose = false;
			this.eyeSpring = false;
			this.snowman = false;
			this.scope = false;
			this.panic = false;
			this.dino = false;
			this.crystalLeaf = false;
			this.pygmy = false;
			this.raven = false;
			this.slime = false;
			this.hornetMinion = false;
			this.impMinion = false;
			this.twinsMinion = false;
			this.spiderMinion = false;
			this.pirateMinion = false;
			this.sharknadoMinion = false;
			this.chilled = false;
			this.frozen = false;
			this.ichor = false;
			this.manaRegenBonus = 0;
			this.manaRegenDelayBonus = 0;
			this.carpet = false;
			this.iceSkate = false;
			this.dash = 0;
			this.spikedBoots = 0;
			this.blackBelt = false;
			this.lavaMax = 0;
			this.archery = false;
			this.poisoned = false;
			this.venom = false;
			this.blind = false;
			this.blackout = false;
			this.onFire = false;
			this.dripping = false;
			this.drippingSlime = false;
			this.burned = false;
			this.suffocating = false;
			this.onFire2 = false;
			this.onFrostBurn = false;
			this.frostBurn = false;
			this.noItems = false;
			this.blockRange = 0;
			this.pickSpeed = 1f;
			this.wereWolf = false;
			this.rulerAcc = false;
			this.bleed = false;
			this.confused = false;
			this.wings = 0;
			this.wingsLogic = 0;
			this.wingTimeMax = 0;
			this.brokenArmor = false;
			this.silence = false;
			this.slow = false;
			this.gross = false;
			this.tongued = false;
			this.kbGlove = false;
			this.kbBuff = false;
			this.starCloak = false;
			this.longInvince = false;
			this.pStone = false;
			this.manaFlower = false;
			this.crimsonRegen = false;
			this.ghostHeal = false;
			this.ghostHurt = false;
			this.turtleArmor = false;
			this.turtleThorns = false;
			this.spiderArmor = false;
			this.loveStruck = false;
			this.stinky = false;
			this.resistCold = false;
			this.ignoreWater = false;
			this.meleeEnchant = 0;
			this.discount = false;
			this.coins = false;
			this.doubleJump2 = false;
			this.doubleJump3 = false;
			this.doubleJump4 = false;
			this.paladinBuff = false;
			this.paladinGive = false;
			this.autoJump = false;
			this.justJumped = false;
			this.jumpSpeedBoost = 0f;
			this.extraFall = 0;
			if (this.whoAmi == Main.myPlayer)
			{
				Player.tileRangeX = 5;
				Player.tileRangeY = 4;
			}
			this.mount.CheckMountBuff(this);
		}
		public void UpdateImmunity()
		{
			if (this.immune)
			{
				this.immuneTime--;
				if (this.immuneTime <= 0)
				{
					this.immune = false;
				}
				this.immuneAlpha += this.immuneAlphaDirection * 50;
				if (this.immuneAlpha <= 50)
				{
					this.immuneAlphaDirection = 1;
					return;
				}
				if (this.immuneAlpha >= 205)
				{
					this.immuneAlphaDirection = -1;
					return;
				}
			}
			else
			{
				this.immuneAlpha = 0;
			}
		}
		public void UpdateLifeRegen()
		{
			if (this.poisoned)
			{
				if (this.lifeRegen > 0)
				{
					this.lifeRegen = 0;
				}
				this.lifeRegenTime = 0;
				this.lifeRegen -= 4;
			}
			else
			{
				if (this.venom)
				{
					if (this.lifeRegen > 0)
					{
						this.lifeRegen = 0;
					}
					this.lifeRegenTime = 0;
					this.lifeRegen -= 12;
				}
			}
			if (this.onFire)
			{
				if (this.lifeRegen > 0)
				{
					this.lifeRegen = 0;
				}
				this.lifeRegenTime = 0;
				this.lifeRegen -= 8;
			}
			if (this.onFrostBurn)
			{
				if (this.lifeRegen > 0)
				{
					this.lifeRegen = 0;
				}
				this.lifeRegenTime = 0;
				this.lifeRegen -= 12;
			}
			if (this.onFire2)
			{
				if (this.lifeRegen > 0)
				{
					this.lifeRegen = 0;
				}
				this.lifeRegenTime = 0;
				this.lifeRegen -= 12;
			}
			if (this.burned)
			{
				if (this.lifeRegen > 0)
				{
					this.lifeRegen = 0;
				}
				this.lifeRegenTime = 0;
				this.lifeRegen -= 60;
				this.moveSpeed *= 0.5f;
			}
			if (this.suffocating)
			{
				if (this.lifeRegen > 0)
				{
					this.lifeRegen = 0;
				}
				this.lifeRegenTime = 0;
				this.lifeRegen -= 40;
			}
			this.lifeRegenTime++;
			if (this.crimsonRegen)
			{
				this.lifeRegenTime++;
			}
			if (this.honey)
			{
				this.lifeRegenTime += 2;
				this.lifeRegen += 2;
			}
			if (this.whoAmi == Main.myPlayer && Main.campfire)
			{
				this.lifeRegen++;
			}
			if (this.whoAmi == Main.myPlayer && Main.heartLantern)
			{
				this.lifeRegen += 2;
			}
			if (this.bleed)
			{
				this.lifeRegenTime = 0;
			}
			float num = 0f;
			if (this.lifeRegenTime >= 300)
			{
				num += 1f;
			}
			if (this.lifeRegenTime >= 600)
			{
				num += 1f;
			}
			if (this.lifeRegenTime >= 900)
			{
				num += 1f;
			}
			if (this.lifeRegenTime >= 1200)
			{
				num += 1f;
			}
			if (this.lifeRegenTime >= 1500)
			{
				num += 1f;
			}
			if (this.lifeRegenTime >= 1800)
			{
				num += 1f;
			}
			if (this.lifeRegenTime >= 2400)
			{
				num += 1f;
			}
			if (this.lifeRegenTime >= 3000)
			{
				num += 1f;
			}
			if (this.lifeRegenTime >= 3600)
			{
				num += 1f;
				this.lifeRegenTime = 3600;
			}
			if (this.velocity.X == 0f || this.grappling[0] > 0)
			{
				num *= 1.25f;
			}
			else
			{
				num *= 0.5f;
			}
			if (this.crimsonRegen)
			{
				num *= 1.5f;
			}
			if (this.whoAmi == Main.myPlayer && Main.campfire)
			{
				num *= 1.1f;
			}
			float num2 = (float)this.statLifeMax2 / 400f * 0.85f + 0.15f;
			num *= num2;
			this.lifeRegen += (int)Math.Round((double)num);
			this.lifeRegenCount += this.lifeRegen;
			if (this.palladiumRegen)
			{
				this.lifeRegenCount += 6;
			}
			while (this.lifeRegenCount >= 120)
			{
				this.lifeRegenCount -= 120;
				if (this.statLife < this.statLifeMax2)
				{
					this.statLife++;
					if (this.crimsonRegen)
					{
						for (int i = 0; i < 10; i++)
						{
							int num3 = Dust.NewDust(this.position, this.width, this.height, 5, 0f, 0f, 175, default(Color), 1.75f);
							Main.dust[num3].noGravity = true;
							Main.dust[num3].velocity *= 0.75f;
							int num4 = Main.rand.Next(-40, 41);
							int num5 = Main.rand.Next(-40, 41);
							Dust expr_448_cp_0 = Main.dust[num3];
							expr_448_cp_0.position.X = expr_448_cp_0.position.X + (float)num4;
							Dust expr_463_cp_0 = Main.dust[num3];
							expr_463_cp_0.position.Y = expr_463_cp_0.position.Y + (float)num5;
							Main.dust[num3].velocity.X = (float)(-(float)num4) * 0.075f;
							Main.dust[num3].velocity.Y = (float)(-(float)num5) * 0.075f;
						}
					}
				}
				if (this.statLife > this.statLifeMax2)
				{
					this.statLife = this.statLifeMax2;
				}
			}
			if (!this.burned)
			{
				if (!this.suffocating)
				{
					while (this.lifeRegenCount <= -120)
					{
						if (this.lifeRegenCount <= -480)
						{
							this.lifeRegenCount += 480;
							this.statLife -= 4;
							CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(255, 60, 70, 255), string.Concat(4), false, true);
						}
						else
						{
							if (this.lifeRegenCount <= -360)
							{
								this.lifeRegenCount += 360;
								this.statLife -= 3;
								CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(255, 60, 70, 255), string.Concat(3), false, true);
							}
							else
							{
								if (this.lifeRegenCount <= -240)
								{
									this.lifeRegenCount += 240;
									this.statLife -= 2;
									CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(255, 60, 70, 255), string.Concat(2), false, true);
								}
								else
								{
									this.lifeRegenCount += 120;
									this.statLife--;
									CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(255, 60, 70, 255), string.Concat(1), false, true);
								}
							}
						}
						if (this.statLife <= 0 && this.whoAmi == Main.myPlayer)
						{
							if (this.poisoned || this.venom)
							{
								this.KillMe(10.0, 0, false, " " + Lang.dt[0]);
							}
							else
							{
								this.KillMe(10.0, 0, false, " " + Lang.dt[1]);
							}
						}
					}
					return;
				}
			}
			while (this.lifeRegenCount <= -600)
			{
				this.lifeRegenCount += 600;
				this.statLife -= 5;
				CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(255, 60, 70, 255), string.Concat(5), false, true);
				if (this.statLife <= 0 && this.whoAmi == Main.myPlayer)
				{
					if (this.suffocating)
					{
						this.KillMe(10.0, 0, false, " " + Lang.dt[2]);
					}
					else
					{
						this.KillMe(10.0, 0, false, " " + Lang.dt[1]);
					}
				}
			}
		}
		public void UpdateManaRegen()
		{
			if (this.manaRegenDelay > 0)
			{
				this.manaRegenDelay--;
				this.manaRegenDelay -= this.manaRegenDelayBonus;
				if ((this.velocity.X == 0f && this.velocity.Y == 0f) || this.grappling[0] >= 0 || this.manaRegenBuff)
				{
					this.manaRegenDelay--;
				}
			}
			if (this.manaRegenBuff && this.manaRegenDelay > 20)
			{
				this.manaRegenDelay = 20;
			}
			if (this.manaRegenDelay <= 0)
			{
				this.manaRegenDelay = 0;
				this.manaRegen = this.statManaMax2 / 7 + 1 + this.manaRegenBonus;
				if ((this.velocity.X == 0f && this.velocity.Y == 0f) || this.grappling[0] >= 0 || this.manaRegenBuff)
				{
					this.manaRegen += this.statManaMax2 / 2;
				}
				float num = (float)this.statMana / (float)this.statManaMax2 * 0.8f + 0.2f;
				if (this.manaRegenBuff)
				{
					num = 1f;
				}
				this.manaRegen = (int)((double)((float)this.manaRegen * num) * 1.15);
			}
			else
			{
				this.manaRegen = 0;
			}
			this.manaRegenCount += this.manaRegen;
			while (this.manaRegenCount >= 120)
			{
				bool flag = false;
				this.manaRegenCount -= 120;
				if (this.statMana < this.statManaMax2)
				{
					this.statMana++;
					flag = true;
				}
				if (this.statMana >= this.statManaMax2)
				{
					if (this.whoAmi == Main.myPlayer && flag)
					{
						Main.PlaySound(25, -1, -1, 1);
						for (int i = 0; i < 5; i++)
						{
							int num2 = Dust.NewDust(this.position, this.width, this.height, 45, 0f, 0f, 255, default(Color), (float)Main.rand.Next(20, 26) * 0.1f);
							Main.dust[num2].noLight = true;
							Main.dust[num2].noGravity = true;
							Main.dust[num2].velocity *= 0.5f;
						}
					}
					this.statMana = this.statManaMax2;
				}
			}
		}
		public void UpdateJumpHeight()
		{
			if (this.mount.Active)
			{
				Player.jumpHeight = this.mount.JumpHeight(this.velocity.X);
				Player.jumpSpeed = this.mount.JumpSpeed(this.velocity.X);
			}
			else
			{
				if (this.jumpBoost)
				{
					Player.jumpHeight = 20;
					Player.jumpSpeed = 6.51f;
				}
				if (this.wereWolf)
				{
					Player.jumpHeight += 2;
					Player.jumpSpeed += 0.2f;
				}
				Player.jumpSpeed += this.jumpSpeedBoost;
			}
			if (this.sticky)
			{
				Player.jumpHeight /= 10;
				Player.jumpSpeed /= 5f;
			}
		}
		public void FindPulley()
		{
			if (this.controlUp || this.controlDown)
			{
				int num = (int)(this.position.X + (float)(this.width / 2)) / 16;
				int num2 = (int)(this.position.Y - 8f) / 16;
				if (Main.tile[num, num2] != null && Main.tile[num, num2].active() && Main.tileRope[(int)Main.tile[num, num2].type])
				{
					float num3 = this.position.Y;
					if (Main.tile[num, num2 - 1] == null)
					{
						Main.tile[num, num2 - 1] = new Tile();
					}
					if (Main.tile[num, num2 + 1] == null)
					{
						Main.tile[num, num2 + 1] = new Tile();
					}
					if ((!Main.tile[num, num2 - 1].active() || !Main.tileRope[(int)Main.tile[num, num2 - 1].type]) && (!Main.tile[num, num2 + 1].active() || !Main.tileRope[(int)Main.tile[num, num2 + 1].type]))
					{
						num3 = (float)(num2 * 16 + 22);
					}
					float num4 = (float)(num * 16 + 8 - this.width / 2 + 6 * this.direction);
					int num5 = num * 16 + 8 - this.width / 2 + 6;
					int num6 = num * 16 + 8 - this.width / 2;
					int num7 = num * 16 + 8 - this.width / 2 + -6;
					int num8 = 1;
					float num9 = Math.Abs(this.position.X - (float)num5);
					if (Math.Abs(this.position.X - (float)num6) < num9)
					{
						num8 = 2;
						num9 = Math.Abs(this.position.X - (float)num6);
					}
					if (Math.Abs(this.position.X - (float)num7) < num9)
					{
						num8 = 3;
						num9 = Math.Abs(this.position.X - (float)num7);
					}
					if (num8 == 1)
					{
						num4 = (float)num5;
						this.pulleyDir = 2;
						this.direction = 1;
					}
					if (num8 == 2)
					{
						num4 = (float)num6;
						this.pulleyDir = 1;
					}
					if (num8 == 3)
					{
						num4 = (float)num7;
						this.pulleyDir = 2;
						this.direction = -1;
					}
					if (!Collision.SolidCollision(new Vector2(num4, this.position.Y), this.width, this.height))
					{
						if (this.whoAmi == Main.myPlayer)
						{
							Main.cameraX = Main.cameraX + this.position.X - num4;
						}
						this.pulley = true;
						this.position.X = num4;
						this.gfxOffY = this.position.Y - num3;
						this.stepSpeed = 2.5f;
						this.position.Y = num3;
						this.velocity.X = 0f;
						return;
					}
					num4 = (float)num5;
					this.pulleyDir = 2;
					this.direction = 1;
					if (!Collision.SolidCollision(new Vector2(num4, this.position.Y), this.width, this.height))
					{
						if (this.whoAmi == Main.myPlayer)
						{
							Main.cameraX = Main.cameraX + this.position.X - num4;
						}
						this.pulley = true;
						this.position.X = num4;
						this.gfxOffY = this.position.Y - num3;
						this.stepSpeed = 2.5f;
						this.position.Y = num3;
						this.velocity.X = 0f;
						return;
					}
					num4 = (float)num7;
					this.pulleyDir = 2;
					this.direction = -1;
					if (!Collision.SolidCollision(new Vector2(num4, this.position.Y), this.width, this.height))
					{
						if (this.whoAmi == Main.myPlayer)
						{
							Main.cameraX = Main.cameraX + this.position.X - num4;
						}
						this.pulley = true;
						this.position.X = num4;
						this.gfxOffY = this.position.Y - num3;
						this.stepSpeed = 2.5f;
						this.position.Y = num3;
						this.velocity.X = 0f;
					}
				}
			}
		}
		public void HorizontalMovement()
		{
			if (this.trackBoost != 0f)
			{
				this.velocity.X = this.velocity.X + this.trackBoost;
				this.trackBoost = 0f;
				if (this.velocity.X < 0f)
				{
					if (this.velocity.X < -this.maxRunSpeed)
					{
						this.velocity.X = -this.maxRunSpeed;
					}
				}
				else
				{
					if (this.velocity.X > this.maxRunSpeed)
					{
						this.velocity.X = this.maxRunSpeed;
					}
				}
			}
			if (this.controlLeft && this.velocity.X > -this.maxRunSpeed)
			{
				if (!this.mount.Active || this.mount.Type != 6 || this.velocity.Y == 0f)
				{
					if (this.velocity.X > this.runSlowdown)
					{
						this.velocity.X = this.velocity.X - this.runSlowdown;
					}
					this.velocity.X = this.velocity.X - this.runAcceleration;
				}
				if (this.onWrongGround)
				{
					if (this.velocity.X < -this.runSlowdown)
					{
						this.velocity.X = this.velocity.X + this.runSlowdown;
					}
					else
					{
						this.velocity.X = 0f;
					}
				}
				if (this.mount.Active && this.mount.Type == 6 && !this.onWrongGround)
				{
					if (this.velocity.X < 0f)
					{
						this.direction = -1;
						return;
					}
					if (this.velocity.Y == 0f)
					{
						Main.PlaySound(2, (int)this.position.X + this.width / 2, (int)this.position.Y + this.height / 2, 55);
						if ((double)Math.Abs(this.velocity.X) > (double)this.maxRunSpeed * 0.66)
						{
							if (Main.rand.Next(2) == 0)
							{
								Minecart.WheelSparks(this.position + this.velocity * 0.66f, this.width, this.height, 1);
							}
							if (Main.rand.Next(2) == 0)
							{
								Minecart.WheelSparks(this.position + this.velocity * 0.33f, this.width, this.height, 1);
							}
							if (Main.rand.Next(2) == 0)
							{
								Minecart.WheelSparks(this.position, this.width, this.height, 1);
								return;
							}
						}
						else
						{
							if ((double)Math.Abs(this.velocity.X) <= (double)this.maxRunSpeed * 0.33)
							{
								Minecart.WheelSparks(this.position, this.width, this.height, 1);
								return;
							}
							if (Main.rand.Next(3) != 0)
							{
								Minecart.WheelSparks(this.position + this.velocity * 0.5f, this.width, this.height, 1);
							}
							if (Main.rand.Next(3) != 0)
							{
								Minecart.WheelSparks(this.position, this.width, this.height, 1);
								return;
							}
						}
					}
				}
				else
				{
					if (!this.sandStorm && (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn))
					{
						this.direction = -1;
						return;
					}
				}
			}
			else
			{
				if (this.controlRight && this.velocity.X < this.maxRunSpeed)
				{
					if (!this.mount.Active || this.mount.Type != 6 || this.velocity.Y == 0f)
					{
						if (this.velocity.X < -this.runSlowdown)
						{
							this.velocity.X = this.velocity.X + this.runSlowdown;
						}
						this.velocity.X = this.velocity.X + this.runAcceleration;
					}
					if (this.onWrongGround)
					{
						if (this.velocity.X > this.runSlowdown)
						{
							this.velocity.X = this.velocity.X - this.runSlowdown;
						}
						else
						{
							this.velocity.X = 0f;
						}
					}
					if (this.mount.Active && this.mount.Type == 6 && !this.onWrongGround)
					{
						if (this.velocity.X > 0f)
						{
							this.direction = 1;
							return;
						}
						if (this.velocity.Y == 0f)
						{
							Main.PlaySound(2, (int)this.position.X + this.width / 2, (int)this.position.Y + this.height / 2, 55);
							if ((double)Math.Abs(this.velocity.X) > (double)this.maxRunSpeed * 0.66)
							{
								if (Main.rand.Next(2) == 0)
								{
									Minecart.WheelSparks(this.position + this.velocity * 0.66f, this.width, this.height, 1);
								}
								if (Main.rand.Next(2) == 0)
								{
									Minecart.WheelSparks(this.position + this.velocity * 0.33f, this.width, this.height, 1);
								}
								if (Main.rand.Next(2) == 0)
								{
									Minecart.WheelSparks(this.position, this.width, this.height, 1);
									return;
								}
							}
							else
							{
								if ((double)Math.Abs(this.velocity.X) <= (double)this.maxRunSpeed * 0.33)
								{
									Minecart.WheelSparks(this.position, this.width, this.height, 1);
									return;
								}
								if (Main.rand.Next(3) != 0)
								{
									Minecart.WheelSparks(this.position + this.velocity * 0.5f, this.width, this.height, 1);
								}
								if (Main.rand.Next(3) != 0)
								{
									Minecart.WheelSparks(this.position, this.width, this.height, 1);
									return;
								}
							}
						}
					}
					else
					{
						if (!this.sandStorm && (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn))
						{
							this.direction = 1;
							return;
						}
					}
				}
				else
				{
					if (this.controlLeft && this.velocity.X > -this.accRunSpeed && this.dashDelay >= 0)
					{
						if (this.mount.Active && this.mount.Type == 6)
						{
							if (this.velocity.X < 0f)
							{
								this.direction = -1;
							}
						}
						else
						{
							if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
							{
								this.direction = -1;
							}
						}
						if (this.velocity.Y == 0f || this.wingsLogic > 0 || this.mount.CanFly)
						{
							if (this.velocity.X > this.runSlowdown)
							{
								this.velocity.X = this.velocity.X - this.runSlowdown;
							}
							this.velocity.X = this.velocity.X - this.runAcceleration * 0.2f;
							if (this.wingsLogic > 0)
							{
								this.velocity.X = this.velocity.X - this.runAcceleration * 0.2f;
							}
						}
						if (this.onWrongGround)
						{
							if (this.velocity.X < this.runSlowdown)
							{
								this.velocity.X = this.velocity.X + this.runSlowdown;
							}
							else
							{
								this.velocity.X = 0f;
							}
						}
						if (this.velocity.X < -(this.accRunSpeed + this.maxRunSpeed) / 2f && this.velocity.Y == 0f && !this.mount.Active)
						{
							int num = 0;
							if (this.gravDir == -1f)
							{
								num -= this.height;
							}
							if (this.runSoundDelay == 0 && this.velocity.Y == 0f)
							{
								Main.PlaySound(17, (int)this.position.X, (int)this.position.Y, 1);
								this.runSoundDelay = 9;
							}
							if (this.wings == 3)
							{
								int num2 = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float)this.height + (float)num), this.width + 8, 4, 186, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 50, default(Color), 1.5f);
								Main.dust[num2].velocity *= 0.025f;
								num2 = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float)this.height + (float)num), this.width + 8, 4, 186, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 50, default(Color), 1.5f);
								Main.dust[num2].velocity *= 0.2f;
								return;
							}
							if (this.coldDash)
							{
								for (int i = 0; i < 2; i++)
								{
									int num3;
									if (i == 0)
									{
										num3 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)this.height + this.gfxOffY), this.width / 2, 6, 76, 0f, 0f, 0, default(Color), 1.35f);
									}
									else
									{
										num3 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2), this.position.Y + (float)this.height + this.gfxOffY), this.width / 2, 6, 76, 0f, 0f, 0, default(Color), 1.35f);
									}
									Main.dust[num3].scale *= 1f + (float)Main.rand.Next(20, 40) * 0.01f;
									Main.dust[num3].noGravity = true;
									Main.dust[num3].noLight = true;
									Main.dust[num3].velocity *= 0.001f;
									Dust expr_AF5_cp_0 = Main.dust[num3];
									expr_AF5_cp_0.velocity.Y = expr_AF5_cp_0.velocity.Y - 0.003f;
								}
								return;
							}
							int num4 = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float)this.height + (float)num), this.width + 8, 4, 16, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 50, default(Color), 1.5f);
							Main.dust[num4].velocity.X = Main.dust[num4].velocity.X * 0.2f;
							Main.dust[num4].velocity.Y = Main.dust[num4].velocity.Y * 0.2f;
							return;
						}
					}
					else
					{
						if (this.controlRight && this.velocity.X < this.accRunSpeed)
						{
							if (this.mount.Active && this.mount.Type == 6)
							{
								if (this.velocity.X > 0f)
								{
									this.direction = -1;
								}
							}
							else
							{
								if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
								{
									this.direction = 1;
								}
							}
							if (this.velocity.Y == 0f || this.wingsLogic > 0 || this.mount.CanFly)
							{
								if (this.velocity.X < -this.runSlowdown)
								{
									this.velocity.X = this.velocity.X + this.runSlowdown;
								}
								this.velocity.X = this.velocity.X + this.runAcceleration * 0.2f;
								if (this.wingsLogic > 0)
								{
									this.velocity.X = this.velocity.X + this.runAcceleration * 0.2f;
								}
							}
							if (this.onWrongGround)
							{
								if (this.velocity.X > this.runSlowdown)
								{
									this.velocity.X = this.velocity.X - this.runSlowdown;
								}
								else
								{
									this.velocity.X = 0f;
								}
							}
							if (this.velocity.X > (this.accRunSpeed + this.maxRunSpeed) / 2f && this.velocity.Y == 0f && !this.mount.Active)
							{
								int num5 = 0;
								if (this.gravDir == -1f)
								{
									num5 -= this.height;
								}
								if (this.runSoundDelay == 0 && this.velocity.Y == 0f)
								{
									Main.PlaySound(17, (int)this.position.X, (int)this.position.Y, 1);
									this.runSoundDelay = 9;
								}
								if (this.wings == 3)
								{
									int num6 = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float)this.height + (float)num5), this.width + 8, 4, 186, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 50, default(Color), 1.5f);
									Main.dust[num6].velocity *= 0.025f;
									num6 = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float)this.height + (float)num5), this.width + 8, 4, 186, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 50, default(Color), 1.5f);
									Main.dust[num6].velocity *= 0.2f;
									return;
								}
								if (this.coldDash)
								{
									for (int j = 0; j < 2; j++)
									{
										int num7;
										if (j == 0)
										{
											num7 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)this.height + this.gfxOffY), this.width / 2, 6, 76, 0f, 0f, 0, default(Color), 1.35f);
										}
										else
										{
											num7 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2), this.position.Y + (float)this.height + this.gfxOffY), this.width / 2, 6, 76, 0f, 0f, 0, default(Color), 1.35f);
										}
										Main.dust[num7].scale *= 1f + (float)Main.rand.Next(20, 40) * 0.01f;
										Main.dust[num7].noGravity = true;
										Main.dust[num7].noLight = true;
										Main.dust[num7].velocity *= 0.001f;
										Dust expr_104D_cp_0 = Main.dust[num7];
										expr_104D_cp_0.velocity.Y = expr_104D_cp_0.velocity.Y - 0.003f;
									}
									return;
								}
								int num8 = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float)this.height + (float)num5), this.width + 8, 4, 16, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 50, default(Color), 1.5f);
								Main.dust[num8].velocity.X = Main.dust[num8].velocity.X * 0.2f;
								Main.dust[num8].velocity.Y = Main.dust[num8].velocity.Y * 0.2f;
								return;
							}
						}
						else
						{
							if (this.mount.Active && this.mount.Type == 6 && Math.Abs(this.velocity.X) >= 1f)
							{
								if (this.onWrongGround)
								{
									if (this.velocity.X > 0f)
									{
										if (this.velocity.X > this.runSlowdown)
										{
											this.velocity.X = this.velocity.X - this.runSlowdown;
										}
										else
										{
											this.velocity.X = 0f;
										}
									}
									else
									{
										if (this.velocity.X < 0f)
										{
											if (this.velocity.X < -this.runSlowdown)
											{
												this.velocity.X = this.velocity.X + this.runSlowdown;
											}
											else
											{
												this.velocity.X = 0f;
											}
										}
									}
								}
								if (this.velocity.X > this.maxRunSpeed)
								{
									this.velocity.X = this.maxRunSpeed;
								}
								if (this.velocity.X < -this.maxRunSpeed)
								{
									this.velocity.X = -this.maxRunSpeed;
									return;
								}
							}
							else
							{
								if (this.velocity.Y == 0f)
								{
									if (this.velocity.X > this.runSlowdown)
									{
										this.velocity.X = this.velocity.X - this.runSlowdown;
										return;
									}
									if (this.velocity.X < -this.runSlowdown)
									{
										this.velocity.X = this.velocity.X + this.runSlowdown;
										return;
									}
									this.velocity.X = 0f;
									return;
								}
								else
								{
									if ((double)this.velocity.X > (double)this.runSlowdown * 0.5)
									{
										this.velocity.X = this.velocity.X - this.runSlowdown * 0.5f;
										return;
									}
									if ((double)this.velocity.X < (double)(-(double)this.runSlowdown) * 0.5)
									{
										this.velocity.X = this.velocity.X + this.runSlowdown * 0.5f;
										return;
									}
									this.velocity.X = 0f;
								}
							}
						}
					}
				}
			}
		}
		public void JumpMovement()
		{
			if (this.controlJump)
			{
				bool flag = false;
				if (this.mount.Active && this.mount.Type == 3 && this.wetSlime > 0)
				{
					flag = true;
				}
				if (this.jump > 0)
				{
					if (this.velocity.Y == 0f)
					{
						this.jump = 0;
					}
					else
					{
						this.velocity.Y = -Player.jumpSpeed * this.gravDir;
						if (this.merman && (!this.mount.Active || this.mount.Type != 6))
						{
							if (this.swimTime <= 10)
							{
								this.swimTime = 30;
							}
						}
						else
						{
							this.jump--;
						}
					}
				}
				else
				{
					if ((this.sliding || this.velocity.Y == 0f || flag || this.jumpAgain || this.jumpAgain2 || this.jumpAgain3 || this.jumpAgain4 || (this.wet && this.accFlipper && (!this.mount.Active || this.mount.Type != 6))) && (this.releaseJump || (this.autoJump && (this.velocity.Y == 0f || this.sliding))))
					{
						if (this.sliding || this.velocity.Y == 0f)
						{
							this.justJumped = true;
						}
						bool flag2 = false;
						if (this.wet && this.accFlipper)
						{
							if (this.swimTime == 0)
							{
								this.swimTime = 30;
							}
							flag2 = true;
						}
						bool flag3 = false;
						bool flag4 = false;
						bool flag5 = false;
						if (!flag)
						{
							if (this.jumpAgain2)
							{
								flag3 = true;
								this.jumpAgain2 = false;
							}
							else
							{
								if (this.jumpAgain3)
								{
									flag4 = true;
									this.jumpAgain3 = false;
								}
								else
								{
									if (this.jumpAgain4)
									{
										this.jumpAgain4 = false;
										flag5 = true;
									}
									else
									{
										this.jumpAgain = false;
									}
								}
							}
						}
						this.canRocket = false;
						this.rocketRelease = false;
						if ((this.velocity.Y == 0f || this.sliding || (this.autoJump && this.justJumped)) && this.doubleJump)
						{
							this.jumpAgain = true;
						}
						if ((this.velocity.Y == 0f || this.sliding || (this.autoJump && this.justJumped)) && this.doubleJump2)
						{
							this.jumpAgain2 = true;
						}
						if ((this.velocity.Y == 0f || this.sliding || (this.autoJump && this.justJumped)) && this.doubleJump3)
						{
							this.jumpAgain3 = true;
						}
						if ((this.velocity.Y == 0f || this.sliding || (this.autoJump && this.justJumped)) && this.doubleJump4)
						{
							this.jumpAgain4 = true;
						}
						if (this.velocity.Y == 0f || flag2 || this.sliding || flag)
						{
							this.velocity.Y = -Player.jumpSpeed * this.gravDir;
							this.jump = Player.jumpHeight;
							if (this.sliding)
							{
								this.velocity.X = (float)(3 * -(float)this.slideDir);
							}
						}
						else
						{
							if (flag3)
							{
								this.dJumpEffect2 = true;
								float arg_366_0 = this.gravDir;
								Main.PlaySound(16, (int)this.position.X, (int)this.position.Y, 1);
								this.velocity.Y = -Player.jumpSpeed * this.gravDir;
								this.jump = Player.jumpHeight * 3;
							}
							else
							{
								if (flag4)
								{
									this.dJumpEffect3 = true;
									float arg_3C7_0 = this.gravDir;
									Main.PlaySound(16, (int)this.position.X, (int)this.position.Y, 1);
									this.velocity.Y = -Player.jumpSpeed * this.gravDir;
									this.jump = (int)((double)Player.jumpHeight * 1.5);
								}
								else
								{
									if (flag5)
									{
										this.dJumpEffect4 = true;
										int num = this.height;
										if (this.gravDir == -1f)
										{
											num = 0;
										}
										Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 16);
										this.velocity.Y = -Player.jumpSpeed * this.gravDir;
										this.jump = Player.jumpHeight * 2;
										for (int i = 0; i < 10; i++)
										{
											int num2 = Dust.NewDust(new Vector2(this.position.X - 34f, this.position.Y + (float)num - 16f), 102, 32, 188, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 100, default(Color), 1.5f);
											Main.dust[num2].velocity.X = Main.dust[num2].velocity.X * 0.5f - this.velocity.X * 0.1f;
											Main.dust[num2].velocity.Y = Main.dust[num2].velocity.Y * 0.5f - this.velocity.Y * 0.3f;
										}
										int num3 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 16f, this.position.Y + (float)num - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(435, 438), 1f);
										Main.gore[num3].velocity.X = Main.gore[num3].velocity.X * 0.1f - this.velocity.X * 0.1f;
										Main.gore[num3].velocity.Y = Main.gore[num3].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
										num3 = Gore.NewGore(new Vector2(this.position.X - 36f, this.position.Y + (float)num - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(435, 438), 1f);
										Main.gore[num3].velocity.X = Main.gore[num3].velocity.X * 0.1f - this.velocity.X * 0.1f;
										Main.gore[num3].velocity.Y = Main.gore[num3].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
										num3 = Gore.NewGore(new Vector2(this.position.X + (float)this.width + 4f, this.position.Y + (float)num - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(435, 438), 1f);
										Main.gore[num3].velocity.X = Main.gore[num3].velocity.X * 0.1f - this.velocity.X * 0.1f;
										Main.gore[num3].velocity.Y = Main.gore[num3].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
									}
									else
									{
										this.dJumpEffect = true;
										int num4 = this.height;
										if (this.gravDir == -1f)
										{
											num4 = 0;
										}
										Main.PlaySound(16, (int)this.position.X, (int)this.position.Y, 1);
										this.velocity.Y = -Player.jumpSpeed * this.gravDir;
										this.jump = (int)((double)Player.jumpHeight * 0.75);
										for (int j = 0; j < 10; j++)
										{
											int num5 = Dust.NewDust(new Vector2(this.position.X - 34f, this.position.Y + (float)num4 - 16f), 102, 32, 16, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 100, default(Color), 1.5f);
											Main.dust[num5].velocity.X = Main.dust[num5].velocity.X * 0.5f - this.velocity.X * 0.1f;
											Main.dust[num5].velocity.Y = Main.dust[num5].velocity.Y * 0.5f - this.velocity.Y * 0.3f;
										}
										int num6 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 16f, this.position.Y + (float)num4 - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14), 1f);
										Main.gore[num6].velocity.X = Main.gore[num6].velocity.X * 0.1f - this.velocity.X * 0.1f;
										Main.gore[num6].velocity.Y = Main.gore[num6].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
										num6 = Gore.NewGore(new Vector2(this.position.X - 36f, this.position.Y + (float)num4 - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14), 1f);
										Main.gore[num6].velocity.X = Main.gore[num6].velocity.X * 0.1f - this.velocity.X * 0.1f;
										Main.gore[num6].velocity.Y = Main.gore[num6].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
										num6 = Gore.NewGore(new Vector2(this.position.X + (float)this.width + 4f, this.position.Y + (float)num4 - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14), 1f);
										Main.gore[num6].velocity.X = Main.gore[num6].velocity.X * 0.1f - this.velocity.X * 0.1f;
										Main.gore[num6].velocity.Y = Main.gore[num6].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
									}
								}
							}
						}
					}
				}
				this.releaseJump = false;
				return;
			}
			this.jump = 0;
			this.releaseJump = true;
			this.rocketRelease = true;
		}
		public void DashMovement()
		{
			if (this.dashDelay > 0)
			{
				this.dashDelay--;
				return;
			}
			if (this.dashDelay < 0)
			{
				for (int i = 0; i < 2; i++)
				{
					int num;
					if (this.velocity.Y == 0f)
					{
						num = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)this.height - 4f), this.width, 8, 31, 0f, 0f, 100, default(Color), 1.4f);
					}
					else
					{
						num = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)(this.height / 2) - 8f), this.width, 16, 31, 0f, 0f, 100, default(Color), 1.4f);
					}
					Main.dust[num].velocity *= 0.1f;
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
				}
				if (this.velocity.X > 13f || this.velocity.X < -13f)
				{
					this.velocity.X = this.velocity.X * 0.99f;
					return;
				}
				if (this.velocity.X > this.accRunSpeed || this.velocity.X < -this.accRunSpeed)
				{
					this.velocity.X = this.velocity.X * 0.9f;
					return;
				}
				this.dashDelay = 20;
				if (this.velocity.X < 0f)
				{
					this.velocity.X = -this.accRunSpeed;
					return;
				}
				if (this.velocity.X > 0f)
				{
					this.velocity.X = this.accRunSpeed;
					return;
				}
			}
			else
			{
				if (this.dash > 0 && !this.mount.Active)
				{
					int num2 = 0;
					bool flag = false;
					if (this.dashTime > 0)
					{
						this.dashTime--;
					}
					if (this.dashTime < 0)
					{
						this.dashTime++;
					}
					if (this.controlRight && this.releaseRight)
					{
						if (this.dashTime > 0)
						{
							num2 = 1;
							flag = true;
							this.dashTime = 0;
						}
						else
						{
							this.dashTime = 15;
						}
					}
					else
					{
						if (this.controlLeft && this.releaseLeft)
						{
							if (this.dashTime < 0)
							{
								num2 = -1;
								flag = true;
								this.dashTime = 0;
							}
							else
							{
								this.dashTime = -15;
							}
						}
					}
					if (flag)
					{
						this.velocity.X = 15.9f * (float)num2;
						this.dashDelay = -1;
						for (int j = 0; j < 20; j++)
						{
							int num3 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 31, 0f, 0f, 100, default(Color), 2f);
							Dust expr_336_cp_0 = Main.dust[num3];
							expr_336_cp_0.position.X = expr_336_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_35D_cp_0 = Main.dust[num3];
							expr_35D_cp_0.position.Y = expr_35D_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num3].velocity *= 0.2f;
							Main.dust[num3].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						}
						int num4 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 34f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[num4].velocity.X = (float)Main.rand.Next(-50, 51) * 0.01f;
						Main.gore[num4].velocity.Y = (float)Main.rand.Next(-50, 51) * 0.01f;
						Main.gore[num4].velocity *= 0.4f;
						num4 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 14f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[num4].velocity.X = (float)Main.rand.Next(-50, 51) * 0.01f;
						Main.gore[num4].velocity.Y = (float)Main.rand.Next(-50, 51) * 0.01f;
						Main.gore[num4].velocity *= 0.4f;
					}
				}
			}
		}
		public void WallslideMovement()
		{
			this.sliding = false;
			if (this.slideDir != 0 && this.spikedBoots > 0 && !this.mount.Active && ((this.controlLeft && this.slideDir == -1) || (this.controlRight && this.slideDir == 1)))
			{
				bool flag = false;
				float num = this.position.X;
				if (this.slideDir == 1)
				{
					num += (float)this.width;
				}
				num += (float)this.slideDir;
				float num2 = this.position.Y + (float)this.height + 1f;
				if (this.gravDir < 0f)
				{
					num2 = this.position.Y - 1f;
				}
				num /= 16f;
				num2 /= 16f;
				if (WorldGen.SolidTile((int)num, (int)num2) && WorldGen.SolidTile((int)num, (int)num2 - 1))
				{
					flag = true;
				}
				if (this.spikedBoots >= 2)
				{
					if (flag && ((this.velocity.Y > 0f && this.gravDir == 1f) || (this.velocity.Y < this.gravity && this.gravDir == -1f)))
					{
						float num3 = this.gravity;
						if (this.slowFall)
						{
							if (this.controlUp)
							{
								num3 = this.gravity / 10f * this.gravDir;
							}
							else
							{
								num3 = this.gravity / 3f * this.gravDir;
							}
						}
						this.fallStart = (int)(this.position.Y / 16f);
						if ((this.controlDown && this.gravDir == 1f) || (this.controlUp && this.gravDir == -1f))
						{
							this.velocity.Y = 4f * this.gravDir;
							int num4 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)((this.width / 2 - 4) * this.slideDir), this.position.Y + (float)(this.height / 2) + (float)(this.height / 2 - 4) * this.gravDir), 8, 8, 31, 0f, 0f, 0, default(Color), 1f);
							if (this.slideDir < 0)
							{
								Dust expr_26B_cp_0 = Main.dust[num4];
								expr_26B_cp_0.position.X = expr_26B_cp_0.position.X - 10f;
							}
							if (this.gravDir < 0f)
							{
								Dust expr_296_cp_0 = Main.dust[num4];
								expr_296_cp_0.position.Y = expr_296_cp_0.position.Y - 12f;
							}
							Main.dust[num4].velocity *= 0.1f;
							Main.dust[num4].scale *= 1.2f;
							Main.dust[num4].noGravity = true;
						}
						else
						{
							if (this.gravDir == -1f)
							{
								this.velocity.Y = (-num3 + 1E-05f) * this.gravDir;
							}
							else
							{
								this.velocity.Y = (-num3 + 1E-05f) * this.gravDir;
							}
						}
						this.sliding = true;
						return;
					}
				}
				else
				{
					if ((flag && (double)this.velocity.Y > 0.5 && this.gravDir == 1f) || ((double)this.velocity.Y < -0.5 && this.gravDir == -1f))
					{
						this.fallStart = (int)(this.position.Y / 16f);
						if (this.controlDown)
						{
							this.velocity.Y = 4f * this.gravDir;
						}
						else
						{
							this.velocity.Y = 0.5f * this.gravDir;
						}
						this.sliding = true;
						int num5 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)((this.width / 2 - 4) * this.slideDir), this.position.Y + (float)(this.height / 2) + (float)(this.height / 2 - 4) * this.gravDir), 8, 8, 31, 0f, 0f, 0, default(Color), 1f);
						if (this.slideDir < 0)
						{
							Dust expr_470_cp_0 = Main.dust[num5];
							expr_470_cp_0.position.X = expr_470_cp_0.position.X - 10f;
						}
						if (this.gravDir < 0f)
						{
							Dust expr_49B_cp_0 = Main.dust[num5];
							expr_49B_cp_0.position.Y = expr_49B_cp_0.position.Y - 12f;
						}
						Main.dust[num5].velocity *= 0.1f;
						Main.dust[num5].scale *= 1.2f;
						Main.dust[num5].noGravity = true;
					}
				}
			}
		}
		public void CarpetMovement()
		{
			bool flag = false;
			if (this.grappling[0] == -1 && this.carpet && !this.jumpAgain && !this.jumpAgain2 && !this.jumpAgain3 && !this.jumpAgain4 && this.jump == 0 && this.velocity.Y != 0f && this.rocketTime == 0 && this.wingTime == 0f && !this.mount.Active)
			{
				if (this.controlJump && this.canCarpet)
				{
					this.canCarpet = false;
					this.carpetTime = 300;
				}
				if (this.carpetTime > 0 && this.controlJump)
				{
					this.fallStart = (int)(this.position.Y / 16f);
					flag = true;
					this.carpetTime--;
					if (this.gravDir == 1f && this.velocity.Y > -this.gravity)
					{
						this.velocity.Y = -(this.gravity + 1E-06f);
					}
					else
					{
						if (this.gravDir == -1f && this.velocity.Y < this.gravity)
						{
							this.velocity.Y = this.gravity + 1E-06f;
						}
					}
					this.carpetFrameCounter += 1f + Math.Abs(this.velocity.X * 0.5f);
					if (this.carpetFrameCounter > 8f)
					{
						this.carpetFrameCounter = 0f;
						this.carpetFrame++;
					}
					if (this.carpetFrame < 0)
					{
						this.carpetFrame = 0;
					}
					if (this.carpetFrame > 5)
					{
						this.carpetFrame = 0;
					}
				}
			}
			if (!flag)
			{
				this.carpetFrame = -1;
			}
		}
		public void DoubleJumpVisuals()
		{
			if (this.dJumpEffect && this.doubleJump && !this.jumpAgain && (this.jumpAgain2 || !this.doubleJump2) && ((this.gravDir == 1f && this.velocity.Y < 0f) || (this.gravDir == -1f && this.velocity.Y > 0f)))
			{
				int num = this.height;
				if (this.gravDir == -1f)
				{
					num = -6;
				}
				int num2 = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float)num), this.width + 8, 4, 16, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 100, default(Color), 1.5f);
				Main.dust[num2].velocity.X = Main.dust[num2].velocity.X * 0.5f - this.velocity.X * 0.1f;
				Main.dust[num2].velocity.Y = Main.dust[num2].velocity.Y * 0.5f - this.velocity.Y * 0.3f;
			}
			if (this.dJumpEffect2 && this.doubleJump2 && !this.jumpAgain2 && ((this.gravDir == 1f && this.velocity.Y < 0f) || (this.gravDir == -1f && this.velocity.Y > 0f)))
			{
				int num3 = this.height;
				if (this.gravDir == -1f)
				{
					num3 = -6;
				}
				float num4 = ((float)this.jump / 75f + 1f) / 2f;
				for (int i = 0; i < 3; i++)
				{
					int num5 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)(num3 / 2)), this.width, 32, 124, this.velocity.X * 0.3f, this.velocity.Y * 0.3f, 150, default(Color), 1f * num4);
					Main.dust[num5].velocity *= 0.5f * num4;
					Main.dust[num5].fadeIn = 1.5f * num4;
				}
				this.sandStorm = true;
				if (this.miscCounter % 3 == 0)
				{
					int num6 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 18f, this.position.Y + (float)(num3 / 2)), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(220, 223), num4);
					Main.gore[num6].velocity = this.velocity * 0.3f * num4;
					Main.gore[num6].alpha = 100;
				}
			}
			if (this.dJumpEffect4 && this.doubleJump4 && !this.jumpAgain4 && ((this.gravDir == 1f && this.velocity.Y < 0f) || (this.gravDir == -1f && this.velocity.Y > 0f)))
			{
				int num7 = this.height;
				if (this.gravDir == -1f)
				{
					num7 = -6;
				}
				int num8 = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float)num7), this.width + 8, 4, 188, -this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 100, default(Color), 1.5f);
				Main.dust[num8].velocity.X = Main.dust[num8].velocity.X * 0.5f - this.velocity.X * 0.1f;
				Main.dust[num8].velocity.Y = Main.dust[num8].velocity.Y * 0.5f - this.velocity.Y * 0.3f;
				Main.dust[num8].velocity *= 0.5f;
			}
			if (this.dJumpEffect3 && this.doubleJump3 && !this.jumpAgain3 && ((this.gravDir == 1f && this.velocity.Y < 0f) || (this.gravDir == -1f && this.velocity.Y > 0f)))
			{
				int num9 = this.height - 6;
				if (this.gravDir == -1f)
				{
					num9 = 6;
				}
				for (int j = 0; j < 2; j++)
				{
					int num10 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)num9), this.width, 12, 76, this.velocity.X * 0.3f, this.velocity.Y * 0.3f, 0, default(Color), 1f);
					Main.dust[num10].velocity *= 0.1f;
					if (j == 0)
					{
						Main.dust[num10].velocity += this.velocity * 0.03f;
					}
					else
					{
						Main.dust[num10].velocity -= this.velocity * 0.03f;
					}
					Main.dust[num10].noLight = true;
				}
				for (int k = 0; k < 3; k++)
				{
					int num11 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)num9), this.width, 12, 76, this.velocity.X * 0.3f, this.velocity.Y * 0.3f, 0, default(Color), 1f);
					Main.dust[num11].fadeIn = 1.5f;
					Main.dust[num11].velocity *= 0.6f;
					Main.dust[num11].velocity += this.velocity * 0.8f;
					Main.dust[num11].noGravity = true;
					Main.dust[num11].noLight = true;
				}
				for (int l = 0; l < 3; l++)
				{
					int num12 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)num9), this.width, 12, 76, this.velocity.X * 0.3f, this.velocity.Y * 0.3f, 0, default(Color), 1f);
					Main.dust[num12].fadeIn = 1.5f;
					Main.dust[num12].velocity *= 0.6f;
					Main.dust[num12].velocity -= this.velocity * 0.8f;
					Main.dust[num12].noGravity = true;
					Main.dust[num12].noLight = true;
				}
			}
		}
		public void WingMovement()
		{
			if (this.wingsLogic == 4 && this.controlUp)
			{
				this.velocity.Y = this.velocity.Y - 0.2f * this.gravDir;
				if (this.gravDir == 1f)
				{
					if (this.velocity.Y > 0f)
					{
						this.velocity.Y = this.velocity.Y - 1f;
					}
					else
					{
						if (this.velocity.Y > -Player.jumpSpeed)
						{
							this.velocity.Y = this.velocity.Y - 0.2f;
						}
					}
					if (this.velocity.Y < -Player.jumpSpeed * 3f)
					{
						this.velocity.Y = -Player.jumpSpeed * 3f;
					}
				}
				else
				{
					if (this.velocity.Y < 0f)
					{
						this.velocity.Y = this.velocity.Y + 1f;
					}
					else
					{
						if (this.velocity.Y < Player.jumpSpeed)
						{
							this.velocity.Y = this.velocity.Y + 0.2f;
						}
					}
					if (this.velocity.Y > Player.jumpSpeed * 3f)
					{
						this.velocity.Y = Player.jumpSpeed * 3f;
					}
				}
				this.wingTime -= 2f;
				return;
			}
			if (this.wingsLogic == 3 && this.controlUp)
			{
				this.velocity.Y = this.velocity.Y - 0.3f * this.gravDir;
				if (this.gravDir == 1f)
				{
					if (this.velocity.Y > 0f)
					{
						this.velocity.Y = this.velocity.Y - 1f;
					}
					else
					{
						if (this.velocity.Y > -Player.jumpSpeed)
						{
							this.velocity.Y = this.velocity.Y - 0.2f;
						}
					}
					if (this.velocity.Y < -Player.jumpSpeed * 3f)
					{
						this.velocity.Y = -Player.jumpSpeed * 3f;
					}
				}
				else
				{
					if (this.velocity.Y < 0f)
					{
						this.velocity.Y = this.velocity.Y + 1f;
					}
					else
					{
						if (this.velocity.Y < Player.jumpSpeed)
						{
							this.velocity.Y = this.velocity.Y + 0.2f;
						}
					}
					if (this.velocity.Y > Player.jumpSpeed * 3f)
					{
						this.velocity.Y = Player.jumpSpeed * 3f;
					}
				}
				this.wingTime -= 2f;
				return;
			}
			if (this.wingsLogic == 26)
			{
				this.velocity.Y = this.velocity.Y - 0.125f * this.gravDir;
				if (this.gravDir == 1f)
				{
					if (this.velocity.Y > 0f)
					{
						this.velocity.Y = this.velocity.Y - 0.75f;
					}
					else
					{
						if (this.velocity.Y > -Player.jumpSpeed)
						{
							this.velocity.Y = this.velocity.Y - 0.15f;
						}
					}
					if (this.velocity.Y < -Player.jumpSpeed * 3f)
					{
						this.velocity.Y = -Player.jumpSpeed * 2f;
					}
				}
				else
				{
					if (this.velocity.Y < 0f)
					{
						this.velocity.Y = this.velocity.Y + 0.75f;
					}
					else
					{
						if (this.velocity.Y < Player.jumpSpeed)
						{
							this.velocity.Y = this.velocity.Y + 0.15f;
						}
					}
					if (this.velocity.Y > Player.jumpSpeed * 3f)
					{
						this.velocity.Y = Player.jumpSpeed * 2f;
					}
				}
				this.wingTime -= 1f;
				return;
			}
			this.velocity.Y = this.velocity.Y - 0.1f * this.gravDir;
			if (this.gravDir == 1f)
			{
				if (this.velocity.Y > 0f)
				{
					this.velocity.Y = this.velocity.Y - 0.5f;
				}
				else
				{
					if ((double)this.velocity.Y > (double)(-(double)Player.jumpSpeed) * 0.5)
					{
						this.velocity.Y = this.velocity.Y - 0.1f;
					}
				}
				if (this.velocity.Y < -Player.jumpSpeed * 1.5f)
				{
					this.velocity.Y = -Player.jumpSpeed * 1.5f;
				}
			}
			else
			{
				if (this.velocity.Y < 0f)
				{
					this.velocity.Y = this.velocity.Y + 0.5f;
				}
				else
				{
					if ((double)this.velocity.Y < (double)Player.jumpSpeed * 0.5)
					{
						this.velocity.Y = this.velocity.Y + 0.1f;
					}
				}
				if (this.velocity.Y > Player.jumpSpeed * 1.5f)
				{
					this.velocity.Y = Player.jumpSpeed * 1.5f;
				}
			}
			if (this.wingsLogic == 22 && this.controlDown && !this.controlLeft && !this.controlRight)
			{
				this.wingTime -= 0.5f;
				return;
			}
			this.wingTime -= 1f;
		}
		public void WOFTongue()
		{
			if (Main.wof >= 0 && Main.npc[Main.wof].active)
			{
				float num = Main.npc[Main.wof].position.X + 40f;
				if (Main.npc[Main.wof].direction > 0)
				{
					num -= 96f;
				}
				if (this.position.X + (float)this.width > num && this.position.X < num + 140f && this.gross)
				{
					this.noKnockback = false;
					this.Hurt(50, Main.npc[Main.wof].direction, false, false, " was slain...", false);
				}
				if (!this.gross && this.position.Y > (float)((Main.maxTilesY - 250) * 16) && this.position.X > num - 1920f && this.position.X < num + 1920f)
				{
					this.AddBuff(37, 10, true);
					Main.PlaySound(4, (int)Main.npc[Main.wof].position.X, (int)Main.npc[Main.wof].position.Y, 10);
				}
				if (this.gross)
				{
					if (this.position.Y < (float)((Main.maxTilesY - 200) * 16))
					{
						this.AddBuff(38, 10, true);
					}
					if (Main.npc[Main.wof].direction < 0)
					{
						if (this.position.X + (float)(this.width / 2) > Main.npc[Main.wof].position.X + (float)(Main.npc[Main.wof].width / 2) + 40f)
						{
							this.AddBuff(38, 10, true);
						}
					}
					else
					{
						if (this.position.X + (float)(this.width / 2) < Main.npc[Main.wof].position.X + (float)(Main.npc[Main.wof].width / 2) - 40f)
						{
							this.AddBuff(38, 10, true);
						}
					}
				}
				if (this.tongued)
				{
					this.controlHook = false;
					this.controlUseItem = false;
					for (int i = 0; i < 1000; i++)
					{
						if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].aiStyle == 7)
						{
							Main.projectile[i].Kill();
						}
					}
					Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
					float num2 = Main.npc[Main.wof].position.X + (float)(Main.npc[Main.wof].width / 2) - vector.X;
					float num3 = Main.npc[Main.wof].position.Y + (float)(Main.npc[Main.wof].height / 2) - vector.Y;
					float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
					if (num4 > 3000f)
					{
						this.KillMe(1000.0, 0, false, " tried to escape.");
						return;
					}
					if (Main.npc[Main.wof].position.X < 608f || Main.npc[Main.wof].position.X > (float)((Main.maxTilesX - 38) * 16))
					{
						this.KillMe(1000.0, 0, false, " was licked.");
					}
				}
			}
		}
		public void StatusPlayer(NPC npc)
		{
			if (npc.type >= 269 && npc.type <= 272)
			{
				if (Main.rand.Next(3) == 0)
				{
					this.AddBuff(30, 600, true);
				}
				else
				{
					if (Main.rand.Next(3) == 0)
					{
						this.AddBuff(32, 300, true);
					}
				}
			}
			if (npc.type >= 273 && npc.type <= 276 && Main.rand.Next(2) == 0)
			{
				this.AddBuff(36, 600, true);
			}
			if (npc.type >= 277 && npc.type <= 280)
			{
				this.AddBuff(24, 600, true);
			}
			if (((npc.type == 1 && npc.name == "Black Slime") || npc.type == 81 || npc.type == 79) && Main.rand.Next(4) == 0)
			{
				this.AddBuff(22, 900, true);
			}
			if ((npc.type == 23 || npc.type == 25) && Main.rand.Next(3) == 0)
			{
				this.AddBuff(24, 420, true);
			}
			if ((npc.type == 34 || npc.type == 83 || npc.type == 84) && Main.rand.Next(3) == 0)
			{
				this.AddBuff(23, 240, true);
			}
			if ((npc.type == 104 || npc.type == 102) && Main.rand.Next(8) == 0)
			{
				this.AddBuff(30, 2700, true);
			}
			if (npc.type == 75 && Main.rand.Next(10) == 0)
			{
				this.AddBuff(35, 420, true);
			}
			if ((npc.type == 163 || npc.type == 238) && Main.rand.Next(10) == 0)
			{
				this.AddBuff(70, 480, true);
			}
			if ((npc.type == 79 || npc.type == 103) && Main.rand.Next(5) == 0)
			{
				this.AddBuff(35, 420, true);
			}
			if ((npc.type == 75 || npc.type == 78 || npc.type == 82) && Main.rand.Next(8) == 0)
			{
				this.AddBuff(32, 900, true);
			}
			if ((npc.type == 93 || npc.type == 109 || npc.type == 80) && Main.rand.Next(14) == 0)
			{
				this.AddBuff(31, 300, true);
			}
			if (npc.type >= 305 && npc.type <= 314 && Main.rand.Next(10) == 0)
			{
				this.AddBuff(33, 3600, true);
			}
			if (npc.type == 77 && Main.rand.Next(6) == 0)
			{
				this.AddBuff(36, 18000, true);
			}
			if (npc.type == 112 && Main.rand.Next(20) == 0)
			{
				this.AddBuff(33, 18000, true);
			}
			if (npc.type == 182 && Main.rand.Next(25) == 0)
			{
				this.AddBuff(33, 7200, true);
			}
			if (npc.type == 141 && Main.rand.Next(2) == 0)
			{
				this.AddBuff(20, 600, true);
			}
			if (npc.type == 147 && !this.frozen && Main.rand.Next(12) == 0)
			{
				this.AddBuff(46, 600, true);
			}
			if (npc.type == 150)
			{
				if (Main.rand.Next(2) == 0)
				{
					this.AddBuff(46, 900, true);
				}
				if (!this.frozen && Main.rand.Next(35) == 0)
				{
					this.AddBuff(47, 60, true);
				}
			}
			if (npc.type == 184)
			{
				this.AddBuff(46, 1200, true);
				if (!this.frozen && Main.rand.Next(15) == 0)
				{
					this.AddBuff(47, 60, true);
				}
			}
		}
		public void GrappleMovement()
		{
			if (this.grappling[0] >= 0)
			{
				if (Main.myPlayer == this.whoAmi && this.mount.Active)
				{
					this.mount.Dismount(this);
				}
				this.canCarpet = true;
				this.carpetFrame = -1;
				this.wingFrame = 1;
				if (this.velocity.Y == 0f || (this.wet && (double)this.velocity.Y > -0.02 && (double)this.velocity.Y < 0.02))
				{
					this.wingFrame = 0;
				}
				if (this.wings == 4)
				{
					this.wingFrame = 3;
				}
				this.wingTime = (float)this.wingTimeMax;
				this.rocketTime = this.rocketTimeMax;
				this.rocketDelay = 0;
				this.rocketFrame = false;
				this.canRocket = false;
				this.rocketRelease = false;
				this.fallStart = (int)(this.position.Y / 16f);
				int num = -1;
				float num2 = 0f;
				float num3 = 0f;
				for (int i = 0; i < this.grapCount; i++)
				{
					if (Main.projectile[this.grappling[i]].type == 403)
					{
						num = i;
					}
					num2 += Main.projectile[this.grappling[i]].position.X + (float)(Main.projectile[this.grappling[i]].width / 2);
					num3 += Main.projectile[this.grappling[i]].position.Y + (float)(Main.projectile[this.grappling[i]].height / 2);
				}
				num2 /= (float)this.grapCount;
				num3 /= (float)this.grapCount;
				Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
				float num4 = num2 - vector.X;
				float num5 = num3 - vector.Y;
				float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
				float num7 = 11f;
				if (Main.projectile[this.grappling[0]].type == 315)
				{
					num7 = 16f;
				}
				float num8;
				if (num6 > num7)
				{
					num8 = num7 / num6;
				}
				else
				{
					num8 = 1f;
				}
				num4 *= num8;
				num5 *= num8;
				this.velocity.X = num4;
				this.velocity.Y = num5;
				if (num != -1)
				{
					Projectile projectile = Main.projectile[this.grappling[num]];
					if (projectile.position.X < this.position.X + (float)this.width && projectile.position.X + (float)projectile.width >= this.position.X && projectile.position.Y < this.position.Y + (float)this.height && projectile.position.Y + (float)projectile.height >= this.position.Y)
					{
						int num9 = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
						int num10 = (int)(projectile.position.Y + (float)(projectile.height / 2)) / 16;
						this.velocity = Vector2.Zero;
						if (Main.tile[num9, num10].type == 314)
						{
							Vector2 vector2;
							vector2.X = projectile.position.X + (float)(projectile.width / 2) - (float)(this.width / 2);
							vector2.Y = projectile.position.Y + (float)(projectile.height / 2) - (float)(this.height / 2);
							this.grappling[0] = -1;
							this.grapCount = 0;
							for (int j = 0; j < 1000; j++)
							{
								if (Main.projectile[j].active && Main.projectile[j].owner == this.whoAmi && Main.projectile[j].aiStyle == 7)
								{
									Main.projectile[j].Kill();
								}
							}
							int num11 = this.height + Mount.GetHeightBoost(6);
							if (Minecart.GetOnTrack(num9, num10, ref vector2, this.width, num11) && !Collision.SolidCollision(vector2, this.width, num11 - 20))
							{
								this.position = vector2;
								this.mount.SetMount(6, this, this.minecartLeft);
								Minecart.WheelSparks(this.position, this.width, this.height, 25);
							}
						}
					}
				}
				if (this.itemAnimation == 0)
				{
					if (this.velocity.X > 0f)
					{
						this.ChangeDir(1);
					}
					if (this.velocity.X < 0f)
					{
						this.ChangeDir(-1);
					}
				}
				if (this.controlJump)
				{
					if (this.releaseJump)
					{
						if ((this.velocity.Y == 0f || (this.wet && (double)this.velocity.Y > -0.02 && (double)this.velocity.Y < 0.02)) && !this.controlDown)
						{
							this.velocity.Y = -Player.jumpSpeed;
							this.jump = Player.jumpHeight / 2;
							this.releaseJump = false;
						}
						else
						{
							this.velocity.Y = this.velocity.Y + 0.01f;
							this.releaseJump = false;
						}
						if (this.doubleJump)
						{
							this.jumpAgain = true;
						}
						if (this.doubleJump2)
						{
							this.jumpAgain2 = true;
						}
						if (this.doubleJump3)
						{
							this.jumpAgain3 = true;
						}
						if (this.doubleJump4)
						{
							this.jumpAgain4 = true;
						}
						this.grappling[0] = 0;
						this.grapCount = 0;
						for (int k = 0; k < 1000; k++)
						{
							if (Main.projectile[k].active && Main.projectile[k].owner == this.whoAmi && Main.projectile[k].aiStyle == 7)
							{
								Main.projectile[k].Kill();
							}
						}
						return;
					}
				}
				else
				{
					this.releaseJump = true;
				}
			}
		}
		public void StickyMovement()
		{
			bool flag = false;
			if (this.mount.Type == 6 && Math.Abs(this.velocity.X) > 5f)
			{
				flag = true;
			}
			int num = this.width / 2;
			int num2 = this.height / 2;
			new Vector2(this.position.X + (float)(this.width / 2) - (float)(num / 2), this.position.Y + (float)(this.height / 2) - (float)(num2 / 2));
			Vector2 vector = Collision.StickyTiles(this.position, this.velocity, this.width, this.height);
			if (vector.Y != -1f && vector.X != -1f)
			{
				int num3 = (int)vector.X;
				int num4 = (int)vector.Y;
				int type = (int)Main.tile[num3, num4].type;
				if (this.whoAmi == Main.myPlayer && type == 51 && (this.velocity.X != 0f || this.velocity.Y != 0f))
				{
					this.stickyBreak++;
					if (this.stickyBreak > Main.rand.Next(20, 100) || flag)
					{
						this.stickyBreak = 0;
						WorldGen.KillTile(num3, num4, false, false, false);
						if (Main.netMode == 1 && !Main.tile[num3, num4].active() && Main.netMode == 1)
						{
							NetMessage.SendData(17, -1, -1, "", 0, (float)num3, (float)num4, 0f, 0);
						}
					}
				}
				if (flag)
				{
					return;
				}
				this.fallStart = (int)(this.position.Y / 16f);
				if (type != 229)
				{
					this.jump = 0;
				}
				if (this.velocity.X > 1f)
				{
					this.velocity.X = 1f;
				}
				if (this.velocity.X < -1f)
				{
					this.velocity.X = -1f;
				}
				if (this.velocity.Y > 1f)
				{
					this.velocity.Y = 1f;
				}
				if (this.velocity.Y < -5f)
				{
					this.velocity.Y = -5f;
				}
				if ((double)this.velocity.X > 0.75 || (double)this.velocity.X < -0.75)
				{
					this.velocity.X = this.velocity.X * 0.85f;
				}
				else
				{
					this.velocity.X = this.velocity.X * 0.6f;
				}
				if (this.velocity.Y < 0f)
				{
					this.velocity.Y = this.velocity.Y * 0.96f;
				}
				else
				{
					this.velocity.Y = this.velocity.Y * 0.3f;
				}
				if (type == 229 && Main.rand.Next(5) == 0 && ((double)this.velocity.Y > 0.15 || this.velocity.Y < 0f))
				{
					if ((float)(num3 * 16) < this.position.X + (float)(this.width / 2))
					{
						int num5 = Dust.NewDust(new Vector2(this.position.X - 4f, (float)(num4 * 16)), 4, 16, 153, 0f, 0f, 50, default(Color), 1f);
						Main.dust[num5].scale += (float)Main.rand.Next(0, 6) * 0.1f;
						Main.dust[num5].velocity *= 0.1f;
						Main.dust[num5].noGravity = true;
					}
					else
					{
						int num6 = Dust.NewDust(new Vector2(this.position.X + (float)this.width - 2f, (float)(num4 * 16)), 4, 16, 153, 0f, 0f, 50, default(Color), 1f);
						Main.dust[num6].scale += (float)Main.rand.Next(0, 6) * 0.1f;
						Main.dust[num6].velocity *= 0.1f;
						Main.dust[num6].noGravity = true;
					}
					if (Main.tile[num3, num4 + 1] != null && Main.tile[num3, num4 + 1].type == 229 && this.position.Y + (float)this.height > (float)((num4 + 1) * 16))
					{
						if ((float)(num3 * 16) < this.position.X + (float)(this.width / 2))
						{
							int num7 = Dust.NewDust(new Vector2(this.position.X - 4f, (float)(num4 * 16 + 16)), 4, 16, 153, 0f, 0f, 50, default(Color), 1f);
							Main.dust[num7].scale += (float)Main.rand.Next(0, 6) * 0.1f;
							Main.dust[num7].velocity *= 0.1f;
							Main.dust[num7].noGravity = true;
						}
						else
						{
							int num8 = Dust.NewDust(new Vector2(this.position.X + (float)this.width - 2f, (float)(num4 * 16 + 16)), 4, 16, 153, 0f, 0f, 50, default(Color), 1f);
							Main.dust[num8].scale += (float)Main.rand.Next(0, 6) * 0.1f;
							Main.dust[num8].velocity *= 0.1f;
							Main.dust[num8].noGravity = true;
						}
					}
					if (Main.tile[num3, num4 + 2] != null && Main.tile[num3, num4 + 2].type == 229 && this.position.Y + (float)this.height > (float)((num4 + 2) * 16))
					{
						if ((float)(num3 * 16) < this.position.X + (float)(this.width / 2))
						{
							int num9 = Dust.NewDust(new Vector2(this.position.X - 4f, (float)(num4 * 16 + 32)), 4, 16, 153, 0f, 0f, 50, default(Color), 1f);
							Main.dust[num9].scale += (float)Main.rand.Next(0, 6) * 0.1f;
							Main.dust[num9].velocity *= 0.1f;
							Main.dust[num9].noGravity = true;
							return;
						}
						int num10 = Dust.NewDust(new Vector2(this.position.X + (float)this.width - 2f, (float)(num4 * 16 + 32)), 4, 16, 153, 0f, 0f, 50, default(Color), 1f);
						Main.dust[num10].scale += (float)Main.rand.Next(0, 6) * 0.1f;
						Main.dust[num10].velocity *= 0.1f;
						Main.dust[num10].noGravity = true;
						return;
					}
				}
			}
			else
			{
				this.stickyBreak = 0;
			}
		}
		public void CheckDrowning()
		{
			bool flag = Collision.DrownCollision(this.position, this.width, this.height, this.gravDir);
			if (this.armor[0].type == 250)
			{
				flag = true;
			}
			if (this.inventory[this.selectedItem].type == 186)
			{
				try
				{
					int num = (int)((this.position.X + (float)(this.width / 2) + (float)(6 * this.direction)) / 16f);
					int num2 = 0;
					if (this.gravDir == -1f)
					{
						num2 = this.height;
					}
					int num3 = (int)((this.position.Y + (float)num2 - 44f * this.gravDir) / 16f);
					if (Main.tile[num, num3].liquid < 128)
					{
						if (Main.tile[num, num3] == null)
						{
							Main.tile[num, num3] = new Tile();
						}
						if (!Main.tile[num, num3].active() || !Main.tileSolid[(int)Main.tile[num, num3].type] || Main.tileSolidTop[(int)Main.tile[num, num3].type])
						{
							flag = false;
						}
					}
				}
				catch
				{
				}
			}
			if (this.gills)
			{
				flag = false;
			}
			if (Main.myPlayer == this.whoAmi)
			{
				if (this.merman)
				{
					flag = false;
				}
				if (flag)
				{
					this.breathCD++;
					int num4 = 7;
					if (this.inventory[this.selectedItem].type == 186)
					{
						num4 *= 2;
					}
					if (this.accDivingHelm)
					{
						num4 *= 4;
					}
					if (this.breathCD >= num4)
					{
						this.breathCD = 0;
						this.breath--;
						if (this.breath == 0)
						{
							Main.PlaySound(23, -1, -1, 1);
						}
						if (this.breath <= 0)
						{
							this.lifeRegenTime = 0;
							this.breath = 0;
							this.statLife -= 2;
							if (this.statLife <= 0)
							{
								this.statLife = 0;
								this.KillMe(10.0, 0, false, Lang.deathMsg(-1, -1, -1, 1));
							}
						}
					}
				}
				else
				{
					this.breath += 3;
					if (this.breath > this.breathMax)
					{
						this.breath = this.breathMax;
					}
					this.breathCD = 0;
				}
			}
			if (flag && Main.rand.Next(20) == 0 && !this.lavaWet && !this.honeyWet)
			{
				int num5 = 0;
				if (this.gravDir == -1f)
				{
					num5 += this.height - 12;
				}
				if (this.inventory[this.selectedItem].type == 186)
				{
					Dust.NewDust(new Vector2(this.position.X + (float)(10 * this.direction) + 4f, this.position.Y + (float)num5 - 54f * this.gravDir), this.width - 8, 8, 34, 0f, 0f, 0, default(Color), 1.2f);
					return;
				}
				Dust.NewDust(new Vector2(this.position.X + (float)(12 * this.direction), this.position.Y + (float)num5 + 4f * this.gravDir), this.width - 8, 8, 34, 0f, 0f, 0, default(Color), 1.2f);
			}
		}
		public void CheckIceBreak()
		{
			if (this.velocity.Y > 7f)
			{
				Vector2 vector = this.position + this.velocity;
				int num = (int)(vector.X / 16f);
				int num2 = (int)((vector.X + (float)this.width) / 16f);
				int num3 = (int)((this.position.Y + (float)this.height + 1f) / 16f);
				for (int i = num; i <= num2; i++)
				{
					for (int j = num3; j <= num3 + 1; j++)
					{
						if (Main.tile[i, j].nactive() && Main.tile[i, j].type == 162)
						{
							WorldGen.KillTile(i, j, false, false, false);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(17, -1, -1, "", 0, (float)i, (float)j, 0f, 0);
							}
						}
					}
				}
			}
		}
		public void SlopeDownMovement()
		{
			this.sloping = false;
			float y = this.velocity.Y;
			Vector4 vector = Collision.WalkDownSlope(this.position, this.velocity, this.width, this.height, this.gravity * this.gravDir);
			this.position.X = vector.X;
			this.position.Y = vector.Y;
			this.velocity.X = vector.Z;
			this.velocity.Y = vector.W;
			if (this.velocity.Y != y)
			{
				this.sloping = true;
			}
		}
		public void HoneyCollision(bool fallThrough, bool ignorePlats)
		{
			int num;
			if (this.onTrack)
			{
				num = this.height - 20;
			}
			else
			{
				num = this.height;
			}
			Vector2 vector = this.velocity;
			this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, num, fallThrough, ignorePlats, (int)this.gravDir);
			Vector2 value = this.velocity * 0.25f;
			if (this.velocity.X != vector.X)
			{
				value.X = this.velocity.X;
			}
			if (this.velocity.Y != vector.Y)
			{
				value.Y = this.velocity.Y;
			}
			this.position += value;
		}
		public void WaterCollision(bool fallThrough, bool ignorePlats)
		{
			int num;
			if (this.onTrack)
			{
				num = this.height - 20;
			}
			else
			{
				num = this.height;
			}
			Vector2 vector = this.velocity;
			this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, num, fallThrough, ignorePlats, (int)this.gravDir);
			Vector2 value = this.velocity * 0.5f;
			if (this.velocity.X != vector.X)
			{
				value.X = this.velocity.X;
			}
			if (this.velocity.Y != vector.Y)
			{
				value.Y = this.velocity.Y;
			}
			this.position += value;
		}
		public void DryCollision(bool fallThrough, bool ignorePlats)
		{
			int num;
			if (this.onTrack)
			{
				num = this.height - 10;
			}
			else
			{
				num = this.height;
			}
			this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, num, fallThrough, ignorePlats, (int)this.gravDir);
			if (Collision.up && this.gravDir == 1f)
			{
				this.jump = 0;
			}
			if (this.waterWalk || this.waterWalk2)
			{
				Vector2 value = this.velocity;
				this.velocity = Collision.WaterCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false, this.waterWalk);
				if (value != this.velocity)
				{
					this.fallStart = (int)(this.position.Y / 16f);
				}
			}
			this.position += this.velocity;
		}
		public void SlopingCollision()
		{
			if (this.controlDown || this.grappling[0] >= 0 || this.gravDir == -1f)
			{
				this.stairFall = true;
			}
			Vector4 vector = Collision.SlopeCollision(this.position, this.velocity, this.width, this.height, this.gravity, this.stairFall);
			if (Collision.stairFall)
			{
				this.stairFall = true;
			}
			else
			{
				if (!this.controlDown)
				{
					this.stairFall = false;
				}
			}
			if (Collision.stair && Math.Abs(vector.Y - this.position.Y) > 8f + Math.Abs(this.velocity.X))
			{
				this.gfxOffY -= vector.Y - this.position.Y;
				this.stepSpeed = 4f;
			}
			this.position.X = vector.X;
			this.position.Y = vector.Y;
			this.velocity.X = vector.Z;
			this.velocity.Y = vector.W;
			if (this.gravDir == -1f && this.velocity.Y == 0.0101f)
			{
				this.velocity.Y = 0f;
			}
		}
		public void FloorVisuals(bool Falling)
		{
			int num = (int)((this.position.X + (float)(this.width / 2)) / 16f);
			int num2 = (int)((this.position.Y + (float)this.height) / 16f);
			int num3 = -1;
			if (Main.tile[num - 1, num2] == null)
			{
				Main.tile[num - 1, num2] = new Tile();
			}
			if (Main.tile[num + 1, num2] == null)
			{
				Main.tile[num + 1, num2] = new Tile();
			}
			if (Main.tile[num, num2] == null)
			{
				Main.tile[num, num2] = new Tile();
			}
			if (Main.tile[num, num2].nactive() && Main.tileSolid[(int)Main.tile[num, num2].type])
			{
				num3 = (int)Main.tile[num, num2].type;
			}
			else
			{
				if (Main.tile[num - 1, num2].nactive() && Main.tileSolid[(int)Main.tile[num - 1, num2].type])
				{
					num3 = (int)Main.tile[num - 1, num2].type;
				}
				else
				{
					if (Main.tile[num + 1, num2].nactive() && Main.tileSolid[(int)Main.tile[num + 1, num2].type])
					{
						num3 = (int)Main.tile[num + 1, num2].type;
					}
				}
			}
			if (num3 > -1)
			{
				if (num3 == 229)
				{
					this.sticky = true;
				}
				else
				{
					this.sticky = false;
				}
				if (num3 == 161 || num3 == 162 || num3 == 163 || num3 == 164 || num3 == 200)
				{
					this.slippy = true;
				}
				else
				{
					this.slippy = false;
				}
				if (num3 == 197)
				{
					this.slippy2 = true;
				}
				else
				{
					this.slippy2 = false;
				}
				if (num3 == 198)
				{
					this.powerrun = true;
				}
				else
				{
					this.powerrun = false;
				}
				if (Main.tile[num - 1, num2].slope() != 0 || Main.tile[num, num2].slope() != 0 || Main.tile[num + 1, num2].slope() != 0)
				{
					num3 = -1;
				}
				if (!this.wet && this.mount.Type != 6 && (num3 == 147 || num3 == 25 || num3 == 53 || num3 == 189 || num3 == 0 || num3 == 123 || num3 == 57 || num3 == 112 || num3 == 116 || num3 == 196 || num3 == 193 || num3 == 195 || num3 == 197 || num3 == 199 || num3 == 229))
				{
					int num4 = 1;
					if (Falling)
					{
						num4 = 20;
					}
					for (int i = 0; i < num4; i++)
					{
						bool flag = true;
						int num5 = 76;
						if (num3 == 53)
						{
							num5 = 32;
						}
						if (num3 == 189)
						{
							num5 = 16;
						}
						if (num3 == 0)
						{
							num5 = 0;
						}
						if (num3 == 123)
						{
							num5 = 53;
						}
						if (num3 == 57)
						{
							num5 = 36;
						}
						if (num3 == 112)
						{
							num5 = 14;
						}
						if (num3 == 116)
						{
							num5 = 51;
						}
						if (num3 == 196)
						{
							num5 = 108;
						}
						if (num3 == 193)
						{
							num5 = 4;
						}
						if (num3 == 195 || num3 == 199)
						{
							num5 = 5;
						}
						if (num3 == 197)
						{
							num5 = 4;
						}
						if (num3 == 229)
						{
							num5 = 153;
						}
						if (num3 == 25)
						{
							num5 = 37;
						}
						if (num5 == 32 && Main.rand.Next(2) == 0)
						{
							flag = false;
						}
						if (num5 == 14 && Main.rand.Next(2) == 0)
						{
							flag = false;
						}
						if (num5 == 51 && Main.rand.Next(2) == 0)
						{
							flag = false;
						}
						if (num5 == 36 && Main.rand.Next(2) == 0)
						{
							flag = false;
						}
						if (num5 == 0 && Main.rand.Next(3) != 0)
						{
							flag = false;
						}
						if (num5 == 53 && Main.rand.Next(3) != 0)
						{
							flag = false;
						}
						Color newColor = default(Color);
						if (num3 == 193)
						{
							newColor = new Color(30, 100, 255, 100);
						}
						if (num3 == 197)
						{
							newColor = new Color(97, 200, 255, 100);
						}
						if (!Falling)
						{
							float num6 = Math.Abs(this.velocity.X) / 3f;
							if ((float)Main.rand.Next(100) > num6 * 100f)
							{
								flag = false;
							}
						}
						if (flag)
						{
							float num7 = this.velocity.X;
							if (num7 > 6f)
							{
								num7 = 6f;
							}
							if (num7 < -6f)
							{
								num7 = -6f;
							}
							if (this.velocity.X != 0f || Falling)
							{
								int num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)this.height - 2f), this.width, 6, num5, 0f, 0f, 50, newColor, 1f);
								if (num5 == 76)
								{
									Main.dust[num8].scale += (float)Main.rand.Next(3) * 0.1f;
									Main.dust[num8].noLight = true;
								}
								if (num5 == 16 || num5 == 108 || num5 == 153)
								{
									Main.dust[num8].scale += (float)Main.rand.Next(6) * 0.1f;
								}
								if (num5 == 37)
								{
									Main.dust[num8].scale += 0.25f;
									Main.dust[num8].alpha = 50;
								}
								if (num5 == 5)
								{
									Main.dust[num8].scale += (float)Main.rand.Next(2, 8) * 0.1f;
								}
								Main.dust[num8].noGravity = true;
								if (num4 > 1)
								{
									Dust expr_5D4_cp_0 = Main.dust[num8];
									expr_5D4_cp_0.velocity.X = expr_5D4_cp_0.velocity.X * 1.2f;
									Dust expr_5F2_cp_0 = Main.dust[num8];
									expr_5F2_cp_0.velocity.Y = expr_5F2_cp_0.velocity.Y * 0.8f;
									Dust expr_610_cp_0 = Main.dust[num8];
									expr_610_cp_0.velocity.Y = expr_610_cp_0.velocity.Y - 1f;
									Main.dust[num8].velocity *= 0.8f;
									Main.dust[num8].scale += (float)Main.rand.Next(3) * 0.1f;
									Main.dust[num8].velocity.X = (Main.dust[num8].position.X - (this.position.X + (float)(this.width / 2))) * 0.2f;
									if (Main.dust[num8].velocity.Y > 0f)
									{
										Dust expr_6CA_cp_0 = Main.dust[num8];
										expr_6CA_cp_0.velocity.Y = expr_6CA_cp_0.velocity.Y * -1f;
									}
									Dust expr_6E8_cp_0 = Main.dust[num8];
									expr_6E8_cp_0.velocity.X = expr_6E8_cp_0.velocity.X + num7 * 0.3f;
								}
								else
								{
									Main.dust[num8].velocity *= 0.2f;
								}
								Dust expr_728_cp_0 = Main.dust[num8];
								expr_728_cp_0.position.X = expr_728_cp_0.position.X - num7 * 1f;
							}
						}
					}
				}
			}
		}
		public void BordersMovement()
		{
			if (this.position.X < Main.leftWorld + 640f + 16f)
			{
				this.position.X = Main.leftWorld + 640f + 16f;
				this.velocity.X = 0f;
			}
			if (this.position.X + (float)this.width > Main.rightWorld - 640f - 32f)
			{
				this.position.X = Main.rightWorld - 640f - 32f - (float)this.width;
				this.velocity.X = 0f;
			}
			if (this.position.Y < Main.topWorld + 640f + 16f)
			{
				this.position.Y = Main.topWorld + 640f + 16f;
				if ((double)this.velocity.Y < 0.11)
				{
					this.velocity.Y = 0.11f;
				}
				this.gravDir = 1f;
			}
			if (this.position.Y > Main.bottomWorld - 640f - 32f - (float)this.height)
			{
				this.position.Y = Main.bottomWorld - 640f - 32f - (float)this.height;
				this.velocity.Y = 0f;
			}
		}
		public void UpdatePlayer(int i)
		{
			if (this.launcherWait > 0)
			{
				this.launcherWait--;
			}
			this.maxFallSpeed = 10f;
			this.gravity = Player.defaultGravity;
			Player.jumpHeight = 15;
			Player.jumpSpeed = 5.01f;
			this.maxRunSpeed = 3f;
			this.runAcceleration = 0.08f;
			this.runSlowdown = 0.2f;
			this.accRunSpeed = this.maxRunSpeed;
			if (!this.mount.Active || this.mount.Type != 6)
			{
				this.onWrongGround = false;
			}
			this.heldProj = -1;
			if (this.wet)
			{
				if (this.honeyWet)
				{
					this.gravity = 0.1f;
					this.maxFallSpeed = 3f;
				}
				else
				{
					if (this.merman)
					{
						this.gravity = 0.3f;
						this.maxFallSpeed = 7f;
					}
					else
					{
						this.gravity = 0.2f;
						this.maxFallSpeed = 5f;
						Player.jumpHeight = 30;
						Player.jumpSpeed = 6.01f;
					}
				}
			}
			this.maxFallSpeed += 0.01f;
			bool flag = false;
			if (this.active)
			{
				if (this.ghostDmg > 0f)
				{
					this.ghostDmg -= 2.5f;
				}
				if (this.ghostDmg < 0f)
				{
					this.ghostDmg = 0f;
				}
				if (this.lifeSteal < 80f)
				{
					this.lifeSteal += 0.6f;
				}
				if (this.lifeSteal > 80f)
				{
					this.lifeSteal = 80f;
				}
				if (this.mount.Active)
				{
					this.position.Y = this.position.Y + (float)this.height;
					this.height = 42 + this.mount.HeightBoost;
					this.position.Y = this.position.Y - (float)this.height;
					if (this.mount.Type == 0)
					{
						int num = (int)(this.position.X + (float)(this.width / 2)) / 16;
						int j = (int)(this.position.Y + (float)(this.height / 2) - 14f) / 16;
						Lighting.addLight(num, j, 0.5f, 0.2f, 0.05f);
						Lighting.addLight(num + this.direction, j, 0.5f, 0.2f, 0.05f);
						Lighting.addLight(num + this.direction * 2, j, 0.5f, 0.2f, 0.05f);
					}
				}
				else
				{
					this.position.Y = this.position.Y + (float)this.height;
					this.height = 42;
					this.position.Y = this.position.Y - (float)this.height;
				}
				Main.numPlayers++;
				this.outOfRange = false;
				if (this.whoAmi != Main.myPlayer)
				{
					int num2 = (int)(this.position.X + (float)(this.width / 2)) / 16;
					int num3 = (int)(this.position.Y + (float)(this.height / 2)) / 16;
					if (Main.tile[num2, num3] == null)
					{
						flag = true;
					}
					else
					{
						if (Main.tile[num2 - 3, num3] == null)
						{
							flag = true;
						}
						else
						{
							if (Main.tile[num2 + 3, num3] == null)
							{
								flag = true;
							}
							else
							{
								if (Main.tile[num2, num3 - 3] == null)
								{
									flag = true;
								}
								else
								{
									if (Main.tile[num2, num3 + 3] == null)
									{
										flag = true;
									}
								}
							}
						}
					}
					if (flag)
					{
						this.outOfRange = true;
						this.numMinions = 0;
						this.slotsMinions = 0f;
						this.itemAnimation = 0;
						this.PlayerFrame();
					}
				}
			}
			if (!this.active || flag)
			{
				return;
			}
			this.miscCounter++;
			if (this.miscCounter >= 300)
			{
				this.miscCounter = 0;
			}
			this.infernoCounter++;
			if (this.infernoCounter >= 180)
			{
				this.infernoCounter = 0;
			}
			float num4 = (float)(Main.maxTilesX / 4200);
			num4 *= num4;
			float num5 = (float)((double)(this.position.Y / 16f - (60f + 10f * num4)) / (Main.worldSurface / 6.0));
			if ((double)num5 < 0.25)
			{
				num5 = 0.25f;
			}
			if (num5 > 1f)
			{
				num5 = 1f;
			}
			this.gravity *= num5;
			this.maxRegenDelay = (1f - (float)this.statMana / (float)this.statManaMax2) * 60f * 4f + 45f;
			this.maxRegenDelay *= 0.7f;
			this.UpdateSocialShadow();
			this.UpdateTeleportVisuals();
			this.whoAmi = i;
			if (this.runSoundDelay > 0)
			{
				this.runSoundDelay--;
			}
			if (this.attackCD > 0)
			{
				this.attackCD--;
			}
			if (this.itemAnimation == 0)
			{
				this.attackCD = 0;
			}
			if (this.chatShowTime > 0)
			{
				this.chatShowTime--;
			}
			if (this.potionDelay > 0)
			{
				this.potionDelay--;
			}
			if (i == Main.myPlayer)
			{
				if (Main.trashItem.type >= 1522 && Main.trashItem.type <= 1527)
				{
					Main.trashItem.SetDefaults(0, false);
				}
				this.UpdateBiomes();
			}
			if (this.ghost)
			{
				this.Ghost();
				return;
			}
			if (this.dead)
			{
				this.UpdateDead();
				return;
			}
			if (i == Main.myPlayer)
			{
				this.controlUp = false;
				this.controlLeft = false;
				this.controlDown = false;
				this.controlRight = false;
				this.controlJump = false;
				this.controlUseItem = false;
				this.controlUseTile = false;
				this.controlThrow = false;
				this.controlInv = false;
				this.controlHook = false;
				this.controlTorch = false;
				this.controlSmart = false;
				this.mapStyle = false;
				this.mapAlphaDown = false;
				this.mapAlphaUp = false;
				this.mapFullScreen = false;
				this.mapZoomIn = false;
				this.mapZoomOut = false;
				if (Main.hasFocus)
				{
					if (!Main.chatMode && !Main.editSign && !Main.editChest && !Main.blockInput)
					{
						Keys[] pressedKeys = Main.keyState.GetPressedKeys();
						if (Main.blockKey != Keys.None)
						{
							bool flag2 = false;
							for (int k = 0; k < pressedKeys.Length; k++)
							{
								if (pressedKeys[k] == Main.blockKey)
								{
									pressedKeys[k] = Keys.None;
									flag2 = true;
								}
							}
							if (!flag2)
							{
								Main.blockKey = Keys.None;
							}
						}
						bool flag3 = false;
						bool flag4 = false;
						for (int l = 0; l < pressedKeys.Length; l++)
						{
							string a = string.Concat(pressedKeys[l]);
							if (a == Main.cUp)
							{
								this.controlUp = true;
							}
							if (a == Main.cLeft)
							{
								this.controlLeft = true;
							}
							if (a == Main.cDown)
							{
								this.controlDown = true;
							}
							if (a == Main.cRight)
							{
								this.controlRight = true;
							}
							if (a == Main.cJump)
							{
								this.controlJump = true;
							}
							if (a == Main.cThrowItem)
							{
								this.controlThrow = true;
							}
							if (a == Main.cInv)
							{
								this.controlInv = true;
							}
							if (a == Main.cBuff)
							{
								this.QuickBuff();
							}
							if (a == Main.cHeal)
							{
								flag4 = true;
							}
							if (a == Main.cMana)
							{
								flag3 = true;
							}
							if (a == Main.cHook)
							{
								this.controlHook = true;
							}
							if (a == Main.cTorch)
							{
								this.controlTorch = true;
							}
							if (a == Main.cSmart)
							{
								this.controlSmart = true;
							}
							if (Main.mapEnabled)
							{
								if (a == Main.cMapZoomIn)
								{
									this.mapZoomIn = true;
								}
								if (a == Main.cMapZoomOut)
								{
									this.mapZoomOut = true;
								}
								if (a == Main.cMapAlphaUp)
								{
									this.mapAlphaUp = true;
								}
								if (a == Main.cMapAlphaDown)
								{
									this.mapAlphaDown = true;
								}
								if (a == Main.cMapFull)
								{
									this.mapFullScreen = true;
								}
								if (a == Main.cMapStyle)
								{
									this.mapStyle = true;
								}
							}
						}
						if (Main.gamePad)
						{
							GamePadState state = GamePad.GetState(PlayerIndex.One);
							if (state.DPad.Up == ButtonState.Pressed)
							{
								this.controlUp = true;
							}
							if (state.DPad.Down == ButtonState.Pressed)
							{
								this.controlDown = true;
							}
							if (state.DPad.Left == ButtonState.Pressed)
							{
								this.controlLeft = true;
							}
							if (state.DPad.Right == ButtonState.Pressed)
							{
								this.controlRight = true;
							}
							if (state.Triggers.Left > 0f)
							{
								this.controlJump = true;
							}
							if (state.Triggers.Right > 0f)
							{
								this.controlUseItem = true;
							}
							Main.mouseX = (int)((float)(Main.screenWidth / 2) + state.ThumbSticks.Right.X * (float)Player.tileRangeX * 16f);
							Main.mouseY = (int)((float)(Main.screenHeight / 2) - state.ThumbSticks.Right.Y * (float)Player.tileRangeX * 16f);
							if (state.ThumbSticks.Right.X == 0f)
							{
								Main.mouseX = Main.screenWidth / 2 + this.direction * 2;
							}
						}
						if (Main.mapFullscreen)
						{
							if (this.controlUp)
							{
								Main.mapFullscreenPos.Y = Main.mapFullscreenPos.Y - 1f * (16f / Main.mapFullscreenScale);
							}
							if (this.controlDown)
							{
								Main.mapFullscreenPos.Y = Main.mapFullscreenPos.Y + 1f * (16f / Main.mapFullscreenScale);
							}
							if (this.controlLeft)
							{
								Main.mapFullscreenPos.X = Main.mapFullscreenPos.X - 1f * (16f / Main.mapFullscreenScale);
							}
							if (this.controlRight)
							{
								Main.mapFullscreenPos.X = Main.mapFullscreenPos.X + 1f * (16f / Main.mapFullscreenScale);
							}
							this.controlUp = false;
							this.controlLeft = false;
							this.controlDown = false;
							this.controlRight = false;
							this.controlJump = false;
							this.controlUseItem = false;
							this.controlUseTile = false;
							this.controlThrow = false;
							this.controlHook = false;
							this.controlTorch = false;
							this.controlSmart = false;
						}
						if (flag4)
						{
							if (this.releaseQuickHeal)
							{
								this.QuickHeal();
							}
							this.releaseQuickHeal = false;
						}
						else
						{
							this.releaseQuickHeal = true;
						}
						if (flag3)
						{
							if (this.releaseQuickMana)
							{
								this.QuickMana();
							}
							this.releaseQuickMana = false;
						}
						else
						{
							this.releaseQuickMana = true;
						}
						if (this.controlLeft && this.controlRight)
						{
							this.controlLeft = false;
							this.controlRight = false;
						}
						if (Main.cSmartToggle)
						{
							if (this.controlSmart && this.releaseSmart)
							{
								Main.PlaySound(12, -1, -1, 1);
								Main.smartDigEnabled = !Main.smartDigEnabled;
							}
						}
						else
						{
							if (Main.smartDigEnabled != this.controlSmart)
							{
								Main.PlaySound(12, -1, -1, 1);
							}
							Main.smartDigEnabled = this.controlSmart;
						}
						if (this.controlSmart)
						{
							this.releaseSmart = false;
						}
						else
						{
							this.releaseSmart = true;
						}
						if (Main.mapFullscreen)
						{
							if (this.mapZoomIn)
							{
								Main.mapFullscreenScale *= 1.05f;
							}
							if (this.mapZoomOut)
							{
								Main.mapFullscreenScale *= 0.95f;
							}
						}
						else
						{
							if (Main.mapStyle == 1)
							{
								if (this.mapZoomIn)
								{
									Main.mapMinimapScale *= 1.025f;
								}
								if (this.mapZoomOut)
								{
									Main.mapMinimapScale *= 0.975f;
								}
								if (this.mapAlphaUp)
								{
									Main.mapMinimapAlpha += 0.015f;
								}
								if (this.mapAlphaDown)
								{
									Main.mapMinimapAlpha -= 0.015f;
								}
							}
							else
							{
								if (Main.mapStyle == 2)
								{
									if (this.mapZoomIn)
									{
										Main.mapOverlayScale *= 1.05f;
									}
									if (this.mapZoomOut)
									{
										Main.mapOverlayScale *= 0.95f;
									}
									if (this.mapAlphaUp)
									{
										Main.mapOverlayAlpha += 0.015f;
									}
									if (this.mapAlphaDown)
									{
										Main.mapOverlayAlpha -= 0.015f;
									}
								}
							}
							if (this.mapStyle)
							{
								if (this.releaseMapStyle)
								{
									Main.PlaySound(12, -1, -1, 1);
									Main.mapStyle++;
									if (Main.mapStyle > 2)
									{
										Main.mapStyle = 0;
									}
								}
								this.releaseMapStyle = false;
							}
							else
							{
								this.releaseMapStyle = true;
							}
						}
						if (this.mapFullScreen)
						{
							if (this.releaseMapFullscreen)
							{
								if (Main.mapFullscreen)
								{
									Main.PlaySound(11, -1, -1, 1);
									Main.mapFullscreen = false;
								}
								else
								{
									Main.playerInventory = false;
									this.talkNPC = -1;
									Main.npcChatCornerItem = 0;
									Main.PlaySound(10, -1, -1, 1);
									float mapFullscreenScale = 2.5f;
									Main.mapFullscreenScale = mapFullscreenScale;
									Main.mapFullscreen = true;
									Main.resetMapFull = true;
									Main.buffString = string.Empty;
								}
							}
							this.releaseMapFullscreen = false;
						}
						else
						{
							this.releaseMapFullscreen = true;
						}
					}
					if (this.confused)
					{
						bool flag5 = this.controlLeft;
						bool flag6 = this.controlUp;
						this.controlLeft = this.controlRight;
						this.controlRight = flag5;
						this.controlUp = this.controlRight;
						this.controlDown = flag6;
					}
					else
					{
						if (this.cartFlip)
						{
							if (this.controlRight || this.controlLeft)
							{
								bool flag7 = this.controlLeft;
								this.controlLeft = this.controlRight;
								this.controlRight = flag7;
							}
							else
							{
								this.cartFlip = false;
							}
						}
					}
					if (Main.mouseLeft)
					{
						if (!Main.blockMouse && !this.mouseInterface)
						{
							this.controlUseItem = true;
						}
					}
					else
					{
						Main.blockMouse = false;
					}
					if (Main.mouseRight && !this.mouseInterface && !Main.blockMouse)
					{
						this.controlUseTile = true;
					}
					if (this.controlInv)
					{
						if (this.releaseInventory)
						{
							if (Main.mapFullscreen)
							{
								Main.mapFullscreen = false;
								this.releaseInventory = false;
								Main.PlaySound(11, -1, -1, 1);
							}
							else
							{
								this.toggleInv();
							}
						}
						this.releaseInventory = false;
					}
					else
					{
						this.releaseInventory = true;
					}
					if (this.delayUseItem)
					{
						if (!this.controlUseItem)
						{
							this.delayUseItem = false;
						}
						this.controlUseItem = false;
					}
					if (this.itemAnimation == 0 && this.itemTime == 0)
					{
						this.dropItemCheck();
						int num6 = this.selectedItem;
						if (!Main.chatMode && this.selectedItem != 58 && !Main.editSign && !Main.editChest)
						{
							if (Main.keyState.IsKeyDown(Keys.D1))
							{
								this.selectedItem = 0;
							}
							if (Main.keyState.IsKeyDown(Keys.D2))
							{
								this.selectedItem = 1;
							}
							if (Main.keyState.IsKeyDown(Keys.D3))
							{
								this.selectedItem = 2;
							}
							if (Main.keyState.IsKeyDown(Keys.D4))
							{
								this.selectedItem = 3;
							}
							if (Main.keyState.IsKeyDown(Keys.D5))
							{
								this.selectedItem = 4;
							}
							if (Main.keyState.IsKeyDown(Keys.D6))
							{
								this.selectedItem = 5;
							}
							if (Main.keyState.IsKeyDown(Keys.D7))
							{
								this.selectedItem = 6;
							}
							if (Main.keyState.IsKeyDown(Keys.D8))
							{
								this.selectedItem = 7;
							}
							if (Main.keyState.IsKeyDown(Keys.D9))
							{
								this.selectedItem = 8;
							}
							if (Main.keyState.IsKeyDown(Keys.D0))
							{
								this.selectedItem = 9;
							}
						}
						if (num6 != this.selectedItem)
						{
							Main.PlaySound(12, -1, -1, 1);
						}
						if (Main.mapFullscreen)
						{
							int num7 = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
							Main.mapFullscreenScale *= 1f + (float)num7 * 0.3f;
						}
						else
						{
							if (!Main.playerInventory)
							{
								int m;
								for (m = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120; m > 9; m -= 10)
								{
								}
								while (m < 0)
								{
									m += 10;
								}
								this.selectedItem -= m;
								if (m != 0)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								if (this.changeItem >= 0)
								{
									if (this.selectedItem != this.changeItem)
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.selectedItem = this.changeItem;
									this.changeItem = -1;
								}
								if (this.itemAnimation == 0)
								{
									while (this.selectedItem > 9)
									{
										this.selectedItem -= 10;
									}
									while (this.selectedItem < 0)
									{
										this.selectedItem += 10;
									}
								}
							}
							else
							{
								int num8 = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
								Main.focusRecipe += num8;
								if (Main.focusRecipe > Main.numAvailableRecipes - 1)
								{
									Main.focusRecipe = Main.numAvailableRecipes - 1;
								}
								if (Main.focusRecipe < 0)
								{
									Main.focusRecipe = 0;
								}
							}
						}
					}
				}
				if (this.selectedItem == 58)
				{
					this.nonTorch = -1;
				}
				else
				{
					this.SmartitemLookup();
				}
				if (this.frozen)
				{
					this.controlJump = false;
					this.controlDown = false;
					this.controlLeft = false;
					this.controlRight = false;
					this.controlUp = false;
					this.controlUseItem = false;
					this.controlUseTile = false;
					this.controlThrow = false;
				}
				if (!this.controlThrow)
				{
					this.releaseThrow = true;
				}
				else
				{
					this.releaseThrow = false;
				}
				if (Main.netMode == 1)
				{
					bool flag8 = false;
					if (this.controlUp != Main.clientPlayer.controlUp)
					{
						flag8 = true;
					}
					if (this.controlDown != Main.clientPlayer.controlDown)
					{
						flag8 = true;
					}
					if (this.controlLeft != Main.clientPlayer.controlLeft)
					{
						flag8 = true;
					}
					if (this.controlRight != Main.clientPlayer.controlRight)
					{
						flag8 = true;
					}
					if (this.controlJump != Main.clientPlayer.controlJump)
					{
						flag8 = true;
					}
					if (this.controlUseItem != Main.clientPlayer.controlUseItem)
					{
						flag8 = true;
					}
					if (this.selectedItem != Main.clientPlayer.selectedItem)
					{
						flag8 = true;
					}
					if (flag8)
					{
						NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
					}
				}
				if (Main.playerInventory)
				{
					this.AdjTiles();
				}
				if (this.chest != -1)
				{
					int num9 = (int)(((double)this.position.X + (double)this.width * 0.5) / 16.0);
					int num10 = (int)(((double)this.position.Y + (double)this.height * 0.5) / 16.0);
					if (num9 < this.chestX - Player.tileRangeX || num9 > this.chestX + Player.tileRangeX + 1 || num10 < this.chestY - Player.tileRangeY || num10 > this.chestY + Player.tileRangeY + 1)
					{
						if (this.chest != -1)
						{
							Main.PlaySound(11, -1, -1, 1);
						}
						this.chest = -1;
					}
					if (!Main.tile[this.chestX, this.chestY].active())
					{
						Main.PlaySound(11, -1, -1, 1);
						this.chest = -1;
					}
				}
				if (this.velocity.Y <= 0f)
				{
					this.fallStart2 = (int)(this.position.Y / 16f);
				}
				if (this.velocity.Y == 0f)
				{
					int num11 = 25;
					num11 += this.extraFall;
					int num12 = (int)(this.position.Y / 16f) - this.fallStart;
					if (this.mount.CanFly)
					{
						num12 = 0;
					}
					if (this.mount.Type == 6 && Minecart.OnTrack(this.position, this.width, this.height))
					{
						num12 = 0;
					}
					this.mount.FatigueRecovery();
					bool flag9 = false;
					for (int n = 3; n < 8; n++)
					{
						if (this.armor[n].stack > 0 && this.armor[n].wingSlot > -1)
						{
							flag9 = true;
						}
					}
					if (((this.gravDir == 1f && num12 > num11) || (this.gravDir == -1f && num12 < -num11)) && !this.noFallDmg && !flag9)
					{
						int num13 = (int)((float)num12 * this.gravDir - (float)num11) * 10;
						if (this.mount.Active)
						{
							num13 = (int)((float)num13 * this.mount.FallDamage);
						}
						this.immune = false;
						this.Hurt(num13, 0, false, false, Lang.deathMsg(-1, -1, -1, 0), false);
					}
					this.fallStart = (int)(this.position.Y / 16f);
				}
				if (this.jump > 0 || this.rocketDelay > 0 || this.wet || this.slowFall || (double)num5 < 0.8 || this.tongued)
				{
					this.fallStart = (int)(this.position.Y / 16f);
				}
			}
			if (this.mouseInterface)
			{
				this.delayUseItem = true;
			}
			Player.tileTargetX = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
			Player.tileTargetY = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
			if (this.gravDir == -1f)
			{
				Player.tileTargetY = (int)((Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16f);
			}
			if (Player.tileTargetX >= Main.maxTilesX - 5)
			{
				Player.tileTargetX = Main.maxTilesX - 5;
			}
			if (Player.tileTargetY >= Main.maxTilesY - 5)
			{
				Player.tileTargetY = Main.maxTilesY - 5;
			}
			if (Player.tileTargetX < 5)
			{
				Player.tileTargetX = 5;
			}
			if (Player.tileTargetY < 5)
			{
				Player.tileTargetY = 5;
			}
			if (Main.tile[Player.tileTargetX - 1, Player.tileTargetY] == null)
			{
				Main.tile[Player.tileTargetX - 1, Player.tileTargetY] = new Tile();
			}
			if (Main.tile[Player.tileTargetX + 1, Player.tileTargetY] == null)
			{
				Main.tile[Player.tileTargetX + 1, Player.tileTargetY] = new Tile();
			}
			if (Main.tile[Player.tileTargetX, Player.tileTargetY] == null)
			{
				Main.tile[Player.tileTargetX, Player.tileTargetY] = new Tile();
			}
			if (!Main.tile[Player.tileTargetX, Player.tileTargetY].active())
			{
				if (Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active() && Main.tile[Player.tileTargetX - 1, Player.tileTargetY].type == 323)
				{
					int frameY = (int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY].frameY;
					if (frameY < -4)
					{
						Player.tileTargetX++;
					}
					if (frameY > 4)
					{
						Player.tileTargetX--;
					}
				}
				else
				{
					if (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active() && Main.tile[Player.tileTargetX + 1, Player.tileTargetY].type == 323)
					{
						int frameY2 = (int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY].frameY;
						if (frameY2 < -4)
						{
							Player.tileTargetX++;
						}
						if (frameY2 > 4)
						{
							Player.tileTargetX--;
						}
					}
				}
			}
			this.SmartCursorLookup();
			this.UpdateImmunity();
			if (this.petalTimer > 0)
			{
				this.petalTimer--;
			}
			if (this.shadowDodgeTimer > 0)
			{
				this.shadowDodgeTimer--;
			}
			if (this.jump > 0 || this.velocity.Y != 0f)
			{
				this.slippy = false;
				this.slippy2 = false;
				this.powerrun = false;
				this.sticky = false;
			}
			this.potionDelayTime = Item.potionDelay;
			if (this.pStone)
			{
				this.potionDelayTime -= 900;
			}
			this.ResetEffects();
			this.meleeCrit += this.inventory[this.selectedItem].crit;
			this.magicCrit += this.inventory[this.selectedItem].crit;
			this.rangedCrit += this.inventory[this.selectedItem].crit;
			if (this.whoAmi == Main.myPlayer)
			{
				Main.musicBox2 = -1;
				if (Main.waterCandles > 0)
				{
					this.AddBuff(86, 2, false);
				}
				if (Main.campfire)
				{
					this.AddBuff(87, 2, false);
				}
				if (Main.heartLantern)
				{
					this.AddBuff(89, 2, false);
				}
			}
			for (int num14 = 0; num14 < 140; num14++)
			{
				this.buffImmune[num14] = false;
			}
			this.UpdateBuffs(i);
			if (this.accMerman && this.wet && !this.lavaWet)
			{
				this.releaseJump = true;
				this.wings = 0;
				this.merman = true;
				this.accFlipper = true;
				this.AddBuff(34, 2, true);
			}
			else
			{
				this.merman = false;
			}
			this.accMerman = false;
			if (this.wolfAcc && !this.merman && !Main.dayTime && !this.wereWolf)
			{
				this.AddBuff(28, 60, true);
			}
			this.wolfAcc = false;
			if (this.whoAmi == Main.myPlayer)
			{
				for (int num15 = 0; num15 < 22; num15++)
				{
					if (this.buffType[num15] > 0 && this.buffTime[num15] <= 0)
					{
						this.DelBuff(num15);
					}
				}
			}
			this.beetleDefense = false;
			this.beetleOffense = false;
			this.doubleJump = false;
			this.head = this.armor[0].headSlot;
			this.body = this.armor[1].bodySlot;
			this.legs = this.armor[2].legSlot;
			this.handon = -1;
			this.handoff = -1;
			this.back = -1;
			this.front = -1;
			this.shoe = -1;
			this.waist = -1;
			this.shield = -1;
			this.neck = -1;
			this.face = -1;
			this.balloon = -1;
			this.UpdateEquips(i);
			if (this.mount.Active)
			{
				this.autoJump = this.mount.AutoJump;
			}
			this.gemCount++;
			if (this.gemCount >= 10)
			{
				this.gem = -1;
				this.gemCount = 0;
				for (int num16 = 0; num16 <= 58; num16++)
				{
					if (this.inventory[num16].type == 0 || this.inventory[num16].stack == 0)
					{
						this.inventory[num16].type = 0;
						this.inventory[num16].stack = 0;
						this.inventory[num16].name = "";
						this.inventory[num16].netID = 0;
					}
					if (this.inventory[num16].type >= 1522 && this.inventory[num16].type <= 1527)
					{
						this.gem = this.inventory[num16].type - 1522;
					}
				}
			}
			if (this.head == 11)
			{
				int i2 = (int)(this.position.X + (float)(this.width / 2) + (float)(8 * this.direction)) / 16;
				int j2 = (int)(this.position.Y + 2f) / 16;
				Lighting.addLight(i2, j2, 0.92f, 0.8f, 0.65f);
			}
			this.UpdateArmorSets(i);
			if (this.merman)
			{
				this.wings = 0;
			}
			if (this.armorSteath)
			{
				if (this.itemAnimation > 0)
				{
					this.stealthTimer = 5;
				}
				if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1 && (double)this.velocity.Y > -0.1 && (double)this.velocity.Y < 0.1)
				{
					if (this.stealthTimer == 0)
					{
						this.stealth -= 0.015f;
						if ((double)this.stealth < 0.0)
						{
							this.stealth = 0f;
						}
					}
				}
				else
				{
					float num17 = Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y);
					this.stealth += num17 * 0.0075f;
					if (this.stealth > 1f)
					{
						this.stealth = 1f;
					}
				}
				this.rangedDamage += (1f - this.stealth) * 0.6f;
				this.rangedCrit += (int)((1f - this.stealth) * 10f);
				this.aggro -= (int)((1f - this.stealth) * 750f);
				if (this.stealthTimer > 0)
				{
					this.stealthTimer--;
				}
			}
			else
			{
				this.stealth = 1f;
			}
			if (this.manaSick)
			{
				this.magicDamage *= 1f - this.manaSickReduction;
			}
			if (this.inventory[this.selectedItem].type == 1947)
			{
				this.meleeSpeed = (1f + this.meleeSpeed) / 2f;
			}
			if ((double)this.pickSpeed < 0.3)
			{
				this.pickSpeed = 0.3f;
			}
			if (this.meleeSpeed > 3f)
			{
				this.meleeSpeed = 3f;
			}
			if ((double)this.moveSpeed > 1.6)
			{
				this.moveSpeed = 1.6f;
			}
			if (this.tileSpeed > 3f)
			{
				this.tileSpeed = 3f;
			}
			this.tileSpeed = 1f / this.tileSpeed;
			if (this.wallSpeed > 3f)
			{
				this.wallSpeed = 3f;
			}
			this.wallSpeed = 1f / this.wallSpeed;
			if (this.statManaMax2 > 400)
			{
				this.statManaMax2 = 400;
			}
			if (this.statDefense < 0)
			{
				this.statDefense = 0;
			}
			if (this.slow)
			{
				this.moveSpeed *= 0.5f;
			}
			if (this.chilled)
			{
				this.moveSpeed *= 0.75f;
			}
			this.meleeSpeed = 1f / this.meleeSpeed;
			this.UpdateLifeRegen();
			this.UpdateManaRegen();
			if (this.manaRegenCount < 0)
			{
				this.manaRegenCount = 0;
			}
			if (this.statMana > this.statManaMax2)
			{
				this.statMana = this.statManaMax2;
			}
			this.runAcceleration *= this.moveSpeed;
			this.maxRunSpeed *= this.moveSpeed;
			this.UpdateJumpHeight();
			for (int num18 = 0; num18 < 22; num18++)
			{
				if (this.buffType[num18] > 0 && this.buffTime[num18] > 0 && this.buffImmune[this.buffType[num18]])
				{
					this.DelBuff(num18);
				}
			}
			if (this.brokenArmor)
			{
				this.statDefense /= 2;
			}
			if (this.mount.Active && this.mount.BlockExtraJumps)
			{
				this.jumpAgain = false;
				this.jumpAgain2 = false;
				this.jumpAgain3 = false;
				this.jumpAgain4 = false;
			}
			else
			{
				if (!this.doubleJump)
				{
					this.jumpAgain = false;
				}
				else
				{
					if (this.velocity.Y == 0f || this.sliding)
					{
						this.jumpAgain = true;
					}
				}
				if (!this.doubleJump2)
				{
					this.jumpAgain2 = false;
				}
				else
				{
					if (this.velocity.Y == 0f || this.sliding)
					{
						this.jumpAgain2 = true;
					}
				}
				if (!this.doubleJump3)
				{
					this.jumpAgain3 = false;
				}
				else
				{
					if (this.velocity.Y == 0f || this.sliding)
					{
						this.jumpAgain3 = true;
					}
				}
				if (!this.doubleJump4)
				{
					this.jumpAgain4 = false;
				}
				else
				{
					if (this.velocity.Y == 0f || this.sliding)
					{
						this.jumpAgain4 = true;
					}
				}
			}
			if (!this.carpet)
			{
				this.canCarpet = false;
				this.carpetFrame = -1;
			}
			else
			{
				if (this.velocity.Y == 0f || this.sliding)
				{
					this.canCarpet = true;
					this.carpetTime = 0;
					this.carpetFrame = -1;
					this.carpetFrameCounter = 0f;
				}
			}
			if (this.gravDir == -1f)
			{
				this.canCarpet = false;
			}
			if (this.ropeCount > 0)
			{
				this.ropeCount--;
			}
			if (!this.pulley && !this.frozen && !this.controlJump && this.gravDir == 1f && this.ropeCount == 0 && this.grappling[0] == -1 && !this.tongued && !this.mount.Active)
			{
				this.FindPulley();
			}
			if (this.pulley)
			{
				if (this.mount.Active)
				{
					this.pulley = false;
				}
				this.sandStorm = false;
				this.dJumpEffect = false;
				this.dJumpEffect2 = false;
				this.dJumpEffect3 = false;
				this.dJumpEffect4 = false;
				int num19 = (int)(this.position.X + (float)(this.width / 2)) / 16;
				int num20 = (int)(this.position.Y - 8f) / 16;
				bool flag10 = false;
				if (this.pulleyDir == 0)
				{
					this.pulleyDir = 1;
				}
				if (this.pulleyDir == 1)
				{
					if (this.direction == -1 && this.controlLeft && (this.releaseLeft || this.leftTimer == 0))
					{
						this.pulleyDir = 2;
						flag10 = true;
					}
					else
					{
						if ((this.direction == 1 && this.controlRight && this.releaseRight) || this.rightTimer == 0)
						{
							this.pulleyDir = 2;
							flag10 = true;
						}
						else
						{
							if (this.direction == 1 && this.controlLeft)
							{
								this.direction = -1;
								flag10 = true;
							}
							if (this.direction == -1 && this.controlRight)
							{
								this.direction = 1;
								flag10 = true;
							}
						}
					}
				}
				else
				{
					if (this.pulleyDir == 2)
					{
						if (this.direction == 1 && this.controlLeft)
						{
							flag10 = true;
							int num21 = num19 * 16 + 8 - this.width / 2;
							if (!Collision.SolidCollision(new Vector2((float)num21, this.position.Y), this.width, this.height))
							{
								this.pulleyDir = 1;
								this.direction = -1;
								flag10 = true;
							}
						}
						if (this.direction == -1 && this.controlRight)
						{
							flag10 = true;
							int num22 = num19 * 16 + 8 - this.width / 2;
							if (!Collision.SolidCollision(new Vector2((float)num22, this.position.Y), this.width, this.height))
							{
								this.pulleyDir = 1;
								this.direction = 1;
								flag10 = true;
							}
						}
					}
				}
				bool flag11 = false;
				if (!flag10 && ((this.controlLeft && (this.releaseLeft || this.leftTimer == 0)) || (this.controlRight && (this.releaseRight || this.rightTimer == 0))))
				{
					int num23 = 1;
					if (this.controlLeft)
					{
						num23 = -1;
					}
					int num24 = num19 + num23;
					if (Main.tile[num24, num20].active() && Main.tileRope[(int)Main.tile[num24, num20].type])
					{
						this.pulleyDir = 1;
						this.direction = num23;
						int num25 = num24 * 16 + 8 - this.width / 2;
						float num26 = this.position.Y;
						num26 = (float)(num20 * 16 + 22);
						if ((!Main.tile[num24, num20 - 1].active() || !Main.tileRope[(int)Main.tile[num24, num20 - 1].type]) && (!Main.tile[num24, num20 + 1].active() || !Main.tileRope[(int)Main.tile[num24, num20 + 1].type]))
						{
							num26 = (float)(num20 * 16 + 22);
						}
						if (Collision.SolidCollision(new Vector2((float)num25, num26), this.width, this.height))
						{
							this.pulleyDir = 2;
							this.direction = -num23;
							if (this.direction == 1)
							{
								num25 = num24 * 16 + 8 - this.width / 2 + 6;
							}
							else
							{
								num25 = num24 * 16 + 8 - this.width / 2 + -6;
							}
						}
						if (i == Main.myPlayer)
						{
							Main.cameraX = Main.cameraX + this.position.X - (float)num25;
						}
						this.position.X = (float)num25;
						this.gfxOffY = this.position.Y - num26;
						this.position.Y = num26;
						flag11 = true;
					}
				}
				if (!flag11 && !flag10 && !this.controlUp && ((this.controlLeft && this.releaseLeft) || (this.controlRight && this.releaseRight)))
				{
					this.pulley = false;
					if (this.controlLeft && this.velocity.X == 0f)
					{
						this.velocity.X = -1f;
					}
					if (this.controlRight && this.velocity.X == 0f)
					{
						this.velocity.X = 1f;
					}
				}
				if (this.velocity.X != 0f)
				{
					this.pulley = false;
				}
				if (Main.tile[num19, num20] == null)
				{
					Main.tile[num19, num20] = new Tile();
				}
				if (!Main.tile[num19, num20].active() || !Main.tileRope[(int)Main.tile[num19, num20].type])
				{
					this.pulley = false;
				}
				if (this.gravDir != 1f)
				{
					this.pulley = false;
				}
				if (this.frozen)
				{
					this.pulley = false;
				}
				if (!this.pulley)
				{
					this.velocity.Y = this.velocity.Y - this.gravity;
				}
				if (this.controlJump)
				{
					this.pulley = false;
					this.jump = Player.jumpHeight;
					this.velocity.Y = -Player.jumpSpeed;
				}
			}
			if (this.pulley)
			{
				this.fallStart = (int)this.position.Y / 16;
				this.wingFrame = 0;
				if (this.wings == 4)
				{
					this.wingFrame = 3;
				}
				int num27 = (int)(this.position.X + (float)(this.width / 2)) / 16;
				int num28 = (int)(this.position.Y - 16f) / 16;
				int num29 = (int)(this.position.Y - 8f) / 16;
				bool flag12 = true;
				bool flag13 = false;
				if ((Main.tile[num27, num29 - 1].active() && Main.tileRope[(int)Main.tile[num27, num29 - 1].type]) || (Main.tile[num27, num29 + 1].active() && Main.tileRope[(int)Main.tile[num27, num29 + 1].type]))
				{
					flag13 = true;
				}
				if (Main.tile[num27, num28] == null)
				{
					Main.tile[num27, num28] = new Tile();
				}
				if (!Main.tile[num27, num28].active() || !Main.tileRope[(int)Main.tile[num27, num28].type])
				{
					flag12 = false;
					if (this.velocity.Y < 0f)
					{
						this.velocity.Y = 0f;
					}
				}
				if (flag13)
				{
					if (this.controlUp && flag12)
					{
						float num30 = this.position.X;
						float y = this.position.Y - Math.Abs(this.velocity.Y) - 2f;
						if (Collision.SolidCollision(new Vector2(num30, y), this.width, this.height))
						{
							num30 = (float)(num27 * 16 + 8 - this.width / 2 + 6);
							if (!Collision.SolidCollision(new Vector2(num30, y), this.width, (int)((float)this.height + Math.Abs(this.velocity.Y) + 2f)))
							{
								if (i == Main.myPlayer)
								{
									Main.cameraX = Main.cameraX + this.position.X - num30;
								}
								this.pulleyDir = 2;
								this.direction = 1;
								this.position.X = num30;
								this.velocity.X = 0f;
							}
							else
							{
								num30 = (float)(num27 * 16 + 8 - this.width / 2 + -6);
								if (!Collision.SolidCollision(new Vector2(num30, y), this.width, (int)((float)this.height + Math.Abs(this.velocity.Y) + 2f)))
								{
									if (i == Main.myPlayer)
									{
										Main.cameraX = Main.cameraX + this.position.X - num30;
									}
									this.pulleyDir = 2;
									this.direction = -1;
									this.position.X = num30;
									this.velocity.X = 0f;
								}
							}
						}
						if (this.velocity.Y > 0f)
						{
							this.velocity.Y = this.velocity.Y * 0.7f;
						}
						if (this.velocity.Y > -3f)
						{
							this.velocity.Y = this.velocity.Y - 0.2f;
						}
						else
						{
							this.velocity.Y = this.velocity.Y - 0.02f;
						}
						if (this.velocity.Y < -8f)
						{
							this.velocity.Y = -8f;
						}
					}
					else
					{
						if (this.controlDown)
						{
							float num31 = this.position.X;
							float y2 = this.position.Y;
							if (Collision.SolidCollision(new Vector2(num31, y2), this.width, (int)((float)this.height + Math.Abs(this.velocity.Y) + 2f)))
							{
								num31 = (float)(num27 * 16 + 8 - this.width / 2 + 6);
								if (!Collision.SolidCollision(new Vector2(num31, y2), this.width, (int)((float)this.height + Math.Abs(this.velocity.Y) + 2f)))
								{
									if (i == Main.myPlayer)
									{
										Main.cameraX = Main.cameraX + this.position.X - num31;
									}
									this.pulleyDir = 2;
									this.direction = 1;
									this.position.X = num31;
									this.velocity.X = 0f;
								}
								else
								{
									num31 = (float)(num27 * 16 + 8 - this.width / 2 + -6);
									if (!Collision.SolidCollision(new Vector2(num31, y2), this.width, (int)((float)this.height + Math.Abs(this.velocity.Y) + 2f)))
									{
										if (i == Main.myPlayer)
										{
											Main.cameraX = Main.cameraX + this.position.X - num31;
										}
										this.pulleyDir = 2;
										this.direction = -1;
										this.position.X = num31;
										this.velocity.X = 0f;
									}
								}
							}
							if (this.velocity.Y < 0f)
							{
								this.velocity.Y = this.velocity.Y * 0.7f;
							}
							if (this.velocity.Y < 3f)
							{
								this.velocity.Y = this.velocity.Y + 0.2f;
							}
							else
							{
								this.velocity.Y = this.velocity.Y + 0.1f;
							}
							if (this.velocity.Y > this.maxFallSpeed)
							{
								this.velocity.Y = this.maxFallSpeed;
							}
						}
						else
						{
							this.velocity.Y = this.velocity.Y * 0.7f;
							if ((double)this.velocity.Y > -0.1 && (double)this.velocity.Y < 0.1)
							{
								this.velocity.Y = 0f;
							}
						}
					}
				}
				else
				{
					if (this.controlDown)
					{
						this.ropeCount = 10;
						this.pulley = false;
						this.velocity.Y = 1f;
					}
					else
					{
						this.velocity.Y = 0f;
						this.position.Y = (float)(num28 * 16 + 22);
					}
				}
				float num32 = (float)(num27 * 16 + 8 - this.width / 2);
				if (this.pulleyDir == 1)
				{
					num32 = (float)(num27 * 16 + 8 - this.width / 2);
				}
				if (this.pulleyDir == 2)
				{
					num32 = (float)(num27 * 16 + 8 - this.width / 2 + 6 * this.direction);
				}
				if (i == Main.myPlayer)
				{
					Main.cameraX = Main.cameraX + this.position.X - num32;
				}
				this.position.X = num32;
				this.pulleyFrameCounter += Math.Abs(this.velocity.Y * 0.75f);
				if (this.velocity.Y != 0f)
				{
					this.pulleyFrameCounter += 0.75f;
				}
				if (this.pulleyFrameCounter > 10f)
				{
					this.pulleyFrame++;
					this.pulleyFrameCounter = 0f;
				}
				if (this.pulleyFrame > 1)
				{
					this.pulleyFrame = 0;
				}
				this.canCarpet = true;
				this.carpetFrame = -1;
				this.wingTime = (float)this.wingTimeMax;
				this.rocketTime = this.rocketTimeMax;
				this.rocketDelay = 0;
				this.rocketFrame = false;
				this.canRocket = false;
				this.rocketRelease = false;
			}
			else
			{
				if (this.grappling[0] == -1 && !this.tongued)
				{
					if (this.wingsLogic == 3 && this.velocity.Y == 0f)
					{
						this.accRunSpeed = 8.5f;
					}
					if (this.wingsLogic == 3 && Main.myPlayer == this.whoAmi)
					{
						this.accRunSpeed = 0f;
					}
					if (this.wingsLogic > 0 && this.velocity.Y != 0f && !this.merman)
					{
						if (this.wingsLogic == 1 || this.wingsLogic == 2)
						{
							this.accRunSpeed = 6.25f;
						}
						if (this.wingsLogic == 4)
						{
							this.accRunSpeed = 6.5f;
						}
						if (this.wingsLogic == 5 || this.wingsLogic == 6 || this.wingsLogic == 13 || this.wingsLogic == 15)
						{
							this.accRunSpeed = 6.75f;
						}
						if (this.wingsLogic == 7 || this.wingsLogic == 8)
						{
							this.accRunSpeed = 7f;
						}
						if (this.wingsLogic == 9 || this.wingsLogic == 10 || this.wingsLogic == 11 || this.wingsLogic == 20 || this.wingsLogic == 21 || this.wingsLogic == 23 || this.wingsLogic == 24)
						{
							this.accRunSpeed = 7.5f;
						}
						if (this.wingsLogic == 22)
						{
							if (this.controlDown && this.controlJump && this.wingTime > 0f)
							{
								this.accRunSpeed = 10f;
								this.runAcceleration *= 10f;
							}
							else
							{
								this.accRunSpeed = 6.25f;
							}
						}
						if (this.wingsLogic == 26)
						{
							this.accRunSpeed = 8f;
							this.runAcceleration *= 2f;
						}
						if (this.wingsLogic == 12)
						{
							this.accRunSpeed = 7.75f;
						}
						if (this.wingsLogic == 16 || this.wingsLogic == 17 || this.wingsLogic == 18 || this.wingsLogic == 19)
						{
							this.accRunSpeed = 7.9f;
						}
						if (this.wingsLogic == 3)
						{
							this.accRunSpeed = 11f;
							this.runAcceleration *= 3f;
						}
					}
					if (Main.myPlayer == this.whoAmi && (this.wings == 3 || this.wings == 16 || this.wings == 17 || this.wings == 18 || this.wings == 19))
					{
						this.accRunSpeed = 0f;
						this.maxRunSpeed *= 0.2f;
						this.runAcceleration *= 0.2f;
					}
					if (this.sticky)
					{
						this.maxRunSpeed *= 0.25f;
						this.runAcceleration *= 0.25f;
						this.runSlowdown *= 2f;
						if (this.velocity.X > this.maxRunSpeed)
						{
							this.velocity.X = this.maxRunSpeed;
						}
						if (this.velocity.X < -this.maxRunSpeed)
						{
							this.velocity.X = -this.maxRunSpeed;
						}
					}
					else
					{
						if (this.powerrun)
						{
							this.maxRunSpeed *= 3.5f;
							this.runAcceleration *= 1f;
							this.runSlowdown *= 2f;
						}
						else
						{
							if (this.slippy2)
							{
								this.runAcceleration *= 0.6f;
								this.runSlowdown = 0f;
								if (this.iceSkate)
								{
									this.runAcceleration *= 3.5f;
									this.maxRunSpeed *= 1.25f;
								}
							}
							else
							{
								if (this.slippy)
								{
									this.runAcceleration *= 0.7f;
									if (this.iceSkate)
									{
										this.runAcceleration *= 3.5f;
										this.maxRunSpeed *= 1.25f;
									}
									else
									{
										this.runSlowdown *= 0.1f;
									}
								}
							}
						}
					}
					if (this.sandStorm)
					{
						this.runAcceleration *= 1.5f;
						this.maxRunSpeed *= 2f;
					}
					if (this.dJumpEffect3 && this.doubleJump3)
					{
						this.runAcceleration *= 3f;
						this.maxRunSpeed *= 1.5f;
					}
					if (this.dJumpEffect4 && this.doubleJump4)
					{
						this.runAcceleration *= 3f;
						this.maxRunSpeed *= 1.75f;
					}
					if (this.carpetFrame != -1)
					{
						this.runAcceleration *= 1.25f;
						this.maxRunSpeed *= 1.5f;
					}
					if (this.mount.Active)
					{
						this.rocketBoots = 0;
						this.wings = 0;
						this.wingsLogic = 0;
						this.maxRunSpeed = this.mount.RunSpeed;
						this.accRunSpeed = this.mount.DashSpeed;
						this.runAcceleration = this.mount.Acceleration;
						if (this.mount.Type == 6 && this.velocity.Y == 0f)
						{
							if (!Minecart.OnTrack(this.position, this.width, this.height))
							{
								this.fullRotation = 0f;
								this.onWrongGround = true;
								this.runSlowdown = 0.2f;
								if ((this.controlLeft && this.releaseLeft) || (this.controlRight && this.releaseRight))
								{
									this.mount.Dismount(this);
								}
							}
							else
							{
								this.runSlowdown = this.runAcceleration;
								this.onWrongGround = false;
							}
						}
					}
					this.HorizontalMovement();
					if (this.gravControl)
					{
						if (this.controlUp && this.releaseUp)
						{
							if (this.gravDir == 1f)
							{
								this.gravDir = -1f;
								this.fallStart = (int)(this.position.Y / 16f);
								this.jump = 0;
								Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 8);
							}
							else
							{
								this.gravDir = 1f;
								this.fallStart = (int)(this.position.Y / 16f);
								this.jump = 0;
								Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 8);
							}
						}
					}
					else
					{
						if (this.gravControl2)
						{
							if (this.controlUp && this.releaseUp && this.velocity.Y == 0f)
							{
								if (this.gravDir == 1f)
								{
									this.gravDir = -1f;
									this.fallStart = (int)(this.position.Y / 16f);
									this.jump = 0;
									Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 8);
								}
								else
								{
									this.gravDir = 1f;
									this.fallStart = (int)(this.position.Y / 16f);
									this.jump = 0;
									Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 8);
								}
							}
						}
						else
						{
							this.gravDir = 1f;
						}
					}
					if (this.velocity.Y == 0f && this.mount.Active && this.mount.Type == 5 && this.controlUp && this.releaseUp)
					{
						this.velocity.Y = -(this.mount.Acceleration + this.gravity + 0.001f);
					}
					if (this.controlUp)
					{
						this.releaseUp = false;
					}
					else
					{
						this.releaseUp = true;
					}
					this.sandStorm = false;
					this.JumpMovement();
					if (this.wingsLogic == 0)
					{
						this.wingTime = 0f;
					}
					if (this.wingsLogic == 3)
					{
						this.wingTime = 1000f;
					}
					if (Main.myPlayer == this.whoAmi && (this.wings == 3 || this.wings == 16 || this.wings == 17 || this.wings == 18 || this.wings == 19))
					{
						this.wingTime = 0f;
						this.jump = 0;
					}
					if (this.rocketBoots == 0)
					{
						this.rocketTime = 0;
					}
					if (this.jump == 0)
					{
						this.dJumpEffect = false;
						this.dJumpEffect2 = false;
						this.dJumpEffect3 = false;
						this.dJumpEffect4 = false;
					}
					this.DashMovement();
					this.WallslideMovement();
					this.CarpetMovement();
					this.DoubleJumpVisuals();
					if (this.wings > 0 || this.mount.Active)
					{
						this.sandStorm = false;
					}
					if (((this.gravDir == 1f && this.velocity.Y > -Player.jumpSpeed) || (this.gravDir == -1f && this.velocity.Y < Player.jumpSpeed)) && this.velocity.Y != 0f)
					{
						this.canRocket = true;
					}
					bool flag14 = false;
					if (((this.velocity.Y == 0f || this.sliding) && this.releaseJump) || (this.autoJump && this.justJumped))
					{
						this.mount.ResetFlightTime(this.velocity.X);
						this.wingTime = (float)this.wingTimeMax;
					}
					if (this.wingsLogic > 0 && this.controlJump && this.wingTime > 0f && !this.jumpAgain && this.jump == 0 && this.velocity.Y != 0f)
					{
						flag14 = true;
					}
					if (this.wingsLogic == 22 && this.controlJump && this.controlDown && this.wingTime > 0f)
					{
						flag14 = true;
					}
					if (this.frozen)
					{
						if (this.mount.Active)
						{
							this.mount.Dismount(this);
						}
						this.velocity.Y = this.velocity.Y + this.gravity;
						if (this.velocity.Y > this.maxFallSpeed)
						{
							this.velocity.Y = this.maxFallSpeed;
						}
						this.sandStorm = false;
						this.dJumpEffect = false;
						this.dJumpEffect2 = false;
						this.dJumpEffect3 = false;
					}
					else
					{
						if (flag14)
						{
							if (this.wings == 10 && Main.rand.Next(2) == 0)
							{
								int num33 = 4;
								if (this.direction == 1)
								{
									num33 = -40;
								}
								int num34 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num33, this.position.Y + (float)(this.height / 2) - 15f), 30, 30, 76, 0f, 0f, 50, default(Color), 0.6f);
								Main.dust[num34].fadeIn = 1.1f;
								Main.dust[num34].noGravity = true;
								Main.dust[num34].noLight = true;
								Main.dust[num34].velocity *= 0.3f;
							}
							if (this.wings == 9 && Main.rand.Next(2) == 0)
							{
								int num35 = 4;
								if (this.direction == 1)
								{
									num35 = -40;
								}
								int num36 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num35, this.position.Y + (float)(this.height / 2) - 15f), 30, 30, 6, 0f, 0f, 200, default(Color), 2f);
								Main.dust[num36].noGravity = true;
								Main.dust[num36].velocity *= 0.3f;
							}
							if (this.wings == 6 && Main.rand.Next(4) == 0)
							{
								int num37 = 4;
								if (this.direction == 1)
								{
									num37 = -40;
								}
								int num38 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num37, this.position.Y + (float)(this.height / 2) - 15f), 30, 30, 55, 0f, 0f, 200, default(Color), 1f);
								Main.dust[num38].velocity *= 0.3f;
							}
							if (this.wings == 5 && Main.rand.Next(3) == 0)
							{
								int num39 = 6;
								if (this.direction == 1)
								{
									num39 = -30;
								}
								int num40 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num39, this.position.Y), 18, this.height, 58, 0f, 0f, 255, default(Color), 1.2f);
								Main.dust[num40].velocity *= 0.3f;
							}
							if (this.wings == 26)
							{
								int num41 = 6;
								if (this.direction == 1)
								{
									num41 = -30;
								}
								int num42 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num41, this.position.Y), 18, this.height, 217, 0f, 0f, 100, default(Color), 1.4f);
								Main.dust[num42].noGravity = true;
								Main.dust[num42].noLight = true;
								Main.dust[num42].velocity /= 4f;
								Main.dust[num42].velocity -= this.velocity;
								if (Main.rand.Next(2) == 0)
								{
									num41 = -24;
									if (this.direction == 1)
									{
										num41 = 12;
									}
									float num43 = this.position.Y;
									if (this.gravDir == -1f)
									{
										num43 += (float)(this.height / 2);
									}
									num42 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num41, num43), 12, this.height / 2, 217, 0f, 0f, 100, default(Color), 1.4f);
									Main.dust[num42].noGravity = true;
									Main.dust[num42].noLight = true;
									Main.dust[num42].velocity /= 4f;
									Main.dust[num42].velocity -= this.velocity;
								}
							}
							this.WingMovement();
						}
						if (this.wings == 4)
						{
							if (flag14 || this.jump > 0)
							{
								this.rocketDelay2--;
								if (this.rocketDelay2 <= 0)
								{
									Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 13);
									this.rocketDelay2 = 60;
								}
								int num44 = 2;
								if (this.controlUp)
								{
									num44 = 4;
								}
								for (int num45 = 0; num45 < num44; num45++)
								{
									int type = 6;
									if (this.head == 41)
									{
										int arg_3E2A_0 = this.body;
									}
									float scale = 1.75f;
									int alpha = 100;
									float x = this.position.X + (float)(this.width / 2) + 16f;
									if (this.direction > 0)
									{
										x = this.position.X + (float)(this.width / 2) - 26f;
									}
									float num46 = this.position.Y + (float)this.height - 18f;
									if (num45 == 1 || num45 == 3)
									{
										x = this.position.X + (float)(this.width / 2) + 8f;
										if (this.direction > 0)
										{
											x = this.position.X + (float)(this.width / 2) - 20f;
										}
										num46 += 6f;
									}
									if (num45 > 1)
									{
										num46 += this.velocity.Y;
									}
									int num47 = Dust.NewDust(new Vector2(x, num46), 8, 8, type, 0f, 0f, alpha, default(Color), scale);
									Dust expr_3F3D_cp_0 = Main.dust[num47];
									expr_3F3D_cp_0.velocity.X = expr_3F3D_cp_0.velocity.X * 0.1f;
									Main.dust[num47].velocity.Y = Main.dust[num47].velocity.Y * 1f + 2f * this.gravDir - this.velocity.Y * 0.3f;
									Main.dust[num47].noGravity = true;
									if (num44 == 4)
									{
										Dust expr_3FB7_cp_0 = Main.dust[num47];
										expr_3FB7_cp_0.velocity.Y = expr_3FB7_cp_0.velocity.Y + 6f;
									}
								}
								this.wingFrameCounter++;
								if (this.wingFrameCounter > 4)
								{
									this.wingFrame++;
									this.wingFrameCounter = 0;
									if (this.wingFrame >= 3)
									{
										this.wingFrame = 0;
									}
								}
							}
							else
							{
								if (!this.controlJump || this.velocity.Y == 0f)
								{
									this.wingFrame = 3;
								}
							}
						}
						else
						{
							if (this.wings == 22)
							{
								if (!this.controlJump)
								{
									this.wingFrame = 0;
									this.wingFrameCounter = 0;
								}
								else
								{
									if (this.wingTime > 0f)
									{
										if (this.controlDown)
										{
											if (this.velocity.X != 0f)
											{
												this.wingFrameCounter++;
												int num48 = 2;
												if (this.wingFrameCounter < num48)
												{
													this.wingFrame = 1;
												}
												else
												{
													if (this.wingFrameCounter < num48 * 2)
													{
														this.wingFrame = 2;
													}
													else
													{
														if (this.wingFrameCounter < num48 * 3)
														{
															this.wingFrame = 3;
														}
														else
														{
															if (this.wingFrameCounter < num48 * 4 - 1)
															{
																this.wingFrame = 2;
															}
															else
															{
																this.wingFrame = 2;
																this.wingFrameCounter = 0;
															}
														}
													}
												}
											}
											else
											{
												this.wingFrameCounter++;
												int num49 = 6;
												if (this.wingFrameCounter < num49)
												{
													this.wingFrame = 4;
												}
												else
												{
													if (this.wingFrameCounter < num49 * 2)
													{
														this.wingFrame = 5;
													}
													else
													{
														if (this.wingFrameCounter < num49 * 3 - 1)
														{
															this.wingFrame = 4;
														}
														else
														{
															this.wingFrame = 4;
															this.wingFrameCounter = 0;
														}
													}
												}
											}
										}
										else
										{
											this.wingFrameCounter++;
											int num50 = 2;
											if (this.wingFrameCounter < num50)
											{
												this.wingFrame = 4;
											}
											else
											{
												if (this.wingFrameCounter < num50 * 2)
												{
													this.wingFrame = 5;
												}
												else
												{
													if (this.wingFrameCounter < num50 * 3)
													{
														this.wingFrame = 6;
													}
													else
													{
														if (this.wingFrameCounter < num50 * 4 - 1)
														{
															this.wingFrame = 5;
														}
														else
														{
															this.wingFrame = 5;
															this.wingFrameCounter = 0;
														}
													}
												}
											}
										}
									}
									else
									{
										this.wingFrameCounter++;
										int num51 = 6;
										if (this.wingFrameCounter < num51)
										{
											this.wingFrame = 4;
										}
										else
										{
											if (this.wingFrameCounter < num51 * 2)
											{
												this.wingFrame = 5;
											}
											else
											{
												if (this.wingFrameCounter < num51 * 3 - 1)
												{
													this.wingFrame = 4;
												}
												else
												{
													this.wingFrame = 4;
													this.wingFrameCounter = 0;
												}
											}
										}
									}
								}
							}
							else
							{
								if (this.wings == 12)
								{
									if (flag14 || this.jump > 0)
									{
										this.wingFrameCounter++;
										int num52 = 5;
										if (this.wingFrameCounter < num52)
										{
											this.wingFrame = 1;
										}
										else
										{
											if (this.wingFrameCounter < num52 * 2)
											{
												this.wingFrame = 2;
											}
											else
											{
												if (this.wingFrameCounter < num52 * 3)
												{
													this.wingFrame = 3;
												}
												else
												{
													if (this.wingFrameCounter < num52 * 4 - 1)
													{
														this.wingFrame = 2;
													}
													else
													{
														this.wingFrame = 2;
														this.wingFrameCounter = 0;
													}
												}
											}
										}
									}
									else
									{
										if (this.velocity.Y != 0f)
										{
											this.wingFrame = 2;
										}
										else
										{
											this.wingFrame = 0;
										}
									}
								}
								else
								{
									if (this.wings == 24)
									{
										if (flag14 || this.jump > 0)
										{
											this.wingFrameCounter++;
											int num53 = 1;
											if (this.wingFrameCounter < num53)
											{
												this.wingFrame = 1;
											}
											else
											{
												if (this.wingFrameCounter < num53 * 2)
												{
													this.wingFrame = 2;
												}
												else
												{
													if (this.wingFrameCounter < num53 * 3)
													{
														this.wingFrame = 3;
													}
													else
													{
														this.wingFrame = 2;
														if (this.wingFrameCounter >= num53 * 4 - 1)
														{
															this.wingFrameCounter = 0;
														}
													}
												}
											}
										}
										else
										{
											if (this.velocity.Y != 0f)
											{
												if (this.controlJump)
												{
													this.wingFrameCounter++;
													int num54 = 3;
													if (this.wingFrameCounter < num54)
													{
														this.wingFrame = 1;
													}
													else
													{
														if (this.wingFrameCounter < num54 * 2)
														{
															this.wingFrame = 2;
														}
														else
														{
															if (this.wingFrameCounter < num54 * 3)
															{
																this.wingFrame = 3;
															}
															else
															{
																this.wingFrame = 2;
																if (this.wingFrameCounter >= num54 * 4 - 1)
																{
																	this.wingFrameCounter = 0;
																}
															}
														}
													}
												}
												else
												{
													if (this.wingTime == 0f)
													{
														this.wingFrame = 0;
													}
													else
													{
														this.wingFrame = 1;
													}
												}
											}
											else
											{
												this.wingFrame = 0;
											}
										}
									}
									else
									{
										if (flag14 || this.jump > 0)
										{
											this.wingFrameCounter++;
											if (this.wingFrameCounter > 4)
											{
												this.wingFrame++;
												this.wingFrameCounter = 0;
												if (this.wingFrame >= 4)
												{
													this.wingFrame = 0;
												}
											}
										}
										else
										{
											if (this.velocity.Y != 0f)
											{
												this.wingFrame = 1;
											}
											else
											{
												this.wingFrame = 0;
											}
										}
									}
								}
							}
						}
						if (this.wingsLogic > 0 && this.rocketBoots > 0 && this.velocity.Y != 0f)
						{
							this.wingTime += (float)(this.rocketTime * 6);
							this.rocketTime = 0;
						}
						if (flag14 && this.wings != 4 && this.wings != 22 && this.wings != 0 && this.wings != 24)
						{
							if (this.wingFrame == 3)
							{
								if (!this.flapSound)
								{
									Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 32);
								}
								this.flapSound = true;
							}
							else
							{
								this.flapSound = false;
							}
						}
						if (this.velocity.Y == 0f || this.sliding || (this.autoJump && this.justJumped))
						{
							this.rocketTime = this.rocketTimeMax;
						}
						if ((this.wingTime == 0f || this.wingsLogic == 0) && this.rocketBoots > 0 && this.controlJump && this.rocketDelay == 0 && this.canRocket && this.rocketRelease && !this.jumpAgain)
						{
							if (this.rocketTime > 0)
							{
								this.rocketTime--;
								this.rocketDelay = 10;
								if (this.rocketDelay2 <= 0)
								{
									if (this.rocketBoots == 1)
									{
										Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 13);
										this.rocketDelay2 = 30;
									}
									else
									{
										if (this.rocketBoots == 2 || this.rocketBoots == 3)
										{
											Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 24);
											this.rocketDelay2 = 15;
										}
									}
								}
							}
							else
							{
								this.canRocket = false;
							}
						}
						if (this.rocketDelay2 > 0)
						{
							this.rocketDelay2--;
						}
						if (this.rocketDelay == 0)
						{
							this.rocketFrame = false;
						}
						if (this.rocketDelay > 0)
						{
							int num55 = this.height;
							if (this.gravDir == -1f)
							{
								num55 = 4;
							}
							this.rocketFrame = true;
							for (int num56 = 0; num56 < 2; num56++)
							{
								int type2 = 6;
								float scale2 = 2.5f;
								int alpha2 = 100;
								if (this.rocketBoots == 2)
								{
									type2 = 16;
									scale2 = 1.5f;
									alpha2 = 20;
								}
								else
								{
									if (this.rocketBoots == 3)
									{
										type2 = 76;
										scale2 = 1f;
										alpha2 = 20;
									}
									else
									{
										if (this.socialShadow)
										{
											type2 = 27;
											scale2 = 1.5f;
										}
									}
								}
								if (num56 == 0)
								{
									int num57 = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float)num55 - 10f), 8, 8, type2, 0f, 0f, alpha2, default(Color), scale2);
									if (this.rocketBoots == 1)
									{
										Main.dust[num57].noGravity = true;
									}
									Main.dust[num57].velocity.X = Main.dust[num57].velocity.X * 1f - 2f - this.velocity.X * 0.3f;
									Main.dust[num57].velocity.Y = Main.dust[num57].velocity.Y * 1f + 2f * this.gravDir - this.velocity.Y * 0.3f;
									if (this.rocketBoots == 2)
									{
										Main.dust[num57].velocity *= 0.1f;
									}
									if (this.rocketBoots == 3)
									{
										Main.dust[num57].velocity *= 0.05f;
										Dust expr_48EE_cp_0 = Main.dust[num57];
										expr_48EE_cp_0.velocity.Y = expr_48EE_cp_0.velocity.Y + 0.15f;
										Main.dust[num57].noLight = true;
										if (Main.rand.Next(2) == 0)
										{
											Main.dust[num57].noGravity = true;
											Main.dust[num57].scale = 1.75f;
										}
									}
								}
								else
								{
									int num58 = Dust.NewDust(new Vector2(this.position.X + (float)this.width - 4f, this.position.Y + (float)num55 - 10f), 8, 8, type2, 0f, 0f, alpha2, default(Color), scale2);
									if (this.rocketBoots == 1)
									{
										Main.dust[num58].noGravity = true;
									}
									Main.dust[num58].velocity.X = Main.dust[num58].velocity.X * 1f + 2f - this.velocity.X * 0.3f;
									Main.dust[num58].velocity.Y = Main.dust[num58].velocity.Y * 1f + 2f * this.gravDir - this.velocity.Y * 0.3f;
									if (this.rocketBoots == 2)
									{
										Main.dust[num58].velocity *= 0.1f;
									}
									if (this.rocketBoots == 3)
									{
										Main.dust[num58].velocity *= 0.05f;
										Dust expr_4A97_cp_0 = Main.dust[num58];
										expr_4A97_cp_0.velocity.Y = expr_4A97_cp_0.velocity.Y + 0.15f;
										Main.dust[num58].noLight = true;
										if (Main.rand.Next(2) == 0)
										{
											Main.dust[num58].noGravity = true;
											Main.dust[num58].scale = 1.75f;
										}
									}
								}
							}
							if (this.rocketDelay == 0)
							{
								this.releaseJump = true;
							}
							this.rocketDelay--;
							this.velocity.Y = this.velocity.Y - 0.1f * this.gravDir;
							if (this.gravDir == 1f)
							{
								if (this.velocity.Y > 0f)
								{
									this.velocity.Y = this.velocity.Y - 0.5f;
								}
								else
								{
									if ((double)this.velocity.Y > (double)(-(double)Player.jumpSpeed) * 0.5)
									{
										this.velocity.Y = this.velocity.Y - 0.1f;
									}
								}
								if (this.velocity.Y < -Player.jumpSpeed * 1.5f)
								{
									this.velocity.Y = -Player.jumpSpeed * 1.5f;
								}
							}
							else
							{
								if (this.velocity.Y < 0f)
								{
									this.velocity.Y = this.velocity.Y + 0.5f;
								}
								else
								{
									if ((double)this.velocity.Y < (double)Player.jumpSpeed * 0.5)
									{
										this.velocity.Y = this.velocity.Y + 0.1f;
									}
								}
								if (this.velocity.Y > Player.jumpSpeed * 1.5f)
								{
									this.velocity.Y = Player.jumpSpeed * 1.5f;
								}
							}
						}
						else
						{
							if (!flag14)
							{
								if (this.mount.Type == 5)
								{
									this.mount.Hover(this);
								}
								else
								{
									if (this.mount.CanFly && this.controlJump && this.jump == 0)
									{
										if (this.mount.Flight())
										{
											if (this.controlDown)
											{
												this.velocity.Y = this.velocity.Y * 0.9f;
												if (this.velocity.Y > -1f && (double)this.velocity.Y < 0.5)
												{
													this.velocity.Y = 1E-05f;
												}
											}
											else
											{
												if (this.velocity.Y > 0f)
												{
													this.velocity.Y = this.velocity.Y - 0.5f;
												}
												else
												{
													if ((double)this.velocity.Y > (double)(-(double)Player.jumpSpeed) * 1.5)
													{
														this.velocity.Y = this.velocity.Y - 0.1f;
													}
												}
												if (this.velocity.Y < -Player.jumpSpeed * 1.5f)
												{
													this.velocity.Y = -Player.jumpSpeed * 1.5f;
												}
											}
										}
										else
										{
											this.velocity.Y = this.velocity.Y + this.gravity / 3f * this.gravDir;
											if (this.gravDir == 1f)
											{
												if (this.velocity.Y > this.maxFallSpeed / 3f && !this.controlDown)
												{
													this.velocity.Y = this.maxFallSpeed / 3f;
												}
											}
											else
											{
												if (this.velocity.Y < -this.maxFallSpeed / 3f && !this.controlUp)
												{
													this.velocity.Y = -this.maxFallSpeed / 3f;
												}
											}
										}
									}
									else
									{
										if (this.slowFall && ((!this.controlDown && this.gravDir == 1f) || (!this.controlDown && this.gravDir == -1f)))
										{
											if ((this.controlUp && this.gravDir == 1f) || (this.controlUp && this.gravDir == -1f))
											{
												this.gravity = this.gravity / 10f * this.gravDir;
											}
											else
											{
												this.gravity = this.gravity / 3f * this.gravDir;
											}
											this.velocity.Y = this.velocity.Y + this.gravity;
										}
										else
										{
											if (this.wingsLogic > 0 && this.controlJump && this.velocity.Y > 0f)
											{
												this.fallStart = (int)(this.position.Y / 16f);
												if (this.velocity.Y > 0f)
												{
													if (this.wings == 10 && Main.rand.Next(3) == 0)
													{
														int num59 = 4;
														if (this.direction == 1)
														{
															num59 = -40;
														}
														int num60 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num59, this.position.Y + (float)(this.height / 2) - 15f), 30, 30, 76, 0f, 0f, 50, default(Color), 0.6f);
														Main.dust[num60].fadeIn = 1.1f;
														Main.dust[num60].noGravity = true;
														Main.dust[num60].noLight = true;
														Main.dust[num60].velocity *= 0.3f;
													}
													if (this.wings == 9 && Main.rand.Next(3) == 0)
													{
														int num61 = 8;
														if (this.direction == 1)
														{
															num61 = -40;
														}
														int num62 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num61, this.position.Y + (float)(this.height / 2) - 15f), 30, 30, 6, 0f, 0f, 200, default(Color), 2f);
														Main.dust[num62].noGravity = true;
														Main.dust[num62].velocity *= 0.3f;
													}
													if (this.wings == 6)
													{
														if (Main.rand.Next(10) == 0)
														{
															int num63 = 4;
															if (this.direction == 1)
															{
																num63 = -40;
															}
															int num64 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num63, this.position.Y + (float)(this.height / 2) - 12f), 30, 20, 55, 0f, 0f, 200, default(Color), 1f);
															Main.dust[num64].velocity *= 0.3f;
														}
													}
													else
													{
														if (this.wings == 5 && Main.rand.Next(6) == 0)
														{
															int num65 = 6;
															if (this.direction == 1)
															{
																num65 = -30;
															}
															int num66 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num65, this.position.Y), 18, this.height, 58, 0f, 0f, 255, default(Color), 1.2f);
															Main.dust[num66].velocity *= 0.3f;
														}
													}
													if (this.wings == 4)
													{
														this.rocketDelay2--;
														if (this.rocketDelay2 <= 0)
														{
															Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 13);
															this.rocketDelay2 = 60;
														}
														int type3 = 6;
														float scale3 = 1.5f;
														int alpha3 = 100;
														float x2 = this.position.X + (float)(this.width / 2) + 16f;
														if (this.direction > 0)
														{
															x2 = this.position.X + (float)(this.width / 2) - 26f;
														}
														float num67 = this.position.Y + (float)this.height - 18f;
														if (Main.rand.Next(2) == 1)
														{
															x2 = this.position.X + (float)(this.width / 2) + 8f;
															if (this.direction > 0)
															{
																x2 = this.position.X + (float)(this.width / 2) - 20f;
															}
															num67 += 6f;
														}
														int num68 = Dust.NewDust(new Vector2(x2, num67), 8, 8, type3, 0f, 0f, alpha3, default(Color), scale3);
														Dust expr_53D1_cp_0 = Main.dust[num68];
														expr_53D1_cp_0.velocity.X = expr_53D1_cp_0.velocity.X * 0.3f;
														Dust expr_53EF_cp_0 = Main.dust[num68];
														expr_53EF_cp_0.velocity.Y = expr_53EF_cp_0.velocity.Y + 10f;
														Main.dust[num68].noGravity = true;
														this.wingFrameCounter++;
														if (this.wingFrameCounter > 4)
														{
															this.wingFrame++;
															this.wingFrameCounter = 0;
															if (this.wingFrame >= 3)
															{
																this.wingFrame = 0;
															}
														}
													}
													else
													{
														if (this.wings != 22)
														{
															if (this.wings == 26)
															{
																int num69 = 6;
																if (this.direction == 1)
																{
																	num69 = -30;
																}
																int num70 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num69, this.position.Y), 18, this.height, 217, 0f, 0f, 100, default(Color), 1.4f);
																Main.dust[num70].noGravity = true;
																Main.dust[num70].noLight = true;
																Main.dust[num70].velocity /= 4f;
																Main.dust[num70].velocity -= this.velocity;
																if (Main.rand.Next(2) == 0)
																{
																	num69 = -24;
																	if (this.direction == 1)
																	{
																		num69 = 12;
																	}
																	float num71 = this.position.Y;
																	if (this.gravDir == -1f)
																	{
																		num71 += (float)(this.height / 2);
																	}
																	num70 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2) + (float)num69, num71), 12, this.height / 2, 217, 0f, 0f, 100, default(Color), 1.4f);
																	Main.dust[num70].noGravity = true;
																	Main.dust[num70].noLight = true;
																	Main.dust[num70].velocity /= 4f;
																	Main.dust[num70].velocity -= this.velocity;
																}
																this.wingFrame = 2;
															}
															else
															{
																if (this.wings != 24)
																{
																	if (this.wings == 12)
																	{
																		this.wingFrame = 3;
																	}
																	else
																	{
																		this.wingFrame = 2;
																	}
																}
															}
														}
													}
												}
												this.velocity.Y = this.velocity.Y + this.gravity / 3f * this.gravDir;
												if (this.gravDir == 1f)
												{
													if (this.velocity.Y > this.maxFallSpeed / 3f && !this.controlDown)
													{
														this.velocity.Y = this.maxFallSpeed / 3f;
													}
												}
												else
												{
													if (this.velocity.Y < -this.maxFallSpeed / 3f && !this.controlUp)
													{
														this.velocity.Y = -this.maxFallSpeed / 3f;
													}
												}
											}
											else
											{
												if (this.cartRampTime <= 0)
												{
													this.velocity.Y = this.velocity.Y + this.gravity * this.gravDir;
												}
												else
												{
													this.cartRampTime--;
												}
											}
										}
									}
								}
							}
						}
						if (!this.mount.Active || this.mount.Type != 5)
						{
							if (this.gravDir == 1f)
							{
								if (this.velocity.Y > this.maxFallSpeed)
								{
									this.velocity.Y = this.maxFallSpeed;
								}
								if (this.slowFall && this.velocity.Y > this.maxFallSpeed / 3f && !this.controlDown)
								{
									this.velocity.Y = this.maxFallSpeed / 3f;
								}
								if (this.slowFall && this.velocity.Y > this.maxFallSpeed / 5f && this.controlUp)
								{
									this.velocity.Y = this.maxFallSpeed / 10f;
								}
							}
							else
							{
								if (this.velocity.Y < -this.maxFallSpeed)
								{
									this.velocity.Y = -this.maxFallSpeed;
								}
								if (this.slowFall && this.velocity.Y < -this.maxFallSpeed / 3f && !this.controlDown)
								{
									this.velocity.Y = -this.maxFallSpeed / 3f;
								}
								if (this.slowFall && this.velocity.Y < -this.maxFallSpeed / 5f && this.controlUp)
								{
									this.velocity.Y = -this.maxFallSpeed / 10f;
								}
							}
						}
					}
				}
			}
			if (this.mount.Active)
			{
				this.wingFrame = 0;
			}
			if (this.wingsLogic == 3)
			{
				if (this.controlUp && this.controlDown)
				{
					this.velocity.Y = 0f;
				}
				else
				{
					if (this.controlDown && this.controlJump)
					{
						this.velocity.Y = this.velocity.Y * 0.9f;
						if (this.velocity.Y > -2f && this.velocity.Y < 1f)
						{
							this.velocity.Y = 1E-05f;
						}
					}
					else
					{
						if (this.controlDown && this.velocity.Y != 0f)
						{
							this.velocity.Y = this.velocity.Y + 0.2f;
						}
					}
				}
			}
			if (this.wingsLogic == 22 && this.controlDown && this.controlJump && this.wingTime > 0f && !this.merman)
			{
				this.velocity.Y = this.velocity.Y * 0.9f;
				if (this.velocity.Y > -2f && this.velocity.Y < 1f)
				{
					this.velocity.Y = 1E-05f;
				}
			}
			for (int num72 = 0; num72 < 400; num72++)
			{
				if (Main.item[num72].active && Main.item[num72].noGrabDelay == 0 && Main.item[num72].owner == i)
				{
					int num73 = Player.defaultItemGrabRange;
					if (this.manaMagnet && (Main.item[num72].type == 184 || Main.item[num72].type == 1735 || Main.item[num72].type == 1868))
					{
						num73 += Item.manaGrabRange;
					}
					if (this.lifeMagnet && (Main.item[num72].type == 58 || Main.item[num72].type == 1734 || Main.item[num72].type == 1867))
					{
						num73 += Item.lifeGrabRange;
					}
					if (new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height).Intersects(new Rectangle((int)Main.item[num72].position.X, (int)Main.item[num72].position.Y, Main.item[num72].width, Main.item[num72].height)))
					{
						if (i == Main.myPlayer && (this.inventory[this.selectedItem].type != 0 || this.itemAnimation <= 0))
						{
							if (Main.item[num72].type == 58 || Main.item[num72].type == 1734 || Main.item[num72].type == 1867)
							{
								Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
								this.statLife += 20;
								if (Main.myPlayer == this.whoAmi)
								{
									this.HealEffect(20, true);
								}
								if (this.statLife > this.statLifeMax2)
								{
									this.statLife = this.statLifeMax2;
								}
								Main.item[num72] = new Item();
								if (Main.netMode == 1)
								{
									NetMessage.SendData(21, -1, -1, "", num72, 0f, 0f, 0f, 0);
								}
							}
							else
							{
								if (Main.item[num72].type == 184 || Main.item[num72].type == 1735 || Main.item[num72].type == 1868)
								{
									Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
									this.statMana += 100;
									if (Main.myPlayer == this.whoAmi)
									{
										this.ManaEffect(100);
									}
									if (this.statMana > this.statManaMax2)
									{
										this.statMana = this.statManaMax2;
									}
									Main.item[num72] = new Item();
									if (Main.netMode == 1)
									{
										NetMessage.SendData(21, -1, -1, "", num72, 0f, 0f, 0f, 0);
									}
								}
								else
								{
									Main.item[num72] = this.GetItem(i, Main.item[num72], false);
									if (Main.netMode == 1)
									{
										NetMessage.SendData(21, -1, -1, "", num72, 0f, 0f, 0f, 0);
									}
								}
							}
						}
					}
					else
					{
						if (new Rectangle((int)this.position.X - num73, (int)this.position.Y - num73, this.width + num73 * 2, this.height + num73 * 2).Intersects(new Rectangle((int)Main.item[num72].position.X, (int)Main.item[num72].position.Y, Main.item[num72].width, Main.item[num72].height)) && this.ItemSpace(Main.item[num72]))
						{
							Main.item[num72].beingGrabbed = true;
							if (this.manaMagnet && (Main.item[num72].type == 184 || Main.item[num72].type == 1735 || Main.item[num72].type == 1868))
							{
								float num74 = 12f;
								Vector2 vector = new Vector2(Main.item[num72].position.X + (float)(Main.item[num72].width / 2), Main.item[num72].position.Y + (float)(Main.item[num72].height / 2));
								float num75 = this.center().X - vector.X;
								float num76 = this.center().Y - vector.Y;
								float num77 = (float)Math.Sqrt((double)(num75 * num75 + num76 * num76));
								num77 = num74 / num77;
								num75 *= num77;
								num76 *= num77;
								int num78 = 5;
								Main.item[num72].velocity.X = (Main.item[num72].velocity.X * (float)(num78 - 1) + num75) / (float)num78;
								Main.item[num72].velocity.Y = (Main.item[num72].velocity.Y * (float)(num78 - 1) + num76) / (float)num78;
							}
							else
							{
								if (this.lifeMagnet && (Main.item[num72].type == 58 || Main.item[num72].type == 1734 || Main.item[num72].type == 1867))
								{
									float num79 = 15f;
									Vector2 vector2 = new Vector2(Main.item[num72].position.X + (float)(Main.item[num72].width / 2), Main.item[num72].position.Y + (float)(Main.item[num72].height / 2));
									float num80 = this.center().X - vector2.X;
									float num81 = this.center().Y - vector2.Y;
									float num82 = (float)Math.Sqrt((double)(num80 * num80 + num81 * num81));
									num82 = num79 / num82;
									num80 *= num82;
									num81 *= num82;
									int num83 = 5;
									Main.item[num72].velocity.X = (Main.item[num72].velocity.X * (float)(num83 - 1) + num80) / (float)num83;
									Main.item[num72].velocity.Y = (Main.item[num72].velocity.Y * (float)(num83 - 1) + num81) / (float)num83;
								}
								else
								{
									if ((double)this.position.X + (double)this.width * 0.5 > (double)Main.item[num72].position.X + (double)Main.item[num72].width * 0.5)
									{
										if (Main.item[num72].velocity.X < Player.itemGrabSpeedMax + this.velocity.X)
										{
											Item expr_6178_cp_0 = Main.item[num72];
											expr_6178_cp_0.velocity.X = expr_6178_cp_0.velocity.X + Player.itemGrabSpeed;
										}
										if (Main.item[num72].velocity.X < 0f)
										{
											Item expr_61B2_cp_0 = Main.item[num72];
											expr_61B2_cp_0.velocity.X = expr_61B2_cp_0.velocity.X + Player.itemGrabSpeed * 0.75f;
										}
									}
									else
									{
										if (Main.item[num72].velocity.X > -Player.itemGrabSpeedMax + this.velocity.X)
										{
											Item expr_6201_cp_0 = Main.item[num72];
											expr_6201_cp_0.velocity.X = expr_6201_cp_0.velocity.X - Player.itemGrabSpeed;
										}
										if (Main.item[num72].velocity.X > 0f)
										{
											Item expr_6238_cp_0 = Main.item[num72];
											expr_6238_cp_0.velocity.X = expr_6238_cp_0.velocity.X - Player.itemGrabSpeed * 0.75f;
										}
									}
									if ((double)this.position.Y + (double)this.height * 0.5 > (double)Main.item[num72].position.Y + (double)Main.item[num72].height * 0.5)
									{
										if (Main.item[num72].velocity.Y < Player.itemGrabSpeedMax)
										{
											Item expr_62C1_cp_0 = Main.item[num72];
											expr_62C1_cp_0.velocity.Y = expr_62C1_cp_0.velocity.Y + Player.itemGrabSpeed;
										}
										if (Main.item[num72].velocity.Y < 0f)
										{
											Item expr_62FB_cp_0 = Main.item[num72];
											expr_62FB_cp_0.velocity.Y = expr_62FB_cp_0.velocity.Y + Player.itemGrabSpeed * 0.75f;
										}
									}
									else
									{
										if (Main.item[num72].velocity.Y > -Player.itemGrabSpeedMax)
										{
											Item expr_633B_cp_0 = Main.item[num72];
											expr_633B_cp_0.velocity.Y = expr_633B_cp_0.velocity.Y - Player.itemGrabSpeed;
										}
										if (Main.item[num72].velocity.Y > 0f)
										{
											Item expr_6372_cp_0 = Main.item[num72];
											expr_6372_cp_0.velocity.Y = expr_6372_cp_0.velocity.Y - Player.itemGrabSpeed * 0.75f;
										}
									}
								}
							}
						}
					}
				}
			}
			if (!Main.mapFullscreen)
			{
				if (this.position.X / 16f - (float)Player.tileRangeX <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY - 2f >= (float)Player.tileTargetY)
				{
					if (Main.tile[Player.tileTargetX, Player.tileTargetY] == null)
					{
						Main.tile[Player.tileTargetX, Player.tileTargetY] = new Tile();
					}
					if (Main.tile[Player.tileTargetX, Player.tileTargetY].active())
					{
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 104)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							switch (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 36)
							{
							case 0:
								this.showItemIcon2 = 359;
								break;
							case 1:
								this.showItemIcon2 = 2237;
								break;
							case 2:
								this.showItemIcon2 = 2238;
								break;
							case 3:
								this.showItemIcon2 = 2239;
								break;
							case 4:
								this.showItemIcon2 = 2240;
								break;
							case 5:
								this.showItemIcon2 = 2241;
								break;
							case 6:
								this.showItemIcon2 = 2560;
								break;
							case 7:
								this.showItemIcon2 = 2575;
								break;
							case 8:
								this.showItemIcon2 = 2591;
								break;
							case 9:
								this.showItemIcon2 = 2592;
								break;
							case 10:
								this.showItemIcon2 = 2593;
								break;
							case 11:
								this.showItemIcon2 = 2594;
								break;
							case 12:
								this.showItemIcon2 = 2595;
								break;
							case 13:
								this.showItemIcon2 = 2596;
								break;
							case 14:
								this.showItemIcon2 = 2597;
								break;
							case 15:
								this.showItemIcon2 = 2598;
								break;
							case 16:
								this.showItemIcon2 = 2599;
								break;
							case 17:
								this.showItemIcon2 = 2600;
								break;
							case 18:
								this.showItemIcon2 = 2601;
								break;
							case 19:
								this.showItemIcon2 = 2602;
								break;
							case 20:
								this.showItemIcon2 = 2603;
								break;
							case 21:
								this.showItemIcon2 = 2604;
								break;
							case 22:
								this.showItemIcon2 = 2605;
								break;
							case 23:
								this.showItemIcon2 = 2606;
								break;
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							int num84 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 36);
							if (num84 == 0)
							{
								this.showItemIcon2 = 224;
							}
							else
							{
								if (num84 == 1)
								{
									this.showItemIcon2 = 644;
								}
								else
								{
									if (num84 == 2)
									{
										this.showItemIcon2 = 645;
									}
									else
									{
										if (num84 == 3)
										{
											this.showItemIcon2 = 646;
										}
										else
										{
											if (num84 == 4)
											{
												this.showItemIcon2 = 920;
											}
											else
											{
												if (num84 == 5)
												{
													this.showItemIcon2 = 1470;
												}
												else
												{
													if (num84 == 6)
													{
														this.showItemIcon2 = 1471;
													}
													else
													{
														if (num84 == 7)
														{
															this.showItemIcon2 = 1472;
														}
														else
														{
															if (num84 == 8)
															{
																this.showItemIcon2 = 1473;
															}
															else
															{
																if (num84 == 9)
																{
																	this.showItemIcon2 = 1719;
																}
																else
																{
																	if (num84 == 10)
																	{
																		this.showItemIcon2 = 1720;
																	}
																	else
																	{
																		if (num84 == 11)
																		{
																			this.showItemIcon2 = 1721;
																		}
																		else
																		{
																			if (num84 == 12)
																			{
																				this.showItemIcon2 = 1722;
																			}
																			else
																			{
																				if (num84 >= 13 && num84 <= 18)
																				{
																					this.showItemIcon2 = 2066 + num84 - 13;
																				}
																				else
																				{
																					if (num84 >= 19 && num84 <= 20)
																					{
																						this.showItemIcon2 = 2139 + num84 - 19;
																					}
																					else
																					{
																						if (num84 == 21)
																						{
																							this.showItemIcon2 = 2231;
																						}
																						else
																						{
																							if (num84 == 22)
																							{
																								this.showItemIcon2 = 2520;
																							}
																							else
																							{
																								if (num84 == 23)
																								{
																									this.showItemIcon2 = 2538;
																								}
																								else
																								{
																									if (num84 == 24)
																									{
																										this.showItemIcon2 = 2553;
																									}
																									else
																									{
																										if (num84 == 26)
																										{
																											this.showItemIcon2 = 2568;
																										}
																										else
																										{
																											if (num84 == 27)
																											{
																												this.showItemIcon2 = 2669;
																											}
																											else
																											{
																												this.showItemIcon2 = 646;
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 209)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX < 72)
							{
								this.showItemIcon2 = 928;
							}
							else
							{
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX < 144)
								{
									this.showItemIcon2 = 1337;
								}
							}
							int num85;
							for (num85 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18); num85 >= 4; num85 -= 4)
							{
							}
							if (num85 < 2)
							{
								this.showItemIconR = true;
							}
							else
							{
								this.showItemIconR = false;
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 216)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							int num86 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameY;
							int num87 = 0;
							while (num86 >= 40)
							{
								num86 -= 40;
								num87++;
							}
							this.showItemIcon2 = 970 + num87;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 335)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 2700;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 338)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 2738;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 219 && (this.inventory[this.selectedItem].type == 424 || this.inventory[this.selectedItem].type == 1103))
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = this.inventory[this.selectedItem].type;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 212)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 949;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 314 && this.gravDir == 1f)
						{
							this.showItemIcon = true;
							this.showItemIcon2 = 2343;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21)
						{
							Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
							int num88 = Player.tileTargetX;
							int num89 = Player.tileTargetY;
							if (tile.frameX % 36 != 0)
							{
								num88--;
							}
							if (tile.frameY % 36 != 0)
							{
								num89--;
							}
							int num90 = Chest.FindChest(num88, num89);
							this.showItemIcon2 = -1;
							if (num90 < 0)
							{
								this.showItemIconText = Lang.chestType[0];
							}
							else
							{
								if (Main.chest[num90].name != "")
								{
									this.showItemIconText = Main.chest[num90].name;
								}
								else
								{
									this.showItemIconText = Lang.chestType[(int)(tile.frameX / 36)];
								}
								if (this.showItemIconText == Lang.chestType[(int)(tile.frameX / 36)])
								{
									this.showItemIcon2 = Chest.typeToIcon[(int)(tile.frameX / 36)];
									this.showItemIconText = "";
								}
							}
							this.noThrow = 2;
							this.showItemIcon = true;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							int num91 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 22);
							if (num91 == 0)
							{
								this.showItemIcon2 = 8;
							}
							else
							{
								if (num91 == 8)
								{
									this.showItemIcon2 = 523;
								}
								else
								{
									if (num91 == 9)
									{
										this.showItemIcon2 = 974;
									}
									else
									{
										if (num91 == 10)
										{
											this.showItemIcon2 = 1245;
										}
										else
										{
											if (num91 == 11)
											{
												this.showItemIcon2 = 1333;
											}
											else
											{
												if (num91 == 12)
												{
													this.showItemIcon2 = 2274;
												}
												else
												{
													this.showItemIcon2 = 426 + num91;
												}
											}
										}
									}
								}
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							int num92 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
							if (num92 == 1)
							{
								this.showItemIcon2 = 28;
							}
							else
							{
								if (num92 == 2)
								{
									this.showItemIcon2 = 110;
								}
								else
								{
									if (num92 == 3)
									{
										this.showItemIcon2 = 350;
									}
									else
									{
										if (num92 == 4)
										{
											this.showItemIcon2 = 351;
										}
										else
										{
											if (num92 == 5)
											{
												this.showItemIcon2 = 2234;
											}
											else
											{
												if (num92 == 6)
												{
													this.showItemIcon2 = 2244;
												}
												else
												{
													if (num92 == 7)
													{
														this.showItemIcon2 = 2257;
													}
													else
													{
														if (num92 == 8)
														{
															this.showItemIcon2 = 2258;
														}
														else
														{
															this.showItemIcon2 = 31;
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 87;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 97)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 346;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 105;
							int num93 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 22);
							if (num93 == 1)
							{
								this.showItemIcon2 = 1405;
							}
							if (num93 == 2)
							{
								this.showItemIcon2 = 1406;
							}
							if (num93 == 3)
							{
								this.showItemIcon2 = 1407;
							}
							if (num93 >= 4 && num93 <= 13)
							{
								this.showItemIcon2 = 2045 + num93 - 4;
							}
							if (num93 >= 14 && num93 <= 16)
							{
								this.showItemIcon2 = 2153 + num93 - 14;
							}
							if (num93 == 17)
							{
								this.showItemIcon2 = 2236;
							}
							if (num93 == 18)
							{
								this.showItemIcon2 = 2523;
							}
							if (num93 == 19)
							{
								this.showItemIcon2 = 2542;
							}
							if (num93 == 20)
							{
								this.showItemIcon2 = 2556;
							}
							if (num93 == 21)
							{
								this.showItemIcon2 = 2571;
							}
							if (num93 == 22)
							{
								this.showItemIcon2 = 2648;
							}
							if (num93 == 23)
							{
								this.showItemIcon2 = 2649;
							}
							if (num93 == 24)
							{
								this.showItemIcon2 = 2650;
							}
							if (num93 == 25)
							{
								this.showItemIcon2 = 2651;
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 148;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 174)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 713;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50)
						{
							this.noThrow = 2;
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 90)
							{
								this.showItemIcon = true;
								this.showItemIcon2 = 165;
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 139)
						{
							this.noThrow = 2;
							int num94 = Player.tileTargetX;
							int num95 = Player.tileTargetY;
							int num96 = 0;
							for (int num97 = (int)(Main.tile[num94, num95].frameY / 18); num97 >= 2; num97 -= 2)
							{
								num96++;
							}
							this.showItemIcon = true;
							if (num96 == 28)
							{
								this.showItemIcon2 = 1963;
							}
							else
							{
								if (num96 == 29)
								{
									this.showItemIcon2 = 1964;
								}
								else
								{
									if (num96 == 30)
									{
										this.showItemIcon2 = 1965;
									}
									else
									{
										if (num96 == 31)
										{
											this.showItemIcon2 = 2742;
										}
										else
										{
											if (num96 >= 13)
											{
												this.showItemIcon2 = 1596 + num96 - 13;
											}
											else
											{
												this.showItemIcon2 = 562 + num96;
											}
										}
									}
								}
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 207)
						{
							this.noThrow = 2;
							int num98 = Player.tileTargetX;
							int num99 = Player.tileTargetY;
							int num100 = 0;
							for (int num101 = (int)(Main.tile[num98, num99].frameX / 18); num101 >= 2; num101 -= 2)
							{
								num100++;
							}
							this.showItemIcon = true;
							if (num100 == 0)
							{
								this.showItemIcon2 = 909;
							}
							else
							{
								if (num100 == 1)
								{
									this.showItemIcon2 = 910;
								}
								else
								{
									if (num100 == 2)
									{
										this.showItemIcon2 = 940;
									}
									else
									{
										if (num100 == 3)
										{
											this.showItemIcon2 = 941;
										}
										else
										{
											if (num100 == 4)
											{
												this.showItemIcon2 = 942;
											}
											else
											{
												if (num100 == 5)
												{
													this.showItemIcon2 = 943;
												}
												else
												{
													if (num100 == 6)
													{
														this.showItemIcon2 = 944;
													}
													else
													{
														if (num100 == 7)
														{
															this.showItemIcon2 = 945;
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 85)
						{
							this.noThrow = 2;
							int num102 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
							int num103 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
							while (num102 > 1)
							{
								num102 -= 2;
							}
							int num104 = Player.tileTargetX - num102;
							int num105 = Player.tileTargetY - num103;
							Main.signBubble = true;
							Main.signX = num104 * 16 + 16;
							Main.signY = num105 * 16;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 237)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 1293;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 88)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							int num106 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 54);
							if (num106 == 0)
							{
								this.showItemIcon2 = 334;
							}
							else
							{
								if (num106 == 1)
								{
									this.showItemIcon2 = 647;
								}
								else
								{
									if (num106 == 2)
									{
										this.showItemIcon2 = 648;
									}
									else
									{
										if (num106 == 3)
										{
											this.showItemIcon2 = 649;
										}
										else
										{
											if (num106 == 4)
											{
												this.showItemIcon2 = 918;
											}
											else
											{
												if (num106 >= 5 && num106 <= 15)
												{
													this.showItemIcon2 = 2386 + num106 - 5;
												}
												else
												{
													if (num106 == 16)
													{
														this.showItemIcon2 = 2529;
													}
													else
													{
														if (num106 == 17)
														{
															this.showItemIcon2 = 2545;
														}
														else
														{
															if (num106 == 18)
															{
																this.showItemIcon2 = 2562;
															}
															else
															{
																if (num106 == 19)
																{
																	this.showItemIcon2 = 2577;
																}
																else
																{
																	if (num106 == 20)
																	{
																		this.showItemIcon2 = 2637;
																	}
																	else
																	{
																		if (num106 == 21)
																		{
																			this.showItemIcon2 = 2638;
																		}
																		else
																		{
																			if (num106 == 22)
																			{
																				this.showItemIcon2 = 2639;
																			}
																			else
																			{
																				if (num106 == 23)
																				{
																					this.showItemIcon2 = 2640;
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							int num107 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameY;
							int num108 = 0;
							while (num107 >= 54)
							{
								num107 -= 54;
								num108++;
							}
							if (num108 == 0)
							{
								this.showItemIcon2 = 25;
							}
							else
							{
								if (num108 == 9)
								{
									this.showItemIcon2 = 837;
								}
								else
								{
									if (num108 == 10)
									{
										this.showItemIcon2 = 912;
									}
									else
									{
										if (num108 == 11)
										{
											this.showItemIcon2 = 1141;
										}
										else
										{
											if (num108 == 12)
											{
												this.showItemIcon2 = 1137;
											}
											else
											{
												if (num108 == 13)
												{
													this.showItemIcon2 = 1138;
												}
												else
												{
													if (num108 == 14)
													{
														this.showItemIcon2 = 1139;
													}
													else
													{
														if (num108 == 15)
														{
															this.showItemIcon2 = 1140;
														}
														else
														{
															if (num108 == 16)
															{
																this.showItemIcon2 = 1411;
															}
															else
															{
																if (num108 == 17)
																{
																	this.showItemIcon2 = 1412;
																}
																else
																{
																	if (num108 == 18)
																	{
																		this.showItemIcon2 = 1413;
																	}
																	else
																	{
																		if (num108 == 19)
																		{
																			this.showItemIcon2 = 1458;
																		}
																		else
																		{
																			if (num108 >= 20 && num108 <= 23)
																			{
																				this.showItemIcon2 = 1709 + num108 - 20;
																			}
																			else
																			{
																				if (num108 == 24)
																				{
																					this.showItemIcon2 = 1793;
																				}
																				else
																				{
																					if (num108 == 25)
																					{
																						this.showItemIcon2 = 1815;
																					}
																					else
																					{
																						if (num108 == 26)
																						{
																							this.showItemIcon2 = 1924;
																						}
																						else
																						{
																							if (num108 == 27)
																							{
																								this.showItemIcon2 = 2044;
																							}
																							else
																							{
																								if (num108 == 28)
																								{
																									this.showItemIcon2 = 2265;
																								}
																								else
																								{
																									if (num108 == 29)
																									{
																										this.showItemIcon2 = 2528;
																									}
																									else
																									{
																										if (num108 == 30)
																										{
																											this.showItemIcon2 = 2561;
																										}
																										else
																										{
																											if (num108 == 31)
																											{
																												this.showItemIcon2 = 2576;
																											}
																											else
																											{
																												if (num108 >= 4 && num108 <= 8)
																												{
																													this.showItemIcon2 = 812 + num108;
																												}
																												else
																												{
																													this.showItemIcon2 = 649 + num108;
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 125)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 487;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 287)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 2177;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 132)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 513;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 136)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = 538;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 144)
						{
							this.noThrow = 2;
							this.showItemIcon = true;
							this.showItemIcon2 = (int)(583 + Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
						}
						if (this.controlUseTile)
						{
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 212 && this.launcherWait <= 0)
							{
								int num109 = Player.tileTargetX;
								int num110 = Player.tileTargetY;
								bool flag15 = false;
								for (int num111 = 0; num111 < 58; num111++)
								{
									if (this.inventory[num111].type == 949 && this.inventory[num111].stack > 0)
									{
										this.inventory[num111].stack--;
										if (this.inventory[num111].stack <= 0)
										{
											this.inventory[num111].SetDefaults(0, false);
										}
										flag15 = true;
										break;
									}
								}
								if (flag15)
								{
									this.launcherWait = 10;
									Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 11);
									int num112 = (int)(Main.tile[num109, num110].frameX / 18);
									int num113 = 0;
									while (num112 >= 3)
									{
										num113++;
										num112 -= 3;
									}
									num112 = num109 - num112;
									int num114;
									for (num114 = (int)(Main.tile[num109, num110].frameY / 18); num114 >= 3; num114 -= 3)
									{
									}
									num114 = num110 - num114;
									float num115 = 12f + (float)Main.rand.Next(450) * 0.01f;
									float num116 = (float)Main.rand.Next(85, 105);
									float num117 = (float)Main.rand.Next(-35, 11);
									int type4 = 166;
									int damage = 17;
									float knockBack = 3.5f;
									Vector2 vector3 = new Vector2((float)((num112 + 2) * 16 - 8), (float)((num114 + 2) * 16 - 8));
									if (num113 == 0)
									{
										num116 *= -1f;
										vector3.X -= 12f;
									}
									else
									{
										vector3.X += 12f;
									}
									float num118 = num116;
									float num119 = num117;
									float num120 = (float)Math.Sqrt((double)(num118 * num118 + num119 * num119));
									num120 = num115 / num120;
									num118 *= num120;
									num119 *= num120;
									Projectile.NewProjectile(vector3.X, vector3.Y, num118, num119, type4, damage, knockBack, Main.myPlayer, 0f, 0f);
								}
							}
							if (this.releaseUseTile)
							{
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 132 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 136 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 144)
								{
									Wiring.hitSwitch(Player.tileTargetX, Player.tileTargetY);
									NetMessage.SendData(59, -1, -1, "", Player.tileTargetX, (float)Player.tileTargetY, 0f, 0f, 0);
								}
								else
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 139)
									{
										Main.PlaySound(28, Player.tileTargetX * 16, Player.tileTargetY * 16, 0);
										WorldGen.SwitchMB(Player.tileTargetX, Player.tileTargetY);
									}
									else
									{
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 207)
										{
											Main.PlaySound(28, Player.tileTargetX * 16, Player.tileTargetY * 16, 0);
											WorldGen.SwitchFountain(Player.tileTargetX, Player.tileTargetY);
										}
										else
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 216)
											{
												WorldGen.LaunchRocket(Player.tileTargetX, Player.tileTargetY);
											}
											else
											{
												if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 335)
												{
													WorldGen.LaunchRocketSmall(Player.tileTargetX, Player.tileTargetY);
												}
												else
												{
													if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 338)
													{
														int num121 = Player.tileTargetX;
														int num122 = Player.tileTargetY;
														if (Main.tile[num121, num122].frameY == 18)
														{
															num122--;
														}
														bool flag16 = false;
														for (int num123 = 0; num123 < 1000; num123++)
														{
															if (Main.projectile[num123].active && Main.projectile[num123].aiStyle == 73 && Main.projectile[num123].ai[0] == (float)num121 && Main.projectile[num123].ai[1] == (float)num122)
															{
																flag16 = true;
																break;
															}
														}
														if (!flag16)
														{
															Projectile.NewProjectile((float)(num121 * 16 + 8), (float)(num122 * 16 + 2), 0f, 0f, 419 + Main.rand.Next(4), 0, 0f, this.whoAmi, (float)num121, (float)num122);
														}
													}
													else
													{
														if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49 || (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50 && Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 90) || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 174)
														{
															WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
															if (Main.netMode == 1)
															{
																NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
															}
														}
														else
														{
															if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 334)
															{
																if (this.ItemFitsWeaponRack(this.inventory[this.selectedItem]))
																{
																	this.PlaceWeapon(Player.tileTargetX, Player.tileTargetY);
																}
																else
																{
																	int num124 = Player.tileTargetX;
																	int num125 = Player.tileTargetY;
																	if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY == 0)
																	{
																		num125++;
																	}
																	if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY == 36)
																	{
																		num125--;
																	}
																	int frameX = (int)Main.tile[Player.tileTargetX, num125].frameX;
																	int num126 = (int)Main.tile[Player.tileTargetX, num125].frameX;
																	int num127 = 0;
																	while (num126 >= 5000)
																	{
																		num126 -= 5000;
																		num127++;
																	}
																	if (num127 != 0)
																	{
																		num126 = (num127 - 1) * 18;
																	}
																	num126 %= 54;
																	if (num126 == 18)
																	{
																		frameX = (int)Main.tile[Player.tileTargetX - 1, num125].frameX;
																		num124--;
																	}
																	if (num126 == 36)
																	{
																		frameX = (int)Main.tile[Player.tileTargetX - 2, num125].frameX;
																		num124 -= 2;
																	}
																	if (frameX >= 5000)
																	{
																		WorldGen.KillTile(Player.tileTargetX, num125, true, false, false);
																		if (Main.netMode == 1)
																		{
																			NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)num125, 1f, 0);
																		}
																	}
																}
															}
															else
															{
																if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 125)
																{
																	this.AddBuff(29, 36000, true);
																	Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 4);
																}
																else
																{
																	if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 287)
																	{
																		this.AddBuff(93, 36000, true);
																		Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
																	}
																	else
																	{
																		if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79)
																		{
																			int num128 = Player.tileTargetX;
																			int num129 = Player.tileTargetY;
																			num128 += (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18 * -1);
																			if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 72)
																			{
																				num128 += 4;
																				num128++;
																			}
																			else
																			{
																				num128 += 2;
																			}
																			int num130 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
																			int num131 = 0;
																			while (num130 > 1)
																			{
																				num130 -= 2;
																				num131++;
																			}
																			num129 -= num130;
																			num129 += 2;
																			if (Player.CheckSpawn(num128, num129))
																			{
																				this.ChangeSpawn(num128, num129);
																				Main.NewText("Spawn point set!", 255, 240, 20, false);
																			}
																		}
																		else
																		{
																			if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 85)
																			{
																				bool flag17 = true;
																				if (this.sign >= 0)
																				{
																					int num132 = Sign.ReadSign(Player.tileTargetX, Player.tileTargetY);
																					if (num132 == this.sign)
																					{
																						this.sign = -1;
																						Main.npcChatText = "";
																						Main.editSign = false;
																						Main.PlaySound(11, -1, -1, 1);
																						flag17 = false;
																					}
																				}
																				if (flag17)
																				{
																					if (Main.netMode == 0)
																					{
																						this.talkNPC = -1;
																						Main.npcChatCornerItem = 0;
																						Main.playerInventory = false;
																						Main.editSign = false;
																						Main.PlaySound(10, -1, -1, 1);
																						int num133 = Sign.ReadSign(Player.tileTargetX, Player.tileTargetY);
																						this.sign = num133;
																						Main.npcChatText = Main.sign[num133].text;
																					}
																					else
																					{
																						int num134 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
																						int num135 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
																						while (num134 > 1)
																						{
																							num134 -= 2;
																						}
																						int num136 = Player.tileTargetX - num134;
																						int num137 = Player.tileTargetY - num135;
																						if (Main.tile[num136, num137].type == 55 || Main.tile[num136, num137].type == 85)
																						{
																							NetMessage.SendData(46, -1, -1, "", num136, (float)num137, 0f, 0f, 0);
																						}
																					}
																				}
																			}
																			else
																			{
																				if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 104)
																				{
																					string text = "AM";
																					double num138 = Main.time;
																					if (!Main.dayTime)
																					{
																						num138 += 54000.0;
																					}
																					num138 = num138 / 86400.0 * 24.0;
																					double num139 = 7.5;
																					num138 = num138 - num139 - 12.0;
																					if (num138 < 0.0)
																					{
																						num138 += 24.0;
																					}
																					if (num138 >= 12.0)
																					{
																						text = "PM";
																					}
																					int num140 = (int)num138;
																					double num141 = num138 - (double)num140;
																					num141 = (double)((int)(num141 * 60.0));
																					string text2 = string.Concat(num141);
																					if (num141 < 10.0)
																					{
																						text2 = "0" + text2;
																					}
																					if (num140 > 12)
																					{
																						num140 -= 12;
																					}
																					if (num140 == 0)
																					{
																						num140 = 12;
																					}
																					string newText = string.Concat(new object[]
																					{
																						"Time: ",
																						num140,
																						":",
																						text2,
																						" ",
																						text
																					});
																					Main.NewText(newText, 255, 240, 20, false);
																				}
																				else
																				{
																					if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 237)
																					{
																						bool flag18 = false;
																						if (!NPC.AnyNPCs(245) && Main.hardMode && NPC.downedPlantBoss)
																						{
																							for (int num142 = 0; num142 < 58; num142++)
																							{
																								if (this.inventory[num142].type == 1293)
																								{
																									this.inventory[num142].stack--;
																									if (this.inventory[num142].stack <= 0)
																									{
																										this.inventory[num142].SetDefaults(0, false);
																									}
																									flag18 = true;
																									break;
																								}
																							}
																						}
																						if (flag18)
																						{
																							Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
																							if (Main.netMode != 1)
																							{
																								NPC.SpawnOnPlayer(i, 245);
																							}
																							else
																							{
																								NetMessage.SendData(61, -1, -1, "", this.whoAmi, 245f, 0f, 0f, 0);
																							}
																						}
																					}
																					else
																					{
																						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10)
																						{
																							int num143 = Player.tileTargetX;
																							int num144 = Player.tileTargetY;
																							if (Main.tile[num143, num144].frameY >= 594 && Main.tile[num143, num144].frameY <= 646)
																							{
																								int num145 = 1141;
																								for (int num146 = 0; num146 < 58; num146++)
																								{
																									if (this.inventory[num146].type == num145 && this.inventory[num146].stack > 0)
																									{
																										this.inventory[num146].stack--;
																										if (this.inventory[num146].stack <= 0)
																										{
																											this.inventory[num146] = new Item();
																										}
																										WorldGen.UnlockDoor(num143, num144);
																										if (Main.netMode == 1)
																										{
																											NetMessage.SendData(52, -1, -1, "", this.whoAmi, 2f, (float)num143, (float)num144, 0);
																										}
																									}
																								}
																							}
																							else
																							{
																								WorldGen.OpenDoor(Player.tileTargetX, Player.tileTargetY, this.direction);
																								NetMessage.SendData(19, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.direction, 0);
																							}
																						}
																						else
																						{
																							if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11 && WorldGen.CloseDoor(Player.tileTargetX, Player.tileTargetY, false))
																							{
																								NetMessage.SendData(19, -1, -1, "", 1, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.direction, 0);
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 88)
								{
									Main.dresserX = Player.tileTargetX;
									Main.dresserY = Player.tileTargetY;
									Main.OpenClothesWindow();
								}
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 209)
								{
									WorldGen.SwitchCannon(Player.tileTargetX, Player.tileTargetY);
								}
								else
								{
									if ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 97) && this.talkNPC == -1)
									{
										Main.mouseRightRelease = false;
										int num147 = 0;
										int num148;
										for (num148 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18); num148 > 1; num148 -= 2)
										{
										}
										num148 = Player.tileTargetX - num148;
										int num149 = Player.tileTargetY - (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29)
										{
											num147 = 1;
										}
										else
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 97)
											{
												num147 = 2;
											}
										}
										if (this.sign > -1)
										{
											Main.PlaySound(11, -1, -1, 1);
											this.sign = -1;
											Main.editSign = false;
											Main.npcChatText = string.Empty;
										}
										if (Main.editChest)
										{
											Main.PlaySound(12, -1, -1, 1);
											Main.editChest = false;
											Main.npcChatText = string.Empty;
										}
										if (this.editedChestName)
										{
											NetMessage.SendData(33, -1, -1, Main.chest[this.chest].name, this.chest, 1f, 0f, 0f, 0);
											this.editedChestName = false;
										}
										if (Main.netMode == 1 && num147 == 0 && (Main.tile[num148, num149].frameX < 72 || Main.tile[num148, num149].frameX > 106) && (Main.tile[num148, num149].frameX < 144 || Main.tile[num148, num149].frameX > 178) && (Main.tile[num148, num149].frameX < 828 || Main.tile[num148, num149].frameX > 1006) && (Main.tile[num148, num149].frameX < 1296 || Main.tile[num148, num149].frameX > 1330) && (Main.tile[num148, num149].frameX < 1368 || Main.tile[num148, num149].frameX > 1402) && (Main.tile[num148, num149].frameX < 1440 || Main.tile[num148, num149].frameX > 1474))
										{
											if (num148 == this.chestX && num149 == this.chestY && this.chest != -1)
											{
												this.chest = -1;
												Main.PlaySound(11, -1, -1, 1);
											}
											else
											{
												NetMessage.SendData(31, -1, -1, "", num148, (float)num149, 0f, 0f, 0);
												Main.stackSplit = 600;
											}
										}
										else
										{
											int num150 = -1;
											if (num147 == 1)
											{
												num150 = -2;
											}
											else
											{
												if (num147 == 2)
												{
													num150 = -3;
												}
												else
												{
													bool flag19 = false;
													if ((Main.tile[num148, num149].frameX >= 72 && Main.tile[num148, num149].frameX <= 106) || (Main.tile[num148, num149].frameX >= 144 && Main.tile[num148, num149].frameX <= 178) || (Main.tile[num148, num149].frameX >= 828 && Main.tile[num148, num149].frameX <= 1006) || (Main.tile[num148, num149].frameX >= 1296 && Main.tile[num148, num149].frameX <= 1330) || (Main.tile[num148, num149].frameX >= 1368 && Main.tile[num148, num149].frameX <= 1402) || (Main.tile[num148, num149].frameX >= 1440 && Main.tile[num148, num149].frameX <= 1474))
													{
														int num151 = 327;
														if (Main.tile[num148, num149].frameX >= 144 && Main.tile[num148, num149].frameX <= 178)
														{
															num151 = 329;
														}
														if (Main.tile[num148, num149].frameX >= 828 && Main.tile[num148, num149].frameX <= 1006)
														{
															int num152 = (int)(Main.tile[num148, num149].frameX / 18);
															int num153 = 0;
															while (num152 >= 2)
															{
																num152 -= 2;
																num153++;
															}
															num153 -= 23;
															num151 = 1533 + num153;
														}
														flag19 = true;
														for (int num154 = 0; num154 < 58; num154++)
														{
															if (this.inventory[num154].type == num151 && this.inventory[num154].stack > 0)
															{
																if (num151 != 329)
																{
																	this.inventory[num154].stack--;
																	if (this.inventory[num154].stack <= 0)
																	{
																		this.inventory[num154] = new Item();
																	}
																}
																Chest.Unlock(num148, num149);
																if (Main.netMode == 1)
																{
																	NetMessage.SendData(52, -1, -1, "", this.whoAmi, 1f, (float)num148, (float)num149, 0);
																}
															}
														}
													}
													if (!flag19)
													{
														num150 = Chest.FindChest(num148, num149);
													}
												}
											}
											if (num150 != -1)
											{
												Main.stackSplit = 600;
												if (num150 == this.chest)
												{
													this.chest = -1;
													Main.PlaySound(11, -1, -1, 1);
												}
												else
												{
													if (num150 != this.chest && this.chest == -1)
													{
														this.chest = num150;
														Main.playerInventory = true;
														Main.PlaySound(10, -1, -1, 1);
														this.chestX = num148;
														this.chestY = num149;
													}
													else
													{
														this.chest = num150;
														Main.playerInventory = true;
														Main.PlaySound(12, -1, -1, 1);
														this.chestX = num148;
														this.chestY = num149;
													}
												}
											}
										}
									}
									else
									{
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 314 && this.gravDir == 1f)
										{
											bool flag20 = true;
											if (this.mount.Active)
											{
												if (this.mount.Type == 6)
												{
													flag20 = false;
												}
												else
												{
													this.mount.Dismount(this);
												}
											}
											if (flag20)
											{
												Vector2 vector4 = new Vector2((float)Main.mouseX + Main.screenPosition.X, (float)Main.mouseY + Main.screenPosition.Y);
												if (this.direction > 0)
												{
													this.minecartLeft = false;
												}
												else
												{
													this.minecartLeft = true;
												}
												this.grappling[0] = -1;
												this.grapCount = 0;
												for (int num155 = 0; num155 < 1000; num155++)
												{
													if (Main.projectile[num155].active && Main.projectile[num155].owner == this.whoAmi && Main.projectile[num155].aiStyle == 7)
													{
														Main.projectile[num155].Kill();
													}
												}
												Projectile.NewProjectile(vector4.X, vector4.Y, 0f, 0f, 403, 0, 0f, this.whoAmi, 0f, 0f);
											}
										}
									}
								}
							}
							this.releaseUseTile = false;
						}
						else
						{
							this.releaseUseTile = true;
						}
					}
				}
				else
				{
					if (Main.tile[Player.tileTargetX, Player.tileTargetY] == null)
					{
						Main.tile[Player.tileTargetX, Player.tileTargetY] = new Tile();
					}
					if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21)
					{
						Tile tile2 = Main.tile[Player.tileTargetX, Player.tileTargetY];
						int num156 = Player.tileTargetX;
						int num157 = Player.tileTargetY;
						if (tile2.frameX % 36 != 0)
						{
							num156--;
						}
						if (tile2.frameY % 36 != 0)
						{
							num157--;
						}
						int num158 = Chest.FindChest(num156, num157);
						this.showItemIcon2 = -1;
						if (num158 < 0)
						{
							this.showItemIconText = Lang.chestType[0];
						}
						else
						{
							if (Main.chest[num158].name != "")
							{
								this.showItemIconText = Main.chest[num158].name;
							}
							else
							{
								this.showItemIconText = Lang.chestType[(int)(tile2.frameX / 36)];
							}
							if (this.showItemIconText == Lang.chestType[(int)(tile2.frameX / 36)])
							{
								this.showItemIcon2 = Chest.typeToIcon[(int)(tile2.frameX / 36)];
								this.showItemIconText = "";
							}
						}
						this.noThrow = 2;
						this.showItemIcon = true;
						if (this.showItemIconText == "")
						{
							this.showItemIcon = false;
							this.showItemIcon2 = 0;
						}
					}
				}
			}
			if (this.tongued)
			{
				bool flag21 = false;
				if (Main.wof >= 0)
				{
					float num159 = Main.npc[Main.wof].position.X + (float)(Main.npc[Main.wof].width / 2);
					num159 += (float)(Main.npc[Main.wof].direction * 200);
					float num160 = Main.npc[Main.wof].position.Y + (float)(Main.npc[Main.wof].height / 2);
					Vector2 vector5 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
					float num161 = num159 - vector5.X;
					float num162 = num160 - vector5.Y;
					float num163 = (float)Math.Sqrt((double)(num161 * num161 + num162 * num162));
					float num164 = 11f;
					float num165;
					if (num163 > num164)
					{
						num165 = num164 / num163;
					}
					else
					{
						num165 = 1f;
						flag21 = true;
					}
					num161 *= num165;
					num162 *= num165;
					this.velocity.X = num161;
					this.velocity.Y = num162;
				}
				else
				{
					flag21 = true;
				}
				if (flag21 && Main.myPlayer == this.whoAmi)
				{
					for (int num166 = 0; num166 < 22; num166++)
					{
						if (this.buffType[num166] == 38)
						{
							this.DelBuff(num166);
						}
					}
				}
			}
			if (Main.myPlayer == this.whoAmi)
			{
				this.WOFTongue();
				if (this.controlHook)
				{
					if (this.releaseHook)
					{
						this.QuickGrapple();
					}
					this.releaseHook = false;
				}
				else
				{
					this.releaseHook = true;
				}
				if (this.talkNPC >= 0)
				{
					Rectangle rectangle = new Rectangle((int)(this.position.X + (float)(this.width / 2) - (float)(Player.tileRangeX * 16)), (int)(this.position.Y + (float)(this.height / 2) - (float)(Player.tileRangeY * 16)), Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
					Rectangle value = new Rectangle((int)Main.npc[this.talkNPC].position.X, (int)Main.npc[this.talkNPC].position.Y, Main.npc[this.talkNPC].width, Main.npc[this.talkNPC].height);
					if (!rectangle.Intersects(value) || this.chest != -1 || !Main.npc[this.talkNPC].active)
					{
						if (this.chest == -1)
						{
							Main.PlaySound(11, -1, -1, 1);
						}
						this.talkNPC = -1;
						Main.npcChatCornerItem = 0;
						Main.npcChatText = "";
					}
				}
				if (this.sign >= 0)
				{
					Rectangle rectangle2 = new Rectangle((int)(this.position.X + (float)(this.width / 2) - (float)(Player.tileRangeX * 16)), (int)(this.position.Y + (float)(this.height / 2) - (float)(Player.tileRangeY * 16)), Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
					try
					{
						Rectangle value2 = new Rectangle(Main.sign[this.sign].x * 16, Main.sign[this.sign].y * 16, 32, 32);
						if (!rectangle2.Intersects(value2))
						{
							Main.PlaySound(11, -1, -1, 1);
							this.sign = -1;
							Main.editSign = false;
							Main.npcChatText = "";
						}
					}
					catch
					{
						Main.PlaySound(11, -1, -1, 1);
						this.sign = -1;
						Main.editSign = false;
						Main.npcChatText = "";
					}
				}
				if (Main.editSign)
				{
					if (this.sign == -1)
					{
						Main.editSign = false;
					}
					else
					{
						Main.npcChatText = Main.GetInputText(Main.npcChatText);
						if (Main.inputTextEnter)
						{
							byte[] bytes = new byte[]
							{
								10
							};
							Main.npcChatText += Encoding.ASCII.GetString(bytes);
						}
						else
						{
							if (Main.inputTextEscape)
							{
								Main.PlaySound(12, -1, -1, 1);
								Main.editSign = false;
								Main.blockKey = Keys.Escape;
								Main.npcChatText = Main.sign[this.sign].text;
							}
						}
					}
				}
				else
				{
					if (Main.editChest)
					{
						string inputText = Main.GetInputText(Main.npcChatText);
						if (Main.inputTextEnter)
						{
							Main.PlaySound(12, -1, -1, 1);
							Main.editChest = false;
							int num167 = Main.player[Main.myPlayer].chest;
							if (Main.npcChatText == Main.defaultChestName)
							{
								Main.npcChatText = "";
							}
							if (Main.chest[num167].name != Main.npcChatText)
							{
								Main.chest[num167].name = Main.npcChatText;
								if (Main.netMode == 1)
								{
									this.editedChestName = true;
								}
							}
						}
						else
						{
							if (Main.inputTextEscape)
							{
								Main.PlaySound(12, -1, -1, 1);
								Main.editChest = false;
								Main.npcChatText = string.Empty;
								Main.blockKey = Keys.Escape;
							}
							else
							{
								if (inputText.Length <= 20)
								{
									Main.npcChatText = inputText;
								}
							}
						}
					}
				}
				if (this.mount.Active && this.mount.Type == 6 && Math.Abs(this.velocity.X) > 4f)
				{
					Rectangle rectangle3 = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
					for (int num168 = 0; num168 < 200; num168++)
					{
						if (Main.npc[num168].active && !Main.npc[num168].friendly && Main.npc[num168].damage > 0 && Main.npc[num168].immune[i] == 0 && rectangle3.Intersects(new Rectangle((int)Main.npc[num168].position.X, (int)Main.npc[num168].position.Y, Main.npc[num168].width, Main.npc[num168].height)))
						{
							float num169 = (float)this.meleeCrit;
							if (this.meleeCrit < this.rangedCrit)
							{
								num169 = (float)this.rangedCrit;
							}
							if (this.rangedCrit < this.magicCrit)
							{
								num169 = (float)this.magicCrit;
							}
							bool crit = false;
							if ((float)Main.rand.Next(1, 101) <= num169)
							{
								crit = true;
							}
							float num170 = Math.Abs(this.velocity.X) / this.maxRunSpeed;
							int num171 = Main.DamageVar(25f + 55f * num170);
							float num172 = 5f + 25f * num170;
							int num173 = 1;
							if (this.velocity.X < 0f)
							{
								num173 = -1;
							}
							Main.npc[num168].StrikeNPC(num171, num172, num173, crit, false);
							if (Main.netMode != 0)
							{
								NetMessage.SendData(28, -1, -1, "", num168, (float)num171, num172, (float)(-(float)num173), 0);
							}
							Main.npc[num168].immune[i] = 30;
						}
					}
				}
				if (!this.immune)
				{
					Rectangle rectangle4 = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
					for (int num174 = 0; num174 < 200; num174++)
					{
						if (Main.npc[num174].active && !Main.npc[num174].friendly && Main.npc[num174].damage > 0 && rectangle4.Intersects(new Rectangle((int)Main.npc[num174].position.X, (int)Main.npc[num174].position.Y, Main.npc[num174].width, Main.npc[num174].height)))
						{
							int num175 = -1;
							if (Main.npc[num174].position.X + (float)(Main.npc[num174].width / 2) < this.position.X + (float)(this.width / 2))
							{
								num175 = 1;
							}
							int num176 = Main.DamageVar((float)Main.npc[num174].damage);
							if (this.whoAmi == Main.myPlayer && this.thorns && !this.immune && !Main.npc[num174].dontTakeDamage)
							{
								int num177 = num176 / 3;
								int num178 = 10;
								if (this.turtleThorns)
								{
									num177 = num176;
								}
								Main.npc[num174].StrikeNPC(num177, (float)num178, -num175, false, false);
								if (Main.netMode != 0)
								{
									NetMessage.SendData(28, -1, -1, "", num174, (float)num177, (float)num178, (float)(-(float)num175), 0);
								}
							}
							if (this.resistCold && Main.npc[num174].coldDamage)
							{
								num176 = (int)((float)num176 * 0.7f);
							}
							if (!this.immune)
							{
								this.StatusPlayer(Main.npc[num174]);
							}
							this.Hurt(num176, num175, false, false, Lang.deathMsg(-1, num174, -1, -1), false);
						}
					}
				}
				Vector2 vector6 = Collision.HurtTiles(this.position, this.velocity, this.width, this.height, this.fireWalk);
				if (vector6.Y == 20f)
				{
					this.AddBuff(67, 20, true);
				}
				else
				{
					if (vector6.Y == 15f)
					{
						if (this.suffocateDelay < 5)
						{
							this.suffocateDelay += 1;
						}
						else
						{
							this.AddBuff(68, 1, true);
						}
					}
					else
					{
						if (vector6.Y != 0f)
						{
							int damage2 = Main.DamageVar(vector6.Y);
							this.Hurt(damage2, 0, false, false, Lang.deathMsg(-1, -1, -1, 3), false);
						}
						else
						{
							this.suffocateDelay = 0;
						}
					}
				}
			}
			if (this.controlRight)
			{
				this.releaseRight = false;
			}
			else
			{
				this.releaseRight = true;
				this.rightTimer = 7;
			}
			if (this.controlLeft)
			{
				this.releaseLeft = false;
			}
			else
			{
				this.releaseLeft = true;
				this.leftTimer = 7;
			}
			if (this.rightTimer > 0)
			{
				this.rightTimer--;
			}
			else
			{
				if (this.controlRight)
				{
					this.rightTimer = 7;
				}
			}
			if (this.leftTimer > 0)
			{
				this.leftTimer--;
			}
			else
			{
				if (this.controlLeft)
				{
					this.leftTimer = 7;
				}
			}
			this.GrappleMovement();
			this.StickyMovement();
			this.CheckDrowning();
			if (this.gravDir == -1f)
			{
				this.waterWalk = false;
				this.waterWalk2 = false;
			}
			int num179 = this.height;
			if (this.waterWalk)
			{
				num179 -= 6;
			}
			bool flag22 = Collision.LavaCollision(this.position, this.width, num179);
			if (flag22)
			{
				if (!this.lavaImmune && Main.myPlayer == i && !this.immune)
				{
					if (this.lavaTime > 0)
					{
						this.lavaTime--;
					}
					else
					{
						if (this.lavaRose)
						{
							this.Hurt(50, 0, false, false, Lang.deathMsg(-1, -1, -1, 2), false);
							this.AddBuff(24, 210, true);
						}
						else
						{
							this.Hurt(80, 0, false, false, Lang.deathMsg(-1, -1, -1, 2), false);
							this.AddBuff(24, 420, true);
						}
					}
				}
				this.lavaWet = true;
			}
			else
			{
				this.lavaWet = false;
				if (this.lavaTime < this.lavaMax)
				{
					this.lavaTime++;
				}
			}
			if (this.lavaTime > this.lavaMax)
			{
				this.lavaTime = this.lavaMax;
			}
			if (this.waterWalk2 && !this.waterWalk)
			{
				num179 -= 6;
			}
			bool flag23 = Collision.WetCollision(this.position, this.width, this.height);
			bool flag24 = Collision.honey;
			if (flag24)
			{
				this.AddBuff(48, 1800, true);
				this.honeyWet = true;
			}
			if (flag23)
			{
				if (this.onFire && !this.lavaWet)
				{
					for (int num180 = 0; num180 < 22; num180++)
					{
						if (this.buffType[num180] == 24)
						{
							this.DelBuff(num180);
						}
					}
				}
				if (!this.wet)
				{
					if (this.wetCount == 0)
					{
						this.wetCount = 10;
						if (!flag22)
						{
							if (this.honeyWet)
							{
								for (int num181 = 0; num181 < 20; num181++)
								{
									int num182 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 152, 0f, 0f, 0, default(Color), 1f);
									Dust expr_9E45_cp_0 = Main.dust[num182];
									expr_9E45_cp_0.velocity.Y = expr_9E45_cp_0.velocity.Y - 1f;
									Dust expr_9E65_cp_0 = Main.dust[num182];
									expr_9E65_cp_0.velocity.X = expr_9E65_cp_0.velocity.X * 2.5f;
									Main.dust[num182].scale = 1.3f;
									Main.dust[num182].alpha = 100;
									Main.dust[num182].noGravity = true;
								}
								Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
							}
							else
							{
								for (int num183 = 0; num183 < 50; num183++)
								{
									int num184 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, Dust.dustWater(), 0f, 0f, 0, default(Color), 1f);
									Dust expr_9F66_cp_0 = Main.dust[num184];
									expr_9F66_cp_0.velocity.Y = expr_9F66_cp_0.velocity.Y - 3f;
									Dust expr_9F86_cp_0 = Main.dust[num184];
									expr_9F86_cp_0.velocity.X = expr_9F86_cp_0.velocity.X * 2.5f;
									Main.dust[num184].scale = 0.8f;
									Main.dust[num184].alpha = 100;
									Main.dust[num184].noGravity = true;
								}
								Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 0);
							}
						}
						else
						{
							for (int num185 = 0; num185 < 20; num185++)
							{
								int num186 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 35, 0f, 0f, 0, default(Color), 1f);
								Dust expr_A084_cp_0 = Main.dust[num186];
								expr_A084_cp_0.velocity.Y = expr_A084_cp_0.velocity.Y - 1.5f;
								Dust expr_A0A4_cp_0 = Main.dust[num186];
								expr_A0A4_cp_0.velocity.X = expr_A0A4_cp_0.velocity.X * 2.5f;
								Main.dust[num186].scale = 1.3f;
								Main.dust[num186].alpha = 100;
								Main.dust[num186].noGravity = true;
							}
							Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
						}
					}
					this.wet = true;
				}
			}
			else
			{
				if (this.wet)
				{
					this.wet = false;
					if (this.jump > Player.jumpHeight / 5 && this.wetSlime == 0)
					{
						this.jump = Player.jumpHeight / 5;
					}
					if (this.wetCount == 0)
					{
						this.wetCount = 10;
						if (!this.lavaWet)
						{
							if (this.honeyWet)
							{
								for (int num187 = 0; num187 < 20; num187++)
								{
									int num188 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 152, 0f, 0f, 0, default(Color), 1f);
									Dust expr_A20B_cp_0 = Main.dust[num188];
									expr_A20B_cp_0.velocity.Y = expr_A20B_cp_0.velocity.Y - 1f;
									Dust expr_A22B_cp_0 = Main.dust[num188];
									expr_A22B_cp_0.velocity.X = expr_A22B_cp_0.velocity.X * 2.5f;
									Main.dust[num188].scale = 1.3f;
									Main.dust[num188].alpha = 100;
									Main.dust[num188].noGravity = true;
								}
								Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
							}
							else
							{
								for (int num189 = 0; num189 < 50; num189++)
								{
									int num190 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2)), this.width + 12, 24, Dust.dustWater(), 0f, 0f, 0, default(Color), 1f);
									Dust expr_A326_cp_0 = Main.dust[num190];
									expr_A326_cp_0.velocity.Y = expr_A326_cp_0.velocity.Y - 4f;
									Dust expr_A346_cp_0 = Main.dust[num190];
									expr_A346_cp_0.velocity.X = expr_A346_cp_0.velocity.X * 2.5f;
									Main.dust[num190].scale = 0.8f;
									Main.dust[num190].alpha = 100;
									Main.dust[num190].noGravity = true;
								}
								Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 0);
							}
						}
						else
						{
							for (int num191 = 0; num191 < 20; num191++)
							{
								int num192 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 35, 0f, 0f, 0, default(Color), 1f);
								Dust expr_A444_cp_0 = Main.dust[num192];
								expr_A444_cp_0.velocity.Y = expr_A444_cp_0.velocity.Y - 1.5f;
								Dust expr_A464_cp_0 = Main.dust[num192];
								expr_A464_cp_0.velocity.X = expr_A464_cp_0.velocity.X * 2.5f;
								Main.dust[num192].scale = 1.3f;
								Main.dust[num192].alpha = 100;
								Main.dust[num192].noGravity = true;
							}
							Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
						}
					}
				}
			}
			if (!flag24)
			{
				this.honeyWet = false;
			}
			if (!this.wet)
			{
				this.lavaWet = false;
				this.honeyWet = false;
			}
			if (this.wetCount > 0)
			{
				this.wetCount -= 1;
			}
			if (this.wetSlime > 0)
			{
				this.wetSlime -= 1;
			}
			if (this.wet && this.mount.Active)
			{
				switch (this.mount.Type)
				{
				case 3:
					this.wetSlime = 30;
					if (this.velocity.Y > 2f)
					{
						this.velocity.Y = this.velocity.Y * 0.9f;
					}
					this.velocity.Y = this.velocity.Y - 0.5f;
					if (this.velocity.Y < -4f)
					{
						this.velocity.Y = -4f;
					}
					break;
				case 5:
					if (this.whoAmi == Main.myPlayer)
					{
						this.mount.Dismount(this);
					}
					break;
				}
			}
			float num193 = 1f + Math.Abs(this.velocity.X) / 3f;
			if (this.gfxOffY > 0f)
			{
				this.gfxOffY -= num193 * this.stepSpeed;
				if (this.gfxOffY < 0f)
				{
					this.gfxOffY = 0f;
				}
			}
			else
			{
				if (this.gfxOffY < 0f)
				{
					this.gfxOffY += num193 * this.stepSpeed;
					if (this.gfxOffY > 0f)
					{
						this.gfxOffY = 0f;
					}
				}
			}
			if (this.gfxOffY > 32f)
			{
				this.gfxOffY = 32f;
			}
			if (this.gfxOffY < -32f)
			{
				this.gfxOffY = -32f;
			}
			if (Main.myPlayer == i && !this.iceSkate)
			{
				this.CheckIceBreak();
			}
			this.SlopeDownMovement();
			if (this.velocity.Y == this.gravity && (!this.mount.Active || this.mount.Type != 6))
			{
				Collision.StepDown(ref this.position, ref this.velocity, this.width, this.height, ref this.stepSpeed, ref this.gfxOffY, (int)this.gravDir, this.waterWalk || this.waterWalk2);
			}
			if (this.gravDir == -1f)
			{
				if ((this.carpetFrame != -1 || this.velocity.Y <= this.gravity) && !this.controlUp)
				{
					Collision.StepUp(ref this.position, ref this.velocity, this.width, this.height, ref this.stepSpeed, ref this.gfxOffY, (int)this.gravDir, this.controlUp);
				}
			}
			else
			{
				if ((this.carpetFrame != -1 || this.velocity.Y >= this.gravity) && !this.controlDown && (!this.mount.Active || this.mount.Type != 6))
				{
					Collision.StepUp(ref this.position, ref this.velocity, this.width, this.height, ref this.stepSpeed, ref this.gfxOffY, (int)this.gravDir, this.controlUp);
				}
			}
			this.oldPosition = this.position;
			bool falling = false;
			if (this.velocity.Y > this.gravity)
			{
				falling = true;
			}
			Vector2 vector7 = this.velocity;
			this.slideDir = 0;
			bool ignorePlats = false;
			bool fallThrough = this.controlDown;
			if (this.gravDir == -1f)
			{
				ignorePlats = true;
				fallThrough = true;
			}
			this.onTrack = false;
			bool flag25 = false;
			if (this.mount.Active && this.mount.Type == 6)
			{
				float num194;
				if (!this.ignoreWater && !this.merman)
				{
					if (this.honeyWet)
					{
						num194 = 0.25f;
					}
					else
					{
						if (this.wet)
						{
							num194 = 0.5f;
						}
						else
						{
							num194 = 1f;
						}
					}
				}
				else
				{
					num194 = 1f;
				}
				this.velocity *= num194;
				BitsByte bitsByte = Minecart.TrackCollision(ref this.position, ref this.velocity, ref this.lastBoost, this.width, this.height, this.controlDown, this.controlUp, this.fallStart2, false);
				if (bitsByte[0])
				{
					this.onTrack = true;
					this.gfxOffY = Minecart.TrackRotation(ref this.fullRotation, this.position + this.velocity, this.width, this.height, this.controlDown, this.controlUp);
					this.fullRotationOrigin = new Vector2((float)(this.width / 2), (float)this.height);
				}
				if (bitsByte[1])
				{
					if (this.controlLeft || this.controlRight)
					{
						if (this.cartFlip)
						{
							this.cartFlip = false;
						}
						else
						{
							this.cartFlip = true;
						}
					}
					if (this.velocity.X > 0f)
					{
						this.direction = 1;
					}
					else
					{
						if (this.velocity.X < 0f)
						{
							this.direction = -1;
						}
					}
					Main.PlaySound(2, (int)this.position.X + this.width / 2, (int)this.position.Y + this.height / 2, 56);
				}
				this.velocity /= num194;
				if (bitsByte[3] && this.whoAmi == Main.myPlayer)
				{
					flag25 = true;
				}
				if (bitsByte[2])
				{
					this.cartRampTime = (int)(Math.Abs(this.velocity.X) / this.mount.RunSpeed * 20f);
				}
				if (bitsByte[4])
				{
					this.trackBoost -= 4f;
				}
				if (bitsByte[5])
				{
					this.trackBoost += 4f;
				}
			}
			if (this.wingsLogic == 3 && this.controlUp && this.controlDown)
			{
				this.position += this.velocity;
			}
			else
			{
				if (this.tongued)
				{
					this.position += this.velocity;
				}
				else
				{
					if (this.honeyWet && !this.ignoreWater)
					{
						this.HoneyCollision(fallThrough, ignorePlats);
					}
					else
					{
						if (this.wet && !this.merman && !this.ignoreWater)
						{
							this.WaterCollision(fallThrough, ignorePlats);
						}
						else
						{
							this.DryCollision(fallThrough, ignorePlats);
						}
					}
				}
			}
			if (this.wingsLogic != 3 || !this.controlUp || !this.controlDown)
			{
				this.SlopingCollision();
			}
			if (flag25)
			{
				NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
				Minecart.HitTrackSwitch(new Vector2(this.position.X, this.position.Y), this.width, this.height);
			}
			if (vector7.X != this.velocity.X)
			{
				if (vector7.X < 0f)
				{
					this.slideDir = -1;
				}
				else
				{
					if (vector7.X > 0f)
					{
						this.slideDir = 1;
					}
				}
			}
			if (this.gravDir == 1f && Collision.up)
			{
				this.velocity.Y = 0.01f;
				if (!this.merman)
				{
					this.jump = 0;
				}
			}
			else
			{
				if (this.gravDir == -1f && Collision.down)
				{
					this.velocity.Y = -0.01f;
					if (!this.merman)
					{
						this.jump = 0;
					}
				}
			}
			if (this.velocity.Y == 0f && this.grappling[0] == -1)
			{
				this.FloorVisuals(falling);
			}
			if (this.whoAmi == Main.myPlayer)
			{
				Collision.SwitchTiles(this.position, this.width, this.height, this.oldPosition, 1);
			}
			this.BordersMovement();
			this.numMinions = 0;
			this.slotsMinions = 0f;
			if (Main.ignoreErrors)
			{
				try
				{
					this.ItemCheck(i);
					goto IL_AD56;
				}
				catch
				{
					goto IL_AD56;
				}
			}
			this.ItemCheck(i);
			IL_AD56:
			this.PlayerFrame();
			if (this.statLife > this.statLifeMax2)
			{
				this.statLife = this.statLifeMax2;
			}
			if (this.statMana > this.statManaMax2)
			{
				this.statMana = this.statManaMax2;
			}
			this.grappling[0] = -1;
			this.grapCount = 0;
		}
		public bool SellItem(int price, int stack)
		{
			if (price <= 0)
			{
				return false;
			}
			Item[] array = new Item[58];
			for (int i = 0; i < 58; i++)
			{
				array[i] = new Item();
				array[i] = this.inventory[i].Clone();
			}
			int j = price / 5;
			j *= stack;
			if (j < 1)
			{
				j = 1;
			}
			bool flag = false;
			while (j >= 1000000)
			{
				if (flag)
				{
					break;
				}
				int num = -1;
				for (int k = 53; k >= 0; k--)
				{
					if (num == -1 && (this.inventory[k].type == 0 || this.inventory[k].stack == 0))
					{
						num = k;
					}
					while (this.inventory[k].type == 74 && this.inventory[k].stack < this.inventory[k].maxStack && j >= 1000000)
					{
						this.inventory[k].stack++;
						j -= 1000000;
						this.DoCoins(k);
						if (this.inventory[k].stack == 0 && num == -1)
						{
							num = k;
						}
					}
				}
				if (j >= 1000000)
				{
					if (num == -1)
					{
						flag = true;
					}
					else
					{
						this.inventory[num].SetDefaults(74, false);
						j -= 1000000;
					}
				}
			}
			while (j >= 10000)
			{
				if (flag)
				{
					break;
				}
				int num2 = -1;
				for (int l = 53; l >= 0; l--)
				{
					if (num2 == -1 && (this.inventory[l].type == 0 || this.inventory[l].stack == 0))
					{
						num2 = l;
					}
					while (this.inventory[l].type == 73 && this.inventory[l].stack < this.inventory[l].maxStack && j >= 10000)
					{
						this.inventory[l].stack++;
						j -= 10000;
						this.DoCoins(l);
						if (this.inventory[l].stack == 0 && num2 == -1)
						{
							num2 = l;
						}
					}
				}
				if (j >= 10000)
				{
					if (num2 == -1)
					{
						flag = true;
					}
					else
					{
						this.inventory[num2].SetDefaults(73, false);
						j -= 10000;
					}
				}
			}
			while (j >= 100)
			{
				if (flag)
				{
					break;
				}
				int num3 = -1;
				for (int m = 53; m >= 0; m--)
				{
					if (num3 == -1 && (this.inventory[m].type == 0 || this.inventory[m].stack == 0))
					{
						num3 = m;
					}
					while (this.inventory[m].type == 72 && this.inventory[m].stack < this.inventory[m].maxStack && j >= 100)
					{
						this.inventory[m].stack++;
						j -= 100;
						this.DoCoins(m);
						if (this.inventory[m].stack == 0 && num3 == -1)
						{
							num3 = m;
						}
					}
				}
				if (j >= 100)
				{
					if (num3 == -1)
					{
						flag = true;
					}
					else
					{
						this.inventory[num3].SetDefaults(72, false);
						j -= 100;
					}
				}
			}
			while (j >= 1 && !flag)
			{
				int num4 = -1;
				for (int n = 53; n >= 0; n--)
				{
					if (num4 == -1 && (this.inventory[n].type == 0 || this.inventory[n].stack == 0))
					{
						num4 = n;
					}
					while (this.inventory[n].type == 71 && this.inventory[n].stack < this.inventory[n].maxStack && j >= 1)
					{
						this.inventory[n].stack++;
						j--;
						this.DoCoins(n);
						if (this.inventory[n].stack == 0 && num4 == -1)
						{
							num4 = n;
						}
					}
				}
				if (j >= 1)
				{
					if (num4 == -1)
					{
						flag = true;
					}
					else
					{
						this.inventory[num4].SetDefaults(71, false);
						j--;
					}
				}
			}
			if (flag)
			{
				for (int num5 = 0; num5 < 58; num5++)
				{
					this.inventory[num5] = array[num5].Clone();
				}
				return false;
			}
			return true;
		}
		public bool BuyItem(int price)
		{
			if (price == 0)
			{
				return true;
			}
			long num = 0L;
			Item[] array = new Item[54];
			for (int i = 0; i < 54; i++)
			{
				array[i] = new Item();
				array[i] = this.inventory[i].Clone();
				if (this.inventory[i].type == 71)
				{
					num += (long)this.inventory[i].stack;
				}
				if (this.inventory[i].type == 72)
				{
					num += (long)(this.inventory[i].stack * 100);
				}
				if (this.inventory[i].type == 73)
				{
					num += (long)(this.inventory[i].stack * 10000);
				}
				if (this.inventory[i].type == 74)
				{
					num += (long)(this.inventory[i].stack * 1000000);
				}
			}
			if (num >= (long)price)
			{
				int j = price;
				while (j > 0)
				{
					if (j >= 1000000)
					{
						for (int k = 0; k < 54; k++)
						{
							if (this.inventory[k].type == 74)
							{
								while (this.inventory[k].stack > 0 && j >= 1000000)
								{
									j -= 1000000;
									this.inventory[k].stack--;
									if (this.inventory[k].stack == 0)
									{
										this.inventory[k].type = 0;
									}
								}
							}
						}
					}
					if (j >= 10000)
					{
						for (int l = 0; l < 54; l++)
						{
							if (this.inventory[l].type == 73)
							{
								while (this.inventory[l].stack > 0 && j >= 10000)
								{
									j -= 10000;
									this.inventory[l].stack--;
									if (this.inventory[l].stack == 0)
									{
										this.inventory[l].type = 0;
									}
								}
							}
						}
					}
					if (j >= 100)
					{
						for (int m = 0; m < 54; m++)
						{
							if (this.inventory[m].type == 72)
							{
								while (this.inventory[m].stack > 0 && j >= 100)
								{
									j -= 100;
									this.inventory[m].stack--;
									if (this.inventory[m].stack == 0)
									{
										this.inventory[m].type = 0;
									}
								}
							}
						}
					}
					if (j >= 1)
					{
						for (int n = 0; n < 54; n++)
						{
							if (this.inventory[n].type == 71)
							{
								while (this.inventory[n].stack > 0 && j >= 1)
								{
									j--;
									this.inventory[n].stack--;
									if (this.inventory[n].stack == 0)
									{
										this.inventory[n].type = 0;
									}
								}
							}
						}
					}
					if (j > 0)
					{
						int num2 = -1;
						for (int num3 = 53; num3 >= 0; num3--)
						{
							if (this.inventory[num3].type == 0 || this.inventory[num3].stack == 0)
							{
								num2 = num3;
								break;
							}
						}
						if (num2 < 0)
						{
							for (int num4 = 0; num4 < 54; num4++)
							{
								this.inventory[num4] = array[num4].Clone();
							}
							return false;
						}
						bool flag = true;
						if (j >= 10000)
						{
							for (int num5 = 0; num5 < 58; num5++)
							{
								if (this.inventory[num5].type == 74 && this.inventory[num5].stack >= 1)
								{
									this.inventory[num5].stack--;
									if (this.inventory[num5].stack == 0)
									{
										this.inventory[num5].type = 0;
									}
									this.inventory[num2].SetDefaults(73, false);
									this.inventory[num2].stack = 100;
									flag = false;
									break;
								}
							}
						}
						else
						{
							if (j >= 100)
							{
								for (int num6 = 0; num6 < 54; num6++)
								{
									if (this.inventory[num6].type == 73 && this.inventory[num6].stack >= 1)
									{
										this.inventory[num6].stack--;
										if (this.inventory[num6].stack == 0)
										{
											this.inventory[num6].type = 0;
										}
										this.inventory[num2].SetDefaults(72, false);
										this.inventory[num2].stack = 100;
										flag = false;
										break;
									}
								}
							}
							else
							{
								if (j >= 1)
								{
									for (int num7 = 0; num7 < 54; num7++)
									{
										if (this.inventory[num7].type == 72 && this.inventory[num7].stack >= 1)
										{
											this.inventory[num7].stack--;
											if (this.inventory[num7].stack == 0)
											{
												this.inventory[num7].type = 0;
											}
											this.inventory[num2].SetDefaults(71, false);
											this.inventory[num2].stack = 100;
											flag = false;
											break;
										}
									}
								}
							}
						}
						if (flag)
						{
							if (j < 10000)
							{
								for (int num8 = 0; num8 < 54; num8++)
								{
									if (this.inventory[num8].type == 73 && this.inventory[num8].stack >= 1)
									{
										this.inventory[num8].stack--;
										if (this.inventory[num8].stack == 0)
										{
											this.inventory[num8].type = 0;
										}
										this.inventory[num2].SetDefaults(72, false);
										this.inventory[num2].stack = 100;
										flag = false;
										break;
									}
								}
							}
							if (flag && j < 1000000)
							{
								for (int num9 = 0; num9 < 54; num9++)
								{
									if (this.inventory[num9].type == 74 && this.inventory[num9].stack >= 1)
									{
										this.inventory[num9].stack--;
										if (this.inventory[num9].stack == 0)
										{
											this.inventory[num9].type = 0;
										}
										this.inventory[num2].SetDefaults(73, false);
										this.inventory[num2].stack = 100;
										break;
									}
								}
							}
						}
					}
				}
				return true;
			}
			return false;
		}
		public void AdjTiles()
		{
			int num = 4;
			int num2 = 3;
			for (int i = 0; i < 340; i++)
			{
				this.oldAdjTile[i] = this.adjTile[i];
				this.adjTile[i] = false;
			}
			this.oldAdjWater = this.adjWater;
			this.adjWater = false;
			this.oldAdjHoney = this.adjHoney;
			this.adjHoney = false;
			this.oldAdjLava = this.adjLava;
			this.adjLava = false;
			int num3 = (int)((this.position.X + (float)(this.width / 2)) / 16f);
			int num4 = (int)((this.position.Y + (float)this.height) / 16f);
			for (int j = num3 - num; j <= num3 + num; j++)
			{
				for (int k = num4 - num2; k < num4 + num2; k++)
				{
					if (Main.tile[j, k].active())
					{
						this.adjTile[(int)Main.tile[j, k].type] = true;
						if (Main.tile[j, k].type == 302)
						{
							this.adjTile[17] = true;
						}
						if (Main.tile[j, k].type == 77)
						{
							this.adjTile[17] = true;
						}
						if (Main.tile[j, k].type == 133)
						{
							this.adjTile[17] = true;
							this.adjTile[77] = true;
						}
						if (Main.tile[j, k].type == 134)
						{
							this.adjTile[16] = true;
						}
					}
					if (Main.tile[j, k].liquid > 200 && Main.tile[j, k].liquidType() == 0)
					{
						this.adjWater = true;
					}
					if (Main.tile[j, k].liquid > 200 && Main.tile[j, k].liquidType() == 2)
					{
						this.adjHoney = true;
					}
					if (Main.tile[j, k].liquid > 200 && Main.tile[j, k].liquidType() == 1)
					{
						this.adjLava = true;
					}
				}
			}
			if (Main.playerInventory)
			{
				bool flag = false;
				for (int l = 0; l < 340; l++)
				{
					if (this.oldAdjTile[l] != this.adjTile[l])
					{
						flag = true;
						break;
					}
				}
				if (this.adjWater != this.oldAdjWater)
				{
					flag = true;
				}
				if (this.adjHoney != this.oldAdjHoney)
				{
					flag = true;
				}
				if (this.adjLava != this.oldAdjLava)
				{
					flag = true;
				}
				if (flag)
				{
					Recipe.FindRecipes();
				}
			}
		}
		public void PlayerFrame()
		{
			if (this.swimTime > 0)
			{
				this.swimTime--;
				if (!this.wet)
				{
					this.swimTime = 0;
				}
			}
			this.head = this.armor[0].headSlot;
			this.body = this.armor[1].bodySlot;
			this.legs = this.armor[2].legSlot;
			for (int i = 3; i < 8; i++)
			{
				if ((this.shield <= 0 || this.armor[i].frontSlot < 1 || this.armor[i].frontSlot > 4) && (this.front < 1 || this.front > 4 || this.armor[i].shieldSlot <= 0))
				{
					if (this.armor[i].wingSlot > 0)
					{
						if (this.hideVisual[i] && (this.velocity.Y == 0f || this.mount.Active))
						{
							goto IL_283;
						}
						this.wings = (int)this.armor[i].wingSlot;
					}
					if (!this.hideVisual[i])
					{
						if (this.armor[i].handOnSlot > 0)
						{
							this.handon = this.armor[i].handOnSlot;
						}
						if (this.armor[i].handOffSlot > 0)
						{
							this.handoff = this.armor[i].handOffSlot;
						}
						if (this.armor[i].backSlot > 0)
						{
							this.back = this.armor[i].backSlot;
							this.front = -1;
						}
						if (this.armor[i].frontSlot > 0)
						{
							this.front = this.armor[i].frontSlot;
						}
						if (this.armor[i].shoeSlot > 0)
						{
							this.shoe = this.armor[i].shoeSlot;
						}
						if (this.armor[i].waistSlot > 0)
						{
							this.waist = this.armor[i].waistSlot;
						}
						if (this.armor[i].shieldSlot > 0)
						{
							this.shield = this.armor[i].shieldSlot;
						}
						if (this.armor[i].neckSlot > 0)
						{
							this.neck = this.armor[i].neckSlot;
						}
						if (this.armor[i].faceSlot > 0)
						{
							this.face = this.armor[i].faceSlot;
						}
						if (this.armor[i].balloonSlot > 0)
						{
							this.balloon = this.armor[i].balloonSlot;
						}
					}
				}
				IL_283:;
			}
			for (int j = 11; j < 16; j++)
			{
				if (this.armor[j].handOnSlot > 0)
				{
					this.handon = this.armor[j].handOnSlot;
				}
				if (this.armor[j].handOffSlot > 0)
				{
					this.handoff = this.armor[j].handOffSlot;
				}
				if (this.armor[j].backSlot > 0)
				{
					this.back = this.armor[j].backSlot;
					this.front = -1;
				}
				if (this.armor[j].frontSlot > 0)
				{
					this.front = this.armor[j].frontSlot;
				}
				if (this.armor[j].shoeSlot > 0)
				{
					this.shoe = this.armor[j].shoeSlot;
				}
				if (this.armor[j].waistSlot > 0)
				{
					this.waist = this.armor[j].waistSlot;
				}
				if (this.armor[j].shieldSlot > 0)
				{
					this.shield = this.armor[j].shieldSlot;
				}
				if (this.armor[j].neckSlot > 0)
				{
					this.neck = this.armor[j].neckSlot;
				}
				if (this.armor[j].faceSlot > 0)
				{
					this.face = this.armor[j].faceSlot;
				}
				if (this.armor[j].balloonSlot > 0)
				{
					this.balloon = this.armor[j].balloonSlot;
				}
				if (this.armor[j].wingSlot > 0)
				{
					this.wings = (int)this.armor[j].wingSlot;
				}
			}
			if (this.armor[8].headSlot >= 0)
			{
				this.head = this.armor[8].headSlot;
			}
			if (this.armor[9].bodySlot >= 0)
			{
				this.body = this.armor[9].bodySlot;
			}
			if (this.armor[10].legSlot >= 0)
			{
				this.legs = this.armor[10].legSlot;
			}
			this.wearsRobe = false;
			int num = this.body;
			if (num <= 36)
			{
				if (num != 15)
				{
					if (num == 36)
					{
						this.legs = 89;
						this.wearsRobe = true;
					}
				}
				else
				{
					this.legs = 88;
					this.wearsRobe = true;
				}
			}
			else
			{
				switch (num)
				{
				case 41:
					this.legs = 97;
					this.wearsRobe = true;
					break;
				case 42:
					this.legs = 90;
					this.wearsRobe = true;
					break;
				default:
					switch (num)
					{
					case 58:
						this.legs = 91;
						this.wearsRobe = true;
						break;
					case 59:
						this.legs = 92;
						this.wearsRobe = true;
						break;
					case 60:
						this.legs = 93;
						this.wearsRobe = true;
						break;
					case 61:
						this.legs = 94;
						this.wearsRobe = true;
						break;
					case 62:
						this.legs = 95;
						this.wearsRobe = true;
						break;
					case 63:
						this.legs = 96;
						this.wearsRobe = true;
						break;
					default:
						switch (num)
						{
						case 165:
							this.legs = 99;
							this.wearsRobe = true;
							break;
						case 166:
							this.legs = 100;
							this.wearsRobe = true;
							break;
						case 167:
							if (this.male)
							{
								this.legs = 101;
							}
							else
							{
								this.legs = 102;
							}
							this.wearsRobe = true;
							break;
						}
						break;
					}
					break;
				}
			}
			if (Main.myPlayer == this.whoAmi)
			{
				bool flag = false;
				if (this.wings == 3 || (this.wings >= 16 && this.wings <= 19))
				{
					flag = true;
				}
				if (this.wingsLogic == 3 || (this.wingsLogic >= 16 && this.wingsLogic <= 19))
				{
					flag = true;
				}
				else
				{
					if (this.head == 45 || (this.head >= 106 && this.head <= 111))
					{
						flag = true;
					}
					else
					{
						if (this.body == 26 || (this.body >= 68 && this.body <= 74))
						{
							flag = true;
						}
						else
						{
							if (this.legs == 25 || (this.legs >= 57 && this.legs <= 63))
							{
								flag = true;
							}
						}
					}
				}
				if (flag)
				{
					this.velocity.X = this.velocity.X * 0.9f;
					if (this.velocity.Y < 0f)
					{
						this.velocity.Y = this.velocity.Y + 0.2f;
					}
					this.jump = 0;
					this.statDefense = -1000;
					this.AddBuff(23, 2, false);
					this.AddBuff(80, 2, false);
					this.AddBuff(67, 2, false);
					this.AddBuff(32, 2, false);
					this.AddBuff(31, 2, false);
					this.AddBuff(33, 2, false);
				}
			}
			if (this.body == 93)
			{
				this.shield = 0;
				this.handoff = 0;
			}
			if (this.legs == 67)
			{
				this.shoe = 0;
			}
			if (this.wereWolf)
			{
				this.legs = 20;
				this.body = 21;
				this.head = 38;
			}
			if (this.merman)
			{
				this.head = 39;
				this.legs = 21;
				this.body = 22;
				this.wings = 0;
			}
			this.socialShadow = false;
			this.socialGhost = false;
			if (this.head == 101 && this.body == 66 && this.legs == 55)
			{
				this.socialGhost = true;
			}
			if (this.head == 156 && this.body == 66 && this.legs == 55)
			{
				this.socialGhost = true;
			}
			if (this.head == 99 && this.body == 65 && this.legs == 54)
			{
				this.turtleArmor = true;
			}
			if (this.head == 162 && this.body == 170 && this.legs == 105)
			{
				this.spiderArmor = true;
			}
			if ((this.head == 75 || this.head == 7) && this.body == 7 && this.legs == 7)
			{
				this.boneArmor = true;
			}
			if (this.wings > 0)
			{
				this.back = -1;
				this.front = -1;
			}
			if (this.head > 0 && this.face != 7)
			{
				this.face = -1;
			}
			if (this.frozen)
			{
				return;
			}
			if (Main.gamePaused && !Main.gameMenu)
			{
				return;
			}
			if (((this.body == 68 && this.legs == 57 && this.head == 106) || (this.body == 74 && this.legs == 63 && this.head == 106)) && Main.rand.Next(10) == 0)
			{
				int num2 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, this.position.Y - 2f - this.velocity.Y * 2f), this.width, this.height, 43, 0f, 0f, 100, new Color(255, 0, 255), 0.3f);
				Main.dust[num2].fadeIn = 0.8f;
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 2f;
			}
			if (this.head == 5 && this.body == 5 && this.legs == 5)
			{
				this.socialShadow = true;
			}
			if (this.head == 5 && this.body == 5 && this.legs == 5 && Main.rand.Next(10) == 0)
			{
				Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 14, 0f, 0f, 200, default(Color), 1.2f);
			}
			if (this.head == 45 && this.body == 26 && this.legs == 25 && Main.rand.Next(12) == 0)
			{
				Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 186, 0f, 0f, 160, default(Color), 1.4f);
			}
			if (this.head == 76 && this.body == 49 && this.legs == 45)
			{
				this.socialShadow = true;
			}
			if (this.head == 74 && this.body == 48 && this.legs == 44)
			{
				this.socialShadow = true;
			}
			if (this.head == 74 && this.body == 48 && this.legs == 44 && Main.rand.Next(10) == 0)
			{
				Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 14, 0f, 0f, 200, default(Color), 1.2f);
			}
			if (this.head == 57 && this.body == 37 && this.legs == 35)
			{
				int maxValue = 10;
				if (Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y) > 1f)
				{
					maxValue = 2;
				}
				if (Main.rand.Next(maxValue) == 0)
				{
					int num3 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 115, 0f, 0f, 140, default(Color), 0.75f);
					Main.dust[num3].noGravity = true;
					Main.dust[num3].fadeIn = 1.5f;
					Main.dust[num3].velocity *= 0.3f;
					Main.dust[num3].velocity += this.velocity * 0.2f;
				}
			}
			if (this.head == 6 && this.body == 6 && this.legs == 6 && Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y) > 1f && !this.rocketFrame)
			{
				for (int k = 0; k < 2; k++)
				{
					int num4 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, this.position.Y - 2f - this.velocity.Y * 2f), this.width, this.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num4].noGravity = true;
					Main.dust[num4].noLight = true;
					Dust expr_DE1_cp_0 = Main.dust[num4];
					expr_DE1_cp_0.velocity.X = expr_DE1_cp_0.velocity.X - this.velocity.X * 0.5f;
					Dust expr_E0B_cp_0 = Main.dust[num4];
					expr_E0B_cp_0.velocity.Y = expr_E0B_cp_0.velocity.Y - this.velocity.Y * 0.5f;
				}
			}
			if (this.head == 8 && this.body == 8 && this.legs == 8 && Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y) > 1f)
			{
				int num5 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, this.position.Y - 2f - this.velocity.Y * 2f), this.width, this.height, 40, 0f, 0f, 50, default(Color), 1.4f);
				Main.dust[num5].noGravity = true;
				Main.dust[num5].velocity.X = this.velocity.X * 0.25f;
				Main.dust[num5].velocity.Y = this.velocity.Y * 0.25f;
			}
			if (this.head == 9 && this.body == 9 && this.legs == 9 && Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y) > 1f && !this.rocketFrame)
			{
				for (int l = 0; l < 2; l++)
				{
					int num6 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, this.position.Y - 2f - this.velocity.Y * 2f), this.width, this.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num6].noGravity = true;
					Main.dust[num6].noLight = true;
					Dust expr_1050_cp_0 = Main.dust[num6];
					expr_1050_cp_0.velocity.X = expr_1050_cp_0.velocity.X - this.velocity.X * 0.5f;
					Dust expr_107A_cp_0 = Main.dust[num6];
					expr_107A_cp_0.velocity.Y = expr_107A_cp_0.velocity.Y - this.velocity.Y * 0.5f;
				}
			}
			if (this.body == 18 && this.legs == 17 && (this.head == 32 || this.head == 33 || this.head == 34) && Main.rand.Next(10) == 0)
			{
				int num7 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, this.position.Y - 2f - this.velocity.Y * 2f), this.width, this.height, 43, 0f, 0f, 100, default(Color), 0.3f);
				Main.dust[num7].fadeIn = 0.8f;
				Main.dust[num7].velocity *= 0f;
			}
			if (this.body == 24 && this.legs == 23 && (this.head == 42 || this.head == 43 || this.head == 41) && this.velocity.X != 0f && this.velocity.Y != 0f && Main.rand.Next(10) == 0)
			{
				int num8 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, this.position.Y - 2f - this.velocity.Y * 2f), this.width, this.height, 43, 0f, 0f, 100, default(Color), 0.3f);
				Main.dust[num8].fadeIn = 0.8f;
				Main.dust[num8].velocity *= 0f;
			}
			if (this.body == 36 && this.head == 56 && this.velocity.X != 0f && this.velocity.Y == 0f)
			{
				for (int m = 0; m < 2; m++)
				{
					int num9 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)((this.gravDir == 1f) ? (this.height - 2) : -4)), this.width, 6, 106, 0f, 0f, 100, default(Color), 0.1f);
					Main.dust[num9].fadeIn = 1f;
					Main.dust[num9].noGravity = true;
					Main.dust[num9].velocity *= 0.2f;
				}
			}
			if (this.body == 27 && this.head == 46 && this.legs == 26)
			{
				this.frostArmor = true;
				if (this.velocity.X != 0f && this.velocity.Y == 0f && this.miscCounter % 2 == 0)
				{
					for (int n = 0; n < 2; n++)
					{
						int num10;
						if (n == 0)
						{
							num10 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)this.height + this.gfxOffY), this.width / 2, 6, 76, 0f, 0f, 0, default(Color), 1.35f);
						}
						else
						{
							num10 = Dust.NewDust(new Vector2(this.position.X + (float)(this.width / 2), this.position.Y + (float)this.height + this.gfxOffY), this.width / 2, 6, 76, 0f, 0f, 0, default(Color), 1.35f);
						}
						Main.dust[num10].scale *= 1f + (float)Main.rand.Next(20, 40) * 0.01f;
						Main.dust[num10].noGravity = true;
						Main.dust[num10].noLight = true;
						Main.dust[num10].velocity *= 0.001f;
						Dust expr_1545_cp_0 = Main.dust[num10];
						expr_1545_cp_0.velocity.Y = expr_1545_cp_0.velocity.Y - 0.003f;
					}
				}
			}
			this.bodyFrame.Width = 40;
			this.bodyFrame.Height = 56;
			this.legFrame.Width = 40;
			this.legFrame.Height = 56;
			this.bodyFrame.X = 0;
			this.legFrame.X = 0;
			if (this.itemAnimation > 0 && this.inventory[this.selectedItem].useStyle != 10)
			{
				if (this.inventory[this.selectedItem].useStyle == 1 || this.inventory[this.selectedItem].type == 0)
				{
					if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.333)
					{
						this.bodyFrame.Y = this.bodyFrame.Height * 3;
					}
					else
					{
						if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.666)
						{
							this.bodyFrame.Y = this.bodyFrame.Height * 2;
						}
						else
						{
							this.bodyFrame.Y = this.bodyFrame.Height;
						}
					}
				}
				else
				{
					if (this.inventory[this.selectedItem].useStyle == 2)
					{
						if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.5)
						{
							this.bodyFrame.Y = this.bodyFrame.Height * 3;
						}
						else
						{
							this.bodyFrame.Y = this.bodyFrame.Height * 2;
						}
					}
					else
					{
						if (this.inventory[this.selectedItem].useStyle == 3)
						{
							if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.666)
							{
								this.bodyFrame.Y = this.bodyFrame.Height * 3;
							}
							else
							{
								this.bodyFrame.Y = this.bodyFrame.Height * 3;
							}
						}
						else
						{
							if (this.inventory[this.selectedItem].useStyle == 4)
							{
								this.bodyFrame.Y = this.bodyFrame.Height * 2;
							}
							else
							{
								if (this.inventory[this.selectedItem].useStyle == 5)
								{
									if (this.inventory[this.selectedItem].type == 281 || this.inventory[this.selectedItem].type == 986)
									{
										this.bodyFrame.Y = this.bodyFrame.Height * 2;
									}
									else
									{
										float num11 = this.itemRotation * (float)this.direction;
										this.bodyFrame.Y = this.bodyFrame.Height * 3;
										if ((double)num11 < -0.75)
										{
											this.bodyFrame.Y = this.bodyFrame.Height * 2;
											if (this.gravDir == -1f)
											{
												this.bodyFrame.Y = this.bodyFrame.Height * 4;
											}
										}
										if ((double)num11 > 0.6)
										{
											this.bodyFrame.Y = this.bodyFrame.Height * 4;
											if (this.gravDir == -1f)
											{
												this.bodyFrame.Y = this.bodyFrame.Height * 2;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (this.mount.Active)
				{
					this.bodyFrameCounter = 0.0;
					this.bodyFrame.Y = this.bodyFrame.Height * this.mount.BodyFrame;
				}
				else
				{
					if (this.pulley)
					{
						if (this.pulleyDir == 2)
						{
							this.bodyFrame.Y = this.bodyFrame.Height;
						}
						else
						{
							this.bodyFrame.Y = this.bodyFrame.Height * 2;
						}
					}
					else
					{
						if (this.inventory[this.selectedItem].holdStyle == 1 && (!this.wet || !this.inventory[this.selectedItem].noWet))
						{
							this.bodyFrame.Y = this.bodyFrame.Height * 3;
						}
						else
						{
							if (this.inventory[this.selectedItem].holdStyle == 2 && (!this.wet || !this.inventory[this.selectedItem].noWet))
							{
								this.bodyFrame.Y = this.bodyFrame.Height * 2;
							}
							else
							{
								if (this.inventory[this.selectedItem].holdStyle == 3)
								{
									this.bodyFrame.Y = this.bodyFrame.Height * 3;
								}
								else
								{
									if (this.grappling[0] >= 0)
									{
										this.sandStorm = false;
										this.dJumpEffect = false;
										this.dJumpEffect2 = false;
										this.dJumpEffect3 = false;
										Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
										float num12 = 0f;
										float num13 = 0f;
										for (int num14 = 0; num14 < this.grapCount; num14++)
										{
											num12 += Main.projectile[this.grappling[num14]].position.X + (float)(Main.projectile[this.grappling[num14]].width / 2);
											num13 += Main.projectile[this.grappling[num14]].position.Y + (float)(Main.projectile[this.grappling[num14]].height / 2);
										}
										num12 /= (float)this.grapCount;
										num13 /= (float)this.grapCount;
										num12 -= vector.X;
										num13 -= vector.Y;
										if (num13 < 0f && Math.Abs(num13) > Math.Abs(num12))
										{
											this.bodyFrame.Y = this.bodyFrame.Height * 2;
											if (this.gravDir == -1f)
											{
												this.bodyFrame.Y = this.bodyFrame.Height * 4;
											}
										}
										else
										{
											if (num13 > 0f && Math.Abs(num13) > Math.Abs(num12))
											{
												this.bodyFrame.Y = this.bodyFrame.Height * 4;
												if (this.gravDir == -1f)
												{
													this.bodyFrame.Y = this.bodyFrame.Height * 2;
												}
											}
											else
											{
												this.bodyFrame.Y = this.bodyFrame.Height * 3;
											}
										}
									}
									else
									{
										if (this.swimTime > 0)
										{
											if (this.swimTime > 20)
											{
												this.bodyFrame.Y = 0;
											}
											else
											{
												if (this.swimTime > 10)
												{
													this.bodyFrame.Y = this.bodyFrame.Height * 5;
												}
												else
												{
													this.bodyFrame.Y = 0;
												}
											}
										}
										else
										{
											if (this.velocity.Y != 0f)
											{
												if (this.sliding)
												{
													this.bodyFrame.Y = this.bodyFrame.Height * 3;
												}
												else
												{
													if (this.sandStorm || this.carpetFrame >= 0)
													{
														this.bodyFrame.Y = this.bodyFrame.Height * 6;
													}
													else
													{
														if (this.wings > 0)
														{
															if (this.wings == 22)
															{
																this.bodyFrame.Y = 0;
															}
															else
															{
																if (this.velocity.Y > 0f)
																{
																	if (this.controlJump)
																	{
																		this.bodyFrame.Y = this.bodyFrame.Height * 6;
																	}
																	else
																	{
																		this.bodyFrame.Y = this.bodyFrame.Height * 5;
																	}
																}
																else
																{
																	this.bodyFrame.Y = this.bodyFrame.Height * 6;
																}
															}
														}
														else
														{
															this.bodyFrame.Y = this.bodyFrame.Height * 5;
														}
													}
												}
												this.bodyFrameCounter = 0.0;
											}
											else
											{
												if (this.velocity.X != 0f)
												{
													this.bodyFrameCounter += (double)Math.Abs(this.velocity.X) * 1.5;
													this.bodyFrame.Y = this.legFrame.Y;
												}
												else
												{
													this.bodyFrameCounter = 0.0;
													this.bodyFrame.Y = 0;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (this.mount.Active)
			{
				this.legFrameCounter = 0.0;
				this.legFrame.Y = this.legFrame.Height * 6;
				if (this.velocity.Y != 0f)
				{
					if (this.mount.FlyTime > 0 && this.jump == 0 && this.controlJump && this.mount.Type != 5)
					{
						if (this.mount.Type == 0)
						{
							if (this.direction > 0)
							{
								if (Main.rand.Next(4) == 0)
								{
									int num15 = Dust.NewDust(new Vector2(this.Center().X - 22f, this.position.Y + (float)this.height - 6f), 20, 10, 64, this.velocity.X * 0.25f, this.velocity.Y * 0.25f, 255, default(Color), 1f);
									Main.dust[num15].velocity *= 0.1f;
									Main.dust[num15].noLight = true;
								}
								if (Main.rand.Next(4) == 0)
								{
									int num16 = Dust.NewDust(new Vector2(this.Center().X + 12f, this.position.Y + (float)this.height - 6f), 20, 10, 64, this.velocity.X * 0.25f, this.velocity.Y * 0.25f, 255, default(Color), 1f);
									Main.dust[num16].velocity *= 0.1f;
									Main.dust[num16].noLight = true;
								}
							}
							else
							{
								if (Main.rand.Next(4) == 0)
								{
									int num17 = Dust.NewDust(new Vector2(this.Center().X - 32f, this.position.Y + (float)this.height - 6f), 20, 10, 64, this.velocity.X * 0.25f, this.velocity.Y * 0.25f, 255, default(Color), 1f);
									Main.dust[num17].velocity *= 0.1f;
									Main.dust[num17].noLight = true;
								}
								if (Main.rand.Next(4) == 0)
								{
									int num18 = Dust.NewDust(new Vector2(this.Center().X + 2f, this.position.Y + (float)this.height - 6f), 20, 10, 64, this.velocity.X * 0.25f, this.velocity.Y * 0.25f, 255, default(Color), 1f);
									Main.dust[num18].velocity *= 0.1f;
									Main.dust[num18].noLight = true;
								}
							}
						}
						this.mount.UpdateFrame(3, this.velocity);
					}
					else
					{
						if (this.wet)
						{
							this.mount.UpdateFrame(4, this.velocity);
						}
						else
						{
							this.mount.UpdateFrame(2, this.velocity);
						}
					}
				}
				else
				{
					if (this.velocity.X == 0f)
					{
						this.mount.UpdateFrame(0, this.velocity);
					}
					else
					{
						this.mount.UpdateFrame(1, this.velocity);
					}
				}
			}
			else
			{
				if (this.swimTime > 0)
				{
					this.legFrameCounter += 2.0;
					while (this.legFrameCounter > 8.0)
					{
						this.legFrameCounter -= 8.0;
						this.legFrame.Y = this.legFrame.Y + this.legFrame.Height;
					}
					if (this.legFrame.Y < this.legFrame.Height * 7)
					{
						this.legFrame.Y = this.legFrame.Height * 19;
					}
					else
					{
						if (this.legFrame.Y > this.legFrame.Height * 19)
						{
							this.legFrame.Y = this.legFrame.Height * 7;
						}
					}
				}
				else
				{
					if (this.velocity.Y != 0f || this.grappling[0] > -1)
					{
						this.legFrameCounter = 0.0;
						this.legFrame.Y = this.legFrame.Height * 5;
						if (this.wings == 22)
						{
							this.legFrame.Y = 0;
						}
					}
					else
					{
						if (this.velocity.X != 0f)
						{
							if ((this.slippy || this.slippy2) && !this.controlLeft && !this.controlRight)
							{
								this.legFrameCounter = 0.0;
								this.legFrame.Y = 0;
							}
							else
							{
								this.legFrameCounter += (double)Math.Abs(this.velocity.X) * 1.3;
								while (this.legFrameCounter > 8.0)
								{
									this.legFrameCounter -= 8.0;
									this.legFrame.Y = this.legFrame.Y + this.legFrame.Height;
								}
								if (this.legFrame.Y < this.legFrame.Height * 7)
								{
									this.legFrame.Y = this.legFrame.Height * 19;
								}
								else
								{
									if (this.legFrame.Y > this.legFrame.Height * 19)
									{
										this.legFrame.Y = this.legFrame.Height * 7;
									}
								}
							}
						}
						else
						{
							this.legFrameCounter = 0.0;
							this.legFrame.Y = 0;
						}
					}
				}
			}
			if (this.carpetFrame >= 0)
			{
				this.legFrameCounter = 0.0;
				this.legFrame.Y = 0;
			}
			if (this.sandStorm)
			{
				if (this.miscCounter % 4 == 0 && this.itemAnimation == 0)
				{
					this.ChangeDir(this.direction * -1);
					if (this.inventory[this.selectedItem].holdStyle == 2)
					{
						if (this.inventory[this.selectedItem].type == 946)
						{
							this.itemLocation.X = this.position.X + (float)this.width * 0.5f - (float)(16 * this.direction);
						}
						if (this.inventory[this.selectedItem].type == 186)
						{
							this.itemLocation.X = this.position.X + (float)this.width * 0.5f + (float)(6 * this.direction);
							this.itemRotation = 0.79f * (float)(-(float)this.direction);
						}
					}
				}
				this.legFrameCounter = 0.0;
				this.legFrame.Y = 0;
			}
		}
		public void Teleport(Vector2 newPos, int Style = 0)
		{
			try
			{
				this.grappling[0] = -1;
				this.grapCount = 0;
				for (int i = 0; i < 1000; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].owner == this.whoAmi && Main.projectile[i].aiStyle == 7)
					{
						Main.projectile[i].Kill();
					}
				}
				Main.TeleportEffect(this.getRect(), Style);
				this.position = newPos;
				this.fallStart = (int)(this.position.Y / 16f);
				if (this.whoAmi == Main.myPlayer)
				{
					Main.BlackFadeIn = 255;
					Lighting.BlackOut();
					Main.screenLastPosition = Main.screenPosition;
					Main.screenPosition.X = this.position.X + (float)(this.width / 2) - (float)(Main.screenWidth / 2);
					Main.screenPosition.Y = this.position.Y + (float)(this.height / 2) - (float)(Main.screenHeight / 2);
					if (Main.mapTime < 5)
					{
						Main.mapTime = 5;
					}
					Main.quickBG = 10;
					Main.maxQ = true;
					Main.renderNow = true;
				}
				Main.TeleportEffect(this.getRect(), Style);
				this.teleportTime = 1f;
				this.teleportStyle = Style;
			}
			catch
			{
			}
		}
		public void Spawn()
		{
			if (this.whoAmi == Main.myPlayer)
			{
				if (Main.mapTime < 5)
				{
					Main.mapTime = 5;
				}
				Main.quickBG = 10;
				this.FindSpawn();
				if (!Player.CheckSpawn(this.SpawnX, this.SpawnY))
				{
					this.SpawnX = -1;
					this.SpawnY = -1;
				}
				Main.maxQ = true;
			}
			if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(12, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				Main.gameMenu = false;
			}
			this.headPosition = Vector2.Zero;
			this.bodyPosition = Vector2.Zero;
			this.legPosition = Vector2.Zero;
			this.headRotation = 0f;
			this.bodyRotation = 0f;
			this.legRotation = 0f;
			this.lavaTime = this.lavaMax;
			if (this.statLife <= 0)
			{
				this.statLife = 100;
				this.breath = this.breathMax;
				if (this.spawnMax)
				{
					this.statLife = this.statLifeMax2;
					this.statMana = this.statManaMax2;
				}
			}
			this.immune = true;
			this.dead = false;
			this.immuneTime = 0;
			this.active = true;
			if (this.SpawnX >= 0 && this.SpawnY >= 0)
			{
				this.position.X = (float)(this.SpawnX * 16 + 8 - this.width / 2);
				this.position.Y = (float)(this.SpawnY * 16 - this.height);
			}
			else
			{
				this.position.X = (float)(Main.spawnTileX * 16 + 8 - this.width / 2);
				this.position.Y = (float)(Main.spawnTileY * 16 - this.height);
				for (int i = Main.spawnTileX - 1; i < Main.spawnTileX + 2; i++)
				{
					for (int j = Main.spawnTileY - 3; j < Main.spawnTileY; j++)
					{
						if (Main.tileSolid[(int)Main.tile[i, j].type] && !Main.tileSolidTop[(int)Main.tile[i, j].type])
						{
							WorldGen.KillTile(i, j, false, false, false);
						}
						if (Main.tile[i, j].liquid > 0)
						{
							Main.tile[i, j].lava(false);
							Main.tile[i, j].liquid = 0;
							WorldGen.SquareTileFrame(i, j, true);
						}
					}
				}
			}
			this.wet = false;
			this.wetCount = 0;
			this.lavaWet = false;
			this.fallStart = (int)(this.position.Y / 16f);
			this.fallStart2 = this.fallStart;
			this.velocity.X = 0f;
			this.velocity.Y = 0f;
			this.talkNPC = -1;
			Main.npcChatCornerItem = 0;
			if (this.pvpDeath)
			{
				this.pvpDeath = false;
				this.immuneTime = 300;
				this.statLife = this.statLifeMax;
			}
			else
			{
				this.immuneTime = 60;
			}
			if (this.whoAmi == Main.myPlayer)
			{
				Main.BlackFadeIn = 255;
				Main.renderNow = true;
				if (Main.netMode == 1)
				{
					Netplay.newRecent();
				}
				Main.screenPosition.X = this.position.X + (float)(this.width / 2) - (float)(Main.screenWidth / 2);
				Main.screenPosition.Y = this.position.Y + (float)(this.height / 2) - (float)(Main.screenHeight / 2);
			}
		}
		public void ShadowDodge()
		{
			this.immune = true;
			this.immuneTime = 80;
			if (this.longInvince)
			{
				this.immuneTime += 40;
			}
			if (this.whoAmi == Main.myPlayer)
			{
				for (int i = 0; i < 22; i++)
				{
					if (this.buffTime[i] > 0 && this.buffType[i] == 59)
					{
						this.DelBuff(i);
					}
				}
				NetMessage.SendData(62, -1, -1, "", this.whoAmi, 2f, 0f, 0f, 0);
			}
		}
		public void NinjaDodge()
		{
			this.immune = true;
			this.immuneTime = 80;
			if (this.longInvince)
			{
				this.immuneTime += 40;
			}
			for (int i = 0; i < 100; i++)
			{
				int num = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 31, 0f, 0f, 100, default(Color), 2f);
				Dust expr_82_cp_0 = Main.dust[num];
				expr_82_cp_0.position.X = expr_82_cp_0.position.X + (float)Main.rand.Next(-20, 21);
				Dust expr_A9_cp_0 = Main.dust[num];
				expr_A9_cp_0.position.Y = expr_A9_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
				Main.dust[num].velocity *= 0.4f;
				Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].noGravity = true;
				}
			}
			int num2 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[num2].scale = 1.5f;
			Main.gore[num2].velocity.X = (float)Main.rand.Next(-50, 51) * 0.01f;
			Main.gore[num2].velocity.Y = (float)Main.rand.Next(-50, 51) * 0.01f;
			Main.gore[num2].velocity *= 0.4f;
			num2 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[num2].scale = 1.5f;
			Main.gore[num2].velocity.X = 1.5f + (float)Main.rand.Next(-50, 51) * 0.01f;
			Main.gore[num2].velocity.Y = 1.5f + (float)Main.rand.Next(-50, 51) * 0.01f;
			Main.gore[num2].velocity *= 0.4f;
			num2 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[num2].scale = 1.5f;
			Main.gore[num2].velocity.X = -1.5f - (float)Main.rand.Next(-50, 51) * 0.01f;
			Main.gore[num2].velocity.Y = 1.5f + (float)Main.rand.Next(-50, 51) * 0.01f;
			Main.gore[num2].velocity *= 0.4f;
			num2 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[num2].scale = 1.5f;
			Main.gore[num2].velocity.X = 1.5f + (float)Main.rand.Next(-50, 51) * 0.01f;
			Main.gore[num2].velocity.Y = -1.5f - (float)Main.rand.Next(-50, 51) * 0.01f;
			Main.gore[num2].velocity *= 0.4f;
			num2 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[num2].scale = 1.5f;
			Main.gore[num2].velocity.X = -1.5f - (float)Main.rand.Next(-50, 51) * 0.01f;
			Main.gore[num2].velocity.Y = -1.5f - (float)Main.rand.Next(-50, 51) * 0.01f;
			Main.gore[num2].velocity *= 0.4f;
			if (this.whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(62, -1, -1, "", this.whoAmi, 1f, 0f, 0f, 0);
			}
		}
		public double Hurt(int Damage, int hitDirection, bool pvp = false, bool quiet = false, string deathText = " was slain...", bool Crit = false)
		{
			if (this.immune)
			{
				return 0.0;
			}
			if (this.whoAmi == Main.myPlayer && this.blackBelt && Main.rand.Next(10) == 0)
			{
				this.NinjaDodge();
				return 0.0;
			}
			if (this.whoAmi == Main.myPlayer && this.shadowDodge)
			{
				this.ShadowDodge();
				return 0.0;
			}
			if (this.whoAmi == Main.myPlayer && this.panic)
			{
				this.AddBuff(63, 300, true);
			}
			int num = Damage;
			double num2 = Main.CalculateDamage(num, this.statDefense);
			if (Crit)
			{
				num *= 2;
			}
			if (num2 >= 1.0)
			{
				num2 = (double)((int)((double)(1f - this.endurance) * num2));
				if (num2 < 1.0)
				{
					num2 = 1.0;
				}
				if (this.beetleDefense && this.beetleOrbs > 0)
				{
					float num3 = 0.15f * (float)this.beetleOrbs;
					num2 = (double)((int)((double)(1f - num3) * num2));
					this.beetleOrbs--;
					for (int i = 0; i < 22; i++)
					{
						if (this.buffType[i] >= 95 && this.buffType[i] <= 97)
						{
							this.DelBuff(i);
						}
					}
					if (this.beetleOrbs > 0)
					{
						this.AddBuff(95 + this.beetleOrbs - 1, 5, false);
					}
					this.beetleCounter = 0f;
					if (num2 < 1.0)
					{
						num2 = 1.0;
					}
				}
				if (this.magicCuffs)
				{
					int num4 = num;
					this.statMana += num4;
					if (this.statMana > this.statManaMax2)
					{
						this.statMana = this.statManaMax2;
					}
					this.ManaEffect(num4);
				}
				if (this.paladinBuff)
				{
					int damage = (int)(num2 * 0.25);
					num2 = (double)((int)(num2 * 0.75));
					if (this.whoAmi != Main.myPlayer && Main.player[Main.myPlayer].paladinGive)
					{
						int myPlayer = Main.myPlayer;
						if (Main.player[myPlayer].team == this.team && this.team != 0)
						{
							float num5 = this.position.X - Main.player[myPlayer].position.X;
							float num6 = this.position.Y - Main.player[myPlayer].position.Y;
							float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
							if (num7 < 800f)
							{
								Main.player[myPlayer].Hurt(damage, 0, false, false, "", false);
							}
						}
					}
				}
				if (Main.netMode == 1 && this.whoAmi == Main.myPlayer && !quiet)
				{
					int number = 0;
					if (Crit)
					{
						number = 1;
					}
					int num8 = 0;
					if (pvp)
					{
						num8 = 1;
					}
					NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
					NetMessage.SendData(16, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
					NetMessage.SendData(26, -1, -1, "", this.whoAmi, (float)hitDirection, (float)Damage, (float)num8, number);
				}
				CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(255, 80, 90, 255), string.Concat((int)num2), Crit, false);
				this.statLife -= (int)num2;
				this.immune = true;
				if (num2 == 1.0)
				{
					this.immuneTime = 20;
					if (this.longInvince)
					{
						this.immuneTime += 20;
					}
				}
				else
				{
					this.immuneTime = 40;
					if (this.longInvince)
					{
						this.immuneTime += 40;
					}
				}
				this.lifeRegenTime = 0;
				if (pvp)
				{
					this.immuneTime = 8;
				}
				if (this.whoAmi == Main.myPlayer)
				{
					if (this.starCloak)
					{
						for (int j = 0; j < 3; j++)
						{
							float x = this.position.X + (float)Main.rand.Next(-400, 400);
							float y = this.position.Y - (float)Main.rand.Next(500, 800);
							Vector2 vector = new Vector2(x, y);
							float num9 = this.position.X + (float)(this.width / 2) - vector.X;
							float num10 = this.position.Y + (float)(this.height / 2) - vector.Y;
							num9 += (float)Main.rand.Next(-100, 101);
							int num11 = 23;
							float num12 = (float)Math.Sqrt((double)(num9 * num9 + num10 * num10));
							num12 = (float)num11 / num12;
							num9 *= num12;
							num10 *= num12;
							int num13 = Projectile.NewProjectile(x, y, num9, num10, 92, 30, 5f, this.whoAmi, 0f, 0f);
							Main.projectile[num13].ai[1] = this.position.Y;
						}
					}
					if (this.bee)
					{
						int num14 = 1;
						if (Main.rand.Next(3) == 0)
						{
							num14++;
						}
						if (Main.rand.Next(3) == 0)
						{
							num14++;
						}
						for (int k = 0; k < num14; k++)
						{
							float speedX = (float)Main.rand.Next(-35, 36) * 0.02f;
							float speedY = (float)Main.rand.Next(-35, 36) * 0.02f;
							Projectile.NewProjectile(this.position.X, this.position.Y, speedX, speedY, 181, 7, 0f, Main.myPlayer, 0f, 0f);
						}
					}
				}
				if (!this.noKnockback && hitDirection != 0 && (!this.mount.Active || this.mount.Type != 6))
				{
					this.velocity.X = 4.5f * (float)hitDirection;
					this.velocity.Y = -3.5f;
				}
				if (this.frostArmor)
				{
					Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 27);
				}
				else
				{
					if (this.wereWolf)
					{
						Main.PlaySound(3, (int)this.position.X, (int)this.position.Y, 6);
					}
					else
					{
						if (this.boneArmor)
						{
							Main.PlaySound(3, (int)this.position.X, (int)this.position.Y, 2);
						}
						else
						{
							if (!this.male)
							{
								Main.PlaySound(20, (int)this.position.X, (int)this.position.Y, 1);
							}
							else
							{
								Main.PlaySound(1, (int)this.position.X, (int)this.position.Y, 1);
							}
						}
					}
				}
				if (this.statLife > 0)
				{
					int num15 = 0;
					while ((double)num15 < num2 / (double)this.statLifeMax2 * 100.0)
					{
						if (this.frostArmor)
						{
							Dust.NewDust(this.position, this.width, this.height, 135, (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
						}
						else
						{
							if (this.boneArmor)
							{
								Dust.NewDust(this.position, this.width, this.height, 26, (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
							}
							else
							{
								Dust.NewDust(this.position, this.width, this.height, 5, (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
							}
						}
						num15++;
					}
				}
				else
				{
					this.statLife = 0;
					if (this.whoAmi == Main.myPlayer)
					{
						this.KillMe(num2, hitDirection, pvp, deathText);
					}
				}
			}
			if (pvp)
			{
				num2 = Main.CalculateDamage(num, this.statDefense);
			}
			return num2;
		}
		public void KillMeForGood()
		{
			if (File.Exists(Main.playerPathName))
			{
				File.Delete(Main.playerPathName);
			}
			if (File.Exists(Main.playerPathName + ".bak"))
			{
				File.Delete(Main.playerPathName + ".bak");
			}
			if (File.Exists(Main.playerPathName + ".dat"))
			{
				File.Delete(Main.playerPathName + ".dat");
			}
			Main.playerPathName = "";
		}
		public void KillMe(double dmg, int hitDirection, bool pvp = false, string deathText = " was slain...")
		{
			if (this.dead)
			{
				return;
			}
			if (pvp)
			{
				this.pvpDeath = true;
			}
			if (this.difficulty == 0)
			{
				if (Main.netMode != 1)
				{
					float num = (float)Main.rand.Next(-35, 36) * 0.1f;
					while (num < 2f && num > -2f)
					{
						num += (float)Main.rand.Next(-30, 31) * 0.1f;
					}
					int num2 = Main.rand.Next(6);
					if (num2 == 0)
					{
						num2 = 43;
					}
					else
					{
						num2 = 200 + num2;
					}
					int num3 = Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), (float)Main.rand.Next(10, 30) * 0.1f * (float)hitDirection + num, (float)Main.rand.Next(-40, -20) * 0.1f, num2, 0, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[num3].miscText = this.name + deathText;
				}
				if (Main.myPlayer == this.whoAmi)
				{
					for (int i = 0; i < 59; i++)
					{
						if (this.inventory[i].stack > 0 && this.inventory[i].type >= 1522 && this.inventory[i].type <= 1527)
						{
							int num4 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.inventory[i].type, 1, false, 0, false);
							Main.item[num4].netDefaults(this.inventory[i].netID);
							Main.item[num4].Prefix((int)this.inventory[i].prefix);
							Main.item[num4].stack = this.inventory[i].stack;
							Main.item[num4].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
							Main.item[num4].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
							Main.item[num4].noGrabDelay = 100;
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, "", num4, 0f, 0f, 0f, 0);
							}
							this.inventory[i].SetDefaults(0, false);
						}
					}
					Main.mapFullscreen = false;
				}
			}
			else
			{
				if (Main.netMode != 1)
				{
					float num5 = (float)Main.rand.Next(-35, 36) * 0.1f;
					while (num5 < 2f && num5 > -2f)
					{
						num5 += (float)Main.rand.Next(-30, 31) * 0.1f;
					}
					int num6 = Main.rand.Next(6);
					if (num6 == 0)
					{
						num6 = 43;
					}
					else
					{
						num6 = 200 + num6;
					}
					int num7 = Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.head / 2), (float)Main.rand.Next(10, 30) * 0.1f * (float)hitDirection + num5, (float)Main.rand.Next(-40, -20) * 0.1f, num6, 0, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[num7].miscText = this.name + deathText;
				}
				if (Main.myPlayer == this.whoAmi)
				{
					if (Main.myPlayer == this.whoAmi)
					{
						Main.mapFullscreen = false;
					}
					Main.trashItem.SetDefaults(0, false);
					if (this.difficulty == 1)
					{
						this.DropItems();
					}
					else
					{
						if (this.difficulty == 2)
						{
							this.DropItems();
							this.KillMeForGood();
						}
					}
				}
			}
			Main.PlaySound(5, (int)this.position.X, (int)this.position.Y, 1);
			this.headVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			this.bodyVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			this.legVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			this.headVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			this.bodyVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			this.legVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			for (int j = 0; j < 100; j++)
			{
				if (this.boneArmor)
				{
					Dust.NewDust(this.position, this.width, this.height, 26, (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
				}
				else
				{
					Dust.NewDust(this.position, this.width, this.height, 5, (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
				}
			}
			this.mount.Dismount(this);
			this.dead = true;
			this.respawnTimer = 600;
			bool flag = false;
			if (Main.netMode != 0 && !pvp)
			{
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].active && (Main.npc[k].boss || Main.npc[k].type == 13 || Main.npc[k].type == 14 || Main.npc[k].type == 15) && Math.Abs(this.center().X - Main.npc[k].center().X) + Math.Abs(this.center().Y - Main.npc[k].center().Y) < 4000f)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.respawnTimer += 600;
			}
			this.immuneAlpha = 0;
			this.palladiumRegen = false;
			this.iceBarrier = false;
			this.crystalLeaf = false;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, this.name + deathText, 255, 225f, 25f, 25f, 0);
			}
			else
			{
				if (Main.netMode == 0)
				{
					Main.NewText(this.name + deathText, 225, 25, 25, false);
				}
			}
			if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
			{
				int num8 = 0;
				if (pvp)
				{
					num8 = 1;
				}
				NetMessage.SendData(44, -1, -1, deathText, this.whoAmi, (float)hitDirection, (float)((int)dmg), (float)num8, 0);
			}
			if (!pvp && this.whoAmi == Main.myPlayer && this.difficulty == 0)
			{
				this.DropCoins();
			}
			if (this.whoAmi == Main.myPlayer)
			{
				try
				{
					WorldGen.saveToonWhilePlaying();
				}
				catch
				{
				}
			}
		}
		public bool ItemSpace(Item newItem)
		{
			if (newItem.uniqueStack && this.HasItem(newItem.type))
			{
				return false;
			}
			if (newItem.type == 58 || newItem.type == 184 || newItem.type == 1734 || newItem.type == 1735 || newItem.type == 1867 || newItem.type == 1868)
			{
				return true;
			}
			int num = 50;
			if (newItem.type == 71 || newItem.type == 72 || newItem.type == 73 || newItem.type == 74)
			{
				num = 54;
			}
			for (int i = 0; i < num; i++)
			{
				if (this.inventory[i].type == 0)
				{
					return true;
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (this.inventory[j].type > 0 && this.inventory[j].stack < this.inventory[j].maxStack && newItem.IsTheSameAs(this.inventory[j]))
				{
					return true;
				}
			}
			if (newItem.ammo > 0 && !newItem.notAmmo)
			{
				if (newItem.type != 75 && newItem.type != 169 && newItem.type != 23 && newItem.type != 408 && newItem.type != 370 && newItem.type != 1246)
				{
					for (int k = 54; k < 58; k++)
					{
						if (this.inventory[k].type == 0)
						{
							return true;
						}
					}
				}
				for (int l = 54; l < 58; l++)
				{
					if (this.inventory[l].type > 0 && this.inventory[l].stack < this.inventory[l].maxStack && newItem.IsTheSameAs(this.inventory[l]))
					{
						return true;
					}
				}
			}
			return false;
		}
		public void DoCoins(int i)
		{
			if (this.inventory[i].stack == 100 && (this.inventory[i].type == 71 || this.inventory[i].type == 72 || this.inventory[i].type == 73))
			{
				this.inventory[i].SetDefaults(this.inventory[i].type + 1, false);
				for (int j = 0; j < 54; j++)
				{
					if (this.inventory[j].IsTheSameAs(this.inventory[i]) && j != i && this.inventory[j].type == this.inventory[i].type && this.inventory[j].stack < this.inventory[j].maxStack)
					{
						this.inventory[j].stack++;
						this.inventory[i].SetDefaults(0, false);
						this.inventory[i].active = false;
						this.inventory[i].name = "";
						this.inventory[i].type = 0;
						this.inventory[i].stack = 0;
						this.DoCoins(j);
					}
				}
			}
		}
		public Item FillAmmo(int plr, Item newItem)
		{
			for (int i = 54; i < 58; i++)
			{
				if (this.inventory[i].type > 0 && this.inventory[i].stack < this.inventory[i].maxStack && newItem.IsTheSameAs(this.inventory[i]))
				{
					Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
					if (newItem.stack + this.inventory[i].stack <= this.inventory[i].maxStack)
					{
						this.inventory[i].stack += newItem.stack;
						ItemText.NewText(newItem, newItem.stack, false, false);
						this.DoCoins(i);
						if (plr == Main.myPlayer)
						{
							Recipe.FindRecipes();
						}
						return new Item();
					}
					newItem.stack -= this.inventory[i].maxStack - this.inventory[i].stack;
					ItemText.NewText(newItem, this.inventory[i].maxStack - this.inventory[i].stack, false, false);
					this.inventory[i].stack = this.inventory[i].maxStack;
					this.DoCoins(i);
					if (plr == Main.myPlayer)
					{
						Recipe.FindRecipes();
					}
				}
			}
			if (newItem.type != 169 && newItem.type != 75 && newItem.type != 23 && newItem.type != 408 && newItem.type != 370 && newItem.type != 1246 && !newItem.notAmmo)
			{
				for (int j = 54; j < 58; j++)
				{
					if (this.inventory[j].type == 0)
					{
						this.inventory[j] = newItem;
						ItemText.NewText(newItem, newItem.stack, false, false);
						this.DoCoins(j);
						Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
						if (plr == Main.myPlayer)
						{
							Recipe.FindRecipes();
						}
						return new Item();
					}
				}
			}
			return newItem;
		}
		public Item GetItem(int plr, Item newItem, bool longText = false)
		{
			Item item = newItem;
			int num = 50;
			if (newItem.noGrabDelay > 0)
			{
				return item;
			}
			int num2 = 0;
			if (newItem.uniqueStack && this.HasItem(newItem.type))
			{
				return item;
			}
			if (newItem.type == 71 || newItem.type == 72 || newItem.type == 73 || newItem.type == 74)
			{
				num2 = -4;
				num = 54;
			}
			if (item.ammo > 0 && !item.notAmmo)
			{
				item = this.FillAmmo(plr, item);
				if (item.type == 0 || item.stack == 0)
				{
					return new Item();
				}
			}
			for (int i = num2; i < 50; i++)
			{
				int num3 = i;
				if (num3 < 0)
				{
					num3 = 54 + i;
				}
				if (this.inventory[num3].type > 0 && this.inventory[num3].stack < this.inventory[num3].maxStack && item.IsTheSameAs(this.inventory[num3]))
				{
					Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
					if (item.stack + this.inventory[num3].stack <= this.inventory[num3].maxStack)
					{
						this.inventory[num3].stack += item.stack;
						ItemText.NewText(newItem, item.stack, false, longText);
						this.DoCoins(num3);
						if (plr == Main.myPlayer)
						{
							Recipe.FindRecipes();
						}
						return new Item();
					}
					item.stack -= this.inventory[num3].maxStack - this.inventory[num3].stack;
					ItemText.NewText(newItem, this.inventory[num3].maxStack - this.inventory[num3].stack, false, longText);
					this.inventory[num3].stack = this.inventory[num3].maxStack;
					this.DoCoins(num3);
					if (plr == Main.myPlayer)
					{
						Recipe.FindRecipes();
					}
				}
			}
			if (newItem.type != 71 && newItem.type != 72 && newItem.type != 73 && newItem.type != 74 && newItem.useStyle > 0)
			{
				for (int j = 0; j < 10; j++)
				{
					if (this.inventory[j].type == 0)
					{
						this.inventory[j] = item;
						ItemText.NewText(newItem, newItem.stack, false, longText);
						this.DoCoins(j);
						Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
						if (plr == Main.myPlayer)
						{
							Recipe.FindRecipes();
						}
						return new Item();
					}
				}
			}
			for (int k = num - 1; k >= 0; k--)
			{
				if (this.inventory[k].type == 0)
				{
					this.inventory[k] = item;
					ItemText.NewText(newItem, newItem.stack, false, longText);
					this.DoCoins(k);
					Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
					if (plr == Main.myPlayer)
					{
						Recipe.FindRecipes();
					}
					return new Item();
				}
			}
			return item;
		}
		public void PlaceThing()
		{
			if ((this.inventory[this.selectedItem].type == 1071 || this.inventory[this.selectedItem].type == 1543) && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f + (float)this.blockRange >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f + (float)this.blockRange >= (float)Player.tileTargetY)
			{
				int num = Player.tileTargetX;
				int num2 = Player.tileTargetY;
				if (Main.tile[num, num2] != null && Main.tile[num, num2].active())
				{
					this.showItemIcon = true;
					if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
					{
						int num3 = -1;
						int num4 = -1;
						for (int i = 0; i < 58; i++)
						{
							if (this.inventory[i].stack > 0 && this.inventory[i].paint > 0)
							{
								num3 = (int)this.inventory[i].paint;
								num4 = i;
								break;
							}
						}
						if (num3 > 0 && (int)Main.tile[num, num2].color() != num3 && WorldGen.paintTile(num, num2, (byte)num3, true))
						{
							int num5 = num4;
							this.inventory[num5].stack--;
							if (this.inventory[num5].stack <= 0)
							{
								this.inventory[num5].SetDefaults(0, false);
							}
							this.itemTime = this.inventory[this.selectedItem].useTime;
						}
					}
				}
			}
			if ((this.inventory[this.selectedItem].type == 1072 || this.inventory[this.selectedItem].type == 1544) && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f + (float)this.blockRange >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f + (float)this.blockRange >= (float)Player.tileTargetY)
			{
				int num6 = Player.tileTargetX;
				int num7 = Player.tileTargetY;
				if (Main.tile[num6, num7] != null && Main.tile[num6, num7].wall > 0)
				{
					this.showItemIcon = true;
					if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
					{
						int num8 = -1;
						int num9 = -1;
						for (int j = 0; j < 58; j++)
						{
							if (this.inventory[j].stack > 0 && this.inventory[j].paint > 0)
							{
								num8 = (int)this.inventory[j].paint;
								num9 = j;
								break;
							}
						}
						if (num8 > 0 && (int)Main.tile[num6, num7].wallColor() != num8 && WorldGen.paintWall(num6, num7, (byte)num8, true))
						{
							int num10 = num9;
							this.inventory[num10].stack--;
							if (this.inventory[num10].stack <= 0)
							{
								this.inventory[num10].SetDefaults(0, false);
							}
							this.itemTime = this.inventory[this.selectedItem].useTime;
						}
					}
				}
			}
			if ((this.inventory[this.selectedItem].type == 1100 || this.inventory[this.selectedItem].type == 1545) && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f + (float)this.blockRange >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f + (float)this.blockRange >= (float)Player.tileTargetY)
			{
				int num11 = Player.tileTargetX;
				int num12 = Player.tileTargetY;
				if (Main.tile[num11, num12] != null && ((Main.tile[num11, num12].wallColor() > 0 && Main.tile[num11, num12].wall > 0) || (Main.tile[num11, num12].color() > 0 && Main.tile[num11, num12].active())))
				{
					this.showItemIcon = true;
					if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
					{
						if (Main.tile[num11, num12].color() > 0 && Main.tile[num11, num12].active() && WorldGen.paintTile(num11, num12, 0, true))
						{
							this.itemTime = this.inventory[this.selectedItem].useTime;
						}
						else
						{
							if (Main.tile[num11, num12].wallColor() > 0 && Main.tile[num11, num12].wall > 0 && WorldGen.paintWall(num11, num12, 0, true))
							{
								this.itemTime = this.inventory[this.selectedItem].useTime;
							}
						}
					}
				}
			}
			if ((this.inventory[this.selectedItem].type == 929 || this.inventory[this.selectedItem].type == 1338) && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f + (float)this.blockRange >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f + (float)this.blockRange >= (float)Player.tileTargetY)
			{
				int num13 = Player.tileTargetX;
				int num14 = Player.tileTargetY;
				if (Main.tile[num13, num14].active() && Main.tile[num13, num14].type == 209)
				{
					int num15 = 0;
					if (Main.tile[num13, num14].frameX < 72)
					{
						if (this.inventory[this.selectedItem].type == 929)
						{
							num15 = 1;
						}
					}
					else
					{
						if (Main.tile[num13, num14].frameX < 144 && this.inventory[this.selectedItem].type == 1338)
						{
							num15 = 2;
						}
					}
					if (num15 > 0)
					{
						this.showItemIcon = true;
						if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
						{
							int k = (int)(Main.tile[num13, num14].frameX / 18);
							int num16 = 0;
							int num17 = 0;
							while (k >= 4)
							{
								num16++;
								k -= 4;
							}
							k = num13 - k;
							int l;
							for (l = (int)(Main.tile[num13, num14].frameY / 18); l >= 3; l -= 3)
							{
								num17++;
							}
							l = num14 - l;
							this.itemTime = this.inventory[this.selectedItem].useTime;
							float num18 = 14f;
							float num19 = 0f;
							float num20 = 0f;
							int type = 162;
							if (num15 == 2)
							{
								type = 281;
							}
							int damage = this.inventory[this.selectedItem].damage;
							int num21 = 8;
							if (num17 == 0)
							{
								num19 = 10f;
								num20 = 0f;
							}
							if (num17 == 1)
							{
								num19 = 7.5f;
								num20 = -2.5f;
							}
							if (num17 == 2)
							{
								num19 = 5f;
								num20 = -5f;
							}
							if (num17 == 3)
							{
								num19 = 2.75f;
								num20 = -6f;
							}
							if (num17 == 4)
							{
								num19 = 0f;
								num20 = -10f;
							}
							if (num17 == 5)
							{
								num19 = -2.75f;
								num20 = -6f;
							}
							if (num17 == 6)
							{
								num19 = -5f;
								num20 = -5f;
							}
							if (num17 == 7)
							{
								num19 = -7.5f;
								num20 = -2.5f;
							}
							if (num17 == 8)
							{
								num19 = -10f;
								num20 = 0f;
							}
							Vector2 vector = new Vector2((float)((k + 2) * 16), (float)((l + 2) * 16));
							float num22 = num19;
							float num23 = num20;
							float num24 = (float)Math.Sqrt((double)(num22 * num22 + num23 * num23));
							num24 = num18 / num24;
							num22 *= num24;
							num23 *= num24;
							Projectile.NewProjectile(vector.X, vector.Y, num22, num23, type, damage, (float)num21, Main.myPlayer, 0f, 0f);
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].type >= 1874 && this.inventory[this.selectedItem].type <= 1905 && Main.tile[Player.tileTargetX, Player.tileTargetY].active() && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 171 && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f + (float)this.blockRange >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f + (float)this.blockRange >= (float)Player.tileTargetY && this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
			{
				int num25 = this.inventory[this.selectedItem].type;
				if (num25 >= 1874 && num25 <= 1877)
				{
					num25 -= 1873;
					if (WorldGen.checkXmasTreeDrop(Player.tileTargetX, Player.tileTargetY, 0) != num25)
					{
						this.itemTime = this.inventory[this.selectedItem].useTime;
						WorldGen.dropXmasTree(Player.tileTargetX, Player.tileTargetY, 0);
						WorldGen.setXmasTree(Player.tileTargetX, Player.tileTargetY, 0, num25);
						int num26 = Player.tileTargetX;
						int num27 = Player.tileTargetY;
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX < 10)
						{
							num26 -= (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameX;
							num27 -= (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameY;
						}
						NetMessage.SendTileSquare(-1, num26, num27, 1);
					}
				}
				else
				{
					if (num25 >= 1878 && num25 <= 1883)
					{
						num25 -= 1877;
						if (WorldGen.checkXmasTreeDrop(Player.tileTargetX, Player.tileTargetY, 1) != num25)
						{
							this.itemTime = this.inventory[this.selectedItem].useTime;
							WorldGen.dropXmasTree(Player.tileTargetX, Player.tileTargetY, 1);
							WorldGen.setXmasTree(Player.tileTargetX, Player.tileTargetY, 1, num25);
							int num28 = Player.tileTargetX;
							int num29 = Player.tileTargetY;
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX < 10)
							{
								num28 -= (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameX;
								num29 -= (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameY;
							}
							NetMessage.SendTileSquare(-1, num28, num29, 1);
						}
					}
					else
					{
						if (num25 >= 1884 && num25 <= 1894)
						{
							num25 -= 1883;
							if (WorldGen.checkXmasTreeDrop(Player.tileTargetX, Player.tileTargetY, 2) != num25)
							{
								this.itemTime = this.inventory[this.selectedItem].useTime;
								WorldGen.dropXmasTree(Player.tileTargetX, Player.tileTargetY, 2);
								WorldGen.setXmasTree(Player.tileTargetX, Player.tileTargetY, 2, num25);
								int num30 = Player.tileTargetX;
								int num31 = Player.tileTargetY;
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX < 10)
								{
									num30 -= (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameX;
									num31 -= (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameY;
								}
								NetMessage.SendTileSquare(-1, num30, num31, 1);
							}
						}
						else
						{
							if (num25 >= 1895 && num25 <= 1905)
							{
								num25 -= 1894;
								if (WorldGen.checkXmasTreeDrop(Player.tileTargetX, Player.tileTargetY, 3) != num25)
								{
									this.itemTime = this.inventory[this.selectedItem].useTime;
									WorldGen.dropXmasTree(Player.tileTargetX, Player.tileTargetY, 3);
									WorldGen.setXmasTree(Player.tileTargetX, Player.tileTargetY, 3, num25);
									int num32 = Player.tileTargetX;
									int num33 = Player.tileTargetY;
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX < 10)
									{
										num32 -= (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameX;
										num33 -= (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameY;
									}
									NetMessage.SendTileSquare(-1, num32, num33, 1);
								}
							}
						}
					}
				}
			}
			if ((this.inventory[this.selectedItem].type == 424 || this.inventory[this.selectedItem].type == 1103) && Main.tile[Player.tileTargetX, Player.tileTargetY].active() && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 219)
			{
				if (this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f + (float)this.blockRange >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f + (float)this.blockRange >= (float)Player.tileTargetY && this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					if (this.inventory[this.selectedItem].stack <= 0)
					{
						this.inventory[this.selectedItem].SetDefaults(0, false);
					}
					if (this.selectedItem == 48)
					{
						Main.mouseItem = this.inventory[this.selectedItem];
					}
					Main.PlaySound(7, -1, -1, 1);
					int num34 = 1;
					int num35;
					if (Main.rand.Next(2) == 0)
					{
						if (Main.rand.Next(10000) == 0)
						{
							num35 = 74;
							if (Main.rand.Next(12) == 0)
							{
								num34 += Main.rand.Next(0, 3);
							}
							if (Main.rand.Next(12) == 0)
							{
								num34 += Main.rand.Next(0, 3);
							}
							if (Main.rand.Next(12) == 0)
							{
								num34 += Main.rand.Next(0, 3);
							}
							if (Main.rand.Next(12) == 0)
							{
								num34 += Main.rand.Next(0, 3);
							}
							if (Main.rand.Next(12) == 0)
							{
								num34 += Main.rand.Next(0, 3);
							}
						}
						else
						{
							if (Main.rand.Next(800) == 0)
							{
								num35 = 73;
								if (Main.rand.Next(6) == 0)
								{
									num34 += Main.rand.Next(1, 21);
								}
								if (Main.rand.Next(6) == 0)
								{
									num34 += Main.rand.Next(1, 21);
								}
								if (Main.rand.Next(6) == 0)
								{
									num34 += Main.rand.Next(1, 21);
								}
								if (Main.rand.Next(6) == 0)
								{
									num34 += Main.rand.Next(1, 21);
								}
								if (Main.rand.Next(6) == 0)
								{
									num34 += Main.rand.Next(1, 20);
								}
							}
							else
							{
								if (Main.rand.Next(60) == 0)
								{
									num35 = 72;
									if (Main.rand.Next(4) == 0)
									{
										num34 += Main.rand.Next(5, 26);
									}
									if (Main.rand.Next(4) == 0)
									{
										num34 += Main.rand.Next(5, 26);
									}
									if (Main.rand.Next(4) == 0)
									{
										num34 += Main.rand.Next(5, 26);
									}
									if (Main.rand.Next(4) == 0)
									{
										num34 += Main.rand.Next(5, 25);
									}
								}
								else
								{
									num35 = 71;
									if (Main.rand.Next(3) == 0)
									{
										num34 += Main.rand.Next(10, 26);
									}
									if (Main.rand.Next(3) == 0)
									{
										num34 += Main.rand.Next(10, 26);
									}
									if (Main.rand.Next(3) == 0)
									{
										num34 += Main.rand.Next(10, 26);
									}
									if (Main.rand.Next(3) == 0)
									{
										num34 += Main.rand.Next(10, 25);
									}
								}
							}
						}
					}
					else
					{
						if (Main.rand.Next(5000) == 0)
						{
							num35 = 1242;
						}
						else
						{
							if (Main.rand.Next(25) == 0)
							{
								num35 = Main.rand.Next(6);
								if (num35 == 0)
								{
									num35 = 181;
								}
								else
								{
									if (num35 == 1)
									{
										num35 = 180;
									}
									else
									{
										if (num35 == 2)
										{
											num35 = 177;
										}
										else
										{
											if (num35 == 3)
											{
												num35 = 179;
											}
											else
											{
												if (num35 == 4)
												{
													num35 = 178;
												}
												else
												{
													num35 = 182;
												}
											}
										}
									}
								}
								if (Main.rand.Next(20) == 0)
								{
									num34 += Main.rand.Next(0, 2);
								}
								if (Main.rand.Next(30) == 0)
								{
									num34 += Main.rand.Next(0, 3);
								}
								if (Main.rand.Next(40) == 0)
								{
									num34 += Main.rand.Next(0, 4);
								}
								if (Main.rand.Next(50) == 0)
								{
									num34 += Main.rand.Next(0, 5);
								}
								if (Main.rand.Next(60) == 0)
								{
									num34 += Main.rand.Next(0, 6);
								}
							}
							else
							{
								if (Main.rand.Next(50) == 0)
								{
									num35 = 999;
									if (Main.rand.Next(20) == 0)
									{
										num34 += Main.rand.Next(0, 2);
									}
									if (Main.rand.Next(30) == 0)
									{
										num34 += Main.rand.Next(0, 3);
									}
									if (Main.rand.Next(40) == 0)
									{
										num34 += Main.rand.Next(0, 4);
									}
									if (Main.rand.Next(50) == 0)
									{
										num34 += Main.rand.Next(0, 5);
									}
									if (Main.rand.Next(60) == 0)
									{
										num34 += Main.rand.Next(0, 6);
									}
								}
								else
								{
									if (Main.rand.Next(3) == 0)
									{
										if (Main.rand.Next(5000) == 0)
										{
											num35 = 74;
											if (Main.rand.Next(10) == 0)
											{
												num34 += Main.rand.Next(0, 3);
											}
											if (Main.rand.Next(10) == 0)
											{
												num34 += Main.rand.Next(0, 3);
											}
											if (Main.rand.Next(10) == 0)
											{
												num34 += Main.rand.Next(0, 3);
											}
											if (Main.rand.Next(10) == 0)
											{
												num34 += Main.rand.Next(0, 3);
											}
											if (Main.rand.Next(10) == 0)
											{
												num34 += Main.rand.Next(0, 3);
											}
										}
										else
										{
											if (Main.rand.Next(400) == 0)
											{
												num35 = 73;
												if (Main.rand.Next(5) == 0)
												{
													num34 += Main.rand.Next(1, 21);
												}
												if (Main.rand.Next(5) == 0)
												{
													num34 += Main.rand.Next(1, 21);
												}
												if (Main.rand.Next(5) == 0)
												{
													num34 += Main.rand.Next(1, 21);
												}
												if (Main.rand.Next(5) == 0)
												{
													num34 += Main.rand.Next(1, 21);
												}
												if (Main.rand.Next(5) == 0)
												{
													num34 += Main.rand.Next(1, 20);
												}
											}
											else
											{
												if (Main.rand.Next(30) == 0)
												{
													num35 = 72;
													if (Main.rand.Next(3) == 0)
													{
														num34 += Main.rand.Next(5, 26);
													}
													if (Main.rand.Next(3) == 0)
													{
														num34 += Main.rand.Next(5, 26);
													}
													if (Main.rand.Next(3) == 0)
													{
														num34 += Main.rand.Next(5, 26);
													}
													if (Main.rand.Next(3) == 0)
													{
														num34 += Main.rand.Next(5, 25);
													}
												}
												else
												{
													num35 = 71;
													if (Main.rand.Next(2) == 0)
													{
														num34 += Main.rand.Next(10, 26);
													}
													if (Main.rand.Next(2) == 0)
													{
														num34 += Main.rand.Next(10, 26);
													}
													if (Main.rand.Next(2) == 0)
													{
														num34 += Main.rand.Next(10, 26);
													}
													if (Main.rand.Next(2) == 0)
													{
														num34 += Main.rand.Next(10, 25);
													}
												}
											}
										}
									}
									else
									{
										num35 = Main.rand.Next(8);
										if (num35 == 0)
										{
											num35 = 12;
										}
										else
										{
											if (num35 == 1)
											{
												num35 = 11;
											}
											else
											{
												if (num35 == 2)
												{
													num35 = 14;
												}
												else
												{
													if (num35 == 3)
													{
														num35 = 13;
													}
													else
													{
														if (num35 == 4)
														{
															num35 = 699;
														}
														else
														{
															if (num35 == 5)
															{
																num35 = 700;
															}
															else
															{
																if (num35 == 6)
																{
																	num35 = 701;
																}
																else
																{
																	num35 = 702;
																}
															}
														}
													}
												}
											}
										}
										if (Main.rand.Next(20) == 0)
										{
											num34 += Main.rand.Next(0, 2);
										}
										if (Main.rand.Next(30) == 0)
										{
											num34 += Main.rand.Next(0, 3);
										}
										if (Main.rand.Next(40) == 0)
										{
											num34 += Main.rand.Next(0, 4);
										}
										if (Main.rand.Next(50) == 0)
										{
											num34 += Main.rand.Next(0, 5);
										}
										if (Main.rand.Next(60) == 0)
										{
											num34 += Main.rand.Next(0, 6);
										}
									}
								}
							}
						}
					}
					if (num35 > 0)
					{
						int number = Item.NewItem((int)Main.screenPosition.X + Main.mouseX, (int)Main.screenPosition.Y + Main.mouseY, 1, 1, num35, num34, false, -1, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number, 1f, 0f, 0f, 0);
						}
					}
				}
			}
			else
			{
				if (this.inventory[this.selectedItem].createTile >= 0 && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f + (float)this.blockRange >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f + (float)this.blockRange >= (float)Player.tileTargetY)
				{
					this.showItemIcon = true;
					bool flag = false;
					if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid > 0 && Main.tile[Player.tileTargetX, Player.tileTargetY].lava())
					{
						if (Main.tileSolid[this.inventory[this.selectedItem].createTile])
						{
							flag = true;
						}
						else
						{
							if (Main.tileLavaDeath[this.inventory[this.selectedItem].createTile])
							{
								flag = true;
							}
						}
					}
					bool flag2 = true;
					if (this.inventory[this.selectedItem].tileWand > 0)
					{
						int tileWand = this.inventory[this.selectedItem].tileWand;
						flag2 = false;
						for (int m = 0; m < 58; m++)
						{
							if (tileWand == this.inventory[m].type && this.inventory[m].stack > 0)
							{
								flag2 = true;
								break;
							}
						}
					}
					if (Main.tileRope[this.inventory[this.selectedItem].createTile] && flag2 && Main.tile[Player.tileTargetX, Player.tileTargetY].active() && Main.tileRope[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
					{
						int num36 = Player.tileTargetY;
						int num37 = Player.tileTargetX;
						int createTile = this.inventory[this.selectedItem].createTile;
						while (Main.tile[num37, num36].active() && (int)Main.tile[num37, num36].type == createTile && num36 < Main.maxTilesX - 5)
						{
							num36++;
							if (Main.tile[num37, num36] == null)
							{
								flag2 = false;
								num36 = Player.tileTargetY;
							}
						}
						if (!Main.tile[num37, num36].active())
						{
							Player.tileTargetY = num36;
						}
					}
					if (flag2 && ((!Main.tile[Player.tileTargetX, Player.tileTargetY].active() && !flag) || Main.tileCut[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] || this.inventory[this.selectedItem].createTile == 199 || this.inventory[this.selectedItem].createTile == 23 || this.inventory[this.selectedItem].createTile == 2 || this.inventory[this.selectedItem].createTile == 109 || this.inventory[this.selectedItem].createTile == 60 || this.inventory[this.selectedItem].createTile == 70) && this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
					{
						bool flag3 = false;
						if (this.inventory[this.selectedItem].createTile == 23 || this.inventory[this.selectedItem].createTile == 2 || this.inventory[this.selectedItem].createTile == 109 || this.inventory[this.selectedItem].createTile == 199)
						{
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].nactive() && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0)
							{
								flag3 = true;
							}
						}
						else
						{
							if (this.inventory[this.selectedItem].createTile == 227)
							{
								flag3 = true;
							}
							else
							{
								if (this.inventory[this.selectedItem].createTile == 60 || this.inventory[this.selectedItem].createTile == 70)
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].nactive() && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 59)
									{
										flag3 = true;
									}
								}
								else
								{
									if (this.inventory[this.selectedItem].createTile == 4 || this.inventory[this.selectedItem].createTile == 136)
									{
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].wall > 0)
										{
											flag3 = true;
										}
										else
										{
											if (!WorldGen.SolidTileNoAttach(Player.tileTargetX, Player.tileTargetY + 1) && !WorldGen.SolidTileNoAttach(Player.tileTargetX - 1, Player.tileTargetY) && !WorldGen.SolidTileNoAttach(Player.tileTargetX + 1, Player.tileTargetY))
											{
												if (!WorldGen.SolidTileNoAttach(Player.tileTargetX, Player.tileTargetY + 1) && (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].halfBrick() || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].slope() != 0))
												{
													if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type != 19)
													{
														WorldGen.SlopeTile(Player.tileTargetX, Player.tileTargetY + 1, 0);
														if (Main.netMode == 1)
														{
															NetMessage.SendData(17, -1, -1, "", 14, (float)Player.tileTargetX, (float)(Player.tileTargetY + 1), 0f, 0);
														}
													}
												}
												else
												{
													if (!WorldGen.SolidTileNoAttach(Player.tileTargetX, Player.tileTargetY + 1) && !WorldGen.SolidTileNoAttach(Player.tileTargetX - 1, Player.tileTargetY) && (Main.tile[Player.tileTargetX - 1, Player.tileTargetY].halfBrick() || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].slope() != 0))
													{
														if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type != 19)
														{
															WorldGen.SlopeTile(Player.tileTargetX - 1, Player.tileTargetY, 0);
															if (Main.netMode == 1)
															{
																NetMessage.SendData(17, -1, -1, "", 14, (float)(Player.tileTargetX - 1), (float)Player.tileTargetY, 0f, 0);
															}
														}
													}
													else
													{
														if (!WorldGen.SolidTileNoAttach(Player.tileTargetX, Player.tileTargetY + 1) && !WorldGen.SolidTileNoAttach(Player.tileTargetX - 1, Player.tileTargetY) && !WorldGen.SolidTileNoAttach(Player.tileTargetX + 1, Player.tileTargetY) && (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].halfBrick() || Main.tile[Player.tileTargetX + 1, Player.tileTargetY].slope() != 0) && Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type != 19)
														{
															WorldGen.SlopeTile(Player.tileTargetX + 1, Player.tileTargetY, 0);
															if (Main.netMode == 1)
															{
																NetMessage.SendData(17, -1, -1, "", 14, (float)(Player.tileTargetX + 1), (float)Player.tileTargetY, 0f, 0);
															}
														}
													}
												}
											}
											int num38 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type;
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].halfBrick())
											{
												num38 = -1;
											}
											int num39 = (int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY].type;
											int num40 = (int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY].type;
											int num41 = (int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].type;
											int num42 = (int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY - 1].type;
											int num43 = (int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].type;
											int num44 = (int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY + 1].type;
											if (!Main.tile[Player.tileTargetX, Player.tileTargetY + 1].nactive())
											{
												num38 = -1;
											}
											if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY].nactive())
											{
												num39 = -1;
											}
											if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY].nactive())
											{
												num40 = -1;
											}
											if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].nactive())
											{
												num41 = -1;
											}
											if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY - 1].nactive())
											{
												num42 = -1;
											}
											if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY + 1].nactive())
											{
												num43 = -1;
											}
											if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY + 1].nactive())
											{
												num44 = -1;
											}
											if (num38 >= 0 && Main.tileSolid[num38] && !Main.tileNoAttach[num38])
											{
												flag3 = true;
											}
											else
											{
												if ((num39 >= 0 && Main.tileSolid[num39] && !Main.tileNoAttach[num39]) || (num39 == 5 && num41 == 5 && num43 == 5) || num39 == 124)
												{
													flag3 = true;
												}
												else
												{
													if ((num40 >= 0 && Main.tileSolid[num40] && !Main.tileNoAttach[num40]) || (num40 == 5 && num42 == 5 && num44 == 5) || num40 == 124)
													{
														flag3 = true;
													}
												}
											}
										}
									}
									else
									{
										if (this.inventory[this.selectedItem].createTile == 78 || this.inventory[this.selectedItem].createTile == 98 || this.inventory[this.selectedItem].createTile == 100 || this.inventory[this.selectedItem].createTile == 173 || this.inventory[this.selectedItem].createTile == 174 || this.inventory[this.selectedItem].createTile == 324)
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].nactive() && (Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type] || Main.tileTable[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type]))
											{
												flag3 = true;
											}
										}
										else
										{
											if (this.inventory[this.selectedItem].createTile == 13 || this.inventory[this.selectedItem].createTile == 29 || this.inventory[this.selectedItem].createTile == 33 || this.inventory[this.selectedItem].createTile == 49 || this.inventory[this.selectedItem].createTile == 50 || this.inventory[this.selectedItem].createTile == 103)
											{
												if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].nactive() && Main.tileTable[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type])
												{
													flag3 = true;
												}
											}
											else
											{
												if (this.inventory[this.selectedItem].createTile == 275 || this.inventory[this.selectedItem].createTile == 276 || this.inventory[this.selectedItem].createTile == 277)
												{
													flag3 = true;
												}
												else
												{
													if (this.inventory[this.selectedItem].createTile == 51 || this.inventory[this.selectedItem].createTile == 330 || this.inventory[this.selectedItem].createTile == 331 || this.inventory[this.selectedItem].createTile == 332 || this.inventory[this.selectedItem].createTile == 333 || this.inventory[this.selectedItem].createTile == 336)
													{
														if (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active() || Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active() || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active() || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active() || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0)
														{
															flag3 = true;
														}
													}
													else
													{
														if (this.inventory[this.selectedItem].createTile == 314)
														{
															for (int n = Player.tileTargetX - 1; n <= Player.tileTargetX + 1; n++)
															{
																for (int num45 = Player.tileTargetY - 1; num45 <= Player.tileTargetY + 1; num45++)
																{
																	Tile tile = Main.tile[n, num45];
																	if (tile.active() || tile.wall > 0)
																	{
																		flag3 = true;
																		break;
																	}
																}
															}
														}
														else
														{
															Tile tile2 = Main.tile[Player.tileTargetX - 1, Player.tileTargetY];
															Tile tile3 = Main.tile[Player.tileTargetX + 1, Player.tileTargetY];
															Tile tile4 = Main.tile[Player.tileTargetX, Player.tileTargetY - 1];
															Tile tile5 = Main.tile[Player.tileTargetX, Player.tileTargetY + 1];
															if ((tile3.active() && (Main.tileSolid[(int)tile3.type] || Main.tileRope[(int)tile3.type] || tile3.type == 314)) || (tile3.wall > 0 || (tile2.active() && (Main.tileSolid[(int)tile2.type] || Main.tileRope[(int)tile2.type] || tile2.type == 314))) || (tile2.wall > 0 || (tile5.active() && (Main.tileSolid[(int)tile5.type] || tile5.type == 124 || Main.tileRope[(int)tile5.type] || tile5.type == 314))) || (tile5.wall > 0 || (tile4.active() && (Main.tileSolid[(int)tile4.type] || tile4.type == 124 || Main.tileRope[(int)tile4.type] || tile4.type == 314))) || tile4.wall > 0)
															{
																flag3 = true;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (Main.tileAlch[this.inventory[this.selectedItem].createTile])
						{
							flag3 = true;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].active() && Main.tileCut[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
						{
							if ((int)Main.tile[Player.tileTargetX, Player.tileTargetY].type != this.inventory[this.selectedItem].createTile)
							{
								if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type != 78 || ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 3 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 73) && Main.tileAlch[this.inventory[this.selectedItem].createTile]))
								{
									WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
									if (!Main.tile[Player.tileTargetX, Player.tileTargetY].active() && Main.netMode == 1)
									{
										NetMessage.SendData(17, -1, -1, "", 4, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
									}
								}
								else
								{
									flag3 = false;
								}
							}
							else
							{
								flag3 = false;
							}
						}
						if (!flag3 && this.inventory[this.selectedItem].createTile == 19)
						{
							for (int num46 = Player.tileTargetX - 1; num46 <= Player.tileTargetX + 1; num46++)
							{
								for (int num47 = Player.tileTargetY - 1; num47 <= Player.tileTargetY + 1; num47++)
								{
									if (Main.tile[num46, num47].active())
									{
										flag3 = true;
										break;
									}
								}
							}
						}
						if (flag3)
						{
							int num48 = this.inventory[this.selectedItem].placeStyle;
							if (this.inventory[this.selectedItem].createTile == 36)
							{
								num48 = Main.rand.Next(7);
							}
							if (this.inventory[this.selectedItem].createTile == 212 && this.direction > 0)
							{
								num48 = 1;
							}
							if (this.inventory[this.selectedItem].createTile == 141)
							{
								num48 = Main.rand.Next(2);
							}
							if (this.inventory[this.selectedItem].createTile == 128 || this.inventory[this.selectedItem].createTile == 269 || this.inventory[this.selectedItem].createTile == 334)
							{
								if (this.direction < 0)
								{
									num48 = -1;
								}
								else
								{
									num48 = 1;
								}
							}
							if (this.inventory[this.selectedItem].createTile == 241 && this.inventory[this.selectedItem].placeStyle == 0)
							{
								num48 = Main.rand.Next(0, 9);
							}
							if (this.inventory[this.selectedItem].createTile == 35 && this.inventory[this.selectedItem].placeStyle == 0)
							{
								num48 = Main.rand.Next(9);
							}
							if (this.inventory[this.selectedItem].createTile == 314 && num48 == 2 && this.direction == 1)
							{
								num48++;
							}
							int[,] array = new int[11, 11];
							if (this.autoPaint)
							{
								for (int num49 = 0; num49 < 11; num49++)
								{
									for (int num50 = 0; num50 < 11; num50++)
									{
										int num51 = Player.tileTargetX - 5 + num49;
										int num52 = Player.tileTargetY - 5 + num50;
										if (Main.tile[num51, num52].active())
										{
											array[num49, num50] = (int)Main.tile[num51, num52].type;
										}
										else
										{
											array[num49, num50] = -1;
										}
									}
								}
							}
							bool forced = false;
							if (WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, this.inventory[this.selectedItem].createTile, false, forced, this.whoAmi, num48))
							{
								this.itemTime = (int)((float)this.inventory[this.selectedItem].useTime * this.tileSpeed);
								if (Main.netMode == 1 && this.inventory[this.selectedItem].createTile != 21)
								{
									NetMessage.SendData(17, -1, -1, "", 1, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.inventory[this.selectedItem].createTile, num48);
								}
								if (this.inventory[this.selectedItem].createTile == 15)
								{
									if (this.direction == 1)
									{
										Tile expr_30DB = Main.tile[Player.tileTargetX, Player.tileTargetY];
										expr_30DB.frameX += 18;
										Tile expr_3100 = Main.tile[Player.tileTargetX, Player.tileTargetY - 1];
										expr_3100.frameX += 18;
									}
									if (Main.netMode == 1)
									{
										NetMessage.SendTileSquare(-1, Player.tileTargetX - 1, Player.tileTargetY - 1, 3);
									}
								}
								else
								{
									if (this.inventory[this.selectedItem].createTile == 137)
									{
										if (this.direction == 1)
										{
											Tile expr_316A = Main.tile[Player.tileTargetX, Player.tileTargetY];
											expr_316A.frameX += 18;
										}
										if (Main.netMode == 1)
										{
											NetMessage.SendTileSquare(-1, Player.tileTargetX, Player.tileTargetY, 1);
										}
									}
									else
									{
										if ((this.inventory[this.selectedItem].createTile == 79 || this.inventory[this.selectedItem].createTile == 90) && Main.netMode == 1)
										{
											NetMessage.SendTileSquare(-1, Player.tileTargetX, Player.tileTargetY, 5);
										}
									}
								}
								if (Main.tileSolid[this.inventory[this.selectedItem].createTile] && this.inventory[this.selectedItem].createTile != 19)
								{
									int num53 = Player.tileTargetX;
									int num54 = Player.tileTargetY + 1;
									if (Main.tile[num53, num54] != null && Main.tile[num53, num54].type != 19 && (Main.tile[num53, num54].topSlope() || Main.tile[num53, num54].halfBrick()))
									{
										WorldGen.SlopeTile(num53, num54, 0);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(17, -1, -1, "", 14, (float)num53, (float)num54, 0f, 0);
										}
									}
									num53 = Player.tileTargetX;
									num54 = Player.tileTargetY - 1;
									if (Main.tile[num53, num54] != null && Main.tile[num53, num54].type != 19 && Main.tile[num53, num54].bottomSlope())
									{
										WorldGen.SlopeTile(num53, num54, 0);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(17, -1, -1, "", 14, (float)num53, (float)num54, 0f, 0);
										}
									}
								}
								if (Main.tileSolid[this.inventory[this.selectedItem].createTile])
								{
									for (int num55 = Player.tileTargetX - 1; num55 <= Player.tileTargetX + 1; num55++)
									{
										for (int num56 = Player.tileTargetY - 1; num56 <= Player.tileTargetY + 1; num56++)
										{
											if (Main.tile[num55, num56].active() && this.inventory[this.selectedItem].createTile != (int)Main.tile[num55, num56].type && (Main.tile[num55, num56].type == 2 || Main.tile[num55, num56].type == 23 || Main.tile[num55, num56].type == 60 || Main.tile[num55, num56].type == 70 || Main.tile[num55, num56].type == 109 || Main.tile[num55, num56].type == 199))
											{
												bool flag4 = true;
												for (int num57 = num55 - 1; num57 <= num55 + 1; num57++)
												{
													for (int num58 = num56 - 1; num58 <= num56 + 1; num58++)
													{
														if (!WorldGen.SolidTile(num57, num58))
														{
															flag4 = false;
														}
													}
												}
												if (flag4)
												{
													WorldGen.KillTile(num55, num56, true, false, false);
													if (Main.netMode == 1)
													{
														NetMessage.SendData(17, -1, -1, "", 0, (float)num55, (float)num56, 1f, 0);
													}
												}
											}
										}
									}
								}
								if (this.autoPaint)
								{
									for (int num59 = 0; num59 < 11; num59++)
									{
										for (int num60 = 0; num60 < 11; num60++)
										{
											int num61 = Player.tileTargetX - 5 + num59;
											int num62 = Player.tileTargetY - 5 + num60;
											if ((Main.tile[num61, num62].active() || array[num59, num60] != -1) && (!Main.tile[num61, num62].active() || array[num59, num60] != (int)Main.tile[num61, num62].type))
											{
												int num63 = -1;
												int num64 = -1;
												for (int num65 = 0; num65 < 58; num65++)
												{
													if (this.inventory[num65].stack > 0 && this.inventory[num65].paint > 0)
													{
														num63 = (int)this.inventory[num65].paint;
														num64 = num65;
														break;
													}
												}
												if (num63 > 0 && (int)Main.tile[num61, num62].color() != num63 && WorldGen.paintTile(num61, num62, (byte)num63, true))
												{
													int num66 = num64;
													this.inventory[num66].stack--;
													if (this.inventory[num66].stack <= 0)
													{
														this.inventory[num66].SetDefaults(0, false);
													}
													this.itemTime = (int)((float)this.inventory[this.selectedItem].useTime * this.tileSpeed);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].createWall >= 0 && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
			{
				this.showItemIcon = true;
				if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem && (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active() || Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active() || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active() || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active() || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0) && (int)Main.tile[Player.tileTargetX, Player.tileTargetY].wall != this.inventory[this.selectedItem].createWall)
				{
					WorldGen.PlaceWall(Player.tileTargetX, Player.tileTargetY, this.inventory[this.selectedItem].createWall, false);
					if ((int)Main.tile[Player.tileTargetX, Player.tileTargetY].wall == this.inventory[this.selectedItem].createWall)
					{
						this.itemTime = (int)((float)this.inventory[this.selectedItem].useTime * this.wallSpeed);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(17, -1, -1, "", 3, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.inventory[this.selectedItem].createWall, 0);
						}
						if (this.inventory[this.selectedItem].stack > 1)
						{
							int createWall = this.inventory[this.selectedItem].createWall;
							for (int num67 = 0; num67 < 4; num67++)
							{
								int num68 = Player.tileTargetX;
								int num69 = Player.tileTargetY;
								if (num67 == 0)
								{
									num68--;
								}
								if (num67 == 1)
								{
									num68++;
								}
								if (num67 == 2)
								{
									num69--;
								}
								if (num67 == 3)
								{
									num69++;
								}
								if (Main.tile[num68, num69].wall == 0)
								{
									int num70 = 0;
									for (int num71 = 0; num71 < 4; num71++)
									{
										int num72 = num68;
										int num73 = num69;
										if (num71 == 0)
										{
											num72--;
										}
										if (num71 == 1)
										{
											num72++;
										}
										if (num71 == 2)
										{
											num73--;
										}
										if (num71 == 3)
										{
											num73++;
										}
										if ((int)Main.tile[num72, num73].wall == createWall)
										{
											num70++;
										}
									}
									if (num70 == 4)
									{
										WorldGen.PlaceWall(num68, num69, createWall, false);
										if ((int)Main.tile[num68, num69].wall == createWall)
										{
											this.inventory[this.selectedItem].stack--;
											if (this.inventory[this.selectedItem].stack == 0)
											{
												this.inventory[this.selectedItem].SetDefaults(0, false);
											}
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 3, (float)num68, (float)num69, (float)createWall, 0);
											}
											if (this.autoPaint)
											{
												int num74 = num68;
												int num75 = num69;
												int num76 = -1;
												int num77 = -1;
												for (int num78 = 0; num78 < 58; num78++)
												{
													if (this.inventory[num78].stack > 0 && this.inventory[num78].paint > 0)
													{
														num76 = (int)this.inventory[num78].paint;
														num77 = num78;
														break;
													}
												}
												if (num76 > 0 && (int)Main.tile[num74, num75].wallColor() != num76 && WorldGen.paintWall(num74, num75, (byte)num76, true))
												{
													int num79 = num77;
													this.inventory[num79].stack--;
													if (this.inventory[num79].stack <= 0)
													{
														this.inventory[num79].SetDefaults(0, false);
													}
													this.itemTime = (int)((float)this.inventory[this.selectedItem].useTime * this.wallSpeed);
												}
											}
										}
									}
								}
							}
						}
						if (this.autoPaint)
						{
							int num80 = Player.tileTargetX;
							int num81 = Player.tileTargetY;
							int num82 = -1;
							int num83 = -1;
							for (int num84 = 0; num84 < 58; num84++)
							{
								if (this.inventory[num84].stack > 0 && this.inventory[num84].paint > 0)
								{
									num82 = (int)this.inventory[num84].paint;
									num83 = num84;
									break;
								}
							}
							if (num82 > 0 && (int)Main.tile[num80, num81].wallColor() != num82 && WorldGen.paintWall(num80, num81, (byte)num82, true))
							{
								int num85 = num83;
								this.inventory[num85].stack--;
								if (this.inventory[num85].stack <= 0)
								{
									this.inventory[num85].SetDefaults(0, false);
								}
								this.itemTime = (int)((float)this.inventory[this.selectedItem].useTime * this.wallSpeed);
							}
						}
					}
				}
			}
		}
		public void ChangeDir(int dir)
		{
			if (!this.pulley || this.pulleyDir != 2)
			{
				this.direction = dir;
				return;
			}
			if (this.pulleyDir == 2 && dir == this.direction)
			{
				return;
			}
			int num = (int)(this.position.X + (float)(this.width / 2)) / 16;
			int num2 = num * 16 + 8 - this.width / 2;
			if (!Collision.SolidCollision(new Vector2((float)num2, this.position.Y), this.width, this.height))
			{
				if (this.whoAmi == Main.myPlayer)
				{
					Main.cameraX = Main.cameraX + this.position.X - (float)num2;
				}
				this.pulleyDir = 1;
				this.position.X = (float)num2;
				this.direction = dir;
			}
		}
		public Vector2 center()
		{
			return new Vector2(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2));
		}
		public Rectangle getRect()
		{
			return new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
		}
		private void pumpkinSword(int i, int dmg, float kb)
		{
			int num = Main.rand.Next(100, 300);
			int num2 = Main.rand.Next(100, 300);
			if (Main.rand.Next(2) == 0)
			{
				num -= Main.maxScreenW / 2 + num;
			}
			else
			{
				num += Main.maxScreenW / 2 - num;
			}
			if (Main.rand.Next(2) == 0)
			{
				num2 -= Main.maxScreenH / 2 + num2;
			}
			else
			{
				num2 += Main.maxScreenH / 2 - num2;
			}
			num += (int)this.position.X;
			num2 += (int)this.position.Y;
			float num3 = 8f;
			Vector2 vector = new Vector2((float)num, (float)num2);
			float num4 = Main.npc[i].position.X - vector.X;
			float num5 = Main.npc[i].position.Y - vector.Y;
			float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
			num6 = num3 / num6;
			num4 *= num6;
			num5 *= num6;
			Projectile.NewProjectile((float)num, (float)num2, num4, num5, 321, dmg, kb, this.whoAmi, (float)i, 0f);
		}
		public void PutItemInInventory(int type, int selItem = -1)
		{
			for (int i = 0; i < 58; i++)
			{
				Item item = this.inventory[i];
				if (item.stack > 0 && item.type == type && item.stack < item.maxStack)
				{
					item.stack++;
					return;
				}
			}
			if (selItem >= 0 && (this.inventory[selItem].type == 0 || this.inventory[selItem].stack <= 0))
			{
				this.inventory[selItem].SetDefaults(type, false);
				return;
			}
			Item item2 = new Item();
			item2.SetDefaults(type, false);
			Item item3 = this.GetItem(this.whoAmi, item2, false);
			if (item3.stack > 0)
			{
				int number = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, type, 1, false, 0, true);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", number, 1f, 0f, 0f, 0);
					return;
				}
			}
			else
			{
				item2.position.X = this.center().X - (float)(item2.width / 2);
				item2.position.Y = this.center().Y - (float)(item2.height / 2);
				item2.active = true;
				ItemText.NewText(item2, 0, false, false);
			}
		}
		public bool SummonItemCheck()
		{
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && ((this.inventory[this.selectedItem].type == 43 && Main.npc[i].type == 4) || (this.inventory[this.selectedItem].type == 70 && Main.npc[i].type == 13) || ((this.inventory[this.selectedItem].type == 560 & Main.npc[i].type == 50) || (this.inventory[this.selectedItem].type == 544 && Main.npc[i].type == 125)) || (this.inventory[this.selectedItem].type == 544 && Main.npc[i].type == 126) || (this.inventory[this.selectedItem].type == 556 && Main.npc[i].type == 134) || (this.inventory[this.selectedItem].type == 557 && Main.npc[i].type == 127) || (this.inventory[this.selectedItem].type == 1133 && Main.npc[i].type == 222) || (this.inventory[this.selectedItem].type == 1331 && Main.npc[i].type == 266)))
				{
					return false;
				}
			}
			return true;
		}
		public int FishingLevel()
		{
			int num = 0;
			int fishingPole = this.inventory[this.selectedItem].fishingPole;
			int i = 0;
			while (i < 58)
			{
				if (this.inventory[i].stack > 0 && this.inventory[i].bait > 0)
				{
					if (this.inventory[i].type == 2673)
					{
						return -1;
					}
					num = this.inventory[i].bait;
					break;
				}
				else
				{
					i++;
				}
			}
			if (num == 0 || fishingPole == 0)
			{
				return 0;
			}
			int num2 = num + fishingPole + this.fishingSkill;
			if (Main.raining)
			{
				num2 = (int)((float)num2 * 1.2f);
			}
			if (Main.dayTime && (Main.time < 5400.0 || Main.time > 48600.0))
			{
				num2 = (int)((float)num2 * 1.3f);
			}
			if (Main.dayTime && Main.time > 16200.0 && Main.time < 37800.0)
			{
				num2 = (int)((float)num2 * 0.8f);
			}
			if (!Main.dayTime && Main.time > 6480.0 && Main.time < 25920.0)
			{
				num2 = (int)((float)num2 * 0.8f);
			}
			if (Main.moonPhase == 0)
			{
				num2 = (int)((float)num2 * 1.1f);
			}
			if (Main.moonPhase == 1 || Main.moonPhase == 7)
			{
				num2 = (int)((float)num2 * 1.05f);
			}
			if (Main.moonPhase == 3 || Main.moonPhase == 5)
			{
				num2 = (int)((float)num2 * 0.95f);
			}
			if (Main.moonPhase == 4)
			{
				num2 = (int)((float)num2 * 0.9f);
			}
			return num2;
		}
		public void ItemCheck(int i)
		{
			if (this.frozen)
			{
				return;
			}
			bool flag = false;
			float num = 5E-06f;
			int num2 = this.inventory[this.selectedItem].damage;
			if (num2 > 0)
			{
				if (this.inventory[this.selectedItem].melee)
				{
					num2 = (int)((float)num2 * this.meleeDamage + num);
				}
				else
				{
					if (this.inventory[this.selectedItem].ranged)
					{
						num2 = (int)((float)num2 * this.rangedDamage + num);
						if (this.inventory[this.selectedItem].useAmmo == 1 || this.inventory[this.selectedItem].useAmmo == 323)
						{
							num2 = (int)((float)num2 * this.arrowDamage + num);
						}
						if (this.inventory[this.selectedItem].useAmmo == 14 || this.inventory[this.selectedItem].useAmmo == 311)
						{
							num2 = (int)((float)num2 * this.bulletDamage + num);
						}
						if (this.inventory[this.selectedItem].useAmmo == 771 || this.inventory[this.selectedItem].useAmmo == 246 || this.inventory[this.selectedItem].useAmmo == 312)
						{
							num2 = (int)((float)num2 * this.rocketDamage + num);
						}
					}
					else
					{
						if (this.inventory[this.selectedItem].magic)
						{
							num2 = (int)((float)num2 * this.magicDamage + num);
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].autoReuse && !this.noItems)
			{
				this.releaseUseItem = true;
				if (this.itemAnimation == 1 && this.inventory[this.selectedItem].stack > 0)
				{
					if (this.inventory[this.selectedItem].shoot > 0 && this.whoAmi != Main.myPlayer && this.controlUseItem && this.inventory[this.selectedItem].useStyle == 5)
					{
						this.itemAnimation = 2;
					}
					else
					{
						this.itemAnimation = 0;
					}
				}
			}
			if (this.inventory[this.selectedItem].fishingPole > 0)
			{
				this.inventory[this.selectedItem].holdStyle = 0;
				if (this.itemTime == 0 && this.itemAnimation == 0)
				{
					for (int j = 0; j < 1000; j++)
					{
						if (Main.projectile[j].active && Main.projectile[j].owner == this.whoAmi && Main.projectile[j].bobber)
						{
							this.inventory[this.selectedItem].holdStyle = 1;
						}
					}
				}
			}
			if (this.whoAmi == Main.myPlayer && this.mount.Active)
			{
				if (this.inventory[this.selectedItem].mountType != this.mount.Type && this.itemAnimation > 0)
				{
					this.mount.Dismount(this);
				}
				if (this.gravDir == -1f)
				{
					this.mount.Dismount(this);
				}
			}
			if (this.itemAnimation == 0 && this.reuseDelay > 0)
			{
				this.itemAnimation = this.reuseDelay;
				this.itemTime = this.reuseDelay;
				this.reuseDelay = 0;
			}
			if (this.controlUseItem && this.releaseUseItem && (this.inventory[this.selectedItem].headSlot > 0 || this.inventory[this.selectedItem].bodySlot > 0 || this.inventory[this.selectedItem].legSlot > 0))
			{
				if (this.inventory[this.selectedItem].useStyle == 0)
				{
					this.releaseUseItem = false;
				}
				if (this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
				{
					int num3 = Player.tileTargetX;
					int num4 = Player.tileTargetY;
					if (Main.tile[num3, num4].active() && (Main.tile[num3, num4].type == 128 || Main.tile[num3, num4].type == 269))
					{
						int num5 = (int)Main.tile[num3, num4].frameY;
						int k = 0;
						if (this.inventory[this.selectedItem].bodySlot >= 0)
						{
							k = 1;
						}
						if (this.inventory[this.selectedItem].legSlot >= 0)
						{
							k = 2;
						}
						num5 /= 18;
						while (k > num5)
						{
							num4++;
							num5 = (int)Main.tile[num3, num4].frameY;
							num5 /= 18;
						}
						while (k < num5)
						{
							num4--;
							num5 = (int)Main.tile[num3, num4].frameY;
							num5 /= 18;
						}
						int l;
						for (l = (int)Main.tile[num3, num4].frameX; l >= 100; l -= 100)
						{
						}
						if (l >= 36)
						{
							l -= 36;
						}
						num3 -= l / 18;
						int m = (int)Main.tile[num3, num4].frameX;
						WorldGen.KillTile(num3, num4, true, false, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(17, -1, -1, "", 0, (float)num3, (float)num4, 1f, 0);
						}
						while (m >= 100)
						{
							m -= 100;
						}
						if (num5 == 0 && this.inventory[this.selectedItem].headSlot >= 0)
						{
							Main.blockMouse = true;
							Main.tile[num3, num4].frameX = (short)(m + this.inventory[this.selectedItem].headSlot * 100);
							if (Main.netMode == 1)
							{
								NetMessage.SendTileSquare(-1, num3, num4, 1);
							}
							this.inventory[this.selectedItem].stack--;
							if (this.inventory[this.selectedItem].stack <= 0)
							{
								this.inventory[this.selectedItem].SetDefaults(0, false);
								Main.mouseItem.SetDefaults(0, false);
							}
							if (this.selectedItem == 58)
							{
								Main.mouseItem = this.inventory[this.selectedItem].Clone();
							}
							this.releaseUseItem = false;
							this.mouseInterface = true;
						}
						else
						{
							if (num5 == 1 && this.inventory[this.selectedItem].bodySlot >= 0)
							{
								Main.blockMouse = true;
								Main.tile[num3, num4].frameX = (short)(m + this.inventory[this.selectedItem].bodySlot * 100);
								if (Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, num3, num4, 1);
								}
								this.inventory[this.selectedItem].stack--;
								if (this.inventory[this.selectedItem].stack <= 0)
								{
									this.inventory[this.selectedItem].SetDefaults(0, false);
									Main.mouseItem.SetDefaults(0, false);
								}
								if (this.selectedItem == 58)
								{
									Main.mouseItem = this.inventory[this.selectedItem].Clone();
								}
								this.releaseUseItem = false;
								this.mouseInterface = true;
							}
							else
							{
								if (num5 == 2 && this.inventory[this.selectedItem].legSlot >= 0)
								{
									Main.blockMouse = true;
									Main.tile[num3, num4].frameX = (short)(m + this.inventory[this.selectedItem].legSlot * 100);
									if (Main.netMode == 1)
									{
										NetMessage.SendTileSquare(-1, num3, num4, 1);
									}
									this.inventory[this.selectedItem].stack--;
									if (this.inventory[this.selectedItem].stack <= 0)
									{
										this.inventory[this.selectedItem].SetDefaults(0, false);
										Main.mouseItem.SetDefaults(0, false);
									}
									if (this.selectedItem == 58)
									{
										Main.mouseItem = this.inventory[this.selectedItem].Clone();
									}
									this.releaseUseItem = false;
									this.mouseInterface = true;
								}
							}
						}
					}
				}
			}
			if (this.controlUseItem && this.itemAnimation == 0 && this.releaseUseItem && this.inventory[this.selectedItem].useStyle > 0)
			{
				bool flag2 = true;
				if (this.inventory[this.selectedItem].shoot == 0)
				{
					this.itemRotation = 0f;
				}
				if (this.pulley && this.inventory[this.selectedItem].fishingPole > 0)
				{
					flag2 = false;
				}
				if (this.wet && (this.inventory[this.selectedItem].shoot == 85 || this.inventory[this.selectedItem].shoot == 15 || this.inventory[this.selectedItem].shoot == 34))
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].makeNPC > 0 && !NPC.CanReleaseNPCs(this.whoAmi))
				{
					flag2 = false;
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 603 && !Main.cEd)
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].type == 1071 || this.inventory[this.selectedItem].type == 1072)
				{
					bool flag3 = false;
					for (int n = 0; n < 58; n++)
					{
						if (this.inventory[n].paint > 0)
						{
							flag3 = true;
							break;
						}
					}
					if (!flag3)
					{
						flag2 = false;
					}
				}
				if (this.noItems)
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].tileWand > 0)
				{
					int tileWand = this.inventory[this.selectedItem].tileWand;
					flag2 = false;
					for (int num6 = 0; num6 < 58; num6++)
					{
						if (tileWand == this.inventory[num6].type && this.inventory[num6].stack > 0)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (this.inventory[this.selectedItem].fishingPole > 0)
				{
					for (int num7 = 0; num7 < 1000; num7++)
					{
						if (Main.projectile[num7].active && Main.projectile[num7].owner == this.whoAmi && Main.projectile[num7].bobber)
						{
							flag2 = false;
							if (this.whoAmi == Main.myPlayer && Main.projectile[num7].ai[0] == 0f)
							{
								Main.projectile[num7].ai[0] = 1f;
								float num8 = -10f;
								if (Main.projectile[num7].wet && Main.projectile[num7].velocity.Y > num8)
								{
									Main.projectile[num7].velocity.Y = num8;
								}
								Main.projectile[num7].netUpdate2 = true;
								if (Main.projectile[num7].ai[1] < 0f && Main.projectile[num7].localAI[1] != 0f)
								{
									bool flag4 = false;
									int num9 = 0;
									for (int num10 = 0; num10 < 58; num10++)
									{
										if (this.inventory[num10].stack > 0 && this.inventory[num10].bait > 0)
										{
											bool flag5 = false;
											int num11 = this.inventory[num10].bait / 5;
											if (num11 < 1)
											{
												num11 = 1;
											}
											if (this.accTackleBox)
											{
												num11++;
											}
											if (Main.rand.Next(num11) == 0)
											{
												flag5 = true;
											}
											if (Main.projectile[num7].localAI[1] < 0f)
											{
												flag5 = true;
											}
											if (Main.projectile[num7].localAI[1] > 0f)
											{
												Item item = new Item();
												item.SetDefaults((int)Main.projectile[num7].localAI[1], false);
												if (item.rare < 0)
												{
													flag5 = false;
												}
											}
											if (flag5)
											{
												num9 = this.inventory[num10].type;
												this.inventory[num10].stack--;
												if (this.inventory[num10].stack <= 0)
												{
													this.inventory[num10].SetDefaults(0, false);
												}
											}
											flag4 = true;
											break;
										}
									}
									if (flag4)
									{
										if (num9 == 2673)
										{
											if (Main.netMode != 1)
											{
												NPC.SpawnOnPlayer(this.whoAmi, 370);
											}
											else
											{
												NetMessage.SendData(61, -1, -1, "", this.whoAmi, 370f, 0f, 0f, 0);
											}
											Main.projectile[num7].ai[0] = 2f;
										}
										else
										{
											if (Main.rand.Next(7) == 0 && !this.accFishingLine)
											{
												Main.projectile[num7].ai[0] = 2f;
											}
											else
											{
												Main.projectile[num7].ai[1] = Main.projectile[num7].localAI[1];
											}
										}
										Main.projectile[num7].netUpdate = true;
									}
								}
							}
						}
					}
				}
				if (this.inventory[this.selectedItem].shoot == 6 || this.inventory[this.selectedItem].shoot == 19 || this.inventory[this.selectedItem].shoot == 33 || this.inventory[this.selectedItem].shoot == 52 || this.inventory[this.selectedItem].shoot == 113 || this.inventory[this.selectedItem].shoot == 182 || this.inventory[this.selectedItem].shoot == 320 || this.inventory[this.selectedItem].shoot == 333 || this.inventory[this.selectedItem].shoot == 383)
				{
					for (int num12 = 0; num12 < 1000; num12++)
					{
						if (Main.projectile[num12].active && Main.projectile[num12].owner == Main.myPlayer && Main.projectile[num12].type == this.inventory[this.selectedItem].shoot)
						{
							flag2 = false;
						}
					}
				}
				if (this.inventory[this.selectedItem].shoot == 106)
				{
					int num13 = 0;
					for (int num14 = 0; num14 < 1000; num14++)
					{
						if (Main.projectile[num14].active && Main.projectile[num14].owner == Main.myPlayer && Main.projectile[num14].type == this.inventory[this.selectedItem].shoot)
						{
							num13++;
						}
					}
					if (num13 >= this.inventory[this.selectedItem].stack)
					{
						flag2 = false;
					}
				}
				if (this.inventory[this.selectedItem].shoot == 272)
				{
					int num15 = 0;
					for (int num16 = 0; num16 < 1000; num16++)
					{
						if (Main.projectile[num16].active && Main.projectile[num16].owner == Main.myPlayer && Main.projectile[num16].type == this.inventory[this.selectedItem].shoot)
						{
							num15++;
						}
					}
					if (num15 >= this.inventory[this.selectedItem].stack)
					{
						flag2 = false;
					}
				}
				if (this.inventory[this.selectedItem].shoot == 13 || this.inventory[this.selectedItem].shoot == 32 || (this.inventory[this.selectedItem].shoot >= 230 && this.inventory[this.selectedItem].shoot <= 235) || this.inventory[this.selectedItem].shoot == 315 || this.inventory[this.selectedItem].shoot == 331 || this.inventory[this.selectedItem].shoot == 372)
				{
					for (int num17 = 0; num17 < 1000; num17++)
					{
						if (Main.projectile[num17].active && Main.projectile[num17].owner == Main.myPlayer && Main.projectile[num17].type == this.inventory[this.selectedItem].shoot && Main.projectile[num17].ai[0] != 2f)
						{
							flag2 = false;
						}
					}
				}
				if (this.inventory[this.selectedItem].shoot == 332)
				{
					int num18 = 0;
					for (int num19 = 0; num19 < 1000; num19++)
					{
						if (Main.projectile[num19].active && Main.projectile[num19].owner == Main.myPlayer && Main.projectile[num19].type == this.inventory[this.selectedItem].shoot && Main.projectile[num19].ai[0] != 2f)
						{
							num18++;
						}
					}
					if (num18 >= 3)
					{
						flag2 = false;
					}
				}
				if (this.inventory[this.selectedItem].potion && flag2)
				{
					if (this.potionDelay <= 0)
					{
						this.potionDelay = this.potionDelayTime;
						this.AddBuff(21, this.potionDelay, true);
					}
					else
					{
						flag2 = false;
					}
				}
				if (this.inventory[this.selectedItem].mana > 0 && this.silence)
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].mana > 0 && flag2)
				{
					if (this.inventory[this.selectedItem].type != 127 || !this.spaceGun)
					{
						if (this.statMana >= (int)((float)this.inventory[this.selectedItem].mana * this.manaCost))
						{
							this.statMana -= (int)((float)this.inventory[this.selectedItem].mana * this.manaCost);
						}
						else
						{
							if (this.manaFlower)
							{
								this.QuickMana();
								if (this.statMana >= (int)((float)this.inventory[this.selectedItem].mana * this.manaCost))
								{
									this.statMana -= (int)((float)this.inventory[this.selectedItem].mana * this.manaCost);
								}
								else
								{
									flag2 = false;
								}
							}
							else
							{
								flag2 = false;
							}
						}
					}
					if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].buffType != 0 && flag2)
					{
						this.AddBuff(this.inventory[this.selectedItem].buffType, this.inventory[this.selectedItem].buffTime, true);
					}
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 603 && Main.cEd)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 669)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 115)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 425)
				{
					int num20 = Main.rand.Next(3);
					if (num20 == 0)
					{
						num20 = 27;
					}
					if (num20 == 1)
					{
						num20 = 101;
					}
					if (num20 == 2)
					{
						num20 = 102;
					}
					for (int num21 = 0; num21 < 22; num21++)
					{
						if (this.buffType[num21] == 27 || this.buffType[num21] == 101 || this.buffType[num21] == 102)
						{
							this.DelBuff(num21);
							num21--;
						}
					}
					this.AddBuff(num20, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 753)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 994)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1169)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1170)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1171)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1172)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1180)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1181)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1182)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1183)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1242)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1157)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1309)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1311)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1837)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1312)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1798)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1799)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1802)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1810)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1927)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 1959)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 2364)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 2365)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 2420)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 2535)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 2551)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 2584)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 2587)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].type == 2621)
				{
					this.AddBuff(this.inventory[this.selectedItem].buffType, 3600, true);
				}
				if (this.whoAmi == Main.myPlayer && this.gravDir == 1f && this.inventory[this.selectedItem].mountType != -1)
				{
					this.mount.SetMount(this.inventory[this.selectedItem].mountType, this, false);
				}
				if (this.inventory[this.selectedItem].type == 43 && Main.dayTime)
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].type == 544 && Main.dayTime)
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].type == 556 && Main.dayTime)
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].type == 557 && Main.dayTime)
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].type == 70 && !this.zoneEvil)
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].type == 1133 && !this.zoneJungle)
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].type == 1844 && (Main.dayTime || Main.pumpkinMoon || Main.snowMoon))
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].type == 1958 && (Main.dayTime || Main.pumpkinMoon || Main.snowMoon))
				{
					flag2 = false;
				}
				if (!this.SummonItemCheck())
				{
					flag2 = false;
				}
				if (this.inventory[this.selectedItem].shoot == 17 && flag2 && i == Main.myPlayer)
				{
					int num22 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
					int num23 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
					if (this.gravDir == -1f)
					{
						num23 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
					}
					Tile tile = Main.tile[num22, num23];
					if (tile.active() && (tile.type == 0 || tile.type == 2 || tile.type == 23 || tile.type == 109 || tile.type == 199))
					{
						WorldGen.KillTile(num22, num23, false, false, true);
						if (!Main.tile[num22, num23].active())
						{
							if (Main.netMode == 1)
							{
								NetMessage.SendData(17, -1, -1, "", 4, (float)num22, (float)num23, 0f, 0);
							}
						}
						else
						{
							flag2 = false;
						}
					}
					else
					{
						flag2 = false;
					}
				}
				if (flag2 && this.inventory[this.selectedItem].useAmmo > 0)
				{
					flag2 = false;
					for (int num24 = 0; num24 < 58; num24++)
					{
						if (this.inventory[num24].ammo == this.inventory[this.selectedItem].useAmmo && this.inventory[num24].stack > 0)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (flag2)
				{
					if (this.inventory[this.selectedItem].pick > 0 || this.inventory[this.selectedItem].axe > 0 || this.inventory[this.selectedItem].hammer > 0)
					{
						this.toolTime = 1;
					}
					if (this.grappling[0] > -1)
					{
						this.pulley = false;
						this.pulleyDir = 1;
						if (this.controlRight)
						{
							this.direction = 1;
						}
						else
						{
							if (this.controlLeft)
							{
								this.direction = -1;
							}
						}
					}
					this.channel = this.inventory[this.selectedItem].channel;
					this.attackCD = 0;
					if (this.inventory[this.selectedItem].melee)
					{
						this.itemAnimation = (int)((float)this.inventory[this.selectedItem].useAnimation * this.meleeSpeed);
						this.itemAnimationMax = (int)((float)this.inventory[this.selectedItem].useAnimation * this.meleeSpeed);
					}
					else
					{
						if (this.inventory[this.selectedItem].createTile >= 0)
						{
							this.itemAnimation = (int)((float)this.inventory[this.selectedItem].useAnimation * this.tileSpeed);
							this.itemAnimationMax = (int)((float)this.inventory[this.selectedItem].useAnimation * this.tileSpeed);
						}
						else
						{
							if (this.inventory[this.selectedItem].createWall >= 0)
							{
								this.itemAnimation = (int)((float)this.inventory[this.selectedItem].useAnimation * this.wallSpeed);
								this.itemAnimationMax = (int)((float)this.inventory[this.selectedItem].useAnimation * this.wallSpeed);
							}
							else
							{
								this.itemAnimation = this.inventory[this.selectedItem].useAnimation;
								this.itemAnimationMax = this.inventory[this.selectedItem].useAnimation;
								this.reuseDelay = this.inventory[this.selectedItem].reuseDelay;
							}
						}
					}
					if (this.inventory[this.selectedItem].useSound > 0)
					{
						Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, this.inventory[this.selectedItem].useSound);
					}
				}
				if (flag2 && this.whoAmi == Main.myPlayer && (this.inventory[this.selectedItem].shoot == 18 || this.inventory[this.selectedItem].shoot == 72 || this.inventory[this.selectedItem].shoot == 86 || this.inventory[this.selectedItem].shoot == 87 || Main.projPet[this.inventory[this.selectedItem].shoot]))
				{
					if ((this.inventory[this.selectedItem].shoot >= 191 && this.inventory[this.selectedItem].shoot <= 194) || this.inventory[this.selectedItem].shoot == 266 || this.inventory[this.selectedItem].shoot == 317 || this.inventory[this.selectedItem].shoot == 373 || this.inventory[this.selectedItem].shoot == 375 || this.inventory[this.selectedItem].shoot == 387 || this.inventory[this.selectedItem].shoot == 390 || this.inventory[this.selectedItem].shoot == 393 || this.inventory[this.selectedItem].shoot == 407)
					{
						List<int> list = new List<int>();
						float num25 = 0f;
						for (int num26 = 0; num26 < 1000; num26++)
						{
							if (Main.projectile[num26].active && Main.projectile[num26].owner == i && Main.projectile[num26].minion)
							{
								int num27;
								for (num27 = 0; num27 < list.Count; num27++)
								{
									if (Main.projectile[list[num27]].minionSlots > Main.projectile[num26].minionSlots)
									{
										list.Insert(num27, num26);
										break;
									}
								}
								if (num27 == list.Count)
								{
									list.Add(num26);
								}
								num25 += Main.projectile[num26].minionSlots;
							}
						}
						int arg_24AB_0 = this.inventory[this.selectedItem].shoot;
						float num28 = 1f;
						float num29 = 0f;
						int num30 = 388;
						int num31 = 0;
						while (num31 < list.Count && num25 - num29 > (float)this.maxMinions - num28)
						{
							int type = Main.projectile[list[num31]].type;
							if (type != num30)
							{
								num29 += Main.projectile[list[num31]].minionSlots;
								if (type == 388 && num30 == 387)
								{
									num30 = 388;
								}
								if (type == 387 && num30 == 388)
								{
									num30 = 387;
								}
								Main.projectile[list[num31]].Kill();
							}
							num31++;
						}
						list.Clear();
					}
					else
					{
						for (int num32 = 0; num32 < 1000; num32++)
						{
							if (Main.projectile[num32].active && Main.projectile[num32].owner == i && Main.projectile[num32].type == this.inventory[this.selectedItem].shoot)
							{
								Main.projectile[num32].Kill();
							}
							if (this.inventory[this.selectedItem].shoot == 72)
							{
								if (Main.projectile[num32].active && Main.projectile[num32].owner == i && Main.projectile[num32].type == 86)
								{
									Main.projectile[num32].Kill();
								}
								if (Main.projectile[num32].active && Main.projectile[num32].owner == i && Main.projectile[num32].type == 87)
								{
									Main.projectile[num32].Kill();
								}
							}
						}
					}
				}
			}
			if (!this.controlUseItem)
			{
				bool arg_267A_0 = this.channel;
				this.channel = false;
			}
			if (this.itemAnimation > 0)
			{
				if (this.inventory[this.selectedItem].melee)
				{
					this.itemAnimationMax = (int)((float)this.inventory[this.selectedItem].useAnimation * this.meleeSpeed);
				}
				else
				{
					this.itemAnimationMax = this.inventory[this.selectedItem].useAnimation;
				}
				if (this.inventory[this.selectedItem].mana > 0 && !flag && (this.inventory[this.selectedItem].type != 127 || !this.spaceGun))
				{
					this.manaRegenDelay = (int)this.maxRegenDelay;
				}
				if (Main.dedServ)
				{
					this.itemHeight = this.inventory[this.selectedItem].height;
					this.itemWidth = this.inventory[this.selectedItem].width;
				}
				else
				{
					this.itemHeight = Main.itemTexture[this.inventory[this.selectedItem].type].Height;
					this.itemWidth = Main.itemTexture[this.inventory[this.selectedItem].type].Width;
				}
				this.itemAnimation--;
				if (!Main.dedServ)
				{
					if (this.inventory[this.selectedItem].useStyle == 1)
					{
						if (this.inventory[this.selectedItem].type == 1827)
						{
							if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.333)
							{
								float num33 = 10f;
								this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num33) * (float)this.direction;
								this.itemLocation.Y = this.position.Y + 26f;
							}
							else
							{
								if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.666)
								{
									float num34 = 8f;
									this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num34) * (float)this.direction;
									num34 = 24f;
									this.itemLocation.Y = this.position.Y + num34;
								}
								else
								{
									float num35 = 6f;
									this.itemLocation.X = this.position.X + (float)this.width * 0.5f - ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num35) * (float)this.direction;
									num35 = 20f;
									this.itemLocation.Y = this.position.Y + num35;
								}
							}
							this.itemRotation = ((float)this.itemAnimation / (float)this.itemAnimationMax - 0.5f) * (float)(-(float)this.direction) * 3.5f - (float)this.direction * 0.3f;
						}
						else
						{
							if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.333)
							{
								float num36 = 10f;
								if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
								{
									num36 = 14f;
								}
								if (Main.itemTexture[this.inventory[this.selectedItem].type].Width >= 52)
								{
									num36 = 24f;
								}
								if (Main.itemTexture[this.inventory[this.selectedItem].type].Width >= 64)
								{
									num36 = 28f;
								}
								if (Main.itemTexture[this.inventory[this.selectedItem].type].Width >= 92)
								{
									num36 = 38f;
								}
								if (this.inventory[this.selectedItem].type == 2330 || this.inventory[this.selectedItem].type == 2320 || this.inventory[this.selectedItem].type == 2341)
								{
									num36 += 8f;
								}
								this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num36) * (float)this.direction;
								this.itemLocation.Y = this.position.Y + 24f;
							}
							else
							{
								if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.666)
								{
									float num37 = 10f;
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
									{
										num37 = 18f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Width >= 52)
									{
										num37 = 24f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Width >= 64)
									{
										num37 = 28f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Width >= 92)
									{
										num37 = 38f;
									}
									if (this.inventory[this.selectedItem].type == 2330 || this.inventory[this.selectedItem].type == 2320 || this.inventory[this.selectedItem].type == 2341)
									{
										num37 += 4f;
									}
									this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num37) * (float)this.direction;
									num37 = 10f;
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 32)
									{
										num37 = 8f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Height >= 32)
									{
										num37 = 12f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 64)
									{
										num37 = 14f;
									}
									if (this.inventory[this.selectedItem].type == 2330 || this.inventory[this.selectedItem].type == 2320 || this.inventory[this.selectedItem].type == 2341)
									{
										num37 += 4f;
									}
									this.itemLocation.Y = this.position.Y + num37;
								}
								else
								{
									float num38 = 6f;
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
									{
										num38 = 14f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Width >= 48)
									{
										num38 = 18f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Width >= 52)
									{
										num38 = 24f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Width >= 64)
									{
										num38 = 28f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Width >= 92)
									{
										num38 = 38f;
									}
									if (this.inventory[this.selectedItem].type == 2330 || this.inventory[this.selectedItem].type == 2320 || this.inventory[this.selectedItem].type == 2341)
									{
										num38 += 4f;
									}
									this.itemLocation.X = this.position.X + (float)this.width * 0.5f - ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num38) * (float)this.direction;
									num38 = 10f;
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 32)
									{
										num38 = 10f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 52)
									{
										num38 = 12f;
									}
									if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 64)
									{
										num38 = 14f;
									}
									if (this.inventory[this.selectedItem].type == 2330 || this.inventory[this.selectedItem].type == 2320 || this.inventory[this.selectedItem].type == 2341)
									{
										num38 += 4f;
									}
									this.itemLocation.Y = this.position.Y + num38;
								}
							}
							this.itemRotation = ((float)this.itemAnimation / (float)this.itemAnimationMax - 0.5f) * (float)(-(float)this.direction) * 3.5f - (float)this.direction * 0.3f;
						}
						if (this.gravDir == -1f)
						{
							this.itemRotation = -this.itemRotation;
							this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
						}
					}
					else
					{
						if (this.inventory[this.selectedItem].useStyle == 2)
						{
							this.itemRotation = (float)this.itemAnimation / (float)this.itemAnimationMax * (float)this.direction * 2f + -1.4f * (float)this.direction;
							if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.5)
							{
								this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 9f - this.itemRotation * 12f * (float)this.direction) * (float)this.direction;
								this.itemLocation.Y = this.position.Y + 38f + this.itemRotation * (float)this.direction * 4f;
							}
							else
							{
								this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 9f - this.itemRotation * 16f * (float)this.direction) * (float)this.direction;
								this.itemLocation.Y = this.position.Y + 38f + this.itemRotation * (float)this.direction;
							}
							if (this.gravDir == -1f)
							{
								this.itemRotation = -this.itemRotation;
								this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
							}
						}
						else
						{
							if (this.inventory[this.selectedItem].useStyle == 3)
							{
								if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.666)
								{
									this.itemLocation.X = -1000f;
									this.itemLocation.Y = -1000f;
									this.itemRotation = -1.3f * (float)this.direction;
								}
								else
								{
									this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 4f) * (float)this.direction;
									this.itemLocation.Y = this.position.Y + 24f;
									float num39 = (float)this.itemAnimation / (float)this.itemAnimationMax * (float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * (float)this.direction * this.inventory[this.selectedItem].scale * 1.2f - (float)(10 * this.direction);
									if (num39 > -4f && this.direction == -1)
									{
										num39 = -8f;
									}
									if (num39 < 4f && this.direction == 1)
									{
										num39 = 8f;
									}
									this.itemLocation.X = this.itemLocation.X - num39;
									this.itemRotation = 0.8f * (float)this.direction;
								}
								if (this.gravDir == -1f)
								{
									this.itemRotation = -this.itemRotation;
									this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
								}
							}
							else
							{
								if (this.inventory[this.selectedItem].useStyle == 4)
								{
									this.itemRotation = 0f;
									this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 9f - this.itemRotation * 14f * (float)this.direction - 4f) * (float)this.direction;
									this.itemLocation.Y = this.position.Y + (float)Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5f + 4f;
									if (this.gravDir == -1f)
									{
										this.itemRotation = -this.itemRotation;
										this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
									}
								}
								else
								{
									if (this.inventory[this.selectedItem].useStyle == 5)
									{
										this.itemLocation.X = this.position.X + (float)this.width * 0.5f - (float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - (float)(this.direction * 2);
										this.itemLocation.Y = this.position.Y + (float)this.height * 0.5f - (float)Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5f;
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (this.inventory[this.selectedItem].holdStyle == 1 && !this.pulley && !this.mount.Active)
				{
					if (Main.dedServ)
					{
						this.itemLocation.X = this.position.X + (float)this.width * 0.5f + 20f * (float)this.direction;
					}
					else
					{
						if (this.inventory[this.selectedItem].type == 930)
						{
							this.itemLocation.X = this.position.X + (float)(this.width / 2) * 0.5f - 12f - (float)(2 * this.direction);
							float num40 = this.position.X + (float)(this.width / 2) + (float)(38 * this.direction);
							if (this.direction == 1)
							{
								num40 -= 10f;
							}
							float num41 = this.position.Y + (float)(this.height / 2) - 4f * this.gravDir;
							if (this.gravDir == -1f)
							{
								num41 -= 8f;
							}
							int num42 = 0;
							for (int num43 = 54; num43 < 58; num43++)
							{
								if (this.inventory[num43].stack > 0 && this.inventory[num43].ammo == 931)
								{
									num42 = this.inventory[num43].type;
									break;
								}
							}
							if (num42 == 0)
							{
								for (int num44 = 0; num44 < 54; num44++)
								{
									if (this.inventory[num44].stack > 0 && this.inventory[num44].ammo == 931)
									{
										num42 = this.inventory[num44].type;
										break;
									}
								}
							}
							if (num42 == 931)
							{
								num42 = 127;
							}
							else
							{
								if (num42 == 1614)
								{
									num42 = 187;
								}
							}
							if (num42 > 0)
							{
								int num45 = Dust.NewDust(new Vector2(num40, num41 + this.gfxOffY), 6, 6, num42, 0f, 0f, 100, default(Color), 1.6f);
								Main.dust[num45].noGravity = true;
								Dust expr_38C3_cp_0 = Main.dust[num45];
								expr_38C3_cp_0.velocity.Y = expr_38C3_cp_0.velocity.Y - 4f * this.gravDir;
							}
						}
						else
						{
							if (this.inventory[this.selectedItem].type == 968)
							{
								this.itemLocation.X = this.position.X + (float)this.width * 0.5f + (float)(8 * this.direction);
								if (this.whoAmi == Main.myPlayer)
								{
									int num46 = (int)(this.itemLocation.X + (float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.8f * (float)this.direction) / 16;
									int num47 = (int)(this.itemLocation.Y + (float)(Main.itemTexture[this.inventory[this.selectedItem].type].Height / 2)) / 16;
									if (Main.tile[num46, num47] == null)
									{
										Main.tile[num46, num47] = new Tile();
									}
									if (Main.tile[num46, num47].active() && Main.tile[num46, num47].type == 215)
									{
										this.miscTimer++;
										if (Main.rand.Next(5) == 0)
										{
											this.miscTimer++;
										}
										if (this.miscTimer > 900)
										{
											this.miscTimer = 0;
											this.inventory[this.selectedItem].SetDefaults(969, false);
											if (this.selectedItem == 58)
											{
												Main.mouseItem.SetDefaults(969, false);
											}
											for (int num48 = 0; num48 < 58; num48++)
											{
												if (this.inventory[num48].type == this.inventory[this.selectedItem].type && num48 != this.selectedItem && this.inventory[num48].stack < this.inventory[num48].maxStack)
												{
													Main.PlaySound(7, -1, -1, 1);
													this.inventory[num48].stack++;
													this.inventory[this.selectedItem].SetDefaults(0, false);
													if (this.selectedItem == 58)
													{
														Main.mouseItem.SetDefaults(0, false);
													}
												}
											}
										}
									}
									else
									{
										this.miscTimer = 0;
									}
								}
							}
							else
							{
								if (this.inventory[this.selectedItem].type == 856)
								{
									this.itemLocation.X = this.position.X + (float)this.width * 0.5f + (float)(4 * this.direction);
								}
								else
								{
									if (this.inventory[this.selectedItem].fishingPole > 0)
									{
										this.itemLocation.X = this.position.X + (float)this.width * 0.5f + (float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.18f * (float)this.direction;
									}
									else
									{
										this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f + 2f) * (float)this.direction;
										if (this.inventory[this.selectedItem].type == 282 || this.inventory[this.selectedItem].type == 286)
										{
											this.itemLocation.X = this.itemLocation.X - (float)(this.direction * 2);
											this.itemLocation.Y = this.itemLocation.Y + 4f;
										}
									}
								}
							}
						}
					}
					this.itemLocation.Y = this.position.Y + 24f;
					if (this.inventory[this.selectedItem].type == 856)
					{
						this.itemLocation.Y = this.position.Y + 34f;
					}
					if (this.inventory[this.selectedItem].type == 930)
					{
						this.itemLocation.Y = this.position.Y + 9f;
					}
					if (this.inventory[this.selectedItem].fishingPole > 0)
					{
						this.itemLocation.Y = this.itemLocation.Y + 4f;
					}
					this.itemRotation = 0f;
					if (this.gravDir == -1f)
					{
						this.itemRotation = -this.itemRotation;
						this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
						if (this.inventory[this.selectedItem].type == 930)
						{
							this.itemLocation.Y = this.itemLocation.Y - 24f;
						}
					}
				}
				else
				{
					if (this.inventory[this.selectedItem].holdStyle == 2 && !this.pulley && !this.mount.Active)
					{
						if (this.inventory[this.selectedItem].type == 946)
						{
							this.itemRotation = 0f;
							this.itemLocation.X = this.position.X + (float)this.width * 0.5f - (float)(16 * this.direction);
							this.itemLocation.Y = this.position.Y + 22f;
							this.fallStart = (int)(this.position.Y / 16f);
							if (this.gravDir == -1f)
							{
								this.itemRotation = -this.itemRotation;
								this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
								if (this.velocity.Y < -2f)
								{
									this.velocity.Y = -2f;
								}
							}
							else
							{
								if (this.velocity.Y > 2f)
								{
									this.velocity.Y = 2f;
								}
							}
						}
						else
						{
							this.itemLocation.X = this.position.X + (float)this.width * 0.5f + (float)(6 * this.direction);
							this.itemLocation.Y = this.position.Y + 16f;
							this.itemRotation = 0.79f * (float)(-(float)this.direction);
							if (this.gravDir == -1f)
							{
								this.itemRotation = -this.itemRotation;
								this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
							}
						}
					}
					else
					{
						if (this.inventory[this.selectedItem].holdStyle == 3 && !this.pulley && !this.mount.Active && !Main.dedServ)
						{
							this.itemLocation.X = this.position.X + (float)this.width * 0.5f - (float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - (float)(this.direction * 2);
							this.itemLocation.Y = this.position.Y + (float)this.height * 0.5f - (float)Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5f;
							this.itemRotation = 0f;
						}
					}
				}
			}
			if ((((this.inventory[this.selectedItem].type == 974 || this.inventory[this.selectedItem].type == 8 || this.inventory[this.selectedItem].type == 1245 || this.inventory[this.selectedItem].type == 2274 || (this.inventory[this.selectedItem].type >= 427 && this.inventory[this.selectedItem].type <= 433)) && !this.wet) || this.inventory[this.selectedItem].type == 523 || this.inventory[this.selectedItem].type == 1333) && !this.pulley && !this.mount.Active)
			{
				float num49 = 1f;
				float num50 = 0.95f;
				float num51 = 0.8f;
				int num52 = 0;
				if (this.inventory[this.selectedItem].type == 523)
				{
					num52 = 8;
				}
				else
				{
					if (this.inventory[this.selectedItem].type == 974)
					{
						num52 = 9;
					}
					else
					{
						if (this.inventory[this.selectedItem].type == 1245)
						{
							num52 = 10;
						}
						else
						{
							if (this.inventory[this.selectedItem].type == 1333)
							{
								num52 = 11;
							}
							else
							{
								if (this.inventory[this.selectedItem].type == 2274)
								{
									num52 = 12;
								}
								else
								{
									if (this.inventory[this.selectedItem].type >= 427)
									{
										num52 = this.inventory[this.selectedItem].type - 426;
									}
								}
							}
						}
					}
				}
				if (num52 == 1)
				{
					num49 = 0f;
					num50 = 0.1f;
					num51 = 1.3f;
				}
				else
				{
					if (num52 == 2)
					{
						num49 = 1f;
						num50 = 0.1f;
						num51 = 0.1f;
					}
					else
					{
						if (num52 == 3)
						{
							num49 = 0f;
							num50 = 1f;
							num51 = 0.1f;
						}
						else
						{
							if (num52 == 4)
							{
								num49 = 0.9f;
								num50 = 0f;
								num51 = 0.9f;
							}
							else
							{
								if (num52 == 5)
								{
									num49 = 1.3f;
									num50 = 1.3f;
									num51 = 1.3f;
								}
								else
								{
									if (num52 == 6)
									{
										num49 = 0.9f;
										num50 = 0.9f;
										num51 = 0f;
									}
									else
									{
										if (num52 == 7)
										{
											num49 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
											num50 = 0.3f;
											num51 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
										}
										else
										{
											if (num52 == 8)
											{
												num51 = 0.7f;
												num49 = 0.85f;
												num50 = 1f;
											}
											else
											{
												if (num52 == 9)
												{
													num51 = 1f;
													num49 = 0.7f;
													num50 = 0.85f;
												}
												else
												{
													if (num52 == 10)
													{
														num51 = 0f;
														num49 = 1f;
														num50 = 0.5f;
													}
													else
													{
														if (num52 == 11)
														{
															num51 = 0.8f;
															num49 = 1.25f;
															num50 = 1.25f;
														}
														else
														{
															if (num52 == 12)
															{
																num49 *= 0.75f;
																num50 *= 1.3499999f;
																num51 *= 1.5f;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				int num53 = num52;
				if (num53 == 0)
				{
					num53 = 6;
				}
				else
				{
					if (num53 == 8)
					{
						num53 = 75;
					}
					else
					{
						if (num53 == 9)
						{
							num53 = 135;
						}
						else
						{
							if (num53 == 10)
							{
								num53 = 158;
							}
							else
							{
								if (num53 == 11)
								{
									num53 = 169;
								}
								else
								{
									if (num53 == 12)
									{
										num53 = 156;
									}
									else
									{
										num53 = 58 + num53;
									}
								}
							}
						}
					}
				}
				int maxValue = 30;
				if (this.itemAnimation > 0)
				{
					maxValue = 7;
				}
				if (this.direction == -1)
				{
					if (Main.rand.Next(maxValue) == 0)
					{
						int num54 = Dust.NewDust(new Vector2(this.itemLocation.X - 16f, this.itemLocation.Y - 14f * this.gravDir), 4, 4, num53, 0f, 0f, 100, default(Color), 1f);
						if (Main.rand.Next(3) != 0)
						{
							Main.dust[num54].noGravity = true;
						}
						Main.dust[num54].velocity *= 0.3f;
						Dust expr_457E_cp_0 = Main.dust[num54];
						expr_457E_cp_0.velocity.Y = expr_457E_cp_0.velocity.Y - 1.5f;
					}
					Lighting.addLight((int)((this.itemLocation.X - 12f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f + this.velocity.Y) / 16f), num49, num50, num51);
				}
				else
				{
					if (Main.rand.Next(maxValue) == 0)
					{
						int num55 = Dust.NewDust(new Vector2(this.itemLocation.X + 6f, this.itemLocation.Y - 14f * this.gravDir), 4, 4, num53, 0f, 0f, 100, default(Color), 1f);
						if (Main.rand.Next(3) != 0)
						{
							Main.dust[num55].noGravity = true;
						}
						Main.dust[num55].velocity *= 0.3f;
						Dust expr_4695_cp_0 = Main.dust[num55];
						expr_4695_cp_0.velocity.Y = expr_4695_cp_0.velocity.Y - 1.5f;
					}
					Lighting.addLight((int)((this.itemLocation.X + 12f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f + this.velocity.Y) / 16f), num49, num50, num51);
				}
			}
			if ((this.inventory[this.selectedItem].type == 105 || this.inventory[this.selectedItem].type == 713) && !this.wet && !this.pulley)
			{
				int maxValue2 = 20;
				if (this.itemAnimation > 0)
				{
					maxValue2 = 7;
				}
				if (this.direction == -1)
				{
					if (Main.rand.Next(maxValue2) == 0)
					{
						int num56 = Dust.NewDust(new Vector2(this.itemLocation.X - 12f, this.itemLocation.Y - 20f * this.gravDir), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
						if (Main.rand.Next(3) != 0)
						{
							Main.dust[num56].noGravity = true;
						}
						Main.dust[num56].velocity *= 0.3f;
						Dust expr_480A_cp_0 = Main.dust[num56];
						expr_480A_cp_0.velocity.Y = expr_480A_cp_0.velocity.Y - 1.5f;
					}
					Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f, 0.95f, 0.8f);
				}
				else
				{
					if (Main.rand.Next(maxValue2) == 0)
					{
						int num57 = Dust.NewDust(new Vector2(this.itemLocation.X + 4f, this.itemLocation.Y - 20f * this.gravDir), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
						if (Main.rand.Next(3) != 0)
						{
							Main.dust[num57].noGravity = true;
						}
						Main.dust[num57].velocity *= 0.3f;
						Dust expr_491D_cp_0 = Main.dust[num57];
						expr_491D_cp_0.velocity.Y = expr_491D_cp_0.velocity.Y - 1.5f;
					}
					Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f, 0.95f, 0.8f);
				}
			}
			else
			{
				if (this.inventory[this.selectedItem].type == 148 && !this.wet)
				{
					int maxValue3 = 10;
					if (this.itemAnimation > 0)
					{
						maxValue3 = 7;
					}
					if (this.direction == -1)
					{
						if (Main.rand.Next(maxValue3) == 0)
						{
							int num58 = Dust.NewDust(new Vector2(this.itemLocation.X - 12f, this.itemLocation.Y - 20f * this.gravDir), 4, 4, 172, 0f, 0f, 100, default(Color), 1f);
							if (Main.rand.Next(3) != 0)
							{
								Main.dust[num58].noGravity = true;
							}
							Main.dust[num58].velocity *= 0.3f;
							Dust expr_4A77_cp_0 = Main.dust[num58];
							expr_4A77_cp_0.velocity.Y = expr_4A77_cp_0.velocity.Y - 1.5f;
						}
						Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 0f, 0.5f, 1f);
					}
					else
					{
						if (Main.rand.Next(maxValue3) == 0)
						{
							int num59 = Dust.NewDust(new Vector2(this.itemLocation.X + 4f, this.itemLocation.Y - 20f * this.gravDir), 4, 4, 172, 0f, 0f, 100, default(Color), 1f);
							if (Main.rand.Next(3) != 0)
							{
								Main.dust[num59].noGravity = true;
							}
							Main.dust[num59].velocity *= 0.3f;
							Dust expr_4B8E_cp_0 = Main.dust[num59];
							expr_4B8E_cp_0.velocity.Y = expr_4B8E_cp_0.velocity.Y - 1.5f;
						}
						Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 0f, 0.5f, 1f);
					}
				}
			}
			if (this.inventory[this.selectedItem].type == 282 && !this.pulley)
			{
				if (this.direction == -1)
				{
					Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 0.7f, 1f, 0.8f);
				}
				else
				{
					Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 0.7f, 1f, 0.8f);
				}
			}
			if (this.inventory[this.selectedItem].type == 286 && !this.pulley)
			{
				if (this.direction == -1)
				{
					Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 0.7f, 0.8f, 1f);
				}
				else
				{
					Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 0.7f, 0.8f, 1f);
				}
			}
			if (this.controlUseItem)
			{
				this.releaseUseItem = false;
			}
			else
			{
				this.releaseUseItem = true;
			}
			if (this.itemTime > 0)
			{
				this.itemTime--;
				if (this.itemTime == 0 && this.whoAmi == Main.myPlayer)
				{
					int type2 = this.inventory[this.selectedItem].type;
					if (type2 == 65 || type2 == 676 || type2 == 723 || type2 == 724 || type2 == 989 || type2 == 1226 || type2 == 1227)
					{
						Main.PlaySound(25, -1, -1, 1);
						for (int num60 = 0; num60 < 5; num60++)
						{
							int num61 = Dust.NewDust(this.position, this.width, this.height, 45, 0f, 0f, 255, default(Color), (float)Main.rand.Next(20, 26) * 0.1f);
							Main.dust[num61].noLight = true;
							Main.dust[num61].noGravity = true;
							Main.dust[num61].velocity *= 0.5f;
						}
					}
				}
			}
			if (i == Main.myPlayer)
			{
				bool flag6 = true;
				int type3 = this.inventory[this.selectedItem].type;
				if ((type3 == 65 || type3 == 676 || type3 == 723 || type3 == 724 || type3 == 757 || type3 == 674 || type3 == 675 || type3 == 989 || type3 == 1226 || type3 == 1227) && this.itemAnimation != this.itemAnimationMax - 1)
				{
					flag6 = false;
				}
				if (this.inventory[this.selectedItem].shoot > 0 && this.itemAnimation > 0 && this.itemTime == 0 && flag6)
				{
					int num62 = this.inventory[this.selectedItem].shoot;
					float num63 = this.inventory[this.selectedItem].shootSpeed;
					if (this.inventory[this.selectedItem].melee && num62 != 25 && num62 != 26 && num62 != 35)
					{
						num63 /= this.meleeSpeed;
					}
					bool flag7 = false;
					int num64 = num2;
					float num65 = this.inventory[this.selectedItem].knockBack;
					if (num62 == 13 || num62 == 32 || num62 == 315 || (num62 >= 230 && num62 <= 235) || num62 == 331)
					{
						this.grappling[0] = -1;
						this.grapCount = 0;
						for (int num66 = 0; num66 < 1000; num66++)
						{
							if (Main.projectile[num66].active && Main.projectile[num66].owner == i)
							{
								if (Main.projectile[num66].type == 13)
								{
									Main.projectile[num66].Kill();
								}
								if (Main.projectile[num66].type == 331)
								{
									Main.projectile[num66].Kill();
								}
								if (Main.projectile[num66].type == 315)
								{
									Main.projectile[num66].Kill();
								}
								if (Main.projectile[num66].type >= 230 && Main.projectile[num66].type <= 235)
								{
									Main.projectile[num66].Kill();
								}
							}
						}
					}
					if (this.inventory[this.selectedItem].useAmmo > 0)
					{
						Item item2 = new Item();
						bool flag8 = false;
						for (int num67 = 54; num67 < 58; num67++)
						{
							if (this.inventory[num67].ammo == this.inventory[this.selectedItem].useAmmo && this.inventory[num67].stack > 0)
							{
								item2 = this.inventory[num67];
								flag7 = true;
								flag8 = true;
								break;
							}
						}
						if (!flag8)
						{
							for (int num68 = 0; num68 < 54; num68++)
							{
								if (this.inventory[num68].ammo == this.inventory[this.selectedItem].useAmmo && this.inventory[num68].stack > 0)
								{
									item2 = this.inventory[num68];
									flag7 = true;
									break;
								}
							}
						}
						if (flag7)
						{
							if (this.inventory[this.selectedItem].type == 1946)
							{
								num62 = 338 + item2.type - 771;
							}
							else
							{
								if (this.inventory[this.selectedItem].useAmmo == 771)
								{
									num62 += item2.shoot;
								}
								else
								{
									if (this.inventory[this.selectedItem].useAmmo == 780)
									{
										num62 += item2.shoot;
									}
									else
									{
										if (item2.shoot > 0)
										{
											num62 = item2.shoot;
										}
									}
								}
							}
							if (num62 == 42)
							{
								if (item2.type == 370)
								{
									num62 = 65;
									num64 += 5;
								}
								else
								{
									if (item2.type == 408)
									{
										num62 = 68;
										num64 += 5;
									}
									else
									{
										if (item2.type == 1246)
										{
											num62 = 354;
											num64 += 5;
										}
									}
								}
							}
							if (this.magicQuiver && (this.inventory[this.selectedItem].useAmmo == 1 || this.inventory[this.selectedItem].useAmmo == 323))
							{
								num65 = (float)((int)((double)num65 * 1.1));
								num63 *= 1.1f;
							}
							num63 += item2.shootSpeed;
							if (item2.ranged)
							{
								if (item2.damage > 0)
								{
									num64 += (int)((float)item2.damage * this.rangedDamage);
								}
							}
							else
							{
								num64 += item2.damage;
							}
							if (this.inventory[this.selectedItem].useAmmo == 1 && this.archery)
							{
								if (num63 < 20f)
								{
									num63 *= 1.2f;
									if (num63 > 20f)
									{
										num63 = 20f;
									}
								}
								num64 = (int)((double)((float)num64) * 1.2);
							}
							num65 += item2.knockBack;
							bool flag9 = false;
							if (this.magicQuiver && this.inventory[this.selectedItem].useAmmo == 1 && Main.rand.Next(5) == 0)
							{
								flag9 = true;
							}
							if (this.ammoBox && Main.rand.Next(5) == 0)
							{
								flag9 = true;
							}
							if (this.ammoPotion && Main.rand.Next(5) == 0)
							{
								flag9 = true;
							}
							if (this.inventory[this.selectedItem].type == 1782 && Main.rand.Next(3) == 0)
							{
								flag9 = true;
							}
							if (this.inventory[this.selectedItem].type == 98 && Main.rand.Next(3) == 0)
							{
								flag9 = true;
							}
							if (this.inventory[this.selectedItem].type == 2270 && Main.rand.Next(2) == 0)
							{
								flag9 = true;
							}
							if (this.inventory[this.selectedItem].type == 533 && Main.rand.Next(2) == 0)
							{
								flag9 = true;
							}
							if (this.inventory[this.selectedItem].type == 1929 && Main.rand.Next(2) == 0)
							{
								flag9 = true;
							}
							if (this.inventory[this.selectedItem].type == 1553 && Main.rand.Next(2) == 0)
							{
								flag9 = true;
							}
							if (this.inventory[this.selectedItem].type == 434 && this.itemAnimation < this.inventory[this.selectedItem].useAnimation - 2)
							{
								flag9 = true;
							}
							if (this.ammoCost80 && Main.rand.Next(5) == 0)
							{
								flag9 = true;
							}
							if (this.ammoCost75 && Main.rand.Next(4) == 0)
							{
								flag9 = true;
							}
							if (num62 == 85 && this.itemAnimation < this.itemAnimationMax - 6)
							{
								flag9 = true;
							}
							if ((num62 == 145 || num62 == 146 || num62 == 147 || num62 == 148 || num62 == 149) && this.itemAnimation < this.itemAnimationMax - 5)
							{
								flag9 = true;
							}
							if (!flag9)
							{
								item2.stack--;
								if (item2.stack <= 0)
								{
									item2.active = false;
									item2.name = "";
									item2.type = 0;
								}
							}
						}
					}
					else
					{
						flag7 = true;
					}
					if (this.inventory[this.selectedItem].type == 71)
					{
						flag7 = false;
					}
					if (this.inventory[this.selectedItem].type == 72)
					{
						flag7 = false;
					}
					if (this.inventory[this.selectedItem].type == 73)
					{
						flag7 = false;
					}
					if (this.inventory[this.selectedItem].type == 74)
					{
						flag7 = false;
					}
					if (this.inventory[this.selectedItem].type == 1254 && num62 == 14)
					{
						num62 = 242;
					}
					if (this.inventory[this.selectedItem].type == 1255 && num62 == 14)
					{
						num62 = 242;
					}
					if (this.inventory[this.selectedItem].type == 1265 && num62 == 14)
					{
						num62 = 242;
					}
					if (num62 == 73)
					{
						for (int num69 = 0; num69 < 1000; num69++)
						{
							if (Main.projectile[num69].active && Main.projectile[num69].owner == i)
							{
								if (Main.projectile[num69].type == 73)
								{
									num62 = 74;
								}
								if (num62 == 74 && Main.projectile[num69].type == 74)
								{
									flag7 = false;
								}
							}
						}
					}
					if (flag7)
					{
						if (this.inventory[this.selectedItem].summon)
						{
							num65 += this.minionKB;
							num64 = (int)((float)num64 * this.minionDamage);
						}
						if (num62 == 228)
						{
							num65 = 0f;
						}
						if (this.inventory[this.selectedItem].melee && this.kbGlove)
						{
							num65 *= 2f;
						}
						if (this.kbBuff)
						{
							num65 *= 1.5f;
						}
						if (this.inventory[this.selectedItem].ranged && this.armorSteath)
						{
							num65 *= 1f + (1f - this.stealth) * 0.5f;
						}
						if (num62 == 1 && this.inventory[this.selectedItem].type == 120)
						{
							num62 = 2;
						}
						if (this.inventory[this.selectedItem].type == 682)
						{
							num62 = 117;
						}
						if (this.inventory[this.selectedItem].type == 725)
						{
							num62 = 120;
						}
						if (this.inventory[this.selectedItem].type == 2223)
						{
							num62 = 357;
						}
						this.itemTime = this.inventory[this.selectedItem].useTime;
						if ((float)Main.mouseX + Main.screenPosition.X > this.position.X + (float)this.width * 0.5f)
						{
							this.ChangeDir(1);
						}
						else
						{
							this.ChangeDir(-1);
						}
						Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
						if (num62 == 9)
						{
							vector = new Vector2(this.position.X + (float)this.width * 0.5f + (float)(Main.rand.Next(201) * -(float)this.direction) + ((float)Main.mouseX + Main.screenPosition.X - this.position.X), this.position.Y + (float)this.height * 0.5f - 600f);
							num65 = 0f;
							num64 *= 2;
						}
						else
						{
							if (num62 == 51)
							{
								vector.Y -= 6f * this.gravDir;
							}
						}
						float num70 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
						float num71 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
						if (this.gravDir == -1f)
						{
							num71 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector.Y;
						}
						float num72 = (float)Math.Sqrt((double)(num70 * num70 + num71 * num71));
						float num73 = num72;
						num72 = num63 / num72;
						if (this.inventory[this.selectedItem].type == 1929 || this.inventory[this.selectedItem].type == 2270)
						{
							num70 += (float)Main.rand.Next(-50, 51) * 0.03f / num72;
							num71 += (float)Main.rand.Next(-50, 51) * 0.03f / num72;
						}
						num70 *= num72;
						num71 *= num72;
						if (this.inventory[this.selectedItem].type == 757)
						{
							num64 = (int)((float)num64 * 1.25f);
						}
						if (num62 == 250)
						{
							for (int num74 = 0; num74 < 1000; num74++)
							{
								if (Main.projectile[num74].active && Main.projectile[num74].owner == this.whoAmi && (Main.projectile[num74].type == 250 || Main.projectile[num74].type == 251))
								{
									Main.projectile[num74].Kill();
								}
							}
						}
						if (num62 == 12)
						{
							vector.X += num70 * 3f;
							vector.Y += num71 * 3f;
						}
						if (this.inventory[this.selectedItem].useStyle == 5)
						{
							this.itemRotation = (float)Math.Atan2((double)(num71 * (float)this.direction), (double)(num70 * (float)this.direction));
							NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
							NetMessage.SendData(41, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
						}
						if (num62 == 17)
						{
							vector.X = (float)Main.mouseX + Main.screenPosition.X;
							vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
							if (this.gravDir == -1f)
							{
								vector.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
							}
						}
						if (num62 == 76)
						{
							num62 += Main.rand.Next(3);
							num73 /= (float)(Main.screenHeight / 2);
							if (num73 > 1f)
							{
								num73 = 1f;
							}
							float num75 = num70 + (float)Main.rand.Next(-40, 41) * 0.01f;
							float num76 = num71 + (float)Main.rand.Next(-40, 41) * 0.01f;
							num75 *= num73 + 0.25f;
							num76 *= num73 + 0.25f;
							int num77 = Projectile.NewProjectile(vector.X, vector.Y, num75, num76, num62, num64, num65, i, 0f, 0f);
							Main.projectile[num77].ai[1] = 1f;
							num73 = num73 * 2f - 1f;
							if (num73 < -1f)
							{
								num73 = -1f;
							}
							if (num73 > 1f)
							{
								num73 = 1f;
							}
							Main.projectile[num77].ai[0] = num73;
							NetMessage.SendData(27, -1, -1, "", num77, 0f, 0f, 0f, 0);
						}
						else
						{
							if (this.inventory[this.selectedItem].type == 98 || this.inventory[this.selectedItem].type == 533)
							{
								float speedX = num70 + (float)Main.rand.Next(-40, 41) * 0.01f;
								float speedY = num71 + (float)Main.rand.Next(-40, 41) * 0.01f;
								Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, num62, num64, num65, i, 0f, 0f);
							}
							else
							{
								if (this.inventory[this.selectedItem].type == 2270)
								{
									float num78 = num70 + (float)Main.rand.Next(-40, 41) * 0.05f;
									float num79 = num71 + (float)Main.rand.Next(-40, 41) * 0.05f;
									if (Main.rand.Next(3) == 0)
									{
										num78 *= 1f + (float)Main.rand.Next(-30, 31) * 0.02f;
										num79 *= 1f + (float)Main.rand.Next(-30, 31) * 0.02f;
									}
									Projectile.NewProjectile(vector.X, vector.Y, num78, num79, num62, num64, num65, i, 0f, 0f);
								}
								else
								{
									if (this.inventory[this.selectedItem].type == 1930)
									{
										int num80 = 2 + Main.rand.Next(3);
										for (int num81 = 0; num81 < num80; num81++)
										{
											float num82 = num70;
											float num83 = num71;
											float num84 = 0.025f * (float)num81;
											num82 += (float)Main.rand.Next(-35, 36) * num84;
											num83 += (float)Main.rand.Next(-35, 36) * num84;
											num72 = (float)Math.Sqrt((double)(num82 * num82 + num83 * num83));
											num72 = num63 / num72;
											num82 *= num72;
											num83 *= num72;
											float x = vector.X + num70 * (float)(num80 - num81) * 1.75f;
											float y = vector.Y + num71 * (float)(num80 - num81) * 1.75f;
											Projectile.NewProjectile(x, y, num82, num83, num62, num64, num65, i, (float)Main.rand.Next(0, 10 * (num81 + 1)), 0f);
										}
									}
									else
									{
										if (this.inventory[this.selectedItem].type == 1931)
										{
											int num85 = 2;
											for (int num86 = 0; num86 < num85; num86++)
											{
												vector = new Vector2(this.position.X + (float)this.width * 0.5f + (float)(Main.rand.Next(201) * -(float)this.direction) + ((float)Main.mouseX + Main.screenPosition.X - this.position.X), this.position.Y + (float)this.height * 0.5f - 600f);
												vector.X = (vector.X + this.center().X) / 2f + (float)Main.rand.Next(-200, 201);
												vector.Y -= (float)(100 * num86);
												num70 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
												num71 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
												if (num71 < 0f)
												{
													num71 *= -1f;
												}
												if (num71 < 20f)
												{
													num71 = 20f;
												}
												num72 = (float)Math.Sqrt((double)(num70 * num70 + num71 * num71));
												num72 = num63 / num72;
												num70 *= num72;
												num71 *= num72;
												float speedX2 = num70 + (float)Main.rand.Next(-40, 41) * 0.02f;
												float speedY2 = num71 + (float)Main.rand.Next(-40, 41) * 0.02f;
												Projectile.NewProjectile(vector.X, vector.Y, speedX2, speedY2, num62, num64, num65, i, 0f, (float)Main.rand.Next(5));
											}
										}
										else
										{
											if (this.inventory[this.selectedItem].type == 2624)
											{
												float num87 = 0.314159274f;
												int num88 = 5;
												Vector2 vector2 = new Vector2(num70, num71);
												vector2.Normalize();
												vector2 *= 40f;
												for (int num89 = 0; num89 < num88; num89++)
												{
													float num90 = (float)num89 - ((float)num88 - 1f) / 2f;
													Vector2 vector3 = vector2.Rotate((double)(num87 * num90), default(Vector2));
													int num91 = Projectile.NewProjectile(vector.X + vector3.X, vector.Y + vector3.Y, num70, num71, num62, num64, num65, i, 0f, 0f);
													Main.projectile[num91].noDropItem = true;
												}
											}
											else
											{
												if (this.inventory[this.selectedItem].type == 1929)
												{
													float speedX3 = num70 + (float)Main.rand.Next(-40, 41) * 0.03f;
													float speedY3 = num71 + (float)Main.rand.Next(-40, 41) * 0.03f;
													Projectile.NewProjectile(vector.X, vector.Y, speedX3, speedY3, num62, num64, num65, i, 0f, 0f);
												}
												else
												{
													if (this.inventory[this.selectedItem].type == 1553)
													{
														float speedX4 = num70 + (float)Main.rand.Next(-40, 41) * 0.005f;
														float speedY4 = num71 + (float)Main.rand.Next(-40, 41) * 0.005f;
														Projectile.NewProjectile(vector.X, vector.Y, speedX4, speedY4, num62, num64, num65, i, 0f, 0f);
													}
													else
													{
														if (this.inventory[this.selectedItem].type == 518)
														{
															float num92 = num70;
															float num93 = num71;
															num92 += (float)Main.rand.Next(-40, 41) * 0.04f;
															num93 += (float)Main.rand.Next(-40, 41) * 0.04f;
															Projectile.NewProjectile(vector.X, vector.Y, num92, num93, num62, num64, num65, i, 0f, 0f);
														}
														else
														{
															if (this.inventory[this.selectedItem].type == 1265)
															{
																float num94 = num70;
																float num95 = num71;
																num94 += (float)Main.rand.Next(-30, 31) * 0.03f;
																num95 += (float)Main.rand.Next(-30, 31) * 0.03f;
																Projectile.NewProjectile(vector.X, vector.Y, num94, num95, num62, num64, num65, i, 0f, 0f);
															}
															else
															{
																if (this.inventory[this.selectedItem].type == 534)
																{
																	for (int num96 = 0; num96 < 4; num96++)
																	{
																		float num97 = num70;
																		float num98 = num71;
																		num97 += (float)Main.rand.Next(-40, 41) * 0.05f;
																		num98 += (float)Main.rand.Next(-40, 41) * 0.05f;
																		Projectile.NewProjectile(vector.X, vector.Y, num97, num98, num62, num64, num65, i, 0f, 0f);
																	}
																}
																else
																{
																	if (this.inventory[this.selectedItem].type == 2188)
																	{
																		int num99 = 4;
																		if (Main.rand.Next(3) == 0)
																		{
																			num99++;
																		}
																		if (Main.rand.Next(4) == 0)
																		{
																			num99++;
																		}
																		if (Main.rand.Next(5) == 0)
																		{
																			num99++;
																		}
																		for (int num100 = 0; num100 < num99; num100++)
																		{
																			float num101 = num70;
																			float num102 = num71;
																			float num103 = 0.05f * (float)num100;
																			num101 += (float)Main.rand.Next(-35, 36) * num103;
																			num102 += (float)Main.rand.Next(-35, 36) * num103;
																			num72 = (float)Math.Sqrt((double)(num101 * num101 + num102 * num102));
																			num72 = num63 / num72;
																			num101 *= num72;
																			num102 *= num72;
																			float x2 = vector.X;
																			float y2 = vector.Y;
																			Projectile.NewProjectile(x2, y2, num101, num102, num62, num64, num65, i, 0f, 0f);
																		}
																	}
																	else
																	{
																		if (this.inventory[this.selectedItem].type == 1308)
																		{
																			int num104 = 3;
																			if (Main.rand.Next(3) == 0)
																			{
																				num104++;
																			}
																			for (int num105 = 0; num105 < num104; num105++)
																			{
																				float num106 = num70;
																				float num107 = num71;
																				float num108 = 0.05f * (float)num105;
																				num106 += (float)Main.rand.Next(-35, 36) * num108;
																				num107 += (float)Main.rand.Next(-35, 36) * num108;
																				num72 = (float)Math.Sqrt((double)(num106 * num106 + num107 * num107));
																				num72 = num63 / num72;
																				num106 *= num72;
																				num107 *= num72;
																				float x3 = vector.X;
																				float y3 = vector.Y;
																				Projectile.NewProjectile(x3, y3, num106, num107, num62, num64, num65, i, 0f, 0f);
																			}
																		}
																		else
																		{
																			if (this.inventory[this.selectedItem].type == 1258)
																			{
																				float num109 = num70;
																				float num110 = num71;
																				num109 += (float)Main.rand.Next(-40, 41) * 0.01f;
																				num110 += (float)Main.rand.Next(-40, 41) * 0.01f;
																				vector.X += (float)Main.rand.Next(-40, 41) * 0.05f;
																				vector.Y += (float)Main.rand.Next(-45, 36) * 0.05f;
																				Projectile.NewProjectile(vector.X, vector.Y, num109, num110, num62, num64, num65, i, 0f, 0f);
																			}
																			else
																			{
																				if (this.inventory[this.selectedItem].type == 964)
																				{
																					for (int num111 = 0; num111 < 3; num111++)
																					{
																						float num112 = num70;
																						float num113 = num71;
																						num112 += (float)Main.rand.Next(-35, 36) * 0.04f;
																						num113 += (float)Main.rand.Next(-35, 36) * 0.04f;
																						Projectile.NewProjectile(vector.X, vector.Y, num112, num113, num62, num64, num65, i, 0f, 0f);
																					}
																				}
																				else
																				{
																					if (this.inventory[this.selectedItem].type == 1569)
																					{
																						int num114 = 4;
																						if (Main.rand.Next(2) == 0)
																						{
																							num114++;
																						}
																						if (Main.rand.Next(4) == 0)
																						{
																							num114++;
																						}
																						if (Main.rand.Next(8) == 0)
																						{
																							num114++;
																						}
																						if (Main.rand.Next(16) == 0)
																						{
																							num114++;
																						}
																						for (int num115 = 0; num115 < num114; num115++)
																						{
																							float num116 = num70;
																							float num117 = num71;
																							float num118 = 0.05f * (float)num115;
																							num116 += (float)Main.rand.Next(-35, 36) * num118;
																							num117 += (float)Main.rand.Next(-35, 36) * num118;
																							num72 = (float)Math.Sqrt((double)(num116 * num116 + num117 * num117));
																							num72 = num63 / num72;
																							num116 *= num72;
																							num117 *= num72;
																							float x4 = vector.X;
																							float y4 = vector.Y;
																							Projectile.NewProjectile(x4, y4, num116, num117, num62, num64, num65, i, 0f, 0f);
																						}
																					}
																					else
																					{
																						if (this.inventory[this.selectedItem].type == 1572 || this.inventory[this.selectedItem].type == 2366)
																						{
																							int num119 = 308;
																							if (this.inventory[this.selectedItem].type == 2366)
																							{
																								num119 = 377;
																							}
																							for (int num120 = 0; num120 < 1000; num120++)
																							{
																								if (Main.projectile[num120].owner == this.whoAmi && Main.projectile[num120].type == num119)
																								{
																									Main.projectile[num120].Kill();
																								}
																							}
																							int num121 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
																							int num122 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
																							if (this.gravDir == -1f)
																							{
																								num122 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
																							}
																							while (num122 < Main.maxTilesY - 10 && Main.tile[num121, num122] != null && !WorldGen.SolidTile(num121, num122) && Main.tile[num121 - 1, num122] != null && !WorldGen.SolidTile(num121 - 1, num122) && Main.tile[num121 + 1, num122] != null && !WorldGen.SolidTile(num121 + 1, num122))
																							{
																								num122++;
																							}
																							num122--;
																							Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, (float)(num122 * 16 - 24), 0f, 15f, num62, num64, num65, i, 0f, 0f);
																						}
																						else
																						{
																							if (this.inventory[this.selectedItem].type == 1244 || this.inventory[this.selectedItem].type == 1256)
																							{
																								int num123 = Projectile.NewProjectile(vector.X, vector.Y, num70, num71, num62, num64, num65, i, 0f, 0f);
																								Main.projectile[num123].ai[0] = (float)Main.mouseX + Main.screenPosition.X;
																								Main.projectile[num123].ai[1] = (float)Main.mouseY + Main.screenPosition.Y;
																							}
																							else
																							{
																								if (this.inventory[this.selectedItem].type == 1229)
																								{
																									int num124 = Main.rand.Next(2, 4);
																									if (Main.rand.Next(5) == 0)
																									{
																										num124++;
																									}
																									for (int num125 = 0; num125 < num124; num125++)
																									{
																										float num126 = num70;
																										float num127 = num71;
																										if (num125 > 0)
																										{
																											num126 += (float)Main.rand.Next(-35, 36) * 0.04f;
																											num127 += (float)Main.rand.Next(-35, 36) * 0.04f;
																										}
																										if (num125 > 1)
																										{
																											num126 += (float)Main.rand.Next(-35, 36) * 0.04f;
																											num127 += (float)Main.rand.Next(-35, 36) * 0.04f;
																										}
																										if (num125 > 2)
																										{
																											num126 += (float)Main.rand.Next(-35, 36) * 0.04f;
																											num127 += (float)Main.rand.Next(-35, 36) * 0.04f;
																										}
																										int num128 = Projectile.NewProjectile(vector.X, vector.Y, num126, num127, num62, num64, num65, i, 0f, 0f);
																										Main.projectile[num128].noDropItem = true;
																									}
																								}
																								else
																								{
																									if (this.inventory[this.selectedItem].type == 1121 || this.inventory[this.selectedItem].type == 1155)
																									{
																										int num129;
																										if (this.inventory[this.selectedItem].type == 1121)
																										{
																											num129 = Main.rand.Next(1, 4);
																											if (Main.rand.Next(6) == 0)
																											{
																												num129++;
																											}
																											if (Main.rand.Next(6) == 0)
																											{
																												num129++;
																											}
																										}
																										else
																										{
																											num129 = Main.rand.Next(2, 5);
																											if (Main.rand.Next(5) == 0)
																											{
																												num129++;
																											}
																											if (Main.rand.Next(5) == 0)
																											{
																												num129++;
																											}
																										}
																										for (int num130 = 0; num130 < num129; num130++)
																										{
																											float num131 = num70;
																											float num132 = num71;
																											num131 += (float)Main.rand.Next(-35, 36) * 0.02f;
																											num132 += (float)Main.rand.Next(-35, 36) * 0.02f;
																											Projectile.NewProjectile(vector.X, vector.Y, num131, num132, num62, num64, num65, i, 0f, 0f);
																										}
																									}
																									else
																									{
																										if (this.inventory[this.selectedItem].type == 1801)
																										{
																											int num133 = Main.rand.Next(1, 4);
																											for (int num134 = 0; num134 < num133; num134++)
																											{
																												float num135 = num70;
																												float num136 = num71;
																												num135 += (float)Main.rand.Next(-35, 36) * 0.05f;
																												num136 += (float)Main.rand.Next(-35, 36) * 0.05f;
																												Projectile.NewProjectile(vector.X, vector.Y, num135, num136, num62, num64, num65, i, 0f, 0f);
																											}
																										}
																										else
																										{
																											if (this.inventory[this.selectedItem].type == 679)
																											{
																												for (int num137 = 0; num137 < 6; num137++)
																												{
																													float num138 = num70;
																													float num139 = num71;
																													num138 += (float)Main.rand.Next(-40, 41) * 0.05f;
																													num139 += (float)Main.rand.Next(-40, 41) * 0.05f;
																													Projectile.NewProjectile(vector.X, vector.Y, num138, num139, num62, num64, num65, i, 0f, 0f);
																												}
																											}
																											else
																											{
																												if (this.inventory[this.selectedItem].type == 2623)
																												{
																													for (int num140 = 0; num140 < 3; num140++)
																													{
																														float num141 = num70;
																														float num142 = num71;
																														num141 += (float)Main.rand.Next(-40, 41) * 0.1f;
																														num142 += (float)Main.rand.Next(-40, 41) * 0.1f;
																														Projectile.NewProjectile(vector.X, vector.Y, num141, num142, num62, num64, num65, i, 0f, 0f);
																													}
																												}
																												else
																												{
																													if (this.inventory[this.selectedItem].type == 434)
																													{
																														float num143 = num70;
																														float num144 = num71;
																														if (this.itemAnimation < 5)
																														{
																															num143 += (float)Main.rand.Next(-40, 41) * 0.01f;
																															num144 += (float)Main.rand.Next(-40, 41) * 0.01f;
																															num143 *= 1.1f;
																															num144 *= 1.1f;
																														}
																														else
																														{
																															if (this.itemAnimation < 10)
																															{
																																num143 += (float)Main.rand.Next(-20, 21) * 0.01f;
																																num144 += (float)Main.rand.Next(-20, 21) * 0.01f;
																																num143 *= 1.05f;
																																num144 *= 1.05f;
																															}
																														}
																														Projectile.NewProjectile(vector.X, vector.Y, num143, num144, num62, num64, num65, i, 0f, 0f);
																													}
																													else
																													{
																														if (this.inventory[this.selectedItem].type == 1157)
																														{
																															num62 = Main.rand.Next(191, 195);
																															num70 = 0f;
																															num71 = 0f;
																															vector.X = (float)Main.mouseX + Main.screenPosition.X;
																															vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
																															int num145 = Projectile.NewProjectile(vector.X, vector.Y, num70, num71, num62, num64, num65, i, 0f, 0f);
																															Main.projectile[num145].localAI[0] = 30f;
																														}
																														else
																														{
																															if (this.inventory[this.selectedItem].type == 1802)
																															{
																																num70 = 0f;
																																num71 = 0f;
																																vector.X = (float)Main.mouseX + Main.screenPosition.X;
																																vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
																																Projectile.NewProjectile(vector.X, vector.Y, num70, num71, num62, num64, num65, i, 0f, 0f);
																															}
																															else
																															{
																																if (this.inventory[this.selectedItem].type == 2364 || this.inventory[this.selectedItem].type == 2365)
																																{
																																	num70 = 0f;
																																	num71 = 0f;
																																	vector.X = (float)Main.mouseX + Main.screenPosition.X;
																																	vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
																																	Projectile.NewProjectile(vector.X, vector.Y, num70, num71, num62, num64, num65, i, 0f, 0f);
																																}
																																else
																																{
																																	if (this.inventory[this.selectedItem].type == 2535)
																																	{
																																		num70 = 0f;
																																		num71 = 0f;
																																		vector.X = (float)Main.mouseX + Main.screenPosition.X;
																																		vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
																																		Vector2 spinningpoint = new Vector2(num70, num71);
																																		spinningpoint = spinningpoint.Rotate(1.5707963705062866, default(Vector2));
																																		Projectile.NewProjectile(vector.X + spinningpoint.X, vector.Y + spinningpoint.Y, spinningpoint.X, spinningpoint.Y, num62, num64, num65, i, 0f, 0f);
																																		spinningpoint = spinningpoint.Rotate(-3.1415927410125732, default(Vector2));
																																		Projectile.NewProjectile(vector.X + spinningpoint.X, vector.Y + spinningpoint.Y, spinningpoint.X, spinningpoint.Y, num62 + 1, num64, num65, i, 0f, 0f);
																																	}
																																	else
																																	{
																																		if (this.inventory[this.selectedItem].type == 2551)
																																		{
																																			num70 = 0f;
																																			num71 = 0f;
																																			vector.X = (float)Main.mouseX + Main.screenPosition.X;
																																			vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
																																			Projectile.NewProjectile(vector.X, vector.Y, num70, num71, num62 + Main.rand.Next(3), num64, num65, i, 0f, 0f);
																																		}
																																		else
																																		{
																																			if (this.inventory[this.selectedItem].type == 2584)
																																			{
																																				num70 = 0f;
																																				num71 = 0f;
																																				vector.X = (float)Main.mouseX + Main.screenPosition.X;
																																				vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
																																				Projectile.NewProjectile(vector.X, vector.Y, num70, num71, num62 + Main.rand.Next(3), num64, num65, i, 0f, 0f);
																																			}
																																			else
																																			{
																																				if (this.inventory[this.selectedItem].type == 2621)
																																				{
																																					num70 = 0f;
																																					num71 = 0f;
																																					vector.X = (float)Main.mouseX + Main.screenPosition.X;
																																					vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
																																					Projectile.NewProjectile(vector.X, vector.Y, num70, num71, num62, num64, num65, i, 0f, 0f);
																																				}
																																				else
																																				{
																																					if (this.inventory[this.selectedItem].type == 1309)
																																					{
																																						num70 = 0f;
																																						num71 = 0f;
																																						vector.X = (float)Main.mouseX + Main.screenPosition.X;
																																						vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
																																						Projectile.NewProjectile(vector.X, vector.Y, num70, num71, num62, num64, num65, i, 0f, 0f);
																																					}
																																					else
																																					{
																																						if (this.inventory[this.selectedItem].shoot > 0 && (Main.projPet[this.inventory[this.selectedItem].shoot] || this.inventory[this.selectedItem].shoot == 72 || this.inventory[this.selectedItem].shoot == 18) && !this.inventory[this.selectedItem].summon)
																																						{
																																							for (int num146 = 0; num146 < 1000; num146++)
																																							{
																																								if (Main.projectile[num146].active && Main.projectile[num146].owner == this.whoAmi)
																																								{
																																									if (this.inventory[this.selectedItem].shoot == 72)
																																									{
																																										if (Main.projectile[num146].type == 72 || Main.projectile[num146].type == 86 || Main.projectile[num146].type == 87)
																																										{
																																											Main.projectile[num146].Kill();
																																										}
																																									}
																																									else
																																									{
																																										if (this.inventory[this.selectedItem].shoot == Main.projectile[num146].type)
																																										{
																																											Main.projectile[num146].Kill();
																																										}
																																									}
																																								}
																																							}
																																							Projectile.NewProjectile(vector.X, vector.Y, num70, num71, num62, num64, num65, i, 0f, 0f);
																																						}
																																						else
																																						{
																																							int num147 = Projectile.NewProjectile(vector.X, vector.Y, num70, num71, num62, num64, num65, i, 0f, 0f);
																																							if (this.inventory[this.selectedItem].type == 726)
																																							{
																																								Main.projectile[num147].magic = true;
																																							}
																																							if (this.inventory[this.selectedItem].type == 724 || this.inventory[this.selectedItem].type == 676)
																																							{
																																								Main.projectile[num147].melee = true;
																																							}
																																							if (num62 == 80)
																																							{
																																								Main.projectile[num147].ai[0] = (float)Player.tileTargetX;
																																								Main.projectile[num147].ai[1] = (float)Player.tileTargetY;
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					else
					{
						if (this.inventory[this.selectedItem].useStyle == 5)
						{
							this.itemRotation = 0f;
							NetMessage.SendData(41, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
						}
					}
				}
				if (this.whoAmi == Main.myPlayer && (this.inventory[this.selectedItem].type == 509 || this.inventory[this.selectedItem].type == 510 || this.inventory[this.selectedItem].type == 849 || this.inventory[this.selectedItem].type == 850 || this.inventory[this.selectedItem].type == 851) && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f + (float)this.blockRange >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost - (float)this.blockRange <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f + (float)this.blockRange >= (float)Player.tileTargetY)
				{
					this.showItemIcon = true;
					if (this.itemAnimation > 0 && this.itemTime == 0 && this.controlUseItem)
					{
						int i2 = Player.tileTargetX;
						int j2 = Player.tileTargetY;
						if (this.inventory[this.selectedItem].type == 509)
						{
							int num148 = -1;
							for (int num149 = 0; num149 < 58; num149++)
							{
								if (this.inventory[num149].stack > 0 && this.inventory[num149].type == 530)
								{
									num148 = num149;
									break;
								}
							}
							if (num148 >= 0 && WorldGen.PlaceWire(i2, j2))
							{
								this.inventory[num148].stack--;
								if (this.inventory[num148].stack <= 0)
								{
									this.inventory[num148].SetDefaults(0, false);
								}
								this.itemTime = this.inventory[this.selectedItem].useTime;
								NetMessage.SendData(17, -1, -1, "", 5, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
							}
						}
						else
						{
							if (this.inventory[this.selectedItem].type == 850)
							{
								int num150 = -1;
								for (int num151 = 0; num151 < 58; num151++)
								{
									if (this.inventory[num151].stack > 0 && this.inventory[num151].type == 530)
									{
										num150 = num151;
										break;
									}
								}
								if (num150 >= 0 && WorldGen.PlaceWire2(i2, j2))
								{
									this.inventory[num150].stack--;
									if (this.inventory[num150].stack <= 0)
									{
										this.inventory[num150].SetDefaults(0, false);
									}
									this.itemTime = this.inventory[this.selectedItem].useTime;
									NetMessage.SendData(17, -1, -1, "", 10, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
								}
							}
						}
						if (this.inventory[this.selectedItem].type == 851)
						{
							int num152 = -1;
							for (int num153 = 0; num153 < 58; num153++)
							{
								if (this.inventory[num153].stack > 0 && this.inventory[num153].type == 530)
								{
									num152 = num153;
									break;
								}
							}
							if (num152 >= 0 && WorldGen.PlaceWire3(i2, j2))
							{
								this.inventory[num152].stack--;
								if (this.inventory[num152].stack <= 0)
								{
									this.inventory[num152].SetDefaults(0, false);
								}
								this.itemTime = this.inventory[this.selectedItem].useTime;
								NetMessage.SendData(17, -1, -1, "", 12, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
							}
						}
						else
						{
							if (this.inventory[this.selectedItem].type == 510)
							{
								if (WorldGen.KillActuator(i2, j2))
								{
									this.itemTime = this.inventory[this.selectedItem].useTime;
									NetMessage.SendData(17, -1, -1, "", 9, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
								}
								else
								{
									if (WorldGen.KillWire3(i2, j2))
									{
										this.itemTime = this.inventory[this.selectedItem].useTime;
										NetMessage.SendData(17, -1, -1, "", 13, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
									}
									else
									{
										if (WorldGen.KillWire2(i2, j2))
										{
											this.itemTime = this.inventory[this.selectedItem].useTime;
											NetMessage.SendData(17, -1, -1, "", 11, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
										}
										else
										{
											if (WorldGen.KillWire(i2, j2))
											{
												this.itemTime = this.inventory[this.selectedItem].useTime;
												NetMessage.SendData(17, -1, -1, "", 6, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
											}
										}
									}
								}
							}
							else
							{
								if (this.inventory[this.selectedItem].type == 849 && this.inventory[this.selectedItem].stack > 0 && WorldGen.PlaceActuator(i2, j2))
								{
									this.itemTime = this.inventory[this.selectedItem].useTime;
									NetMessage.SendData(17, -1, -1, "", 8, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
									this.inventory[this.selectedItem].stack--;
									if (this.inventory[this.selectedItem].stack <= 0)
									{
										this.inventory[this.selectedItem].SetDefaults(0, false);
									}
								}
							}
						}
					}
				}
				if (this.itemAnimation > 0 && this.itemTime == 0 && (this.inventory[this.selectedItem].type == 507 || this.inventory[this.selectedItem].type == 508))
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					Vector2 vector4 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
					float num154 = (float)Main.mouseX + Main.screenPosition.X - vector4.X;
					float num155 = (float)Main.mouseY + Main.screenPosition.Y - vector4.Y;
					float num156 = (float)Math.Sqrt((double)(num154 * num154 + num155 * num155));
					num156 /= (float)(Main.screenHeight / 2);
					if (num156 > 1f)
					{
						num156 = 1f;
					}
					num156 = num156 * 2f - 1f;
					if (num156 < -1f)
					{
						num156 = -1f;
					}
					if (num156 > 1f)
					{
						num156 = 1f;
					}
					Main.harpNote = num156;
					int style = 26;
					if (this.inventory[this.selectedItem].type == 507)
					{
						style = 35;
					}
					Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, style);
					NetMessage.SendData(58, -1, -1, "", this.whoAmi, num156, 0f, 0f, 0);
				}
				if (((this.inventory[this.selectedItem].type >= 205 && this.inventory[this.selectedItem].type <= 207) || this.inventory[this.selectedItem].type == 1128) && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
				{
					this.showItemIcon = true;
					if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
					{
						if (this.inventory[this.selectedItem].type == 205)
						{
							int num157 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType();
							int num158 = 0;
							for (int num159 = Player.tileTargetX - 1; num159 <= Player.tileTargetX + 1; num159++)
							{
								for (int num160 = Player.tileTargetY - 1; num160 <= Player.tileTargetY + 1; num160++)
								{
									if ((int)Main.tile[num159, num160].liquidType() == num157)
									{
										num158 += (int)Main.tile[num159, num160].liquid;
									}
								}
							}
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid > 0 && num158 > 100)
							{
								int liquidType = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType();
								if (!Main.tile[Player.tileTargetX, Player.tileTargetY].lava())
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].honey())
									{
										this.inventory[this.selectedItem].stack--;
										this.PutItemInInventory(1128, this.selectedItem);
									}
									else
									{
										this.inventory[this.selectedItem].stack--;
										this.PutItemInInventory(206, this.selectedItem);
									}
								}
								else
								{
									this.inventory[this.selectedItem].stack--;
									this.PutItemInInventory(207, this.selectedItem);
								}
								Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
								this.itemTime = this.inventory[this.selectedItem].useTime;
								int num161 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].liquid;
								Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 0;
								Main.tile[Player.tileTargetX, Player.tileTargetY].lava(false);
								Main.tile[Player.tileTargetX, Player.tileTargetY].honey(false);
								WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, false);
								if (Main.netMode == 1)
								{
									NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
								}
								else
								{
									Liquid.AddWater(Player.tileTargetX, Player.tileTargetY);
								}
								for (int num162 = Player.tileTargetX - 1; num162 <= Player.tileTargetX + 1; num162++)
								{
									for (int num163 = Player.tileTargetY - 1; num163 <= Player.tileTargetY + 1; num163++)
									{
										if (num161 < 256 && (int)Main.tile[num162, num163].liquidType() == num157)
										{
											int num164 = (int)Main.tile[num162, num163].liquid;
											if (num164 + num161 > 255)
											{
												num164 = 255 - num161;
											}
											num161 += num164;
											Tile expr_859A = Main.tile[num162, num163];
											expr_859A.liquid -= (byte)num164;
											Main.tile[num162, num163].liquidType(liquidType);
											if (Main.tile[num162, num163].liquid == 0)
											{
												Main.tile[num162, num163].lava(false);
												Main.tile[num162, num163].honey(false);
											}
											WorldGen.SquareTileFrame(num162, num163, false);
											if (Main.netMode == 1)
											{
												NetMessage.sendWater(num162, num163);
											}
											else
											{
												Liquid.AddWater(num162, num163);
											}
										}
									}
								}
							}
						}
						else
						{
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid < 200 && (!Main.tile[Player.tileTargetX, Player.tileTargetY].nactive() || !Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] || Main.tileSolidTop[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]))
							{
								if (this.inventory[this.selectedItem].type == 207)
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType() == 1)
									{
										Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
										Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType(1);
										Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 255;
										WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
										this.inventory[this.selectedItem].stack--;
										this.PutItemInInventory(205, this.selectedItem);
										this.itemTime = this.inventory[this.selectedItem].useTime;
										if (Main.netMode == 1)
										{
											NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
										}
									}
								}
								else
								{
									if (this.inventory[this.selectedItem].type == 206)
									{
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType() == 0)
										{
											Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
											Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType(0);
											Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 255;
											WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
											this.inventory[this.selectedItem].stack--;
											this.PutItemInInventory(205, this.selectedItem);
											this.itemTime = this.inventory[this.selectedItem].useTime;
											if (Main.netMode == 1)
											{
												NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
											}
										}
									}
									else
									{
										if (this.inventory[this.selectedItem].type == 1128 && (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType() == 2))
										{
											Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
											Main.tile[Player.tileTargetX, Player.tileTargetY].liquidType(2);
											Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 255;
											WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
											this.inventory[this.selectedItem].stack--;
											this.PutItemInInventory(205, this.selectedItem);
											this.itemTime = this.inventory[this.selectedItem].useTime;
											if (Main.netMode == 1)
											{
												NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
											}
										}
									}
								}
							}
						}
					}
				}
				if (!this.channel)
				{
					this.toolTime = this.itemTime;
				}
				else
				{
					this.toolTime--;
					if (this.toolTime < 0)
					{
						if (this.inventory[this.selectedItem].pick > 0)
						{
							this.toolTime = this.inventory[this.selectedItem].useTime;
						}
						else
						{
							this.toolTime = (int)((float)this.inventory[this.selectedItem].useTime * this.pickSpeed);
						}
					}
				}
				if ((this.inventory[this.selectedItem].pick > 0 || this.inventory[this.selectedItem].axe > 0 || this.inventory[this.selectedItem].hammer > 0) && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
				{
					int num165 = 0;
					bool flag10 = true;
					this.showItemIcon = true;
					if (this.toolTime == 0 && this.itemAnimation > 0 && this.controlUseItem && (!Main.tile[Player.tileTargetX, Player.tileTargetY].active() || (!Main.tileHammer[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] && !Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] && Main.tile[Player.tileTargetX, Player.tileTargetY].type != 314)))
					{
						this.poundRelease = false;
					}
					if (Main.tile[Player.tileTargetX, Player.tileTargetY].active())
					{
						if ((this.inventory[this.selectedItem].pick > 0 && !Main.tileAxe[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] && !Main.tileHammer[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]) || (this.inventory[this.selectedItem].axe > 0 && Main.tileAxe[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]) || (this.inventory[this.selectedItem].hammer > 0 && Main.tileHammer[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]))
						{
							flag10 = false;
						}
						if (this.toolTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
						{
							int tileId = this.hitTile.HitObject(Player.tileTargetX, Player.tileTargetY, 1);
							if (Main.tileNoFail[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
							{
								num165 = 100;
							}
							if (Main.tileHammer[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
							{
								flag10 = false;
								if (this.inventory[this.selectedItem].hammer > 0)
								{
									num165 += this.inventory[this.selectedItem].hammer;
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 26 && (this.inventory[this.selectedItem].hammer < 80 || !Main.hardMode))
									{
										num165 = 0;
										this.Hurt(this.statLife / 2, -this.direction, false, false, Lang.deathMsg(-1, -1, -1, 4), false);
									}
									if (this.hitTile.AddDamage(tileId, num165, true) >= 100)
									{
										this.hitTile.Clear(tileId);
										WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
										}
									}
									else
									{
										WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
										}
									}
									if (num165 != 0)
									{
										this.hitTile.Prune();
									}
									this.itemTime = this.inventory[this.selectedItem].useTime;
								}
							}
							else
							{
								if (Main.tileAxe[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 80)
									{
										num165 += this.inventory[this.selectedItem].axe * 3;
									}
									else
									{
										num165 += this.inventory[this.selectedItem].axe;
									}
									if (this.inventory[this.selectedItem].axe > 0)
									{
										if (this.hitTile.AddDamage(tileId, num165, true) >= 100)
										{
											this.hitTile.Clear(tileId);
											WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
											}
										}
										else
										{
											WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
											}
										}
										if (num165 != 0)
										{
											this.hitTile.Prune();
										}
										this.itemTime = this.inventory[this.selectedItem].useTime;
									}
								}
								else
								{
									if (this.inventory[this.selectedItem].pick > 0)
									{
										if (Main.tileDungeon[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 25 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 58 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 117 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 203)
										{
											num165 += this.inventory[this.selectedItem].pick / 2;
										}
										else
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 48 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 232)
											{
												num165 += this.inventory[this.selectedItem].pick / 4;
											}
											else
											{
												if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 226)
												{
													num165 += this.inventory[this.selectedItem].pick / 4;
												}
												else
												{
													if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 107 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 221)
													{
														num165 += this.inventory[this.selectedItem].pick / 2;
													}
													else
													{
														if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 108 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 222)
														{
															num165 += this.inventory[this.selectedItem].pick / 3;
														}
														else
														{
															if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 111 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 223)
															{
																num165 += this.inventory[this.selectedItem].pick / 4;
															}
															else
															{
																if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 211)
																{
																	num165 += this.inventory[this.selectedItem].pick / 5;
																}
																else
																{
																	num165 += this.inventory[this.selectedItem].pick;
																}
															}
														}
													}
												}
											}
										}
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 211 && this.inventory[this.selectedItem].pick < 200)
										{
											num165 = 0;
										}
										if ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 25 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 203) && this.inventory[this.selectedItem].pick < 65)
										{
											num165 = 0;
										}
										else
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 117 && this.inventory[this.selectedItem].pick < 65)
											{
												num165 = 0;
											}
											else
											{
												if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 37 && this.inventory[this.selectedItem].pick < 50)
												{
													num165 = 0;
												}
												else
												{
													if ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 22 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 204) && (double)Player.tileTargetY > Main.worldSurface && this.inventory[this.selectedItem].pick < 55)
													{
														num165 = 0;
													}
													else
													{
														if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 56 && this.inventory[this.selectedItem].pick < 65)
														{
															num165 = 0;
														}
														else
														{
															if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 58 && this.inventory[this.selectedItem].pick < 65)
															{
																num165 = 0;
															}
															else
															{
																if ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 226 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 237) && this.inventory[this.selectedItem].pick < 210)
																{
																	num165 = 0;
																}
																else
																{
																	if (Main.tileDungeon[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] && this.inventory[this.selectedItem].pick < 65)
																	{
																		if ((double)Player.tileTargetX < (double)Main.maxTilesX * 0.35 || (double)Player.tileTargetX > (double)Main.maxTilesX * 0.65)
																		{
																			num165 = 0;
																		}
																	}
																	else
																	{
																		if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 107 && this.inventory[this.selectedItem].pick < 100)
																		{
																			num165 = 0;
																		}
																		else
																		{
																			if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 108 && this.inventory[this.selectedItem].pick < 110)
																			{
																				num165 = 0;
																			}
																			else
																			{
																				if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 111 && this.inventory[this.selectedItem].pick < 150)
																				{
																					num165 = 0;
																				}
																				else
																				{
																					if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 221 && this.inventory[this.selectedItem].pick < 100)
																					{
																						num165 = 0;
																					}
																					else
																					{
																						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 222 && this.inventory[this.selectedItem].pick < 110)
																						{
																							num165 = 0;
																						}
																						else
																						{
																							if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 223 && this.inventory[this.selectedItem].pick < 150)
																							{
																								num165 = 0;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 147 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 40 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 53 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 57 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 59 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 123 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 224)
										{
											num165 += this.inventory[this.selectedItem].pick;
										}
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 165 || Main.tileRope[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 199 || Main.tileMoss[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
										{
											num165 = 100;
										}
										if (this.hitTile.AddDamage(tileId, num165, false) >= 100 && (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 2 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 23 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 60 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 70 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 109 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 71 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 199 || Main.tileMoss[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]))
										{
											num165 = 0;
										}
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 128 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 269)
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 18 || Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 54)
											{
												Player.tileTargetX--;
												this.hitTile.UpdatePosition(tileId, Player.tileTargetX, Player.tileTargetY);
											}
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 100)
											{
												num165 = 0;
												Main.blockMouse = true;
											}
										}
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 334)
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY == 0)
											{
												Player.tileTargetY++;
												this.hitTile.UpdatePosition(tileId, Player.tileTargetX, Player.tileTargetY);
											}
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY == 36)
											{
												Player.tileTargetY--;
												this.hitTile.UpdatePosition(tileId, Player.tileTargetX, Player.tileTargetY);
											}
											int num166 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameX;
											bool flag11 = num166 >= 5000;
											bool flag12 = false;
											if (!flag11)
											{
												int num167 = num166 / 18;
												num167 %= 3;
												Player.tileTargetX -= num167;
												if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 5000)
												{
													flag11 = true;
												}
											}
											if (flag11)
											{
												num166 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].frameX;
												int num168 = 0;
												while (num166 >= 5000)
												{
													num166 -= 5000;
													num168++;
												}
												if (num168 != 0)
												{
													flag12 = true;
												}
											}
											if (flag12)
											{
												num165 = 0;
												Main.blockMouse = true;
											}
										}
										if (this.hitTile.AddDamage(tileId, num165, true) >= 100)
										{
											this.hitTile.Clear(tileId);
											if (Main.netMode == 1 && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21)
											{
												WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
												NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
												NetMessage.SendData(34, -1, -1, "", 1, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
											}
											else
											{
												int num169 = Player.tileTargetY;
												WorldGen.KillTile(Player.tileTargetX, num169, false, false, false);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)num169, 0f, 0);
												}
											}
										}
										else
										{
											WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
											}
										}
										if (num165 != 0)
										{
											this.hitTile.Prune();
										}
										this.itemTime = (int)((float)this.inventory[this.selectedItem].useTime * this.pickSpeed);
									}
								}
							}
							if (this.inventory[this.selectedItem].hammer > 0 && Main.tile[Player.tileTargetX, Player.tileTargetY].active() && ((Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] && Main.tile[Player.tileTargetX, Player.tileTargetY].type != 10) || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 314) && this.poundRelease)
							{
								flag10 = false;
								this.itemTime = this.inventory[this.selectedItem].useTime;
								num165 += (int)((double)this.inventory[this.selectedItem].hammer * 1.25);
								num165 = 100;
								if (Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active() && Main.tile[Player.tileTargetX, Player.tileTargetY - 1].type == 10)
								{
									num165 = 0;
								}
								if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active() && Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type == 10)
								{
									num165 = 0;
								}
								if (this.hitTile.AddDamage(tileId, num165, true) >= 100)
								{
									this.hitTile.Clear(tileId);
									if (this.poundRelease)
									{
										int num170 = Player.tileTargetX;
										int num171 = Player.tileTargetY;
										if (Main.tile[num170, num171].type == 19)
										{
											if (Main.tile[num170, num171].halfBrick())
											{
												WorldGen.PoundTile(num170, num171);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(17, -1, -1, "", 7, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
												}
											}
											else
											{
												int num172 = 1;
												int slope = 2;
												if (Main.tile[num170 + 1, num171 - 1].type == 19 || Main.tile[num170 - 1, num171 + 1].type == 19 || (WorldGen.SolidTile(num170 + 1, num171) && !WorldGen.SolidTile(num170 - 1, num171)))
												{
													num172 = 2;
													slope = 1;
												}
												if (Main.tile[num170, num171].slope() == 0)
												{
													WorldGen.SlopeTile(num170, num171, num172);
													int num173 = (int)Main.tile[num170, num171].slope();
													if (Main.netMode == 1)
													{
														NetMessage.SendData(17, -1, -1, "", 14, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)num173, 0);
													}
												}
												else
												{
													if ((int)Main.tile[num170, num171].slope() == num172)
													{
														WorldGen.SlopeTile(num170, num171, slope);
														int num174 = (int)Main.tile[num170, num171].slope();
														if (Main.netMode == 1)
														{
															NetMessage.SendData(17, -1, -1, "", 14, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)num174, 0);
														}
													}
													else
													{
														WorldGen.SlopeTile(num170, num171, 0);
														int num175 = (int)Main.tile[num170, num171].slope();
														if (Main.netMode == 1)
														{
															NetMessage.SendData(17, -1, -1, "", 14, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)num175, 0);
														}
														WorldGen.PoundTile(num170, num171);
														if (Main.netMode == 1)
														{
															NetMessage.SendData(17, -1, -1, "", 7, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
														}
													}
												}
											}
										}
										else
										{
											if (Main.tile[num170, num171].type == 314)
											{
												if (Minecart.FrameTrack(num170, num171, true, false) && Main.netMode == 1)
												{
													NetMessage.SendData(17, -1, -1, "", 15, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
												}
											}
											else
											{
												if ((Main.tile[num170, num171].halfBrick() || Main.tile[num170, num171].slope() != 0) && !Main.tileSolidTop[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
												{
													int num176 = 1;
													int num177 = 1;
													int num178 = 2;
													if ((WorldGen.SolidTile(num170 + 1, num171) || Main.tile[num170 + 1, num171].slope() == 1 || Main.tile[num170 + 1, num171].slope() == 3) && !WorldGen.SolidTile(num170 - 1, num171))
													{
														num177 = 2;
														num178 = 1;
													}
													if (WorldGen.SolidTile(num170, num171 - 1) && !WorldGen.SolidTile(num170, num171 + 1))
													{
														num176 = -1;
													}
													if (num176 == 1)
													{
														if (Main.tile[num170, num171].slope() == 0)
														{
															WorldGen.SlopeTile(num170, num171, num177);
														}
														else
														{
															if ((int)Main.tile[num170, num171].slope() == num177)
															{
																WorldGen.SlopeTile(num170, num171, num178);
															}
															else
															{
																if ((int)Main.tile[num170, num171].slope() == num178)
																{
																	WorldGen.SlopeTile(num170, num171, num177 + 2);
																}
																else
																{
																	if ((int)Main.tile[num170, num171].slope() == num177 + 2)
																	{
																		WorldGen.SlopeTile(num170, num171, num178 + 2);
																	}
																	else
																	{
																		WorldGen.SlopeTile(num170, num171, 0);
																	}
																}
															}
														}
													}
													else
													{
														if (Main.tile[num170, num171].slope() == 0)
														{
															WorldGen.SlopeTile(num170, num171, num177 + 2);
														}
														else
														{
															if ((int)Main.tile[num170, num171].slope() == num177 + 2)
															{
																WorldGen.SlopeTile(num170, num171, num178 + 2);
															}
															else
															{
																if ((int)Main.tile[num170, num171].slope() == num178 + 2)
																{
																	WorldGen.SlopeTile(num170, num171, num177);
																}
																else
																{
																	if ((int)Main.tile[num170, num171].slope() == num177)
																	{
																		WorldGen.SlopeTile(num170, num171, num178);
																	}
																	else
																	{
																		WorldGen.SlopeTile(num170, num171, 0);
																	}
																}
															}
														}
													}
													int num179 = (int)Main.tile[num170, num171].slope();
													if (Main.netMode == 1)
													{
														NetMessage.SendData(17, -1, -1, "", 14, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)num179, 0);
													}
												}
												else
												{
													WorldGen.PoundTile(num170, num171);
													if (Main.netMode == 1)
													{
														NetMessage.SendData(17, -1, -1, "", 7, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
													}
												}
											}
										}
										this.poundRelease = false;
									}
								}
								else
								{
									WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, true, false);
									Main.PlaySound(0, Player.tileTargetX * 16, Player.tileTargetY * 16, 1);
								}
							}
							else
							{
								this.poundRelease = false;
							}
						}
					}
					if (this.releaseUseItem)
					{
						this.poundRelease = true;
					}
					int num180 = Player.tileTargetX;
					int num181 = Player.tileTargetY;
					bool flag13 = true;
					if (Main.tile[num180, num181].wall > 0)
					{
						if (!Main.wallHouse[(int)Main.tile[num180, num181].wall])
						{
							for (int num182 = num180 - 1; num182 < num180 + 2; num182++)
							{
								for (int num183 = num181 - 1; num183 < num181 + 2; num183++)
								{
									if (Main.tile[num182, num183].wall != Main.tile[num180, num181].wall)
									{
										flag13 = false;
										break;
									}
								}
							}
						}
						else
						{
							flag13 = false;
						}
					}
					if (flag13 && !Main.tile[num180, num181].active())
					{
						int num184 = -1;
						if ((double)(((float)Main.mouseX + Main.screenPosition.X) / 16f) < Math.Round((double)(((float)Main.mouseX + Main.screenPosition.X) / 16f)))
						{
							num184 = 0;
						}
						int num185 = -1;
						if ((double)(((float)Main.mouseY + Main.screenPosition.Y) / 16f) < Math.Round((double)(((float)Main.mouseY + Main.screenPosition.Y) / 16f)))
						{
							num185 = 0;
						}
						for (int num186 = Player.tileTargetX + num184; num186 <= Player.tileTargetX + num184 + 1; num186++)
						{
							for (int num187 = Player.tileTargetY + num185; num187 <= Player.tileTargetY + num185 + 1; num187++)
							{
								if (flag13)
								{
									num180 = num186;
									num181 = num187;
									if (Main.tile[num180, num181].wall > 0)
									{
										if (!Main.wallHouse[(int)Main.tile[num180, num181].wall])
										{
											for (int num188 = num180 - 1; num188 < num180 + 2; num188++)
											{
												for (int num189 = num181 - 1; num189 < num181 + 2; num189++)
												{
													if (Main.tile[num188, num189].wall != Main.tile[num180, num181].wall)
													{
														flag13 = false;
														break;
													}
												}
											}
										}
										else
										{
											flag13 = false;
										}
									}
								}
							}
						}
					}
					if (flag10 && Main.tile[num180, num181].wall > 0 && (!Main.tile[num180, num181].active() || num180 != Player.tileTargetX || num181 != Player.tileTargetY || (!Main.tileHammer[(int)Main.tile[num180, num181].type] && !this.poundRelease)) && this.toolTime == 0 && this.itemAnimation > 0 && this.controlUseItem && this.inventory[this.selectedItem].hammer > 0)
					{
						bool flag14 = true;
						if (!Main.wallHouse[(int)Main.tile[num180, num181].wall])
						{
							flag14 = false;
							for (int num190 = num180 - 1; num190 < num180 + 2; num190++)
							{
								for (int num191 = num181 - 1; num191 < num181 + 2; num191++)
								{
									if (Main.tile[num190, num191].wall == 0 || Main.wallHouse[(int)Main.tile[num190, num191].wall])
									{
										flag14 = true;
										break;
									}
								}
							}
						}
						if (flag14)
						{
							int tileId = this.hitTile.HitObject(num180, num181, 2);
							num165 += (int)((float)this.inventory[this.selectedItem].hammer * 1.5f);
							if (this.hitTile.AddDamage(tileId, num165, true) >= 100)
							{
								this.hitTile.Clear(tileId);
								WorldGen.KillWall(num180, num181, false);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(17, -1, -1, "", 2, (float)num180, (float)num181, 0f, 0);
								}
							}
							else
							{
								WorldGen.KillWall(num180, num181, true);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(17, -1, -1, "", 2, (float)num180, (float)num181, 1f, 0);
								}
							}
							if (num165 != 0)
							{
								this.hitTile.Prune();
							}
							this.itemTime = this.inventory[this.selectedItem].useTime / 2;
						}
					}
				}
				if (Main.myPlayer == this.whoAmi && this.inventory[this.selectedItem].type == 1326 && this.itemAnimation > 0 && this.itemTime == 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					Vector2 newPos;
					newPos.X = (float)Main.mouseX + Main.screenPosition.X;
					if (this.gravDir == 1f)
					{
						newPos.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)this.height;
					}
					else
					{
						newPos.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
					}
					newPos.X -= (float)(this.width / 2);
					if (newPos.X > 50f && newPos.X < (float)(Main.maxTilesX * 16 - 50) && newPos.Y > 50f && newPos.Y < (float)(Main.maxTilesY * 16 - 50))
					{
						int num192 = (int)(newPos.X / 16f);
						int num193 = (int)(newPos.Y / 16f);
						if ((Main.tile[num192, num193].wall != 87 || (double)num193 <= Main.worldSurface || NPC.downedPlantBoss) && !Collision.SolidCollision(newPos, this.width, this.height))
						{
							this.Teleport(newPos, 1);
							NetMessage.SendData(65, -1, -1, "", 0, (float)this.whoAmi, newPos.X, newPos.Y, 1);
							if (this.chaosState)
							{
								this.statLife -= this.statLifeMax2 / 6;
								if (Lang.lang <= 1)
								{
									string deathText = " didn't materialize";
									if (Main.rand.Next(2) == 0)
									{
										if (this.male)
										{
											deathText = "'s legs appeared where his head should be";
										}
										else
										{
											deathText = "'s legs appeared where her head should be";
										}
									}
									if (this.statLife <= 0)
									{
										this.KillMe(1.0, 0, false, deathText);
									}
								}
								else
								{
									if (this.statLife <= 0)
									{
										this.KillMe(1.0, 0, false, "");
									}
								}
								this.lifeRegenCount = 0;
								this.lifeRegenTime = 0;
							}
							this.AddBuff(88, 480, true);
						}
					}
				}
				if (this.inventory[this.selectedItem].type == 29 && this.itemAnimation > 0 && this.statLifeMax < 400 && this.itemTime == 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					this.statLifeMax += 20;
					this.statLifeMax2 += 20;
					this.statLife += 20;
					if (Main.myPlayer == this.whoAmi)
					{
						this.HealEffect(20, true);
					}
				}
				if (this.inventory[this.selectedItem].type == 1291 && this.itemAnimation > 0 && this.statLifeMax >= 400 && this.statLifeMax < 500 && this.itemTime == 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					this.statLifeMax += 5;
					this.statLifeMax2 += 5;
					this.statLife += 5;
					if (Main.myPlayer == this.whoAmi)
					{
						this.HealEffect(5, true);
					}
				}
				if (this.inventory[this.selectedItem].type == 109 && this.itemAnimation > 0 && this.statManaMax < 200 && this.itemTime == 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					this.statManaMax += 20;
					this.statManaMax2 += 20;
					this.statMana += 20;
					if (Main.myPlayer == this.whoAmi)
					{
						this.ManaEffect(20);
					}
				}
				this.PlaceThing();
			}
			if (((this.inventory[this.selectedItem].damage >= 0 && this.inventory[this.selectedItem].type > 0 && !this.inventory[this.selectedItem].noMelee) || this.inventory[this.selectedItem].type == 1450 || this.inventory[this.selectedItem].type == 1991) && this.itemAnimation > 0)
			{
				bool flag15 = false;
				Rectangle rectangle = new Rectangle((int)this.itemLocation.X, (int)this.itemLocation.Y, 32, 32);
				if (!Main.dedServ)
				{
					rectangle = new Rectangle((int)this.itemLocation.X, (int)this.itemLocation.Y, Main.itemTexture[this.inventory[this.selectedItem].type].Width, Main.itemTexture[this.inventory[this.selectedItem].type].Height);
				}
				rectangle.Width = (int)((float)rectangle.Width * this.inventory[this.selectedItem].scale);
				rectangle.Height = (int)((float)rectangle.Height * this.inventory[this.selectedItem].scale);
				if (this.direction == -1)
				{
					rectangle.X -= rectangle.Width;
				}
				if (this.gravDir == 1f)
				{
					rectangle.Y -= rectangle.Height;
				}
				if (this.inventory[this.selectedItem].useStyle == 1)
				{
					if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.333)
					{
						if (this.direction == -1)
						{
							rectangle.X -= (int)((double)rectangle.Width * 1.4 - (double)rectangle.Width);
						}
						rectangle.Width = (int)((double)rectangle.Width * 1.4);
						rectangle.Y += (int)((double)rectangle.Height * 0.5 * (double)this.gravDir);
						rectangle.Height = (int)((double)rectangle.Height * 1.1);
					}
					else
					{
						if ((double)this.itemAnimation >= (double)this.itemAnimationMax * 0.666)
						{
							if (this.direction == 1)
							{
								rectangle.X -= (int)((double)rectangle.Width * 1.2);
							}
							rectangle.Width *= 2;
							rectangle.Y -= (int)(((double)rectangle.Height * 1.4 - (double)rectangle.Height) * (double)this.gravDir);
							rectangle.Height = (int)((double)rectangle.Height * 1.4);
						}
					}
				}
				else
				{
					if (this.inventory[this.selectedItem].useStyle == 3)
					{
						if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.666)
						{
							flag15 = true;
						}
						else
						{
							if (this.direction == -1)
							{
								rectangle.X -= (int)((double)rectangle.Width * 1.4 - (double)rectangle.Width);
							}
							rectangle.Width = (int)((double)rectangle.Width * 1.4);
							rectangle.Y += (int)((double)rectangle.Height * 0.6);
							rectangle.Height = (int)((double)rectangle.Height * 0.6);
						}
					}
				}
				float arg_B0B8_0 = this.gravDir;
				if (this.inventory[this.selectedItem].type == 1450 && Main.rand.Next(3) == 0)
				{
					int num194 = -1;
					float x5 = (float)(rectangle.X + Main.rand.Next(rectangle.Width));
					float y5 = (float)(rectangle.Y + Main.rand.Next(rectangle.Height));
					if (Main.rand.Next(500) == 0)
					{
						num194 = Gore.NewGore(new Vector2(x5, y5), default(Vector2), 415, (float)Main.rand.Next(51, 101) * 0.01f);
					}
					else
					{
						if (Main.rand.Next(250) == 0)
						{
							num194 = Gore.NewGore(new Vector2(x5, y5), default(Vector2), 414, (float)Main.rand.Next(51, 101) * 0.01f);
						}
						else
						{
							if (Main.rand.Next(80) == 0)
							{
								num194 = Gore.NewGore(new Vector2(x5, y5), default(Vector2), 413, (float)Main.rand.Next(51, 101) * 0.01f);
							}
							else
							{
								if (Main.rand.Next(10) == 0)
								{
									num194 = Gore.NewGore(new Vector2(x5, y5), default(Vector2), 412, (float)Main.rand.Next(51, 101) * 0.01f);
								}
								else
								{
									if (Main.rand.Next(3) == 0)
									{
										num194 = Gore.NewGore(new Vector2(x5, y5), default(Vector2), 411, (float)Main.rand.Next(51, 101) * 0.01f);
									}
								}
							}
						}
					}
					if (num194 >= 0)
					{
						Gore expr_B2B2_cp_0 = Main.gore[num194];
						expr_B2B2_cp_0.velocity.X = expr_B2B2_cp_0.velocity.X + (float)(this.direction * 2);
						Gore expr_B2D4_cp_0 = Main.gore[num194];
						expr_B2D4_cp_0.velocity.Y = expr_B2D4_cp_0.velocity.Y * 0.3f;
					}
				}
				if (!flag15)
				{
					if (this.inventory[this.selectedItem].type == 989 && Main.rand.Next(5) == 0)
					{
						int num195 = Main.rand.Next(3);
						if (num195 == 0)
						{
							num195 = 15;
						}
						else
						{
							if (num195 == 1)
							{
								num195 = 57;
							}
							else
							{
								num195 = 58;
							}
						}
						int num196 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, num195, (float)(this.direction * 2), 0f, 150, default(Color), 1.3f);
						Main.dust[num196].velocity *= 0.2f;
					}
					if ((this.inventory[this.selectedItem].type == 44 || this.inventory[this.selectedItem].type == 45 || this.inventory[this.selectedItem].type == 46 || this.inventory[this.selectedItem].type == 103 || this.inventory[this.selectedItem].type == 104) && Main.rand.Next(15) == 0)
					{
						Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 14, (float)(this.direction * 2), 0f, 150, default(Color), 1.3f);
					}
					if (this.inventory[this.selectedItem].type == 273 || this.inventory[this.selectedItem].type == 675)
					{
						if (Main.rand.Next(5) == 0)
						{
							Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 14, (float)(this.direction * 2), 0f, 150, default(Color), 1.4f);
						}
						int num197 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 27, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default(Color), 1.2f);
						Main.dust[num197].noGravity = true;
						Dust expr_B5AD_cp_0 = Main.dust[num197];
						expr_B5AD_cp_0.velocity.X = expr_B5AD_cp_0.velocity.X / 2f;
						Dust expr_B5CD_cp_0 = Main.dust[num197];
						expr_B5CD_cp_0.velocity.Y = expr_B5CD_cp_0.velocity.Y / 2f;
					}
					if (this.inventory[this.selectedItem].type == 723 && Main.rand.Next(2) == 0)
					{
						int num198 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 64, 0f, 0f, 150, default(Color), 1.2f);
						Main.dust[num198].noGravity = true;
					}
					if (this.inventory[this.selectedItem].type == 65)
					{
						if (Main.rand.Next(5) == 0)
						{
							Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 58, 0f, 0f, 150, default(Color), 1.2f);
						}
						if (Main.rand.Next(10) == 0)
						{
							Gore.NewGore(new Vector2((float)rectangle.X, (float)rectangle.Y), default(Vector2), Main.rand.Next(16, 18), 1f);
						}
					}
					if (this.inventory[this.selectedItem].type == 190 || this.inventory[this.selectedItem].type == 213)
					{
						int num199 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 40, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 0, default(Color), 1.2f);
						Main.dust[num199].noGravity = true;
					}
					if (this.inventory[this.selectedItem].type == 121)
					{
						for (int num200 = 0; num200 < 2; num200++)
						{
							int num201 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 6, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default(Color), 2.5f);
							Main.dust[num201].noGravity = true;
							Dust expr_B881_cp_0 = Main.dust[num201];
							expr_B881_cp_0.velocity.X = expr_B881_cp_0.velocity.X * 2f;
							Dust expr_B8A1_cp_0 = Main.dust[num201];
							expr_B8A1_cp_0.velocity.Y = expr_B8A1_cp_0.velocity.Y * 2f;
						}
					}
					if (this.inventory[this.selectedItem].type == 122 || this.inventory[this.selectedItem].type == 217)
					{
						int num202 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 6, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default(Color), 1.9f);
						Main.dust[num202].noGravity = true;
					}
					if (this.inventory[this.selectedItem].type == 155)
					{
						int num203 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 172, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default(Color), 0.9f);
						Main.dust[num203].noGravity = true;
						Main.dust[num203].velocity *= 0.1f;
					}
					if (this.inventory[this.selectedItem].type == 676 && Main.rand.Next(3) == 0)
					{
						int num204 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 67, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 90, default(Color), 1.5f);
						Main.dust[num204].noGravity = true;
						Main.dust[num204].velocity *= 0.2f;
					}
					if (this.inventory[this.selectedItem].type == 724 && Main.rand.Next(5) == 0)
					{
						int num205 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 67, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 90, default(Color), 1.5f);
						Main.dust[num205].noGravity = true;
						Main.dust[num205].velocity *= 0.2f;
					}
					if (this.inventory[this.selectedItem].type >= 795 && this.inventory[this.selectedItem].type <= 802 && Main.rand.Next(3) == 0)
					{
						int num206 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 115, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 140, default(Color), 1.5f);
						Main.dust[num206].noGravity = true;
						Main.dust[num206].velocity *= 0.25f;
					}
					if (this.inventory[this.selectedItem].type == 367 || this.inventory[this.selectedItem].type == 368 || this.inventory[this.selectedItem].type == 674)
					{
						if (Main.rand.Next(3) == 0)
						{
							int num207 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 57, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default(Color), 1.1f);
							Main.dust[num207].noGravity = true;
							Dust expr_BD9E_cp_0 = Main.dust[num207];
							expr_BD9E_cp_0.velocity.X = expr_BD9E_cp_0.velocity.X / 2f;
							Dust expr_BDBE_cp_0 = Main.dust[num207];
							expr_BDBE_cp_0.velocity.Y = expr_BDBE_cp_0.velocity.Y / 2f;
							Dust expr_BDDE_cp_0 = Main.dust[num207];
							expr_BDDE_cp_0.velocity.X = expr_BDDE_cp_0.velocity.X + (float)(this.direction * 2);
						}
						if (Main.rand.Next(4) == 0)
						{
							int num207 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 43, 0f, 0f, 254, default(Color), 0.3f);
							Main.dust[num207].velocity *= 0f;
						}
					}
					if (this.inventory[this.selectedItem].type >= 198 && this.inventory[this.selectedItem].type <= 203)
					{
						float num208 = 0.5f;
						float num209 = 0.5f;
						float num210 = 0.5f;
						if (this.inventory[this.selectedItem].type == 198)
						{
							num208 *= 0.1f;
							num209 *= 0.5f;
							num210 *= 1.2f;
						}
						else
						{
							if (this.inventory[this.selectedItem].type == 199)
							{
								num208 *= 1f;
								num209 *= 0.2f;
								num210 *= 0.1f;
							}
							else
							{
								if (this.inventory[this.selectedItem].type == 200)
								{
									num208 *= 0.1f;
									num209 *= 1f;
									num210 *= 0.2f;
								}
								else
								{
									if (this.inventory[this.selectedItem].type == 201)
									{
										num208 *= 0.8f;
										num209 *= 0.1f;
										num210 *= 1f;
									}
									else
									{
										if (this.inventory[this.selectedItem].type == 202)
										{
											num208 *= 0.8f;
											num209 *= 0.9f;
											num210 *= 1f;
										}
										else
										{
											if (this.inventory[this.selectedItem].type == 203)
											{
												num208 *= 0.9f;
												num209 *= 0.9f;
												num210 *= 0.1f;
											}
										}
									}
								}
							}
						}
						Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), num208, num209, num210);
					}
					if (this.frostBurn && this.inventory[this.selectedItem].melee && !this.inventory[this.selectedItem].noMelee && !this.inventory[this.selectedItem].noUseGraphic && Main.rand.Next(2) == 0)
					{
						int num211 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 135, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default(Color), 2.5f);
						Main.dust[num211].noGravity = true;
						Main.dust[num211].velocity *= 0.7f;
						Dust expr_C1C7_cp_0 = Main.dust[num211];
						expr_C1C7_cp_0.velocity.Y = expr_C1C7_cp_0.velocity.Y - 0.5f;
					}
					if (this.inventory[this.selectedItem].melee && !this.inventory[this.selectedItem].noMelee && !this.inventory[this.selectedItem].noUseGraphic && this.meleeEnchant > 0)
					{
						if (this.meleeEnchant == 1)
						{
							if (Main.rand.Next(3) == 0)
							{
								int num212 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 171, 0f, 0f, 100, default(Color), 1f);
								Main.dust[num212].noGravity = true;
								Main.dust[num212].fadeIn = 1.5f;
								Main.dust[num212].velocity *= 0.25f;
							}
						}
						else
						{
							if (this.meleeEnchant == 2)
							{
								if (Main.rand.Next(2) == 0)
								{
									int num213 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 75, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default(Color), 2.5f);
									Main.dust[num213].noGravity = true;
									Main.dust[num213].velocity *= 0.7f;
									Dust expr_C3A6_cp_0 = Main.dust[num213];
									expr_C3A6_cp_0.velocity.Y = expr_C3A6_cp_0.velocity.Y - 0.5f;
								}
							}
							else
							{
								if (this.meleeEnchant == 3)
								{
									if (Main.rand.Next(2) == 0)
									{
										int num214 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 6, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default(Color), 2.5f);
										Main.dust[num214].noGravity = true;
										Main.dust[num214].velocity *= 0.7f;
										Dust expr_C484_cp_0 = Main.dust[num214];
										expr_C484_cp_0.velocity.Y = expr_C484_cp_0.velocity.Y - 0.5f;
									}
								}
								else
								{
									if (this.meleeEnchant == 4)
									{
										if (Main.rand.Next(2) == 0)
										{
											int num215 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 57, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default(Color), 1.1f);
											Main.dust[num215].noGravity = true;
											Dust expr_C549_cp_0 = Main.dust[num215];
											expr_C549_cp_0.velocity.X = expr_C549_cp_0.velocity.X / 2f;
											Dust expr_C569_cp_0 = Main.dust[num215];
											expr_C569_cp_0.velocity.Y = expr_C569_cp_0.velocity.Y / 2f;
										}
									}
									else
									{
										if (this.meleeEnchant == 5)
										{
											if (Main.rand.Next(2) == 0)
											{
												int num216 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 169, 0f, 0f, 100, default(Color), 1f);
												Dust expr_C5FA_cp_0 = Main.dust[num216];
												expr_C5FA_cp_0.velocity.X = expr_C5FA_cp_0.velocity.X + (float)this.direction;
												Dust expr_C61C_cp_0 = Main.dust[num216];
												expr_C61C_cp_0.velocity.Y = expr_C61C_cp_0.velocity.Y + 0.2f;
												Main.dust[num216].noGravity = true;
											}
										}
										else
										{
											if (this.meleeEnchant == 6)
											{
												if (Main.rand.Next(2) == 0)
												{
													int num217 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 135, 0f, 0f, 100, default(Color), 1f);
													Dust expr_C6BD_cp_0 = Main.dust[num217];
													expr_C6BD_cp_0.velocity.X = expr_C6BD_cp_0.velocity.X + (float)this.direction;
													Dust expr_C6DF_cp_0 = Main.dust[num217];
													expr_C6DF_cp_0.velocity.Y = expr_C6DF_cp_0.velocity.Y + 0.2f;
													Main.dust[num217].noGravity = true;
												}
											}
											else
											{
												if (this.meleeEnchant == 7)
												{
													if (Main.rand.Next(20) == 0)
													{
														int type4 = Main.rand.Next(139, 143);
														int num218 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, type4, this.velocity.X, this.velocity.Y, 0, default(Color), 1.2f);
														Dust expr_C7A3_cp_0 = Main.dust[num218];
														expr_C7A3_cp_0.velocity.X = expr_C7A3_cp_0.velocity.X * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
														Dust expr_C7D9_cp_0 = Main.dust[num218];
														expr_C7D9_cp_0.velocity.Y = expr_C7D9_cp_0.velocity.Y * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
														Dust expr_C80F_cp_0 = Main.dust[num218];
														expr_C80F_cp_0.velocity.X = expr_C80F_cp_0.velocity.X + (float)Main.rand.Next(-50, 51) * 0.05f;
														Dust expr_C83F_cp_0 = Main.dust[num218];
														expr_C83F_cp_0.velocity.Y = expr_C83F_cp_0.velocity.Y + (float)Main.rand.Next(-50, 51) * 0.05f;
														Main.dust[num218].scale *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
													}
													if (Main.rand.Next(40) == 0)
													{
														int type5 = Main.rand.Next(276, 283);
														int num219 = Gore.NewGore(new Vector2((float)rectangle.X, (float)rectangle.Y), this.velocity, type5, 1f);
														Gore expr_C8F6_cp_0 = Main.gore[num219];
														expr_C8F6_cp_0.velocity.X = expr_C8F6_cp_0.velocity.X * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
														Gore expr_C92C_cp_0 = Main.gore[num219];
														expr_C92C_cp_0.velocity.Y = expr_C92C_cp_0.velocity.Y * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
														Main.gore[num219].scale *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
														Gore expr_C993_cp_0 = Main.gore[num219];
														expr_C993_cp_0.velocity.X = expr_C993_cp_0.velocity.X + (float)Main.rand.Next(-50, 51) * 0.05f;
														Gore expr_C9C3_cp_0 = Main.gore[num219];
														expr_C9C3_cp_0.velocity.Y = expr_C9C3_cp_0.velocity.Y + (float)Main.rand.Next(-50, 51) * 0.05f;
													}
												}
												else
												{
													if (this.meleeEnchant == 8 && Main.rand.Next(4) == 0)
													{
														int num220 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 46, 0f, 0f, 100, default(Color), 1f);
														Main.dust[num220].noGravity = true;
														Main.dust[num220].fadeIn = 1.5f;
														Main.dust[num220].velocity *= 0.25f;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					if (this.magmaStone && this.inventory[this.selectedItem].melee && !this.inventory[this.selectedItem].noMelee && !this.inventory[this.selectedItem].noUseGraphic && Main.rand.Next(3) != 0)
					{
						int num221 = Dust.NewDust(new Vector2((float)rectangle.X, (float)rectangle.Y), rectangle.Width, rectangle.Height, 6, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default(Color), 2.5f);
						Main.dust[num221].noGravity = true;
						Dust expr_CB82_cp_0 = Main.dust[num221];
						expr_CB82_cp_0.velocity.X = expr_CB82_cp_0.velocity.X * 2f;
						Dust expr_CBA2_cp_0 = Main.dust[num221];
						expr_CBA2_cp_0.velocity.Y = expr_CBA2_cp_0.velocity.Y * 2f;
					}
					if (Main.myPlayer == i && this.inventory[this.selectedItem].type == 1991)
					{
						for (int num222 = 0; num222 < 200; num222++)
						{
							if (Main.npc[num222].active && Main.npc[num222].catchItem > 0)
							{
								Rectangle value = new Rectangle((int)Main.npc[num222].position.X, (int)Main.npc[num222].position.Y, Main.npc[num222].width, Main.npc[num222].height);
								if (rectangle.Intersects(value) && (Main.npc[num222].noTileCollide || Collision.CanHit(this.position, this.width, this.height, Main.npc[num222].position, Main.npc[num222].width, Main.npc[num222].height)))
								{
									NPC.CatchNPC(num222, i);
								}
							}
						}
					}
					else
					{
						if (Main.myPlayer == i && this.inventory[this.selectedItem].damage > 0)
						{
							int num223 = (int)((float)this.inventory[this.selectedItem].damage * this.meleeDamage);
							float num224 = this.inventory[this.selectedItem].knockBack;
							float num225 = 1f;
							if (this.kbGlove)
							{
								num225 += 1f;
							}
							if (this.kbBuff)
							{
								num225 += 0.5f;
							}
							num224 *= num225;
							int num226 = rectangle.X / 16;
							int num227 = (rectangle.X + rectangle.Width) / 16 + 1;
							int num228 = rectangle.Y / 16;
							int num229 = (rectangle.Y + rectangle.Height) / 16 + 1;
							for (int num230 = num226; num230 < num227; num230++)
							{
								for (int num231 = num228; num231 < num229; num231++)
								{
									if (Main.tile[num230, num231] != null && Main.tileCut[(int)Main.tile[num230, num231].type] && Main.tile[num230, num231 + 1] != null && Main.tile[num230, num231 + 1].type != 78)
									{
										if (this.inventory[this.selectedItem].type == 1786)
										{
											int type6 = (int)Main.tile[num230, num231].type;
											WorldGen.KillTile(num230, num231, false, false, false);
											if (!Main.tile[num230, num231].active())
											{
												int num232 = 0;
												if (type6 == 3 || type6 == 24 || type6 == 61 || type6 == 110 || type6 == 201)
												{
													num232 = Main.rand.Next(1, 3);
												}
												if (type6 == 73 || type6 == 74 || type6 == 113)
												{
													num232 = Main.rand.Next(2, 5);
												}
												if (num232 > 0)
												{
													int number = Item.NewItem(num230 * 16, num231 * 16, 16, 16, 1727, num232, false, 0, false);
													if (Main.netMode == 1)
													{
														NetMessage.SendData(21, -1, -1, "", number, 1f, 0f, 0f, 0);
													}
												}
											}
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)num230, (float)num231, 0f, 0);
											}
										}
										else
										{
											WorldGen.KillTile(num230, num231, false, false, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)num230, (float)num231, 0f, 0);
											}
										}
									}
								}
							}
							for (int num233 = 0; num233 < 200; num233++)
							{
								if (Main.npc[num233].active && Main.npc[num233].immune[i] == 0 && this.attackCD == 0 && !Main.npc[num233].dontTakeDamage && (!Main.npc[num233].friendly || (Main.npc[num233].type == 22 && this.killGuide) || (Main.npc[num233].type == 54 && this.killClothier)))
								{
									Rectangle value2 = new Rectangle((int)Main.npc[num233].position.X, (int)Main.npc[num233].position.Y, Main.npc[num233].width, Main.npc[num233].height);
									if (rectangle.Intersects(value2) && (Main.npc[num233].noTileCollide || Collision.CanHit(this.position, this.width, this.height, Main.npc[num233].position, Main.npc[num233].width, Main.npc[num233].height)))
									{
										bool flag16 = false;
										if (Main.rand.Next(1, 101) <= this.meleeCrit)
										{
											flag16 = true;
										}
										int num234 = Main.DamageVar((float)num223);
										this.StatusNPC(this.inventory[this.selectedItem].type, num233);
										this.onHit(Main.npc[num233].center().X, Main.npc[num233].center().Y);
										int num235 = (int)Main.npc[num233].StrikeNPC(num234, num224, this.direction, flag16, false);
										if (this.beetleOffense)
										{
											this.beetleCounter += (float)num235;
											this.beetleCountdown = 0;
										}
										if (this.inventory[this.selectedItem].type == 1826)
										{
											this.pumpkinSword(num233, (int)((double)num223 * 1.5), num224);
										}
										if (this.meleeEnchant == 7)
										{
											Projectile.NewProjectile(Main.npc[num233].center().X, Main.npc[num233].center().Y, Main.npc[num233].velocity.X, Main.npc[num233].velocity.Y, 289, 0, 0f, this.whoAmi, 0f, 0f);
										}
										if (this.inventory[this.selectedItem].type == 1123)
										{
											int num236 = Main.rand.Next(1, 4);
											for (int num237 = 0; num237 < num236; num237++)
											{
												float num238 = (float)(this.direction * 2) + (float)Main.rand.Next(-35, 36) * 0.02f;
												float num239 = (float)Main.rand.Next(-35, 36) * 0.02f;
												num238 *= 0.2f;
												num239 *= 0.2f;
												Projectile.NewProjectile((float)(rectangle.X + rectangle.Width / 2), (float)(rectangle.Y + rectangle.Height / 2), num238, num239, 181, num234 / 3, 0f, i, 0f, 0f);
											}
										}
										if (Main.npc[num233].value > 0f && this.coins && Main.rand.Next(5) == 0)
										{
											int type7 = 71;
											if (Main.rand.Next(10) == 0)
											{
												type7 = 72;
											}
											if (Main.rand.Next(100) == 0)
											{
												type7 = 73;
											}
											int num240 = Item.NewItem((int)Main.npc[num233].position.X, (int)Main.npc[num233].position.Y, Main.npc[num233].width, Main.npc[num233].height, type7, 1, false, 0, false);
											Main.item[num240].stack = Main.rand.Next(1, 11);
											Main.item[num240].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
											Main.item[num240].velocity.X = (float)Main.rand.Next(10, 31) * 0.2f * (float)this.direction;
											if (Main.netMode == 1)
											{
												NetMessage.SendData(21, -1, -1, "", num240, 0f, 0f, 0f, 0);
											}
										}
										if (Main.netMode != 0)
										{
											if (flag16)
											{
												NetMessage.SendData(28, -1, -1, "", num233, (float)num234, num224, (float)this.direction, 1);
											}
											else
											{
												NetMessage.SendData(28, -1, -1, "", num233, (float)num234, num224, (float)this.direction, 0);
											}
										}
										Main.npc[num233].immune[i] = this.itemAnimation;
										this.attackCD = (int)((double)this.itemAnimationMax * 0.33);
									}
								}
							}
							if (this.hostile)
							{
								for (int num241 = 0; num241 < 255; num241++)
								{
									if (num241 != i && Main.player[num241].active && Main.player[num241].hostile && !Main.player[num241].immune && !Main.player[num241].dead && (Main.player[i].team == 0 || Main.player[i].team != Main.player[num241].team))
									{
										Rectangle value3 = new Rectangle((int)Main.player[num241].position.X, (int)Main.player[num241].position.Y, Main.player[num241].width, Main.player[num241].height);
										if (rectangle.Intersects(value3) && Collision.CanHit(this.position, this.width, this.height, Main.player[num241].position, Main.player[num241].width, Main.player[num241].height))
										{
											bool flag17 = false;
											if (Main.rand.Next(1, 101) <= 10)
											{
												flag17 = true;
											}
											int num242 = Main.DamageVar((float)num223);
											this.StatusPvP(this.inventory[this.selectedItem].type, num241);
											this.onHit(Main.player[num241].center().X, Main.player[num241].center().Y);
											Main.player[num241].Hurt(num242, this.direction, true, false, "", flag17);
											if (this.meleeEnchant == 7)
											{
												Projectile.NewProjectile(Main.player[num241].center().X, Main.player[num241].center().Y, Main.player[num241].velocity.X, Main.player[num241].velocity.Y, 289, 0, 0f, this.whoAmi, 0f, 0f);
											}
											if (this.inventory[this.selectedItem].type == 1123)
											{
												int num243 = Main.rand.Next(1, 4);
												for (int num244 = 0; num244 < num243; num244++)
												{
													float num245 = (float)(this.direction * 2) + (float)Main.rand.Next(-35, 36) * 0.02f;
													float num246 = (float)Main.rand.Next(-35, 36) * 0.02f;
													num245 *= 0.2f;
													num246 *= 0.2f;
													Projectile.NewProjectile((float)(rectangle.X + rectangle.Width / 2), (float)(rectangle.Y + rectangle.Height / 2), num245, num246, 181, num242 / 3, 0f, i, 0f, 0f);
												}
											}
											if (Main.netMode != 0)
											{
												if (flag17)
												{
													NetMessage.SendData(26, -1, -1, Lang.deathMsg(this.whoAmi, -1, -1, -1), num241, (float)this.direction, (float)num242, 1f, 1);
												}
												else
												{
													NetMessage.SendData(26, -1, -1, Lang.deathMsg(this.whoAmi, -1, -1, -1), num241, (float)this.direction, (float)num242, 1f, 0);
												}
											}
											this.attackCD = (int)((double)this.itemAnimationMax * 0.33);
										}
									}
								}
							}
							if (this.inventory[this.selectedItem].type == 787 && (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.1) || this.itemAnimation == (int)((double)this.itemAnimationMax * 0.3) || this.itemAnimation == (int)((double)this.itemAnimationMax * 0.5) || this.itemAnimation == (int)((double)this.itemAnimationMax * 0.7) || this.itemAnimation == (int)((double)this.itemAnimationMax * 0.9)))
							{
								float num247 = 0f;
								float num248 = 0f;
								float num249 = 0f;
								float num250 = 0f;
								if (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.9))
								{
									num247 = -7f;
								}
								if (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.7))
								{
									num247 = -6f;
									num248 = 2f;
								}
								if (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.5))
								{
									num247 = -4f;
									num248 = 4f;
								}
								if (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.3))
								{
									num247 = -2f;
									num248 = 6f;
								}
								if (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.1))
								{
									num248 = 7f;
								}
								if (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.7))
								{
									num250 = 26f;
								}
								if (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.3))
								{
									num250 -= 4f;
									num249 -= 20f;
								}
								if (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.1))
								{
									num249 += 6f;
								}
								if (this.direction == -1)
								{
									if (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.9))
									{
										num250 -= 8f;
									}
									if (this.itemAnimation == (int)((double)this.itemAnimationMax * 0.7))
									{
										num250 -= 6f;
									}
								}
								num247 *= 1.5f;
								num248 *= 1.5f;
								num250 *= (float)this.direction;
								num249 *= this.gravDir;
								Projectile.NewProjectile((float)(rectangle.X + rectangle.Width / 2) + num250, (float)(rectangle.Y + rectangle.Height / 2) + num249, (float)this.direction * num248, num247 * this.gravDir, 131, num223 / 2, 0f, i, 0f, 0f);
							}
						}
					}
				}
			}
			if (this.itemTime == 0 && this.itemAnimation > 0)
			{
				if (this.inventory[this.selectedItem].hairDye >= 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					if (this.whoAmi == Main.myPlayer)
					{
						this.hairDye = (byte)this.inventory[this.selectedItem].hairDye;
						NetMessage.SendData(4, -1, -1, Main.player[this.whoAmi].name, this.whoAmi, 0f, 0f, 0f, 0);
					}
				}
				if (this.inventory[this.selectedItem].healLife > 0)
				{
					this.statLife += this.inventory[this.selectedItem].healLife;
					this.itemTime = this.inventory[this.selectedItem].useTime;
					if (Main.myPlayer == this.whoAmi)
					{
						this.HealEffect(this.inventory[this.selectedItem].healLife, true);
					}
				}
				if (this.inventory[this.selectedItem].healMana > 0)
				{
					this.statMana += this.inventory[this.selectedItem].healMana;
					this.itemTime = this.inventory[this.selectedItem].useTime;
					if (Main.myPlayer == this.whoAmi)
					{
						this.AddBuff(94, Player.manaSickTime, true);
						this.ManaEffect(this.inventory[this.selectedItem].healMana);
					}
				}
				if (this.inventory[this.selectedItem].buffType > 0)
				{
					if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].buffType != 90)
					{
						this.AddBuff(this.inventory[this.selectedItem].buffType, this.inventory[this.selectedItem].buffTime, true);
					}
					this.itemTime = this.inventory[this.selectedItem].useTime;
				}
				if (this.inventory[this.selectedItem].type == 678)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					if (this.whoAmi == Main.myPlayer)
					{
						this.AddBuff(20, 216000, true);
						this.AddBuff(22, 216000, true);
						this.AddBuff(23, 216000, true);
						this.AddBuff(24, 216000, true);
						this.AddBuff(30, 216000, true);
						this.AddBuff(31, 216000, true);
						this.AddBuff(32, 216000, true);
						this.AddBuff(33, 216000, true);
						this.AddBuff(35, 216000, true);
						this.AddBuff(36, 216000, true);
						this.AddBuff(68, 216000, true);
					}
				}
			}
			if (this.whoAmi == Main.myPlayer)
			{
				if (this.itemTime == 0 && this.itemAnimation > 0 && this.inventory[this.selectedItem].type == 361 && Main.CanStartInvasion(1, true))
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
					if (Main.netMode != 1)
					{
						if (Main.invasionType == 0)
						{
							Main.invasionDelay = 0;
							Main.StartInvasion(1);
						}
					}
					else
					{
						NetMessage.SendData(61, -1, -1, "", this.whoAmi, -1f, 0f, 0f, 0);
					}
				}
				if (this.itemTime == 0 && this.itemAnimation > 0 && this.inventory[this.selectedItem].type == 602 && Main.CanStartInvasion(2, true))
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
					if (Main.netMode != 1)
					{
						if (Main.invasionType == 0)
						{
							Main.invasionDelay = 0;
							Main.StartInvasion(2);
						}
					}
					else
					{
						NetMessage.SendData(61, -1, -1, "", this.whoAmi, -2f, 0f, 0f, 0);
					}
				}
				if (this.itemTime == 0 && this.itemAnimation > 0 && this.inventory[this.selectedItem].type == 1315 && Main.CanStartInvasion(3, true))
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
					if (Main.netMode != 1)
					{
						if (Main.invasionType == 0)
						{
							Main.invasionDelay = 0;
							Main.StartInvasion(3);
						}
					}
					else
					{
						NetMessage.SendData(61, -1, -1, "", this.whoAmi, -3f, 0f, 0f, 0);
					}
				}
				if (this.itemTime == 0 && this.itemAnimation > 0 && this.inventory[this.selectedItem].type == 1844 && !Main.dayTime && !Main.pumpkinMoon && !Main.snowMoon)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
					if (Main.netMode != 1)
					{
						Main.NewText(Lang.misc[31], 50, 255, 130, false);
						Main.startPumpkinMoon();
					}
					else
					{
						NetMessage.SendData(61, -1, -1, "", this.whoAmi, -4f, 0f, 0f, 0);
					}
				}
				if (this.itemTime == 0 && this.itemAnimation > 0 && this.inventory[this.selectedItem].type == 1958 && !Main.dayTime && !Main.pumpkinMoon && !Main.snowMoon)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
					if (Main.netMode != 1)
					{
						Main.NewText(Lang.misc[34], 50, 255, 130, false);
						Main.startSnowMoon();
					}
					else
					{
						NetMessage.SendData(61, -1, -1, "", this.whoAmi, -5f, 0f, 0f, 0);
					}
				}
				if (this.itemTime == 0 && this.itemAnimation > 0 && this.inventory[this.selectedItem].makeNPC > 0 && this.controlUseItem && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					int x6 = Main.mouseX + (int)Main.screenPosition.X;
					int y6 = Main.mouseY + (int)Main.screenPosition.Y;
					NPC.ReleaseNPC(x6, y6, (int)this.inventory[this.selectedItem].makeNPC, this.inventory[this.selectedItem].placeStyle, this.whoAmi);
				}
				if (this.itemTime == 0 && this.itemAnimation > 0 && (this.inventory[this.selectedItem].type == 43 || this.inventory[this.selectedItem].type == 70 || this.inventory[this.selectedItem].type == 544 || this.inventory[this.selectedItem].type == 556 || this.inventory[this.selectedItem].type == 557 || this.inventory[this.selectedItem].type == 560 || this.inventory[this.selectedItem].type == 1133 || this.inventory[this.selectedItem].type == 1331) && this.SummonItemCheck())
				{
					if (this.inventory[this.selectedItem].type == 560)
					{
						this.itemTime = this.inventory[this.selectedItem].useTime;
						Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
						if (Main.netMode != 1)
						{
							NPC.SpawnOnPlayer(i, 50);
						}
						else
						{
							NetMessage.SendData(61, -1, -1, "", this.whoAmi, 50f, 0f, 0f, 0);
						}
					}
					else
					{
						if (this.inventory[this.selectedItem].type == 43)
						{
							if (!Main.dayTime)
							{
								this.itemTime = this.inventory[this.selectedItem].useTime;
								Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
								if (Main.netMode != 1)
								{
									NPC.SpawnOnPlayer(i, 4);
								}
								else
								{
									NetMessage.SendData(61, -1, -1, "", this.whoAmi, 4f, 0f, 0f, 0);
								}
							}
						}
						else
						{
							if (this.inventory[this.selectedItem].type == 70)
							{
								if (this.zoneEvil)
								{
									this.itemTime = this.inventory[this.selectedItem].useTime;
									Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
									if (Main.netMode != 1)
									{
										NPC.SpawnOnPlayer(i, 13);
									}
									else
									{
										NetMessage.SendData(61, -1, -1, "", this.whoAmi, 13f, 0f, 0f, 0);
									}
								}
							}
							else
							{
								if (this.inventory[this.selectedItem].type == 544)
								{
									if (!Main.dayTime)
									{
										this.itemTime = this.inventory[this.selectedItem].useTime;
										Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
										if (Main.netMode != 1)
										{
											NPC.SpawnOnPlayer(i, 125);
											NPC.SpawnOnPlayer(i, 126);
										}
										else
										{
											NetMessage.SendData(61, -1, -1, "", this.whoAmi, 125f, 0f, 0f, 0);
											NetMessage.SendData(61, -1, -1, "", this.whoAmi, 126f, 0f, 0f, 0);
										}
									}
								}
								else
								{
									if (this.inventory[this.selectedItem].type == 556)
									{
										if (!Main.dayTime)
										{
											this.itemTime = this.inventory[this.selectedItem].useTime;
											Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
											if (Main.netMode != 1)
											{
												NPC.SpawnOnPlayer(i, 134);
											}
											else
											{
												NetMessage.SendData(61, -1, -1, "", this.whoAmi, 134f, 0f, 0f, 0);
											}
										}
									}
									else
									{
										if (this.inventory[this.selectedItem].type == 557)
										{
											if (!Main.dayTime)
											{
												this.itemTime = this.inventory[this.selectedItem].useTime;
												Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
												if (Main.netMode != 1)
												{
													NPC.SpawnOnPlayer(i, 127);
												}
												else
												{
													NetMessage.SendData(61, -1, -1, "", this.whoAmi, 127f, 0f, 0f, 0);
												}
											}
										}
										else
										{
											if (this.inventory[this.selectedItem].type == 1133)
											{
												this.itemTime = this.inventory[this.selectedItem].useTime;
												Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
												if (Main.netMode != 1)
												{
													NPC.SpawnOnPlayer(i, 222);
												}
												else
												{
													NetMessage.SendData(61, -1, -1, "", this.whoAmi, 222f, 0f, 0f, 0);
												}
											}
											else
											{
												if (this.inventory[this.selectedItem].type == 1331 && this.zoneBlood)
												{
													this.itemTime = this.inventory[this.selectedItem].useTime;
													Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
													if (Main.netMode != 1)
													{
														NPC.SpawnOnPlayer(i, 266);
													}
													else
													{
														NetMessage.SendData(61, -1, -1, "", this.whoAmi, 266f, 0f, 0f, 0);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].type == 50 && this.itemAnimation > 0)
			{
				if (Main.rand.Next(2) == 0)
				{
					Dust.NewDust(this.position, this.width, this.height, 15, 0f, 0f, 150, default(Color), 1.1f);
				}
				if (this.itemTime == 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
				}
				else
				{
					if (this.itemTime == this.inventory[this.selectedItem].useTime / 2)
					{
						for (int num251 = 0; num251 < 70; num251++)
						{
							Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 150, default(Color), 1.5f);
						}
						this.grappling[0] = -1;
						this.grapCount = 0;
						for (int num252 = 0; num252 < 1000; num252++)
						{
							if (Main.projectile[num252].active && Main.projectile[num252].owner == i && Main.projectile[num252].aiStyle == 7)
							{
								Main.projectile[num252].Kill();
							}
						}
						this.Spawn();
						for (int num253 = 0; num253 < 70; num253++)
						{
							Dust.NewDust(this.position, this.width, this.height, 15, 0f, 0f, 150, default(Color), 1.5f);
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].type == 2350 && this.itemAnimation > 0)
			{
				if (this.itemTime == 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
				}
				else
				{
					if (this.itemTime == 2)
					{
						for (int num254 = 0; num254 < 70; num254++)
						{
							Main.dust[Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 150, Color.Cyan, 1.2f)].velocity *= 0.5f;
						}
						this.grappling[0] = -1;
						this.grapCount = 0;
						for (int num255 = 0; num255 < 1000; num255++)
						{
							if (Main.projectile[num255].active && Main.projectile[num255].owner == i && Main.projectile[num255].aiStyle == 7)
							{
								Main.projectile[num255].Kill();
							}
						}
						bool flag18 = this.immune;
						int num256 = this.immuneTime;
						this.Spawn();
						this.immune = flag18;
						this.immuneTime = num256;
						for (int num257 = 0; num257 < 70; num257++)
						{
							Main.dust[Dust.NewDust(this.position, this.width, this.height, 15, 0f, 0f, 150, Color.Cyan, 1.2f)].velocity *= 0.5f;
						}
						if (this.inventory[this.selectedItem].stack > 0)
						{
							this.inventory[this.selectedItem].stack--;
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].type == 2351 && this.itemAnimation > 0)
			{
				if (this.itemTime == 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
				}
				else
				{
					if (this.itemTime == 2)
					{
						if (Main.netMode == 0)
						{
							this.TeleportationPotion();
						}
						else
						{
							if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
							{
								NetMessage.SendData(73, -1, -1, "", 0, 0f, 0f, 0f, 0);
							}
						}
						if (this.inventory[this.selectedItem].stack > 0)
						{
							this.inventory[this.selectedItem].stack--;
						}
					}
				}
			}
			if (i == Main.myPlayer)
			{
				if (this.itemTime == (int)((float)this.inventory[this.selectedItem].useTime * this.tileSpeed) && this.inventory[this.selectedItem].tileWand > 0)
				{
					int tileWand2 = this.inventory[this.selectedItem].tileWand;
					int num258 = 0;
					while (num258 < 58)
					{
						if (tileWand2 == this.inventory[num258].type && this.inventory[num258].stack > 0)
						{
							this.inventory[num258].stack--;
							if (this.inventory[num258].stack <= 0)
							{
								this.inventory[num258] = new Item();
								break;
							}
							break;
						}
						else
						{
							num258++;
						}
					}
				}
				int num259;
				if (this.inventory[this.selectedItem].createTile >= 0)
				{
					num259 = (int)((float)this.inventory[this.selectedItem].useTime * this.tileSpeed);
				}
				else
				{
					if (this.inventory[this.selectedItem].createWall > 0)
					{
						num259 = (int)((float)this.inventory[this.selectedItem].useTime * this.wallSpeed);
					}
					else
					{
						num259 = this.inventory[this.selectedItem].useTime;
					}
				}
				if (this.itemTime == num259 && this.inventory[this.selectedItem].consumable)
				{
					bool flag19 = true;
					if (this.inventory[this.selectedItem].type == 2350 || this.inventory[this.selectedItem].type == 2351)
					{
						flag19 = false;
					}
					if (this.inventory[this.selectedItem].ranged)
					{
						if (this.ammoCost80 && Main.rand.Next(5) == 0)
						{
							flag19 = false;
						}
						if (this.ammoCost75 && Main.rand.Next(4) == 0)
						{
							flag19 = false;
						}
					}
					if (this.inventory[this.selectedItem].type >= 71 && this.inventory[this.selectedItem].type <= 74)
					{
						flag19 = true;
					}
					if (flag19)
					{
						if (this.inventory[this.selectedItem].stack > 0)
						{
							this.inventory[this.selectedItem].stack--;
						}
						if (this.inventory[this.selectedItem].stack <= 0)
						{
							this.itemTime = this.itemAnimation;
						}
					}
				}
				if (this.inventory[this.selectedItem].stack <= 0 && this.itemAnimation == 0)
				{
					this.inventory[this.selectedItem] = new Item();
				}
				if (this.selectedItem == 58)
				{
					if (this.itemAnimation == 0)
					{
						return;
					}
					Main.mouseItem = this.inventory[this.selectedItem].Clone();
				}
			}
		}
		public bool ItemFitsWeaponRack(Item i)
		{
			bool flag = false;
			if (i.fishingPole > 0)
			{
				flag = true;
			}
			int netID = i.netID;
			if (netID == 905 || netID == 1326)
			{
				flag = true;
			}
			return (i.damage > 0 || flag) && i.useStyle > 0 && i.stack > 0;
		}
		public void PlaceWeapon(int x, int y)
		{
			if (Main.tile[x, y].active() && Main.tile[x, y].type == 334)
			{
				int num = (int)Main.tile[x, y].frameY;
				int i = 1;
				num /= 18;
				while (i > num)
				{
					y++;
					num = (int)Main.tile[x, y].frameY;
					num /= 18;
				}
				while (i < num)
				{
					y--;
					num = (int)Main.tile[x, y].frameY;
					num /= 18;
				}
				int j = (int)Main.tile[x, y].frameX;
				int num2 = 0;
				while (j >= 5000)
				{
					j -= 5000;
					num2++;
				}
				if (num2 != 0)
				{
					j = (num2 - 1) * 18;
				}
				bool flag = false;
				if (j >= 54)
				{
					j -= 54;
					flag = true;
				}
				x -= j / 18;
				int k = (int)Main.tile[x, y].frameX;
				WorldGen.KillTile(x, y, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, "", 0, (float)x, (float)y, 1f, 0);
				}
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, "", 0, (float)(x + 1), (float)y, 1f, 0);
				}
				while (k >= 5000)
				{
					k -= 5000;
				}
				Main.blockMouse = true;
				int num3 = 5000;
				int num4 = 10000;
				if (flag)
				{
					num3 = 20000;
					num4 = 25000;
				}
				Main.tile[x, y].frameX = (short)(this.inventory[this.selectedItem].netID + num3 + 100);
				Main.tile[x + 1, y].frameX = (short)((int)this.inventory[this.selectedItem].prefix + num4);
				if (Main.netMode == 1)
				{
					NetMessage.SendTileSquare(-1, x, y, 1);
				}
				if (Main.netMode == 1)
				{
					NetMessage.SendTileSquare(-1, x + 1, y, 1);
				}
				this.inventory[this.selectedItem].stack--;
				if (this.inventory[this.selectedItem].stack <= 0)
				{
					this.inventory[this.selectedItem].SetDefaults(0, false);
					Main.mouseItem.SetDefaults(0, false);
				}
				if (this.selectedItem == 58)
				{
					Main.mouseItem = this.inventory[this.selectedItem].Clone();
				}
				this.releaseUseItem = false;
				this.mouseInterface = true;
			}
		}
		public Color GetImmuneAlpha(Color newColor, float alphaReduction)
		{
			float num = (float)(255 - this.immuneAlpha) / 255f;
			if (alphaReduction > 0f)
			{
				num *= 1f - alphaReduction;
			}
			if (this.immuneAlpha > 125)
			{
				return Color.Transparent;
			}
			return Color.Multiply(newColor, num);
		}
		public Color GetImmuneAlphaPure(Color newColor, float alphaReduction)
		{
			float num = (float)(255 - this.immuneAlpha) / 255f;
			if (alphaReduction > 0f)
			{
				num *= 1f - alphaReduction;
			}
			return Color.Multiply(newColor, num);
		}
		public Color GetDeathAlpha(Color newColor)
		{
			int r = (int)newColor.R + (int)((double)this.immuneAlpha * 0.9);
			int g = (int)newColor.G + (int)((double)this.immuneAlpha * 0.5);
			int b = (int)newColor.B + (int)((double)this.immuneAlpha * 0.5);
			int num = (int)newColor.A + (int)((double)this.immuneAlpha * 0.4);
			if (num < 0)
			{
				num = 0;
			}
			if (num > 255)
			{
				num = 255;
			}
			return new Color(r, g, b, num);
		}
		public void DropCoins()
		{
			for (int i = 0; i < 59; i++)
			{
				if (this.inventory[i].type >= 71 && this.inventory[i].type <= 74)
				{
					int num = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.inventory[i].type, 1, false, 0, false);
					int num2 = this.inventory[i].stack / 2;
					num2 = this.inventory[i].stack - num2;
					this.inventory[i].stack -= num2;
					if (this.inventory[i].stack <= 0)
					{
						this.inventory[i] = new Item();
					}
					Main.item[num].stack = num2;
					Main.item[num].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
					Main.item[num].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
					Main.item[num].noGrabDelay = 100;
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f, 0);
					}
					if (i == 58)
					{
						Main.mouseItem = this.inventory[i].Clone();
					}
				}
			}
		}
		public void DropItems()
		{
			for (int i = 0; i < 59; i++)
			{
				if (this.inventory[i].stack > 0 && this.inventory[i].name != "Copper Pickaxe" && this.inventory[i].name != "Copper Axe" && this.inventory[i].name != "Copper Shortsword")
				{
					int num = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.inventory[i].type, 1, false, 0, false);
					Main.item[num].netDefaults(this.inventory[i].netID);
					Main.item[num].Prefix((int)this.inventory[i].prefix);
					Main.item[num].stack = this.inventory[i].stack;
					Main.item[num].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
					Main.item[num].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
					Main.item[num].noGrabDelay = 100;
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f, 0);
					}
				}
				this.inventory[i] = new Item();
				if (i < 16)
				{
					if (this.armor[i].stack > 0)
					{
						int num2 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.armor[i].type, 1, false, 0, false);
						Main.item[num2].netDefaults(this.armor[i].netID);
						Main.item[num2].Prefix((int)this.armor[i].prefix);
						Main.item[num2].stack = this.armor[i].stack;
						Main.item[num2].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
						Main.item[num2].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
						Main.item[num2].noGrabDelay = 100;
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", num2, 0f, 0f, 0f, 0);
						}
					}
					this.armor[i] = new Item();
				}
				if (i < 8)
				{
					if (this.dye[i].stack > 0)
					{
						int num3 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.dye[i].type, 1, false, 0, false);
						Main.item[num3].netDefaults(this.dye[i].netID);
						Main.item[num3].Prefix((int)this.dye[i].prefix);
						Main.item[num3].stack = this.dye[i].stack;
						Main.item[num3].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
						Main.item[num3].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
						Main.item[num3].noGrabDelay = 100;
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", num3, 0f, 0f, 0f, 0);
						}
					}
					this.dye[i] = new Item();
				}
			}
			this.inventory[0].SetDefaults("Copper Shortsword");
			this.inventory[0].Prefix(-1);
			this.inventory[1].SetDefaults("Copper Pickaxe");
			this.inventory[1].Prefix(-1);
			this.inventory[2].SetDefaults("Copper Axe");
			this.inventory[2].Prefix(-1);
			Main.mouseItem = new Item();
		}
		public object Clone()
		{
			return base.MemberwiseClone();
		}
		public object clientClone()
		{
			Player player = new Player();
			player.zoneEvil = this.zoneEvil;
			player.zoneMeteor = this.zoneMeteor;
			player.zoneDungeon = this.zoneDungeon;
			player.zoneJungle = this.zoneJungle;
			player.zoneHoly = this.zoneHoly;
			player.zoneSnow = this.zoneSnow;
			player.zoneBlood = this.zoneBlood;
			player.zoneCandle = this.zoneCandle;
			player.direction = this.direction;
			player.selectedItem = this.selectedItem;
			player.controlUp = this.controlUp;
			player.controlDown = this.controlDown;
			player.controlLeft = this.controlLeft;
			player.controlRight = this.controlRight;
			player.controlJump = this.controlJump;
			player.controlUseItem = this.controlUseItem;
			player.statLife = this.statLife;
			player.statLifeMax = this.statLifeMax;
			player.statMana = this.statMana;
			player.statManaMax = this.statManaMax;
			player.position.X = this.position.X;
			player.chest = this.chest;
			player.talkNPC = this.talkNPC;
			player.hideVisual = this.hideVisual;
			for (int i = 0; i < 59; i++)
			{
				player.inventory[i] = this.inventory[i].Clone();
				if (i < 16)
				{
					player.armor[i] = this.armor[i].Clone();
				}
				if (i < 8)
				{
					player.dye[i] = this.dye[i].Clone();
				}
			}
			for (int j = 0; j < 22; j++)
			{
				player.buffType[j] = this.buffType[j];
				player.buffTime[j] = this.buffTime[j];
			}
			return player;
		}
		private static void EncryptFile(string inputFile, string outputFile)
		{
			string s = "h3y_gUyZ";
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			byte[] bytes = unicodeEncoding.GetBytes(s);
			FileStream fileStream = new FileStream(outputFile, FileMode.Create);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
			FileStream fileStream2 = new FileStream(inputFile, FileMode.Open);
			int num;
			while ((num = fileStream2.ReadByte()) != -1)
			{
				cryptoStream.WriteByte((byte)num);
			}
			fileStream2.Close();
			cryptoStream.Close();
			fileStream.Close();
		}
		private static bool DecryptFile(string inputFile, string outputFile)
		{
			string s = "h3y_gUyZ";
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			byte[] bytes = unicodeEncoding.GetBytes(s);
			FileStream fileStream = new FileStream(inputFile, FileMode.Open);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
			FileStream fileStream2 = new FileStream(outputFile, FileMode.Create);
			try
			{
				int num;
				while ((num = cryptoStream.ReadByte()) != -1)
				{
					fileStream2.WriteByte((byte)num);
				}
				fileStream2.Close();
				cryptoStream.Close();
				fileStream.Close();
			}
			catch
			{
				fileStream2.Close();
				fileStream.Close();
				File.Delete(outputFile);
				return true;
			}
			return false;
		}
		public static bool CheckSpawn(int x, int y)
		{
			if (x < 10 || x > Main.maxTilesX - 10 || y < 10 || y > Main.maxTilesX - 10)
			{
				return false;
			}
			if (Main.tile[x, y - 1] == null)
			{
				return false;
			}
			if (!Main.tile[x, y - 1].active() || Main.tile[x, y - 1].type != 79)
			{
				return false;
			}
			for (int i = x - 1; i <= x + 1; i++)
			{
				for (int j = y - 3; j < y; j++)
				{
					if (Main.tile[i, j] == null)
					{
						return false;
					}
					if (Main.tile[i, j].nactive() && Main.tileSolid[(int)Main.tile[i, j].type] && !Main.tileSolidTop[(int)Main.tile[i, j].type])
					{
						Main.NewText("There is not enough space", 255, 240, 20, false);
						return false;
					}
				}
			}
			return WorldGen.StartRoomCheck(x, y - 1);
		}
		public void FindSpawn()
		{
			for (int i = 0; i < 200; i++)
			{
				if (this.spN[i] == null)
				{
					this.SpawnX = -1;
					this.SpawnY = -1;
					return;
				}
				if (this.spN[i] == Main.worldName && this.spI[i] == Main.worldID)
				{
					this.SpawnX = this.spX[i];
					this.SpawnY = this.spY[i];
					return;
				}
			}
		}
		public void ChangeSpawn(int x, int y)
		{
			int num = 0;
			while (num < 200 && this.spN[num] != null)
			{
				if (this.spN[num] == Main.worldName && this.spI[num] == Main.worldID)
				{
					for (int i = num; i > 0; i--)
					{
						this.spN[i] = this.spN[i - 1];
						this.spI[i] = this.spI[i - 1];
						this.spX[i] = this.spX[i - 1];
						this.spY[i] = this.spY[i - 1];
					}
					this.spN[0] = Main.worldName;
					this.spI[0] = Main.worldID;
					this.spX[0] = x;
					this.spY[0] = y;
					return;
				}
				num++;
			}
			for (int j = 199; j > 0; j--)
			{
				if (this.spN[j - 1] != null)
				{
					this.spN[j] = this.spN[j - 1];
					this.spI[j] = this.spI[j - 1];
					this.spX[j] = this.spX[j - 1];
					this.spY[j] = this.spY[j - 1];
				}
			}
			this.spN[0] = Main.worldName;
			this.spI[0] = Main.worldID;
			this.spX[0] = x;
			this.spY[0] = y;
		}
		public static void SavePlayer(Player newPlayer, string playerPath, bool skipMapSave = false, bool fileAlreadyDecrypted = false)
		{
			if (!skipMapSave)
			{
				try
				{
					if (Main.mapEnabled)
					{
						Map.saveMap();
					}
				}
				catch
				{
				}
				try
				{
					Directory.CreateDirectory(Main.PlayerPath);
				}
				catch
				{
				}
			}
			if (Main.ServerSideCharacter)
			{
				return;
			}
			if (playerPath == null || playerPath == "")
			{
				return;
			}
			string destFileName = playerPath + ".bak";
			if (File.Exists(playerPath))
			{
				File.Copy(playerPath, destFileName, true);
			}
			string text = playerPath + ".dat";
			using (FileStream fileStream = new FileStream(text, FileMode.Create))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					binaryWriter.Write(Main.curRelease);
					binaryWriter.Write(newPlayer.name);
					binaryWriter.Write(newPlayer.difficulty);
					binaryWriter.Write(newPlayer.hair);
					binaryWriter.Write(newPlayer.hairDye);
					binaryWriter.Write(newPlayer.hideVisual);
					binaryWriter.Write(newPlayer.male);
					binaryWriter.Write(newPlayer.statLife);
					binaryWriter.Write(newPlayer.statLifeMax);
					binaryWriter.Write(newPlayer.statMana);
					binaryWriter.Write(newPlayer.statManaMax);
					binaryWriter.Write(newPlayer.hairColor.R);
					binaryWriter.Write(newPlayer.hairColor.G);
					binaryWriter.Write(newPlayer.hairColor.B);
					binaryWriter.Write(newPlayer.skinColor.R);
					binaryWriter.Write(newPlayer.skinColor.G);
					binaryWriter.Write(newPlayer.skinColor.B);
					binaryWriter.Write(newPlayer.eyeColor.R);
					binaryWriter.Write(newPlayer.eyeColor.G);
					binaryWriter.Write(newPlayer.eyeColor.B);
					binaryWriter.Write(newPlayer.shirtColor.R);
					binaryWriter.Write(newPlayer.shirtColor.G);
					binaryWriter.Write(newPlayer.shirtColor.B);
					binaryWriter.Write(newPlayer.underShirtColor.R);
					binaryWriter.Write(newPlayer.underShirtColor.G);
					binaryWriter.Write(newPlayer.underShirtColor.B);
					binaryWriter.Write(newPlayer.pantsColor.R);
					binaryWriter.Write(newPlayer.pantsColor.G);
					binaryWriter.Write(newPlayer.pantsColor.B);
					binaryWriter.Write(newPlayer.shoeColor.R);
					binaryWriter.Write(newPlayer.shoeColor.G);
					binaryWriter.Write(newPlayer.shoeColor.B);
					for (int i = 0; i < 16; i++)
					{
						if (newPlayer.armor[i].name == null)
						{
							newPlayer.armor[i].name = "";
						}
						binaryWriter.Write(newPlayer.armor[i].netID);
						binaryWriter.Write(newPlayer.armor[i].prefix);
					}
					for (int j = 0; j < 8; j++)
					{
						binaryWriter.Write(newPlayer.dye[j].netID);
						binaryWriter.Write(newPlayer.dye[j].prefix);
					}
					for (int k = 0; k < 58; k++)
					{
						if (newPlayer.inventory[k].name == null)
						{
							newPlayer.inventory[k].name = "";
						}
						binaryWriter.Write(newPlayer.inventory[k].netID);
						binaryWriter.Write(newPlayer.inventory[k].stack);
						binaryWriter.Write(newPlayer.inventory[k].prefix);
					}
					for (int l = 0; l < Chest.maxItems; l++)
					{
						if (newPlayer.bank.item[l].name == null)
						{
							newPlayer.bank.item[l].name = "";
						}
						binaryWriter.Write(newPlayer.bank.item[l].netID);
						binaryWriter.Write(newPlayer.bank.item[l].stack);
						binaryWriter.Write(newPlayer.bank.item[l].prefix);
					}
					for (int m = 0; m < Chest.maxItems; m++)
					{
						if (newPlayer.bank2.item[m].name == null)
						{
							newPlayer.bank2.item[m].name = "";
						}
						binaryWriter.Write(newPlayer.bank2.item[m].netID);
						binaryWriter.Write(newPlayer.bank2.item[m].stack);
						binaryWriter.Write(newPlayer.bank2.item[m].prefix);
					}
					for (int n = 0; n < 22; n++)
					{
						if (Main.buffNoSave[newPlayer.buffType[n]])
						{
							binaryWriter.Write(0);
							binaryWriter.Write(0);
						}
						else
						{
							binaryWriter.Write(newPlayer.buffType[n]);
							binaryWriter.Write(newPlayer.buffTime[n]);
						}
					}
					for (int num = 0; num < 200; num++)
					{
						if (newPlayer.spN[num] == null)
						{
							binaryWriter.Write(-1);
							break;
						}
						binaryWriter.Write(newPlayer.spX[num]);
						binaryWriter.Write(newPlayer.spY[num]);
						binaryWriter.Write(newPlayer.spI[num]);
						binaryWriter.Write(newPlayer.spN[num]);
					}
					binaryWriter.Write(newPlayer.hbLocked);
					binaryWriter.Write(newPlayer.anglerQuestsFinished);
					binaryWriter.Close();
				}
			}
			Player.EncryptFile(text, playerPath);
			File.Delete(text);
		}
		public static Player LoadPlayer(string playerPath, bool decryptedCopy = false)
		{
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			Player player = new Player();
			try
			{
				string text = playerPath + ".dat";
				Player result;
				if (!decryptedCopy)
				{
					bool flag = Player.DecryptFile(playerPath, text);
					if (flag)
					{
						using (FileStream fileStream = new FileStream(playerPath, FileMode.Open))
						{
							while (fileStream.Position < fileStream.Length && fileStream.ReadByte() == 0)
							{
							}
							if (fileStream.Position == fileStream.Length)
							{
								player.loadStatus = 3;
							}
							else
							{
								player.loadStatus = 4;
							}
							string[] array = playerPath.Split(new char[]
							{
								Path.DirectorySeparatorChar
							});
							player.name = array[array.Length - 1].Split(new char[]
							{
								'.'
							})[0];
							result = player;
							return result;
						}
					}
				}
				using (FileStream fileStream2 = new FileStream(text, FileMode.Open))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream2))
					{
						int num = binaryReader.ReadInt32();
						if (num > Main.curRelease)
						{
							player.loadStatus = 1;
							player.name = binaryReader.ReadString();
							result = player;
							return result;
						}
						player.name = binaryReader.ReadString();
						if (num >= 10)
						{
							if (num >= 17)
							{
								player.difficulty = binaryReader.ReadByte();
							}
							else
							{
								bool flag2 = binaryReader.ReadBoolean();
								if (flag2)
								{
									player.difficulty = 2;
								}
							}
						}
						player.hair = binaryReader.ReadInt32();
						if (num >= 82)
						{
							player.hairDye = binaryReader.ReadByte();
						}
						if (num >= 83)
						{
							player.hideVisual = binaryReader.ReadByte();
						}
						if (num <= 17)
						{
							if (player.hair == 5 || player.hair == 6 || player.hair == 9 || player.hair == 11)
							{
								player.male = false;
							}
							else
							{
								player.male = true;
							}
						}
						else
						{
							player.male = binaryReader.ReadBoolean();
						}
						player.statLife = binaryReader.ReadInt32();
						player.statLifeMax = binaryReader.ReadInt32();
						if (player.statLifeMax > 500)
						{
							player.statLifeMax = 500;
						}
						player.statMana = binaryReader.ReadInt32();
						player.statManaMax = binaryReader.ReadInt32();
						if (player.statManaMax > 200)
						{
							player.statManaMax = 200;
						}
						if (player.statMana > 400)
						{
							player.statMana = 400;
						}
						player.hairColor.R = binaryReader.ReadByte();
						player.hairColor.G = binaryReader.ReadByte();
						player.hairColor.B = binaryReader.ReadByte();
						player.skinColor.R = binaryReader.ReadByte();
						player.skinColor.G = binaryReader.ReadByte();
						player.skinColor.B = binaryReader.ReadByte();
						player.eyeColor.R = binaryReader.ReadByte();
						player.eyeColor.G = binaryReader.ReadByte();
						player.eyeColor.B = binaryReader.ReadByte();
						player.shirtColor.R = binaryReader.ReadByte();
						player.shirtColor.G = binaryReader.ReadByte();
						player.shirtColor.B = binaryReader.ReadByte();
						player.underShirtColor.R = binaryReader.ReadByte();
						player.underShirtColor.G = binaryReader.ReadByte();
						player.underShirtColor.B = binaryReader.ReadByte();
						player.pantsColor.R = binaryReader.ReadByte();
						player.pantsColor.G = binaryReader.ReadByte();
						player.pantsColor.B = binaryReader.ReadByte();
						player.shoeColor.R = binaryReader.ReadByte();
						player.shoeColor.G = binaryReader.ReadByte();
						player.shoeColor.B = binaryReader.ReadByte();
						Main.player[Main.myPlayer].shirtColor = player.shirtColor;
						Main.player[Main.myPlayer].pantsColor = player.pantsColor;
						Main.player[Main.myPlayer].hairColor = player.hairColor;
						if (num >= 38)
						{
							int num2 = 11;
							if (num >= 81)
							{
								num2 = 16;
							}
							for (int i = 0; i < num2; i++)
							{
								player.armor[i].netDefaults(binaryReader.ReadInt32());
								player.armor[i].Prefix((int)binaryReader.ReadByte());
							}
							if (num >= 47)
							{
								int num3 = 3;
								if (num >= 81)
								{
									num3 = 8;
								}
								for (int j = 0; j < num3; j++)
								{
									player.dye[j].netDefaults(binaryReader.ReadInt32());
									player.dye[j].Prefix((int)binaryReader.ReadByte());
								}
							}
							if (num >= 58)
							{
								for (int k = 0; k < 58; k++)
								{
									int num4 = binaryReader.ReadInt32();
									if (num4 >= 2749)
									{
										player.inventory[k].netDefaults(0);
									}
									else
									{
										player.inventory[k].netDefaults(num4);
										player.inventory[k].stack = binaryReader.ReadInt32();
										player.inventory[k].Prefix((int)binaryReader.ReadByte());
									}
								}
							}
							else
							{
								for (int l = 0; l < 48; l++)
								{
									int num5 = binaryReader.ReadInt32();
									if (num5 >= 2749)
									{
										player.inventory[l].netDefaults(0);
									}
									else
									{
										player.inventory[l].netDefaults(num5);
										player.inventory[l].stack = binaryReader.ReadInt32();
										player.inventory[l].Prefix((int)binaryReader.ReadByte());
									}
								}
							}
							if (num >= 58)
							{
								for (int m = 0; m < 40; m++)
								{
									player.bank.item[m].netDefaults(binaryReader.ReadInt32());
									player.bank.item[m].stack = binaryReader.ReadInt32();
									player.bank.item[m].Prefix((int)binaryReader.ReadByte());
								}
								for (int n = 0; n < 40; n++)
								{
									player.bank2.item[n].netDefaults(binaryReader.ReadInt32());
									player.bank2.item[n].stack = binaryReader.ReadInt32();
									player.bank2.item[n].Prefix((int)binaryReader.ReadByte());
								}
							}
							else
							{
								for (int num6 = 0; num6 < 20; num6++)
								{
									player.bank.item[num6].netDefaults(binaryReader.ReadInt32());
									player.bank.item[num6].stack = binaryReader.ReadInt32();
									player.bank.item[num6].Prefix((int)binaryReader.ReadByte());
								}
								for (int num7 = 0; num7 < 20; num7++)
								{
									player.bank2.item[num7].netDefaults(binaryReader.ReadInt32());
									player.bank2.item[num7].stack = binaryReader.ReadInt32();
									player.bank2.item[num7].Prefix((int)binaryReader.ReadByte());
								}
							}
						}
						else
						{
							for (int num8 = 0; num8 < 8; num8++)
							{
								player.armor[num8].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
								if (num >= 36)
								{
									player.armor[num8].Prefix((int)binaryReader.ReadByte());
								}
							}
							if (num >= 6)
							{
								for (int num9 = 8; num9 < 11; num9++)
								{
									player.armor[num9].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
									if (num >= 36)
									{
										player.armor[num9].Prefix((int)binaryReader.ReadByte());
									}
								}
							}
							for (int num10 = 0; num10 < 44; num10++)
							{
								player.inventory[num10].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
								player.inventory[num10].stack = binaryReader.ReadInt32();
								if (num >= 36)
								{
									player.inventory[num10].Prefix((int)binaryReader.ReadByte());
								}
							}
							if (num >= 15)
							{
								for (int num11 = 44; num11 < 48; num11++)
								{
									player.inventory[num11].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
									player.inventory[num11].stack = binaryReader.ReadInt32();
									if (num >= 36)
									{
										player.inventory[num11].Prefix((int)binaryReader.ReadByte());
									}
								}
							}
							for (int num12 = 0; num12 < 20; num12++)
							{
								player.bank.item[num12].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
								player.bank.item[num12].stack = binaryReader.ReadInt32();
								if (num >= 36)
								{
									player.bank.item[num12].Prefix((int)binaryReader.ReadByte());
								}
							}
							if (num >= 20)
							{
								for (int num13 = 0; num13 < 20; num13++)
								{
									player.bank2.item[num13].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
									player.bank2.item[num13].stack = binaryReader.ReadInt32();
									if (num >= 36)
									{
										player.bank2.item[num13].Prefix((int)binaryReader.ReadByte());
									}
								}
							}
						}
						if (num < 58)
						{
							for (int num14 = 40; num14 < 48; num14++)
							{
								player.inventory[num14 + 10] = player.inventory[num14].Clone();
								player.inventory[num14].SetDefaults(0, false);
							}
						}
						if (num >= 11)
						{
							int num15 = 22;
							if (num < 74)
							{
								num15 = 10;
							}
							for (int num16 = 0; num16 < num15; num16++)
							{
								player.buffType[num16] = binaryReader.ReadInt32();
								player.buffTime[num16] = binaryReader.ReadInt32();
								if (player.buffType[num16] == 0)
								{
									num16--;
									num15--;
								}
							}
						}
						for (int num17 = 0; num17 < 200; num17++)
						{
							int num18 = binaryReader.ReadInt32();
							if (num18 == -1)
							{
								break;
							}
							player.spX[num17] = num18;
							player.spY[num17] = binaryReader.ReadInt32();
							player.spI[num17] = binaryReader.ReadInt32();
							player.spN[num17] = binaryReader.ReadString();
						}
						if (num >= 16)
						{
							player.hbLocked = binaryReader.ReadBoolean();
						}
						if (num >= 98)
						{
							player.anglerQuestsFinished = binaryReader.ReadInt32();
						}
						for (int num19 = 3; num19 < 8; num19++)
						{
							int type = player.armor[num19].type;
							if (type == 908)
							{
								player.lavaMax += 420;
							}
							if (type == 906)
							{
								player.lavaMax += 420;
							}
							if (player.wingsLogic == 0 && player.armor[num19].wingSlot >= 0)
							{
								player.wingsLogic = (int)player.armor[num19].wingSlot;
							}
							if (type == 158 || type == 396 || type == 1250 || type == 1251 || type == 1252)
							{
								player.noFallDmg = true;
							}
							player.lavaTime = player.lavaMax;
						}
						binaryReader.Close();
					}
				}
				player.PlayerFrame();
				if (decryptedCopy)
				{
					Player.EncryptFile(text, playerPath);
					File.Delete(text);
				}
				else
				{
					File.Delete(text);
				}
				player.loadStatus = 0;
				result = player;
				return result;
			}
			catch
			{
			}
			Player player2 = new Player();
			player2.loadStatus = 2;
			if (player.name != "")
			{
				player2.name = player.name;
			}
			else
			{
				string[] array2 = playerPath.Split(new char[]
				{
					Path.DirectorySeparatorChar
				});
				player.name = array2[array2.Length - 1].Split(new char[]
				{
					'.'
				})[0];
			}
			return player2;
		}
		public Color GetHairColor(bool lighting = true)
		{
			Color color = this.hairColor;
			if (this.hairDye == 1)
			{
				color.R = (byte)((float)this.statLife / (float)this.statLifeMax2 * 235f + 20f);
				color.B = 20;
				color.G = 20;
			}
			else
			{
				if (this.hairDye == 2)
				{
					color.R = (byte)((1f - (float)this.statMana / (float)this.statManaMax2) * 200f + 50f);
					color.B = 255;
					color.G = (byte)((1f - (float)this.statMana / (float)this.statManaMax2) * 180f + 75f);
				}
				else
				{
					if (this.hairDye == 3)
					{
						float num = (float)(Main.worldSurface * 0.45) * 16f;
						float num2 = (float)(Main.worldSurface + Main.rockLayer) * 8f;
						float num3 = (float)(Main.rockLayer + (double)Main.maxTilesY) * 8f;
						float num4 = (float)(Main.maxTilesY - 150) * 16f;
						if (this.center().Y < num)
						{
							float num5 = this.center().Y / num;
							float num6 = 1f - num5;
							color.R = (byte)(116f * num6 + 28f * num5);
							color.G = (byte)(160f * num6 + 216f * num5);
							color.B = (byte)(249f * num6 + 94f * num5);
						}
						else
						{
							if (this.center().Y < num2)
							{
								float num7 = num;
								float num8 = (this.center().Y - num7) / (num2 - num7);
								float num9 = 1f - num8;
								color.R = (byte)(28f * num9 + 151f * num8);
								color.G = (byte)(216f * num9 + 107f * num8);
								color.B = (byte)(94f * num9 + 75f * num8);
							}
							else
							{
								if (this.center().Y < num3)
								{
									float num10 = num2;
									float num11 = (this.center().Y - num10) / (num3 - num10);
									float num12 = 1f - num11;
									color.R = (byte)(151f * num12 + 128f * num11);
									color.G = (byte)(107f * num12 + 128f * num11);
									color.B = (byte)(75f * num12 + 128f * num11);
								}
								else
								{
									if (this.center().Y < num4)
									{
										float num13 = num3;
										float num14 = (this.center().Y - num13) / (num4 - num13);
										float num15 = 1f - num14;
										color.R = (byte)(128f * num15 + 255f * num14);
										color.G = (byte)(128f * num15 + 50f * num14);
										color.B = (byte)(128f * num15 + 15f * num14);
									}
									else
									{
										color.R = 255;
										color.G = 50;
										color.B = 10;
									}
								}
							}
						}
					}
					else
					{
						if (this.hairDye == 4)
						{
							int num16 = 0;
							for (int i = 0; i < 54; i++)
							{
								if (this.inventory[i].type == 71)
								{
									num16 += this.inventory[i].stack;
								}
								if (this.inventory[i].type == 72)
								{
									num16 += this.inventory[i].stack * 100;
								}
								if (this.inventory[i].type == 73)
								{
									num16 += this.inventory[i].stack * 10000;
								}
								if (this.inventory[i].type == 74)
								{
									num16 += this.inventory[i].stack * 1000000;
								}
							}
							float num17 = (float)Item.buyPrice(0, 5, 0, 0);
							float num18 = (float)Item.buyPrice(0, 50, 0, 0);
							float num19 = (float)Item.buyPrice(2, 0, 0, 0);
							Color color2 = new Color(226, 118, 76);
							Color color3 = new Color(174, 194, 196);
							Color color4 = new Color(204, 181, 72);
							Color color5 = new Color(161, 172, 173);
							if ((float)num16 < num17)
							{
								float num20 = (float)num16 / num17;
								float num21 = 1f - num20;
								color.R = (byte)((float)color2.R * num21 + (float)color3.R * num20);
								color.G = (byte)((float)color2.G * num21 + (float)color3.G * num20);
								color.B = (byte)((float)color2.B * num21 + (float)color3.B * num20);
							}
							else
							{
								if ((float)num16 < num18)
								{
									float num22 = num17;
									float num23 = ((float)num16 - num22) / (num18 - num22);
									float num24 = 1f - num23;
									color.R = (byte)((float)color3.R * num24 + (float)color4.R * num23);
									color.G = (byte)((float)color3.G * num24 + (float)color4.G * num23);
									color.B = (byte)((float)color3.B * num24 + (float)color4.B * num23);
								}
								else
								{
									if ((float)num16 < num19)
									{
										float num25 = num18;
										float num26 = ((float)num16 - num25) / (num19 - num25);
										float num27 = 1f - num26;
										color.R = (byte)((float)color4.R * num27 + (float)color5.R * num26);
										color.G = (byte)((float)color4.G * num27 + (float)color5.G * num26);
										color.B = (byte)((float)color4.B * num27 + (float)color5.B * num26);
									}
									else
									{
										color = color5;
									}
								}
							}
						}
						else
						{
							if (this.hairDye == 5)
							{
								Color color6 = new Color(1, 142, 255);
								Color color7 = new Color(255, 255, 0);
								Color color8 = new Color(211, 45, 127);
								Color color9 = new Color(67, 44, 118);
								if (Main.dayTime)
								{
									if (Main.time < 27000.0)
									{
										float num28 = (float)(Main.time / 27000.0);
										float num29 = 1f - num28;
										color.R = (byte)((float)color6.R * num29 + (float)color7.R * num28);
										color.G = (byte)((float)color6.G * num29 + (float)color7.G * num28);
										color.B = (byte)((float)color6.B * num29 + (float)color7.B * num28);
									}
									else
									{
										float num30 = 27000f;
										float num31 = (float)((Main.time - (double)num30) / (54000.0 - (double)num30));
										float num32 = 1f - num31;
										color.R = (byte)((float)color7.R * num32 + (float)color8.R * num31);
										color.G = (byte)((float)color7.G * num32 + (float)color8.G * num31);
										color.B = (byte)((float)color7.B * num32 + (float)color8.B * num31);
									}
								}
								else
								{
									if (Main.time < 16200.0)
									{
										float num33 = (float)(Main.time / 16200.0);
										float num34 = 1f - num33;
										color.R = (byte)((float)color8.R * num34 + (float)color9.R * num33);
										color.G = (byte)((float)color8.G * num34 + (float)color9.G * num33);
										color.B = (byte)((float)color8.B * num34 + (float)color9.B * num33);
									}
									else
									{
										float num35 = 16200f;
										float num36 = (float)((Main.time - (double)num35) / (32400.0 - (double)num35));
										float num37 = 1f - num36;
										color.R = (byte)((float)color9.R * num37 + (float)color6.R * num36);
										color.G = (byte)((float)color9.G * num37 + (float)color6.G * num36);
										color.B = (byte)((float)color9.B * num37 + (float)color6.B * num36);
									}
								}
							}
							else
							{
								if (this.hairDye == 6)
								{
									if (this.team == 1)
									{
										color = new Color(255, 0, 0);
									}
									else
									{
										if (this.team == 2)
										{
											color = new Color(0, 255, 0);
										}
										else
										{
											if (this.team == 3)
											{
												color = new Color(0, 0, 255);
											}
											else
											{
												if (this.team == 4)
												{
													color = new Color(255, 255, 0);
												}
											}
										}
									}
								}
								else
								{
									if (this.hairDye == 7)
									{
										Color color10 = default(Color);
										if (Main.waterStyle == 2)
										{
											color10 = new Color(124, 118, 242);
										}
										else
										{
											if (Main.waterStyle == 3)
											{
												color10 = new Color(143, 215, 29);
											}
											else
											{
												if (Main.waterStyle == 4)
												{
													color10 = new Color(78, 193, 227);
												}
												else
												{
													if (Main.waterStyle == 5)
													{
														color10 = new Color(189, 231, 255);
													}
													else
													{
														if (Main.waterStyle == 6)
														{
															color10 = new Color(230, 219, 100);
														}
														else
														{
															if (Main.waterStyle == 7)
															{
																color10 = new Color(151, 107, 75);
															}
															else
															{
																if (Main.waterStyle == 8)
																{
																	color10 = new Color(128, 128, 128);
																}
																else
																{
																	if (Main.waterStyle == 9)
																	{
																		color10 = new Color(200, 0, 0);
																	}
																	else
																	{
																		if (Main.waterStyle == 10)
																		{
																			color10 = new Color(208, 80, 80);
																		}
																		else
																		{
																			color10 = new Color(28, 216, 94);
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
										if (this.hairDyeColor.A == 0)
										{
											this.hairDyeColor = color10;
										}
										if (this.hairDyeColor.R > color10.R)
										{
											this.hairDyeColor.R = this.hairDyeColor.R - 1;
										}
										if (this.hairDyeColor.R < color10.R)
										{
											this.hairDyeColor.R = this.hairDyeColor.R + 1;
										}
										if (this.hairDyeColor.G > color10.G)
										{
											this.hairDyeColor.G = this.hairDyeColor.G - 1;
										}
										if (this.hairDyeColor.G < color10.G)
										{
											this.hairDyeColor.G = this.hairDyeColor.G + 1;
										}
										if (this.hairDyeColor.B > color10.B)
										{
											this.hairDyeColor.B = this.hairDyeColor.B - 1;
										}
										if (this.hairDyeColor.B < color10.B)
										{
											this.hairDyeColor.B = this.hairDyeColor.B + 1;
										}
										color = this.hairDyeColor;
									}
									else
									{
										if (this.hairDye == 8)
										{
											color = new Color(244, 22, 175);
											if (!Main.gameMenu && !Main.gamePaused)
											{
												if (Main.rand.Next(45) == 0)
												{
													int type = Main.rand.Next(139, 143);
													int num38 = Dust.NewDust(this.position, this.width, 8, type, 0f, 0f, 0, default(Color), 1.2f);
													Dust expr_BE5_cp_0 = Main.dust[num38];
													expr_BE5_cp_0.velocity.X = expr_BE5_cp_0.velocity.X * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
													Dust expr_C19_cp_0 = Main.dust[num38];
													expr_C19_cp_0.velocity.Y = expr_C19_cp_0.velocity.Y * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
													Dust expr_C4D_cp_0 = Main.dust[num38];
													expr_C4D_cp_0.velocity.X = expr_C4D_cp_0.velocity.X + (float)Main.rand.Next(-50, 51) * 0.01f;
													Dust expr_C7B_cp_0 = Main.dust[num38];
													expr_C7B_cp_0.velocity.Y = expr_C7B_cp_0.velocity.Y + (float)Main.rand.Next(-50, 51) * 0.01f;
													Dust expr_CA9_cp_0 = Main.dust[num38];
													expr_CA9_cp_0.velocity.Y = expr_CA9_cp_0.velocity.Y - 1f;
													Main.dust[num38].scale *= 0.7f + (float)Main.rand.Next(-30, 31) * 0.01f;
													Main.dust[num38].velocity += this.velocity * 0.2f;
												}
												if (Main.rand.Next(225) == 0)
												{
													int type2 = Main.rand.Next(276, 283);
													int num39 = Gore.NewGore(new Vector2(this.position.X + (float)Main.rand.Next(this.width), this.position.Y + (float)Main.rand.Next(8)), this.velocity, type2, 1f);
													Gore expr_D96_cp_0 = Main.gore[num39];
													expr_D96_cp_0.velocity.X = expr_D96_cp_0.velocity.X * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
													Gore expr_DCA_cp_0 = Main.gore[num39];
													expr_DCA_cp_0.velocity.Y = expr_DCA_cp_0.velocity.Y * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
													Main.gore[num39].scale *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
													Gore expr_E2D_cp_0 = Main.gore[num39];
													expr_E2D_cp_0.velocity.X = expr_E2D_cp_0.velocity.X + (float)Main.rand.Next(-50, 51) * 0.01f;
													Gore expr_E5B_cp_0 = Main.gore[num39];
													expr_E5B_cp_0.velocity.Y = expr_E5B_cp_0.velocity.Y + (float)Main.rand.Next(-50, 51) * 0.01f;
													Gore expr_E89_cp_0 = Main.gore[num39];
													expr_E89_cp_0.velocity.Y = expr_E89_cp_0.velocity.Y - 1f;
													Main.gore[num39].velocity += this.velocity * 0.2f;
												}
											}
										}
										else
										{
											if (this.hairDye == 9)
											{
												color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
											}
											else
											{
												if (this.hairDye == 10)
												{
													float num40 = Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y);
													float num41 = 10f;
													if (num40 > num41)
													{
														num40 = num41;
													}
													float num42 = num40 / num41;
													float num43 = 1f - num42;
													color.R = (byte)(75f * num42 + (float)this.hairColor.R * num43);
													color.G = (byte)(255f * num42 + (float)this.hairColor.G * num43);
													color.B = (byte)(200f * num42 + (float)this.hairColor.B * num43);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (lighting)
			{
				color = Lighting.GetColor((int)((double)this.position.X + (double)this.width * 0.5) / 16, (int)(((double)this.position.Y + (double)this.height * 0.25) / 16.0), color);
			}
			return color;
		}
		public bool HasItem(int type)
		{
			for (int i = 0; i < 58; i++)
			{
				if (type == this.inventory[i].type && this.inventory[i].stack > 0)
				{
					return true;
				}
			}
			return false;
		}
		public int FindItem(int netid)
		{
			for (int i = 0; i < 58; i++)
			{
				if (netid == this.inventory[i].netID && this.inventory[i].stack > 0)
				{
					return i;
				}
			}
			return -1;
		}
		public void QuickGrapple()
		{
			if (this.mount.Active)
			{
				this.mount.Dismount(this);
			}
			if (this.noItems)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < 58; i++)
			{
				if (this.inventory[i].shoot == 13 || this.inventory[i].shoot == 32 || this.inventory[i].shoot == 73 || this.inventory[i].shoot == 165 || (this.inventory[i].shoot >= 230 && this.inventory[i].shoot <= 235) || this.inventory[i].shoot == 256 || this.inventory[i].shoot == 315 || this.inventory[i].shoot == 322 || this.inventory[i].shoot == 331 || this.inventory[i].shoot == 332 || this.inventory[i].shoot == 372 || this.inventory[i].shoot == 396)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				return;
			}
			if (this.inventory[num].shoot == 73)
			{
				int num2 = 0;
				if (num >= 0)
				{
					for (int j = 0; j < 1000; j++)
					{
						if (Main.projectile[j].active && Main.projectile[j].owner == Main.myPlayer && (Main.projectile[j].type == 73 || Main.projectile[j].type == 74))
						{
							num2++;
						}
					}
				}
				if (num2 > 1)
				{
					num = -1;
				}
			}
			else
			{
				if (this.inventory[num].shoot == 165)
				{
					int num3 = 0;
					if (num >= 0)
					{
						for (int k = 0; k < 1000; k++)
						{
							if (Main.projectile[k].active && Main.projectile[k].owner == Main.myPlayer && Main.projectile[k].type == 165)
							{
								num3++;
							}
						}
					}
					if (num3 > 8)
					{
						num = -1;
					}
				}
				else
				{
					if (num >= 0)
					{
						for (int l = 0; l < 1000; l++)
						{
							if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == this.inventory[num].shoot && Main.projectile[l].ai[0] != 2f)
							{
								num = -1;
								break;
							}
						}
					}
				}
			}
			if (num >= 0)
			{
				Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, this.inventory[num].useSound);
				if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
				{
					NetMessage.SendData(51, -1, -1, "", this.whoAmi, 2f, 0f, 0f, 0);
				}
				int num4 = this.inventory[num].shoot;
				float shootSpeed = this.inventory[num].shootSpeed;
				int damage = this.inventory[num].damage;
				float knockBack = this.inventory[num].knockBack;
				if (num4 == 13 || num4 == 32 || num4 == 315 || (num4 >= 230 && num4 <= 235) || num4 == 331)
				{
					this.grappling[0] = -1;
					this.grapCount = 0;
					for (int m = 0; m < 1000; m++)
					{
						if (Main.projectile[m].active && Main.projectile[m].owner == this.whoAmi)
						{
							if (Main.projectile[m].type == 13)
							{
								Main.projectile[m].Kill();
							}
							if (Main.projectile[m].type == 331)
							{
								Main.projectile[m].Kill();
							}
							if (Main.projectile[m].type == 315)
							{
								Main.projectile[m].Kill();
							}
							if (Main.projectile[m].type >= 230 && Main.projectile[m].type <= 235)
							{
								Main.projectile[m].Kill();
							}
						}
					}
				}
				if (num4 == 256)
				{
					int num5 = 0;
					int num6 = -1;
					int num7 = 100000;
					for (int n = 0; n < 1000; n++)
					{
						if (Main.projectile[n].active && Main.projectile[n].owner == this.whoAmi && Main.projectile[n].type == 256)
						{
							num5++;
							if (Main.projectile[n].timeLeft < num7)
							{
								num6 = n;
								num7 = Main.projectile[n].timeLeft;
							}
						}
					}
					if (num5 > 1)
					{
						Main.projectile[num6].Kill();
					}
				}
				if (num4 == 73)
				{
					for (int num8 = 0; num8 < 1000; num8++)
					{
						if (Main.projectile[num8].active && Main.projectile[num8].owner == this.whoAmi && Main.projectile[num8].type == 73)
						{
							num4 = 74;
						}
					}
				}
				Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
				float num9 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
				float num10 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
				if (this.gravDir == -1f)
				{
					num10 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector.Y;
				}
				float num11 = (float)Math.Sqrt((double)(num9 * num9 + num10 * num10));
				num11 = shootSpeed / num11;
				num9 *= num11;
				num10 *= num11;
				Projectile.NewProjectile(vector.X, vector.Y, num9, num10, num4, damage, knockBack, this.whoAmi, 0f, 0f);
			}
		}
		public Player()
		{
			this.name = string.Empty;
			for (int i = 0; i < 59; i++)
			{
				if (i < 16)
				{
					this.armor[i] = new Item();
					this.armor[i].name = "";
				}
				this.inventory[i] = new Item();
				this.inventory[i].name = "";
			}
			for (int j = 0; j < Chest.maxItems; j++)
			{
				this.bank.item[j] = new Item();
				this.bank.item[j].name = "";
				this.bank2.item[j] = new Item();
				this.bank2.item[j].name = "";
			}
			for (int k = 0; k < 8; k++)
			{
				this.dye[k] = new Item();
			}
			this.grappling[0] = -1;
			this.inventory[0].SetDefaults("Copper Shortsword");
			this.inventory[1].SetDefaults("Copper Pickaxe");
			this.inventory[2].SetDefaults("Copper Axe");
			this.statManaMax = 20;
			if (Main.cEd)
			{
				this.inventory[3].SetDefaults(603, false);
			}
			for (int l = 0; l < 340; l++)
			{
				this.adjTile[l] = false;
				this.oldAdjTile[l] = false;
			}
			this.hitTile = new HitTile();
			this.mount = new Mount();
		}
		public void TeleportationPotion()
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = this.width;
			Vector2 vector = new Vector2((float)num2, (float)num3) * 16f + new Vector2((float)(-(float)num4 / 2 + 8), (float)(-(float)this.height));
			while (!flag && num < 1000)
			{
				num++;
				num2 = 100 + Main.rand.Next(Main.maxTilesX - 200);
				num3 = 100 + Main.rand.Next(Main.maxTilesY - 200);
				vector = new Vector2((float)num2, (float)num3) * 16f + new Vector2((float)(-(float)num4 / 2 + 8), (float)(-(float)this.height));
				if (!Collision.SolidCollision(vector, num4, this.height))
				{
					if (Main.tile[num2, num3] == null)
					{
						Main.tile[num2, num3] = new Tile();
					}
					if ((Main.tile[num2, num3].wall != 87 || (double)num3 <= Main.worldSurface || NPC.downedPlantBoss) && (!Main.wallDungeon[(int)Main.tile[num2, num3].wall] || (double)num3 <= Main.worldSurface || NPC.downedBoss3))
					{
						int i = 0;
						while (i < 100)
						{
							if (Main.tile[num2, num3 + i] == null)
							{
								Main.tile[num2, num3 + i] = new Tile();
							}
							Tile tile = Main.tile[num2, num3 + i];
							vector = new Vector2((float)num2, (float)(num3 + i)) * 16f + new Vector2((float)(-(float)num4 / 2 + 8), (float)(-(float)this.height));
							Vector4 vector2 = Collision.SlopeCollision(vector, this.velocity, num4, this.height, this.gravDir, false);
							bool flag2 = !Collision.SolidCollision(vector, num4, this.height);
							if (flag2)
							{
								i++;
							}
							else
							{
								if (tile.active() && !tile.inActive() && Main.tileSolid[(int)tile.type])
								{
									break;
								}
								i++;
							}
						}
						if (!Collision.LavaCollision(vector, num4, this.height) && Collision.HurtTiles(vector, this.velocity, num4, this.height, false).Y <= 0f)
						{
							Vector4 vector3 = Collision.SlopeCollision(vector, this.velocity, num4, this.height, this.gravDir, false);
							if (Collision.SolidCollision(vector, num4, this.height) && i < 99)
							{
								Vector2 value = Vector2.UnitX * 16f;
								if (!(Collision.TileCollision(vector - value, value, this.width, this.height, false, false, (int)this.gravDir) != value))
								{
									value = -Vector2.UnitX * 16f;
									if (!(Collision.TileCollision(vector - value, value, this.width, this.height, false, false, (int)this.gravDir) != value))
									{
										value = Vector2.UnitY * 16f;
										if (!(Collision.TileCollision(vector - value, value, this.width, this.height, false, false, (int)this.gravDir) != value))
										{
											value = -Vector2.UnitY * 16f;
											if (!(Collision.TileCollision(vector - value, value, this.width, this.height, false, false, (int)this.gravDir) != value))
											{
												flag = true;
												num3 += i;
												break;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (flag)
			{
				Vector2 newPos = vector;
				this.Teleport(newPos, 2);
				this.velocity = Vector2.Zero;
				if (Main.netMode == 2)
				{
					ServerSock.CheckSection(this.whoAmi, this.position);
					NetMessage.SendData(65, -1, -1, "", 0, (float)this.whoAmi, newPos.X, newPos.Y, 3);
				}
			}
		}
		public void GetAnglerReward()
		{
			Item item = new Item();
			item.type = 0;
			float num = 1f;
			if (this.anglerQuestsFinished <= 50)
			{
				num -= (float)this.anglerQuestsFinished * 0.01f;
			}
			else
			{
				if (this.anglerQuestsFinished <= 100)
				{
					num = 0.5f - (float)(this.anglerQuestsFinished - 50) * 0.005f;
				}
				else
				{
					if (this.anglerQuestsFinished <= 150)
					{
						num = 0.25f - (float)(this.anglerQuestsFinished - 100) * 0.002f;
					}
				}
			}
			if (this.anglerQuestsFinished == 10)
			{
				item.SetDefaults(2428, false);
			}
			else
			{
				if (this.anglerQuestsFinished == 20)
				{
					item.SetDefaults(2367, false);
				}
				else
				{
					if (this.anglerQuestsFinished == 30)
					{
						item.SetDefaults(2368, false);
					}
					else
					{
						if (this.anglerQuestsFinished == 40)
						{
							item.SetDefaults(2369, false);
						}
						else
						{
							if (this.anglerQuestsFinished == 50)
							{
								item.SetDefaults(2294, false);
							}
							else
							{
								if (this.anglerQuestsFinished > 75 && Main.rand.Next((int)(250f * num)) == 0)
								{
									item.SetDefaults(2294, false);
								}
								else
								{
									if (Main.hardMode && this.anglerQuestsFinished > 25 && Main.rand.Next((int)(150f * num)) == 0)
									{
										item.SetDefaults(2422, false);
									}
									else
									{
										if (Main.hardMode && this.anglerQuestsFinished > 10 && Main.rand.Next((int)(100f * num)) == 0)
										{
											item.SetDefaults(2494, false);
										}
										else
										{
											if (Main.rand.Next((int)(75f * num)) == 0)
											{
												item.SetDefaults(2360, false);
											}
											else
											{
												if (Main.rand.Next((int)(50f * num)) == 0)
												{
													item.SetDefaults(2373, false);
												}
												else
												{
													if (Main.rand.Next((int)(50f * num)) == 0)
													{
														item.SetDefaults(2374, false);
													}
													else
													{
														if (Main.rand.Next((int)(50f * num)) == 0)
														{
															item.SetDefaults(2375, false);
														}
														else
														{
															if (Main.rand.Next((int)(50f * num)) == 0)
															{
																item.SetDefaults(2417, false);
															}
															else
															{
																if (Main.rand.Next((int)(50f * num)) == 0)
																{
																	item.SetDefaults(2498, false);
																}
																else
																{
																	int num2 = Main.rand.Next(27);
																	if (num2 == 0)
																	{
																		item.SetDefaults(2442, false);
																	}
																	else
																	{
																		if (num2 == 1)
																		{
																			item.SetDefaults(2443, false);
																		}
																		else
																		{
																			if (num2 == 2)
																			{
																				item.SetDefaults(2444, false);
																			}
																			else
																			{
																				if (num2 == 3)
																				{
																					item.SetDefaults(2445, false);
																				}
																				else
																				{
																					if (num2 == 4)
																					{
																						item.SetDefaults(2497, false);
																					}
																					else
																					{
																						if (num2 == 5)
																						{
																							item.SetDefaults(2495, false);
																						}
																						else
																						{
																							if (num2 == 6)
																							{
																								item.SetDefaults(2446, false);
																							}
																							else
																							{
																								if (num2 == 7)
																								{
																									item.SetDefaults(2447, false);
																								}
																								else
																								{
																									if (num2 == 8)
																									{
																										item.SetDefaults(2448, false);
																									}
																									else
																									{
																										if (num2 == 9)
																										{
																											item.SetDefaults(2449, false);
																										}
																										else
																										{
																											if (num2 == 10)
																											{
																												item.SetDefaults(2490, false);
																											}
																											else
																											{
																												if (num2 == 11)
																												{
																													item.SetDefaults(2435, false);
																													item.stack = Main.rand.Next(50, 151);
																												}
																												else
																												{
																													if (num2 == 12)
																													{
																														item.SetDefaults(2496, false);
																													}
																													else
																													{
																														if (num2 == 13 || num2 == 14)
																														{
																															item.SetDefaults(2354, false);
																															item.stack = Main.rand.Next(2, 6);
																														}
																														else
																														{
																															if (num2 == 15 || num2 == 16)
																															{
																																item.SetDefaults(2355, false);
																																item.stack = Main.rand.Next(2, 6);
																															}
																															else
																															{
																																if (num2 == 17 || num2 == 18)
																																{
																																	item.SetDefaults(2356, false);
																																	item.stack = Main.rand.Next(2, 6);
																																}
																																else
																																{
																																	int num3 = (this.anglerQuestsFinished + 50) / 2;
																																	num3 = (int)((float)(num3 * Main.rand.Next(50, 201)) * 0.01f);
																																	if (num3 > 100)
																																	{
																																		num3 /= 100;
																																		if (num3 > 10)
																																		{
																																			num3 = 10;
																																		}
																																		if (num3 < 1)
																																		{
																																			num3 = 1;
																																		}
																																		item.SetDefaults(73, false);
																																		item.stack = num3;
																																	}
																																	else
																																	{
																																		if (num3 > 99)
																																		{
																																			num3 = 99;
																																		}
																																		if (num3 < 1)
																																		{
																																			num3 = 1;
																																		}
																																		item.SetDefaults(72, false);
																																		item.stack = num3;
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			item.position = this.center();
			Item item2 = this.GetItem(this.whoAmi, item, true);
			if (item2.stack > 0)
			{
				int number = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, item2.type, item2.stack, false, 0, true);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", number, 1f, 0f, 0f, 0);
				}
			}
			if (item.type == 2417)
			{
				Item item3 = new Item();
				Item item4 = new Item();
				item3.SetDefaults(2418, false);
				item3.position = this.center();
				item2 = this.GetItem(this.whoAmi, item3, true);
				if (item2.stack > 0)
				{
					int number2 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, item2.type, item2.stack, false, 0, true);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", number2, 1f, 0f, 0f, 0);
					}
				}
				item4.SetDefaults(2419, false);
				item4.position = this.center();
				item2 = this.GetItem(this.whoAmi, item4, true);
				if (item2.stack > 0)
				{
					int number3 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, item2.type, item2.stack, false, 0, true);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", number3, 1f, 0f, 0f, 0);
					}
				}
			}
			else
			{
				if (item.type == 2498)
				{
					Item item5 = new Item();
					Item item6 = new Item();
					item5.SetDefaults(2499, false);
					item5.position = this.center();
					item2 = this.GetItem(this.whoAmi, item5, true);
					if (item2.stack > 0)
					{
						int number4 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, item2.type, item2.stack, false, 0, true);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number4, 1f, 0f, 0f, 0);
						}
					}
					item6.SetDefaults(2500, false);
					item6.position = this.center();
					item2 = this.GetItem(this.whoAmi, item6, true);
					if (item2.stack > 0)
					{
						int number5 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, item2.type, item2.stack, false, 0, true);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", number5, 1f, 0f, 0f, 0);
						}
					}
				}
			}
			if (Main.rand.Next((int)(100f * num)) <= 50)
			{
				Item item7 = new Item();
				if (Main.rand.Next((int)(15f * num)) == 0)
				{
					item7.SetDefaults(2676, false);
				}
				else
				{
					if (Main.rand.Next((int)(5f * num)) == 0)
					{
						item7.SetDefaults(2675, false);
					}
					else
					{
						item7.SetDefaults(2674, false);
					}
				}
				if (Main.rand.Next(25) <= this.anglerQuestsFinished)
				{
					item7.stack++;
				}
				if (Main.rand.Next(50) <= this.anglerQuestsFinished)
				{
					item7.stack++;
				}
				if (Main.rand.Next(100) <= this.anglerQuestsFinished)
				{
					item7.stack++;
				}
				if (Main.rand.Next(150) <= this.anglerQuestsFinished)
				{
					item7.stack++;
				}
				if (Main.rand.Next(200) <= this.anglerQuestsFinished)
				{
					item7.stack++;
				}
				if (Main.rand.Next(250) <= this.anglerQuestsFinished)
				{
					item7.stack++;
				}
				item7.position = this.center();
				item2 = this.GetItem(this.whoAmi, item7, true);
				if (item2.stack > 0)
				{
					int number6 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, item2.type, item2.stack, false, 0, true);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", number6, 1f, 0f, 0f, 0);
					}
				}
			}
		}
	}
}
