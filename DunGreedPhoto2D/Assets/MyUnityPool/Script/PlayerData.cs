using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public int SlotNum_;
    public SlotData SlotData_ = new SlotData();

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
        DontDestroyOnLoad(this.gameObject);

        SlotNum_ = 0;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartGame(int slotNum)
    {
        SlotNum_ = slotNum;
    }
}