using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player2Attack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private Animator anim;
    private Player2 playerMovement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Player2>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
            Attack();
         
    }
    
    // если зажата клавиша Shift, игрок атакует
    private void Attack()
    {
        anim.SetTrigger("attack");
    }
}