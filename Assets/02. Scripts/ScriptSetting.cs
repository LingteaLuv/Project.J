using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class ScriptSetting
{
    public static bool SkipRequested = false;
    public static IEnumerator WriteWords(TextMeshProUGUI text, string str, WaitForSeconds delay)
    {
        text.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            text.text += str[i];
            if (SkipRequested)
            {
                text.text = str;
                break;
            }
            yield return delay;
        }
        SkipRequested = false;
    }
}
