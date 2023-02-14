using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class SlotData
{
    public void Setting(int slotnum_,int playTime_, int dungeonFloor_, int money_,int mapPos_)
    {
        slotnum = slotnum_;
        playTime = playTime_;
        dungeonFloor = dungeonFloor_;
        money = money_;
        mapPos = mapPos_;
    }

    /*
     * mapPos 가이드
     * 0 : 마을
     * 1 ~ : 던전 ( + 층수) 
     */

    public int slotnum;
    public int playTime;
    public int dungeonFloor;
    public int money;
    public int mapPos;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public SlotData slotData = new SlotData();

    public string DirectoryPath;
    public string fileName;

    public int PlaySlotNum;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        DirectoryPath = Application.dataPath + "/Save";
        PlaySlotNum = 0;
    }

    public bool SlotDataCheck(int slotNum)
    {
        fileName = "slot_";

        //파일 체크
        if (File.Exists(instance.DirectoryPath + "/" + fileName + slotNum.ToString()))
        {
            return true;
        }
        return false;
    }

    public void SlotDataDelete(int slotNum)
    {
        if (SlotDataCheck(slotNum) == true)
        {
            fileName = "slot_";
            File.Delete(DirectoryPath + "/" + fileName + slotNum.ToString());
        }
    }

    public void SlotSaveData()
    {
        //폴더 체크
        if (Directory.Exists(instance.DirectoryPath) == false)
        {
            Directory.CreateDirectory(instance.DirectoryPath);
            //File.Create는 Close를 붙여 닫아줘야한다.
        }

        fileName = "slot_";
        string data = JsonUtility.ToJson(slotData);
        File.WriteAllText(DirectoryPath + "/" + fileName + slotData.slotnum.ToString(), data);
    }
    public void SlotLoadData(int slotNum)
    {
        if (SlotDataCheck(slotNum) == true)
        {
            fileName = "slot_";
            string data = File.ReadAllText(DirectoryPath + "/" + fileName + slotNum.ToString());
            slotData = JsonUtility.FromJson<SlotData>(data);
        }
    }

    public void SlotDataClear()
    {
        slotData = new SlotData();
    }
}
