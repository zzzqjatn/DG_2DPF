using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInfo : MonoBehaviour
{
    private bool ActiveKeyAct;
    private GameObject ActiveKey;

    private GameObject ShopBaseUI;
    private GameObject playerInven;

    private GameObject ShopNPC;

    void Start()
    {
        ActiveKeyAct = false;
        ActiveKey = GFunc.FindRootObj("GameObjs").FindChildObj("ActiveKey");
        ShopNPC = gameObject.transform.parent.gameObject;

        ShopBaseUI = GFunc.FindRootObj("UIObjs").FindChildObj("Shop");
        playerInven = GFunc.FindRootObj("UIObjs").FindChildObj("Invnetory");
    }

    void Update()
    {
        shop();
        shopkeyInput();
    }

    private void shopkeyInput()
    {
        if(ActiveKeyAct)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                if (ShopBaseUI.activeSelf == false)
                {
                    ShopBaseUI.SetActive(true);
                    playerInven.SetActive(true);
                }
                else if(ShopBaseUI.activeSelf == true)
                {
                    ShopBaseUI.SetActive(false);
                    playerInven.SetActive(false);
                }
            }
        }
        if (!ActiveKeyAct)
        {
            ShopBaseUI.SetActive(false);
            playerInven.SetActive(false);
        }
    }

    private void shop()
    {
        if(ActiveKeyAct)
        {
            if(ActiveKey.activeSelf == false)
            {
                ActiveKey.SetActive(true);
                ActiveKey.RectLocalPosSet(new Vector3(ShopNPC.RectLocalPos().x, ShopNPC.RectLocalPos().y + 18, 0.0f));
            }
        }
        else if(!ActiveKeyAct)
        {
            ActiveKey.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name.Equals("Player"))
        {
            ActiveKeyAct = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name.Equals("Player"))
        {
            ActiveKeyAct = false;
        }
    }
}
