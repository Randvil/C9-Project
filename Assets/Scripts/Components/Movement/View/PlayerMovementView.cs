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
    }

    private void OnStartMove()
    {
        float speed = Mathf.Abs(movement.Speed);
        float relativeSpeed = speed / movement.MaxSpeed;

        animator.SetFloat("IdleRunSpeed", speed);
        animator.SetFloat("IdleRunBlend", 1f);

        audioSource.pitch = relativeSpeed;
        audioSource.Play();
    }

    private void OnBreakMove()
    {
        animator.SetFloat("IdleRunSpeed", 0f);
        animator.SetFloat("IdleRunBlend", 0f);

        audioSource.Pause();
    }

    //public void SetMovementParams()
    //{
    //    float speed = Mathf.Abs(movement.Speed);
    //    float relativeSpeed = Mathf.Abs(movement.Speed) / movement.MaxSpeed;
    //    float animationSpeed = speed;

    //    if (speed == 0f)
    //    {
    //        animationSpeed = 1f;
    //        animator.SetFloat("IdleRunSpeed", animationSpeed);
    //        animator.SetFloat("IdleRunBlend", 0f, 0.5f, 5f * Time.deltaTime);
    //    }
    //    else
    //    {
    //        animator.SetFloat("IdleRunSpeed", animationSpeed);
    //        animator.SetFloat("IdleRunBlend", 1f, 0.5f, 5f * Time.deltaTime);
    //    }        

    //    if (gravity.IsGrounded == false || speed == 0f)
    //    {
    //        audioSource.Pause();
    //        return;
    //    }

    //    audioSource.pitch = relativeSpeed;
    //    if (audioSource.isPlaying == false)
    //    {
    //        //audioSource.pitch = Random.Range(0.8f, 1.2f);
    //        audioSource.Play();
    //    }
    //}
}
