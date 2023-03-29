using UnityEngine;

public class MovementView : IMovementView
{
    private IMovement movement;
    private IGravity gravity;
    private Animator animator;
    private AudioSource audioSource;

    public MovementView(IMovement movement, IGravity gravity, Animator animator, AudioSource audioSource)
    {
        this.movement = movement;
        this.gravity = gravity;
        this.animator = animator;
        this.audioSource = audioSource;
    }

    public void SetMovementParams()
    {
        float speed = Mathf.Abs(movement.Speed);
        float relativeSpeed = Mathf.Abs(movement.Speed) / movement.MaxSpeed;

        animator.SetFloat("HorizontalSpeed", speed);

        if (gravity.IsGrounded == false || speed == 0f)
        {
            audioSource.Pause();
            return;
        }

        audioSource.pitch = relativeSpeed;
        if (audioSource.isPlaying == false)
        {
            //audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
        }
    }
}
