using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour {

    public float damping = 1.5f;
    public float offsetY = 1f;
    [SerializeField] Player1 Player1;
    [SerializeField] Player2 Player2;
    private Vector3 position;

    void Start ()
    {
        position = (Player1.transform.position + Player2.transform.position) / 2;
        transform.position = new Vector3(position.x, position.y + offsetY, transform.position.z);
    }

    void Update () 
    {
        position = (Player1.transform.position + Player2.transform.position) / 2;
        if (!Player1 && !Player2) return;
        var target = new Vector3(position.x, position.y + offsetY, transform.position.z);
        var currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
        transform.position = currentPosition;
    }
}