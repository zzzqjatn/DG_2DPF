using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Gmanager : MonoBehaviour
{
    public static Gmanager instance;

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
    }

    public bool IsDungeon;

    private GameObject escapeUI;
    private bool IsEscapeOn;

    private GameObject quit;
    private GameObject quitDungeon;

    private GameObject Town;
    private GameObject Dungeon1;
    private GameObject player_;
    private CamerCon camera_;

    void Start()
    {
        Town = GFunc.FindRootObj("GameObjs").FindChildObj("Town");
        Dungeon1 = GFunc.FindRootObj("GameObjs").FindChildObj("Dungeon1-1");
        player_ = GFunc.FindRootObj("Playercanvas").FindChildObj("Player");
        camera_ = GFunc.FindRootObj("CameraCon").GetComponent<CamerCon>();

        escapeUI = GFunc.FindRootObj("UIObjs").FindChildObj("EscapeUI");
        quit = escapeUI.FindChildObj("escape");
        quitDungeon = escapeUI.FindChildObj("escapeDungeon");

        IsEscapeOn = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Clicked();
        }
    }

    public void Clicked()
    {
        if (!IsEscapeOn)
        {
            escapeUI.SetActive(true);
            stateCheck();
            IsEscapeOn = true;
        }
        else if (IsEscapeOn)
        {
            escapeUI.SetActive(false);
            IsEscapeOn = false;
        }
    }

    public void stateCheck()
    {
        if(IsDungeon)
        {
            if(quit.activeSelf == true) quit.SetActive(false);
            quitDungeon.SetActive(true);
        }
        else if(!IsDungeon)
        {
            if (quitDungeon.activeSelf == true) quitDungeon.SetActive(false);
            quit.SetActive(true);
        }
    }

    public void Close_Click()
    {
        escapeUI.SetActive(false);
        IsEscapeOn = false;
    }

    public void quit_Clicked()
    {
        LoadingSceneManager.LoadScene("00.TitleScene");
    }

    public void quitDungeon_Clicked()
    {
        Town.SetActive(true);
        Dungeon1.SetActive(false);
        player_.SetActive(true);
        player_.RectLocalPosSet(new Vector3(0, 0, 0));
        camera_.CameraSizeReset(-57, -6, 80, 52);
        IsDungeon = false;
        escapeUI.SetActive(false);
        IsEscapeOn = false;
    }
}
