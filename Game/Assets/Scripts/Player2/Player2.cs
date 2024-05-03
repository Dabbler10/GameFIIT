using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float offset;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Camera camera;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        camera = Camera.main;
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal2");
        var localScale = transform.localScale;
        var minCamBorder = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        var maxCamBorder = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
        
        // прыгает, если зажат W
        if (Input.GetKey(KeyCode.UpArrow) && IsGrounded())
            Jump();
        
        // если двигается вправо, лицом поворачивается вправо
        if (horizontalInput > 0.01f)
        {
            if (maxCamBorder.x - transform.position.x <= offset)
                horizontalInput = 0f;
            localScale = new Vector2(Math.Abs(localScale.x), localScale.y);
        }

        // если двигается влево, лицом поворачивается влево
        if (horizontalInput < -0.01f)
        {
            if (transform.position.x - minCamBorder.x <= offset)
                horizontalInput = 0f;
            localScale = new Vector2(-Math.Abs(localScale.x), localScale.y);
        }
        
        transform.localScale = localScale;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
    }
    
    private bool IsGrounded()
    {
        var bounds = boxCollider.bounds;
        var raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}