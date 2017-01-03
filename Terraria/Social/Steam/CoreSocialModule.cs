// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.CoreSocialModule
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Steamworks;
using System;
using System.Threading;
using System.Windows.Forms;
using Terraria.Localization;

namespace Terraria.Social.Steam
{
  public class CoreSocialModule : ISocialModule
  {
    private object _steamTickLock = new object();
    private object _steamCallbackLock = new object();
    public const int SteamAppId = 105600;
    private static CoreSocialModule _instance;
    private bool IsSteamValid;
    private Callback<GameOverlayActivated_t> _onOverlayActivated;

    public static event Action OnTick;

    public void Initialize()
    {
      CoreSocialModule._instance = this;
      if (SteamAPI.RestartAppIfNecessary(new AppId_t(105600U)))
      {
        Environment.Exit(1);
      }
      else
      {
        if (!SteamAPI.Init())
        {
          int num = (int) MessageBox.Show(Language.GetTextValue("Error.LaunchFromSteam"), Language.GetTextValue("Error.Error"));
          Environment.Exit(1);
        }
        this.IsSteamValid = true;
        ThreadPool.QueueUserWorkItem(new WaitCallback(this.SteamCallbackLoop), (object) null);
        ThreadPool.QueueUserWorkItem(new WaitCallback(this.SteamTickLoop), (object) null);
        Main.OnTick += new Action(this.PulseSteamTick);
        Main.OnTick += new Action(this.PulseSteamCallback);
      }
    }

    public void PulseSteamTick()
    {
      if (!Monitor.TryEnter(this._steamTickLock))
        return;
      Monitor.Pulse(this._steamTickLock);
      Monitor.Exit(this._steamTickLock);
    }

    public void PulseSteamCallback()
    {
      if (!Monitor.TryEnter(this._steamCallbackLock))
        return;
      Monitor.Pulse(this._steamCallbackLock);
      Monitor.Exit(this._steamCallbackLock);
    }

    public static void Pulse()
    {
      CoreSocialModule._instance.PulseSteamTick();
      CoreSocialModule._instance.PulseSteamCallback();
    }

    private void SteamTickLoop(object context)
    {
      Monitor.Enter(this._steamTickLock);
      while (this.IsSteamValid)
      {
        if (CoreSocialModule.OnTick != null)
          CoreSocialModule.OnTick();
        Monitor.Wait(this._steamTickLock);
      }
      Monitor.Exit(this._steamTickLock);
    }

    private void SteamCallbackLoop(object context)
    {
      Monitor.Enter(this._steamCallbackLock);
      while (this.IsSteamValid)
      {
        SteamAPI.RunCallbacks();
        Monitor.Wait(this._steamCallbackLock);
      }
      Monitor.Exit(this._steamCallbackLock);
      SteamAPI.Shutdown();
    }

    public void Shutdown()
    {
      Application.ApplicationExit += (EventHandler) ((obj, evt) => this.IsSteamValid = false);
    }

    public void OnOverlayActivated(GameOverlayActivated_t result)
    {
      Main.instance.IsMouseVisible = result.m_bActive == 1;
    }
  }
}
