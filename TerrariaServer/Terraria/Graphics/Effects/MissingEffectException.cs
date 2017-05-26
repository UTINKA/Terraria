// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.MissingEffectException
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 8A63A7A2-328D-424C-BC9D-BF23F93646F7
// Assembly location: H:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using System;

namespace Terraria.Graphics.Effects
{
  public class MissingEffectException : Exception
  {
    public MissingEffectException(string text)
      : base(text)
    {
    }
  }
}
