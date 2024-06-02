using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryBackgroundSound : MonoBehaviour
{
    public AudioClip newMusic; 

    private GameObject audioSource;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music");
        audioSource.SetActive(false);
    }
    
}