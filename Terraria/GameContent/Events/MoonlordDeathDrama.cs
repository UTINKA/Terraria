// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Events.MoonlordDeathDrama
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.Graphics;
using Terraria.Utilities;

namespace Terraria.GameContent.Events
{
  public class MoonlordDeathDrama
  {
    private static List<MoonlordDeathDrama.MoonlordPiece> _pieces = new List<MoonlordDeathDrama.MoonlordPiece>();
    private static List<MoonlordDeathDrama.MoonlordExplosion> _explosions = new List<MoonlordDeathDrama.MoonlordExplosion>();
    private static List<Vector2> _lightSources = new List<Vector2>();
    private static float whitening = 0.0f;
    private static float requestedLight = 0.0f;

    public static void Update()
    {
      for (int index = 0; index < MoonlordDeathDrama._pieces.Count; ++index)
      {
        MoonlordDeathDrama.MoonlordPiece piece = MoonlordDeathDrama._pieces[index];
        piece.Update();
        if (piece.Dead)
        {
          MoonlordDeathDrama._pieces.Remove(piece);
          --index;
        }
      }
      for (int index = 0; index < MoonlordDeathDrama._explosions.Count; ++index)
      {
        MoonlordDeathDrama.MoonlordExplosion explosion = MoonlordDeathDrama._explosions[index];
        explosion.Update();
        if (explosion.Dead)
        {
          MoonlordDeathDrama._explosions.Remove(explosion);
          --index;
        }
      }
      bool flag = false;
      for (int index = 0; index < MoonlordDeathDrama._lightSources.Count; ++index)
      {
        if ((double) Main.player[Main.myPlayer].Distance(MoonlordDeathDrama._lightSources[index]) < 2000.0)
        {
          flag = true;
          break;
        }
      }
      MoonlordDeathDrama._lightSources.Clear();
      if (!flag)
        MoonlordDeathDrama.requestedLight = 0.0f;
      if ((double) MoonlordDeathDrama.requestedLight != (double) MoonlordDeathDrama.whitening)
      {
        if ((double) Math.Abs(MoonlordDeathDrama.requestedLight - MoonlordDeathDrama.whitening) < 0.0199999995529652)
          MoonlordDeathDrama.whitening = MoonlordDeathDrama.requestedLight;
        else
          MoonlordDeathDrama.whitening += (float) Math.Sign(MoonlordDeathDrama.requestedLight - MoonlordDeathDrama.whitening) * 0.02f;
      }
      MoonlordDeathDrama.requestedLight = 0.0f;
    }

    public static void DrawPieces(SpriteBatch spriteBatch)
    {
      Rectangle playerScreen = Utils.CenteredRectangle(Vector2.op_Addition(Main.screenPosition, Vector2.op_Multiply(new Vector2((float) Main.screenWidth, (float) Main.screenHeight), 0.5f)), new Vector2((float) (Main.screenWidth + 1000), (float) (Main.screenHeight + 1000)));
      for (int index = 0; index < MoonlordDeathDrama._pieces.Count; ++index)
      {
        if (MoonlordDeathDrama._pieces[index].InDrawRange(playerScreen))
          MoonlordDeathDrama._pieces[index].Draw(spriteBatch);
      }
    }

    public static void DrawExplosions(SpriteBatch spriteBatch)
    {
      Rectangle playerScreen = Utils.CenteredRectangle(Vector2.op_Addition(Main.screenPosition, Vector2.op_Multiply(new Vector2((float) Main.screenWidth, (float) Main.screenHeight), 0.5f)), new Vector2((float) (Main.screenWidth + 1000), (float) (Main.screenHeight + 1000)));
      for (int index = 0; index < MoonlordDeathDrama._explosions.Count; ++index)
      {
        if (MoonlordDeathDrama._explosions[index].InDrawRange(playerScreen))
          MoonlordDeathDrama._explosions[index].Draw(spriteBatch);
      }
    }

    public static void DrawWhite(SpriteBatch spriteBatch)
    {
      if ((double) MoonlordDeathDrama.whitening == 0.0)
        return;
      Color color = Color.op_Multiply(Color.get_White(), MoonlordDeathDrama.whitening);
      spriteBatch.Draw(Main.magicPixel, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
    }

    public static void ThrowPieces(Vector2 MoonlordCoreCenter, int DramaSeed)
    {
      UnifiedRandom r = new UnifiedRandom(DramaSeed);
      Vector2 vector2_1 = Vector2.get_UnitY().RotatedBy((double) r.NextFloat() * 1.57079637050629 - 0.785398185253143 + 3.14159274101257, (Vector2) null);
      MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(TextureManager.Load("Images/Misc/MoonExplosion/Spine"), new Vector2(64f, 150f), Vector2.op_Addition(MoonlordCoreCenter, new Vector2(0.0f, 50f)), Vector2.op_Multiply(vector2_1, 6f), 0.0f, (float) ((double) r.NextFloat() * 0.100000001490116 - 0.0500000007450581)));
      Vector2 vector2_2 = Vector2.get_UnitY().RotatedBy((double) r.NextFloat() * 1.57079637050629 - 0.785398185253143 + 3.14159274101257, (Vector2) null);
      MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(TextureManager.Load("Images/Misc/MoonExplosion/Shoulder"), new Vector2(40f, 120f), Vector2.op_Addition(MoonlordCoreCenter, new Vector2(50f, -120f)), Vector2.op_Multiply(vector2_2, 10f), 0.0f, (float) ((double) r.NextFloat() * 0.100000001490116 - 0.0500000007450581)));
      Vector2 vector2_3 = Vector2.get_UnitY().RotatedBy((double) r.NextFloat() * 1.57079637050629 - 0.785398185253143 + 3.14159274101257, (Vector2) null);
      MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(TextureManager.Load("Images/Misc/MoonExplosion/Torso"), new Vector2(192f, 252f), MoonlordCoreCenter, Vector2.op_Multiply(vector2_3, 8f), 0.0f, (float) ((double) r.NextFloat() * 0.100000001490116 - 0.0500000007450581)));
      Vector2 vector2_4 = Vector2.get_UnitY().RotatedBy((double) r.NextFloat() * 1.57079637050629 - 0.785398185253143 + 3.14159274101257, (Vector2) null);
      MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(TextureManager.Load("Images/Misc/MoonExplosion/Head"), new Vector2(138f, 185f), Vector2.op_Subtraction(MoonlordCoreCenter, new Vector2(0.0f, 200f)), Vector2.op_Multiply(vector2_4, 12f), 0.0f, (float) ((double) r.NextFloat() * 0.100000001490116 - 0.0500000007450581)));
    }

    public static void AddExplosion(Vector2 spot)
    {
      MoonlordDeathDrama._explosions.Add(new MoonlordDeathDrama.MoonlordExplosion(TextureManager.Load("Images/Misc/MoonExplosion/Explosion"), spot, Main.rand.Next(2, 4)));
    }

    public static void RequestLight(float light, Vector2 spot)
    {
      MoonlordDeathDrama._lightSources.Add(spot);
      if ((double) light > 1.0)
        light = 1f;
      if ((double) MoonlordDeathDrama.requestedLight >= (double) light)
        return;
      MoonlordDeathDrama.requestedLight = light;
    }

    public class MoonlordPiece
    {
      private Texture2D _texture;
      private Vector2 _position;
      private Vector2 _velocity;
      private Vector2 _origin;
      private float _rotation;
      private float _rotationVelocity;

      public bool Dead
      {
        get
        {
          if (this._position.Y <= (double) (Main.maxTilesY * 16) - 480.0 && this._position.X >= 480.0)
            return this._position.X >= (double) (Main.maxTilesX * 16) - 480.0;
          return true;
        }
      }

      public MoonlordPiece(Texture2D pieceTexture, Vector2 textureOrigin, Vector2 centerPos, Vector2 velocity, float rot, float angularVelocity)
      {
        this._texture = pieceTexture;
        this._origin = textureOrigin;
        this._position = centerPos;
        this._velocity = velocity;
        this._rotation = rot;
        this._rotationVelocity = angularVelocity;
      }

      public void Update()
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local = @this._velocity.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num = (double) ^(float&) local + 0.300000011920929;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local = (float) num;
        this._rotation = this._rotation + this._rotationVelocity;
        this._rotationVelocity = this._rotationVelocity * 0.99f;
        this._position = Vector2.op_Addition(this._position, this._velocity);
      }

      public void Draw(SpriteBatch sp)
      {
        Color light = this.GetLight();
        sp.Draw(this._texture, Vector2.op_Subtraction(this._position, Main.screenPosition), new Rectangle?(), light, this._rotation, this._origin, 1f, (SpriteEffects) 0, 0.0f);
      }

      public bool InDrawRange(Rectangle playerScreen)
      {
        // ISSUE: explicit reference operation
        return ((Rectangle) @playerScreen).Contains(this._position.ToPoint());
      }

      public Color GetLight()
      {
        Vector3 vector3_1 = Vector3.get_Zero();
        float num1 = 0.0f;
        int num2 = 5;
        Point tileCoordinates = this._position.ToTileCoordinates();
        for (int x = tileCoordinates.X - num2; x <= tileCoordinates.X + num2; ++x)
        {
          for (int y = tileCoordinates.Y - num2; y <= tileCoordinates.Y + num2; ++y)
          {
            Vector3 vector3_2 = vector3_1;
            Color color = Lighting.GetColor(x, y);
            // ISSUE: explicit reference operation
            Vector3 vector3_3 = ((Color) @color).ToVector3();
            vector3_1 = Vector3.op_Addition(vector3_2, vector3_3);
            ++num1;
          }
        }
        if ((double) num1 == 0.0)
          return Color.get_White();
        return new Color(Vector3.op_Division(vector3_1, num1));
      }
    }

    public class MoonlordExplosion
    {
      private Texture2D _texture;
      private Vector2 _position;
      private Vector2 _origin;
      private Rectangle _frame;
      private int _frameCounter;
      private int _frameSpeed;

      public bool Dead
      {
        get
        {
          if (this._position.Y <= (double) (Main.maxTilesY * 16) - 480.0 && this._position.X >= 480.0 && this._position.X < (double) (Main.maxTilesX * 16) - 480.0)
            return this._frameCounter >= this._frameSpeed * 7;
          return true;
        }
      }

      public MoonlordExplosion(Texture2D pieceTexture, Vector2 centerPos, int frameSpeed)
      {
        this._texture = pieceTexture;
        this._position = centerPos;
        this._frameSpeed = frameSpeed;
        this._frameCounter = 0;
        this._frame = this._texture.Frame(1, 7, 0, 0);
        this._origin = Vector2.op_Division(this._frame.Size(), 2f);
      }

      public void Update()
      {
        this._frameCounter = this._frameCounter + 1;
        this._frame = this._texture.Frame(1, 7, 0, this._frameCounter / this._frameSpeed);
      }

      public void Draw(SpriteBatch sp)
      {
        Color light = this.GetLight();
        sp.Draw(this._texture, Vector2.op_Subtraction(this._position, Main.screenPosition), new Rectangle?(this._frame), light, 0.0f, this._origin, 1f, (SpriteEffects) 0, 0.0f);
      }

      public bool InDrawRange(Rectangle playerScreen)
      {
        // ISSUE: explicit reference operation
        return ((Rectangle) @playerScreen).Contains(this._position.ToPoint());
      }

      public Color GetLight()
      {
        return new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue);
      }
    }
  }
}
