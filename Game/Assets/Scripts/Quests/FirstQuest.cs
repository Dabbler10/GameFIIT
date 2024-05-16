using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirstQuest : Quest
{
    private readonly float questDuration = 30f; // 180 seconds = 3 minutes
    private float currentQestTime = 0f;
    private float questTime = 0f;
    public float delta = 15f;
    //[SerializeField] public Quest previousQuest;

    private void Update()
    {
        //Debug.Log(IsActive);
        if (IsActive)
        {
            questTime += Time.deltaTime;
            currentQestTime += Time.deltaTime;

            if (questTime >= delta)
            {
                SwitchControl();
                questTime = 0f;
            }

            if (currentQestTime >= questDuration)
            {
                CompleteQuest();
            }
        }
    }

    private void SwitchControl()
    {
        (player1.input, player2.input) = (player2.input, player1.input);
        (player1.jump, player2.jump) = (player2.jump, player1.jump);
        (player1.attack, player2.attack) = (player2.attack, player1.attack);
    }
}