using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deleteButton : MonoBehaviour
{
    private bool buttonDown;
    private GameObject DeleteGuage;
    private float DeletePoint;

    private bool isDelete;

    public bool GetisDelete() { return isDelete; }
    public void SetisDelete(bool isDelete_) { isDelete = isDelete_; }

    void Start()
    {
        DeleteGuage = gameObject.FindChildObj("DeleteGauge");
        DeletePoint = DeleteGuage.GetComponent<Image>().fillAmount;
        isDelete = false;
    }

    void Update()
    {
        if(buttonDown == true)
        {
            FilledDeleteGuage();
        }
    }

    public void PointerDown()
    {
        buttonDown = true;
    }
    public void PointerUp()
    {
        buttonDown = false;
        DeleteGuage.GetComponent<Image>().fillAmount = 0.0f;
    }

    public void FilledDeleteGuage()
    {
        DeletePoint = DeleteGuage.GetComponent<Image>().fillAmount;
        DeletePoint += 0.004f;

        if(DeletePoint >= 1.0f)
        {
            isDelete = true;
            PointerUp();
        }
        else
        {
            DeleteGuage.GetComponent<Image>().fillAmount = DeletePoint;
        }
    }
}
