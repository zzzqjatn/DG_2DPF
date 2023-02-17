using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    private const float SET_SCREEN_WIDTH = 320.0f;
    private const float SET_SCREEN_HEIGHT = 180.0f;

    private const float MagicNum = 30.0f;

    public static Vector2 ScreenMatchSizeMousePos()
    {
        float scaleX, scaleY;
        Vector2 Result = default;

        scaleX = Screen.width / SET_SCREEN_WIDTH;
        scaleY = Screen.height / SET_SCREEN_HEIGHT;

        Result = new Vector2(
            (Input.mousePosition.x / scaleX) - SET_SCREEN_WIDTH / 2,
            (Input.mousePosition.y / scaleY) - SET_SCREEN_HEIGHT / 2);

        Result.x += CamerCon.instance.CameraPos.x * MagicNum;
        Result.y += CamerCon.instance.CameraPos.y * MagicNum;

        return Result;
    }

    public static Vector2 ScreenMatchSizeMousePos2()
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
}
