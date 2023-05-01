using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityManager : IAbilityManager
{
    private Dictionary<eAbilityType, IAbility> abilities = new();
    private Dictionary<int, IAbility> learnedAbilities = new();

    public int CurrentLayoutNumber { get; private set; } = 1;
    public int LayoutCount { get; set; } = 2;
    public int AbilityCountInLayout { get; set; } = 2;
    public int AbilityCount => abilities.Count;

    public UnityEvent<eAbilityType> AbilityLearnEvent { get; } = new();
    public UnityEvent<eAbilityType> AbilityForgetEvent { get; } = new();

    public UnityEvent<int> SwitchLayoutEvent { get; private set; } = new();
    public Dictionary<int, IAbility> LearnedAbilities { get => learnedAbilities; set => learnedAbilities = value; }
    public Dictionary<eAbilityType, IAbility> Abilities { get => abilities; set => abilities = value; }

    public IAbility GetAbilityByType(eAbilityType type) => abilities[type];

    public void AddAbility(eAbilityType abilityType, IAbility ability)
    {
        abilities.Add(abilityType, ability);
    }

    public int LearnAbility(eAbilityType abilityType)
    {
        if (abilities.TryGetValue(abilityType, out IAbility ability))
        {
            return LearnAbility(ability);
        }

        return 0;
    }

    public int LearnAbility(IAbility ability)
    {
        if (learnedAbilities.ContainsValue(ability))
        {
            return 0;
        }

        for (int abilityNumber = 1; abilityNumber < int.MaxValue; abilityNumber++)
        {
            if (!learnedAbilities.ContainsKey(abilityNumber))
            {
                learnedAbilities.Add(abilityNumber, ability);
                AbilityLearnEvent.Invoke(ability.Type);
                return abilityNumber;
            }
        }

        return 0;
    }

    public bool LearnAbility(eAbilityType abilityType, int actualAbilityNumber)
    {
        if (abilities.TryGetValue(abilityType, out IAbility ability))
        {
            return LearnAbility(abilities[abilityType], actualAbilityNumber);
        }
        
        return false;
    }


    public bool LearnAbility(IAbility ability, int actualAbilityNumber)
    {
        if (actualAbilityNumber <= 0)
        {
            return false;
        }

        if (learnedAbilities.ContainsValue(ability))
        {
            return false;
        }

        learnedAbilities[actualAbilityNumber] = ability;
        return true;        
    }

    public bool ForgetAbility(eAbilityType abilityType)
    {
        if (abilities.TryGetValue(abilityType, out IAbility ability))
        {
            return ForgetAbility(ability);
        }

        return false;
    }

    public bool ForgetAbility(IAbility ability)
    {
        foreach (KeyValuePair<int, IAbility> learnedAbility in learnedAbilities)
        {
            if (learnedAbility.Value == ability)
            {
                learnedAbilities.Remove(learnedAbility.Key);
                AbilityForgetEvent.Invoke(ability.Type);
                return true;
            }
        }

        return false;
    }

    public bool ForgetAbility(int actualAbilityNumber)
    {
        return learnedAbilities.Remove(actualAbilityNumber);
    }

    public bool StartCastAbility(int inputAbilityNumber)
    {
        if (learnedAbilities.TryGetValue(InputToActualAbilityNumber(inputAbilityNumber), out IAbility ability))
        {
            ability.StartCast();
            return true;
        }

        return false;
    }

    public void BreakCastAbility(int inputAbilityNumber)
    {
        if (learnedAbilities.TryGetValue(InputToActualAbilityNumber(inputAbilityNumber), out IAbility ability))
        {
            ability.BreakCast();
        }
    }

    public void StopSustainingAbility(int inputAbilityNumber)
    {
        if (learnedAbilities.TryGetValue(InputToActualAbilityNumber(inputAbilityNumber), out IAbility ability)
            && ability is ISustainableAbility sustainableAbility)
        {
            sustainableAbility.StopSustaining();
        }
    }

    public bool CanCastAbility(int inputAbilityNumber)
    {
        return learnedAbilities.ContainsKey(InputToActualAbilityNumber(inputAbilityNumber)) && learnedAbilities[InputToActualAbilityNumber(inputAbilityNumber)].CanBeUsed;
    }

    public bool IsPerforming(int inputAbilityNumber)
    {
        return learnedAbilities.ContainsKey(InputToActualAbilityNumber(inputAbilityNumber)) && learnedAbilities[InputToActualAbilityNumber(inputAbilityNumber)].IsPerforming;
    }

    public void SwitchAbilityLayout()
    {
        CurrentLayoutNumber++;
        if (CurrentLayoutNumber > LayoutCount)
        {
            CurrentLayoutNumber -= LayoutCount;
        }
        SwitchLayoutEvent.Invoke(CurrentLayoutNumber);
    }

    public int GetInputAbilityNumber(int actualAbilityNumber)
    {
        return actualAbilityNumber - (CurrentLayoutNumber - 1) * AbilityCountInLayout;
    }

    private int InputToActualAbilityNumber(int inputAbilityNumber)
    {
        return inputAbilityNumber + (CurrentLayoutNumber - 1) * AbilityCountInLayout;
    }

}
