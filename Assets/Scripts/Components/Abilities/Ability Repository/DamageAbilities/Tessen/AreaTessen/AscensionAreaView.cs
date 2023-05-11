using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AscensionAreaView
{
    private AudioSource audioSource;
    private VisualEffect visualEffect;
    private AscensionArea ascensionArea;

    public AscensionAreaView(AscensionArea ascensionArea, AudioSource audioSource, VisualEffect visualEffect)
    {
        this.audioSource = audioSource;
        this.visualEffect = visualEffect;
        this.ascensionArea = ascensionArea;

        ascensionArea.SpawnEvent.AddListener(OnSpawn);
    }

    private void OnSpawn()
    {
        PlaySound();
        PlayVisualEffect();
    }

    private void PlaySound()
    {
        audioSource.Play();
    }

    private void PlayVisualEffect()
    {
        visualEffect.Play();
    }
}
