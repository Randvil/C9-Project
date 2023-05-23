using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseCreature : MonoBehaviour, ITeamMember, IDamageable, IEffectable, IMortal
{
    [Header("Land Creature Prefab Components")]
    [SerializeField] protected GameObject avatar;
    [SerializeField] protected Slider healthBarSlider;
    [SerializeField] protected SkinnedMeshRenderer[] meshesToCloneMaterials;
    [SerializeField] protected AudioSource sharedAudioSource;

    [Header("Land Creature Data")]
    [SerializeField] protected eTeam initialTeam = eTeam.Enemies;
    [SerializeField] protected HealthManagerData healthManagerData;
    [SerializeField] protected EffectManagerData effectManagerData;
    [SerializeField] protected TurningViewData turningViewData;
    [SerializeField] protected DeathViewData deathViewData;

    public BoxCollider2D Collider { get; protected set; }
    public Rigidbody2D Rigidbody { get; protected set; }
    public Animator Animator { get; protected set; }
    public IGravity Gravity { get; protected set; }
    public ITeam CharacterTeam { get; protected set; }
    public ITurning Turning { get; protected set; }
    public IHealthManager HealthManager { get; protected set; }
    public IModifierManager DefenceModifierManager { get; protected set; }
    public IEffectManager EffectManager { get; protected set; }
    public IDamageHandler DamageHandler { get; protected set; }
    public IDeathManager DeathManager { get; protected set; }

    public TurningView TurningView { get; protected set; }
    public MovementView MovementView { get; protected set; }
    public IHealthBarView HealthBarView { get; protected set; }
    public DeathView DeathView { get; protected set; }

    protected IAIBehavior currentBehavior;

    protected virtual void Awake()
    {
        CloneMaterials();

        Collider = GetComponent<BoxCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Gravity = GetComponent<IGravity>();

        CharacterTeam = new CharacterTeam(initialTeam);
        HealthManager = new HealthManager(healthManagerData);
        DefenceModifierManager = new ModifierManager();
        EffectManager = new EffectManager(this, effectManagerData);
        DeathManager = new DeathManager(HealthManager);
        DamageHandler = new DamageHandler(HealthManager, DefenceModifierManager, EffectManager, DeathManager);
        Turning = new Turning();

        TurningView = new TurningView(this, avatar, turningViewData, Turning);
        HealthBarView = new HealthBarView(healthBarSlider, HealthManager, DeathManager);
        DeathView = new DeathView(this, deathViewData, DeathManager, Animator, sharedAudioSource);

        //DeathManager.DeathEvent.AddListener(OnDeath);
    }

    protected void CloneMaterials()
    {
        if (meshesToCloneMaterials.Length == 0)
        {
            return;
        }

        for (int j = 0; j < meshesToCloneMaterials.Length; j++)
        {
            Material[] newMaterials = new Material[meshesToCloneMaterials[j].materials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = Instantiate(meshesToCloneMaterials[j].materials[i]);
            }

            meshesToCloneMaterials[j].materials = newMaterials;
        }
    }

    protected virtual void OnDeath()
    {
        if (currentBehavior != null)
        {
            currentBehavior.Deactivate();
        }

        Destroy(gameObject, 1.5f);
    }

    protected void DeactivateBehaviour()
    {
        if (currentBehavior != null)
        {
            currentBehavior.Deactivate();
        }
    }
}