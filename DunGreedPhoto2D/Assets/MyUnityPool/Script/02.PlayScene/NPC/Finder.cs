using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    public bool ActiveKeyAct;

    void Start()
    {
        ActiveKeyAct = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name.Equals("Player"))
        {
            ActiveKeyAct = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name.Equals("Player"))
        {
            ActiveKeyAct = false;
        }
    }
}
