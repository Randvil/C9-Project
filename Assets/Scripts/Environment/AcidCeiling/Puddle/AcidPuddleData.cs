using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AcidPuddleData", menuName = "Data/Acid Ceiling/New Acid Puddle Data")]
public class AcidPuddleData : ScriptableObject
{
    [Min(0f), Tooltip("Puddle will be destroyed this time after spawn [seconds]")]
    public float puddleLifeTime;
    [Min(0f), Tooltip("If someone steps into a puddle, it can be destroyed sooner [seconds]")]
    public float squashedPuddleLifeTime;
    public DoTEffectData doTEffectData;
    public SlowEffectData slowEffectData;
}
