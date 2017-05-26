// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UICharacter
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UICharacter : UIElement
  {
    private static Item _blankItem = new Item();
    private Player _player;
    private Texture2D _texture;

    public UICharacter(Player player)
    {
      this._player = player;
      this.Width.Set(59f, 0.0f);
      this.Height.Set(58f, 0.0f);
      this._texture = TextureManager.Load("Images/UI/PlayerBackground");
      this._useImmediateMode = true;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      spriteBatch.Draw(this._texture, dimensions.Position(), Color.get_White());
      Vector2 vector2 = Vector2.op_Addition(dimensions.Position(), new Vector2(dimensions.Width * 0.5f - (float) (this._player.width >> 1), dimensions.Height * 0.5f - (float) (this._player.height >> 1)));
      Item obj = this._player.inventory[this._player.selectedItem];
      this._player.inventory[this._player.selectedItem] = UICharacter._blankItem;
      Main.instance.DrawPlayer(this._player, Vector2.op_Addition(vector2, Main.screenPosition), 0.0f, Vector2.get_Zero(), 0.0f);
      this._player.inventory[this._player.selectedItem] = obj;
    }
  }
}
