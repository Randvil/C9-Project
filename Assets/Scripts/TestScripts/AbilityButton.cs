using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private eAbilityType abilityType;

    private IAbilityManager abilityManager;

    private TextMeshProUGUI tmp;
    private Image image;
    private string abilityText;
    private int abilityNumber;

    private void Start()
    {
        abilityManager = GameObject.FindGameObjectWithTag("Player").GetComponent<IAbilityCaster>().AbilityManager;
        abilityManager.SwitchLayoutEvent.AddListener(OnLayoutChange);

        tmp = GetComponentInChildren<TextMeshProUGUI>();
        abilityText = tmp.text;

        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            int abilityNumber = abilityManager.LearnAbility(abilityType);            
            if (abilityNumber > 0)
            {
                this.abilityNumber = abilityNumber;
                image.color = Color.green;
                ShowTooltip();
                Debug.Log($"Ability was learned and added to {abilityNumber} slot");
            }
            else
            {
                Debug.Log($"Ability wasn't learned because you've already learned it");
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (abilityManager.ForgetAbility(abilityType))
            {
                image.color = Color.white;
                abilityNumber = 0;
                ShowTooltip();
                Debug.Log("Ability was forget");
            }
            else
            {
                Debug.Log("Ability wasn't forget because you didn't learn it yet");
            }
        }
    }

    private void ShowTooltip()
    {
        int inputAbilityNumber = abilityManager.GetInputAbilityNumber(abilityNumber);

        if (inputAbilityNumber == 1)
        {
            tmp.text = $"{abilityText}\nQ or RT";
        }
        else if (inputAbilityNumber == 2)
        {
            tmp.text = $"{abilityText}\nE or LT";
        }
        else
        {
            tmp.text = $"{abilityText}";
        }
    }

    private void OnLayoutChange(int layoutNumber)
    {
        ShowTooltip();
    }
}
