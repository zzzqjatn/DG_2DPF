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
            //������ �ִٸ�
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
            firstTxt = "������ ����";
            SecondTxt = "";
        }
        else
        {               //<line-height=10> </line-height> //����ũ�� ����
            firstTxt = $"<�÷��� �ð�>\n<line-height=10>{data.playTime / 60}H {data.playTime % 60}m\n" +
                     $"<������ ��></line-height>\n<line-height=10>{data.dungeonFloor}F\n" +
                     $"<������></line-height>\n{data.money}G";

            switch(data.mapPos)
            {
                case 0:
                    SecondTxt = $"<���� ��ġ>\n<color=green>����</color>";
                    break;
                default:
                    SecondTxt = $"<���� ��ġ>\n<color=red>����</color> {data.mapPos}F";
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
        //������ �ִٸ�
        if (DataManager.instance.SlotDataCheck(num) == true)
        {
            //���� ������ �����ֱ�
            DataManager.instance.PlaySlotNum = num;

            //�÷��̾�����
            LoadingSceneManager.LoadScene("02.PlayScene");
        }
        else
        {
            // �⺻ ���������
            SlotData temp = new SlotData();
            temp.Setting(num, 0, 0, 100, 0);
            DataManager.instance.slotData = temp;
            DataManager.instance.SlotSaveData();
            DataManager.instance.SlotDataClear();

            DataManager.instance.PlaySlotNum = num;

            //�÷��̾�����
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
