using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public bool IsActive { get; protected set; }
    public bool IsCompleted { get; protected set; }
    public Quest Previous;
    internal PlayerMovement player1;
    internal PlayerMovement player2;
    private string numberPlayer1, numberPlayer2;
    public Note ImageNote;
    public Sprite StartNoteImage;

    public virtual void StartQuest()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        (player1, player2) = (players[0].GetComponent<PlayerMovement>(), players[1].GetComponent<PlayerMovement>());
        (numberPlayer1, numberPlayer2) = (player1.Number, player2.Number);
        IsActive = true;
        ImageNote.Appear(StartNoteImage);
    }

    public virtual void CompleteQuest()
    {
        IsActive = false;
        IsCompleted = true;
        (player1.Number, player2.Number) = (numberPlayer1, numberPlayer2);
    }
}