using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftClickManager : MonoBehaviour
{
    public static LeftClickManager instance;

    public bool IsClicked;
    public bool IsNPC;

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

        IsClicked = false;
        IsNPC = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        CLICKcheck();
    }

    public bool CLICKcheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsClicked = true;
        }
        else IsClicked = false;

        return IsClicked;
    }
}
