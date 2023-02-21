using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CursorSet : MonoBehaviour
{
    public int mouseType;

    void Start()
    {
        Cursor.visible = false;
        if (gameObject.GetComponent<Graphic>())
            gameObject.GetComponent<Graphic>().raycastTarget = false;
    }

    void Update()
    {
        CursorSetting(mouseType);
    }

    private void CursorSetting(int type)
    {
        Vector2 temp = default;
        if (type == 1) { temp = MouseManager.ScreenMatchSizeMousePos(); }
        else if (type == 2) { temp = MouseManager.ScreenMatchSizeMousePos2(); }
        gameObject.RectLocalPosSet(new Vector3(temp.x, temp.y,0.0f));
    }
}
