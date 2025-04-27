using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    // 폭발 부적
    [SerializeField] private TalismanBomb talismanBomb;
    // 순간이동 부적
    [SerializeField] private TalismanTeleport talismanTeleport;
    
    // 부적 풀?
    // 부적 소환 위치
    [SerializeField] private Transform spawnPos;
    // 날아가는 속도
    [SerializeField] private float moveSpeed;
    // 부적 발사 여부
    private bool _isFiring;
    // 순간이동 콜백함수

    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _isFiring = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isFiring)
        {
            FireLeft();
            _isFiring = false;
        }
    }
    private void FireLeft()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        TalismanBomb instance = Instantiate(talismanBomb, spawnPos.position, Quaternion.identity);
        instance.transform.position = spawnPos.position;
        Vector3 fireDir = (mousePos - instance.transform.position).normalized;
        Rigidbody2D rigid = instance.GetComponent<Rigidbody2D>();
        rigid.velocity = fireDir * moveSpeed;
    }

    private void FireRight()
    {
        
    }

    private void Init()
    {
        _isFiring = false;
    }
    
}
