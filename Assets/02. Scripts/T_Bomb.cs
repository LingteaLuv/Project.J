using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanBomb : MonoBehaviour
{
    //[SerializeField] private GameObject talismanPrefab;
    [SerializeField] private float liveTime;
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private GameObject effectPrefab;
    private float _timer;

    private void Awake()
    {
        Init();
    }
    
    private void OnEnable()
    {
        _timer = 0;
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
        //if (other.gameObject.layer == ignoreLayer) return;
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Init()
    {
        
    }
}
