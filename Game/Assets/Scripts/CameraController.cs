using System;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float damping = 1.5f;
    public float offsetY = 1f;
    [SerializeField] Transform Player1;
    [SerializeField] Transform Player2;
    public static Action<float> changeCameraSizeEvent;
    private Vector3 position;
    private Camera camera;
    private float camSize;

    void OnEnable()
    {
        position = (Player1.position + Player2.position) / 2;
        transform.position = new Vector3(position.x, position.y + offsetY, transform.position.z);
        camera = GetComponent<Camera>();
        changeCameraSizeEvent += ChangeCameraSize;
    }

    private void OnDisable()
    {
        changeCameraSizeEvent -= ChangeCameraSize;
    }

    void Update() 
    {
        position = (Player1.transform.position + Player2.transform.position) / 2;
        if (!Player1 && !Player2) return;
        var target = new Vector3(position.x, position.y + offsetY, transform.position.z);
        var currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
        transform.position = currentPosition;
    }

    void ChangeCameraSize(float newSize)
    {
        StopCoroutine(ChangeSize(newSize));
        camSize = camera.orthographicSize;
        StartCoroutine(ChangeSize(newSize));
    }

    private IEnumerator ChangeSize(float newSize)
    {
        if (Math.Abs(camera.orthographicSize - newSize) < 0.01f) yield break;

        for (var i = 0f; i < 1f; i += Time.deltaTime)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, newSize, EaseInOut(i));
            yield return null;
        }
    }

    float EaseInOut(float x) => x < 0.5 ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
}