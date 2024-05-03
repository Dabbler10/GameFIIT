using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float offset;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Transform anotherPlayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Camera camera;
    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        anotherPlayer = GetComponent<Player2>().transform;
        camera = Camera.main;
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal1");
        var localScale = transform.localScale;
        var minCamBorder = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        var maxCamBorder = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
        
        // прыгает, если зажат W
        if (Input.GetKey(KeyCode.W) && IsGrounded())
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

        ChangeCameraSize();
        transform.localScale = localScale;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
        
        //print(OnWall());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
    }

    // если расстояние между игроками по Y больше 5, камера отдаляется
    private void ChangeCameraSize()
    {
        if (Math.Abs(transform.position.y - anotherPlayer.position.y) > 5)
            CameraController.changeCameraSizeEvent?.Invoke(10);
        else
            CameraController.changeCameraSizeEvent?.Invoke(7);
    }

    private bool IsGrounded()
    {
        var bounds = boxCollider.bounds;
        var raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    
    private bool OnWall()
    {
        var bounds = boxCollider.bounds;
        var raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
