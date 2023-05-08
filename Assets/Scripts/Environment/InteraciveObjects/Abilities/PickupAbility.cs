using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAbility : MonoBehaviour, IInteractive
{
    [SerializeField] private eAbilityType abilityType;

    public bool IsInteracting { get; private set; }

    public void StartInteraction(GameObject interactingCharacter)
    {
        IsInteracting = true;

        if (interactingCharacter.TryGetComponent(out IAbilityCaster abilityCaster))
        {
            abilityCaster.AbilityManager.LearnAbility(abilityType);
        }

        StopInteraction();
        Destroy(gameObject);
    }

    public void StopInteraction()
    {
        IsInteracting = false;
    }

    public void NextStep()
    {
        StopInteraction();
    }

    public void ShowTooltip()
    {
        
    }

    public void HideTooltip()
    {
        
    }
}
