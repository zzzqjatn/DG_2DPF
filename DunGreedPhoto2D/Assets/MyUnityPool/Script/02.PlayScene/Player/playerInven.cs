using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerInven : MonoBehaviour
{
    public Dictionary<string,GameObject> inventory;
    private const int MAX_INVEN = 15;

    public void AddItem(string itemName,GameObject obj)
    {
        if(MAX_INVEN > inventory.Count)
        {
            inventory.Add(itemName, obj);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

public class inven
{

}

public class invenSlot
{
    public string itemName;
    public Vector2 pos;
    public bool IsDrag;
}