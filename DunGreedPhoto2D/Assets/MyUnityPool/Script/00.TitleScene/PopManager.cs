using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameStartPopOn()
    {
        gameObject.PopUpControl("Pop1", PopType.ON);
    }

    public void GameStartPopOff()
    {
        gameObject.PopUpControl("Pop1", PopType.OFF);
    }

}
