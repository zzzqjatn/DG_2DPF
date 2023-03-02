using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public static Player instance;

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
    }

    private Animator playerAni;
    private Rigidbody2D playerRB;
    private CapsuleCollider2D playerCollider;
    private BoxCollider2D playerWallCollider;

    public bool isDash;
    public bool isJump;

    private float dashCoolTimeCurrent;

    private float dashPlayTime;
    private float dashEndTime;

    private int JumpPower;
    private float dashPower;

    private float dustEventTime;
    private float dashEventTime;

    private bool isRight;
    private bool isLeft;
    public bool isDown;
    private bool isSpaceUp;
    private bool isSpaceDown;
    private bool isRightClick;

    private Vector2 Dir;
    public bool isRun;
    public bool isJumpExit;

    private Vector2 boxCastSize = new Vector2(0.3f, 0.05f);
    private float boxCastMaxDistance = 0.35f;

    public bool isSlope;
    private Vector2 perp;
    public float clamAngle;
    public float MaxAngle = 60;

    public Vector2 perpVelo;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerAni = gameObject.GetComponent<Animator>();
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();
        playerWallCollider = gameObject.FindChildObj("WallCheck").GetComponent<BoxCollider2D>();

        isDash = false;
        isJump = false;

        JumpPower = 10;
        dashPower = 50;

        playerState.instance.p_state.settingState(1, 80, 80, 2, 2, 0.2f, 50, 5);

        dashEndTime = 0.08f;
        dashPlayTime = 0.0f;

        dashCoolTimeCurrent = 0.0f;

        dustEventTime = 0.0f;
        dashEventTime = 0.2f;

        isRight = false;
        isLeft = false;
        isDown = false;
        isSpaceDown = false;
        isSpaceUp = false;
        isRun = false;
        isRightClick = false;
        isJumpExit = false;

        isSlope = false;

        perpVelo = Vector2.zero;

        Dir = new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);
    }

    private void FixedUpdate()
    {
        playerMoves();

        //Physics2D.IgnoreLayerCollision(10, 12, true);

        //if (isDown == true)
        //{
        //    Physics2D.IgnoreCollision(IgnoreCollider, playerCollider, true);
        //    Physics2D.IgnoreCollision(IgnoreCollider, playerWallCollider, true);

        //    Physics2D.IgnoreCollision(IgnoreCollider2, playerCollider, true);
        //    Physics2D.IgnoreCollision(IgnoreCollider2, playerWallCollider, true);
        //}

        //if (isDown == false)
        //{
        //    Physics2D.IgnoreCollision(IgnoreCollider, playerCollider, false);
        //    Physics2D.IgnoreCollision(IgnoreCollider, playerWallCollider, false);

        //    Physics2D.IgnoreCollision(IgnoreCollider2, playerCollider, false);
        //    Physics2D.IgnoreCollision(IgnoreCollider2, playerWallCollider, false);
        //}
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

        PlayerControl();
    }

    public void playerMoves()
    {
        //�ȱ�
        Player_Walk();

        //����
        Player_LongJump();
        Player_ShortJump();

        //�뽬
        Player_Dash();

        playerClam();
    }

    public void playerClam()
    {
        if (isRun == false && isDash == false && isJump == false)
        {
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        float searchDistance = 0.48f;
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, searchDistance, LayerMask.GetMask("Wall"));

        //�Ʒ� �浹�ϸ� (�ٴ��� ������)
        if(hit)
        {
            perp = Vector2.Perpendicular(hit.normal).normalized;
            clamAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (clamAngle != 0 && isDown == false)
                isSlope = true;
            else
                isSlope = false;

            //if(Dir.x < 0)   //��
            //{
            //    Debug.DrawLine(hit.point, hit.point + hit.normal, Color.red);
            //    Debug.DrawLine(hit.point, hit.point + perp, Color.blue);
            //}
            //else if (Dir.x > 0) //��
            //{
            //    Debug.DrawLine(hit.point, hit.point + hit.normal, Color.red);
            //    Debug.DrawLine(hit.point, hit.point + perp * -1, Color.blue);
            //}
        }
    }

    public void playerKeyControl()
    {
        //����Ű
        if (Input.GetKey(KeyCode.A))
        {
            if (isDash == false) isLeft = true;
            if (isJump == false && isDash == false) isRun = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (isDash == false) isRight = true;
            if (isJump == false && isDash == false) isRun = true;
        }
        else isRun = false;

        if (Input.GetKey(KeyCode.S))
        {
            isDown = true;
        }
        else isDown = false;

        //����
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
        {
            isSpaceDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && 0 < playerRB.velocity.y)
        {
            isSpaceUp = true;
        }

        //�뽬
        if (Input.GetMouseButtonDown(1) && playerState.instance.p_state.currentDash > 0)
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

                //�����϶�
                if(isSlope)
                {
                    if (IsOnGround2() && !isJump && clamAngle < MaxAngle)
                    {
                        perpVelo = perp * playerState.instance.p_state.speed;
                        playerRB.gravityScale = Mathf.Abs(perpVelo.y);

                        playerRB.velocity = perpVelo;
                    }
                    else if(isJump)
                    {
                        playerRB.velocity = new Vector2(-playerState.instance.p_state.speed, playerRB.velocity.y);
                    }
                }
                else if(!isSlope)
                {
                    playerRB.gravityScale = 2.0f;

                    if (IsOnGround() && !isJump) //����
                    {
                        playerRB.velocity = new Vector2(-playerState.instance.p_state.speed, 0);
                    }
                    else if (!IsOnGround())
                    {
                        playerRB.velocity = new Vector2(-playerState.instance.p_state.speed, playerRB.velocity.y);
                    }
                }
            }
            else if (isRight == true)
            {
                isRight = false;

                //�����϶�
                if (isSlope)
                {
                    if (IsOnGround2() && !isJump && clamAngle < MaxAngle)
                    {
                        perpVelo = perp * -playerState.instance.p_state.speed;
                        playerRB.gravityScale = Mathf.Abs(perpVelo.y);

                        playerRB.velocity = perpVelo;
                    }
                    else if (isJump)
                    {
                        playerRB.velocity = new Vector2(playerState.instance.p_state.speed, playerRB.velocity.y);
                    }
                }
                else if (!isSlope)
                {
                    playerRB.gravityScale = 2.0f;

                    if (IsOnGround() && !isJump) //����
                    {
                        playerRB.velocity = new Vector2(playerState.instance.p_state.speed, 0);
                    }
                    else if (!IsOnGround())
                    {
                        playerRB.velocity = new Vector2(playerState.instance.p_state.speed, playerRB.velocity.y);
                    }
                }
            }
        }
    }

    public void Player_Dash()
    {
        if(isRightClick == true)
        {
            isRightClick = false;

            dashEventTime = 0.02f;
            dashPlayTime = 0.0f;
            playerState.instance.p_state.currentDash -= 1;
            playerRB.velocity = Vector2.zero;
            playerRB.gravityScale = 0.0f;

            playerRB.AddForce(new Vector2(Dir.x, Dir.y).normalized * dashPower, ForceMode2D.Impulse);
            isDash = true;
            isJump = true;
            isJumpExit = true;

            //Physics2D.iga(,12,true);
        }
    }

    public void Player_DashRunnig()
    {
        //�뽬 ��Ÿ�� �κ�
        if (playerState.instance.p_state.currentDash != playerState.instance.p_state.maxDash)
        {
            dashCoolTimeCurrent += Time.deltaTime;

            if (playerState.instance.p_state.cooltimeDash < dashCoolTimeCurrent)
            {
                dashCoolTimeCurrent = 0.0f;
                playerState.instance.p_state.currentDash += 1;
            }
        }

        //�뽬 �κ�
        if (isDash)
        {   //���� �ð���ŭ ����
            dashPlayTime += Time.deltaTime;
            //�뽬 �ܻ� �߻�
            if (dashPlayTime > dashEventTime)
            {
                dashEventTime += 0.019f;

                if (gameObject.RectLocalRot().y == 0.0f)
                {
                    MirageEffect.instance.setMirage(new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y), false);
                }
                else if (gameObject.RectLocalRot().y == 1.0f)
                {
                    MirageEffect.instance.setMirage(new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y), true);
                }
            }

            if (dashPlayTime > dashEndTime)
            {
                dashPlayTime = 0.0f;
                playerRB.velocity = Vector2.zero;
                playerRB.gravityScale = 2.0f;
                isDash = false;

                if (IsOnGround())
                {
                    isJump = false;
                    isJumpExit = false;
                }

                playerAni.SetBool("IsJump", isDash);
            }
            else
            {
                playerAni.SetBool("IsJump", isDash);
            }
        }
        else if(!isDash)
        {

        }
    }

    public void Player_Running()
    {
        //�÷��̾� �޸��� ����
        if (isRun == true && isJump == false)
        {
            dustEventTime += Time.deltaTime;
            if (dustEventTime > 0.4f)
            {
                dustEventTime = 0.0f;

                //����
                if (gameObject.RectLocalRot().y == 1.0f)
                {
                    DushEffect.instance.setDust(
                        new Vector2(gameObject.RectLocalPos().x + 7.0f, gameObject.RectLocalPos().y - 5.0f), true);
                }
                else if (gameObject.RectLocalRot().y == 0.0f)
                {
                    DushEffect.instance.setDust( 
                        new Vector2(gameObject.RectLocalPos().x - 7.0f, gameObject.RectLocalPos().y - 5.0f), false);
                }
            }
        }
        else
        {
            dustEventTime = 0.0f;
        }
    }

    public void Player_LongJump()
    {
        if (isSpaceDown == true)
        {
            isSpaceDown = false;
            isJump = true;
            playerRB.velocity = Vector2.zero;
            playerRB.gravityScale = 2.0f;
            playerRB.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
    }

    public void Player_ShortJump()
    {
        //���� �϶� ª�� ������ ���� �ٴ� ���� ������ ����
        //��������� �Ϲ������� �� ���� ª�� ������ ª�� ������ �ȴ�. 
        if (isSpaceUp == true)
        {
            isSpaceUp = false;
            if (isJump == true)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);
            }
        }
    }

    public void Player_Xfilp()
    {
        Dir = new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);
        Dir = MouseManager.ScreenMatchSizeMousePos() - Dir;

        //ĳ���� �¿� ȸ��
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
        //ĳ���� ���
        playerAni.SetBool("IsJump", isJump);
        playerAni.SetBool("IsRun", isRun);
    }

    public void PlayerControl()
    {
        //����
        if (Input.GetMouseButtonDown(0))
        {
            WeaponCon.instance.motionschange();

            //StartCoroutine("LeftPunch", LeftHand.RectLocalPos());

            //Rigidbody2D handRig = LeftHand.GetComponent<Rigidbody2D>();
            //handRig.velocity = Vector2.zero;

            //Vector2 Dir = new Vector2(LeftHand.RectLocalPos().x, LeftHand.RectLocalPos().y);
            ////�� ���콺 ��ġ
            //Vector2 mousePos = MouseManager.ScreenMatchSizeMousePos();
            //// �� ���콺 ��ġ - �� ��ġ
            //Dir = mousePos - Dir;

            //handRig.AddForce(new Vector2(Dir.x, Dir.y).normalized * 50, ForceMode2D.Impulse);
        }
    }

    //IEnumerator LeftPunch(Vector3 firstPos)
    //{
    //    bool isRight = false;
    //    Vector2 Dir = new Vector2(gameObject.RectLocalPos().x, gameObject.RectLocalPos().y);
    //    //�� ���콺 ��ġ
    //    Vector2 mousePos = MouseManager.ScreenMatchSizeMousePos();
    //    // �� ���콺 ��ġ - �� ��ġ
    //    Dir = mousePos - Dir;

    //    if (gameObject.RectLocalRot().y == 0) { isRight = true; }


    //    yield return new WaitForSeconds(0.05f);

    //    //handRig.velocity = Vector2.zero;

    //    //Vector2 tempruslt = new Vector2(7.0f - firstPos.x,-5.0f - firstPos.y);

    //    //while(true)
    //    //{
    //    //    handRig.AddForce(new Vector2(tempruslt.x, tempruslt.y).normalized * 1, ForceMode2D.Force);

    //    //    if(isRight == false)
    //    //    {
    //    //        if (LeftHand.RectLocalPos().x < 7.0f)
    //    //        {
    //    //            break;
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        if (LeftHand.RectLocalPos().x > 7.0f)
    //    //        {
    //    //            break;
    //    //        }
    //    //    }
    //    //}

    //    //LeftHand.RectLocalPosSet(new Vector3(7.0f, -5.0f, 0.0f));
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDash == false)
        {
            if (collision.transform.tag.Equals("Ground") || collision.transform.tag.Equals("Wall"))
            {
                if (isJumpExit == true && IsOnGround())
                {
                    //playerRB.velocity = Vector2.zero;
                    isJump = false;
                    isJumpExit = false;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Ground") || collision.transform.tag.Equals("Wall"))
        {
            if (isRun == true)
            {
                if (!IsOnGround())
                {
                    //playerRB.velocity = Vector2.zero;
                    isRun = false;
                    isJump = true;
                    isJumpExit = true;
                }
            }
            
            if (isJump == true)
            {
                isJumpExit = true;
            }
            else if (isJump == false)
            {
                if (!IsOnGround())
                {
                    //playerRB.velocity = Vector2.zero;
                    isJump = true;
                    isJumpExit = true;
                }
            }
        }
    }

    private bool IsOnGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, LayerMask.GetMask("Wall"));
        return (raycastHit.collider != null);
    }

    private bool IsOnGround2()  //�� �ٴ�üũ (��缱�� ��� üũ�ؾ� �������鼭 üũ�� �����ϱ� ������)
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, 0.5f, LayerMask.GetMask("Wall"));
        return (raycastHit.collider != null);
    }

    //void OnDrawGizmos()
    //{
    //    RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, LayerMask.GetMask("Wall"));

    //    Gizmos.color = Color.red;
    //    if (raycastHit.collider != null)
    //    {
    //        Gizmos.DrawRay(transform.position, Vector2.down * raycastHit.distance);
    //        Gizmos.DrawWireCube(transform.position + Vector3.down * raycastHit.distance, boxCastSize);
    //    }
    //    else
    //    {
    //        Gizmos.DrawRay(transform.position, Vector2.down * boxCastMaxDistance);
    //    }
    //}
}