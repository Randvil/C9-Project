
using UnityEngine;

public class StunEffect : IStunEffect
{
    public float EndEffectTime { get; }

    public StunEffect(StunEffectData stunEffectData)
    {
        float duration = stunEffectData.duration;

        if (duration == float.MaxValue)
        {
            EndEffectTime = float.MaxValue;
        }
        else
        {
            EndEffectTime = Time.time + duration;
        }
    }
}
