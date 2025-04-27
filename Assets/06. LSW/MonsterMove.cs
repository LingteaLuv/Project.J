using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private float moveSpeed;
    private WaitForSeconds _timer;
    private Coroutine _moveRoutine;
    private WaitForSeconds _stopTimer;
    private Vector3 _originLook;
    
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        _moveRoutine = StartCoroutine(MoveRoutine());
    }

    private void Update()
    {

    }
    
    private void OnDisable()
    {
        if (_moveRoutine != null)
        {
            StopCoroutine(_moveRoutine);
        }
    }

    private IEnumerator MoveRoutine()
    {
        while (true) // 무한 반복
        {
            // 오른쪽으로 이동
            rigid.velocity = moveSpeed * Vector2.right;
            transform.localScale = new Vector3(-_originLook.x, _originLook.y, _originLook.z);
            yield return _timer;

            rigid.velocity = Vector2.zero;
            yield return _stopTimer;

            // 왼쪽으로 이동
            rigid.velocity = moveSpeed * Vector2.left;
            transform.localScale = new Vector3(_originLook.x, _originLook.y, _originLook.z);
            yield return _timer;
            
            rigid.velocity = Vector2.zero;
            yield return _stopTimer;
        }
    }
    
    private void Init()
    {
        rigid = GetComponent<Rigidbody2D>();
        _timer = new WaitForSeconds(3f);
        _stopTimer = new WaitForSeconds(0.5f);
        _originLook = transform.localScale;
    }
}
