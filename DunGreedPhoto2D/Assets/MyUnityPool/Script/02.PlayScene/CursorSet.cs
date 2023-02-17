using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSet : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        CursorSetting();
    }

    private void CursorSetting()
    {
        Cursor.visible = false;
        Vector2 temp = MouseManager.ScreenMatchSizeMousePos();
        gameObject.RectLocalPosSet(new Vector3(temp.x, temp.y,0.0f));
    }
}
