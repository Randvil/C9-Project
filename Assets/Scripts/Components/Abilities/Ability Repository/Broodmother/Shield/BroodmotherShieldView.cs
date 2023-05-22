using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherShieldView
{
    private Material shieldMaterial;
    private MonoBehaviour owner;
    private float dissolveRate;
    private float refreshRate = 0.025f;

    public BroodmotherShieldView(GameObject shield, IHealthManager shieldManager, MonoBehaviour owner, float dissolveRate)
    {
        this.owner = owner;
        this.dissolveRate = dissolveRate;
        shieldMaterial = shield.GetComponent<MeshRenderer>().material;

        shieldManager.CurrentHealthChangedEvent.AddListener(ShowShield);
    }

    public void ShowShield(Health health)
    {
        if (health.currentHealth > 0)
        {
            shieldMaterial.SetFloat("_DissolveAmount", 0);
        }
        else
        {
            ApplyDissolve();
        }
    }

    public void ApplyDissolve()
    {
        owner.StartCoroutine(DissolveCoroutine());
    }

    public IEnumerator DissolveCoroutine()
    {
        float counter = 0;

        while (shieldMaterial.GetFloat("_DissolveAmount") < 1)
        {

            counter += dissolveRate;
            shieldMaterial.SetFloat("_DissolveAmount", counter);

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
