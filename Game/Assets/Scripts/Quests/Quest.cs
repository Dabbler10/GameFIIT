using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public bool IsActive { get; protected set; }
    public Quest Previous;
    internal Player player1;
    internal Player player2;
    private string inputPlayer1;
    private string inputPlayer2;

    public virtual void StartQuest()
    {
        IsActive = true;
        var players = GameObject.FindGameObjectsWithTag("Player");
        (player1, player2) = (players[0].GetComponent<Player>(), players[1].GetComponent<Player>());
        (inputPlayer1, inputPlayer2) = (player1.input, player2.input);
    }

    public virtual void CompleteQuest()
    {
        IsActive = false;
        (player1.input, player2.input) = (inputPlayer1, inputPlayer2);
    }
}
