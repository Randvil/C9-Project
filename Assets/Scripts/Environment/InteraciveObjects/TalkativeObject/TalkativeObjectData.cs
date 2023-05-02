using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTalkativeObjectData", menuName = "Data/Environment/New Talkative Object Data")]
public class TalkativeObjectData : ScriptableObject
{
    public string tooltip = "Press F to pay respect";
    public string[] speeches =
    {
        "Hello",
        "world!"
    };
}
