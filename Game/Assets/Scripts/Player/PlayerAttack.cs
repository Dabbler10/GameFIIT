using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public PlayerMovement playerMovement;

    [Header("Attack Parameters")] [SerializeField]
    private float range = 0.5f;

    [SerializeField] private int damage;
    [SerializeField] private float pushPower;

    [Header("Collider Parameters")] [SerializeField]
    private float colliderDistance;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] Transform attackPoint;
    public LayerMask enemyLayers;


    //References
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack" + playerMovement.Number))
            Attack();
    }

    // если зажата клавиша E, игрок атакует
    private void Attack()
    {
        anim.SetTrigger("attack");
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            var enemyHealth = enemy.GetComponent<GhostHealth>();
            enemyHealth.TakeDamage(damage);
            enemyHealth.PushAway(transform.position, pushPower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}