using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 14;
    private GameState gameState;
    public float currentHealth { get; private set; }
    private Animator anim;
    private const float deltaTimeForTakingDamage = 1f;
    private const float deltaTimeForRecovery = 5f;
    private float timerTakinDamage;
    private float timerRecovery;
    
    private void Update()
    {
        if (timerRecovery > deltaTimeForRecovery && currentHealth < maxHealth)
        {
            currentHealth += 1;
            timerRecovery = 0f;
        }

        if (currentHealth < maxHealth)
        {
            timerTakinDamage += Time.deltaTime;
            timerRecovery += Time.deltaTime;
        }

    }

    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        gameState = FindObjectOfType<GameState>();
    }

    public void TakeDamage(float damage)
    {
        if (timerTakinDamage > deltaTimeForTakingDamage || Math.Abs(currentHealth - maxHealth) < Math.E)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            timerTakinDamage = 0f;
        }

        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        anim.SetFloat("Speed", 0f);
        StartCoroutine(HandleDeath());
    }

    IEnumerator HandleDeath()
    {
        anim.SetTrigger("dead");
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            yield return null;
        gameState.GameOver();
    }
}