using UnityEngine;
using UnityEngine.VFX;

public class RegenerationAbilityView
{
    private VisualEffect regenerationEffectGraph;
    private IAbility ability;

    public RegenerationAbilityView(VisualEffect regenerationEffectGraph, IAbility ability)
    {
        this.regenerationEffectGraph = regenerationEffectGraph;
        this.ability = ability;

        ability.StartCastEvent.AddListener(OnStartCast);
        ability.BreakCastEvent.AddListener(OnBreakCast);
    }

    private void OnStartCast()
    {
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
