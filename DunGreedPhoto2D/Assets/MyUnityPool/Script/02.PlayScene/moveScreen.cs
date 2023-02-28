using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class moveScreen : MonoBehaviour
{
    public static moveScreen instance;

    public bool IsActive;

    private GameObject Camera_;

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
        IsActive = false;

        Camera_ = GFunc.FindRootObj("CameraCon");
    }

    void Start()
    {
        
    }

    void Update()
    {
        dungeonMoveCheck();
    }

    public void dungeonMoveCheck()
    {
        if (IsActive == false)
        {
            gameObject.FindChildObj("Loading").SetActive(false);
        }
        else if (IsActive == true)
        {
            gameObject.FindChildObj("Loading").SetActive(true);
        }
    }

    public void dungeonTogo(float litx,float lity,float maxx, float maxy)
    {
        IsActive = true;
        Camera_.GetComponent<CamerCon>().CameraSizeReset(-8, -10, 122, 7);
        StartCoroutine(going());
    }
    
    IEnumerator going()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (Camera_.GetComponent<CamerCon>().IsRightPosition() == true &&
                IsActive == true)
            {
                IsActive = false;
                break;
            }
        }
    }
}
