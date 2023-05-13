using UnityEngine;
using UnityEngine.VFX;

public class TessenAbilityView 
{
    private GameObject tessenObject;

    private string animatorParameter;
    private AudioClip startCastSoundEffect;
    private AudioClip releaseCastSoundEffect;
    private AudioClip breakCastSoundEffect;

    private Animator animator;
    private AudioSource audioSource;
    private VisualEffect tessenEffectGraph;
    private IAbility ability;
    private ITurning turning;
    private float xPos;

    public TessenAbilityView(GameObject tessenObject, TessenViewData tessenViewData, VisualEffect tessenEffectGraph, IAbility ability, ITurning turning, Animator animator, AudioSource audioSource)
    {
        this.tessenObject = tessenObject;

        animatorParameter = tessenViewData.animatorParameter;
        startCastSoundEffect = tessenViewData.startCastSoundEffect;
        releaseCastSoundEffect = tessenViewData.releaseCastSoundEffect;
        breakCastSoundEffect = tessenViewData.breakCastSoundEffect;

        this.animator = animator;
        this.audioSource = audioSource;
        this.tessenEffectGraph = tessenEffectGraph;
        this.ability = ability;
        this.turning = turning;
        xPos = tessenEffectGraph.GetFloat("xPositionOverPlayer");

        ability.StartCastEvent.AddListener(OnStartCast);
        ability.ReleaseCastEvent.AddListener(OnReleaseCast);
        ability.BreakCastEvent.AddListener(OnBreakCast);
    }


    private void OnStartCast()
    {
        tessenObject.SetActive(true);
        animator.SetBool(animatorParameter, true);
        PlaySoundEffect(startCastSoundEffect);
    }

    private void OnReleaseCast()
    {
        PlaySoundEffect(releaseCastSoundEffect);
        //PlayTessenVFXGraph();
    }

    private void OnBreakCast()
    {
        tessenObject.SetActive(false);
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

    public void PlayTessenVFXGraph()
    {
        switch (turning.Direction)
        {
            case eDirection.Right:
                tessenEffectGraph.SetFloat("xPositionOverPlayer", xPos);
                break;
            case eDirection.Left:
                tessenEffectGraph.SetFloat("xPositionOverPlayer", -xPos);
                break;

        }
        tessenEffectGraph.Play();
    }
}
