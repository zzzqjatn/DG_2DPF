using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using Unity.Mathematics;
using UnityEngine;

using UnityEngine.SceneManagement;

public static partial class GFunc
{
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
}
