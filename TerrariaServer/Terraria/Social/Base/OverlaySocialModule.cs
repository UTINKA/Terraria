// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.OverlaySocialModule
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: 880A80AC-FC6C-4F43-ABDD-E2472DA66CB5
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

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
