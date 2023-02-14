using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private bool isJump;
    //private bool isDash;

    private int Speed;
    private int JumpPower;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();

        isJump = false;
        //isDash = false;
        Speed = 5;
        JumpPower = 10;
    }

    void Update()
    {
        PlayerControl();
    }

    public void PlayerControl()
    {
        float DirX = Input.GetAxis("Horizontal");
        float DirY = Input.GetAxis("Vertical");

        Vector3 tempPos = gameObject.transform.position;

        gameObject.transform.position = new Vector3(
            tempPos.x + DirX * Speed * Time.deltaTime,
            tempPos.y, 0.0f);


        if(Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            playerRB.AddForce(new Vector2(0,1) * JumpPower, ForceMode2D.Impulse);
            isJump = true;
        }

        //Ä³¸¯ÅÍ È¸Àü
        if(DirX < 0.0f)
        {
            gameObject.Rect().localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (DirX > 0.0f)
        {
            gameObject.Rect().localRotation = Quaternion.Euler(0, 0, 0);
        }


        //if(Input.GetKeyDown(KeyCode.W))
        //{

        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{

        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{

        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{

        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name.Equals("wall"))
        {
            Debug.Log("¶¥¿¡ ´êÀ½");
            isJump = false;
        }
    }
}
