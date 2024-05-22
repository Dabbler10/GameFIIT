using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    public string Number;
    
    private float horizontalMove = 0f;
    private bool jump;
    private bool crouch;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal" + Number) * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump" + Number))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch" + Number))
            crouch = true;
        else if (Input.GetButtonUp("Crouch" + Number))
            crouch = false;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void OnLanding1()
    {
        animator.SetBool("IsJumping", false);
    }
    
    public void OnLanding2()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching1(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    
    public void OnCrouching2(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
}