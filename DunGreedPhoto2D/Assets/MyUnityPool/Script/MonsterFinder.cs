using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFinder : MonoBehaviour
{
    public bool FindPlayer;
    public Vector2 P_position;

    void Start()
    {
        FindPlayer = false;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name.Equals("Player"))
        {
            FindPlayer = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.name.Equals("Player"))
        {
            P_position = collision.transform.gameObject.RectLocalPos();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name.Equals("Player"))
        {
            FindPlayer = false;
        }
    }
}
