// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.MissingEffectException
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
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
