using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class PlayerTakeDamageView
{
    private IDamageHandler damageHandler;
    private AudioSource audioSource;
    private VolumeProfile volumeProfile;
    private Vignette vignette;

    public PlayerTakeDamageView(IDamageHandler damageHandler, AudioSource audioSource, Volume volume)
    {
        this.damageHandler = damageHandler;
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
        
        ShowRedVignette();
    }

    public async void ShowRedVignette()
    {
        if (volumeProfile.TryGet(out vignette))
        {
            vignette.intensity.value = 0.3f;
            await Task.Delay(100);
            vignette.intensity.value = 0;
        }
            
    }
}
