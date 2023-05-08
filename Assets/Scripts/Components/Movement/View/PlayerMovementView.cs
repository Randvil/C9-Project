using UnityEngine;

public class PlayerMovementView
{
    private IMovement movement;
    private IGravity gravity;
    private Animator animator;
    private AudioSource audioSource;

    public PlayerMovementView(IMovement movement, IGravity gravity, Animator animator, AudioSource audioSource)
    {
        this.movement = movement;
        this.gravity = gravity;
        this.animator = animator;
        this.audioSource = audioSource;

        movement.StartMoveEvent.AddListener(OnStartMove);
        movement.BreakMoveEvent.AddListener(OnBreakMove);
        gravity.GroundedEvent.AddListener(OnStartMove);
        gravity.LostGroundEvent.AddListener(OnBreakMove);
    }

    private void OnStartMove()
    {
        if (gravity.IsGrounded && movement.IsMoving)
        {
            float speed = Mathf.Abs(movement.Speed);
            float relativeSpeed = speed / movement.MaxSpeed;

            animator.SetFloat("HorizontalSpeed", speed);

            audioSource.pitch = relativeSpeed;
            audioSource.Play();
        }
        else
        {
            OnBreakMove();
        }
    }

    private void OnBreakMove()
    {
        animator.SetFloat("HorizontalSpeed", 0f);

        audioSource.Pause();
    }
}
