using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class TileScrollbarControl : MonoBehaviour
{
    //프리펩에 대한 설정
    private List<GameObject> tilePrefabs;
    private GameObject Contents;

    public GameObject prefabs1;

    void Start()
    {
        Contents = gameObject.FindChildObj("contents");
        tilePrefabs = new List<GameObject>();

        SettingTiles();
    }

    void Update()
    {
        
    }

    public void SettingTiles()
    {
        for(int i = 0; i < 10; i++)
        {
            Instantiate<GameObject>(this.prefabs1, Contents.transform);
        }
    }

    public void prefabsSettingList()
    {
        //파일에서 받은 개체만큼 돌려주고

        //그걸 리스트에 저장

    }
}
