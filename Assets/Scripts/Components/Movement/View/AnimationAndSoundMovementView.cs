using UnityEngine;

public class AnimationAndSoundMovementView
{
    private IMovement movement;
    private Animator animator;
    private AudioSource audioSource;

    public AnimationAndSoundMovementView(IMovement movement, Animator animator, AudioSource audioSource)
    {
        this.movement = movement;
        this.animator = animator;
        this.audioSource = audioSource;

        movement.StartMoveEvent.AddListener(OnStartMove);
        movement.BreakMoveEvent.AddListener(OnBreakMove);
    }

    public void OnStartMove()
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

    public void OnBreakMove()
    {
        animator.SetFloat("HorizontalSpeed", 0);
        audioSource.Stop();
    }
}
