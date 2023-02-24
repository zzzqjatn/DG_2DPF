using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setAni : MonoBehaviour
{
    private Animator motionAni;
    void Start()
    {
        motionAni = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public void onEndEvent()
    {
        motionAni.SetBool("IsAttack", false);
    }
}
