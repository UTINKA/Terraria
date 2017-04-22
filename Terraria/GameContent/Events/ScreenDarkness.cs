// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Events.ScreenDarkness
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: E90A5A2F-CD10-4A2C-9D2A-6B036D4E8877
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Events
{
  public class ScreenDarkness
  {
    public static float screenObstruction;

    public static void Update()
    {
      float num = 0.0f;
      float amount = 0.1f;
      Vector2 mountedCenter = Main.player[Main.myPlayer].MountedCenter;
      for (int index = 0; index < 200; ++index)
      {
        if (Main.npc[index].active && Main.npc[index].type == 370 && (double) Main.npc[index].Distance(mountedCenter) < 3000.0 && ((double) Main.npc[index].ai[0] >= 10.0 || (double) Main.npc[index].ai[0] == 9.0 && (double) Main.npc[index].ai[2] > 120.0))
        {
          num = 0.95f;
          amount = 0.03f;
        }
      }
      ScreenDarkness.screenObstruction = MathHelper.Lerp(ScreenDarkness.screenObstruction, num, amount);
    }

    public static void DrawBack(SpriteBatch spriteBatch)
    {
      if ((double) ScreenDarkness.screenObstruction == 0.0)
        return;
      Color color = Color.Black * ScreenDarkness.screenObstruction;
      spriteBatch.Draw(Main.magicPixel, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
    }

    public static void DrawFront(SpriteBatch spriteBatch)
    {
      if ((double) ScreenDarkness.screenObstruction == 0.0)
        return;
      Color color = new Color(0, 0, 120) * ScreenDarkness.screenObstruction * 0.3f;
      spriteBatch.Draw(Main.magicPixel, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
    }
  }
}
