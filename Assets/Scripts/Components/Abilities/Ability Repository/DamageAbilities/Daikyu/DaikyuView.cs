using UnityEngine;
using UnityEngine.VFX;

public class DaikyuView 
{
    private GameObject daikyuObject;

    private string animatorParameter;
    private AudioClip startCastSoundEffect;
    private AudioClip releaseCastSoundEffect;
    private AudioClip breakCastSoundEffect;

    private IAbility ability;
    private Animator animator;
    private AudioSource audioSource;

    public DaikyuView(DaikyuViewData daikyuViewData, GameObject daikyuObject, IAbility ability, Animator animator, AudioSource audioSource)
    {
        this.daikyuObject = daikyuObject;

        animatorParameter = daikyuViewData.animatorParameter;
        startCastSoundEffect = daikyuViewData.startCastSoundEffect;
        releaseCastSoundEffect = daikyuViewData.releaseCastSoundEffect;
        breakCastSoundEffect = daikyuViewData.breakCastSoundEffect;

        this.ability = ability;
        this.animator = animator;
        this.audioSource = audioSource;

        ability.StartCastEvent.AddListener(OnStartCast);
        ability.ReleaseCastEvent.AddListener(OnReleaseCast);
        ability.BreakCastEvent.AddListener(OnBreakCast);
    }

    public void OnStartCast()
    {
        daikyuObject.SetActive(true);
        animator.SetBool(animatorParameter, true);
        PlaySoundEffect(startCastSoundEffect);
    }

    public void OnReleaseCast()
    {
        PlaySoundEffect(releaseCastSoundEffect);
    }

    public void OnBreakCast()
    {
        daikyuObject.SetActive(false);
        animator.SetBool(animatorParameter, false);
        PlaySoundEffect(breakCastSoundEffect);
    }

    private void PlaySoundEffect(AudioClip soundEffect)
    {
        if (soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }
}
