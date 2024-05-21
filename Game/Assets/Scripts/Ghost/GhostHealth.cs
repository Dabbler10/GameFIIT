using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth = 2;
    [SerializeField] public float currentHealth;
    private Rigidbody2D rBody;
    
    private void Awake()
    {
        currentHealth = maxHealth;
        rBody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        //currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void PushAway(Vector3 pushFrom, float pushPower)
    {
        if (rBody == null || pushPower == 0)
            return;

        var pushDirection = (pushFrom - transform.position).normalized;

        // Толкаем объект в нужном направлении с силой pushPower
        rBody.AddForce(pushDirection * pushPower);
    }

    void Die()
    {
        Debug.Log("Ghost died!");
        GetComponent<Ghost>().gameObject.SetActive(false);
        Destroy(GetComponent<Ghost>()); // неверно находит объект??
        
    }
}
