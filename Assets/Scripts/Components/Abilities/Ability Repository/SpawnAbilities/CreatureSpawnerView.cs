using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawnerView
{
    private IAbility ability;
    private AudioSource audioSource;
    private AudioClip spawnCreatureAudioClip;

    public CreatureSpawnerView(CreatureSpawnerViewData creatureSpawnerViewData, IAbility ability, AudioSource audioSource)
    {
        this.ability = ability;
        this.audioSource =audioSource;
        spawnCreatureAudioClip = creatureSpawnerViewData.spawnCreatureAudioClip;

        ability.ReleaseCastEvent.AddListener(OnSpawnCreature);
    }


    public void OnSpawnCreature()
    {
        audioSource.PlayOneShot(spawnCreatureAudioClip);
    }
}
