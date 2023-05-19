using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerWeaponViewData", menuName = "Data/Weapon/View/New Player Weapon View Data")]
public class PlayerWeaponViewData : ScriptableObject
{
    public AudioClip takeSwordAudioClip;
    public AudioClip putAwaySwordAudioClip;
    public AudioClip[] hitAudioClips;
    public AudioClip missAudioClip;
}
