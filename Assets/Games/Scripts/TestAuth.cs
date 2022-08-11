using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;
using System;

public class TestAuth : MonoBehaviour
{
    private void Start()
    {
        LoginWithPlayfab("snap_arijeet", r =>
        {
            PlayFabGroupService.Instance.ListPlayerMembership(result =>
            {
                Debug.Log(result.ToJson());
                Auto();
            }, error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
        });

    }

    private void Auto()
    {
        PlayFabCloudScriptAPI.ExecuteFunction(new PlayFab.CloudScriptModels.ExecuteFunctionRequest
        {
            FunctionName = "hello"
        }, result =>
        {
            print(result.FunctionResult.ToString());
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    private void CreateGroup()
    {
        PlayFabGroupService.Instance.CreateGroup("The Dragon", result =>
        {
            Debug.Log(result.ToJson());
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    private void LoginWithPlayfab(string customID, Action<LoginResult> OnSuccess)
    {
        PlayFabAuthService.Instance.LoginWithCustomID(customID, OnSuccess, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}
