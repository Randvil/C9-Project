using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeDamageModifier : IDamageModifier
{
    private float modifier;

    public RelativeDamageModifier(float modifier)
    {
        this.modifier = modifier;
    }

    public  float ApplyModifier(float damage)
    {
        return Mathf.Clamp(1f + modifier, 0f, float.MaxValue) * damage;
    }
}
