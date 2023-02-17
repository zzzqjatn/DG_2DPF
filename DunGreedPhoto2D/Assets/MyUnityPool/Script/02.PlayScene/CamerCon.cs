using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.U2D.IK;
using UnityEngine;

public class CamerCon : MonoBehaviour
{
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
        target = GFunc.FindRootObj("playerCanVas").FindChildObj("Player").Rect();

        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        //CameraSmoothMove();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ShakingCamera());
        }
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
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp((target.position.x) + offset.x, limitMaxX + cameraHalfWidth, limitMaxX - cameraHalfWidth),
            Mathf.Clamp((target.position.y) + offset.y, limitMaxY + cameraHalfHeight, limitMaxY - cameraHalfHeight),
            -10.0f);
        camera.transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
}
