using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour, IProjectile
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

    public void Initialize(eDirection direction, float rotation, float speed, eTeam ownerTeam, Damage damage)
    {
        this.ownerTeam = ownerTeam;
        this.damage = damage;

        float directionalSpeed = speed;
        float relativeRotation = rotation;
        if (direction == eDirection.Left)
        {
            directionalSpeed = -directionalSpeed;
            relativeRotation = -relativeRotation;
        }

        velocityVector = new Vector2(directionalSpeed, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, relativeRotation);

        SpawnEvent.Invoke();
    }

    public void Remove()
    {
        ExtinctionEvent.Invoke();

        Destroy(gameObject);
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
