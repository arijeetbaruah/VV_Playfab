using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabAuthService
{
    private static PlayFabAuthService instance = null;
    public static PlayFabAuthService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayFabAuthService();
            }

            return instance;
        }
    }

    private string AuthID
    {
        get
        {
            return PlayerPrefs.GetString("Auth", "");
        }
        set
        {
            var guid = value;
            PlayerPrefs.SetString("Auth", guid);
        }
    }

    public void IsAuthenticated(Action<LoginResult> OnSuccess, Action<PlayFabError> OnError)
    {
        if (PlayerPrefs.HasKey("Auth"))
        {
            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
            {
                CustomId = AuthID,
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
        }
    }

    public void LoginWithEmailAndPassword(string email, string password, Action<LoginResult> OnSuccess, Action<PlayFabError> OnError)
    {
        LoginWithEmailAddressRequest request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnSuccess, OnError);
    }

    public void RegisterPlayer(string email, string displayname, string password, Action<RegisterPlayFabUserResult> OnSuccess, Action<PlayFabError> OnError)
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
        {
            Email = email,
            DisplayName = displayname,
            Username = displayname,
            Password = password,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnSuccess, OnError);
    }

    public void LinkCustomID(Action<LinkCustomIDResult> OnSuccess, Action<PlayFabError> OnError)
    {
        AuthID = System.Guid.NewGuid().ToString();
        LinkCustomIDRequest request = new LinkCustomIDRequest
        {
            CustomId = AuthID,
        };
        PlayFabClientAPI.LinkCustomID(request, OnSuccess, OnError);
    }

    public void LoginWithCustomID(string customID, Action<LoginResult> OnSuccess, Action<PlayFabError> OnError)
    {
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
        {
            CreateAccount = true,
            CustomId = customID
        }, OnSuccess, OnError);
    }
}
