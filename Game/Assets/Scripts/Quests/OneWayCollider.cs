using System;
using UnityEngine;

public class OneWayCollider : MonoBehaviour
{
    private int countOfPlayers;
    public bool playersInRoom { get; private set; }

    private void Update()
    {
        if (countOfPlayers == 2)
            playersInRoom = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект, который входит в триггер, игроком
        if (other.CompareTag("Player") && other.GetComponent<Rigidbody2D>().velocity.x > 0 && countOfPlayers < 2)
            countOfPlayers += 1;
        else if (other.CompareTag("Player") && other.GetComponent<Rigidbody2D>().velocity.x < 0 && countOfPlayers < 2)
            countOfPlayers -= 1;
        else if (other.CompareTag("Player") && countOfPlayers == 2)
            GetComponent<Collider2D>().isTrigger = false;

        Debug.Log(countOfPlayers);
    }
}