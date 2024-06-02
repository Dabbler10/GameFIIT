

public class QuestFinal : Quest
{
    private readonly float questDuration = 30f; // 180 seconds = 3 minutes
    public BurnScript burn;
    
    private void Update()
    {
        if (!IsActive) return;
        if (burn.IsCompleted())
            CompleteQuest();
    }
}