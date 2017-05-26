// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.WaterShaderData
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.DataStructures;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria.GameContent.Shaders
{
  public class WaterShaderData : ScreenShaderData
  {
    public bool _useViscosityFilter = true;
    private Vector2 _lastDistortionDrawOffset = Vector2.get_Zero();
    private WaterShaderData.Ripple[] _rippleQueue = new WaterShaderData.Ripple[200];
    public bool _useProjectileWaves = true;
    private bool _useNPCWaves = true;
    private bool _usePlayerWaves = true;
    private bool _useRippleWaves = true;
    private bool _useCustomWaves = true;
    private bool _clearNextFrame = true;
    private Texture2D[] _viscosityMaskChain = new Texture2D[3];
    private bool _isWaveBufferDirty = true;
    private const float DISTORTION_BUFFER_SCALE = 0.25f;
    private const float WAVE_FRAMERATE = 0.01666667f;
    private const int MAX_RIPPLES_QUEUED = 200;
    private RenderTarget2D _distortionTarget;
    private RenderTarget2D _distortionTargetSwap;
    private bool _usingRenderTargets;
    private float _progress;
    private int _rippleQueueCount;
    private int _lastScreenWidth;
    private int _lastScreenHeight;
    private int _activeViscosityMask;
    private Texture2D _rippleShapeTexture;
    private int _queuedSteps;
    private const int MAX_QUEUED_STEPS = 2;

    public event Action<TileBatch> OnWaveDraw;

    public WaterShaderData(string passName)
      : base(passName)
    {
      Main.OnRenderTargetsInitialized += new ResolutionChangeEvent(this.InitRenderTargets);
      Main.OnRenderTargetsReleased += new Action(this.ReleaseRenderTargets);
      this._rippleShapeTexture = Main.instance.OurLoad<Texture2D>("Images/Misc/Ripples");
      Main.OnPreDraw += new Action<GameTime>(this.PreDraw);
    }

    public override void Update(GameTime gameTime)
    {
      this._useViscosityFilter = Main.WaveQuality >= 3;
      this._useProjectileWaves = Main.WaveQuality >= 3;
      this._usePlayerWaves = Main.WaveQuality >= 2;
      this._useRippleWaves = Main.WaveQuality >= 2;
      this._useCustomWaves = Main.WaveQuality >= 2;
      if (Main.gamePaused || !Main.hasFocus)
        return;
      this._progress = this._progress + (float) (gameTime.get_ElapsedGameTime().TotalSeconds * (double) this.Intensity * 0.75);
      this._progress = this._progress % 86400f;
      if (this._useProjectileWaves || this._useRippleWaves || (this._useCustomWaves || this._usePlayerWaves))
        this._queuedSteps = this._queuedSteps + 1;
      base.Update(gameTime);
    }

    private void StepLiquids()
    {
      this._isWaveBufferDirty = true;
      Vector2 vector2_1 = Main.drawToScreen ? Vector2.get_Zero() : new Vector2((float) Main.offScreenRange, (float) Main.offScreenRange);
      Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, Main.screenPosition);
      TileBatch tileBatch = Main.tileBatch;
      GraphicsDevice graphicsDevice = Main.instance.get_GraphicsDevice();
      graphicsDevice.SetRenderTarget(this._distortionTarget);
      if (this._clearNextFrame)
      {
        graphicsDevice.Clear(new Color(0.5f, 0.5f, 0.0f, 1f));
        this._clearNextFrame = false;
      }
      this.DrawWaves();
      graphicsDevice.SetRenderTarget(this._distortionTargetSwap);
      graphicsDevice.Clear(new Color(0.5f, 0.5f, 0.5f, 1f));
      Main.tileBatch.Begin();
      Vector2 vector2_3 = Vector2.op_Multiply(vector2_2, 0.25f);
      vector2_3.X = (__Null) Math.Floor((double) vector2_3.X);
      vector2_3.Y = (__Null) Math.Floor((double) vector2_3.Y);
      Vector2 vector2_4 = Vector2.op_Subtraction(vector2_3, this._lastDistortionDrawOffset);
      this._lastDistortionDrawOffset = vector2_3;
      tileBatch.Draw((Texture2D) this._distortionTarget, new Vector4((float) vector2_4.X, (float) vector2_4.Y, (float) ((Texture2D) this._distortionTarget).get_Width(), (float) ((Texture2D) this._distortionTarget).get_Height()), new VertexColors(Color.get_White()));
      GameShaders.Misc["WaterProcessor"].Apply(new DrawData?(new DrawData((Texture2D) this._distortionTarget, Vector2.get_Zero(), Color.get_White())));
      tileBatch.End();
      RenderTarget2D distortionTarget = this._distortionTarget;
      this._distortionTarget = this._distortionTargetSwap;
      this._distortionTargetSwap = distortionTarget;
      if (this._useViscosityFilter)
      {
        LiquidRenderer.Instance.SetWaveMaskData(ref this._viscosityMaskChain[this._activeViscosityMask]);
        tileBatch.Begin();
        Rectangle cachedDrawArea = LiquidRenderer.Instance.GetCachedDrawArea();
        Rectangle rectangle;
        // ISSUE: explicit reference operation
        ((Rectangle) @rectangle).\u002Ector(0, 0, (int) cachedDrawArea.Height, (int) cachedDrawArea.Width);
        Vector4 vector4_1;
        // ISSUE: explicit reference operation
        ((Vector4) @vector4_1).\u002Ector((float) (cachedDrawArea.X + cachedDrawArea.Width), (float) cachedDrawArea.Y, (float) cachedDrawArea.Height, (float) cachedDrawArea.Width);
        Vector4 vector4_2 = Vector4.op_Multiply(vector4_1, 16f);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local1 = @vector4_2.X;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num1 = (double) ^(float&) local1 - vector2_1.X;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local1 = (float) num1;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local2 = @vector4_2.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num2 = (double) ^(float&) local2 - vector2_1.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local2 = (float) num2;
        Vector4 destination = Vector4.op_Multiply(vector4_2, 0.25f);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local3 = @destination.X;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num3 = (double) ^(float&) local3 + vector2_3.X;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local3 = (float) num3;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local4 = @destination.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num4 = (double) ^(float&) local4 + vector2_3.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local4 = (float) num4;
        graphicsDevice.get_SamplerStates().set_Item(0, (SamplerState) SamplerState.PointClamp);
        tileBatch.Draw(this._viscosityMaskChain[this._activeViscosityMask], destination, new Rectangle?(rectangle), new VertexColors(Color.get_White()), Vector2.get_Zero(), (SpriteEffects) 1, 1.570796f);
        tileBatch.End();
        this._activeViscosityMask = this._activeViscosityMask + 1;
        this._activeViscosityMask = this._activeViscosityMask % this._viscosityMaskChain.Length;
      }
      graphicsDevice.SetRenderTarget((RenderTarget2D) null);
    }

    private void DrawWaves()
    {
      Vector2 screenPosition = Main.screenPosition;
      Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Division(Vector2.op_UnaryNegation(this._lastDistortionDrawOffset), 0.25f), Main.drawToScreen ? Vector2.get_Zero() : new Vector2((float) Main.offScreenRange, (float) Main.offScreenRange));
      TileBatch tileBatch = Main.tileBatch;
      Main.instance.get_GraphicsDevice();
      Vector2 dimensions1;
      // ISSUE: explicit reference operation
      ((Vector2) @dimensions1).\u002Ector((float) Main.screenWidth, (float) Main.screenHeight);
      Vector2 vector2_2;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_2).\u002Ector(16f, 16f);
      tileBatch.Begin();
      GameShaders.Misc["WaterDistortionObject"].Apply(new DrawData?());
      if (this._useNPCWaves)
      {
        for (int index = 0; index < 200; ++index)
        {
          if (Main.npc[index] != null && Main.npc[index].active && (Main.npc[index].wet || (int) Main.npc[index].wetCount != 0) && Collision.CheckAABBvAABBCollision(screenPosition, dimensions1, Vector2.op_Subtraction(Main.npc[index].position, vector2_2), Vector2.op_Addition(Main.npc[index].Size, vector2_2)))
          {
            NPC npc = Main.npc[index];
            Vector2 vector2_3 = Vector2.op_Subtraction(npc.Center, vector2_1);
            Vector2 vector2_4 = Vector2.op_Division(npc.velocity.RotatedBy(-(double) npc.rotation, (Vector2) null), new Vector2((float) npc.height, (float) npc.width));
            // ISSUE: explicit reference operation
            float num1 = ((Vector2) @vector2_4).LengthSquared();
            double num2 = (double) Math.Min((float) ((double) num1 * 0.300000011920929 + 0.699999988079071 * (double) num1 * (1024.0 / (double) (npc.height * npc.width))), 0.08f);
            Vector2 vector2_5 = Vector2.op_Subtraction(npc.velocity, npc.oldVelocity);
            // ISSUE: explicit reference operation
            double num3 = (double) ((Vector2) @vector2_5).Length() * 0.5;
            float num4 = (float) (num2 + num3);
            // ISSUE: explicit reference operation
            ((Vector2) @vector2_4).Normalize();
            Vector2 velocity = npc.velocity;
            // ISSUE: explicit reference operation
            ((Vector2) @velocity).Normalize();
            Vector2 vector2_6 = Vector2.op_Subtraction(vector2_3, Vector2.op_Multiply(velocity, 10f));
            if (!this._useViscosityFilter && (npc.honeyWet || npc.lavaWet))
              num4 *= 0.3f;
            if (npc.wet)
              tileBatch.Draw(Main.magicPixel, Vector4.op_Multiply(new Vector4((float) vector2_6.X, (float) vector2_6.Y, (float) npc.width * 2f, (float) npc.height * 2f), 0.25f), new Rectangle?(), new VertexColors(new Color((float) (vector2_4.X * 0.5 + 0.5), (float) (vector2_4.Y * 0.5 + 0.5), 0.5f * num4)), new Vector2((float) Main.magicPixel.get_Width() / 2f, (float) Main.magicPixel.get_Height() / 2f), (SpriteEffects) 0, npc.rotation);
            if ((int) npc.wetCount != 0)
            {
              // ISSUE: explicit reference operation
              float num5 = 0.195f * (float) Math.Sqrt((double) ((Vector2) @npc.velocity).Length());
              float num6 = 5f;
              if (!npc.wet)
                num6 = -20f;
              this.QueueRipple(Vector2.op_Addition(npc.Center, Vector2.op_Multiply(velocity, num6)), Color.op_Multiply(new Color(0.5f, (float) ((npc.wet ? (double) num5 : -(double) num5) * 0.5 + 0.5), 0.0f, 1f), 0.5f), Vector2.op_Multiply(new Vector2((float) npc.width, (float) npc.height * ((float) npc.wetCount / 9f)), MathHelper.Clamp(num5 * 10f, 0.0f, 1f)), RippleShape.Circle, 0.0f);
            }
          }
        }
      }
      if (this._usePlayerWaves)
      {
        for (int index = 0; index < (int) byte.MaxValue; ++index)
        {
          if (Main.player[index] != null && Main.player[index].active && (Main.player[index].wet || (int) Main.player[index].wetCount != 0) && Collision.CheckAABBvAABBCollision(screenPosition, dimensions1, Vector2.op_Subtraction(Main.player[index].position, vector2_2), Vector2.op_Addition(Main.player[index].Size, vector2_2)))
          {
            Player player = Main.player[index];
            Vector2 vector2_3 = Vector2.op_Subtraction(player.Center, vector2_1);
            // ISSUE: explicit reference operation
            float num1 = 0.05f * (float) Math.Sqrt((double) ((Vector2) @player.velocity).Length());
            Vector2 velocity = player.velocity;
            // ISSUE: explicit reference operation
            ((Vector2) @velocity).Normalize();
            Vector2 vector2_4 = Vector2.op_Subtraction(vector2_3, Vector2.op_Multiply(velocity, 10f));
            if (!this._useViscosityFilter && (player.honeyWet || player.lavaWet))
              num1 *= 0.3f;
            if (player.wet)
              tileBatch.Draw(Main.magicPixel, Vector4.op_Multiply(new Vector4((float) (vector2_4.X - (double) player.width * 2.0 * 0.5), (float) (vector2_4.Y - (double) player.height * 2.0 * 0.5), (float) player.width * 2f, (float) player.height * 2f), 0.25f), new VertexColors(new Color((float) (velocity.X * 0.5 + 0.5), (float) (velocity.Y * 0.5 + 0.5), 0.5f * num1)));
            if ((int) player.wetCount != 0)
            {
              float num2 = 5f;
              if (!player.wet)
                num2 = -20f;
              float num3 = num1 * 3f;
              this.QueueRipple(Vector2.op_Addition(player.Center, Vector2.op_Multiply(velocity, num2)), player.wet ? num3 : -num3, Vector2.op_Multiply(new Vector2((float) player.width, (float) player.height * ((float) player.wetCount / 9f)), MathHelper.Clamp(num3 * 10f, 0.0f, 1f)), RippleShape.Circle, 0.0f);
            }
          }
        }
      }
      if (this._useProjectileWaves)
      {
        for (int index = 0; index < 1000; ++index)
        {
          Projectile projectile = Main.projectile[index];
          int num1 = !projectile.wet || projectile.lavaWet ? 0 : (!projectile.honeyWet ? 1 : 0);
          bool flag1 = projectile.lavaWet;
          bool flag2 = projectile.honeyWet;
          bool flag3 = projectile.wet;
          if (projectile.ignoreWater)
            flag3 = true;
          if (((projectile == null || !projectile.active ? 0 : (ProjectileID.Sets.CanDistortWater[projectile.type] ? 1 : 0)) & (flag3 ? 1 : 0)) != 0 && !ProjectileID.Sets.NoLiquidDistortion[projectile.type] && Collision.CheckAABBvAABBCollision(screenPosition, dimensions1, Vector2.op_Subtraction(projectile.position, vector2_2), Vector2.op_Addition(projectile.Size, vector2_2)))
          {
            if (projectile.ignoreWater)
            {
              int num2 = Collision.LavaCollision(projectile.position, projectile.width, projectile.height) ? 1 : 0;
              flag1 = Collision.WetCollision(projectile.position, projectile.width, projectile.height);
              flag2 = Collision.honey;
              int num3 = flag1 ? 1 : 0;
              if ((num2 | num3 | (flag2 ? 1 : 0)) == 0)
                continue;
            }
            Vector2 vector2_3 = Vector2.op_Subtraction(projectile.Center, vector2_1);
            // ISSUE: explicit reference operation
            float num4 = 2f * (float) Math.Sqrt(0.0500000007450581 * (double) ((Vector2) @projectile.velocity).Length());
            Vector2 velocity = projectile.velocity;
            // ISSUE: explicit reference operation
            ((Vector2) @velocity).Normalize();
            if (!this._useViscosityFilter && flag2 | flag1)
              num4 *= 0.3f;
            float num5 = Math.Max(12f, (float) projectile.width * 0.75f);
            float num6 = Math.Max(12f, (float) projectile.height * 0.75f);
            tileBatch.Draw(Main.magicPixel, Vector4.op_Multiply(new Vector4((float) (vector2_3.X - (double) num5 * 0.5), (float) (vector2_3.Y - (double) num6 * 0.5), num5, num6), 0.25f), new VertexColors(new Color((float) (velocity.X * 0.5 + 0.5), (float) (velocity.Y * 0.5 + 0.5), num4 * 0.5f)));
          }
        }
      }
      tileBatch.End();
      if (this._useRippleWaves)
      {
        tileBatch.Begin();
        for (int index = 0; index < this._rippleQueueCount; ++index)
        {
          Vector2 vector2_3 = Vector2.op_Subtraction(this._rippleQueue[index].Position, vector2_1);
          Vector2 size = this._rippleQueue[index].Size;
          Rectangle sourceRectangle = this._rippleQueue[index].SourceRectangle;
          Texture2D rippleShapeTexture = this._rippleShapeTexture;
          tileBatch.Draw(rippleShapeTexture, Vector4.op_Multiply(new Vector4((float) vector2_3.X, (float) vector2_3.Y, (float) size.X, (float) size.Y), 0.25f), new Rectangle?(sourceRectangle), new VertexColors(this._rippleQueue[index].WaveData), new Vector2((float) (sourceRectangle.Width / 2), (float) (sourceRectangle.Height / 2)), (SpriteEffects) 0, this._rippleQueue[index].Rotation);
        }
        tileBatch.End();
      }
      this._rippleQueueCount = 0;
      // ISSUE: reference to a compiler-generated field
      if (!this._useCustomWaves || this.OnWaveDraw == null)
        return;
      tileBatch.Begin();
      // ISSUE: reference to a compiler-generated field
      this.OnWaveDraw(tileBatch);
      tileBatch.End();
    }

    private void PreDraw(GameTime gameTime)
    {
      this.ValidateRenderTargets();
      if (!this._usingRenderTargets || !Main.IsGraphicsDeviceAvailable)
        return;
      if (this._useProjectileWaves || this._useRippleWaves || (this._useCustomWaves || this._usePlayerWaves))
      {
        for (int index = 0; index < Math.Min(this._queuedSteps, 2); ++index)
          this.StepLiquids();
      }
      else if (this._isWaveBufferDirty || this._clearNextFrame)
      {
        GraphicsDevice graphicsDevice = Main.instance.get_GraphicsDevice();
        RenderTarget2D distortionTarget = this._distortionTarget;
        graphicsDevice.SetRenderTarget(distortionTarget);
        Color color = new Color(0.5f, 0.5f, 0.0f, 1f);
        graphicsDevice.Clear(color);
        this._clearNextFrame = false;
        this._isWaveBufferDirty = false;
        // ISSUE: variable of the null type
        __Null local = null;
        graphicsDevice.SetRenderTarget((RenderTarget2D) local);
      }
      this._queuedSteps = 0;
    }

    public override void Apply()
    {
      if (!this._usingRenderTargets || !Main.IsGraphicsDeviceAvailable)
        return;
      this.UseProgress(this._progress);
      Main.graphics.get_GraphicsDevice().get_SamplerStates().set_Item(0, (SamplerState) SamplerState.PointClamp);
      Vector2 vector2_1 = Vector2.op_Multiply(Vector2.op_Multiply(new Vector2((float) Main.screenWidth, (float) Main.screenHeight), 0.5f), Vector2.op_Subtraction(Vector2.get_One(), Vector2.op_Division(Vector2.get_One(), Main.GameViewMatrix.Zoom)));
      Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Subtraction(Main.drawToScreen ? Vector2.get_Zero() : new Vector2((float) Main.offScreenRange, (float) Main.offScreenRange), Main.screenPosition), vector2_1);
      this.UseImage((Texture2D) this._distortionTarget, 1, (SamplerState) null);
      this.UseImage((Texture2D) Main.waterTarget, 2, (SamplerState) SamplerState.PointClamp);
      this.UseTargetPosition(Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(Main.screenPosition, Main.sceneWaterPos), new Vector2((float) Main.offScreenRange, (float) Main.offScreenRange)), vector2_1));
      this.UseImageOffset(Vector2.op_Division(Vector2.op_UnaryNegation(Vector2.op_Subtraction(Vector2.op_Multiply(vector2_2, 0.25f), this._lastDistortionDrawOffset)), new Vector2((float) ((Texture2D) this._distortionTarget).get_Width(), (float) ((Texture2D) this._distortionTarget).get_Height())));
      base.Apply();
    }

    private void ValidateRenderTargets()
    {
      int backBufferWidth = Main.instance.get_GraphicsDevice().get_PresentationParameters().get_BackBufferWidth();
      int backBufferHeight = Main.instance.get_GraphicsDevice().get_PresentationParameters().get_BackBufferHeight();
      bool flag = !Main.drawToScreen;
      if (this._usingRenderTargets && !flag)
        this.ReleaseRenderTargets();
      else if (!this._usingRenderTargets & flag)
      {
        this.InitRenderTargets(backBufferWidth, backBufferHeight);
      }
      else
      {
        if (!(this._usingRenderTargets & flag) || !this._distortionTarget.get_IsContentLost() && !this._distortionTargetSwap.get_IsContentLost())
          return;
        this._clearNextFrame = true;
      }
    }

    private void InitRenderTargets(int width, int height)
    {
      this._lastScreenWidth = width;
      this._lastScreenHeight = height;
      width = (int) ((double) width * 0.25);
      height = (int) ((double) height * 0.25);
      try
      {
        this._distortionTarget = new RenderTarget2D(Main.instance.get_GraphicsDevice(), width, height, false, (SurfaceFormat) 0, (DepthFormat) 0, 0, (RenderTargetUsage) 1);
        this._distortionTargetSwap = new RenderTarget2D(Main.instance.get_GraphicsDevice(), width, height, false, (SurfaceFormat) 0, (DepthFormat) 0, 0, (RenderTargetUsage) 1);
        this._usingRenderTargets = true;
        this._clearNextFrame = true;
      }
      catch (Exception ex)
      {
        Lighting.lightMode = 2;
        this._usingRenderTargets = false;
        Console.WriteLine("Failed to create water distortion render targets. " + ex.ToString());
      }
    }

    private void ReleaseRenderTargets()
    {
      try
      {
        if (this._distortionTarget != null)
          ((GraphicsResource) this._distortionTarget).Dispose();
        if (this._distortionTargetSwap != null)
          ((GraphicsResource) this._distortionTargetSwap).Dispose();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error disposing of water distortion render targets. " + ex.ToString());
      }
      this._distortionTarget = (RenderTarget2D) null;
      this._distortionTargetSwap = (RenderTarget2D) null;
      this._usingRenderTargets = false;
    }

    public void QueueRipple(Vector2 position, float strength = 1f, RippleShape shape = RippleShape.Square, float rotation = 0.0f)
    {
      float num1 = (float) ((double) strength * 0.5 + 0.5);
      float num2 = Math.Min(Math.Abs(strength), 1f);
      this.QueueRipple(position, Color.op_Multiply(new Color(0.5f, num1, 0.0f, 1f), num2), new Vector2(4f * Math.Max(Math.Abs(strength), 1f)), shape, rotation);
    }

    public void QueueRipple(Vector2 position, float strength, Vector2 size, RippleShape shape = RippleShape.Square, float rotation = 0.0f)
    {
      float num1 = (float) ((double) strength * 0.5 + 0.5);
      float num2 = Math.Min(Math.Abs(strength), 1f);
      this.QueueRipple(position, Color.op_Multiply(new Color(0.5f, num1, 0.0f, 1f), num2), size, shape, rotation);
    }

    public void QueueRipple(Vector2 position, Color waveData, Vector2 size, RippleShape shape = RippleShape.Square, float rotation = 0.0f)
    {
      if (!this._useRippleWaves || Main.drawToScreen)
      {
        this._rippleQueueCount = 0;
      }
      else
      {
        if (this._rippleQueueCount >= this._rippleQueue.Length)
          return;
        WaterShaderData.Ripple[] rippleQueue = this._rippleQueue;
        int rippleQueueCount = this._rippleQueueCount;
        this._rippleQueueCount = rippleQueueCount + 1;
        int index = rippleQueueCount;
        WaterShaderData.Ripple ripple = new WaterShaderData.Ripple(position, waveData, size, shape, rotation);
        rippleQueue[index] = ripple;
      }
    }

    private struct Ripple
    {
      private static readonly Rectangle[] RIPPLE_SHAPE_SOURCE_RECTS = new Rectangle[3]
      {
        new Rectangle(0, 0, 0, 0),
        new Rectangle(1, 1, 62, 62),
        new Rectangle(1, 65, 62, 62)
      };
      public readonly Vector2 Position;
      public readonly Color WaveData;
      public readonly Vector2 Size;
      public readonly RippleShape Shape;
      public readonly float Rotation;

      public Rectangle SourceRectangle
      {
        get
        {
          return WaterShaderData.Ripple.RIPPLE_SHAPE_SOURCE_RECTS[(int) this.Shape];
        }
      }

      public Ripple(Vector2 position, Color waveData, Vector2 size, RippleShape shape, float rotation)
      {
        this.Position = position;
        this.WaveData = waveData;
        this.Size = size;
        this.Shape = shape;
        this.Rotation = rotation;
      }
    }
  }
}
