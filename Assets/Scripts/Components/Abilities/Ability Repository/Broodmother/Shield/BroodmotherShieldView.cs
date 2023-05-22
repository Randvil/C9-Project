using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherShieldView
{
    private MonoBehaviour owner;
    private float dissolveRate;
    private float refreshRate = 0.025f;
    private Material shieldMaterial;

    private AudioClip shieldDestroyingAudioClip;

    private AudioSource audioSource;

    public BroodmotherShieldView(MonoBehaviour owner, GameObject shield, ShieldViewData shieldViewData, IHealthManager shieldManager, AudioSource audioSource, float dissolveRate)
    {
        this.owner = owner;

        this.shield = shield;

        this.dissolveRate = dissolveRate;
        shieldDestroyingAudioClip = shieldViewData.shieldDestroyingAudioClip;

        this.audioSource = audioSource;
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
            audioSource.PlayOneShot(shieldDestroyingAudioClip);
            
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
