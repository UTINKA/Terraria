// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Events.ScreenObstruction
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Events
{
  public class ScreenObstruction
  {
    public static float screenObstruction;

    public static void Update()
    {
      float num1 = 0.0f;
      float num2 = 0.1f;
      if (Main.player[Main.myPlayer].headcovered)
      {
        num1 = 0.95f;
        num2 = 0.3f;
      }
      ScreenObstruction.screenObstruction = MathHelper.Lerp(ScreenObstruction.screenObstruction, num1, num2);
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
      if ((double) ScreenObstruction.screenObstruction == 0.0)
        return;
      Color color = Color.op_Multiply(Color.get_Black(), ScreenObstruction.screenObstruction);
      int width = Main.extraTexture[49].get_Width();
      int num = 10;
      Rectangle rect = Main.player[Main.myPlayer].getRect();
      // ISSUE: explicit reference operation
      ((Rectangle) @rect).Inflate((width - rect.Width) / 2, (width - rect.Height) / 2 + num / 2);
      // ISSUE: explicit reference operation
      ((Rectangle) @rect).Offset(-(int) Main.screenPosition.X, -(int) Main.screenPosition.Y + (int) Main.player[Main.myPlayer].gfxOffY - num);
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      Rectangle rectangle1 = Rectangle.Union(new Rectangle(0, 0, 1, 1), new Rectangle(((Rectangle) @rect).get_Right() - 1, ((Rectangle) @rect).get_Top() - 1, 1, 1));
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      Rectangle rectangle2 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, 0, 1, 1), new Rectangle(((Rectangle) @rect).get_Right(), ((Rectangle) @rect).get_Bottom() - 1, 1, 1));
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      Rectangle rectangle3 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, Main.screenHeight - 1, 1, 1), new Rectangle(((Rectangle) @rect).get_Left(), ((Rectangle) @rect).get_Bottom(), 1, 1));
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      Rectangle rectangle4 = Rectangle.Union(new Rectangle(0, Main.screenHeight - 1, 1, 1), new Rectangle(((Rectangle) @rect).get_Left() - 1, ((Rectangle) @rect).get_Top(), 1, 1));
      spriteBatch.Draw(Main.magicPixel, rectangle1, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
      spriteBatch.Draw(Main.magicPixel, rectangle2, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
      spriteBatch.Draw(Main.magicPixel, rectangle3, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
      spriteBatch.Draw(Main.magicPixel, rectangle4, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
      spriteBatch.Draw(Main.extraTexture[49], rect, color);
    }
  }
}
