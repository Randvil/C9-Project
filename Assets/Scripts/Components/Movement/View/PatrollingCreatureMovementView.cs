using UnityEngine;

public class PatrollingCreatureMovementView : IMovementView
{
    private IMovement movement;
    private Animator animator;
    private AudioSource audioSource;

    public PatrollingCreatureMovementView(IMovement movement, Animator animator, AudioSource audioSource)
    {
        this.movement = movement;
        this.animator = animator;
        this.audioSource = audioSource;
    }

    public void SetMovementParams()
    {
        float speed = Mathf.Abs(movement.Speed);
        float relativeSpeed = Mathf.Abs(movement.Speed) / movement.MaxSpeed;

        animator.SetFloat("HorizontalSpeed", speed);

        audioSource.pitch = relativeSpeed;
        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
    }
}
