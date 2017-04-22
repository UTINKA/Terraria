// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.MissingEffectException
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
