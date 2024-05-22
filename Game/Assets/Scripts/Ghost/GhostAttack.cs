using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    private float attackRate = 0.5f;
    private float nextAttackTime = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time >= nextAttackTime)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Debug.Log("aboba");
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                nextAttackTime = Time.time + attackRate;
            }
        }
    }
}