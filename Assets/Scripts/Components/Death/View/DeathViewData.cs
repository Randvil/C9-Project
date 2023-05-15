using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathViewData", menuName = "Data/Death/New Death View Data")]
public class DeathViewData : ScriptableObject
{
    public string animatorParameter = "IsDying";
    public AudioClip deathAudioClip;
}
