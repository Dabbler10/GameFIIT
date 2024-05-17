using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth = 2;
    [SerializeField] public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
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

    void Die()
    {
        //GetComponent<Ghost>().enabled = false;
        Debug.Log("Ghost died!");
        GetComponent<Ghost>().gameObject.SetActive(false);
        Destroy(GetComponent<Ghost>()); // неверно находит объект??
        
    }
}
