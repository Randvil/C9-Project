using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParryViewData", menuName = "Data/Components/Parry/New Parry View Data")]
public class ParryViewData : ScriptableObject
{
    public string stanceAnimatorParameter = "IsParrying";
    public string triggerAnimatorParameter = "ParryTrigger";
    public AudioClip startParrySound;
    public AudioClip successfulParrySound;
    public AudioClip breakParrySound;
}
