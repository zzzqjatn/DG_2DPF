using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerState : MonoBehaviour
{
    public static playerState instance;

    public State p_state;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }

        p_state = new State();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

public class State
{
    private float maxHP;
    private float currentHP;

    private float maxDash;
    private float currentDash;
    private float cooltimeDash;

    private float attack;
    private float speed;

    public void settingState(float maxHP_,float currentHP_,float maxDash_,float currentDash_,float cooltimeDash_,float attack_,float speed_)
    {
        maxHP = maxHP_;
        currentHP = currentHP_;

        maxDash = maxDash_;
        currentDash = currentDash_;
        cooltimeDash = cooltimeDash_;

        attack = attack_;
        speed = speed_;
    }
}