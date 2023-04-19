using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer mixer;

    private AudioMixerSnapshot inGameSnapshot;
    private AudioMixerSnapshot pauseSnapshot;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        inGameSnapshot = mixer.FindSnapshot("InGame");
        pauseSnapshot = mixer.FindSnapshot("Pause");

        RegisterVolumeSliders(root);
    }

    /// <summary>
    /// UI elements must have same names as Exposed parameters
    /// </summary>
    private void RegisterVolumeSliders(VisualElement root)
    {
        var sliders = root.Q<VisualElement>("audio").Query<Slider>().ToList();

        foreach (var slider in sliders)
        {
            slider.highValue = 1f;
            slider.lowValue = 0.00001f;

            slider.RegisterValueChangedCallback
                (evt => SetVolumeOnGroupInAllShanpshots(slider.name, Mathf.Log10(evt.newValue) * 20f));

            mixer.GetFloat(slider.name, out float t);
            slider.value = Mathf.Pow(10f, t / 20f);
        }
    }

    /// <summary>
    /// Из-за крайней ограничености api миксера, приходиться юзать костыль с переключением снапшотов,
    /// иначе изменение громкости в одном снапшоте не отразится на другом
    /// </summary>
    private void SetVolumeOnGroupInAllShanpshots(string paramName, float vol)
    {
        mixer.SetFloat(paramName, vol);
        inGameSnapshot.TransitionTo(0f);
        mixer.SetFloat(paramName, vol);
        pauseSnapshot.TransitionTo(0f);
    }
}
