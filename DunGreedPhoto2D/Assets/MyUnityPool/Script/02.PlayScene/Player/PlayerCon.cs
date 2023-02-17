using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour
{
    private Animator playerAni;
    private Rigidbody2D playerRB;
    private bool isGround;
    private bool isDash;

    private int dashMaxCount;
    private int dashCurrentCount;

    private float dashReTime;
    private float dashCoolTime;

    private float dashPlayTime;
    private float dashEndTime;

    private int Speed;
    private int JumpPower;
    private float dashPower;

    private float dustTime;

    private GameObject LeftHand;

    private float dashEffectTime;
    private bool dashisLeft;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerAni = gameObject.GetComponent<Animator>();

        LeftHand = gameObject.FindChildObj("LeftHand");

        isGround = false;
        isDash = false;
        Speed = 150;
        JumpPower = 10;
        dashPower = 50;
        dashEndTime = 0.08f;
        dashPlayTime = 0.0f;
        dashMaxCount = 2;
        dashCurrentCount = 2;
        dashReTime = 0.0f;
        dashCoolTime = 0.1f;

        dustTime = 0.0f;
        dashEffectTime = 0.2f;

        dashisLeft = false;
    }

    void Update()
    {
        PlayerControl();
        HandPosition();
    }

    public void HandPosition()
    {
        LeftHand.RectLocalPosSet(new Vector3(7.0f, -5.0f, 0.0f));
    }
    public void PlayerControl()
    {
        float DirX = Input.GetAxis("Horizontal");
        float DirY = Input.GetAxis("Vertical");

        //기본 움직임 좌우 (하지만 대쉬땐 안되게)
        if (!isDash)
        {
            Vector3 tempPos = gameObject.RectLocalPos();

            gameObject.RectLocalPosSet(new Vector3(
                tempPos.x + DirX * Speed * Time.deltaTime,
                tempPos.y, 0.0f));
        }

        //대쉬 충전 쿨링
        if(dashCurrentCount != dashMaxCount)
        {
            dashReTime += Time.deltaTime;
            
            if(dashCoolTime < dashReTime)
            {
                dashReTime = 0.0f;
                dashCurrentCount += 1;
            }
        }

        //대쉬
        if(isDash)
        {   //일정 시간만큼 돌진
            dashPlayTime += Time.deltaTime;

            if (dashPlayTime > dashEffectTime)
            {
                dashEffectTime += 0.019f;

                if (gameObject.RectLocalRot().y == 0.0f) dashisLeft = false;
                else if (gameObject.RectLocalRot().y == 1.0f) dashisLeft = true;

                DashEffect.instance.setMirage(
                    new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y), dashisLeft);
            }

            if (dashPlayTime > dashEndTime)
            {
                dashPlayTime = 0.0f;
                playerRB.velocity = Vector2.zero;
                playerRB.gravityScale = 2.0f;
                isDash = false;
            }
        }

        //공격
        if (Input.GetMouseButtonDown(0))
        {
            //StartCoroutine("LeftPunch", LeftHand.RectLocalPos());

            //Rigidbody2D handRig = LeftHand.GetComponent<Rigidbody2D>();
            //handRig.velocity = Vector2.zero;

            //Vector2 Dir = new Vector2(LeftHand.RectLocalPos().x, LeftHand.RectLocalPos().y);
            ////내 마우스 위치
            //Vector2 mousePos = MouseManager.ScreenMatchSizeMousePos();
            //// 내 마우스 위치 - 나 위치
            //Dir = mousePos - Dir;

            //handRig.AddForce(new Vector2(Dir.x, Dir.y).normalized * 50, ForceMode2D.Impulse);
        }

        if (Input.GetMouseButtonDown(1) && dashCurrentCount > 0)
        {
            dashEffectTime = 0.02f;
            dashPlayTime = 0.0f;
            dashCurrentCount -= 1;
            playerRB.velocity = Vector2.zero;
            Vector2 Dir = new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);
            //내 마우스 위치
            Vector2 mousePos = MouseManager.ScreenMatchSizeMousePos();
            // 내 마우스 위치 - 나 위치
            Dir = mousePos - Dir;

            playerRB.gravityScale = 0.0f;
            if (isGround == true && Dir.normalized.y <= 0.0f)
            {
                Vector2 tmp = new Vector2(Dir.x, Dir.y).normalized;
                playerRB.AddForce(new Vector2(tmp.x * dashPower, 0), ForceMode2D.Impulse);
                isDash = true;
            }
            else
            {
                playerRB.AddForce(new Vector2(Dir.x, Dir.y).normalized * dashPower, ForceMode2D.Impulse);
                isDash = true;
                isGround = false;
            }
        }

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            playerRB.velocity = Vector2.zero;
            playerRB.AddForce(new Vector2(0,1) * JumpPower, ForceMode2D.Impulse);
        }
        else if(Input.GetKeyUp(KeyCode.Space) && 0 < playerRB.velocity.y)
        {
            //점프 일때 짧게 눌렀다 때면 뛰는 힘에 제제를 가해
            //상대적으로 일반점프가 긴 점프 짧게 누르며 짧은 점프가 된다. 
            playerRB.velocity = playerRB.velocity * 0.5f;
        }

        //캐릭터 좌우 회전
        if(DirX < 0.0f)
        {
            gameObject.Rect().localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (DirX > 0.0f)
        {
            gameObject.Rect().localRotation = Quaternion.Euler(0, 0, 0);
        }

        //캐릭터 모션
        playerAni.SetBool("IsGround", isGround);

        if (isGround == true)
        {
            //플레이어 달리기 설정
            if (DirX != 0)
            {
                playerAni.SetBool("IsRun", true);

                dustTime += Time.deltaTime;
                if (dustTime > 0.6f)
                {
                    dustTime = 0.0f;
                    //왼쪽
                    if (gameObject.RectLocalRot().y == 1.0f)
                    {
                        MoveEffect.instance.setDust(
                            new Vector2(gameObject.RectLocalPos().x + 7.0f, gameObject.RectLocalPos().y - 5.0f), true);
                    }
                    else if(gameObject.RectLocalRot().y == 0.0f)
                    {
                        MoveEffect.instance.setDust(
                            new Vector2(gameObject.RectLocalPos().x - 7.0f, gameObject.RectLocalPos().y - 5.0f), false);
                    }
                }
            }
            else
            {
                playerAni.SetBool("IsRun", false);
            }
        }
    }

    IEnumerator LeftPunch(Vector3 firstPos)
    {
        bool isRight = false;
        Rigidbody2D handRig = LeftHand.GetComponent<Rigidbody2D>();
        handRig.velocity = Vector2.zero;
        Vector2 Dir = new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);
        //내 마우스 위치
        Vector2 mousePos = MouseManager.ScreenMatchSizeMousePos();
        // 내 마우스 위치 - 나 위치
        Dir = mousePos - Dir;

        if (gameObject.RectLocalRot().y == 0) { isRight = true; }

        handRig.AddForce(new Vector2(Dir.x, Dir.y).normalized * 50, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(0.05f);

        //handRig.velocity = Vector2.zero;

        //Vector2 tempruslt = new Vector2(7.0f - firstPos.x,-5.0f - firstPos.y);

        //while(true)
        //{
        //    handRig.AddForce(new Vector2(tempruslt.x, tempruslt.y).normalized * 1, ForceMode2D.Force);

        //    if(isRight == false)
        //    {
        //        if (LeftHand.RectLocalPos().x < 7.0f)
        //        {
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        if (LeftHand.RectLocalPos().x > 7.0f)
        //        {
        //            break;
        //        }
        //    }
        //}

        //LeftHand.RectLocalPosSet(new Vector3(7.0f, -5.0f, 0.0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name.Equals("wall"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.name.Equals("wall"))
        {
            isGround = false;
        }
    }
}
