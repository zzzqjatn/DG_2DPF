using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHandCon : MonoBehaviour
{
    private GameObject leftHand;
    private GameObject rightHand;

    void Start()
    {
        leftHand = gameObject.FindChildObj("LeftHand");
        rightHand = gameObject.FindChildObj("RIghtHand");

        //rightHand.RectLocalPosSet(new Vector3(-7.0f,-5.0f,0.0f));
        //leftHand.RectLocalPosSet(new Vector3(7.0f, -5.0f, 0.0f));
    }

    void Update()
    {
        
    }

    public void Handcon()
    {
        
    }
}
