using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class ScriptSetting
{
    public static IEnumerator WriteWords(TextMeshProUGUI text, string str, WaitForSeconds delay, Func<bool> skipRequested)
    {
        StringBuilder strText = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            strText.Append(str[i]);
            text.text = strText.ToString();
            if (skipRequested())
            {
                text.text = str;
                break;
            }
            yield return delay;
        }
    }

    public static Vector2 SetSize(TextMeshProUGUI text, string str)
    {
        text.text = str;
        LayoutRebuilder.ForceRebuildLayoutImmediate(text.rectTransform);
        Vector2 preferredSize = text.GetPreferredValues(str);
        float paddingX = 0f;
        float paddingY = 30f;
        
        text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,preferredSize.x);
        text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,preferredSize.y);
        
        return new Vector2(preferredSize.x + paddingX, preferredSize.y + paddingY);
    }
}
