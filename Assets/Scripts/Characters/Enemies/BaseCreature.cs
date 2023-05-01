using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseCreature : MonoBehaviour, ITeamMember, IDamageable, IEffectable
{
    [Header("Land Creature Prefab Components")]
    [SerializeField] protected GameObject avatar;
    [SerializeField] protected Slider healthBarSlider;

    [Header("Land Creature Data")]
    [SerializeField] protected eTeam initialTeam = eTeam.Enemies;
    [SerializeField] protected TurningViewData turningViewData;
    [SerializeField] protected HealthManagerData healthManagerData;
    [SerializeField] protected EffectManagerData effectManagerData;

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
    public AnimationAndSoundMovementView MovementView { get; protected set; }
    public IHealthBarView HealthBarView { get; protected set; }

    protected virtual void Awake()
    {
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
    }
}