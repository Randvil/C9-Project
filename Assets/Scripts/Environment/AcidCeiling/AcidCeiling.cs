using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AcidCeiling : MonoBehaviour, IDamageDealer
{
    [SerializeField] private AcidCeilingData acidCeilingData;
    private GameObject dropPrefab;
    private eTeam ceilingTeam;
    private float createDropPeriod;

    private int dropNumber = 0;

    private new BoxCollider2D collider;
    private ITeam team;
    private IModifierManager modifierManager;

    public UnityEvent<DamageInfo> DealDamageEventCallback { get; } = new();

    private void Awake()
    {
        dropPrefab = acidCeilingData.dropPrefab;
        ceilingTeam = acidCeilingData.ceilingTeam;
        createDropPeriod = acidCeilingData.createDropPeriod;

        collider = GetComponent<BoxCollider2D>();
        team = new CharacterTeam(ceilingTeam);
        modifierManager = new ModifierManager();
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad >= dropNumber * createDropPeriod)
        {
            CreateDrop();
            dropNumber++;
        }
    }

    private void CreateDrop()
    {
        float colliderXPosition = collider.bounds.center.x;
        float colliderLeftBound = colliderXPosition - collider.bounds.extents.x;
        float colliderRightBound = colliderXPosition + collider.bounds.extents.x;
        float randomXPosition = Random.Range(colliderLeftBound, colliderRightBound);
        Vector2 randomPosition = new(randomXPosition, collider.transform.position.y);

        Instantiate(dropPrefab, randomPosition, Quaternion.identity).GetComponent<AcidDrop>().Initialize(gameObject, team, modifierManager, this);
    }
}
