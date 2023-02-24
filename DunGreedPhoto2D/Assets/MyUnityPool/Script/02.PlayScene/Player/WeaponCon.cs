using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class WeaponCon : MonoBehaviour
{
    public static WeaponCon instance;

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

    private const string Sword = "GreatSword";

    private SpriteAtlas SA_tmp;
    private GameObject weaponPart;
    private GameObject LeftHand;
    private GameObject RightHand;

    private GameObject motionImag;
    private Quaternion motionImagQ;
    private Animator motionAni;

    private bool IsEquite;

    private int motion;

    void Start()
    {
        weaponPart = gameObject.FindChildObj("shortRangeWeapon");
        LeftHand = gameObject.FindChildObj("LeftHand");
        RightHand = gameObject.FindChildObj("RightHand");
        motionImag = gameObject.FindChildObj("motionImg");
        motionAni = motionImag.GetComponent<Animator>();

        IsEquite = false;

        SA_tmp = Resources.Load<SpriteAtlas>("SpriteAtlas/SpriteAtlas");
        Setting();
        motion = 1;
    }

    void Update()
    {
        if (IsEquite)
        {
            SetHandPos();
            motions();
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

        LeftHand.transform.parent = weaponPart.transform;
        RightHand.transform.parent = weaponPart.transform;
    }

    public void SetHandPos()
    {
        LeftHand.RectLocalPosSet(new Vector3(-7.0f, 0.0f, 0.0f));
        RightHand.RectLocalPosSet(new Vector3(-9.0f, 0.0f, 0.0f));
    }

    public void motions()
    {
        if(motion == 1)
        {
            weaponPart.RectLocalPosSet(new Vector3(-1,8,0.0f));
            weaponPart.RectLocalRotSet(new Vector3(0, 0, 140));

            motionImag.RectLocalPosSet(new Vector3(-5.9f, -8.9f, 0));

            motionImagQ = Quaternion.Euler(new Vector3(180, 0, 0)) * Quaternion.Euler(new Vector3(0, 0, 49));
            motionImag.RectLocalRotSet(motionImagQ);

            motionAni.SetBool("IsAttack", true);
        }
        else if(motion == 2)
        {
            weaponPart.RectLocalPosSet(new Vector3(9.5f, -10, 0.0f));
            weaponPart.RectLocalRotSet(new Vector3(0, 0, 320));

            motionImag.RectLocalPosSet(new Vector3(-6.2f, 9f, 0));
            motionImagQ = Quaternion.Euler(new Vector3(0, 0, 48));
            motionImag.RectLocalRotSet(motionImagQ);

            motionAni.SetBool("IsAttack", true);
        }
    }

    public void motionschange()
    {
        motion += 1;

        if (motion > 2) motion = 1;
    }

    public void onEndEvent()
    {
        motionAni.SetBool("IsAttack", false);
    }

}
