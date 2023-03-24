using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryView : IParryView
{
    private Animator animator;
    private IParry parry;

    public ParryView(IParry parry, Animator animator)
    {
        this.animator = animator;
        this.parry = parry;

        parry.ParryEvent.AddListener(OnParry);
    }

    public void StartParry()
    {
        animator.SetBool("IsParrying", true);
    }

    public void BreakParry()
    {
        animator.SetBool("IsParrying", false);
    }

    public void OnParry()
    {
        animator.SetTrigger("ParryTrigger");
    }

}
