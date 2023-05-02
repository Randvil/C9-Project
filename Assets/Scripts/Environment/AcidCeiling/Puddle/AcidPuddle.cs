using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    [SerializeField] private AcidPuddleData acidPuddleData;

    private float puddleLifeTime;
    private float squashedPuddleLifeTime;
    private DoTEffectData doTEffectData;
    private SlowEffectData slowEffectData;

    private GameObject ownerObject;
    private ITeam ownerTeam;
    private IModifierManager ownerModifierManager;
    private IDamageDealer damageDealer;

    private List<Collider2D> affectedCharacters = new();

    private void Awake()
    {
        puddleLifeTime = acidPuddleData.puddleLifeTime;
        squashedPuddleLifeTime = acidPuddleData.squashedPuddleLifeTime;
        doTEffectData = acidPuddleData.doTEffectData;
        slowEffectData = acidPuddleData.slowEffectData;
    }

    public void Initialize(GameObject ownerObject, ITeam ownerTeam, IModifierManager ownerModifierManager, IDamageDealer damageDealer)
    {
        this.ownerObject = ownerObject;
        this.ownerTeam = ownerTeam;
        this.ownerModifierManager = ownerModifierManager;
        this.damageDealer = damageDealer;

        Destroy(gameObject, puddleLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (affectedCharacters.Contains(other) || ownerTeam.IsSame(other))
        {
            return;
        }

        affectedCharacters.Add(other);

        if (other.TryGetComponent(out IEffectable effectableCharacter))
        {
            effectableCharacter.EffectManager.AddEffect(new SlowEffect(slowEffectData));
        }

        if (other.TryGetComponent(out IDamageable damageableCharacter) && effectableCharacter != null)
        {
            effectableCharacter.EffectManager.AddEffect(new DoTEffect(ownerObject, gameObject, doTEffectData, ownerModifierManager, damageableCharacter.DamageHandler, damageDealer));
        }

        Destroy(gameObject, squashedPuddleLifeTime);
    }
}
