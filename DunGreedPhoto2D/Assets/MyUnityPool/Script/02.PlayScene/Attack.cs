using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static Attack instance;
    private Animator attackAni;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }

        attackAni = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {

        if(attackAni.GetCurrentAnimatorStateInfo(0).IsName("SwingAni") && 
            attackAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            End();
        }
    }

    public void End()
    {
        gameObject.SetActive(false);
    }
}
