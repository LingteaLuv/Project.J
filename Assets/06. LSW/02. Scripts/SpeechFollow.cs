using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 _offset = new Vector3(0f, 1f, 0f);
    private RectTransform _rectTransform;
    private Canvas _canvas;
    
    
    private void Awake()
    {
        Init();
    }

    private void LateUpdate()
    {
        if (target == null || _canvas == null) return;
        
        Vector3 worldPos = target.position + _offset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        
        if (_canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            _rectTransform.position = screenPos;
        }
        else
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                screenPos,
                _canvas.worldCamera,
                out Vector2 localPoint);

            _rectTransform.localPosition = localPoint;
        }
    }

    private void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        if (target == null)
        {
            Debug.LogWarning("SpeechFollow: target이 지정되지 않았습니다.");
            return;
        }
    }
}
