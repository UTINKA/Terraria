// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.NetServerSocialModule
// Assembly: Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null
// MVID: DEE50102-BCC2-472F-987B-153E892583F1
// Assembly location: E:\Steam\SteamApps\common\Terraria\Terraria.exe

using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.Steam
{
  public class NetServerSocialModule : NetSocialModule
  {
    private ServerMode _mode;
    private Callback<P2PSessionRequest_t> _p2pSessionRequest;
    private bool _acceptingClients;
    private SocketConnectionAccepted _connectionAcceptedCallback;

    public NetServerSocialModule()
      : base(1, 2)
    {
    }

    private void BroadcastConnectedUsers()
    {
      List<ulong> ulongList = new List<ulong>();
      using (IEnumerator<KeyValuePair<CSteamID, NetSocialModule.ConnectionState>> enumerator = this._connectionStateMap.GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          KeyValuePair<CSteamID, NetSocialModule.ConnectionState> current = enumerator.Current;
          if (current.Value == NetSocialModule.ConnectionState.Connected)
            ulongList.Add((ulong) current.Key.m_SteamID);
        }
      }
      byte[] numArray = new byte[ulongList.Count * 8 + 1];
      using (MemoryStream memoryStream = new MemoryStream(numArray))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          binaryWriter.Write((byte) 1);
          foreach (ulong num in ulongList)
            binaryWriter.Write(num);
        }
      }
      this._lobby.SendMessage(numArray);
    }

    public override void Initialize()
    {
      base.Initialize();
      this._reader.SetReadEvent(new SteamP2PReader.OnReadEvent(this.OnPacketRead));
      // ISSUE: method pointer
      this._p2pSessionRequest = Callback<P2PSessionRequest_t>.Create(new Callback<P2PSessionRequest_t>.DispatchDelegate((object) this, __methodptr(OnP2PSessionRequest)));
      if (Program.LaunchParameters.ContainsKey("-lobby"))
      {
        this._mode |= ServerMode.Lobby;
        switch (Program.LaunchParameters["-lobby"])
        {
          case "private":
            // ISSUE: method pointer
            this._lobby.Create(true, new CallResult<LobbyCreated_t>.APIDispatchDelegate((object) this, __methodptr(OnLobbyCreated)));
            break;
          case "friends":
            this._mode |= ServerMode.FriendsCanJoin;
            // ISSUE: method pointer
            this._lobby.Create(false, new CallResult<LobbyCreated_t>.APIDispatchDelegate((object) this, __methodptr(OnLobbyCreated)));
            break;
          default:
            Console.WriteLine(Language.GetTextValue("Error.InvalidLobbyFlag", (object) "private", (object) "friends"));
            break;
        }
      }
      if (!Program.LaunchParameters.ContainsKey("-friendsoffriends"))
        return;
      this._mode |= ServerMode.FriendsOfFriends;
    }

    public override ulong GetLobbyId()
    {
      return (ulong) this._lobby.Id.m_SteamID;
    }

    public override void OpenInviteInterface()
    {
    }

    public override void CancelJoin()
    {
    }

    public override bool CanInvite()
    {
      return false;
    }

    public override void LaunchLocalServer(Process process, ServerMode mode)
    {
    }

    public override bool StartListening(SocketConnectionAccepted callback)
    {
      this._acceptingClients = true;
      this._connectionAcceptedCallback = callback;
      return true;
    }

    public override void StopListening()
    {
      this._acceptingClients = false;
    }

    public override void Connect(RemoteAddress address)
    {
    }

    public override void Close(RemoteAddress address)
    {
      this.Close(this.RemoteAddressToSteamId(address));
    }

    private void Close(CSteamID user)
    {
      if (!this._connectionStateMap.ContainsKey(user))
        return;
      SteamUser.EndAuthSession(user);
      SteamNetworking.CloseP2PSessionWithUser(user);
      this._connectionStateMap[user] = NetSocialModule.ConnectionState.Inactive;
      this._reader.ClearUser(user);
      this._writer.ClearUser(user);
    }

    private void OnLobbyCreated(LobbyCreated_t result, bool failure)
    {
      if (failure)
        return;
      SteamFriends.SetRichPresence("status", Language.GetTextValue("Social.StatusInGame"));
    }

    private bool OnPacketRead(byte[] data, int length, CSteamID userId)
    {
      if (!this._connectionStateMap.ContainsKey(userId) || this._connectionStateMap[userId] == NetSocialModule.ConnectionState.Inactive)
      {
        P2PSessionRequest_t result;
        result.m_steamIDRemote = (__Null) userId;
        this.OnP2PSessionRequest(result);
        if (!this._connectionStateMap.ContainsKey(userId) || this._connectionStateMap[userId] == NetSocialModule.ConnectionState.Inactive)
          return false;
      }
      NetSocialModule.ConnectionState connectionState = this._connectionStateMap[userId];
      if (connectionState != NetSocialModule.ConnectionState.Authenticating)
        return connectionState == NetSocialModule.ConnectionState.Connected;
      if (length < 3 || ((int) data[1] << 8 | (int) data[0]) != length || (int) data[2] != 93)
        return false;
      byte[] numArray = new byte[data.Length - 3];
      Array.Copy((Array) data, 3, (Array) numArray, 0, numArray.Length);
      switch ((int) SteamUser.BeginAuthSession(numArray, numArray.Length, userId))
      {
        case 0:
          this._connectionStateMap[userId] = NetSocialModule.ConnectionState.Connected;
          this.BroadcastConnectedUsers();
          break;
        case 1:
          this.Close(userId);
          break;
        case 2:
          this.Close(userId);
          break;
        case 3:
          this.Close(userId);
          break;
        case 4:
          this.Close(userId);
          break;
        case 5:
          this.Close(userId);
          break;
      }
      return false;
    }

    private void OnP2PSessionRequest(P2PSessionRequest_t result)
    {
      CSteamID steamIdRemote = (CSteamID) result.m_steamIDRemote;
      if (this._connectionStateMap.ContainsKey(steamIdRemote) && this._connectionStateMap[steamIdRemote] != NetSocialModule.ConnectionState.Inactive)
      {
        SteamNetworking.AcceptP2PSessionWithUser(steamIdRemote);
      }
      else
      {
        if (!this._acceptingClients || !this._mode.HasFlag((Enum) ServerMode.FriendsOfFriends) && SteamFriends.GetFriendRelationship(steamIdRemote) != 3)
          return;
        SteamNetworking.AcceptP2PSessionWithUser(steamIdRemote);
        P2PSessionState_t p2PsessionStateT;
        do
          ;
        while (SteamNetworking.GetP2PSessionState(steamIdRemote, ref p2PsessionStateT) && p2PsessionStateT.m_bConnecting == 1);
        if (p2PsessionStateT.m_bConnectionActive == null)
          this.Close(steamIdRemote);
        this._connectionStateMap[steamIdRemote] = NetSocialModule.ConnectionState.Authenticating;
        this._connectionAcceptedCallback((ISocket) new SocialSocket((RemoteAddress) new SteamAddress(steamIdRemote)));
      }
    }
  }
}
