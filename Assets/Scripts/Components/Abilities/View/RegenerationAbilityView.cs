using UnityEngine;
using UnityEngine.VFX;

public class RegenerationAbilityView
{
    private AudioClip startHealAudioClip;
    private VisualEffect regenerationEffectGraph;
    private IAbility ability;
    private AudioSource audioSource;

    public RegenerationAbilityView(CommonAbilityViewData regeneraionViewData, VisualEffect regenerationEffectGraph, IAbility ability, AudioSource audioSource)
    {
        startHealAudioClip = regeneraionViewData.startCastSoundEffect;
        this.regenerationEffectGraph = regenerationEffectGraph;
        this.ability = ability;
        this.audioSource = audioSource;

        ability.StartCastEvent.AddListener(OnStartCast);
        ability.BreakCastEvent.AddListener(OnBreakCast);
    }

    private void OnStartCast()
    {
        audioSource.PlayOneShot(startHealAudioClip);
        PlayHealingVFXGraph();
    }

    private void OnBreakCast()
    {
        PlayFinalEffectsVFXGraph();
    }

    private void PlayHealingVFXGraph()
    {
        regenerationEffectGraph.SendEvent("OnRegenerationRelease");
    }

    private void PlayFinalEffectsVFXGraph()
    {
        regenerationEffectGraph.SendEvent("OnBreakRegeneration");
    }
}
