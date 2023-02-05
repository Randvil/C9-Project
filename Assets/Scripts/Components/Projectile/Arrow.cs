using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour, IReflectableProjectile
{
    private eTeam ownerTeam;
    private Damage damage;
    private Vector2 velocityVector;

    public UnityEvent SpawnEvent { get; } = new();
    public UnityEvent ExtinctionEvent { get; } = new();

    private void Update()
    {
        transform.Translate(velocityVector * Time.deltaTime);
    }

    public void Initialize(eDirection direction, float zRotation, float speed, eTeam ownerTeam, Damage damage)
    {
        float directionalSpeed = speed;
        float relativeRotation = zRotation;
        if (direction == eDirection.Left)
        {
            directionalSpeed = -directionalSpeed;
            relativeRotation = -relativeRotation;
        }

        Vector3 velocityVector = new Vector2(directionalSpeed, 0f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, relativeRotation);

        Initialize(velocityVector, rotation, ownerTeam, damage);
    }

    public void Initialize(Vector2 velocityVector, Quaternion rotation, eTeam ownerTeam, Damage damage)
    {
        this.ownerTeam = ownerTeam;
        this.damage = damage;
        this.damage.sourceObject = gameObject;
        this.velocityVector = velocityVector;
        transform.rotation = rotation;

        SpawnEvent.Invoke();
    }

    public void Remove()
    {
        ExtinctionEvent.Invoke();

        Destroy(gameObject);
    }

    public GameObject CreateReflectedProjectile(eTeam newTeam)
    {
        GameObject reflectedProjectile = Instantiate(gameObject);
        reflectedProjectile.GetComponent<IReflectableProjectile>().Initialize(-velocityVector, transform.rotation, newTeam, damage);        

        return reflectedProjectile;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ITeam hitCreature = other.GetComponent<ITeam>();
        if (hitCreature == null || hitCreature.Team == ownerTeam)
            return;

        IDamageHandler damageableEnemy = other.GetComponent<IDamageHandler>();
        if (damageableEnemy == null)
            return;

        damageableEnemy.TakeDamage(damage);

        Remove();
    }

    private void OnBecameInvisible()
    {
        Remove();
    }

}
