using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNoArmsWeaponViewData", menuName = "Data/Weapon/View/New No Arms Weapon View Data")]
public class NoArmsWeaponViewData : ScriptableObject
{
    public AudioClip startAttackAudioClip;
    public AudioClip releaseAttackAudioClip;
}
