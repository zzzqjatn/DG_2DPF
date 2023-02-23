using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class WeaponCon : MonoBehaviour
{
    private const string Sword = "GreatSword";

    private SpriteAtlas SA_tmp;
    private GameObject weaponPart;
    private GameObject LeftHand;
    private GameObject RightHand;

    private bool IsEquite;

    void Start()
    {
        weaponPart = gameObject.FindChildObj("shortRangeWeapon");
        LeftHand = gameObject.FindChildObj("LeftHand");
        RightHand = gameObject.FindChildObj("RightHand");
        IsEquite = false;

        SA_tmp = Resources.Load<SpriteAtlas>("SpriteAtlas/SpriteAtlas");
        Setting();
    }

    void Update()
    {
        if (IsEquite)
        {
            SetHandPos();
        }
    }

    public void Setting()
    {
        weaponPart.SetActive(true);
        weaponPart.GetComponent<Image>().sprite = SA_tmp.GetSprite(Sword);
        weaponPart.GetComponent<Image>().SetNativeSize();
        weaponPart.GetComponent<BoxCollider2D>().size =
            new Vector2(weaponPart.RectSize().x, weaponPart.RectSize().y);
        IsEquite = true;
    }

    public void SetHandPos()
    {
        LeftHand.RectLocalPosSet(new Vector3(-7.0f, 0.0f, 0.0f));
        RightHand.RectLocalPosSet(new Vector3(-9.0f, 0.0f, 0.0f));
    }
}
