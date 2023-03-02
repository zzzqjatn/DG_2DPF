using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitting : MonoBehaviour
{
    public static hitting instance;
    public bool IsHit;
    
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
        IsHit = false;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("enemy"))
        {
            collision.GetComponent<monsterState>().hitDamage(playerState.instance.p_state.attack);
        }
    }

    IEnumerator Off()
    {
        yield return new WaitForSeconds(0.2f);
        IsHit = false;
    }
}
