using System;
using Unity.VisualScripting;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    public bool playersInRoom { get; private set; }
    private Rigidbody2D player1;
    private Rigidbody2D player2;
    private bool player1InRoom;
    private bool player2InRoom;

    private void Start()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        (player1, player2) = (players[0].GetComponent<Rigidbody2D>(), players[1].GetComponent<Rigidbody2D>());
    }

    private void Update()
    {
        if (player1InRoom && player2InRoom)
        {
            playersInRoom = true;
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Rigidbody2D>();
            var velocity = other.GetComponent<Rigidbody2D>().velocity.x;
            if (player.GameObject() == player1.GameObject() && velocity > 0)
                player1InRoom = true;
            else if (player.GameObject() == player2.GameObject() && velocity > 0)
                player2InRoom = true;
            else if (player.GameObject() == player1.GameObject() && velocity < 0)
                player1InRoom = false;
            else if (player.GameObject() == player2.GameObject() && velocity < 0)
                player2InRoom = false;
        }
    }
    
}