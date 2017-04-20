// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UICharacterListItem
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UICharacterListItem : UIPanel
  {
    private PlayerFileData _data;
    private Texture2D _dividerTexture;
    private Texture2D _innerPanelTexture;
    private UICharacter _playerPanel;
    private UIText _buttonLabel;
    private UIText _deleteButtonLabel;
    private Texture2D _buttonCloudActiveTexture;
    private Texture2D _buttonCloudInactiveTexture;
    private Texture2D _buttonFavoriteActiveTexture;
    private Texture2D _buttonFavoriteInactiveTexture;
    private Texture2D _buttonPlayTexture;
    private Texture2D _buttonDeleteTexture;
    private UIImageButton _deleteButton;

    public bool IsFavorite
    {
      get
      {
        return this._data.IsFavorite;
      }
    }

    public UICharacterListItem(PlayerFileData data, int snapPointIndex)
    {
      this.BorderColor = Color.op_Multiply(new Color(89, 116, 213), 0.7f);
      this._dividerTexture = TextureManager.Load("Images/UI/Divider");
      this._innerPanelTexture = TextureManager.Load("Images/UI/InnerPanelBackground");
      this._buttonCloudActiveTexture = TextureManager.Load("Images/UI/ButtonCloudActive");
      this._buttonCloudInactiveTexture = TextureManager.Load("Images/UI/ButtonCloudInactive");
      this._buttonFavoriteActiveTexture = TextureManager.Load("Images/UI/ButtonFavoriteActive");
      this._buttonFavoriteInactiveTexture = TextureManager.Load("Images/UI/ButtonFavoriteInactive");
      this._buttonPlayTexture = TextureManager.Load("Images/UI/ButtonPlay");
      this._buttonDeleteTexture = TextureManager.Load("Images/UI/ButtonDelete");
      this.Height.Set(96f, 0.0f);
      this.Width.Set(0.0f, 1f);
      this.SetPadding(6f);
      this._data = data;
      this._playerPanel = new UICharacter(data.Player);
      this._playerPanel.Left.Set(4f, 0.0f);
      this._playerPanel.OnDoubleClick += new UIElement.MouseEvent(this.PlayGame);
      this.OnDoubleClick += new UIElement.MouseEvent(this.PlayGame);
      this.Append((UIElement) this._playerPanel);
      UIImageButton uiImageButton1 = new UIImageButton(this._buttonPlayTexture);
      uiImageButton1.VAlign = 1f;
      uiImageButton1.Left.Set(4f, 0.0f);
      uiImageButton1.OnClick += new UIElement.MouseEvent(this.PlayGame);
      uiImageButton1.OnMouseOver += new UIElement.MouseEvent(this.PlayMouseOver);
      uiImageButton1.OnMouseOut += new UIElement.MouseEvent(this.ButtonMouseOut);
      this.Append((UIElement) uiImageButton1);
      UIImageButton uiImageButton2 = new UIImageButton(this._data.IsFavorite ? this._buttonFavoriteActiveTexture : this._buttonFavoriteInactiveTexture);
      uiImageButton2.VAlign = 1f;
      uiImageButton2.Left.Set(28f, 0.0f);
      uiImageButton2.OnClick += new UIElement.MouseEvent(this.FavoriteButtonClick);
      uiImageButton2.OnMouseOver += new UIElement.MouseEvent(this.FavoriteMouseOver);
      uiImageButton2.OnMouseOut += new UIElement.MouseEvent(this.ButtonMouseOut);
      uiImageButton2.SetVisibility(1f, this._data.IsFavorite ? 0.8f : 0.4f);
      this.Append((UIElement) uiImageButton2);
      if (SocialAPI.Cloud != null)
      {
        UIImageButton uiImageButton3 = new UIImageButton(this._data.IsCloudSave ? this._buttonCloudActiveTexture : this._buttonCloudInactiveTexture);
        uiImageButton3.VAlign = 1f;
        uiImageButton3.Left.Set(52f, 0.0f);
        uiImageButton3.OnClick += new UIElement.MouseEvent(this.CloudButtonClick);
        uiImageButton3.OnMouseOver += new UIElement.MouseEvent(this.CloudMouseOver);
        uiImageButton3.OnMouseOut += new UIElement.MouseEvent(this.ButtonMouseOut);
        this.Append((UIElement) uiImageButton3);
        uiImageButton3.SetSnapPoint("Cloud", snapPointIndex, new Vector2?(), new Vector2?());
      }
      UIImageButton uiImageButton4 = new UIImageButton(this._buttonDeleteTexture);
      uiImageButton4.VAlign = 1f;
      uiImageButton4.HAlign = 1f;
      uiImageButton4.OnClick += new UIElement.MouseEvent(this.DeleteButtonClick);
      uiImageButton4.OnMouseOver += new UIElement.MouseEvent(this.DeleteMouseOver);
      uiImageButton4.OnMouseOut += new UIElement.MouseEvent(this.DeleteMouseOut);
      this._deleteButton = uiImageButton4;
      if (!this._data.IsFavorite)
        this.Append((UIElement) uiImageButton4);
      this._buttonLabel = new UIText("", 1f, false);
      this._buttonLabel.VAlign = 1f;
      this._buttonLabel.Left.Set(80f, 0.0f);
      this._buttonLabel.Top.Set(-3f, 0.0f);
      this.Append((UIElement) this._buttonLabel);
      this._deleteButtonLabel = new UIText("", 1f, false);
      this._deleteButtonLabel.VAlign = 1f;
      this._deleteButtonLabel.HAlign = 1f;
      this._deleteButtonLabel.Left.Set(-30f, 0.0f);
      this._deleteButtonLabel.Top.Set(-3f, 0.0f);
      this.Append((UIElement) this._deleteButtonLabel);
      uiImageButton1.SetSnapPoint("Play", snapPointIndex, new Vector2?(), new Vector2?());
      uiImageButton2.SetSnapPoint("Favorite", snapPointIndex, new Vector2?(), new Vector2?());
      uiImageButton4.SetSnapPoint("Delete", snapPointIndex, new Vector2?(), new Vector2?());
    }

    private void FavoriteMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      if (this._data.IsFavorite)
        this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
      else
        this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
    }

    private void CloudMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      if (this._data.IsCloudSave)
        this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
      else
        this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
    }

    private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonLabel.SetText(Language.GetTextValue("UI.Play"));
    }

    private void DeleteMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      this._deleteButtonLabel.SetText(Language.GetTextValue("UI.Delete"));
    }

    private void DeleteMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
      this._deleteButtonLabel.SetText("");
    }

    private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
      this._buttonLabel.SetText("");
    }

    private void CloudButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      if (this._data.IsCloudSave)
        this._data.MoveToLocal();
      else
        this._data.MoveToCloud();
      ((UIImageButton) evt.Target).SetImage(this._data.IsCloudSave ? this._buttonCloudActiveTexture : this._buttonCloudInactiveTexture);
      if (this._data.IsCloudSave)
        this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
      else
        this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
    }

    private void DeleteButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      for (int index = 0; index < Main.PlayerList.Count; ++index)
      {
        if (Main.PlayerList[index] == this._data)
        {
          Main.PlaySound(10, -1, -1, 1, 1f, 0.0f);
          Main.selectedPlayer = index;
          Main.menuMode = 5;
          break;
        }
      }
    }

    private void PlayGame(UIMouseEvent evt, UIElement listeningElement)
    {
      if (listeningElement != evt.Target || this._data.Player.loadStatus != 0)
        return;
      Main.SelectPlayer(this._data);
    }

    private void FavoriteButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      this._data.ToggleFavorite();
      ((UIImageButton) evt.Target).SetImage(this._data.IsFavorite ? this._buttonFavoriteActiveTexture : this._buttonFavoriteInactiveTexture);
      ((UIImageButton) evt.Target).SetVisibility(1f, this._data.IsFavorite ? 0.8f : 0.4f);
      if (this._data.IsFavorite)
      {
        this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
        this.RemoveChild((UIElement) this._deleteButton);
      }
      else
      {
        this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
        this.Append((UIElement) this._deleteButton);
      }
      UIList parent = this.Parent.Parent as UIList;
      if (parent == null)
        return;
      parent.UpdateOrder();
    }

    public override int CompareTo(object obj)
    {
      UICharacterListItem characterListItem = obj as UICharacterListItem;
      if (characterListItem == null)
        return base.CompareTo(obj);
      if (this.IsFavorite && !characterListItem.IsFavorite)
        return -1;
      if (!this.IsFavorite && characterListItem.IsFavorite)
        return 1;
      if (this._data.Name.CompareTo(characterListItem._data.Name) != 0)
        return this._data.Name.CompareTo(characterListItem._data.Name);
      return this._data.GetFileName(true).CompareTo(characterListItem._data.GetFileName(true));
    }

    public override void MouseOver(UIMouseEvent evt)
    {
      base.MouseOver(evt);
      this.BackgroundColor = new Color(73, 94, 171);
      this.BorderColor = new Color(89, 116, 213);
    }

    public override void MouseOut(UIMouseEvent evt)
    {
      base.MouseOut(evt);
      this.BackgroundColor = Color.op_Multiply(new Color(63, 82, 151), 0.7f);
      this.BorderColor = Color.op_Multiply(new Color(89, 116, 213), 0.7f);
    }

    private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
    {
      spriteBatch.Draw(this._innerPanelTexture, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.get_Height())), Color.get_White());
      spriteBatch.Draw(this._innerPanelTexture, new Vector2((float) (position.X + 8.0), (float) position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.get_Height())), Color.get_White(), 0.0f, Vector2.get_Zero(), new Vector2((float) (((double) width - 16.0) / 8.0), 1f), (SpriteEffects) 0, 0.0f);
      spriteBatch.Draw(this._innerPanelTexture, new Vector2((float) (position.X + (double) width - 8.0), (float) position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.get_Height())), Color.get_White());
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      CalculatedStyle dimensions = this._playerPanel.GetDimensions();
      float num1 = dimensions.X + dimensions.Width;
      Utils.DrawBorderString(spriteBatch, this._data.Name, new Vector2(num1 + 6f, dimensions.Y - 2f), Color.get_White(), 1f, 0.0f, 0.0f, -1);
      spriteBatch.Draw(this._dividerTexture, new Vector2(num1, innerDimensions.Y + 21f), new Rectangle?(), Color.get_White(), 0.0f, Vector2.get_Zero(), new Vector2((float) (((double) this.GetDimensions().X + (double) this.GetDimensions().Width - (double) num1) / 8.0), 1f), (SpriteEffects) 0, 0.0f);
      Vector2 vector2;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2).\u002Ector(num1 + 6f, innerDimensions.Y + 29f);
      float width1 = 200f;
      Vector2 position1 = vector2;
      this.DrawPanel(spriteBatch, position1, width1);
      spriteBatch.Draw(Main.heartTexture, Vector2.op_Addition(position1, new Vector2(5f, 2f)), Color.get_White());
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local1 = @position1;
      // ISSUE: explicit reference operation
      double num2 = (^local1).X + (10.0 + (double) Main.heartTexture.get_Width());
      // ISSUE: explicit reference operation
      (^local1).X = (__Null) num2;
      Utils.DrawBorderString(spriteBatch, this._data.Player.statLifeMax.ToString() + " HP", Vector2.op_Addition(position1, new Vector2(0.0f, 3f)), Color.get_White(), 1f, 0.0f, 0.0f, -1);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local2 = @position1;
      // ISSUE: explicit reference operation
      double num3 = (^local2).X + 65.0;
      // ISSUE: explicit reference operation
      (^local2).X = (__Null) num3;
      spriteBatch.Draw(Main.manaTexture, Vector2.op_Addition(position1, new Vector2(5f, 2f)), Color.get_White());
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local3 = @position1;
      // ISSUE: explicit reference operation
      double num4 = (^local3).X + (10.0 + (double) Main.manaTexture.get_Width());
      // ISSUE: explicit reference operation
      (^local3).X = (__Null) num4;
      Utils.DrawBorderString(spriteBatch, this._data.Player.statManaMax.ToString() + " MP", Vector2.op_Addition(position1, new Vector2(0.0f, 3f)), Color.get_White(), 1f, 0.0f, 0.0f, -1);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local4 = @vector2;
      // ISSUE: explicit reference operation
      double num5 = (^local4).X + ((double) width1 + 5.0);
      // ISSUE: explicit reference operation
      (^local4).X = (__Null) num5;
      Vector2 position2 = vector2;
      float width2 = 140f;
      if (GameCulture.Russian.IsActive)
        width2 = 180f;
      this.DrawPanel(spriteBatch, position2, width2);
      string text1 = "";
      Color color = Color.get_White();
      switch (this._data.Player.difficulty)
      {
        case 0:
          text1 = Language.GetTextValue("UI.Softcore");
          break;
        case 1:
          text1 = Language.GetTextValue("UI.Mediumcore");
          color = Main.mcColor;
          break;
        case 2:
          text1 = Language.GetTextValue("UI.Hardcore");
          color = Main.hcColor;
          break;
      }
      Vector2 pos1 = Vector2.op_Addition(position2, new Vector2((float) ((double) width2 * 0.5 - Main.fontMouseText.MeasureString(text1).X * 0.5), 3f));
      Utils.DrawBorderString(spriteBatch, text1, pos1, color, 1f, 0.0f, 0.0f, -1);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local5 = @vector2;
      // ISSUE: explicit reference operation
      double num6 = (^local5).X + ((double) width2 + 5.0);
      // ISSUE: explicit reference operation
      (^local5).X = (__Null) num6;
      Vector2 position3 = vector2;
      float width3 = (float) ((double) innerDimensions.X + (double) innerDimensions.Width - position3.X);
      this.DrawPanel(spriteBatch, position3, width3);
      TimeSpan playTime = this._data.GetPlayTime();
      int num7 = playTime.Days * 24 + playTime.Hours;
      string text2 = (num7 < 10 ? (object) "0" : (object) "").ToString() + (object) num7 + playTime.ToString("\\:mm\\:ss");
      Vector2 pos2 = Vector2.op_Addition(position3, new Vector2((float) ((double) width3 * 0.5 - Main.fontMouseText.MeasureString(text2).X * 0.5), 3f));
      Utils.DrawBorderString(spriteBatch, text2, pos2, Color.get_White(), 1f, 0.0f, 0.0f, -1);
    }
  }
}
