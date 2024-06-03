using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuestStatue : Quest
{
    public MainStatue statue;
    
    private void Update()
    {
        if (!IsActive) return;
        if (statue.IsCompleted())
            CompleteQuest();
    }
}