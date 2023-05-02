using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IClimbableObject
{
    [SerializeField]
    private eDirection playerDirection;

    [SerializeField]
    private float relativeXPosition;

    public eDirection PlayerDirection => playerDirection;

    public float XPosition => transform.position.x + relativeXPosition;
}
