using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GunManager : MonoBehaviour
{
    [Header("Drag&Drop")]
    [Tooltip("Rigidbody2D")]
    [SerializeField] private Rigidbody2D rigid;
    [Tooltip("T_Bomb")]
    [SerializeField] private TalismanBomb talismanBomb;
    [Tooltip("T_Teleport")]
    [SerializeField] private TalismanTeleport talismanTeleport;
    [Tooltip("부적 소환 위치")]
    [SerializeField] private Transform spawnPos;
    [Tooltip("teleport 이펙트 프리팹")]
    [SerializeField] private GameObject effectPrefab;
    [Tooltip("teleport 잔상 프리팹")]
    [SerializeField] private GameObject afterImage;
    
    [Header("Number")]
    [Tooltip("폭발 부적 날아가는 속도")]
    [SerializeField] private float bombMoveSpeed;
    [Tooltip("이동 부적 날아가는 속도")]
    [SerializeField] private float teleportMoveSpeed;
    [Tooltip("이동 부적 쿨타임")]
    [SerializeField] private float teleportCooldown = 2f;

    // 마지막으로 이동 부적을 날린 시각
    private float _lastTeleportFireTime = -Mathf.Infinity;
    
    // 폭발 부적 발사 여부
    private bool _isBombFiring;
    // 이동 부적 발사 여부
    private bool _isTeleportFiring;
    // 텔레포트 사용 여부
    private bool _isTeleport;
    private TalismanTeleport _instance;
    private float _originalGravity;
    private Coroutine _zeroGravityRoutine;
    

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (!_isTeleportFiring && Input.GetMouseButtonDown(0))
        {
            if (_isTeleport || Time.time > _lastTeleportFireTime + teleportCooldown)
            {
                _isTeleport = false;
                _lastTeleportFireTime = Time.time;
                _isTeleportFiring = true;
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            _isBombFiring = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Teleport();
        }
    }

    private void FixedUpdate()
    {
        if (_isTeleportFiring)
        {
            FireLeft();
            _isTeleportFiring = false;
        }
        
        if (_isBombFiring)
        {
            FireRight();
            _isBombFiring = false;
        }
    }
    private void FireRight()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        TalismanBomb instance = Instantiate(talismanBomb, spawnPos.position, Quaternion.identity);
        instance.transform.position = spawnPos.position;
        Vector3 fireDir = (mousePos - instance.transform.position).normalized;
        Rigidbody2D rigid = instance.GetComponent<Rigidbody2D>();
        rigid.velocity = fireDir * bombMoveSpeed;
    }

    private void FireLeft()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        _instance = Instantiate(talismanTeleport, spawnPos.position, Quaternion.identity);
        _instance.transform.position = spawnPos.position;
        Vector3 fireDir = (mousePos - _instance.transform.position).normalized;
        Rigidbody2D rigid = _instance.GetComponent<Rigidbody2D>();
        rigid.velocity = fireDir * teleportMoveSpeed;
    }

    private void Teleport()
    {
        if (_instance != null)
        {
            if (transform.localScale.x >= 0)
            {
                Instantiate(afterImage, (transform.position - new Vector3(0,0.4f,0)), transform.rotation);
            }
            else
            {
                afterImage.transform.localScale = new Vector3(-1, 1, 1);
                Instantiate(afterImage, (transform.position - new Vector3(0,0.4f,0)), transform.rotation);
                afterImage.transform.localScale = new Vector3(1, 1, 1);
            }
            transform.position = _instance.transform.position;
            Instantiate(effectPrefab, (transform.position - new Vector3(0,0.8f,0)), Quaternion.identity);
            rigid.velocity = Vector2.zero;
            if (_zeroGravityRoutine != null)
            {
                StopCoroutine(_zeroGravityRoutine);
            }
            _zeroGravityRoutine = StartCoroutine(ZeroGravity());
            Destroy(_instance.gameObject);
            _isTeleport = true;
        }
    }

    private IEnumerator ZeroGravity()
    {
        rigid.gravityScale = 0f;
        float timer = 0.2f;
        float duration = 0.8f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            rigid.gravityScale = Mathf.Lerp(0.2f,_originalGravity,t);
            yield return null;
        }
        rigid.gravityScale = _originalGravity;
    }
    
    private void Init()
    {
        _isBombFiring = false;
        _isTeleportFiring = false;
        rigid = GetComponent<Rigidbody2D>();
        _originalGravity = rigid.gravityScale;
    }
}
