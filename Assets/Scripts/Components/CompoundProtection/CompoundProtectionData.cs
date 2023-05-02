using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCompoundProtectionData", menuName = "Data/Compound Protection/New Compound Protection Data")]
public class CompoundProtectionData : ScriptableObject
{
    [Range(0f, 1f), Tooltip("How low must health be to use defensive abilities [parts of max health]")]
    public float relativeHealthThreshold = 0.75f;
}
