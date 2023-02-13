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
}
