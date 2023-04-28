using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunView
{
    private Animator animator;

    public StunView(IEffectManager effectManager, Animator animator)
    {
        this.animator = animator;

        effectManager.EffectEvent.AddListener(OnStunStatusChange);
    }

    public void OnStunStatusChange(eEffectType effectType, eEffectStatus effectStatus)
    {
        if (effectType != eEffectType.Stun)
        {
            return;
        }

        if (effectStatus == eEffectStatus.Added)
        {
            animator.SetBool("IsStunned", true);
        }
        else if (effectStatus == eEffectStatus.Cleared)
        {
            animator.SetBool("IsStunned", false);
        }
    }
}
