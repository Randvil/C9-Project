using UnityEngine;

public class RollView
{
    private string rollingAnimatorParameter;
    private string rollSpeedAnimatorParameter;
    private AudioClip rollAudioClip;

    private IRoll roll;
    private Animator animator;
    private AudioSource audioSource;

    public RollView(RollViewData rollViewData, IRoll roll, Animator animator, AudioSource audioSource)
    {
        rollingAnimatorParameter = rollViewData.rollingAnimatorParameter;
        rollSpeedAnimatorParameter = rollViewData.rollSpeedAnimatorParameter;
        rollAudioClip = rollViewData.rollAudioClip;

        this.roll = roll;
        this.animator = animator;
        this.audioSource = audioSource;

        roll.StartRollEvent.AddListener(OnStartRoll);
        roll.BreakRollEvent.AddListener(OnBreakRoll);
    }

    public void OnStartRoll()
    {
        animator.SetBool(rollingAnimatorParameter, true);
        animator.SetFloat(rollSpeedAnimatorParameter, 1f / roll.RollDuration);

        audioSource.PlayOneShot(rollAudioClip);
    }

    public void OnBreakRoll()
    {
        animator.SetBool(rollingAnimatorParameter, false);
    }
}
