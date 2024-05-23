using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public PlayerMovement player1;
    public PlayerMovement player2;
    public float openDistance = 5f; // Расстояние, на котором двери откроются
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
        // Проверяем, завершен ли все предыдущий квест
        if (quest.Previous == null || !quest.Previous.IsActive)
        {
            // Проверяем, находятся ли игроки рядом с дверью
            Debug.Log(Vector3.Distance(player1.transform.position, transform.position) < openDistance);
            if (Vector3.Distance(player1.transform.position, transform.position) < openDistance ||
                Vector3.Distance(player2.transform.position, transform.position) < openDistance)
            {
                OpenDoor();
            }
        }

        // Проверяем, вошли ли оба игрока в комнату
        if (IsBothPlayersInRoom() && !isClosed && !quest.IsCompleted)
        {
            CloseDoor();
            quest.StartQuest();
        }
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

    private bool IsBothPlayersInRoom() => GetComponent<DoorCollider>().playersInRoom;
}