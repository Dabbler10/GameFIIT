using UnityEngine;

public class HealthBarForPlayer : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private UnityEngine.UI.Image totalHealthBar;
    [SerializeField] private UnityEngine.UI.Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 20;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 20;
    }
}
