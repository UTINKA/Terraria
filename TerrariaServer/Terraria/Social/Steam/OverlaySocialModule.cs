// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.OverlaySocialModule
// Assembly: TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null
// MVID: C2103E81-0935-4BEA-9E98-4159FC80C2BB
// Assembly location: F:\Steam\steamapps\common\Terraria\TerrariaServer.exe

using Steamworks;

namespace Terraria.Social.Steam
{
  public class OverlaySocialModule : Terraria.Social.Base.OverlaySocialModule
  {
    private Callback<GamepadTextInputDismissed_t> _gamepadTextInputDismissed;
    private bool _gamepadTextInputActive;

    public override void Initialize()
    {
      // ISSUE: method pointer
      this._gamepadTextInputDismissed = Callback<GamepadTextInputDismissed_t>.Create(new Callback<GamepadTextInputDismissed_t>.DispatchDelegate((object) this, __methodptr(OnGamepadTextInputDismissed)));
    }

    public override void Shutdown()
    {
    }

    public override bool IsGamepadTextInputActive()
    {
      return this._gamepadTextInputActive;
    }

    public override bool ShowGamepadTextInput(string description, uint maxLength, bool multiLine = false, string existingText = "", bool password = false)
    {
      if (this._gamepadTextInputActive)
        return false;
      bool flag = SteamUtils.ShowGamepadTextInput(password ? (EGamepadTextInputMode) 1 : (EGamepadTextInputMode) 0, multiLine ? (EGamepadTextInputLineMode) 1 : (EGamepadTextInputLineMode) 0, description, maxLength, existingText);
      if (flag)
        this._gamepadTextInputActive = true;
      return flag;
    }

    public override string GetGamepadText()
    {
      uint gamepadTextLength = SteamUtils.GetEnteredGamepadTextLength();
      string str;
      SteamUtils.GetEnteredGamepadTextInput(ref str, gamepadTextLength);
      return str;
    }

    private void OnGamepadTextInputDismissed(GamepadTextInputDismissed_t result)
    {
      this._gamepadTextInputActive = false;
    }
  }
}
