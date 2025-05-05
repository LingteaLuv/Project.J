using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int CurHealth;
    private int MaxHealth = 5;
    [SerializeField] private GameObject Flag;
    [SerializeField] private GameObject[] Flags;
    [SerializeField] private List<Animator> animator = new List<Animator>();
    [SerializeField] private bool isHpdamage = false;
    [SerializeField] private int damage;


    private void Awake()
    {
        CurHealth = MaxHealth;
    }

    void Update()
    {
        if (isHpdamage)
        {
            TakeDamage(damage);
            isHpdamage = false;
        }
    }

    private void TakeDamage(int damage)
    {
        CurHealth -= damage;
        Hp();
        if (CurHealth <= 0)
        {
            Die();
        }
    }

    private void Hp()
    {

        if (CurHealth == 4)
        {
            StartCoroutine(Delay(animator[0], "Hp5", Flags[0]));
        }

        else if (CurHealth == 3)
        {
            StartCoroutine(Delay(animator[1], "Hp4", Flags[1]));
        }
        
        else if (CurHealth == 2)
        {
            StartCoroutine(Delay(animator[2], "Hp3", Flags[2]));
        }
        else if (CurHealth == 1)
        {
            StartCoroutine(Delay(animator[3], "Hp2", Flags[3]));
        }
    }

    private void Die()
    {
        CurHealth = 0;
        StartCoroutine(Delay(animator[4], "Hp1", Flags[4]));
    }

    private IEnumerator Delay(Animator animator, string name, GameObject obj)
    {
        animator.SetTrigger(name);
        yield return new WaitForSeconds(0.833f);
        obj.SetActive(false);
    }



}
