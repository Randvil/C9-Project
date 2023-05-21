using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BroodmotherSelfStun : ITalent
{
    private StunEffectData stunEffectData;

    private IHealthManager shieldManager;
    private IEffectManager effectManager;

    public eTalentType TalentType => eTalentType.Unknown;
    public UnityEvent<eTalentType> LearnEvent { get; }
    public UnityEvent<eTalentType> ForgetEvent { get; }

    public BroodmotherSelfStun(StunEffectData stunEffectData, IEffectManager effectManager, IHealthManager shieldManager)
    {
        this.stunEffectData = stunEffectData;

        this.shieldManager = shieldManager;
        this.effectManager = effectManager;
    }

    public void Learn()
    {
        shieldManager.CurrentHealthChangedEvent.AddListener(SelfStun);
    }

    public void Forget()
    {
        shieldManager.CurrentHealthChangedEvent.RemoveListener(SelfStun);
    }

    private void SelfStun(Health health)
    {
        if (health.currentHealth > 0f)
        {
            return;
        }

        effectManager.AddEffect(new StunEffect(stunEffectData));
    }
}
