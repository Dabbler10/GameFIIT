using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HealthBarForPlayer1 : MonoBehaviour
{
    [SerializeField] private Player1Health playerHealth;
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
