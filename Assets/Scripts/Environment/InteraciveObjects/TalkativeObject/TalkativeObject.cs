using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkativeObject : MonoBehaviour, IInteractive
{
    [SerializeField]
    private TextMeshPro tooltipTextField;

    [SerializeField]
    private TextMeshPro speechTextField;

    [SerializeField]
    private TalkativeObjectData talkativeObjectData;

    private int currentSpeech;

    public bool IsInteracting { get; private set; }

    public void StartInteraction(GameObject interactingCharacter)
    {
        HideTooltip();

        IsInteracting = true;

        currentSpeech = 0;
        speechTextField.text = talkativeObjectData.speeches[currentSpeech];
    }

    public void StopInteraction()
    {
        IsInteracting = false;
        speechTextField.text = string.Empty;
    }

    public void NextStep()
    {
        currentSpeech++;
        if (talkativeObjectData.speeches.Length > currentSpeech)
        {
            speechTextField.text = talkativeObjectData.speeches[currentSpeech];
        }
        else
        {
            IsInteracting = false;
        }
    }

    public void ShowTooltip()
    {
        tooltipTextField.text = talkativeObjectData.tooltip;
    }

    public void HideTooltip()
    {
        tooltipTextField.text = string.Empty;
    }
}
