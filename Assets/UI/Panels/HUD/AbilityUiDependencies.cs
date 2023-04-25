using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityUiDependencies : MonoBehaviour
{
    private IAbilityManager abilityManager;

    private PanelManager panelManager;

    private VisualElement hudScreen;
    private VisualElement abilitiesScreen;

    private Polygon[] polyInHud;
    private Polygon[] polyInCollection;

    private readonly List<eAbilityType> abilityTypeOrder = new();
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

        polyInHud = new Polygon[polyCount];
        for (int i = 1; i <= polyCount; i++)
            polyInHud[i - 1] = hudScreen.Q<Polygon>("p" + i);

        polyInCollection = new Polygon[polyCount];
        for (int i = 1; i <= polyCount; i++)
            polyInCollection[i - 1] = abilitiesScreen.Q<Polygon>("p" + i);

        CheckForAlreadyLearned();

        abilityManager.SwitchLayoutEvent.AddListener(OnLayoutChange);
        abilityManager.AbilityLearnEvent.AddListener(OnLearn);
        abilityManager.AbilityForgetEvent.AddListener(OnForget);
    }

    // В AbilityManager'e нет реактивных полей, поэтому нужно самим следить за ними
    private void Update()
    {
        CheckAbilitiesStatus();
    }

    private void CheckForAlreadyLearned()
    {
        int slotIndex = 0;
        foreach (var ab in abilityManager.LearnedAbilities.Values)
        {
            SetAbilityOnSlot(ab.Type, slotIndex++);
            abilityCanBeUsed.Add(ab.Type, ab.CanBeUsed);
            CheckAllAbilitiesStatus();
        }
    }

    private void CheckAbilitiesStatus()
    {
        foreach (var ab in abilityManager.LearnedAbilities.Values)
        {
            if (ab.CanBeUsed != abilityCanBeUsed[ab.Type])
            {
                abilityCanBeUsed[ab.Type] = ab.CanBeUsed;
                UpdateAbilityStatus(ab.Type);
            }
        }
    }

    private void CheckAllAbilitiesStatus()
    {
        foreach (var ab in abilityManager.LearnedAbilities.Values)
        {
            abilityCanBeUsed[ab.Type] = ab.CanBeUsed;
            UpdateAbilityStatus(ab.Type);
        }
    }

    private void UpdateAbilityStatus(eAbilityType type)
    {
        Polygon poly = polyInHud[abilityTypeOrder.IndexOf(type)];

        if (abilityCanBeUsed[type])
            poly.RemoveFromClassList(cantUseClass);
        else
            poly.AddToClassList(cantUseClass);
    }

    private void OnLearn(eAbilityType type)
    {
        int newAbilityIndex = abilityManager.LearnedAbilities.Count - 1;

        SetAbilityOnSlot(type, newAbilityIndex);
        abilityCanBeUsed.Add(type, false);
        CheckAllAbilitiesStatus();
    }

    private void OnForget(eAbilityType type)
    {
        int lastAbilityIndex = abilityTypeOrder.Count - 1;

        for (int i = 0; i <= lastAbilityIndex; i++)
            if (abilityTypeOrder[i] == type)
            {
                for (int j = i; j < lastAbilityIndex; j++)
                    SetAbilityOnSlot(abilityTypeOrder[j + 1], j);

                ClearAbilitySlot(lastAbilityIndex);
                abilityTypeOrder.RemoveAt(i);
                abilityCanBeUsed.Remove(type);

                CheckAllAbilitiesStatus();

                break;
            }        
    }

    private void SetAbilityOnSlot(eAbilityType type, int slotIndex)
    {
        if (slotIndex < abilityTypeOrder.Count) // Если слот уже занят
            ClearAbilitySlot(slotIndex);
        else
            abilityTypeOrder.Add(type);

        string strNewType = type.ToString();
        polyInHud[slotIndex].AddToClassList(strNewType);
        polyInCollection[slotIndex].AddToClassList(strNewType);
    }

    private void ClearAbilitySlot(int slotIndex)
    {
        string strPreviousType = abilityTypeOrder[slotIndex].ToString();

        polyInHud[slotIndex].RemoveFromClassList(strPreviousType);
        polyInHud[slotIndex].RemoveFromClassList(cantUseClass);
        polyInCollection[slotIndex].RemoveFromClassList(strPreviousType);
    }

    private void OnLayoutChange(int layoutNumber)
    {
        foreach (Polygon poly in polyInHud)
            poly.ToggleInClassList(hiddenPolyClass);
    }
}
