using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // двигается по гирозонтали
        var horizontalInput = Input.GetAxis("Horizontal2");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        // прыгает, если зажат W
        if (Input.GetKey(KeyCode.UpArrow) && grounded)
            Jump();
        

        // если двигается вправо, лицом поворачивается вправо
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y); 
        // если двигается влево, лицом поворачивается влево
        if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y); 
        
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D colllision)
    {
        if (colllision.gameObject.CompareTag("Ground"))
            grounded = true;
    }
    void Start()
    {
        
    }
}