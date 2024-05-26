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
    private int minGhostNumber;
    protected override int QuestNumber { get; } = 1;

    //[SerializeField] public Quest previousQuest;
    
    void Start()
    {
        spawner = new();
        ghosts = GameObject.FindGameObjectsWithTag("Ghost").ToList();
        minGhostNumber = ghosts.Count;
    }

    private void Update()
    {
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
        (player1.Number, player2.Number) = (player2.Number, player1.Number);
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