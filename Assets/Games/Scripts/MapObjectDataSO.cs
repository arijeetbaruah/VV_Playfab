using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map Object Data")]
public class MapObjectDataSO : ScriptableObject
{
    public List<MapObject> mapElement = new List<MapObject>();

    private Dictionary<string, MapObject> mapDict = null;

    public Dictionary<string, MapObject> MapObjectData
    {
        get
        {
            if (mapDict == null)
            {
                mapDict = new Dictionary<string, MapObject>();

                foreach(var element in mapElement)
                {
                    mapDict.Add(element.mapElementID, element);
                }
            }

            return mapDict;
        }
    }
}

[System.Serializable]
public class MapObject
{
    public string mapElementID;
    public GameObject defaultPrefab;
    public Vector3 spawnPoint;
    public List<MapVarient> mapVarientList = new List<MapVarient>();
}

[System.Serializable]
public class MapVarient
{
    public string variantID;
    public GameObject prefab;
}
