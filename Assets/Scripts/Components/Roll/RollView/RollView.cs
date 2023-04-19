using UnityEngine;

public class RollView : IRollView
{
    private IRoll roll;
    private Animator animator;

    public RollView(IRoll roll, Animator animator)
    {
        this.roll = roll;
        this.animator = animator;
    }

    public void StartRoll()
    {
        animator.SetBool("IsRolling", true);
        animator.SetFloat("RollSpeed", 1f / roll.RollDuration);
    }

    public void BreakRoll()
    {
        animator.SetBool("IsRolling", false);
    }
}
