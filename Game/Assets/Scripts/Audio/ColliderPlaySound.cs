using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        SoundManager.instance.PlaySound(sound);
    }
}
