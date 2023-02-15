using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using Unity.Mathematics;
using UnityEngine;

using TMPro;

public static partial class GFunc
{
    public static void SetTxt(this GameObject obj,string txt)
    {
        TMP_Text TargetTxt = default;
        TargetTxt = obj.GetComponent<TMP_Text>();

        TargetTxt.text = txt;
    }

    public static string GetInputFieldTxt(this GameObject obj)
    {
        TMP_InputField TargetTxt = default;
        string ResultTxt = "";
        TargetTxt = obj.GetComponent<TMP_InputField>();

        ResultTxt = TargetTxt.text;

        return ResultTxt;
    }

    public static string IntInputFieldLimit(this GameObject obj,int min,int max)
    {
        TMP_InputField TargetTxt = default;
        TargetTxt = obj.GetComponent<TMP_InputField>();
        
        string ResultTxt = "";
        int temp = 0;

        ResultTxt = TargetTxt.text;

        int.TryParse(ResultTxt, out temp);

        if(temp > max) 
        {
            temp = max;
            ResultTxt = temp.ToString();
            TargetTxt.text = ResultTxt;
        }
        else if (temp < min) 
        {
            temp = min;
            ResultTxt = temp.ToString();
            TargetTxt.text = ResultTxt;
        }

        return ResultTxt;
    }
}
