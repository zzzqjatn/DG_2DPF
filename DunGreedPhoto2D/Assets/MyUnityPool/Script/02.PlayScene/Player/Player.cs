using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAni;
    private Rigidbody2D playerRB;
    private Collider2D playerLandCollider;

    private bool isGround;
    private bool isDash;
    private bool isJump;

    private int dashCurrentCount;
    private int dashMaxCount;

    private float dashCoolTimeCurrent;
    private float dashCoolTimeMax;

    private float dashPlayTime;
    private float dashEndTime;

    private int Speed;
    private int JumpPower;
    private float dashPower;

    private float dustEventTime;
    private float dashEventTime;

    private GameObject LeftHand;

    private bool isRight;
    private bool isLeft;
    private bool isUp;
    private bool isDown;
    private bool isSpaceUp;
    private bool isSpaceDown;
    private bool isRun;
    private bool isRightClick;

    private Vector2 Dir;

    private bool isFalling;
    private RaycastHit2D raycastHit;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerAni = gameObject.GetComponent<Animator>();
        playerLandCollider = gameObject.GetComponent<Collider2D>();

        isGround = false;
        isDash = false;
        isJump = false;

        Speed = 5;
        JumpPower = 10;
        dashPower = 50;

        dashMaxCount = 2;
        dashCurrentCount = 2;

        dashEndTime = 0.08f;
        dashPlayTime = 0.0f;

        dashCoolTimeMax = 0.1f;
        dashCoolTimeCurrent = 0.0f;

        dustEventTime = 0.0f;
        dashEventTime = 0.2f;

        LeftHand = gameObject.FindChildObj("LeftHand");

        isRight = false;
        isLeft = false;
        isUp = false;
        isDown = false;
        isSpaceDown = false;
        isSpaceUp = false;
        isRun = false;
        isRightClick = false;
        isFalling = false;

        Dir = new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);
    }

    private void FixedUpdate()
    {
        playerMoves();
    }

    void Update()
    {
        playerCons();
    }

    public void playerCons()
    {
        playerKeyControl();
        Player_Xfilp();
        Player_Running();
        Player_DashRunnig();
        Player_AniCon();
        //LandCheckCollider();

        HandPosition();
    }

    public void playerMoves()
    {
        //걷기
        Player_Walk();

        //점프
        Player_LongJump();
        Player_ShortJump();

        //대쉬
        Player_Dash();
    }

    public void LandCheckCollider()
    {
            raycastHit = Physics2D.BoxCast(
                playerLandCollider.bounds.center, playerLandCollider.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Wall"));
    }

    public void playerKeyControl()
    {
        //방향키
        if (Input.GetKey(KeyCode.A))
        {
            if(isDash == false) isLeft = true;
            if(isGround == true && isDash == false) isRun = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (isDash == false) isRight = true;
            if (isGround == true && isDash == false) isRun = true;
        }
        else isRun = false;

        if (Input.GetKey(KeyCode.W))
        {
            isUp = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isDown = true;
        }

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            isSpaceDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && 0 < playerRB.velocity.y)
        {
            isSpaceUp = true;
        }

        //대쉬
        if (Input.GetMouseButtonDown(1) && dashCurrentCount > 0)
        {
            isRightClick = true;
        }
    }

    public void Player_Walk()
    {
        if (isDash == false)
        {
            if(isLeft == true)
            {
                isLeft = false;
                playerRB.velocity = new Vector2(-Speed, playerRB.velocity.y);
            }
            else if (isRight == true)
            {
                isRight = false;
                playerRB.velocity = new Vector2(Speed, playerRB.velocity.y);
            }
            else playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }
    }

    public void Player_Dash()
    {
        if(isRightClick == true)
        {
            isRightClick = false;

            dashEventTime = 0.02f;
            dashPlayTime = 0.0f;
            dashCurrentCount -= 1;
            playerRB.velocity = Vector2.zero;
            playerRB.gravityScale = 0.0f;

            playerRB.AddForce(new Vector2(Dir.x, Dir.y).normalized * dashPower, ForceMode2D.Impulse);
            isDash = true;
            isJump = true;
            isGround = false;
        }
    }

    public void Player_DashRunnig()
    {
        //대쉬 쿨타입 부분
        if (dashCurrentCount != dashMaxCount)
        {
            dashCoolTimeCurrent += Time.deltaTime;

            if (dashCoolTimeMax < dashCoolTimeCurrent)
            {
                dashCoolTimeCurrent = 0.0f;
                dashCurrentCount += 1;
            }
        }

        //대쉬 부분
        if (isDash)
        {   //일정 시간만큼 돌진
            dashPlayTime += Time.deltaTime;

            //대쉬 잔상 발생
            if (dashPlayTime > dashEventTime)
            {
                dashEventTime += 0.019f;

                if (gameObject.RectLocalRot().y == 0.0f)
                {
                    DashEffect.instance.setMirage(new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y), false);
                }
                else if (gameObject.RectLocalRot().y == 1.0f)
                {
                    DashEffect.instance.setMirage(new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y), true);
                }
            }

            if (dashPlayTime > dashEndTime)
            {
                dashPlayTime = 0.0f;
                playerRB.velocity = Vector2.zero;
                playerRB.gravityScale = 2.0f;
                isDash = false;
                playerAni.SetBool("IsDash", false);
            }
            else
            {
                playerAni.SetBool("IsDash", true);
            }
        }
    }

    public void Player_Running()
    {
        //플레이어 달리기 설정
        if (isRun == true && isJump == false)
        {
            dustEventTime += Time.deltaTime;
            if (dustEventTime > 0.6f)
            {
                dustEventTime = 0.0f;
                //왼쪽
                if (gameObject.RectLocalRot().y == 1.0f)
                {
                    MirageEffect.instance.setDust(
                        new Vector2(gameObject.RectLocalPos().x + 7.0f, gameObject.RectLocalPos().y - 5.0f), true);
                }
                else if (gameObject.RectLocalRot().y == 0.0f)
                {
                    MirageEffect.instance.setDust(
                        new Vector2(gameObject.RectLocalPos().x - 7.0f, gameObject.RectLocalPos().y - 5.0f), false);
                }
            }
        }
    }

    public void Player_LongJump()
    {
        if (isSpaceDown == true)
        {
            isSpaceDown = false;
            isJump = true;
            playerRB.AddForce(Vector2.up * JumpPower,ForceMode2D.Impulse);
        }
    }

    public void Player_ShortJump()
    {
        //점프 일때 짧게 눌렀다 때면 뛰는 힘에 제제를 가해
        //상대적으로 일반점프가 긴 점프 짧게 누르며 짧은 점프가 된다. 
        if (isSpaceUp == true)
        {
            isSpaceUp = false;
            playerRB.velocity = new Vector2(playerRB.velocity.x,playerRB.velocity.y * 0.5f);
        }
    }

    public void Player_Xfilp()
    {
        Dir = new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);
        Dir = MouseManager.ScreenMatchSizeMousePos() - Dir;

        //캐릭터 좌우 회전
        if (Dir.x < 0)
        {
            gameObject.Rect().localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Dir.x > 0)
        {
            gameObject.Rect().localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Player_AniCon()
    {
        //캐릭터 모션
        playerAni.SetBool("IsGround", isGround);
        playerAni.SetBool("IsRun", isRun);
    }

    public void PlayerControl()
    {
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
    }

    public void HandPosition()
    {
        LeftHand.RectLocalPosSet(new Vector3(7.0f, -5.0f, 0.0f));
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
        if (isDash == false)
        {
            if (collision.transform.tag.Equals("Wall"))
            {
                playerRB.velocity = Vector2.zero;
                isGround = true;
                isJump = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDash == false)
        {
            if (collision.transform.tag.Equals("Wall"))
            {
                isGround = true;
                isJump = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Wall"))
        {
            isGround = false;
        }
    }
}
