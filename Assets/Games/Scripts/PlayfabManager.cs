using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    private void Start()
    {
        PlayFabCharacterManager.Instance.GrantCharacter("MC", OnSuccess: result =>
        {
            Debug.Log(result.CharacterId);
        }, OnError: error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
        //PlayFabCharacterManager.Instance.GetCharacters
    }
}
