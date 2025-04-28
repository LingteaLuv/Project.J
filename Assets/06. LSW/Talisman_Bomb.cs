using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanBomb : MonoBehaviour
{
    //[SerializeField] private GameObject talismanPrefab;
    [SerializeField] private float liveTime;
    [SerializeField] private LayerMask ignoreLayer;
    private float _timer;

    private Animator _animator;

    private void Awake()
    {
        Init();
    }
    
    private void OnEnable()
    {
        _timer = 0;
        //_animator.SetBool(0, true);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > liveTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == ignoreLayer) return;
        
        Destroy(gameObject);
    }

    private void Init()
    {
        _animator = GetComponent<Animator>();
    }
}
