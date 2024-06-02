using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class BurnScript : MonoBehaviour
{
    private bool complete;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Torch"))
        {
            gameObject.GetComponent<PlayableDirector>().Play();
            var playerObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in playerObjects)
                player.SetActive(false);
            Invoke("MakeCompleteTrue", 4.0f);
        }
    }

    public bool IsCompleted()
    {
        return complete;
    }

    private void LoadEndScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void MakeCompleteTrue()
    {
        complete = true;
        Invoke("LoadEndScene", 1.0f);
    }
    
}
