// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.OverlaySocialModule
// Assembly: TerrariaServer, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: C7ED7F12-DBD9-42C5-B3E5-7642F0F95B55
// Assembly location: E:\Steam\SteamApps\common\Terraria\TerrariaServer.exe

namespace Terraria.Social.Base
{
  public abstract class OverlaySocialModule : ISocialModule
  {
    public abstract void Initialize();

    public abstract void Shutdown();

    public abstract bool IsGamepadTextInputActive();

    public abstract bool ShowGamepadTextInput(string description, uint maxLength, bool multiLine = false, string existingText = "", bool password = false);

    public abstract string GetGamepadText();
  }
}
