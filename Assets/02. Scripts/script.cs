using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class script : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private WaitForSeconds _delay;
    
    private void OnEnable()
    {
        Init();
        StartCoroutine(ScriptSetting.WriteWords(text, "안녕하세요 저는 원숭이 강소현이라고 합니다.", _delay));
    }

    private void Init()
    {
        _delay = new WaitForSeconds(0.05f);
    }

    public void OnClickSkipButton()
    {
        ScriptSetting.SkipRequested = true;
    }
}
