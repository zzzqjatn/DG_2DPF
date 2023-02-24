using UnityEngine;

public class TimeCheck : MonoBehaviour
{
    public static TimeCheck instance;

    private bool IsPlay;
    private float InTime;

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

        IsPlay = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        playTimeAdd();
    }

    public void playTimeAdd()
    {
        if (IsPlay)
        {
            InTime += Time.deltaTime;
        }
    }

    public void playGameForTime(float beforePlayTime)
    {
        IsPlay = true;
        InTime = beforePlayTime;
    }
}
