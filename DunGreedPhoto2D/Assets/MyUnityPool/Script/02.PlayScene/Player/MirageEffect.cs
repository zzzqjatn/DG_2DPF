using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageEffect : MonoBehaviour
{
    public static MirageEffect instance;

    private List<GameObject> mirageEffects;
    private GameObject miragePrefab;

    private const int MIRAGE_MAX = 12;

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

    // Start is called before the first frame update
    void Start()
    {
        mirageEffects = new List<GameObject>();

        miragePrefab = gameObject.FindChildObj("mirage");

        for (int i = 0; i < MIRAGE_MAX; i++)
        {
            GameObject temp = Instantiate(miragePrefab, gameObject.transform);
            temp.RectLocalPosSet(new Vector3(0.0f, 0.0f, 0.0f));
            temp.SetActive(false);
            mirageEffects.Add(temp);
        }
        miragePrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Time을 주고 맞춰서 진행
    public void setMirage(Vector2 pos, bool isLeft)
    {
        for (int i = 0; i < MIRAGE_MAX; i++)
        {
            if (mirageEffects[i].activeSelf == false)
            {
                mirageEffects[i].SetActive(true);
                mirageEffects[i].GetComponent<Mirage>().Respown(pos, isLeft);
                break;
            }
        }
    }
}
