using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class TalkData
{
    public TalkData()
    {
        main = new List<string>();
        select = new List<SelectQusetion>();
    }

    public List<string> main;
    public List<SelectQusetion> select;

    public void MainAdd(string main_)
    {
        main.Add(main_);
    }
    
    public void QuestionAdd(SelectQusetion Quest_)
    {
        select.Add(Quest_);
    }

    public void DeletAll()
    {
        main.Clear();
        select.Clear();
    }

    public bool CheckData()
    {
        if (main.Count <= 0) return false;
        return true;
    }
}

[System.Serializable]
public class SelectQusetion
{
    public int incount;
    public int index;
    public string talk;
}

public class TalkboxCon : MonoBehaviour
{
    private TalkData talkDatas;
    private bool IsLoad;
    private int nowTalkCount;

    private GameObject selectboxPrefab;
    private List<GameObject> selectboxList;
    private List<GameObject> selectboxTxtList;

    private GameObject qbox;
    private GameObject mainTxt;


    void Start()
    {
        talkDatas = new TalkData();
        IsLoad = false;
        nowTalkCount = 0;

        mainTxt = gameObject.FindChildObj("boxTxt");
        selectboxPrefab = gameObject.FindChildObj("SelectText");
        qbox = gameObject.FindChildObj("qBox");

        selectboxList = new List<GameObject>();
        selectboxTxtList = new List<GameObject>();

        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(selectboxPrefab, selectboxPrefab.transform.parent);
            temp.SetActive(false);
            GameObject tempChild = temp.transform.GetChild(0).gameObject;
            
            selectboxTxtList.Add(tempChild);
            selectboxList.Add(temp);
        }
        selectboxPrefab.SetActive(false);
    }

    void Update()
    {

    }

    //private void TESTtempSaveData()
    //{
    //    talkDatas.MainAdd("상점에 오신것을 환영합니다.");

    //    SelectQusetion temp = new SelectQusetion();
    //    temp.index = 0;
    //    temp.incount = 0;
    //    temp.talk = "상점";
    //    talkDatas.QuestionAdd(temp);

    //    temp = new SelectQusetion();
    //    temp.index = 0;
    //    temp.incount = 1;
    //    temp.talk = "확인1";
    //    talkDatas.QuestionAdd(temp);

    //    temp = new SelectQusetion();
    //    temp.index = 0;
    //    temp.incount = 2;
    //    temp.talk = "나가기";
    //    talkDatas.QuestionAdd(temp);


    //    string DirectoryPath = Application.dataPath + "/Save/Talk";

    //    //폴더 체크
    //    if (Directory.Exists(DirectoryPath) == false)
    //    {
    //        Directory.CreateDirectory(DirectoryPath);
    //    }
        
    //    string data = JsonUtility.ToJson(talkDatas, true);
    //    File.WriteAllText(DirectoryPath + "/shop", data);
    //    print(data);
    //}

    public void LoadTalkData(string npcName)
    {
        AllClear();
        string DirectoryPath = Application.dataPath + "/Save/Talk";
        string data = File.ReadAllText(DirectoryPath + "/" + npcName);
        talkDatas = JsonUtility.FromJson<TalkData>(data);
        IsLoad = true;
    }

    public void AllClear()
    {
        talkDatas.DeletAll();
        nowTalkCount = 0;
        IsLoad = false;
    }

    public void Talksett1(int incount_)
    {
        StartCoroutine(Typing(incount_,talkDatas.main[incount_]));
    }

    public void SetTalkBox()
    {
        if(IsLoad)
        {
            Talksett1(nowTalkCount);

            //if(LeftClickManager.instance.CLICKcheck())
            //{
            //    nowTalkCount++;
            //}
        }
    }

    IEnumerator Typing(int inCount,string text)
    {
        string tempTyping = "";

        foreach (char char_ in text.ToCharArray())
        {
            tempTyping += char_;
            mainTxt.SetTxt(tempTyping);
            yield return new WaitForSeconds(0.1f);
        }

        qbox.SetActive(true);
        float starty = 0;
        int CountingY = 0;

        for (int i = 0; i < talkDatas.select.Count; i++)
        {
            if (talkDatas.select[i].incount == inCount)
            {
                for (int j = 0; j < selectboxList.Count; j++)
                {
                    if (j == talkDatas.select[i].index)
                    {
                        CountingY++;
                        selectboxList[j].SetActive(true);
                        selectboxTxtList[j].SetTxt(talkDatas.select[i].talk);
                        selectboxList[j].RectLocalPosSet(new Vector3(
                            selectboxList[j].RectLocalPos().x,
                            starty + j * (selectboxList[j].RectSize().y + 2) * (-1), 0.0f));
                    }
                    else { continue; }
                }
            }
            else { continue; }
        }
        qbox.RectSizeSet(new Vector2(70, CountingY * 8 + 16));
        qbox.RectLocalPosSet(new Vector3(
            qbox.RectLocalPos().x,
            132 - (132 - qbox.RectSize().y) - (132 / 2) + 2 - (qbox.RectSize().y / 2), 0.0f));

    }

    //    IEnumerator Typing22(string text)
    //    {
    //        IsTyping = false;

    //        string tempTyping = "";

    //        foreach (char char_ in text.ToCharArray())
    //        {
    //            tempTyping += char_;
    //            mainTxt.SetTxt(tempTyping);
    //            yield return new WaitForSeconds(0.1f);
    //        }
    //        IsTyping = true;
    //    }

    public void talkconOut()
    {
        qbox.SetActive(false);
    }
}
