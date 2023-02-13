using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

using UnityEngine.SceneManagement;

public enum PopType { OFF, ON }

public static partial class GFunc
{
    public static void PopUpControl(this GameObject rootObjs, string popupName, PopType popSwitch)
    {
        GameObject Target = default;

        Target = rootObjs.FindChildObj(popupName);

        if (Target == null || Target == default) return;

        switch(popSwitch)
        {
            case PopType.OFF:
                Target.Rect().localScale = new Vector3(0.00001f, 0.00001f, 0.00001f);
                Target.RectLocalPosSet(new Vector3(-1f - Target.RectSize().x * 0.5f,
                                                    -1f - Target.RectSize().y * 0.5f, 0.0f));
                //Target.SetActive(false);
                break;
            case PopType.ON:
                Target.Rect().localScale = new Vector3(1f, 1f, 1f);
                Target.RectLocalPosSet(new Vector3(0.0f,0.0f,0.0f));
                //Target.SetActive(true);
                break;
        }
    }

    public static GameObject FindChildObj(this GameObject rootObjs,string objName)
    {
        GameObject Result = default;
        GameObject Target = default;

        for(int i = 0; i < rootObjs.transform.childCount; i++)
        {
            Target = rootObjs.transform.GetChild(i).gameObject;
            if(Target.name.Equals(objName))
            {
                Result = Target;
                return Result;
            }
            else
            {
                Result = FindChildObj(Target, objName);
                if(Result == default || Result == null) { /* Pass */ }
                else { return Result; }
            }
        }

        return Result;
    }

    public static GameObject FindRootObj(string objName)
    {
        GameObject Result = default;
        GameObject[] RootObjs = GFunc.GetActiveScene().GetRootGameObjects();

        foreach(GameObject obj in RootObjs)
        {
            if(obj.name.Equals(objName))
            {
                Result = obj;
                return Result;
            }
            else { continue; }
        }

        return Result;
    }

    //! Scene Load 하기
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //! 현재 활성화된 Scene 창 가져오기
    public static Scene GetActiveScene()
    {
        Scene Result = default;
        Result = SceneManager.GetActiveScene();
        return Result;
    }

    //! 종료하는 함수
    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
