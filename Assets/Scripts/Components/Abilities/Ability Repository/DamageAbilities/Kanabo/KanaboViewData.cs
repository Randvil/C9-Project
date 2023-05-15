using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KanaboViewData", menuName = "Data/Abilities/Kanabo/New Kanabo View Data")]
public class KanaboViewData : ScriptableObject
{
    public string animatorParameter = "IsUsingKanabo";
    public AudioClip startCastSoundEffect;
    public AudioClip releaseCastSoundEffect;
    public AudioClip breakCastSoundEffect;
}
