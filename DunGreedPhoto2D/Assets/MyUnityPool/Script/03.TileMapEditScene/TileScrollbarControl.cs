using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class TileScrollbarControl : MonoBehaviour
{
    //�����鿡 ���� ����
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
        //���Ͽ��� ���� ��ü��ŭ �����ְ�

        //�װ� ����Ʈ�� ����

    }
}
