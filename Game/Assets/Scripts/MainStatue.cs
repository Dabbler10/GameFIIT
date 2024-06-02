using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainStatue : MonoBehaviour
{
    public LayerMask statueLayers;
    public Transform HeadPoint;
    public Transform Arm1Point;
    public Transform Arm2Point;
    
    private float range = 1.7f;
    private bool head;
    private bool arm1;
    private bool arm2;
    
    
    void Update()
    {
        var statueParts = Physics2D.OverlapCircleAll(transform.position + Vector3.up * 1.7f, range, statueLayers);
        foreach (var part in statueParts)
        {
            if (part.gameObject.CompareTag("StatueHead") && part.gameObject.GetComponent<BoxCollider2D>().isTrigger == false)
            {
                part.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                GameObject o;
                (o = part.gameObject).GetComponent<Rigidbody2D>().bodyType = (RigidbodyType2D)2;
                o.transform.position = HeadPoint.position;
                o.transform.rotation = HeadPoint.rotation;
                head = true;
            }
            
            if (part.gameObject.CompareTag("StatueArm") && part.gameObject.GetComponent<BoxCollider2D>().isTrigger == false)
            {
                part.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                part.gameObject.GetComponent<Rigidbody2D>().bodyType = (RigidbodyType2D)2;
                if (!arm1)
                {
                    Debug.Log("aboba");
                    var o = part.gameObject;
                    o.transform.position = Arm1Point.position;
                    o.transform.rotation = Arm1Point.rotation;
                    arm1 = true;
                }
                else if (!arm2)
                {
                    var o = part.gameObject;
                    o.transform.position = Arm2Point.position;
                    o.transform.rotation = Arm2Point.rotation;
                    arm2 = true;
                }
            }
        }
    }

    public bool IsCompleted()
    {
        return head && arm1 && arm2;
    }
}
