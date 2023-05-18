using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveEffectManager : EffectManager
{
    public eEffectPower SusceptibilityType { get; } = eEffectPower.Weak;

    public SelectiveEffectManager(MonoBehaviour owner, SelectiveEffectManagerData effectManagerData) : base(owner, effectManagerData)
    {
        SusceptibilityType = effectManagerData.susceptibilityType;
    }

    public override void AddEffect(IEffect effect)
    {
        if (effect.EffectPower < SusceptibilityType)
        {
            return;
        }

        base.AddEffect(effect);
    }
}
