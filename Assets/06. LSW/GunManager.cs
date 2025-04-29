using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GunManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    // 폭발 부적
    [SerializeField] private TalismanBomb talismanBomb;
    // 순간이동 부적
    [SerializeField] private TalismanTeleport talismanTeleport;
    // 부적 소환 위치
    [SerializeField] private Transform spawnPos;
    // 폭발 부적 날아가는 속도
    [SerializeField] private float bombMoveSpeed;
    // 이동 부적 날아가는 속도
    [SerializeField] private float teleportMoveSpeed;
    // teleport 이펙트 프리팹
    [SerializeField] private GameObject effectPrefab;
    // teleport 잔상 프리팹
    [SerializeField] private GameObject afterImage;
    // 폭발 부적 발사 여부
    private bool _isBombFiring;
    // 이동 부적 발사 여부
    private bool _isTeleportFiring;
    private TalismanTeleport _instance;
    private float _originalGravity;
    // private WaitForSeconds _airTime;
    private Coroutine _zeroGravityRoutine;
    

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isTeleportFiring = true;
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
        rigid = GetComponent<Rigidbody2D>();
        //_airTime = new WaitForSeconds(0.5f);
        _originalGravity = rigid.gravityScale;
    }
}
