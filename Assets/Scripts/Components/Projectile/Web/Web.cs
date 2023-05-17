using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : Projectile
{
    [SerializeField] protected SlowEffectData slowEffectData;
    [SerializeField] protected RootEffectData rootEffectData;
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
            effectableEnemy.EffectManager.AddEffect(new RootEffect(rootEffectData));
        }

        if (other.TryGetComponent(out Player player))
        {
            matArray = player.MainMesh.materials;
            matArray[1] = slowEffectData.webMaterial;
            player.MainMesh.materials = matArray;
        }

        Remove();
    }
}
