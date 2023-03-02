using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class monsterState : MonoBehaviour
{
    private float maxHP;
    private float currentHP;

    private GameObject lifeBar;
    private bool IsViewHPbar = false;

    private IEnumerator endcoroutin;

    void Start()
    {
        maxHP = 2000;
        currentHP = 2000;
        lifeBar = gameObject.FindChildObj("HpFront");
        lifeBar.GetComponent<Image>().fillAmount = currentHP / maxHP;
    }

    void Update()
    {
        
    }

    public void hitDamage(float damage_)
    {
        if (IsViewHPbar == true)
        {
            StopCoroutine(endcoroutin);
        }
        currentHP -= damage_;

        lifeBar.GetComponent<Image>().fillAmount = currentHP / maxHP;

        StartCoroutine(viewHPbar());
    }

    IEnumerator viewHPbar()
    {
        yield return null;
        lifeBar.transform.parent.gameObject.SetActive(true);
        IsViewHPbar = true;

        endcoroutin = endviewHPbar();
        StartCoroutine(endcoroutin);
    }

    IEnumerator endviewHPbar()
    {
        yield return new WaitForSeconds(1.5f);
        lifeBar.transform.parent.gameObject.SetActive(false);
        IsViewHPbar = false;
    }
}
