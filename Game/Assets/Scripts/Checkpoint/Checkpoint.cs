using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    private bool player1OnCheckpoint;
    private bool player2OnCheckpoint;
    [SerializeField] int index;
    [SerializeField] private Transform camera;
    
    
    void Update()
    {
        if (player2OnCheckpoint && player1OnCheckpoint)
            DataContainer.checkpointIndex = index;
    }

    private void Start()
    {
        if (DataContainer.checkpointIndex == index)
        {
            player1.position = transform.position;
            player2.position = transform.position;
            camera.position =new Vector3(transform.position.x, transform.position.y, camera.position.z);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Rigidbody2D>();
            var velocity = other.GetComponent<Rigidbody2D>().velocity.x;
            if (player.GameObject() == player1.GameObject() && velocity > 0)
                player1OnCheckpoint = true;
            else if (player.GameObject() == player2.GameObject() && velocity > 0)
                player2OnCheckpoint = true;
            else if (player.GameObject() == player1.GameObject() && velocity < 0)
                player1OnCheckpoint = false;
            else if (player.GameObject() == player2.GameObject() && velocity < 0)
                player2OnCheckpoint = false;
        }
    }
}
