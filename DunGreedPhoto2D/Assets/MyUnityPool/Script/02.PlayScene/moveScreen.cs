using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class moveScreen : MonoBehaviour
{
    public static moveScreen instance;

    public bool IsActive;

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
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void dungeonMoveCheck(bool IsRight)
    {
        if (IsActive == false)
        {
            gameObject.FindChildObj("Loading").SetActive(true);
            IsActive = true;
        }

        if(IsRight)
        {
            gameObject.FindChildObj("Loading").SetActive(false);
        }
    }

}
