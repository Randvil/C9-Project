using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherShieldView
{
    private GameObject shield;

    public BroodmotherShieldView(GameObject shield, IHealthManager shieldManager)
    {
        this.shield = shield;
        shieldManager.CurrentHealthChangedEvent.AddListener(ShowShield);
    }

    public void ShowShield(Health health)
    {
        if (health.currentHealth > 0)
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
        }
    }
}
