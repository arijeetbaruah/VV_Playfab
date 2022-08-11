using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.GroupsModels;
using System;

public class PlayFabGroupService
{
    public static PlayFabGroupService instance = null;
    public static PlayFabGroupService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayFabGroupService();
            }
            return instance;
        }
    }

    public void CreateGroup(string groupName, Action<CreateGroupResponse> OnSuccess, Action<PlayFabError> OnError)
    {
        PlayFabGroupsAPI.CreateGroup(new CreateGroupRequest
        {
            GroupName = groupName
        }, OnSuccess, OnError);
    }

    public void ListTitle()
    {
        PlayFabGroupsAPI.GetGroup(new GetGroupRequest
        {
            GroupName = "test group"
        }, result =>
        {
            Debug.Log(result.ToJson());
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    public void ListPlayerMembership(Action<ListMembershipResponse> OnSuccess, Action<PlayFabError> OnError)
    {
        PlayFabGroupsAPI.ListMembership(new ListMembershipRequest(), OnSuccess, OnError);
    }
}
