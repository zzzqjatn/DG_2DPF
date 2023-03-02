using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class batbullet : MonoBehaviour
{
    private bool IsAlive;

    private float bulletDamage;
    private float bulletSpeed;
    private Vector2 bulletDir;

    private Animator bulletAni;

    private void Start()
    {
        bulletAni = gameObject.GetComponent<Animator>();
        IsAlive = false;
    }

    private void FixedUpdate()
    {
        if (IsAlive)
        {
            move();
        }
    }

    public void setBullet(Vector2 pos_,Vector2 dir_,float speed_,float damage_)
    {
        bulletAni = gameObject.GetComponent<Animator>();

        gameObject.RectLocalPosSet(new Vector3(pos_.x, pos_.y, 0.0f));

        IsAlive = true;
        bulletDir = dir_;
        bulletSpeed = speed_;
        bulletDamage = damage_;
    }
    private void move()
    {
        gameObject.RectLocalPosAdd(bulletDir.normalized * bulletSpeed * Time.deltaTime);
    }

    private void die()
    {
        IsAlive = false;
        StartCoroutine(dieMotion());
    }

    IEnumerator dieMotion()
    {
        bulletAni.SetBool("IsHit", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            //플레이어 데미지 주기
            playerState.instance.p_state.hitDamage(bulletDamage);
            die();
        }
        else if(collision.tag.Equals("Wall") || collision.tag.Equals("Ground"))
        {
            die();
        }
    }
}
