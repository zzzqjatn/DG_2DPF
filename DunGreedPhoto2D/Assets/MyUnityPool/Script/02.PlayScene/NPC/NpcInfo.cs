using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInfo : MonoBehaviour
{
    private GameObject ActiveKey;

    private GameObject ShopBaseUI;
    private GameObject playerInven;
    private GameObject talkBox;
    private TalkboxCon talkCon;

    private Finder find_player;

    private bool playerLeft;

    void Start()
    {
        ActiveKey = GFunc.FindRootObj("GameObjs").FindChildObj("ActiveKey");

        GameObject tmpRoot = GFunc.FindRootObj("UIObjs");
        ShopBaseUI = tmpRoot.FindChildObj("Shop");
        playerInven = tmpRoot.FindChildObj("Invnetory");
        talkBox = tmpRoot.FindChildObj("TalkBox");

        talkCon = tmpRoot.GetComponent<TalkboxCon>();

        find_player = gameObject.FindChildObj("Finder").GetComponent<Finder>();

        playerLeft = false;
    }

    void Update()
    {
        ActivekeyShow();
        shopkeyInput();
    }

    private void shopkeyInput()
    {
        if(find_player.ActiveKeyAct)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                if (talkBox.activeSelf == false)
                {
                    talkBox.SetActive(true);
                    talkCon.LoadTalkData("shop");
                    talkCon.Talksett1(0);

                    //ShopBaseUI.SetActive(true);
                    //playerInven.SetActive(true);
                }
                else if(talkBox.activeSelf == true)
                {
                    talkCon.talkconOut();
                    talkBox.SetActive(false);
                    //ShopBaseUI.SetActive(false);
                    //playerInven.SetActive(false);
                }
            }
            playerLeft = false;
        }
        if (!find_player.ActiveKeyAct && !playerLeft)
        {
            talkCon.talkconOut();
            talkBox.SetActive(false);
            //ShopBaseUI.SetActive(false);
            //playerInven.SetActive(false);
            playerLeft = true;
        }
    }

    private void ActivekeyShow()
    {
        if(find_player.ActiveKeyAct)
        {
            if(ActiveKey.activeSelf == false)
            {
                ActiveKey.SetActive(true);
                ActiveKey.RectLocalPosSet(new Vector3(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y + 20, 0.0f));
            }
        }
        else if(!find_player.ActiveKeyAct && playerLeft)
        {
            ActiveKey.SetActive(false);
        }
    }

    private void temp()
    {
        if (ShopBaseUI.activeSelf == false)
        {
            ShopBaseUI.SetActive(true);
            playerInven.SetActive(true);
        }
        else if (ShopBaseUI.activeSelf == true)
        {
            ShopBaseUI.SetActive(false);
            playerInven.SetActive(false);
        }
    }
}
