using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private CircleCollider2D playerCircleCollider;
    private GameObject currentPlatform;
    private string number;

    private void Awake()
    {
        number = playerBoxCollider.gameObject.GetComponent<PlayerMovement>().Number;
    }

    void Update()
    {
        if (Input.GetButtonDown("Crouch" + number))
            if (currentPlatform != null)
            {
                Debug.Log("aboba");
                StartCoroutine(DisablecCollusion());
            }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            currentPlatform = other.gameObject;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            currentPlatform = null;
        }
    }

    private IEnumerator DisablecCollusion()
    {
        var platformCollider = currentPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, playerBoxCollider);
        Physics2D.IgnoreCollision(platformCollider, playerCircleCollider);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(platformCollider, playerBoxCollider, false);
        Physics2D.IgnoreCollision(platformCollider, playerCircleCollider, false);
    }
}
