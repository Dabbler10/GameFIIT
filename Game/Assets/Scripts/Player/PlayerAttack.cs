using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    private Player playerMovement;

    [Header("Attack Parameters")] [SerializeField]
    private float range = 2;

    [SerializeField] private int damage;

    [Header("Collider Parameters")] [SerializeField]
    private float colliderDistance;

    [SerializeField] private BoxCollider2D boxCollider;


    //References
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Player>();
    }

    private void Update()
    {
        if (playerMovement.attack == "E" && Input.GetKeyDown(KeyCode.E))
            Attack();
        if (playerMovement.attack == "Shift" && Input.GetKeyDown(KeyCode.RightShift))
            Attack();
    }

    // если зажата клавиша E, игрок атакует
    private void Attack()
    {
        anim.SetTrigger("attack");
        var hits = GetTargetsInSight();
        if (hits.Count != 0)
        {
            foreach (var ghost in hits)
            {
                ghost.GetComponent<GhostHealth>().TakeDamage(damage);
            }
        }
    }

    private List<GameObject> GetTargetsInSight()
    {
        var targetsInSight = new List<GameObject>();

        // Создаем луч из центра камеры в направлении, в котором направлен игрок
        var hits = Physics.RaycastAll(transform.position, transform.forward, range, 9);

        // Проходим по всем пересечениям луча
        if (hits != null)
        {
            foreach (RaycastHit hit in hits)
            {
                targetsInSight.Add(hit.transform.gameObject);
            }
        }

        return targetsInSight;
    }
}