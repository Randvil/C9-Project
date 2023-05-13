using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class AscensionArea : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private AscensionAreaData ascensionAreaData;

    private float lifetime;
    private float impactPeriod;
    private float ascensionalPower;
    private StunEffectData stunEffectData;
    private DamageData damageData;

    private IModifierManager modifierManager;
    private ITeam team;
    private IDamageDealer damageDealer;

    private Damage damage;
    private Dictionary<GameObject, IDamageHandler> damagedEnemies = new();
    private Dictionary<IEffectManager, IEffect> stunnedEnemies = new();

    public UnityEvent SpawnEvent { get; } = new();

    private void Awake()
    {
        lifetime = ascensionAreaData.lifetime;
        impactPeriod = ascensionAreaData.impactPeriod;
        ascensionalPower = ascensionAreaData.ascensionalPower;
        stunEffectData = ascensionAreaData.stunEffectData;
        damageData = ascensionAreaData.damageData;

        AscensionAreaView ascensionAreaView = new AscensionAreaView(this, audioSource, visualEffect);

        SpawnEvent.Invoke();
    }

    public void Initialize(GameObject caster, IModifierManager modifierManager, ITeam team, IDamageDealer damageDealer)
    {
        this.modifierManager = modifierManager;
        this.team = team;
        this.damageDealer = damageDealer;

        damage = new Damage(caster, gameObject, damageData, modifierManager);

        StartCoroutine(DamageCoroutine());
        Destroy(gameObject, lifetime);
    }

    private IEnumerator DamageCoroutine()
    {
        while (true)
        {
            foreach (KeyValuePair<GameObject, IDamageHandler> damagedEnemy in damagedEnemies)
            {
                if (damagedEnemy.Key != null)
                {
                    damagedEnemy.Value.TakeDamage(damage, damageDealer);
                }
            }

            yield return new WaitForSeconds(impactPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (team.IsSame(enemy))
        {
            return;
        }

        if (enemy.TryGetComponent(out IGravity enemyGravity) == true)
        {
            enemyGravity.Disable(this);
            Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
            enemyRigidbody.velocity = new(enemyRigidbody.velocity.x, ascensionalPower);
        }

        if (enemy.TryGetComponent(out IEffectable stunnableEnemy) == true
            && stunnedEnemies.ContainsKey(stunnableEnemy.EffectManager) == false)
        {
            IEffect stunEffect = new StunEffect(stunEffectData);
            stunnableEnemy.EffectManager.AddEffect(stunEffect);
            stunnedEnemies.Add(stunnableEnemy.EffectManager, stunEffect);
        }

        if (enemy.TryGetComponent(out IDamageable damageableEnemy) == true
            && damagedEnemies.ContainsKey(enemy.gameObject) == false)
        {
            damagedEnemies.Add(enemy.gameObject, damageableEnemy.DamageHandler);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damagedEnemies.ContainsKey(collision.gameObject))
        {
            GameObject enemy = collision.gameObject;
            Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
            enemyRigidbody.velocity = new(enemyRigidbody.velocity.x, 0f);
        }
    }

    private void OnDestroy()
    {
        foreach (KeyValuePair<GameObject, IDamageHandler> damagedEnemy in damagedEnemies)
        {
            if (damagedEnemy.Key != null)
            {
                GameObject enemy = damagedEnemy.Key;
                if (enemy.TryGetComponent(out IGravity enemyGravity) == true)
                {
                    enemyGravity.Enable(this);
                    Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                    enemyRigidbody.velocity = new(enemyRigidbody.velocity.x, 0f);
                }
            }
        }

        foreach (KeyValuePair<IEffectManager, IEffect> stunnedEnemy in stunnedEnemies)
        {
            stunnedEnemy.Key.RemoveEffect(stunnedEnemy.Value);
        }
    }
}
