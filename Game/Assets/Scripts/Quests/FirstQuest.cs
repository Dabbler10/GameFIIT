using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FirstQuest : Quest
{
    private readonly float questDuration = 30f; // 180 seconds = 3 minutes
    private float currentQestTime = 0f;
    private float questTime = 0f;
    public float delta = 15f;
    private GhostSpawn spawner;
    public GameObject prefab;
    private List<GameObject> ghosts;
    private int minGhostNumber = 3;
    //[SerializeField] public Quest previousQuest;
    
    void Start()
    {
        spawner = new();
        ghosts = GameObject.FindGameObjectsWithTag("Ghost").ToList();
    }

    private void Update()
    {
        //Debug.Log(IsActive);
        if (!IsActive) return;
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

        if (ghosts.Count < minGhostNumber)
        {
            ghosts.AddRange(spawner.SpawnObjects(prefab, transform.position + new Vector3(5,0,0)));
            // исправить потом центр спавна на что-то лоигичное относительно комнаты квеста
        }

        ClearGhostList();

    }

    private void SwitchControl()
    {
        (player1.input, player2.input) = (player2.input, player1.input);
        (player1.jump, player2.jump) = (player2.jump, player1.jump);
        (player1.attack, player2.attack) = (player2.attack, player1.attack);
    }

    private void ClearGhostList()
    {
        for (var i = 0; i < ghosts.Count; i++)
        {
            if (!ghosts[i].activeSelf)
                ghosts.RemoveAt(i);
        }
    }
}