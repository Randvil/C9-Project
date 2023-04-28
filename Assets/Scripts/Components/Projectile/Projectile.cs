using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour, IReflectableProjectile
{
    protected GameObject projectileOwner;

    protected DamageData damageData;
    protected ProjectileData projectileData;
    protected GameObject prefab;
    protected float speed;
    protected float zRotation;
    protected float lifeTime;
    protected eDirection direction;

    protected ITeam ownerTeam;
    protected IModifierManager modifierManager;
    protected IDamageDealer damageDealer;

    protected Damage damage;

    public UnityEvent SpawnEvent { get; } = new();
    public UnityEvent ExtinctionEvent { get; } = new();

    protected virtual void Start()
    {
        transform.rotation = Quaternion.Euler(0f, (float)direction, zRotation);
        damage = new(projectileOwner, gameObject, damageData, modifierManager);
        Destroy(gameObject, lifeTime);
    }

    protected virtual void FixedUpdate()
    {
        transform.Translate(new Vector2(speed, 0f) * Time.fixedDeltaTime);
    }

    public void Initialize(GameObject projectileOwner, DamageData damageData, ProjectileData projectileData, eDirection direction, ITeam ownerTeam, IModifierManager modifierManager, IDamageDealer damageDealer)
    {
        this.projectileOwner = projectileOwner;
        this.damageData = damageData;
        this.projectileData = projectileData;
        this.direction = direction;
        this.ownerTeam = ownerTeam;
        this.modifierManager = modifierManager;
        this.damageDealer = damageDealer;

        prefab = projectileData.prefab;
        speed = projectileData.speed;
        zRotation = projectileData.zRotation;
        lifeTime = projectileData.lifeTime;
    }

    public void Remove()
    {
        ExtinctionEvent.Invoke();

        Destroy(gameObject);
    }

    public GameObject CreateReflectedProjectile(ITeam newTeam)
    {
        eDirection newDirection = direction == eDirection.Left ? eDirection.Right : eDirection.Left;

        GameObject reflectedProjectile = Instantiate(gameObject);
        reflectedProjectile.GetComponent<IReflectableProjectile>().Initialize(projectileOwner, damageData, projectileData, newDirection, newTeam, modifierManager, damageDealer);

        return reflectedProjectile;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ITeamMember creatureTeam) == false || creatureTeam.CharacterTeam.Team == ownerTeam.Team)
        {
            return;
        }

        if (other.TryGetComponent(out IDamageable damageableEnemy) == false)
        {
            return;
        }            

        damageableEnemy.DamageHandler.TakeDamage(damage, damageDealer.DealDamageEvent);

        Remove();
    }
}
