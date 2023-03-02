using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Monster_bat : MonoBehaviour
{
    private MonsterFinder bat_finder;
    private Animator bat_Ani;

    private float currentCoolTime;
    private float maxCoolTime;

    private float currentWaitTime;
    private float maxWaitTime;

    public Vector2 respownPosition;
    public Vector2 movePoint;

    private Vector2 moveDir;

    private bulletObjPool bulletPool;

    private GameObject lifebar;

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
        bat_finder = gameObject.FindChildObj("Finder").GetComponent<MonsterFinder>();
        bat_Ani = gameObject.GetComponent<Animator>();
        bulletPool = GFunc.FindRootObj("GameObjs").FindChildObj("bulletObjs").GetComponent<bulletObjPool>();
        lifebar = gameObject.FindChildObj("HpFront");

        respownPosition = pos;
        gameObject.RectLocalPosSet(new Vector3(pos.x, pos.y, 0.0f));

        state = State.Idle;
        ChangeState(state);
    }

    void Start()
    {
        bat_finder = gameObject.FindChildObj("Finder").GetComponent<MonsterFinder>();
        bat_Ani = gameObject.GetComponent<Animator>();
        bulletPool = GFunc.FindRootObj("GameObjs").FindChildObj("bulletObjs").GetComponent<bulletObjPool>();
        lifebar = gameObject.FindChildObj("HpFront");

        maxCoolTime = 1.0f;
        currentCoolTime = 0.0f;

        maxWaitTime = 1.0f;
        currentWaitTime = maxWaitTime;

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

    //------------------ Idle -------------                             
    private void IdleEnter()
    {
        bat_Ani.SetBool("IsAttack", false);

        maxWaitTime = Random.Range(1.0f,5.0f);
        currentWaitTime = maxWaitTime;
        currentCoolTime = 3;
    }
    private void IdleUpdate()
    {
        //공격 쿨타임
        if(currentCoolTime >= 0)
        {
            currentCoolTime -= Time.deltaTime;
        }
        else if(bat_finder.FindPlayer == true && currentCoolTime < 0)
        {
            state = State.Attack;
            ChangeState(state);
        }

        if(currentWaitTime >= 0)
        {
            currentWaitTime -= Time.deltaTime;
        }
        else if(currentWaitTime < 0)
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
    //------------------ Idle -------------

    //------------------ Move -------------
    private void MoveEnter()
    {
        bat_Ani.SetBool("IsAttack",false);

        movePoint = new Vector2(
            Random.Range(respownPosition.x - 10f, respownPosition.x + 10f),
            Random.Range(respownPosition.y - 10f, respownPosition.y + 10f));

        moveDir = movePoint - new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);

        if (moveDir.normalized.x < 0f)
        {
            gameObject.RectLocalRotSet(Quaternion.Euler(0, 180, 0));
            lifebar.RectLocalRotSet(Quaternion.Euler(0, 180, 0));
        }
        else if (moveDir.normalized.x > 0f)
        {
            gameObject.RectLocalRotSet(Quaternion.Euler(0, 0, 0));
            lifebar.RectLocalRotSet(Quaternion.Euler(0, 0, 0));
        }
        currentCoolTime = 3;
    }
    private void MoveUpdate()
    {
        //거리비교 일정 거리 되면 idle
        float Distance = Vector2.Distance(gameObject.RectLocalPos(), movePoint);
        
        if (Distance <= 0.5f)
        {
            state = State.Idle;
            ChangeState(state);
        }

        if (currentCoolTime >= 0)
        {
            currentCoolTime -= Time.deltaTime;
        }
        else if (bat_finder.FindPlayer == true && currentCoolTime < 0)
        {
            state = State.Attack;
            ChangeState(state);
        }
    }
    private void MovePhysicalUpdate()
    {
        gameObject.RectLocalPosAdd(moveDir.normalized * 10 * Time.deltaTime);
    }
    private void MoveExit()
    {

    }
    //------------------ Move -------------

    //------------------ Attack -------------
    private void AttackEnter()
    {
        bat_Ani.SetBool("IsAttack",true);

        Vector2 dir = bat_finder.P_position - new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);
        if (dir.normalized.x < 0f)
        {
            gameObject.RectLocalRotSet(Quaternion.Euler(0, 180, 0));
            lifebar.RectLocalRotSet(Quaternion.Euler(0, 180, 0));
        }
        else if (dir.normalized.x > 0f)
        {
            gameObject.RectLocalRotSet(Quaternion.Euler(0, 0, 0));
            lifebar.RectLocalRotSet(Quaternion.Euler(0, 0, 0));
        }
        StartCoroutine(bulletCoroutin(gameObject.RectLocalPos(), dir, 20, 30));
    }

    IEnumerator bulletCoroutin(Vector2 pos_,Vector2 dir_,float speed_,float damage_)
    {
        yield return new WaitForSeconds(0.6f);
        bulletPool.bulletFire(pos_, dir_, speed_, damage_);
    }

    private void AttackUpdate()
    {
        if (bat_Ani.GetCurrentAnimatorStateInfo(0).IsName("batAttack") &&
          bat_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        {
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
    //------------------ Attack -------------

    //------------------ Die -------------
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
    //------------------ Die -------------
}
