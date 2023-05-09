using UnityEngine.VFX;

public class KanaboAbilityView
{
    private VisualEffect kanaboEffectGraph;
    private IAbility ability;
    private ITurning turning;
    private float xPos;

    public KanaboAbilityView(VisualEffect kanaboEffectGraph, IAbility ability, ITurning turning)
    {
        this.kanaboEffectGraph = kanaboEffectGraph;
        this.ability = ability;
        this.turning = turning;
        xPos = kanaboEffectGraph.GetFloat("xPositionOverPlayer");

        ability.ReleaseCastEvent.AddListener(OnCastRelease);
    }

    public void OnCastRelease()
    {
        PlayKanaboVFXGraph();
    }

    public void PlayKanaboVFXGraph()
    {
        switch (turning.Direction)
        {
            case eDirection.Right:
                kanaboEffectGraph.SetFloat("xPositionOverPlayer", xPos);
                break;
            case eDirection.Left:
                kanaboEffectGraph.SetFloat("xPositionOverPlayer", -xPos);
                break;

        }
        kanaboEffectGraph.Play();
    }
}
