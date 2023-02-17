using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mirage : MonoBehaviour
{
    private const float START_ALPHA = 255f;

    private Image mirageImg;
    private bool isAlive = false;
    private float mirageAlpha;
    private Color ActColor;

    void Update()
    {
        if (isAlive)
        {
            if (mirageAlpha <= 0.0f)
            {
                Die();
            }
            mirageAlpha -= 10f;

            ActColor.a = (mirageAlpha/255.0f);
            mirageImg.color = ActColor;
        }
    }

    public void Respown(Vector2 pos, bool isLeft)
    {
        isAlive = true;
        mirageImg = gameObject.GetComponent<Image>();
        mirageAlpha = START_ALPHA;
        ActColor = new Color(255f, 255f, 255f, START_ALPHA);
        mirageImg.color = ActColor;

        gameObject.RectLocalPosSet(new Vector3(pos.x, pos.y, 0));
        if (isLeft == true)
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
