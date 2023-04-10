using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Data", menuName = "Save System Data/Model/New Game Data", order = 170)]
public class NewGameData : ScriptableObject
{
    public Data CheckpointData;

    public Data CurrentGameData;


}
