using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}

public class SaveData
{
    public SaveData(string fileName_,int playTime_, int dungeonFloor_,int money_)
    {
        fileName = fileName_;
        playTime = playTime_;
        dungeonFloor = dungeonFloor_;
        money = money_;
    }

    public string fileName;
    private string filePath = Application.dataPath + "/Saves/";

    public int playTime;
    public int dungeonFloor;
    public int money;
}