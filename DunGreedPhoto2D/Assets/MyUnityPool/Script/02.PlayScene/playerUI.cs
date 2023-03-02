using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour
{
    public static playerUI instance;

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

    private GameObject lifeBar;
    private GameObject frontWeapon;
    private GameObject backWeapon;

    void Start()
    {
        lifeBar = gameObject.FindChildObj("Lifebar");
        frontWeapon = gameObject.FindChildObj("FrontWeaponbar");
        backWeapon = gameObject.FindChildObj("BackWeaponbar");
    }

    void Update()
    {
        
    }

    public void settingLifeBar()
    {
        lifeBar.FindChildObj("Life").GetComponent<Image>().fillAmount =
            playerState.instance.p_state.currentHP / playerState.instance.p_state.maxHP;

        lifeBar.FindChildObj("LifeTxt").SetTxt(
            string.Format($"{(int)playerState.instance.p_state.currentHP} / {(int)playerState.instance.p_state.maxHP}"));

        lifeBar.FindChildObj("LvTxt").SetTxt(string.Format($"{playerState.instance.p_state.level}"));
    }

    public void rebootbar()
    {
        lifeBar.FindChildObj("Life").GetComponent<Image>().fillAmount =
            playerState.instance.p_state.currentHP / playerState.instance.p_state.maxHP;

        lifeBar.FindChildObj("LifeTxt").SetTxt(
            string.Format($"{(int)playerState.instance.p_state.currentHP} / {(int)playerState.instance.p_state.maxHP}"));
    }
}
