using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Dungeon_Eat : MonoBehaviour
{
    private GameObject Town;
    private GameObject Dungeon1;

    private GameObject DungeonEat_;
    private Animator DungeonEatAni;
    private GameObject player_;

    private bool IsGone;

    void Start()
    {
        Town = GFunc.FindRootObj("GameObjs").FindChildObj("Town");
        Dungeon1 = GFunc.FindRootObj("GameObjs").FindChildObj("Dungeon1-1");
        player_ = GFunc.FindRootObj("Playercanvas").FindChildObj("Player");

        DungeonEat_ = gameObject.FindChildObj("DungeonEat");
        DungeonEatAni = DungeonEat_.GetComponent<Animator>();
        IsGone = false;
    }

    void Update()
    {
        if (IsGone == true)
        {
            moveDungeon();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name.Equals("Player"))
        {
            IsGone = true;
            DungeonEat_.SetActive(true);
            moveDungeon();
        }
    }

    public void moveDungeon()
    {
        if (DungeonEatAni.GetCurrentAnimatorStateInfo(0).IsName("DungeonEat") &&
            DungeonEatAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            Town.SetActive(false);
            Dungeon1.SetActive(true);
            player_.SetActive(true);
            IsGone = false;
        }
        else if (DungeonEatAni.GetCurrentAnimatorStateInfo(0).IsName("DungeonEat") &&
            DungeonEatAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.15f)
        {
            player_.SetActive(false);
        }
        //LoadingSceneManager.LoadScene("02.PlayScene");
    }

}
