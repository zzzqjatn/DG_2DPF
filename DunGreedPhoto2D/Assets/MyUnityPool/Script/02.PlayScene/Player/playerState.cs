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
    public int level;

    public float maxHP;
    public float currentHP;

    public float maxDash;
    public float currentDash;
    public float cooltimeDash;

    public float attack;
    public float speed;

    public void levelUp()
    {
        level += 1;

        maxHP = 80 * level;
        currentHP = maxHP;

        attack = 50 * level;
    }

    public void settingState(int level_,float maxHP_,float currentHP_,float maxDash_,float currentDash_,float cooltimeDash_,float attack_,float speed_)
    {
        level = level_;
        maxHP = maxHP_;
        currentHP = currentHP_;

        maxDash = maxDash_;
        currentDash = currentDash_;
        cooltimeDash = cooltimeDash_;

        attack = attack_;
        speed = speed_;
    }

    public void hitDamage(float damage_)
    {
        currentHP -= damage_;

        if (currentHP < 0) currentHP = 0;

        playerUI.instance.rebootbar();
    }
}