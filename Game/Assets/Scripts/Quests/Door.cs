using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public PlayerMovement player1;
    public PlayerMovement player2;
    public float openDistance = 0.03f; // Расстояние, на котором двери откроются
    private Quest quest;
    private Animator anim;
    private BoxCollider2D collider;

    private bool isOpen = false;
    private bool isClosed = true;

    private void Start()
    {
        quest = GetComponent<Quest>();
        var players = GameObject.FindGameObjectsWithTag("Player");
        (player1, player2) = (players[0].GetComponent<PlayerMovement>(), players[1].GetComponent<PlayerMovement>());
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (player1.transform.position.x > transform.position.x + 5 && player2.transform.position.x > transform.position.x + 5  && !quest.IsActive && !quest.IsCompleted)
        {
            CloseDoor();
            quest.StartQuest();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isClosed  && !quest.IsActive && quest.Previous == null || !quest.Previous.IsActive)
            OpenDoor();
    }

    private void OpenDoor()
    {
        (isOpen, isClosed) = (true, false);
        anim.SetBool("IsOpened", true);
        collider.isTrigger = true;
    }

    private void CloseDoor()
    {
        (isOpen, isClosed) = (false, true);
        anim.SetBool("IsOpened", false);
        collider.isTrigger = false;
    }
}