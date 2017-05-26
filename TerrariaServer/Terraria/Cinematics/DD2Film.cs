// Decompiled with JetBrains decompiler
// Type: Terraria.Cinematics.DD2Film
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.GameContent.UI;
using Terraria.ID;

namespace Terraria.Cinematics
{
  public class DD2Film : Film
  {
    private List<NPC> _army = new List<NPC>();
    private List<NPC> _critters = new List<NPC>();
    private NPC _dryad;
    private NPC _ogre;
    private NPC _portal;
    private Vector2 _startPoint;

    public DD2Film()
    {
      this.AppendKeyFrames(new FrameEvent(this.CreateDryad), new FrameEvent(this.CreateCritters));
      this.AppendSequences(120, new FrameEvent(this.DryadStand), new FrameEvent(this.DryadLookRight));
      this.AppendSequences(100, new FrameEvent(this.DryadLookRight), new FrameEvent(this.DryadInteract));
      this.AddKeyFrame(this.AppendPoint - 20, new FrameEvent(this.CreatePortal));
      this.AppendSequences(30, new FrameEvent(this.DryadLookLeft), new FrameEvent(this.DryadStand));
      this.AppendSequences(40, new FrameEvent(this.DryadConfusedEmote), new FrameEvent(this.DryadStand), new FrameEvent(this.DryadLookLeft));
      this.AppendKeyFrame(new FrameEvent(this.CreateOgre));
      this.AddKeyFrame(this.AppendPoint + 60, new FrameEvent(this.SpawnJavalinThrower));
      this.AddKeyFrame(this.AppendPoint + 120, new FrameEvent(this.SpawnGoblin));
      this.AddKeyFrame(this.AppendPoint + 180, new FrameEvent(this.SpawnGoblin));
      this.AddKeyFrame(this.AppendPoint + 240, new FrameEvent(this.SpawnWitherBeast));
      this.AppendSequences(30, new FrameEvent(this.DryadStand), new FrameEvent(this.DryadLookLeft));
      this.AppendSequences(30, new FrameEvent(this.DryadLookRight), new FrameEvent(this.DryadWalk));
      this.AppendSequences(300, new FrameEvent(this.DryadAttack), new FrameEvent(this.DryadLookLeft));
      this.AppendKeyFrame(new FrameEvent(this.RemoveEnemyDamage));
      this.AppendSequences(60, new FrameEvent(this.DryadLookRight), new FrameEvent(this.DryadStand), new FrameEvent(this.DryadAlertEmote));
      this.AddSequences(this.AppendPoint - 90, 60, new FrameEvent(this.OgreLookLeft), new FrameEvent(this.OgreStand));
      this.AddKeyFrame(this.AppendPoint - 12, new FrameEvent(this.OgreSwingSound));
      this.AddSequences(this.AppendPoint - 30, 50, new FrameEvent(this.DryadPortalKnock), new FrameEvent(this.DryadStand));
      this.AppendKeyFrame(new FrameEvent(this.RestoreEnemyDamage));
      this.AppendSequences(40, new FrameEvent(this.DryadPortalFade), new FrameEvent(this.DryadStand));
      this.AppendSequence(180, new FrameEvent(this.DryadStand));
      this.AddSequence(0, this.AppendPoint, new FrameEvent(this.PerFrameSettings));
    }

    private void PerFrameSettings(FrameEventData evt)
    {
      CombatText.clearAll();
    }

    private void CreateDryad(FrameEventData evt)
    {
      this._dryad = this.PlaceNPCOnGround(20, this._startPoint);
      this._dryad.knockBackResist = 0.0f;
      this._dryad.immortal = true;
      this._dryad.dontTakeDamage = true;
      this._dryad.takenDamageMultiplier = 0.0f;
      this._dryad.immune[(int) byte.MaxValue] = 100000;
    }

    private void DryadInteract(FrameEventData evt)
    {
      if (this._dryad == null)
        return;
      this._dryad.ai[0] = 9f;
      if (evt.IsFirstFrame)
        this._dryad.ai[1] = (float) evt.Duration;
      this._dryad.localAI[0] = 0.0f;
    }

    private void SpawnWitherBeast(FrameEventData evt)
    {
      int index = NPC.NewNPC((int) this._portal.Center.X, (int) this._portal.Bottom.Y, 568, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
      NPC npc = Main.npc[index];
      npc.knockBackResist = 0.0f;
      npc.immortal = true;
      npc.dontTakeDamage = true;
      npc.takenDamageMultiplier = 0.0f;
      npc.immune[(int) byte.MaxValue] = 100000;
      npc.friendly = this._ogre.friendly;
      this._army.Add(npc);
    }

    private void SpawnJavalinThrower(FrameEventData evt)
    {
      int index = NPC.NewNPC((int) this._portal.Center.X, (int) this._portal.Bottom.Y, 561, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
      NPC npc = Main.npc[index];
      npc.knockBackResist = 0.0f;
      npc.immortal = true;
      npc.dontTakeDamage = true;
      npc.takenDamageMultiplier = 0.0f;
      npc.immune[(int) byte.MaxValue] = 100000;
      npc.friendly = this._ogre.friendly;
      this._army.Add(npc);
    }

    private void SpawnGoblin(FrameEventData evt)
    {
      int index = NPC.NewNPC((int) this._portal.Center.X, (int) this._portal.Bottom.Y, 552, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
      NPC npc = Main.npc[index];
      npc.knockBackResist = 0.0f;
      npc.immortal = true;
      npc.dontTakeDamage = true;
      npc.takenDamageMultiplier = 0.0f;
      npc.immune[(int) byte.MaxValue] = 100000;
      npc.friendly = this._ogre.friendly;
      this._army.Add(npc);
    }

    private void CreateCritters(FrameEventData evt)
    {
      for (int index = 0; index < 5; ++index)
      {
        float num = (float) index / 5f;
        NPC npc = this.PlaceNPCOnGround((int) Utils.SelectRandom<short>(Main.rand, new short[4]
        {
          (short) 46,
          (short) 46,
          (short) 299,
          (short) 538
        }), Vector2.op_Addition(this._startPoint, new Vector2((float) (((double) num - 0.25) * 400.0 + (double) Main.rand.NextFloat() * 50.0 - 25.0), 0.0f)));
        npc.ai[0] = 0.0f;
        npc.ai[1] = 600f;
        this._critters.Add(npc);
      }
      if (this._dryad == null)
        return;
      for (int index1 = 0; index1 < 10; ++index1)
      {
        double num = (double) index1 / 10.0;
        int index2 = NPC.NewNPC((int) this._dryad.position.X + Main.rand.Next(-1000, 800), (int) this._dryad.position.Y - Main.rand.Next(-50, 300), 356, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
        NPC npc = Main.npc[index2];
        npc.ai[0] = (float) ((double) Main.rand.NextFloat() * 4.0 - 2.0);
        npc.ai[1] = (float) ((double) Main.rand.NextFloat() * 4.0 - 2.0);
        npc.velocity.X = (__Null) ((double) Main.rand.NextFloat() * 4.0 - 2.0);
        this._critters.Add(npc);
      }
    }

    private void OgreSwingSound(FrameEventData evt)
    {
      Main.PlaySound(SoundID.DD2_OgreAttack, this._ogre.Center);
    }

    private void DryadPortalKnock(FrameEventData evt)
    {
      if (this._dryad != null)
      {
        if (evt.Frame == 20)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local1 = @this._dryad.velocity.Y;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          double num1 = (double) ^(float&) local1 - 7.0;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) local1 = (float) num1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          __Null& local2 = @this._dryad.velocity.X;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          double num2 = (double) ^(float&) local2 - 8.0;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) local2 = (float) num2;
          Main.PlaySound(3, (int) this._dryad.Center.X, (int) this._dryad.Center.Y, 1, 1f, 0.0f);
        }
        if (evt.Frame >= 20)
        {
          this._dryad.ai[0] = 1f;
          this._dryad.ai[1] = (float) evt.Remaining;
          this._dryad.rotation += 0.05f;
        }
      }
      if (this._ogre == null)
        return;
      if (evt.Frame > 40)
      {
        this._ogre.target = Main.myPlayer;
        this._ogre.direction = 1;
      }
      else
      {
        this._ogre.direction = -1;
        this._ogre.ai[1] = 0.0f;
        this._ogre.ai[0] = Math.Min(40f, this._ogre.ai[0]);
        this._ogre.target = 300 + this._dryad.whoAmI;
      }
    }

    private void RemoveEnemyDamage(FrameEventData evt)
    {
      this._ogre.friendly = true;
      foreach (NPC npc in this._army)
        npc.friendly = true;
    }

    private void RestoreEnemyDamage(FrameEventData evt)
    {
      this._ogre.friendly = false;
      foreach (NPC npc in this._army)
        npc.friendly = false;
    }

    private void DryadPortalFade(FrameEventData evt)
    {
      if (this._dryad == null || this._portal == null)
        return;
      if (evt.IsFirstFrame)
        Main.PlaySound(SoundID.DD2_EtherianPortalDryadTouch, this._dryad.Center);
      float num1 = Math.Max(0.0f, (float) (evt.Frame - 7) / (float) (evt.Duration - 7));
      this._dryad.color = new Color(Vector3.Lerp(Vector3.get_One(), new Vector3(0.5f, 0.0f, 0.8f), num1));
      this._dryad.Opacity = 1f - num1;
      this._dryad.rotation += (float) (0.0500000007450581 * ((double) num1 * 4.0 + 1.0));
      this._dryad.scale = 1f - num1;
      if (this._dryad.position.X < this._portal.Right.X)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local1 = @this._dryad.velocity.X;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num2 = (double) ^(float&) local1 * 0.949999988079071;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local1 = (float) num2;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local2 = @this._dryad.velocity.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num3 = (double) ^(float&) local2 * 0.550000011920929;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local2 = (float) num3;
      }
      int num4 = (int) (6.0 * (double) num1);
      Vector2 size = this._dryad.Size;
      // ISSUE: explicit reference operation
      float num5 = ((Vector2) @size).Length() / 2f / 20f;
      for (int index = 0; index < num4; ++index)
      {
        if (Main.rand.Next(5) == 0)
        {
          Dust dust = Dust.NewDustDirect(this._dryad.position, this._dryad.width, this._dryad.height, 27, (float) (this._dryad.velocity.X * 1.0), 0.0f, 100, (Color) null, 1f);
          double num2 = 0.550000011920929;
          dust.scale = (float) num2;
          double num3 = 0.699999988079071;
          dust.fadeIn = (float) num3;
          Vector2 vector2_1 = Vector2.op_Multiply(dust.velocity, 0.1f * num5);
          dust.velocity = vector2_1;
          Vector2 vector2_2 = Vector2.op_Addition(dust.velocity, this._dryad.velocity);
          dust.velocity = vector2_2;
        }
      }
    }

    private void CreatePortal(FrameEventData evt)
    {
      this._portal = this.PlaceNPCOnGround(549, Vector2.op_Addition(this._startPoint, new Vector2(-240f, 0.0f)));
      this._portal.immortal = true;
    }

    private void DryadStand(FrameEventData evt)
    {
      if (this._dryad == null)
        return;
      this._dryad.ai[0] = 0.0f;
      this._dryad.ai[1] = (float) evt.Remaining;
    }

    private void DryadLookRight(FrameEventData evt)
    {
      if (this._dryad == null)
        return;
      this._dryad.direction = 1;
      this._dryad.spriteDirection = 1;
    }

    private void DryadLookLeft(FrameEventData evt)
    {
      if (this._dryad == null)
        return;
      this._dryad.direction = -1;
      this._dryad.spriteDirection = -1;
    }

    private void DryadWalk(FrameEventData evt)
    {
      this._dryad.ai[0] = 1f;
      this._dryad.ai[1] = 2f;
    }

    private void DryadConfusedEmote(FrameEventData evt)
    {
      if (this._dryad == null || !evt.IsFirstFrame)
        return;
      EmoteBubble.NewBubble(87, new WorldUIAnchor((Entity) this._dryad), evt.Duration);
    }

    private void DryadAlertEmote(FrameEventData evt)
    {
      if (this._dryad == null || !evt.IsFirstFrame)
        return;
      EmoteBubble.NewBubble(3, new WorldUIAnchor((Entity) this._dryad), evt.Duration);
    }

    private void CreateOgre(FrameEventData evt)
    {
      int index = NPC.NewNPC((int) this._portal.Center.X, (int) this._portal.Bottom.Y, 576, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
      this._ogre = Main.npc[index];
      this._ogre.knockBackResist = 0.0f;
      this._ogre.immortal = true;
      this._ogre.dontTakeDamage = true;
      this._ogre.takenDamageMultiplier = 0.0f;
      this._ogre.immune[(int) byte.MaxValue] = 100000;
    }

    private void OgreStand(FrameEventData evt)
    {
      if (this._ogre == null)
        return;
      this._ogre.ai[0] = 0.0f;
      this._ogre.ai[1] = 0.0f;
      this._ogre.velocity = Vector2.get_Zero();
    }

    private void DryadAttack(FrameEventData evt)
    {
      if (this._dryad == null)
        return;
      this._dryad.ai[0] = 14f;
      this._dryad.ai[1] = (float) evt.Remaining;
      this._dryad.dryadWard = false;
    }

    private void OgreLookRight(FrameEventData evt)
    {
      if (this._ogre == null)
        return;
      this._ogre.direction = 1;
      this._ogre.spriteDirection = 1;
    }

    private void OgreLookLeft(FrameEventData evt)
    {
      if (this._ogre == null)
        return;
      this._ogre.direction = -1;
      this._ogre.spriteDirection = -1;
    }

    public override void OnBegin()
    {
      Main.NewText("DD2Film: Begin", byte.MaxValue, byte.MaxValue, byte.MaxValue, false);
      Main.dayTime = true;
      Main.time = 27000.0;
      this._startPoint = Vector2.op_Addition(Main.screenPosition, new Vector2((float) Main.mouseX, (float) Main.mouseY - 32f));
      base.OnBegin();
    }

    private NPC PlaceNPCOnGround(int type, Vector2 position)
    {
      int x = (int) position.X;
      int y = (int) position.Y;
      int i = x / 16;
      int j = y / 16;
      while (!WorldGen.SolidTile(i, j))
        ++j;
      int Y = j * 16;
      int Start = 100;
      if (type == 20)
        Start = 1;
      else if (type == 576)
        Start = 50;
      int index = NPC.NewNPC(x, Y, type, Start, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
      return Main.npc[index];
    }

    public override void OnEnd()
    {
      if (this._dryad != null)
        this._dryad.active = false;
      if (this._portal != null)
        this._portal.active = false;
      if (this._ogre != null)
        this._ogre.active = false;
      foreach (Entity critter in this._critters)
        critter.active = false;
      foreach (Entity entity in this._army)
        entity.active = false;
      Main.NewText("DD2Film: End", byte.MaxValue, byte.MaxValue, byte.MaxValue, false);
      base.OnEnd();
    }
  }
}
