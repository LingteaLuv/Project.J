using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanTeleport : MonoBehaviour
{
    [SerializeField] private float liveTime;
    [SerializeField] private LayerMask ignoreLayer;
    private float _timer;
    
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
        if (other.gameObject.layer == ignoreLayer) return;
        
        Destroy(gameObject);
    }
}
