using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DaikyuViewData", menuName = "Data/Abilities/Daikyu/New Daikyu View Data")]
public class DaikyuViewData : ScriptableObject
{
    public string animatorParameter = "IsUsingDaikyu";
    public AudioClip startCastSoundEffect;
    public AudioClip releaseCastSoundEffect;
    public AudioClip breakCastSoundEffect;
}
