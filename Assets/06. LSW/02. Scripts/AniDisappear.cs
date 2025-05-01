using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        float duration = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, duration);
    }
}
