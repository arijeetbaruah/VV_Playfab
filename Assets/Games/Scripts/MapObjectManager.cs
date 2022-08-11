using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class MapObjectManager : MonoBehaviour
{
    [SerializeField] private MapObjectDataSO mapConfig;

    private Dictionary<string, GameObject> mapObjectInstance = new Dictionary<string, GameObject>();

    private void Start()
    {
        moInstance();
    }

    private void moInstance()
    {
        foreach(var mapData in mapConfig.MapObjectData)
        {
            var moInstance = Instantiate(mapData.Value.defaultPrefab);
            moInstance.transform.position = mapData.Value.defaultPrefab.transform.position;
            mapObjectInstance.Add(mapData.Key, moInstance);
        }

        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            Keys = new List<string>(mapConfig.MapObjectData.Keys)
        }, result =>
        {
            foreach(var data in result.Data)
            {
                UpdateMapObject(data.Key, data.Value.Value);
            }
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    public void UpdateMapObject(string objectID, string varientID)
    {
        var varientData = mapConfig.MapObjectData[objectID].mapVarientList.Find(varient => varient.variantID == varientID);

        Destroy(mapObjectInstance[objectID]);
        mapObjectInstance[objectID] = Instantiate(varientData.prefab);
        mapObjectInstance[objectID].transform.position = mapConfig.MapObjectData[objectID].spawnPoint;
    }

    public void SetMapObjectVarient(string varientID)
    {
        string objectID = "Cube";
        UpdateMapObject(objectID, varientID);

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { objectID, varientID }
            }
        }, result =>
        {
            print("SetMapData");
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}
