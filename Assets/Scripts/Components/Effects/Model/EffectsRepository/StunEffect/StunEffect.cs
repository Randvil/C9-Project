using UnityEngine;

public class StunEffect : IStunEffect
{
    public float EndEffectTime { get; }

    public StunEffect(StunEffectData stunEffectData)
    {
        EndEffectTime = Time.time + stunEffectData.duration;
    }
}
