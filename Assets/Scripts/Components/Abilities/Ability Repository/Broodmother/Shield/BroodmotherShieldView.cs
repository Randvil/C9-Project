using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherShieldView
{
    private SkinnedMeshRenderer SkinnedMeshRenderer;
    private Material shieldMAT;
    private Material[] matArray;

    public BroodmotherShieldView(Material shieldMAT, IHealthManager shieldManager, SkinnedMeshRenderer skinnedMeshRenderer)
    {
        this.shieldMAT = shieldMAT;
        SkinnedMeshRenderer = skinnedMeshRenderer;
        matArray = SkinnedMeshRenderer.materials;
        shieldManager.CurrentHealthChangedEvent.AddListener(ShowShield);
    }

    public void ShowShield(Health health)
    {
        if (health.currentHealth > 0)
        {
            matArray[1] = shieldMAT;
            SkinnedMeshRenderer.materials = matArray;
        }
        else
        {
            matArray[1] = matArray[0];
            SkinnedMeshRenderer.materials = matArray;
        }
    }
}
