using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using Unity.Mathematics;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.U2D;

public static partial class GFunc
{
    public static void OutImage(this GameObject obj)
    {
        Image tempImage = default;
        tempImage = obj.GetComponent<Image>();

        tempImage.sprite = null;

        Color tempAlpha = tempImage.color;
        tempAlpha.a = 0.0f;
        tempImage.color = tempAlpha;
    }
    public static void SetImage(this GameObject obj, string name,SpriteAtlas imageatlas)
    {
        //imageAtlas 가 null에 대한 방어로직 없음

        Image targetImage = default;
        targetImage = obj.GetComponent<Image>();

        targetImage.sprite = imageatlas.GetSprite(name);

        if(targetImage.color.a == 0.0f)
        {
            Color tempAlpha = targetImage.color;
            tempAlpha.a = 1.0f;
            targetImage.color = tempAlpha;
        }
    }

    //! 오브젝트의 앵커 포지션을 연산하는 함수
    public static Vector2 ReturnAnchoredPos(this GameObject obj_, Vector2 position2D)
    {
        Vector2 Result = obj_.Rect().anchoredPosition;
        Result += position2D;
        return Result;
    }

    //! 오브젝트의 앵커 포지션을 연산하는 함수
    public static void AddAnchoredPos(this GameObject obj_, Vector2 position2D)
    {
        obj_.Rect().anchoredPosition += position2D;
    }

    public static Vector3 RectLeftTop(this GameObject obj)
    {
        Vector3 Result = default;

        Vector3 tmepPos = obj.GetComponent<RectTransform>().localPosition;
        Vector3 tempSize = obj.GetComponent<RectTransform>().sizeDelta;

        Result = new Vector3((tmepPos.x - (tempSize.x / 2)),
                            (tmepPos.y + (tempSize.y / 2)), 
                            tmepPos.z);
        return Result;
    }
    public static Quaternion RectLocalRot(this GameObject obj)
    {
        Quaternion Result = default;
        Result = obj.GetComponent<RectTransform>().localRotation;
        return Result;
    }

    public static void RectLocalPosAdd(this GameObject obj, Vector3 add)
    {
        obj.GetComponent<RectTransform>().localPosition += add;
    }
    public static void RectLocalPosSet(this GameObject obj,Vector3 set)
    {
        obj.GetComponent<RectTransform>().localPosition = set;
    }

    public static Vector3 RectLocalPos(this GameObject obj)
    {
        Vector3 Result = default;
        Result = obj.GetComponent<RectTransform>().localPosition;
        return Result;
    }

    public static void RectSizeSet(this GameObject obj,Vector2 size)
    {
        obj.GetComponent<RectTransform>().sizeDelta = size;
    }
    public static Vector2 RectSize(this GameObject obj)
    {
        Vector2 Result = default;
        Result = obj.GetComponent<RectTransform>().sizeDelta;
        return Result;
    }

    public static RectTransform Rect(this GameObject obj)
    {
        RectTransform Result = default;
        Result = obj.GetComponent<RectTransform>();
        return Result;
    }
}
