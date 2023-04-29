using UnityEngine;

public class RollView
{
    private IRoll roll;
    private Animator animator;

    public RollView(IRoll roll, Animator animator)
    {
        this.roll = roll;
        this.animator = animator;

        roll.StartRollEvent.AddListener(OnStartRoll);
        roll.BreakRollEvent.AddListener(OnBreakRoll);
    }

    public void OnStartRoll()
    {
        animator.SetBool("IsRolling", true);
        animator.SetFloat("RollSpeed", 1f / roll.RollDuration);
    }

    public void OnBreakRoll()
    {
        animator.SetBool("IsRolling", false);
    }
}
