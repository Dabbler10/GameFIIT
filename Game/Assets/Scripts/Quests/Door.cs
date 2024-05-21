using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public PlayerMovement player1;
    public PlayerMovement player2;
    public float openDistance = 0.03f; // Расстояние, на котором двери откроются
    private Quest quest;

    private bool isOpen = false;
    private bool isClosed = true;

    private void Start()
    {
        quest = GetComponent<Quest>();
        var players = GameObject.FindGameObjectsWithTag("Player");
        (player1, player2) = (players[0].GetComponent<PlayerMovement>(), players[1].GetComponent<PlayerMovement>());
    }

    private void Update()
    {
        // Проверяем, завершен ли все предыдущий квест
        if (quest.Previous == null || !quest.Previous.IsActive)
        {
            // Проверяем, находятся ли игроки рядом с дверью
            if (Vector3.Distance(player1.transform.position, transform.position) < openDistance ||
                Vector3.Distance(player2.transform.position, transform.position) < openDistance)
            {
                OpenDoor();
            }
        }

        // Проверяем, вошли ли оба игрока в комнату
        if (IsBothPlayersInRoom() && !quest.IsActive && !quest.IsCompleted)
        {
            CloseDoor();
            quest.StartQuest();
        }
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            // здесь должна быть анимация открытия двери
            (isOpen, isClosed) = (true, false);
        }
    }

    private void CloseDoor()
    {
        if (!isClosed)
        {
            // здесь должна быть анимация закрытия двери
            (isOpen, isClosed) = (false, true);
        }
    }

    private bool IsBothPlayersInRoom()
    {
        var player1Position = player1.transform.position;
        var player2Position = player2.transform.position;
        return transform.position.x < player1Position.x && transform.position.x < player2Position.x;
    }
}