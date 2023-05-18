using UnityEngine;

public class SlowEffect : BaseEffect, ISlowEffect
{
    public float MovementSlowValue { get; }

    public SlowEffect(SlowEffectData slowEffectData) : base(slowEffectData)
    {
        MovementSlowValue = slowEffectData.movementSlowValue;
    }
}
