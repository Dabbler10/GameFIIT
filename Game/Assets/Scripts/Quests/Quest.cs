using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public bool IsActive { get; protected set; }
    public bool IsCompleted { get; protected set; }
    [SerializeField] public Quest Previous;
    internal PlayerMovement player1;
    internal PlayerMovement player2;
    private string numberPlayer1, numberPlayer2;
    [SerializeField] public int QuestNumber;

    public virtual void StartQuest()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        (player1, player2) = (players[0].GetComponent<PlayerMovement>(), players[1].GetComponent<PlayerMovement>());
        (numberPlayer1, numberPlayer2) = (player1.Number, player2.Number);
        IsActive = true;
        FindObjectsOfType<Note>(true)[0].Appear(QuestNumber, 1);
            
    }

    public virtual void CompleteQuest()
    {
        IsActive = false;
        IsCompleted = true;
        (player1.Number, player2.Number) = (numberPlayer1, numberPlayer2);
        // убрать из конца? Оставить только в начале квеста? Пока нет 2х квестов, будет так
        FindObjectsOfType<Note>(true)[0].Appear(QuestNumber, 2);
    }
}