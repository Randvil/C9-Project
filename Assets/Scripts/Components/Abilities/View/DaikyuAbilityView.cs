using UnityEngine.VFX;

public class DaikyuAbilityView 
{
    private IAbility ability;

    public DaikyuAbilityView(IAbility ability)
    {
        this.ability = ability;

        ability.ReleaseCastEvent.AddListener(OnCastRelease);
    }

    public void OnCastRelease()
    {

    }
}
