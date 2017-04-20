// Decompiled with JetBrains decompiler
// Type: Terraria.UI.LegacyGameInterfaceLayer
// Assembly: Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: DF0400F4-EE47-4864-BE80-932EDB02D8A6
// Assembly location: F:\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.UI
{
  public class LegacyGameInterfaceLayer : GameInterfaceLayer
  {
    private GameInterfaceDrawMethod _drawMethod;

    public LegacyGameInterfaceLayer(string name, GameInterfaceDrawMethod drawMethod, InterfaceScaleType scaleType = InterfaceScaleType.Game)
      : base(name, scaleType)
    {
      this._drawMethod = drawMethod;
    }

    protected override bool DrawSelf()
    {
      return this._drawMethod();
    }
  }
}
