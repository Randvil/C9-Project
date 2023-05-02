using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ITalent
{
    public void Learn();
    public void Forget();
    public eTalentType TalentType { get; }
    public UnityEvent<eTalentType> LearnEvent { get; }
    public UnityEvent<eTalentType> ForgetEvent { get; }
}
