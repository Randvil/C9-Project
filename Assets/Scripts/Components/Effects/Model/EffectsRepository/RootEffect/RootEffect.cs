using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEffect : IRootEffect
{
    public float EndEffectTime { get; }

    public RootEffect(RootEffectData rootEffectData)
    {
        EndEffectTime = Time.time + rootEffectData.duration;
    }
}
