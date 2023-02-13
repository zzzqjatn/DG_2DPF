using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseControler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isClicked;

    void Start()
    {
        isClicked = false;
    }

    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClicked = true;

        //float MouseX = eventData.position.x / mainCanvas.scaleFactor;
        //float MouseY = eventData.position.y / mainCanvas.scaleFactor;

        //float offsetX = (BgObj.RectranSize().x - gameObject.RectranSize().x) / 2;
        //float offsetY = (BgObj.RectranSize().y - gameObject.RectranSize().y);

        //float PosOffsetX = gameObject.RectranSize().x / 2;
        //float PosOffsetY = gameObject.RectranSize().y / 2;

        //float LocalOffsetX = gameObject.RectranLocalPos().x * (-1);
        //float LocalOffsetY = gameObject.RectranLocalPos().y * (-1);

        //float RightX = MouseX - (offsetX + PosOffsetX) + LocalOffsetX;
        //float RightY = MouseY - (offsetY + PosOffsetY - 1.5f);


        //switch (IsType)
        //{
        //    case 1:
        //        Tile temp = towerTile_.GetTile(new Vector2(RightX, RightY));
        //        towerobjs_.SetTower(temp.GetPos());
        //        break;
        //}
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClicked = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isClicked == true)
        {

        }
    }
}
