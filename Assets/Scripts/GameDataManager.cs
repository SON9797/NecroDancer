using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    [Header("SO 파일")]
    public DoorDatabase doorDB;

    private void Awake()
    {
        Instance = this;
    }

    public DoorData GetDoorData(string id)
    {
        if (doorDB == null) 
        { 
            return null;
        }

        return doorDB.doorList.Find(d => d.ID == id);
    }
}
