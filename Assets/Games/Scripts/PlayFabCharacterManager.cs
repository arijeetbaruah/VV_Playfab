using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabCharacterManager
{
    private static PlayFabCharacterManager instance;
    public static PlayFabCharacterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayFabCharacterManager();
            }

            return instance;
        }
    }

    public void GetCharacters(Action<ListUsersCharactersResult> OnSuccess, Action<PlayFabError> OnError)
    {
        PlayFabClientAPI.GetAllUsersCharacters(new ListUsersCharactersRequest(), OnSuccess, OnError);
    }

    public void GrantCharacter(string characterName, string itemID = "MC", Action<GrantCharacterToUserResult> OnSuccess = null, Action<PlayFabError> OnError = null)
    {
        PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest
        {
            ItemId = itemID,
            CharacterName = characterName
        }, OnSuccess, OnError);
    }
}
