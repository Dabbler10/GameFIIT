using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHold : MonoBehaviour
{
    private GameObject holdStatuePart;
    private bool hold;
    private RaycastHit2D hit;
    public float distance;
    public Transform holdPoint;
    public float throwPower;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!hold)
            {
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position - new Vector3(0, 2, 0), Vector2.right * transform.localScale.x, distance);
                if (hit.collider != null && (hit.collider.gameObject.CompareTag("StatueHead") || hit.collider.gameObject.CompareTag("StatueArm")) && hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger == false)
                {
                    hold = true;
                    holdStatuePart = hit.collider.gameObject;
                    holdStatuePart.GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
            else
            {
                hold = false;
                holdStatuePart.GetComponent<BoxCollider2D>().isTrigger = false;
                holdStatuePart.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(transform.localScale.x, 1) * throwPower;
            }
        }

        if (hold)
        {
            holdStatuePart.gameObject.transform.position = holdPoint.position;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position - new Vector3(0, 2, 0), transform.position - new Vector3(0, 2, 0) + Vector3.right * transform.localScale.x * distance);
    }
}
