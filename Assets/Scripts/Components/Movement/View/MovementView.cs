using UnityEngine;

public class MovementView : IMovementView
{
    private IMovement movement;
    private Animator animator;

    public MovementView(IMovement movement, Animator animator)
    {
        this.movement = movement;
        this.animator = animator;
    }

    public void SetMovementParams()
    {
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(movement.Speed));
    }
}
