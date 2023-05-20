using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class PlayerTakeDamageView
{
    private MonoBehaviour owner;
    private AudioSource audioSource;
    private VolumeProfile volumeProfile;
    private Vignette vignette;

    public PlayerTakeDamageView(IDamageHandler damageHandler, AudioSource audioSource, Volume volume, MonoBehaviour owner)
    {
        this.owner = owner;
        this.audioSource = audioSource;
        volumeProfile = volume.sharedProfile;

        damageHandler.TakeDamageEvent.AddListener(OnTakeDamage);
    }

    public void OnTakeDamage(DamageInfo damageInfo)
    {
        if (damageInfo.effectiveDamageValue <= 0f)
        {
            return;
        }

        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
        }

        owner.StartCoroutine(ShowVignetteCoroutine());
    }

    public IEnumerator ShowVignetteCoroutine()
    {
        if (volumeProfile.TryGet(out vignette))
        {
            vignette.intensity.value = 0.4f;

            float counter = 0.4f;
            while (vignette.intensity.value > 0)
            {
                counter -= 0.001f;
                vignette.intensity.value = counter;
                yield return new WaitForSeconds(0.001f);
            }
            
        }
    }

}
