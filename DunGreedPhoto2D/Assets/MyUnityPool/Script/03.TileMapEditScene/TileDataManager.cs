using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class TileMapData
{
    public void Init(string fileName_, int NumX_, int NumY_, List<TileData> tiles_)
    {
        FileName = fileName_;
        NumX = NumX_;
        NumY = NumY_;

        for(int i = 0; i < tiles_.Count; i++)
        {
            tiles.Add(tiles_[i]);
        }
    }

    public string FileName;
    public int NumX;
    public int NumY;
    public List<TileData> tiles = new List<TileData>();
}

[System.Serializable]
public class TileData
{
    public void Setting(int Number_, string PreFabsName_, Vector2 Pos_)
    {
        Number = Number_;
        PreFabsName = PreFabsName_;
        Pos = Pos_;
    }
    public int Number;
    public string PreFabsName;
    public Vector2 Pos;
}

public class TileDataManager : MonoBehaviour
{
    public static TileDataManager instance;

    public TileMapData tileMapData = new TileMapData();

    private string DirectoryPath;

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

        DirectoryPath = Application.dataPath + "/SaveMaps";
    }

    public bool TileMapDataCheck(string filename)
    {
        //파일 체크
        if (File.Exists(instance.DirectoryPath + "/" + filename))
        {
            return true;
        }
        return false;
    }

    public void TileMapDataDelete(string filename)
    {
        if (TileMapDataCheck(filename) == true)
        {
            File.Delete(DirectoryPath + "/" + filename);
        }
    }

    public void TileMapSaveData()
    {
        //폴더 체크
        if (Directory.Exists(instance.DirectoryPath) == false)
        {
            Directory.CreateDirectory(instance.DirectoryPath);
            //File.Create는 Close를 붙여 닫아줘야한다.
        }

        string data = JsonUtility.ToJson(tileMapData,true);
        File.WriteAllText(DirectoryPath + "/" + tileMapData.FileName, data);
        print(data);
    }
    public void TileMapLoadData(string filename)
    {
        if (TileMapDataCheck(filename) == true)
        {
            string data = File.ReadAllText(DirectoryPath + "/" + filename);
            tileMapData = JsonUtility.FromJson<TileMapData>(data);
        }
    }

    public void TileMapDataClear()
    {
        tileMapData = new TileMapData();
    }
}