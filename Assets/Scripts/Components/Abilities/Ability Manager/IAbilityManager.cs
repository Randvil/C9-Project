using System.Collections.Generic;
using UnityEngine.Events;

public interface IAbilityManager
{
    public void AddAbility(eAbilityType abilityType, IAbility ability);
    public int LearnAbility(eAbilityType abilityType);
    public int LearnAbility(IAbility ability);
    public bool LearnAbility(eAbilityType abilityType, int actualAbilityNumber);
    public bool LearnAbility(IAbility ability, int actualAbilityNumber);
    public bool ForgetAbility(eAbilityType abilityType);
    public bool ForgetAbility(IAbility ability);
    public bool ForgetAbility(int actualAbilityNumber);
    public bool CanCastAbility(int inputAbilityNumber);
    public bool IsPerforming(int inputAbilityNumber);
    public bool StartCastAbility(int inputAbilityNumber);
    public void BreakCastAbility(int inputAbilityNumber);
    public void StopSustainingAbility(int inputAbilityNumber);
    public void SwitchAbilityLayout();
    public int GetInputAbilityNumber(int actualAbilityNumber);
    public IAbility GetAbilityByType(eAbilityType type);

    public int CurrentLayoutNumber { get; }
    public int LayoutCount { get; set; }
    public int AbilityCountInLayout { get; set; }

    public Dictionary<int, IAbility> LearnedAbilities { get; }

    public UnityEvent<eAbilityType> AbilityLearnEvent { get; }
    public UnityEvent<eAbilityType> AbilityForgetEvent { get; }
    public UnityEvent<int> SwitchLayoutEvent { get; }
}
