using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.tag == "Player1" || collision.tag == "Player2")
        if (collision.tag == "Player")
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}