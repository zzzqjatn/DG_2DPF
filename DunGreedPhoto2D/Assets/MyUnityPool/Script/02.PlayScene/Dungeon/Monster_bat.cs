using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_bat : MonoBehaviour
{
    private MonsterFinder bat_finder;
    private Animator bat_Ani;

    void Start()
    {
        bat_finder = gameObject.FindChildObj("Finder").GetComponent<MonsterFinder>();
        bat_Ani = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if(bat_finder.FindPlayer == true)
        {
            bat_Ani.SetBool("IsAttack", true);

            Vector2 dir = bat_finder.P_position - new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);

            if(dir.normalized.x < 0f)
            {
                gameObject.RectLocalRotSet(Quaternion.Euler(0, 180, 0));
            }
            else if (dir.normalized.x > 0f)
            {
                gameObject.RectLocalRotSet(Quaternion.Euler(0, 0, 0));
            }
        }
        else if(bat_finder.FindPlayer == false)
        {
            bat_Ani.SetBool("IsAttack", false);
        }
    }
}
