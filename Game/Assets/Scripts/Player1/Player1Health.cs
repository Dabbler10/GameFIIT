using System.Collections;
using UnityEngine;

public class Player1Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10;
    public float currentHealth { get; private set; }
    private bool dead;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!dead)
        {
            GetComponent<Player1>().enabled = false;
        }
    }
}