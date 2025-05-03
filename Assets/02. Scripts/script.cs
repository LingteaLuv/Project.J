using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform img;
    [SerializeField] private GameObject speechBubble;
    private WaitForSeconds _delay;
    private bool _skipRequested;
    
    private void OnEnable()
    {
        Init();
        StartCoroutine(TypeScripts());
    }

    private void Init()
    {
        _delay = new WaitForSeconds(0.05f);
    }

    private IEnumerator TypeScripts()
    {
        string[] scripts = Scripts(3);
        for (int i = 0; i < scripts.Length; i++)
        {
            string str = scripts[i];
            img.sizeDelta = ScriptSetting.SetSize(text, str);
            yield return StartCoroutine(ScriptSetting.WriteWords(text, str, _delay, () => _skipRequested));
            _skipRequested = false;
            yield return new WaitForSeconds(1f);
        }
        HideSpeechBubble();
    }

    private string[] Scripts(int num)
    {
        string[] scripts = new string[num];
        scripts[0] = "원숭이 강현숭";
        scripts[1] = "이 몸 등장";
        scripts[2] = "나를 막을 자는 누구인가";
        return scripts;
    }

    private void HideSpeechBubble()
    {
        speechBubble.SetActive(false);
    }
    
    public void OnClickSkipButton()
    {
        _skipRequested = true;
    }
}
