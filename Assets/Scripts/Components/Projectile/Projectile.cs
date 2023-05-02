using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] protected Collider2D damageCollider;
    [SerializeField] private ProjectileData projectileData;
    protected float speed;
    protected float zRotation;
    protected float lifeTime;

    protected GameObject projectileOwner;
    protected DamageData damageData;
    protected eDirection direction;

    protected ITeam ownerTeam;
    protected IModifierManager modifierManager;
    protected IDamageDealer damageDealer;

    protected Damage damage;

    public bool Released { get; private set; }
    public UnityEvent SpawnEvent { get; } = new();
    public UnityEvent ExtinctionEvent { get; } = new();

    protected virtual void FixedUpdate()
    {
        if (Released == false)
        {
            return;
        }

        transform.Translate(new Vector2(speed, 0f) * Time.fixedDeltaTime);
    }

    public virtual void Release(GameObject projectileOwner, DamageData damageData, eDirection direction, ITeam ownerTeam, IModifierManager modifierManager, IDamageDealer damageDealer)
    {
        this.projectileOwner = projectileOwner;
        this.damageData = damageData;
        this.direction = direction;
        this.ownerTeam = ownerTeam;
        this.modifierManager = modifierManager;
        this.damageDealer = damageDealer;

        speed = projectileData.speed;
        zRotation = projectileData.zRotation;
        lifeTime = projectileData.lifeTime;

        damageCollider.enabled = true;
        transform.rotation = Quaternion.Euler(0f, (float)direction, zRotation);
        damage = new(projectileOwner, gameObject, damageData, modifierManager);
        Destroy(gameObject, lifeTime);

        Released = true;
    }

    public virtual void Remove()
    {
        Destroy(gameObject, 0.01f);

        ExtinctionEvent.Invoke();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (ownerTeam.IsSame(other))
        {
            return;
        }

        if (other.TryGetComponent(out IDamageable damageableEnemy) == true)
        {
            damageableEnemy.DamageHandler.TakeDamage(damage, damageDealer);
            Remove();
        }
    }
}
