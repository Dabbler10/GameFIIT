using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuestStatue : Quest
{
    private readonly float questDuration = 30f; // 180 seconds = 3 minutes
    public MainStatue statue;
    
    private void Update()
    {
        if (!IsActive) return;
        if (statue.IsCompleted())
            CompleteQuest();
    }
}