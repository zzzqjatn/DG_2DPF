using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    bool playerCheck;
    PlatformEffector2D platformObject;

    void Start()
    {
        playerCheck = false;
        platformObject = gameObject.GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.S) && playerCheck)
        {
            platformObject.rotationalOffset = 180f;
        }
        else if(Input.GetKeyUp(KeyCode.S) && playerCheck)
        {
            platformObject.rotationalOffset = 0f;
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            platformObject.rotationalOffset = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name.Equals("Player"))
        {
            playerCheck = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.name.Equals("Player"))
        {
            playerCheck = false;
        }
    }
}
