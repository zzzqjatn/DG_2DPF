using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class bulletObjPool : MonoBehaviour
{
    private const int BAT_BULLET_MAX = 30;
    private const int BANSHEE_BULLET_MAX = 30;

    private GameObject batbulletObj;
    private List<GameObject> batbulletList;

    private GameObject ghostbulletObj;
    private List<GameObject> ghostbulletList;

    void Start()
    {
        batbulletObj = gameObject.FindChildObj("batbullet");

        batbulletList = new List<GameObject>();

        for (int i = 0; i < BAT_BULLET_MAX; i++)
        {
            GameObject temp = Instantiate(batbulletObj, gameObject.transform);
            batbulletList.Add(temp);
        }
        
        batbulletObj.SetActive(false);
    }

    void Update()
    {
        
    }

    public void bulletFire(Vector2 pos_, Vector2 dir_, float speed_, float damage_)
    {
        for(int i = 0; i < batbulletList.Count; i++)
        {
            if (batbulletList[i].activeSelf == false)
            {
                batbulletList[i].GetComponent<batbullet>().setBullet(pos_, dir_, speed_, damage_);
                batbulletList[i].SetActive(true);
                break;
            }
        }
    }
}
