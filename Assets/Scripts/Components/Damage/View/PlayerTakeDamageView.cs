using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamageView
{
    private IDamageHandler damageHandler;
    private AudioSource audioSource;

    public PlayerTakeDamageView(IDamageHandler damageHandler, AudioSource audioSource)
    {
        this.damageHandler = damageHandler;
        this.audioSource = audioSource;

        damageHandler.TakeDamageEvent.AddListener(OnTakeDamage);
    }

    public void OnTakeDamage(DamageInfo damageInfo)
    {
        if (damageInfo.effectiveDamageValue > 0f && audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
    }
}
