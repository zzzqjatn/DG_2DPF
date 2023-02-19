using System;
using System.Collections;
using UnityEngine;

public class CamerCon : MonoBehaviour
{
    public static CamerCon instance;

    public Vector3 CameraPos;

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

    private const float SET_SCREEN_WIDTH = 320.0f;
    private const float SET_SCREEN_HEIGHT = 180.0f;

    private float shakeTime = 0.2f;
    private float shakeSpeed = 5.0f;
    private float shakeAmount = 1.0f;

    private GameObject camera;

    //---------------------------
    private RectTransform target;
    public float smoothSpeed = 3;
    public Vector2 offset;
    public float limitMinX,limitMinY, limitMaxX,limitMaxY;

    private float cameraHalfWidth, cameraHalfHeight;

    void Start()
    {
        camera = GFunc.FindRootObj("Main Camera");
        target = GFunc.FindRootObj("GameObjs").FindChildObj("Player").Rect();

        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ShakingCamera());
        }
        CameraSmoothMove();
    }

    void Update()
    {

    }

    IEnumerator ShakingCamera()
    {
        Vector3 originalPos = camera.transform.localPosition;
        float elapsedTime = 0.0f;
        while(elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;
            camera.transform.localPosition = 
                Vector3.Lerp(camera.transform.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }
        camera.transform.localPosition = originalPos;
    }

    private void CameraSmoothMove()
    {
        Vector2 tempOffset = ReMatchPos(new Vector2(offset.x, offset.y));
        Vector2 tempLimitMin = ReMatchPos(new Vector2(limitMinX + cameraHalfWidth, limitMinY + cameraHalfHeight));
        Vector2 tempLimitMax = ReMatchPos(new Vector2(limitMaxX - cameraHalfWidth, limitMaxY - cameraHalfHeight));

        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + tempOffset.x, tempLimitMin.x, tempLimitMax.x) + SET_SCREEN_WIDTH / 2,
            Mathf.Clamp(target.position.y + tempOffset.y, tempLimitMin.y, tempLimitMax.y) + SET_SCREEN_HEIGHT / 2,
            -10.0f);

        camera.transform.position = Vector3.Lerp(camera.transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        CameraPos = camera.transform.position;    //카메라 중점
    }

    private Vector2 ReMatchPos(Vector2 inputPos)
    {
        float scaleX, scaleY;
        Vector2 Result = default;

        scaleX = Screen.width / SET_SCREEN_WIDTH;
        scaleY = Screen.height / SET_SCREEN_HEIGHT;

        Result = new Vector2(
            (inputPos.x / scaleX) - SET_SCREEN_WIDTH / 2,
            (inputPos.y / scaleY) - SET_SCREEN_HEIGHT / 2);

        return Result;
    }
}
