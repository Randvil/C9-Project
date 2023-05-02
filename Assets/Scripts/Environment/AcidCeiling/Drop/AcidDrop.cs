using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AcidDrop : MonoBehaviour
{
    [SerializeField] private AcidDropData acidDropData;

    private DamageData explosionDamageData;
    private float explosionRadius;
    private GameObject acidPuddlePrefab;

    private GameObject ownerObject;
    private ITeam ownerTeam;
    private IModifierManager ownerModifierManager;
    private IDamageDealer damageDealer;
    private Explosion explosion;

    private void Awake()
    {
        explosionDamageData = acidDropData.explosionDamageData;
        explosionRadius = acidDropData.explosionRadius;
        acidPuddlePrefab = acidDropData.acidPuddlePrefab;
    }

    public void Initialize(GameObject ownerObject, ITeam ownerTeam, IModifierManager ownerModifierManager, IDamageDealer damageDealer)
    {
        this.ownerObject = ownerObject;
        this.ownerTeam = ownerTeam;
        this.ownerModifierManager = ownerModifierManager;
        this.damageDealer = damageDealer;

        Damage damage = new(ownerObject, gameObject, explosionDamageData, ownerModifierManager);
        explosion = new Explosion(explosionRadius, damage, ownerTeam, damageDealer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        explosion.Explode(transform.position);

        Vector2 contactPoint = new(transform.position.x, other.transform.position.y + other.bounds.extents.y);
        Instantiate(acidPuddlePrefab, contactPoint, acidPuddlePrefab.transform.rotation).GetComponent<AcidPuddle>().Initialize(ownerObject, ownerTeam, ownerModifierManager, damageDealer);

        Destroy(gameObject);
    }
}
