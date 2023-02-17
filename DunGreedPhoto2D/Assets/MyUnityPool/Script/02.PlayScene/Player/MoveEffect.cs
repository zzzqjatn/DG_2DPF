using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffect : MonoBehaviour
{
    public static MoveEffect instance;

    private const int DUST_MAX = 5;

    private List<GameObject> dusts;
    private GameObject dustPreFab;

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

    void Start()
    {
        dusts = new List<GameObject>();

        dustPreFab = gameObject.FindChildObj("dust");

        for (int i = 0; i < DUST_MAX; i++)
        {
            GameObject temp = Instantiate(dustPreFab, gameObject.transform);
            temp.RectLocalPosSet(new Vector3(0.0f, 0.0f, 0.0f));
            temp.SetActive(false);
            dusts.Add(temp);
        }
        dustPreFab.SetActive(false);
    }

    void Update()
    {
        
    }

    public void setDust(Vector2 pos, bool isLeft)
    {
        for(int i = 0; i < DUST_MAX; i++)
        {
            if (dusts[i].activeSelf == false)
            {
                dusts[i].SetActive(true);
                dusts[i].GetComponent<dust>().Respown(pos, isLeft);
                break;
            }
        }
    }
}
