using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerInven : MonoBehaviour
{
    public static playerInven instance;

    public Dictionary<string,GameObject> inventory;
    private const int MAX_INVEN = 15;

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

        inventory = new Dictionary<string, GameObject>();
    }

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

public class invenSlot
{
    public string itemName;
    public Vector2 pos;
    public bool IsDrag;
}