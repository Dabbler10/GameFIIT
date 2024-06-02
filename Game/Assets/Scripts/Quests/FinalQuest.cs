

public class QuestFinal : Quest
{
    public BurnScript burn;
    
    private void Update()
    {
        if (!IsActive) return;
        if (burn.IsCompleted())
            CompleteQuest();
    }
}