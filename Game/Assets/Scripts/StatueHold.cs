using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatueHold : MonoBehaviour
{
    private GameObject holdPart;
    private bool hold;
    public float range;
    public Transform holdPoint;
    public float throwPower;
    public LayerMask statueLayer;
    public int number;
    [SerializeField] private AudioClip assembleStatueSound;
    [SerializeField] private AudioClip collectStatueSound;
    
    void Update()
    {
        if (Input.GetButtonDown("PickUp" + number))
        {
            if (!hold)
            {
                var hit = Physics2D.OverlapCircleAll(holdPoint.position, range, statueLayer).FirstOrDefault();
                if (hit != null && (((hit.gameObject.CompareTag("StatueHead") || hit.gameObject.CompareTag("StatueArm") || hit.gameObject.CompareTag("Torch")) && hit.gameObject.GetComponent<BoxCollider2D>().isTrigger == false)
                    || (hit.gameObject.GetComponent<BoxCollider2D>().isTrigger && hit.gameObject.CompareTag("Torch"))))
                {
                    SoundManager.instance.PlaySound(collectStatueSound);
                    hold = true;
                    holdPart = hit.gameObject;
                    holdPart.GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
            else
            {
                SoundManager.instance.PlaySound(assembleStatueSound);
                hold = false;
                holdPart.GetComponent<BoxCollider2D>().isTrigger = false;
                holdPart.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                holdPart.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(transform.localScale.x, 1) * throwPower;
            }
        }

        if (hold)
        {
            holdPart.gameObject.transform.position = holdPoint.position;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(holdPoint.position, range);
    }
    
}
