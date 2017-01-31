// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.MissingEffectException
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

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
