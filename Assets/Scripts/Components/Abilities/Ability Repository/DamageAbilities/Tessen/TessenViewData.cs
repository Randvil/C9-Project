using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TessenViewData", menuName = "Data/Abilities/Tessen/New Tessen View Data")]
public class TessenViewData : ScriptableObject
{
    public string animatorParameter = "IsUsingTessen";
    public AudioClip startCastSoundEffect;
    public AudioClip releaseCastSoundEffect;
    public AudioClip breakCastSoundEffect;
}
