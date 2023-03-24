using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierManager : IModifierManager
{
    private List<IDamageModifier> absoluteModifiers = new();
    private List<IDamageModifier> relativeModiers = new();

    public void AddModifier(IDamageModifier modifier)
    {
        if (modifier == null)
        {
            throw new ArgumentNullException("You are trying to add null-modifier");
        }

        switch (modifier)
        {
            case AbsoluteDamageModifier:
                absoluteModifiers.Add(modifier);
                break;

            case RelativeDamageModifier:
                relativeModiers.Add(modifier);
                break;

            default:
                throw new NotImplementedException($"Unknown modifier type: {modifier.GetType()}");
        }
    }

    public void RemoveModifier(IDamageModifier modifier)
    {
        if (modifier == null)
        {
            throw new ArgumentNullException("You are trying to remove null-modifier");
        }

        switch (modifier)
        {
            case AbsoluteDamageModifier:
                absoluteModifiers.Remove(modifier);
                break;

            case RelativeDamageModifier:
                relativeModiers.Remove(modifier);
                break;

            default:
                throw new NotImplementedException($"Unknown modifier type: {modifier.GetType()}");
        }
    }

    public float ApplyModifiers(float damage)
    {
        float modifiedDamage = damage;

        foreach (IDamageModifier damageModifier in absoluteModifiers)
        {
            modifiedDamage = damageModifier.ApplyModifier(modifiedDamage);
        }

        foreach (IDamageModifier damageModifier in relativeModiers)
        {
            modifiedDamage = damageModifier.ApplyModifier(modifiedDamage);
        }

        return modifiedDamage;
    }
}
