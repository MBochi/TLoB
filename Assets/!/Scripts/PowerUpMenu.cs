using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpMenu : MonoBehaviour
{
    private Stats playerStats;
    [SerializeField] private TextMeshProUGUI currentLevel;
    [SerializeField] private TextMeshProUGUI availablePoints;
    [SerializeField] private TextMeshProUGUI currentDamage;
    [SerializeField] private TextMeshProUGUI currentMaxHP;

    private void Start()
    {
        playerStats = GetComponent<Stats>();
    }
    public void LoadStats()
    {
        currentLevel.text = "Level " + playerStats.GetCurrentLevel().ToString();
        availablePoints.text = "Available Points: " + playerStats.GetAvailablePoints().ToString();
        currentMaxHP.text = "MaxHealth: " + playerStats.GetMaxHealth().ToString();
        currentDamage.text = "Damage: " + playerStats.GetAttackDamage().ToString();
    }

    public void MaxHealthUp()
    {
        if (playerStats.GetAvailablePoints() > 0) {
            playerStats.SetMaxHealth(playerStats.GetMaxHealth() + 10);
            playerStats.SetAvailablePoints(playerStats.GetAvailablePoints() - 1);
            availablePoints.text = "Available Points: " + playerStats.GetAvailablePoints().ToString();
            currentMaxHP.text = "MaxHealth: " + playerStats.GetMaxHealth().ToString();
        }   
    }

    public void DamageUp()
    {
        if (playerStats.GetAvailablePoints() > 0)
        {
            playerStats.SetAttackDamage(playerStats.GetAttackDamage() + 10);
            playerStats.SetAvailablePoints(playerStats.GetAvailablePoints() - 1);
            availablePoints.text = "Available Points: " + playerStats.GetAvailablePoints().ToString();
            currentDamage.text = "Damage: " + playerStats.GetAttackDamage().ToString();
        }
    }
}
