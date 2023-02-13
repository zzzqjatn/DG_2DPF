using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TitleManager : MonoBehaviour
{
    private const int BackGroundCount = 2;
    private const int SubBackGroundCount = 7;

    private GameObject backCloudPreFab;
    private GameObject midCloudPreFab;
    private GameObject frontCloudPreFab;
    private GameObject birdPreFab;

    private List<GameObject> L_backcloud;
    private List<GameObject> L_midCloud;
    private List<GameObject> L_frontCloud;
    private List<GameObject> L_birdCloud;

    private GameObject MouseCursor;
    private float screenX;
    private float screenY;

    void Start()
    {
        GFunc.FindRootObj("GameObjs").FindChildObj("PopObjs").GetComponent<PopManager>().GameStartPopOff();
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
        GameObject tempRoot = GFunc.FindRootObj("GameObjs");

        backCloudPreFab = tempRoot.FindChildObj("BackCloud");
        midCloudPreFab = tempRoot.FindChildObj("MidCloud");
        frontCloudPreFab = tempRoot.FindChildObj("FrontCloud");
        birdPreFab = tempRoot.FindChildObj("Bird");

        L_backcloud = new List<GameObject>();
        L_midCloud = new List<GameObject>();
        L_frontCloud = new List<GameObject>();
        L_birdCloud = new List<GameObject>();

        GameObject temp = default;

        for(int i = 0; i < BackGroundCount; i++)
        {
            temp = Instantiate(backCloudPreFab, backCloudPreFab.transform.parent);
            temp.RectLocalPosAdd(new Vector3(temp.RectSize().x * i, 0, 0));
            L_backcloud.Add(temp);

            temp = Instantiate(frontCloudPreFab, frontCloudPreFab.transform.parent);
            temp.RectLocalPosAdd(new Vector3(temp.RectSize().x * i, 0, 0));
            L_frontCloud.Add(temp);
        }

        for(int i = 0; i < SubBackGroundCount; i++)
        {
            temp = Instantiate(midCloudPreFab, midCloudPreFab.transform.parent);
            temp.RectLocalPosSet(new Vector3(0.0f, 0.0f, 1.0f));
            temp.SetActive(false);
            L_midCloud.Add(temp);

            temp = Instantiate(birdPreFab, birdPreFab.transform.parent);
            temp.RectLocalPosSet(new Vector3(0.0f, 0.0f, 1.0f));
            temp.SetActive(false);
            L_birdCloud.Add(temp);
        }

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

    public void SetMouseCursor()
    {
        GameObject tempRoot = GFunc.FindRootObj("GameObjs");
        MouseCursor = tempRoot.FindChildObj("MouseCursor");

        screenX = Screen.width - 320.0f;
        screenY = Screen.height - 180.0f;
    }
    public void MouseCursorOn()
    {
        GameObject tempRoot = GFunc.FindRootObj("GameObjs");
        GameObject temp222 = tempRoot.FindChildObj("MousePointTxt");
        temp222.SetTxt(string.Format($"¸¶¿ì½º : {Input.mousePosition.x}, {Input.mousePosition.y}"));

        //Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseCursor.RectLocalPosSet(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
    }

    public void GameStartButton_Clicked()
    {
        GFunc.FindRootObj("GameObjs").FindChildObj("PopObjs").GetComponent<PopManager>().GameStartPopOn();
    }

    public void GameQuitButton_Clicked()
    {
        GFunc.QuitGame();
    }
}
