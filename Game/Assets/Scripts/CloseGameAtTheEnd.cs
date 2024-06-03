using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGameAtTheEnd : MonoBehaviour{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            Application.Quit();
    }
}
