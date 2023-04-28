using UnityEngine;

public class DeathView
{
    private Animator animator;

    public DeathView(IDeathManager deathManager, Animator animator)
    {
        this.animator = animator;

        deathManager.DeathEvent.AddListener(OnDeath);
        deathManager.ResurrectionEvent.AddListener(OnResurrect);
    }

    public void OnDeath()
    {
        animator.SetBool("IsDying", true);
    }

    public void OnResurrect()
    {
        animator.SetBool("IsDying", false);
    }

}
