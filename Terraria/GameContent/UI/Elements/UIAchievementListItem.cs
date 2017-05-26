// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIAchievementListItem
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: H:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Achievements;
using Terraria.Graphics;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
  public class UIAchievementListItem : UIPanel
  {
    private Achievement _achievement;
    private UIImageFramed _achievementIcon;
    private UIImage _achievementIconBorders;
    private const int _iconSize = 64;
    private const int _iconSizeWithSpace = 66;
    private const int _iconsPerRow = 8;
    private int _iconIndex;
    private Rectangle _iconFrame;
    private Rectangle _iconFrameUnlocked;
    private Rectangle _iconFrameLocked;
    private Texture2D _innerPanelTopTexture;
    private Texture2D _innerPanelBottomTexture;
    private Texture2D _categoryTexture;
    private bool _locked;
    private bool _large;

    public UIAchievementListItem(Achievement achievement, bool largeForOtherLanguages)
    {
      this._large = largeForOtherLanguages;
      this.BackgroundColor = Color.op_Multiply(new Color(26, 40, 89), 0.8f);
      this.BorderColor = Color.op_Multiply(new Color(13, 20, 44), 0.8f);
      float num1 = (float) (16 + this._large.ToInt() * 20);
      float pixels1 = (float) (this._large.ToInt() * 6);
      float pixels2 = (float) (this._large.ToInt() * 12);
      this._achievement = achievement;
      this.Height.Set(66f + num1, 0.0f);
      this.Width.Set(0.0f, 1f);
      this.PaddingTop = 8f;
      this.PaddingLeft = 9f;
      int iconIndex = Main.Achievements.GetIconIndex(achievement.Name);
      this._iconIndex = iconIndex;
      this._iconFrameUnlocked = new Rectangle(iconIndex % 8 * 66, iconIndex / 8 * 66, 64, 64);
      this._iconFrameLocked = this._iconFrameUnlocked;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local = @this._iconFrameLocked.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      int num2 = ^(int&) local + 528;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) local = num2;
      this._iconFrame = this._iconFrameLocked;
      this.UpdateIconFrame();
      this._achievementIcon = new UIImageFramed(TextureManager.Load("Images/UI/Achievements"), this._iconFrame);
      this._achievementIcon.Left.Set(pixels1, 0.0f);
      this._achievementIcon.Top.Set(pixels2, 0.0f);
      this.Append((UIElement) this._achievementIcon);
      this._achievementIconBorders = new UIImage(TextureManager.Load("Images/UI/Achievement_Borders"));
      this._achievementIconBorders.Left.Set(pixels1 - 4f, 0.0f);
      this._achievementIconBorders.Top.Set(pixels2 - 4f, 0.0f);
      this.Append((UIElement) this._achievementIconBorders);
      this._innerPanelTopTexture = TextureManager.Load("Images/UI/Achievement_InnerPanelTop");
      this._innerPanelBottomTexture = !this._large ? TextureManager.Load("Images/UI/Achievement_InnerPanelBottom") : TextureManager.Load("Images/UI/Achievement_InnerPanelBottom_Large");
      this._categoryTexture = TextureManager.Load("Images/UI/Achievement_Categories");
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      int num1 = this._large.ToInt() * 6;
      Vector2 vector2_1;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).\u002Ector((float) num1, 0.0f);
      this._locked = !this._achievement.IsCompleted;
      this.UpdateIconFrame();
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      CalculatedStyle dimensions = this._achievementIconBorders.GetDimensions();
      Vector2 vector2_2 = new Vector2(dimensions.X + dimensions.Width + 7f, innerDimensions.Y);
      Tuple<Decimal, Decimal> trackerValues = this.GetTrackerValues();
      bool flag = false;
      if ((!(trackerValues.Item1 == Decimal.Zero) || !(trackerValues.Item2 == Decimal.Zero)) && this._locked)
        flag = true;
      float num2 = (float) ((double) innerDimensions.Width - (double) dimensions.Width + 1.0) - (float) (num1 * 2);
      Vector2 baseScale1;
      // ISSUE: explicit reference operation
      ((Vector2) @baseScale1).\u002Ector(0.85f);
      Vector2 baseScale2;
      // ISSUE: explicit reference operation
      ((Vector2) @baseScale2).\u002Ector(0.92f);
      string wrappedText = Main.fontItemStack.CreateWrappedText(this._achievement.Description.Value, (float) (((double) num2 - 20.0) * (1.0 / baseScale2.X)), Language.ActiveCulture.CultureInfo);
      Vector2 stringSize1 = ChatManager.GetStringSize(Main.fontItemStack, wrappedText, baseScale2, num2);
      if (!this._large)
        stringSize1 = ChatManager.GetStringSize(Main.fontItemStack, this._achievement.Description.Value, baseScale2, num2);
      float num3 = (float) (38.0 + (this._large ? 20.0 : 0.0));
      if (stringSize1.Y > (double) num3)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        __Null& local = @baseScale2.Y;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        double num4 = (double) ^(float&) local * ((double) num3 / stringSize1.Y);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) local = (float) num4;
      }
      Color baseColor1 = Color.Lerp(this._locked ? Color.get_Silver() : Color.get_Gold(), Color.get_White(), this.IsMouseHovering ? 0.5f : 0.0f);
      Color baseColor2 = Color.Lerp(this._locked ? Color.get_DarkGray() : Color.get_Silver(), Color.get_White(), this.IsMouseHovering ? 1f : 0.0f);
      Color color1 = this.IsMouseHovering ? Color.get_White() : Color.get_Gray();
      Vector2 vector2_3 = Vector2.op_Multiply(Vector2.get_UnitY(), 2f);
      Vector2 position1 = Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, vector2_3), vector2_1);
      this.DrawPanelTop(spriteBatch, position1, num2, color1);
      AchievementCategory category = this._achievement.Category;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local1 = @position1.Y;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num5 = (double) ^(float&) local1 + 2.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local1 = (float) num5;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local2 = @position1.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num6 = (double) ^(float&) local2 + 4.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local2 = (float) num6;
      spriteBatch.Draw(this._categoryTexture, position1, new Rectangle?(this._categoryTexture.Frame(4, 2, (int) category, 0)), this.IsMouseHovering ? Color.get_White() : Color.get_Silver(), 0.0f, Vector2.get_Zero(), 0.5f, (SpriteEffects) 0, 0.0f);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local3 = @position1.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num7 = (double) ^(float&) local3 + 4.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local3 = (float) num7;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local4 = @position1.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num8 = (double) ^(float&) local4 + 17.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local4 = (float) num8;
      ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, this._achievement.FriendlyName.Value, position1, baseColor1, 0.0f, Vector2.get_Zero(), baseScale1, num2, 2f);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local5 = @position1.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num9 = (double) ^(float&) local5 - 17.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local5 = (float) num9;
      Vector2 vector2_4 = Vector2.op_Multiply(Vector2.get_UnitY(), 27f);
      Vector2 position2 = Vector2.op_Addition(Vector2.op_Addition(vector2_2, vector2_4), vector2_1);
      this.DrawPanelBottom(spriteBatch, position2, num2, color1);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local6 = @position2.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num10 = (double) ^(float&) local6 + 8.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local6 = (float) num10;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local7 = @position2.Y;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num11 = (double) ^(float&) local7 + 4.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local7 = (float) num11;
      ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, wrappedText, position2, baseColor2, 0.0f, Vector2.get_Zero(), baseScale2, -1f, 2f);
      if (!flag)
        return;
      Vector2 position3 = Vector2.op_Addition(Vector2.op_Addition(position1, Vector2.op_Multiply(Vector2.get_UnitX(), num2)), Vector2.get_UnitY());
      string text = (int) trackerValues.Item1.ToString() + "/" + (int) trackerValues.Item2.ToString();
      Vector2 baseScale3;
      // ISSUE: explicit reference operation
      ((Vector2) @baseScale3).\u002Ector(0.75f);
      Vector2 stringSize2 = ChatManager.GetStringSize(Main.fontItemStack, text, baseScale3, -1f);
      float progress = (float) (trackerValues.Item1 / trackerValues.Item2);
      float Width = 80f;
      Color color2;
      // ISSUE: explicit reference operation
      ((Color) @color2).\u002Ector(100, (int) byte.MaxValue, 100);
      if (!this.IsMouseHovering)
        color2 = Color.Lerp(color2, Color.get_Black(), 0.25f);
      Color BackColor;
      // ISSUE: explicit reference operation
      ((Color) @BackColor).\u002Ector((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      if (!this.IsMouseHovering)
        BackColor = Color.Lerp(BackColor, Color.get_Black(), 0.25f);
      this.DrawProgressBar(spriteBatch, progress, Vector2.op_Subtraction(position3, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.get_UnitX(), Width), 0.7f)), Width, BackColor, color2, color2.MultiplyRGBA(new Color(new Vector4(1f, 1f, 1f, 0.5f))));
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local8 = @position3.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num12 = (double) ^(float&) local8 - ((double) Width * 1.39999997615814 + stringSize2.X);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local8 = (float) num12;
      ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, text, position3, baseColor1, 0.0f, new Vector2(0.0f, 0.0f), baseScale3, 90f, 2f);
    }

    private void UpdateIconFrame()
    {
      this._iconFrame = this._locked ? this._iconFrameLocked : this._iconFrameUnlocked;
      if (this._achievementIcon == null)
        return;
      this._achievementIcon.SetFrame(this._iconFrame);
    }

    private void DrawPanelTop(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
    {
      spriteBatch.Draw(this._innerPanelTopTexture, position, new Rectangle?(new Rectangle(0, 0, 2, this._innerPanelTopTexture.get_Height())), color);
      spriteBatch.Draw(this._innerPanelTopTexture, new Vector2((float) (position.X + 2.0), (float) position.Y), new Rectangle?(new Rectangle(2, 0, 2, this._innerPanelTopTexture.get_Height())), color, 0.0f, Vector2.get_Zero(), new Vector2((float) (((double) width - 4.0) / 2.0), 1f), (SpriteEffects) 0, 0.0f);
      spriteBatch.Draw(this._innerPanelTopTexture, new Vector2((float) (position.X + (double) width - 2.0), (float) position.Y), new Rectangle?(new Rectangle(4, 0, 2, this._innerPanelTopTexture.get_Height())), color);
    }

    private void DrawPanelBottom(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
    {
      spriteBatch.Draw(this._innerPanelBottomTexture, position, new Rectangle?(new Rectangle(0, 0, 6, this._innerPanelBottomTexture.get_Height())), color);
      spriteBatch.Draw(this._innerPanelBottomTexture, new Vector2((float) (position.X + 6.0), (float) position.Y), new Rectangle?(new Rectangle(6, 0, 7, this._innerPanelBottomTexture.get_Height())), color, 0.0f, Vector2.get_Zero(), new Vector2((float) (((double) width - 12.0) / 7.0), 1f), (SpriteEffects) 0, 0.0f);
      spriteBatch.Draw(this._innerPanelBottomTexture, new Vector2((float) (position.X + (double) width - 6.0), (float) position.Y), new Rectangle?(new Rectangle(13, 0, 6, this._innerPanelBottomTexture.get_Height())), color);
    }

    public override void MouseOver(UIMouseEvent evt)
    {
      base.MouseOver(evt);
      this.BackgroundColor = new Color(46, 60, 119);
      this.BorderColor = new Color(20, 30, 56);
    }

    public override void MouseOut(UIMouseEvent evt)
    {
      base.MouseOut(evt);
      this.BackgroundColor = Color.op_Multiply(new Color(26, 40, 89), 0.8f);
      this.BorderColor = Color.op_Multiply(new Color(13, 20, 44), 0.8f);
    }

    public Achievement GetAchievement()
    {
      return this._achievement;
    }

    private Tuple<Decimal, Decimal> GetTrackerValues()
    {
      if (!this._achievement.HasTracker)
        return Tuple.Create<Decimal, Decimal>(Decimal.Zero, Decimal.Zero);
      IAchievementTracker tracker = this._achievement.GetTracker();
      if (tracker.GetTrackerType() == TrackerType.Int)
      {
        AchievementTracker<int> achievementTracker = (AchievementTracker<int>) tracker;
        return Tuple.Create<Decimal, Decimal>((Decimal) achievementTracker.Value, (Decimal) achievementTracker.MaxValue);
      }
      if (tracker.GetTrackerType() != TrackerType.Float)
        return Tuple.Create<Decimal, Decimal>(Decimal.Zero, Decimal.Zero);
      AchievementTracker<float> achievementTracker1 = (AchievementTracker<float>) tracker;
      return Tuple.Create<Decimal, Decimal>((Decimal) achievementTracker1.Value, (Decimal) achievementTracker1.MaxValue);
    }

    private void DrawProgressBar(SpriteBatch spriteBatch, float progress, Vector2 spot, float Width = 169f, Color BackColor = null, Color FillingColor = null, Color BlipColor = null)
    {
      if (Color.op_Equality(BlipColor, Color.get_Transparent()))
      {
        // ISSUE: explicit reference operation
        ((Color) @BlipColor).\u002Ector((int) byte.MaxValue, 165, 0, (int) sbyte.MaxValue);
      }
      if (Color.op_Equality(FillingColor, Color.get_Transparent()))
      {
        // ISSUE: explicit reference operation
        ((Color) @FillingColor).\u002Ector((int) byte.MaxValue, 241, 51);
      }
      if (Color.op_Equality(BackColor, Color.get_Transparent()))
      {
        // ISSUE: explicit reference operation
        ((Color) @FillingColor).\u002Ector((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      }
      Texture2D colorBarTexture = Main.colorBarTexture;
      Texture2D colorBlipTexture = Main.colorBlipTexture;
      Texture2D magicPixel = Main.magicPixel;
      float num1 = MathHelper.Clamp(progress, 0.0f, 1f);
      float num2 = Width * 1f;
      float num3 = 8f;
      float num4 = num2 / 169f;
      Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Addition(spot, Vector2.op_Multiply(Vector2.get_UnitY(), num3)), Vector2.op_Multiply(Vector2.get_UnitX(), 1f));
      spriteBatch.Draw(colorBarTexture, spot, new Rectangle?(new Rectangle(5, 0, colorBarTexture.get_Width() - 9, colorBarTexture.get_Height())), BackColor, 0.0f, new Vector2(84.5f, 0.0f), new Vector2(num4, 1f), (SpriteEffects) 0, 0.0f);
      spriteBatch.Draw(colorBarTexture, Vector2.op_Addition(spot, new Vector2((float) (-(double) num4 * 84.5 - 5.0), 0.0f)), new Rectangle?(new Rectangle(0, 0, 5, colorBarTexture.get_Height())), BackColor, 0.0f, Vector2.get_Zero(), Vector2.get_One(), (SpriteEffects) 0, 0.0f);
      spriteBatch.Draw(colorBarTexture, Vector2.op_Addition(spot, new Vector2(num4 * 84.5f, 0.0f)), new Rectangle?(new Rectangle(colorBarTexture.get_Width() - 4, 0, 4, colorBarTexture.get_Height())), BackColor, 0.0f, Vector2.get_Zero(), Vector2.get_One(), (SpriteEffects) 0, 0.0f);
      Vector2 vector2_2 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.get_UnitX(), num1 - 0.5f), num2));
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local = @vector2_2.X;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      double num5 = (double) ^(float&) local - 1.0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) local = (float) num5;
      spriteBatch.Draw(magicPixel, vector2_2, new Rectangle?(new Rectangle(0, 0, 1, 1)), FillingColor, 0.0f, new Vector2(1f, 0.5f), new Vector2(num2 * num1, num3), (SpriteEffects) 0, 0.0f);
      if ((double) progress != 0.0)
        spriteBatch.Draw(magicPixel, vector2_2, new Rectangle?(new Rectangle(0, 0, 1, 1)), BlipColor, 0.0f, new Vector2(1f, 0.5f), new Vector2(2f, num3), (SpriteEffects) 0, 0.0f);
      spriteBatch.Draw(magicPixel, vector2_2, new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.get_Black(), 0.0f, new Vector2(0.0f, 0.5f), new Vector2(num2 * (1f - num1), num3), (SpriteEffects) 0, 0.0f);
    }

    public override int CompareTo(object obj)
    {
      UIAchievementListItem achievementListItem = obj as UIAchievementListItem;
      if (achievementListItem == null)
        return 0;
      if (this._achievement.IsCompleted && !achievementListItem._achievement.IsCompleted)
        return -1;
      if (!this._achievement.IsCompleted && achievementListItem._achievement.IsCompleted)
        return 1;
      return this._achievement.Id.CompareTo(achievementListItem._achievement.Id);
    }
  }
}
