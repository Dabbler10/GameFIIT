using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirstQuest : Quest
{
    private readonly float questDuration = 90f; // 180 seconds = 3 minutes
    private float currentQestTime = 0f;
    private float questTime = 0f;
    public float delta = 15f;
    //[SerializeField] public Quest previousQuest;

    private void Update()
    {
        if (!IsActive && currentQestTime == 0f)
        {
            IsActive = true;
            StartQuest();
        }
        else
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
                IsActive = false;
                CompleteQuest();
            }
        }
    }

    private void SwitchControl()
    {
        (player1.input, player2.input) = (player2.input, player1.input);
    }

}