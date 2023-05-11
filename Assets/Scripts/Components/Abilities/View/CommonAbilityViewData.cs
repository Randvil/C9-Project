using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CommonAbilityView", menuName = "Data/Abilities/Common/New Common Ability View")]
public class CommonAbilityViewData : ScriptableObject
{
    public string animatorParameter = "IsUsingAbility";
    public AudioClip startCastSoundEffect;
    public AudioClip releaseCastSoundEffect;
    public AudioClip breakCastSoundEffect;
}
