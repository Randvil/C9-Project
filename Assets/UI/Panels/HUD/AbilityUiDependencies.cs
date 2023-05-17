using DG.Tweening;
using NS.RomanLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityUiDependencies : MonoBehaviour
{
    private IAbilityManager abilityManager;
    
    public IParry Parry { get; set; }

    private PanelManager panelManager;

    private VisualElement hudScreen;
    private VisualElement abilitiesScreen;

    private VisualElement[] polyInHud;
    private VisualElement parryPoly;
    private Polygon[] polyInCollection;

    private readonly Dictionary<eAbilityType, int> abilityTypeOrder = new();
    private readonly Dictionary<eAbilityType, bool> abilityCanBeUsed = new();

    const int polyCount = 4;

    const string cantUseClass = "poly-cant_use";
    const string hiddenPolyClass = "hidden-poly";

    private void Start()
    {
        panelManager = GetComponentInParent<PanelManager>();

        abilityManager = panelManager.Abilities;

        hudScreen = panelManager.panels[0];
        abilitiesScreen = panelManager.panels[2];

        parryPoly = hudScreen.Q<VisualElement>("parry");
        SetFogForFilledArea(parryPoly, false);
        Parry.BreakParryEvent.AddListener(OnParry); // because cooldown starts after duration

        polyInHud = new VisualElement[polyCount];
        for (int i = 1; i <= polyCount; i++)
        {
            polyInHud[i - 1] = hudScreen.Q<VisualElement>("p" + i);
            SetFogForFilledArea(polyInHud[i - 1], false);
        }

        polyInCollection = new Polygon[polyCount];
        for (int i = 1; i <= polyCount; i++)
            polyInCollection[i - 1] = abilitiesScreen.Q<Polygon>("p" + i);

        abilityManager.SwitchLayoutEvent.AddListener(OnLayoutChange);
        abilityManager.AbilityLearnEvent.AddListener(OnSet);
        abilityManager.AbilityForgetEvent.AddListener(OnUnset);
        
        CheckForAlreadyLearned();
    }

    // we need to check dis manually coz of AbilityManager hasn't reactive properties
    private void Update()
    {
        CheckAbilitiesStatus();
    }

    private eAbilityType GetTypeByIndex(int slotIndex) => abilityTypeOrder.First(x => x.Value == slotIndex).Key;

    private void CheckForAlreadyLearned()
    {
        int i = 0;
        foreach (var abilityWithInd in abilityManager.LearnedAbilities)
        {
            if (++i > polyCount)
                return;

            OnSetWithoutChecking(abilityWithInd.Value.AbilityType);
        }
        CheckAllAbilitiesStatus();
    }

    private void CheckAbilitiesStatus()
    {
        int i = 0;
        foreach (var ab in abilityManager.LearnedAbilities.Values)
        {
            if (++i > polyCount)
                return;

            if (ab.CanBeUsed != abilityCanBeUsed[ab.AbilityType])
            {
                abilityCanBeUsed[ab.AbilityType] = ab.CanBeUsed;
                UpdateAbilityStatus(ab.AbilityType);
            }
        }
    }

    private void CheckAllAbilitiesStatus()
    {
        int i = 0;
        foreach (var ab in abilityManager.LearnedAbilities.Values)
        {
            if (++i > polyCount)
                return;

            abilityCanBeUsed[ab.AbilityType] = ab.CanBeUsed;
            UpdateAbilityStatus(ab.AbilityType);
        }
    }

    private void UpdateAbilityStatus(eAbilityType type)
    {
        VisualElement poly = polyInHud[abilityTypeOrder[type]];

        if (abilityCanBeUsed[type])
            poly.RemoveFromClassList(cantUseClass);
        else
            poly.AddToClassList(cantUseClass);
    }

    private void SubscribeToFog(eAbilityType type)
    {
        IAbility ability = abilityManager.GetAbilityByType(type);
        ability.ReleaseCastEvent.AddListener(() =>
        {
            int index = abilityTypeOrder[type];
            if (abilityCanBeUsed.ContainsKey(type))
                SetFogForFilledArea(polyInHud[index], ability.Cooldown);
        });
    }

    private void OnSet(eAbilityType type)
    {
        OnSetWithoutChecking(type);

        CheckAllAbilitiesStatus();
    }

    private void OnSetWithoutChecking(eAbilityType type)
    {
        if (abilityTypeOrder.ContainsKey(type))
            return;

        int newAbilityIndex = abilityManager.LearnedAbilities.First(x => x.Value.AbilityType == type).Key - 1;
        SetAbilityOnSlot(type, newAbilityIndex);
        abilityCanBeUsed.Add(type, false);

        SubscribeToFog(type);
    }

    private void OnUnset(eAbilityType type)
    {
        int index = abilityTypeOrder[type];
        ClearAbilitySlot(index);
        abilityCanBeUsed.Remove(type);

        abilityTypeOrder.Remove(type);

        CheckAllAbilitiesStatus();    
    }

    private void OnParry()
    {
        SetFogForFilledArea(parryPoly, Parry.Cooldown);
    }

    private void SetAbilityOnSlot(eAbilityType type, int slotIndex)
    {
        if (abilityTypeOrder.ContainsValue(slotIndex))
            ClearAbilitySlot(slotIndex);
        else
            abilityTypeOrder.Add(type, slotIndex);

        string strNewType = type.ToString();
        polyInHud[slotIndex].AddToClassList(strNewType);
        polyInCollection[slotIndex].AddToClassList(strNewType + "PolyControl");
    }

    private void ClearAbilitySlot(int slotIndex)
    {
        string strPreviousType = GetTypeByIndex(slotIndex).ToString();
        
        polyInHud[slotIndex].RemoveFromClassList(strPreviousType);
        polyInHud[slotIndex].RemoveFromClassList(cantUseClass);
        polyInCollection[slotIndex].RemoveFromClassList(strPreviousType + "PolyControl");
    }

    private void SetFogForFilledArea(VisualElement poly, bool fog)
    {
        RadialFill radialFill = poly.Q<RadialFill>();
        radialFill.value = fog ? 1f : 0f;
    }

    private void SetFogForFilledArea(VisualElement poly, float cooldown)
    {
        RadialFill radialFill = poly.Q<RadialFill>();
        DOTween.To(x => radialFill.value = x, 1f, 0f, cooldown).SetEase(Ease.Linear);
    }

    private void OnLayoutChange(int layoutNumber)
    {
        int leftBorder = abilityManager.AbilityCountInLayout * (layoutNumber - 1);
        int rightBorder = abilityManager.AbilityCountInLayout * layoutNumber - 1;
        for (int i = 0; i < polyCount; i++)
            if (i >= leftBorder && i <= rightBorder)
                polyInHud[i].RemoveFromClassList(hiddenPolyClass);
            else
                polyInHud[i].AddToClassList(hiddenPolyClass);
    }
}
