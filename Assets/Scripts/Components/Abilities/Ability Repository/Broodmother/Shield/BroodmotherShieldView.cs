using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherShieldView
{
    private GameObject shield;

    private AudioClip shieldDestroyingAudioClip;

    private AudioSource audioSource;

    public BroodmotherShieldView(GameObject shield, ShieldViewData shieldViewData, IHealthManager shieldManager, AudioSource audioSource)
    {
        this.shield = shield;

        shieldDestroyingAudioClip = shieldViewData.shieldDestroyingAudioClip;

        this.audioSource = audioSource;

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
            audioSource.PlayOneShot(shieldDestroyingAudioClip);
        }
    }
}
