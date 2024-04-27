using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float offset;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private Camera camera;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        camera = Camera.main;
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal2");
        var localScale = transform.localScale;
        var min = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        var max = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
        
        // прыгает, если зажат W
        if (Input.GetKey(KeyCode.UpArrow) && grounded)
            Jump();
        
        // если двигается вправо, лицом поворачивается вправо
        if (horizontalInput > 0.01f)
        {
            if (max.x - transform.position.x <= offset)
                horizontalInput = 0f;
            localScale = new Vector2(Math.Abs(localScale.x), localScale.y);
        }

        // если двигается влево, лицом поворачивается влево
        if (horizontalInput < -0.01f)
        {
            if (transform.position.x - min.x <= offset)
                horizontalInput = 0f;
            localScale = new Vector2(-Math.Abs(localScale.x), localScale.y);
        }
        
        transform.localScale = localScale;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
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