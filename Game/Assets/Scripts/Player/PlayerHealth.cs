using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10;
    private GameState gameState;
    public float currentHealth { get; private set; }
    private Animator anim;

    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        gameState = FindObjectOfType<GameState>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
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