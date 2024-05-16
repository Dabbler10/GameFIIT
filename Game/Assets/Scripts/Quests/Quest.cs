using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public bool IsActive { get; protected set; }
    public bool IsCompleted { get; protected set; }
    public Quest Previous;
    internal Player player1;
    internal Player player2;
    private string inputPlayer1, inputPlayer2;
    private string attackPlayer1, attackPlayer2;
    private string jumpPlayer1, jumpPlayer2;
    public Note ImageNote;
    public Sprite StartNoteImage;

    public virtual void StartQuest()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        (player1, player2) = (players[0].GetComponent<Player>(), players[1].GetComponent<Player>());
        (inputPlayer1, inputPlayer2) = (new string(player1.input), new string(player2.input));
        (attackPlayer1, attackPlayer2) = (string.Copy(player1.attack), string.Copy(player2.attack));
        (jumpPlayer1, jumpPlayer2) = (string.Copy(player1.jump), string.Copy(player2.jump));
        IsActive = true;
        ImageNote.Appear(StartNoteImage);
    }

    public virtual void CompleteQuest()
    {
        IsActive = false;
        IsCompleted = true;
        (player1.input, player2.input) = (inputPlayer1, inputPlayer2);
        (player1.jump, player2.jump) = (jumpPlayer1, jumpPlayer2);
        (player1.attack, player2.attack) = (attackPlayer1, attackPlayer2);
    }
}