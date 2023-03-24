using UnityEngine;

public class RollView : IRollView
{
    private Animator animator;

    public RollView(Animator animator)
    {
        this.animator = animator;
    }

    public void StartRoll()
    {
        animator.SetBool("IsRolling", true);
    }

    public void BreakRoll()
    {
        animator.SetBool("IsRolling", false);
    }
}
