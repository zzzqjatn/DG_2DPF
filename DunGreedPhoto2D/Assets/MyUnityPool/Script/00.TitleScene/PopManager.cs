using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class PopManager : MonoBehaviour
{
    public const string POPUP1 = "Pop1";
    private List<GameObject> DelButtons;
        
    void Start()
    {
        SlotLoadData();
        DelButtonSet();
    }

    void Update()
    {
        DelButtonCheck();
    }

    public void DelButtonCheck()
    {
        for (int i = 0; i < 3; i++)
        {
            deleteButton temp = DelButtons[i].GetComponent<deleteButton>();
            if (temp.GetisDelete() == true)
            {
                DataManager.instance.SlotDataDelete(i + 1);
                temp.SetisDelete(false);
                SlotLoadData();
            }
        }
    }

    public void DelButtonSet()
    {
        DelButtons = new List<GameObject>();

        for(int i = 1; i <= 3; i++)
        {
            GameObject temp = gameObject.FindChildObj($"Slot{i}DeleteButton");
            DelButtons.Add(temp);
        }
    }

    public void SlotLoadData()
    {
        string tempName = "Slot";

        for (int i = 1; i <= 3; i++)
        {
            GameObject slotObj = gameObject.FindChildObj(tempName + i);
            //파일이 있다면
            if (DataManager.instance.SlotDataCheck(i) == true)
            {
                DataManager.instance.SlotLoadData(i);
                SlotDataFormatToString(slotObj, DataManager.instance.slotData);
                DataManager.instance.SlotDataClear();
            }
            else
            {
                SlotDataFormatToString(slotObj, null);
            }
        }
    }

    public void SlotDataFormatToString(GameObject obj, SlotData data)
    {
        string firstTxt = default;
        string SecondTxt = default;

        if (data == null || data == default)
        {
            firstTxt = "데이터 없음";
            SecondTxt = "";
        }
        else
        {               //<line-height=10> </line-height> //개행크기 설정
            firstTxt = $"<플레이 시간>\n<line-height=10>{data.playTime / 60}H {data.playTime % 60}m\n" +
                     $"<도달한 층></line-height>\n<line-height=10>{data.dungeonFloor}F\n" +
                     $"<소지금></line-height>\n{data.money}G";

            switch(data.mapPos)
            {
                case 0:
                    SecondTxt = $"<현재 위치>\n<color=green>마을</color>";
                    break;
                default:
                    SecondTxt = $"<현재 위치>\n<color=red>던전</color> {data.mapPos}F";
                    break;
            }
        }
        obj.FindChildObj("FirstSlotTxt").SetTxt(firstTxt);
        obj.FindChildObj("SecondSlotTxt").SetTxt(SecondTxt);
    }

    public void charButton_Clicked(int num)
    {
        string tempName = "Slot";

        GameObject slotObj = gameObject.FindChildObj(tempName + num);
        //파일이 있다면
        if (DataManager.instance.SlotDataCheck(num) == true)
        {
            //기존 데이터 보내주기
            DataManager.instance.PlaySlotNum = num;

            //플레이씬으로
            LoadingSceneManager.LoadScene("02.PlayScene");
        }
        else
        {
            // 기본 설정만들기
            SlotData temp = new SlotData();
            temp.Setting(num, 0, 0, 100, 0);
            DataManager.instance.slotData = temp;
            DataManager.instance.SlotSaveData();
            DataManager.instance.SlotDataClear();

            DataManager.instance.PlaySlotNum = num;

            //플레이씬으로
            LoadingSceneManager.LoadScene("02.PlayScene");
        }
    }

    public void GameStartPopOn(string popupName)
    {
        gameObject.PopUpControl(popupName, PopType.ON);
    }

    public void GameStartPopOff(string popupName)
    {
        gameObject.PopUpControl(popupName, PopType.OFF);
    }
}
