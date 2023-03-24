using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsoluteDamageModifier : IDamageModifier
{
    private float modifier;

    public AbsoluteDamageModifier(float modifier)
    {
        this.modifier = modifier;
    }

    public float ApplyModifier(float damage)
    {
        return Mathf.Clamp(damage + modifier, 0f, float.MaxValue);
    }
}
