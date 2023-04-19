using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crouch Data", menuName = "Component Data/Model/New Crouch Data", order = 160)]
public class CrouchData : ScriptableObject
{
    public float colliderSizeMultiplier = 0.4f;
    public float movementSlow = 0.5f;
}
