using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
    IDragHandler
{
    private const float SET_SCREEN_WIDTH = 320.0f;
    private const float SET_SCREEN_HEIGHT = 180.0f;

    public static Vector2 ScreenMatchSizeMousePos()
    {
        float scaleX, scaleY;
        Vector2 Result = default;
        
        scaleX = Screen.width / SET_SCREEN_WIDTH;
        scaleY = Screen.height / SET_SCREEN_HEIGHT;

        Result = new Vector2(
            (Input.mousePosition.x / scaleX) - SET_SCREEN_WIDTH / 2,
            (Input.mousePosition.y / scaleY) - SET_SCREEN_HEIGHT / 2);

        return Result;
    }


    ///*
    //*  IsType 상태
    //*  0 : None
    //*  1 : 포탑 설치 on
    //*/

    //private GameObject BgObj;
    //private Canvas mainCanvas;

    //private TowerTile towerTile_;
    //private TowerObjPool towerobjs_;

    //public int IsType;
    //private bool isClicked;

    //void Start()
    //{
    //    BgObj = GFunc.FindRootObj(GFunc.GAMEOBJ_ROOT_NAME).FindChildObj("BgObjs");
    //    mainCanvas = GFunc.FindRootObj(GFunc.GAMEOBJ_ROOT_NAME).GetComponent<Canvas>();

    //    towerTile_ = gameObject.GetComponent<TowerTile>();

    //    GameObject obj = gameObject.FindChildObj("TowerObjPool");

    //    towerobjs_ = obj.GetComponent<TowerObjPool>();

    //    IsType = 1;
    //    isClicked = false;
    //}

    //void Update()
    //{

    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    isClicked = true;

    //    float MouseX = eventData.position.x / mainCanvas.scaleFactor;
    //    float MouseY = eventData.position.y / mainCanvas.scaleFactor;

    //    //Debug.Log($" 현재 마우스 위치 확인 : ({MouseX} , {MouseY})");

    //    float offsetX = (BgObj.RectranSize().x - gameObject.RectranSize().x) / 2;
    //    float offsetY = (BgObj.RectranSize().y - gameObject.RectranSize().y);

    //    float PosOffsetX = gameObject.RectranSize().x / 2;
    //    float PosOffsetY = gameObject.RectranSize().y / 2;

    //    float LocalOffsetX = gameObject.RectranLocalPos().x * (-1);
    //    float LocalOffsetY = gameObject.RectranLocalPos().y * (-1);

    //    float RightX = MouseX - (offsetX + PosOffsetX) + LocalOffsetX;
    //    float RightY = MouseY - (offsetY + PosOffsetY - 1.5f);

    //    //Debug.Log($" 마우스 재정의 위치 확인 : ({RightX} , {RightY})");

    //    switch (IsType)
    //    {
    //        case 1:
    //            Tile temp = towerTile_.GetTile(new Vector2(RightX, RightY));
    //            towerobjs_.SetTower(temp.GetPos());
    //            break;
    //    }
    //}

    public void OnPointerDown(PointerEventData eventData)
    {

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //isClicked = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (isClicked == true)
        //{

        //}
    }
}
