using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Talkative Object Data", menuName = "Component Data/Model/New Talkative Object Data", order = 510)]
public class TalkativeObjectData : ScriptableObject
{
    public string tooltip = "Press F to pay respect";
    public string[] speeches =
    {
        "Hello",
        "world!"
    };
}
