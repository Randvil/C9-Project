using UnityEngine;
using UnityEngine.VFX;

public class TessenAbilityView 
{
    private VisualEffect tessenEffectGraph;
    private IAbility ability;
    private ITurning turning;
    private float xPos;

    public TessenAbilityView(VisualEffect tessenEffectGraph, IAbility ability, ITurning turning)
    {
        this.tessenEffectGraph = tessenEffectGraph;
        this.ability = ability;
        this.turning = turning;
        xPos = tessenEffectGraph.GetFloat("xPositionOverPlayer");

        ability.ReleaseCastEvent.AddListener(OnCastRelease);
    }

    public void OnCastRelease()
    {
        PlayTessenVFXGraph();
    }

    public void PlayTessenVFXGraph()
    {
        switch (turning.Direction)
        {
            case eDirection.Right:
                tessenEffectGraph.SetFloat("xPositionOverPlayer", xPos);
                break;
            case eDirection.Left:
                tessenEffectGraph.SetFloat("xPositionOverPlayer", -xPos);
                break;

        }
        tessenEffectGraph.Play();
    }
}
