using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : IEffect
{
    public eEffectPower EffectPower { get; }
    public float EndEffectTime { get; }

    public BaseEffect(EffectData effectData)
    {
        EffectPower = effectData.effectPower;
        EndEffectTime = Time.time + effectData.duration;
    }
}
