using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupAbility : MonoBehaviour, IInteractive
{
    [SerializeField] private eAbilityType abilityType;
    [SerializeField] private TextMeshPro tooltipTextField;
    [SerializeField] private string tooltipText = "Press \"interact\" to pick up the ";

    public bool IsInteracting { get; private set; }

    private void Awake()
    {
        tooltipText += abilityType.ToString();
        HideTooltip();
    }

    public void StartInteraction(GameObject interactingCharacter)
    {
        IsInteracting = true;

        HideTooltip();

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
        tooltipTextField.text = tooltipText;
    }

    public void HideTooltip()
    {
        tooltipTextField.text = string.Empty;
    }
}
