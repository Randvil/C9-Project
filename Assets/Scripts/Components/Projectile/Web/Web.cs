using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Web : Projectile
{
    [SerializeField] protected SlowEffectData slowEffectData;
    private Material[] matArray;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (ownerTeam.IsSame(other))
        {
            return;
        }

        if (other.TryGetComponent(out IDamageable damageableEnemy) == true)
        {
            damageableEnemy.DamageHandler.TakeDamage(damage, damageDealer);
        }

        if (other.TryGetComponent(out IEffectable effectableEnemy) == true)
        {
            effectableEnemy.EffectManager.AddEffect(new SlowEffect(slowEffectData));
            matArray = other.GetComponentInChildren<SkinnedMeshRenderer>().materials;
            matArray[1] = slowEffectData.webMaterial;
            other.GetComponentInChildren<SkinnedMeshRenderer>().materials = matArray;
        }

        Remove();
    }
}
