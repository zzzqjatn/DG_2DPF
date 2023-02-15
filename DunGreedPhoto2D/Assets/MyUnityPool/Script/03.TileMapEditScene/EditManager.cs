using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class EditManager : MonoBehaviour
{
    private const int MAX_TILE_COUNTX = 25;
    private const int MAX_TILE_COUNTY = 25;

    private const int TileSize = 16;

    private GameObject MainRootObj;

    private GameObject SizeinputRow;
    private GameObject SizeinputCol;

    private GameObject emptyTile;

    private List<GameObject> TileList;
    private GameObject TileMap;

    private GameObject fileInputField;
    //private GameObject saveButton;
    //private GameObject loadButton;

    int MapSizeX;
    int MapSizeY;

    void Start()
    {
        MainRootObj = GFunc.FindRootObj("MapEditObjs");
        SizeinputRow = MainRootObj.FindChildObj("RowInputField");
        SizeinputCol = MainRootObj.FindChildObj("ColInputField");
        TileMap = MainRootObj.FindChildObj("TileMap");

        fileInputField = MainRootObj.FindChildObj("FileInputField");
        //saveButton = MainRootObj.FindChildObj("SaveButton");
        //loadButton = MainRootObj.FindChildObj("LoadButton");

        emptyTile = Resources.Load<GameObject>("Prefabs/emptyTile");

        TileList = new List<GameObject>();

        setMapSetting();
    }

    void Update()
    {

    }

    //맵사이즈 버튼을 눌렀을 때 비활성화된 타일 활성화
    public void MapSize_Clicked()
    {
        MapSizeX = 0;
        MapSizeY = 0;

        int.TryParse(SizeinputRow.IntInputFieldLimit(0, MAX_TILE_COUNTX), out MapSizeX);
        int.TryParse(SizeinputCol.IntInputFieldLimit(0, MAX_TILE_COUNTY), out MapSizeY);

        int StartXnum = (MAX_TILE_COUNTX - MapSizeX) / 2;
        int StartYnum = (MAX_TILE_COUNTY - MapSizeY) / 2;

        int EndXnum = StartXnum + MapSizeX;
        int EndYnum = StartYnum + MapSizeY;

        for (int i = StartYnum; i < EndYnum; i++)
        {
            for (int j = StartXnum; j < EndXnum; j++)
            {
                TileList[j + ((i - StartYnum) * MAX_TILE_COUNTX)].SetActive(true);
            }
        }
    }

    //미리 오브젝트 풀하는 변수
    public void setMapSetting()
    {
        int StartX = 0 - (TileSize * MAX_TILE_COUNTX) / 2;
        int StartY = -80;

        for (int i = 0; i < MAX_TILE_COUNTX; i++)
        {
            for (int j = 0; j < MAX_TILE_COUNTY; j++)
            {
                GameObject tempObj = Instantiate<GameObject>(emptyTile, TileMap.transform);
                tempObj.Rect().localPosition = new Vector3(
                    StartX + TileSize * j, StartY + TileSize * i, 0.0f);
                tempObj.name = "Tile_" + string.Format("{0}", j + i * MAX_TILE_COUNTX);
                tempObj.SetActive(false);
                TileList.Add(tempObj);
            }
        }
    }

    //저장버튼 클릭이벤트
    public void saveButton_Clicked()
    {
        if (MapSizeX > 0 && MapSizeY > 0)
        {
            TileMapData tempMapData = new TileMapData();
            List<TileData> tempTileData = new List<TileData>();

            for (int i = 0; i < TileList.Count; i++)
            {
                if (TileList[i].activeSelf == true)
                {
                    TileData temp = new TileData();
                    temp.Setting(i, TileList[i].name, TileList[i].RectLocalPos());
                    tempTileData.Add(temp);
                }
            }

            TileDataManager.instance.tileMapData.Init(fileInputField.GetInputFieldTxt(), MapSizeX, MapSizeY, tempTileData);
            TileDataManager.instance.TileMapSaveData();
        }
    }
}
