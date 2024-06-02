using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}
