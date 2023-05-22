using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FallViewData", menuName = "Data/Components/Gravity/View/Fall View Data")]
public class FallViewData : ScriptableObject
{
    public string animatorParameter = "IsFalling";
    public AudioClip landingAudioClip;
}
