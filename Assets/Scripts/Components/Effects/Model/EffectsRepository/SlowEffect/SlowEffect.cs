using UnityEngine;

public class SlowEffect : ISlowEffect
{
    public float MovementSlowValue { get; }
    public float EndEffectTime { get; }

    public SlowEffect(SlowEffectData slowEffectData)
    {
        MovementSlowValue = slowEffectData.movementSlowValue;

        if (slowEffectData.duration == float.MaxValue)
        {
            EndEffectTime = float.MaxValue;
        }
        else
        {
            EndEffectTime = Time.time + slowEffectData.duration;
        }
    }
}
