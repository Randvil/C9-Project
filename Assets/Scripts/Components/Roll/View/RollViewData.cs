using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RollViewData", menuName = "Data/Components/Roll/Roll View Data")]
public class RollViewData : ScriptableObject
{
    public string rollingAnimatorParameter = "IsRolling";
    public string rollSpeedAnimatorParameter = "RollSpeed";
    public AudioClip rollAudioClip;
}
