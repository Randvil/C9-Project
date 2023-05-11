using UnityEngine;
using UnityEngine.VFX;

public class KanaboAbilityView
{
    private GameObject kanaboObject;

    private string animatorParameter;
    private AudioClip startCastSoundEffect;
    private AudioClip releaseCastSoundEffect;
    private AudioClip breakCastSoundEffect;

    private Animator animator;
    private AudioSource audioSource;
    private VisualEffect kanaboEffectGraph;
    private IAbility ability;
    private ITurning turning;

    private float xPos;

    public KanaboAbilityView(GameObject kanaboObject, KanaboViewData kanaboViewData, VisualEffect kanaboEffectGraph, IAbility ability, ITurning turning, Animator animator, AudioSource audioSource)
    {
        this.kanaboObject = kanaboObject;

        animatorParameter = kanaboViewData.animatorParameter;
        startCastSoundEffect = kanaboViewData.startCastSoundEffect;
        releaseCastSoundEffect = kanaboViewData.releaseCastSoundEffect;
        breakCastSoundEffect = kanaboViewData.breakCastSoundEffect;

        this.animator = animator;
        this.audioSource = audioSource;
        this.kanaboEffectGraph = kanaboEffectGraph;
        this.ability = ability;
        this.turning = turning;
        xPos = kanaboEffectGraph.GetFloat("xPositionOverPlayer");

        ability.StartCastEvent.AddListener(OnStartCast);
        ability.ReleaseCastEvent.AddListener(OnReleaseCast);
        ability.BreakCastEvent.AddListener(OnBreakCast);
    }

    private void OnStartCast()
    {
        kanaboObject.SetActive(true);
        animator.SetBool(animatorParameter, true);
        PlaySoundEffect(startCastSoundEffect);
    }

    private void OnReleaseCast()
    {
        PlaySoundEffect(releaseCastSoundEffect);
        PlayKanaboVFXGraph();
    }

    private void OnBreakCast()
    {
        kanaboObject.SetActive(false);
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

    private void PlayKanaboVFXGraph()
    {
        switch (turning.Direction)
        {
            case eDirection.Right:
                kanaboEffectGraph.SetFloat("xPositionOverPlayer", xPos);
                break;
            case eDirection.Left:
                kanaboEffectGraph.SetFloat("xPositionOverPlayer", -xPos);
                break;

        }
        kanaboEffectGraph.Play();
    }
}
