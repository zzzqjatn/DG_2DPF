using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class dust : MonoBehaviour
{
    private Animator dustAni;
    private bool isAlive = false;
    void Update()
    {
        if(isAlive)
        {
            if (dustAni.GetCurrentAnimatorStateInfo(0).IsName("dust") &&
                dustAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                Die();
            }
        }
    }

    public void Respown(Vector2 pos, bool isLeft)
    {
        isAlive = true;
        dustAni = gameObject.GetComponent<Animator>();
        gameObject.RectLocalPosSet(new Vector3(pos.x, pos.y, 0));
        if(isLeft == true)
        {
            gameObject.Rect().localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            gameObject.Rect().localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void Die()
    {
        isAlive = false;
        gameObject.RectLocalPosSet(new Vector3(0, 0, 0));
        gameObject.SetActive(false);
    }
}
