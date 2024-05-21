using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBorders : MonoBehaviour
{
    public GameObject objBorderTopRightCorner;
    public GameObject objBorderBottomLeftCorner;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    private void FixedUpdate()
    {
        SetupBorders();
    }

    void SetupBorders()
    {
        var point = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));
        objBorderTopRightCorner.transform.position = point;
        
        point = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        objBorderBottomLeftCorner.transform.position = point;
    }
}
