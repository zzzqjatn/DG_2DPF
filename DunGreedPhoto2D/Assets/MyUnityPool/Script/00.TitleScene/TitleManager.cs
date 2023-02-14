using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private const int BackGroundCount = 2;
    private const int SubBackGroundCount = 7;

    private GameObject backCloudPreFab;
    private GameObject midCloudPreFab;
    private GameObject frontCloudPreFab;
    private GameObject birdPreFab;

    private List<GameObject> L_backcloud;
    private List<GameObject> L_frontCloud;

    private List<RollingObj> L_midCloud;
    private List<RollingObj> L_birdCloud;

    private float scaleX;
    private float scaleY;
    private GameObject MouseCursor;

    void Start()
    {
        scaleX = Screen.width / 320.0f;
        scaleY = Screen.height / 180.0f;

        GFunc.FindRootObj(GFunc.N_GAME_OBJ).FindChildObj(GFunc.N_POP_OBJ).GetComponent<PopManager>().GameStartPopOff(PopManager.POPUP1);
        SetupObjPool();
        SetMouseCursor();
    }

    private void FixedUpdate()
    {
        RollingBackGround(L_backcloud, 3f);
        RollingBackGround(L_frontCloud, 15f);
        MouseCursorOn();
    }

    void Update()
    {

    }

    private void SetupObjPool()
    {
        GameObject tempRoot = GFunc.FindRootObj(GFunc.N_GAME_OBJ);

        backCloudPreFab = tempRoot.FindChildObj("BackCloud");
        midCloudPreFab = tempRoot.FindChildObj("MidCloud");
        frontCloudPreFab = tempRoot.FindChildObj("FrontCloud");
        birdPreFab = tempRoot.FindChildObj("Bird");

        L_backcloud = new List<GameObject>();
        L_frontCloud = new List<GameObject>();
        L_midCloud = new List<RollingObj>();
        L_birdCloud = new List<RollingObj>();

        GameObject temp1 = default;

        for(int i = 0; i < BackGroundCount; i++)
        {
            temp1 = Instantiate(backCloudPreFab, backCloudPreFab.transform.parent);
            temp1.RectLocalPosAdd(new Vector3(temp1.RectSize().x * i, 0, 0));
            L_backcloud.Add(temp1);

            temp1 = Instantiate(frontCloudPreFab, frontCloudPreFab.transform.parent);
            temp1.RectLocalPosAdd(new Vector3(temp1.RectSize().x * i, 0, 0));
            L_frontCloud.Add(temp1);
        }

        // 추 후 폴리싱 : 작은 구름, 새 타이틀화면
        //RollingObj temp2 = default;
        //for (int i = 0; i < SubBackGroundCount; i++)
        //{
        //    temp2 = new RollingObj();
        //    temp2.obj = Instantiate(midCloudPreFab, midCloudPreFab.transform.parent);
        //    temp2.obj.RectLocalPosSet(new Vector3(0.0f, 0.0f, 1.0f));
        //    temp2.obj.SetActive(false);
        //    temp2.speed = 0;
        //    L_midCloud.Add(temp2);

        //    temp2 = new RollingObj();
        //    temp2.obj = Instantiate(birdPreFab, birdPreFab.transform.parent);
        //    temp2.obj.RectLocalPosSet(new Vector3(0.0f, 0.0f, 1.0f));
        //    temp2.obj.SetActive(false);
        //    temp2.speed = 0;
        //    L_birdCloud.Add(temp2);
        //}

        backCloudPreFab.SetActive(false);
        midCloudPreFab.SetActive(false);
        frontCloudPreFab.SetActive(false);
        birdPreFab.SetActive(false);
    }

    public void RollingBackGround(List<GameObject> Objs,float speed)
    {
        for(int i = 0; i < Objs.Count; i++)
        {
            Objs[i].RectLocalPosAdd(new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f));

            if(Objs[Objs.Count - 1].RectLocalPos().x <= 0.0f)
            {
                GameObject temp = Objs[0];
                Objs.RemoveAt(0);
                temp.RectLocalPosSet(
                    Objs[Objs.Count - 1].RectLocalPos() + new Vector3(Objs[0].RectSize().x, 0.0f, 0.0f));
                Objs.Add(temp);
            }
        }
    }

    public void GameStartButton_Clicked()
    {
        GFunc.FindRootObj(GFunc.N_GAME_OBJ).FindChildObj(GFunc.N_POP_OBJ).GetComponent<PopManager>().GameStartPopOn(PopManager.POPUP1);
    }

    public void GameQuitButton_Clicked()
    {
        GFunc.QuitGame();
    }

    public void SetMouseCursor()
    {
        Cursor.visible = false;

        GameObject tempRoot = GFunc.FindRootObj(GFunc.N_GAME_OBJ);
        MouseCursor = tempRoot.FindChildObj("MouseCursor");

        if (MouseCursor.GetComponent<Graphic>())
            MouseCursor.GetComponent<Graphic>().raycastTarget = false;
    }
    public void MouseCursorOn()
    {
        MouseCursor.RectLocalPosSet(new Vector3(
            (Input.mousePosition.x / scaleX) - 160.0f,
            (Input.mousePosition.y / scaleY) - 90.0f, 0.0f));

        Vector3 MousePos = MouseCursor.RectLocalPos();
        //4방향 끝지점 막기
        if (MousePos.x <= -160.0f && MousePos.y >= 90.0f) { MouseCursor.RectLocalPosSet(new Vector3(-160.5f, 89.5f, 0.0f)); }
        else if (MousePos.x >= 160.0f && MousePos.y >= 90.0f) { MouseCursor.RectLocalPosSet(new Vector3(159.5f, 89.5f, 0.0f)); }
        else if (MousePos.x <= -160.0f && MousePos.y <= -90.0f) { MouseCursor.RectLocalPosSet(new Vector3(-160.5f, -90.5f, 0.0f)); }
        else if (MousePos.x >= 160.0f && MousePos.y <= -90.0f) { MouseCursor.RectLocalPosSet(new Vector3(159.5f, -90.5f, 0.0f)); }
        else if (MousePos.x <= -160.0f) { MouseCursor.RectLocalPosSet(new Vector3(-160.5f, MousePos.y, 0.0f)); }
        else if (MousePos.x >= 160.0f) { MouseCursor.RectLocalPosSet(new Vector3(159.5f, MousePos.y, 0.0f)); }
        else if (MousePos.y >= 90.0f) { MouseCursor.RectLocalPosSet(new Vector3(MousePos.x, 89.5f, 0.0f)); }
        else if (MousePos.y <= -90.0f) { MouseCursor.RectLocalPosSet(new Vector3(MousePos.x, -90.5f, 0.0f)); }
    }
}

class RollingObj
{
    public RollingObj()
    {
        obj = new GameObject();
    }

    public GameObject obj;
    public int speed;

    public void SetObj(GameObject obj_, int speed_)
    {
        obj = obj_;
        speed = speed_;
    }
}