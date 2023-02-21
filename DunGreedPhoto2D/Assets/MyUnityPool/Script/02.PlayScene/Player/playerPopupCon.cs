using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPopupCon : MonoBehaviour
{
    private GameObject invenPopup;
    private GameObject charPopup;
    private GameObject mapPopup;

    private bool IsInvenAct;
    private bool IsCharAct;
    private bool IsMapAct;

    void Start()
    {
        GameObject tempRoot = GFunc.FindRootObj("UIObjs");

        invenPopup = tempRoot.FindChildObj("Invnetory");

        IsInvenAct = false;
        IsCharAct = false;
        IsMapAct = false;
    }

    void Update()
    {
        PopUpControl();
    }

    private void PopUpControl()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {

        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            if (!IsInvenAct)
            {
                IsInvenAct = true;
                invenPopup.SetActive(true);
            }
            else if (IsInvenAct)
            {
                IsInvenAct = false;
                invenPopup.SetActive(false); 
            }
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            IsMapAct = true;
        }
        else IsMapAct = false;
    }

    public void invenClose_Clicked()
    {
        IsInvenAct = false;
        invenPopup.SetActive(false);
    }
}
