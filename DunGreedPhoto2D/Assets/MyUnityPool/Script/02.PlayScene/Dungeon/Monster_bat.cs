using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Monster_bat : MonoBehaviour
{
    private MonsterFinder bat_finder;
    private Animator bat_Ani;

    private float currenCoolTime;
    private float maxCoolTime;

    private float currenWaitTime;
    private float maxWaitTime;

    public Vector2 respownPosition;
    public Vector2 movePoint;

    public enum State
    {
        Idle,
        Move,
        Attack,
        Die
    }

    public State state = State.Idle;

    public void SettingBat(Vector2 pos)
    {
        respownPosition = pos;
        gameObject.RectLocalPosSet(new Vector3(pos.x, pos.y, 0.0f));
    }

    void Start()
    {
        bat_finder = gameObject.FindChildObj("Finder").GetComponent<MonsterFinder>();
        bat_Ani = gameObject.GetComponent<Animator>();

        maxCoolTime = 1.0f;
        currenCoolTime = 0.0f;

        maxWaitTime = 1.0f;
        currenWaitTime = maxWaitTime;

        respownPosition = new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);

        ChangeState(state);
    }

    void Update()
    {
        switch(state)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Move:
                MoveUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;
            case State.Die:
                DieUpdate();
                break;
        }
    }

    void LateUpdate()
    {
        switch (state)
        {
            case State.Idle:
                IdlePhysicalUpdate();
                break;
            case State.Move:
                MovePhysicalUpdate();
                break;
            case State.Attack:
                AttackPhysicalUpdate();
                break;
            case State.Die:
                DiePhysicalUpdate();
                break;
        }
    }

    private void ChangeState(State state)
    {
        switch(this.state)
        {
            case State.Idle:
                IdleExit();
                break;
            case State.Move:
                MoveExit();
                break;
            case State.Attack:
                AttackExit();
                break;
            case State.Die:
                DieExit();
                break;
        }

        this.state = state;

        switch (state)
        {
            case State.Idle:
                IdleEnter();
                break;
            case State.Move:
                MoveEnter();
                break;
            case State.Attack:
                AttackEnter();
                break;
            case State.Die:
                DieEnter();
                break;
        }
    }
    //------------------------------------

    private void IdleEnter()
    {
        bat_Ani.SetBool("IsAttack", false);

        maxWaitTime = Random.Range(0.5f,2.0f);
        currenWaitTime = maxWaitTime;
    }
    private void IdleUpdate()
    {
        if(currenCoolTime >= 0)
        {
            currenCoolTime -= Time.deltaTime;
        }
        else if(bat_finder.FindPlayer == true && currenCoolTime < 0)
        {
            state = State.Attack;
            ChangeState(state);
        }

        if(currenWaitTime >= 0)
        {
            currenWaitTime -= Time.deltaTime;
        }
        else if(currenWaitTime < 0)
        {
            state = State.Move;
            ChangeState(state);
        }
    }
    private void IdlePhysicalUpdate()
    {

    }
    private void IdleExit()
    {

    }
    //------------------------------------

    private void MoveEnter()
    {
        bat_Ani.SetBool("IsAttack",false);

        movePoint = new Vector2(
            Random.Range(respownPosition.x - 5f, respownPosition.x + 5f),
            Random.Range(respownPosition.y - 5f, respownPosition.y + 5f));

        movePoint -= respownPosition;

        if (movePoint.normalized.x < 0f)
        {
            gameObject.RectLocalRotSet(Quaternion.Euler(0, 180, 0));
        }
        else if (movePoint.normalized.x > 0f)
        {
            gameObject.RectLocalRotSet(Quaternion.Euler(0, 0, 0));
        }
    }
    private void MoveUpdate()
    {
        //거리비교 일정 거리 되면 idle
        if (Vector2.Distance(gameObject.RectLocalPos(), movePoint) <= 1f)
        {
            state = State.Idle;
            ChangeState(state);
        }

        if (currenCoolTime >= 0)
        {
            currenCoolTime -= Time.deltaTime;
        }
        else if (bat_finder.FindPlayer == true && currenCoolTime < 0)
        {
            state = State.Attack;
            ChangeState(state);
        }
    }
    private void MovePhysicalUpdate()
    {
        gameObject.transform.Translate(movePoint.normalized * 2 * Time.deltaTime);
    }
    private void MoveExit()
    {

    }
    //------------------------------------

    private void AttackEnter()
    {
        bat_Ani.SetBool("IsAttack",true);

        Vector2 dir = bat_finder.P_position - new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);
        if (dir.normalized.x < 0f)
        {
            gameObject.RectLocalRotSet(Quaternion.Euler(0, 180, 0));
        }
        else if (dir.normalized.x > 0f)
        {
            gameObject.RectLocalRotSet(Quaternion.Euler(0, 0, 0));
        }
    }
    private void AttackUpdate()
    {
        if (bat_Ani.GetCurrentAnimatorStateInfo(0).IsName("IsAttack") &&
                  bat_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            currenCoolTime = maxCoolTime;
            bat_Ani.SetBool("IsAttack", false);

            state = State.Idle;
            ChangeState(state);
        }
    }
    private void AttackPhysicalUpdate()
    {

    }
    private void AttackExit()
    {

    }
    //------------------------------------

    private void DieEnter()
    {

    }
    private void DieUpdate()
    {

    }
    private void DiePhysicalUpdate()
    {

    }
    private void DieExit()
    {

    }
    //------------------------------------
}
