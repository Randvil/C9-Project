using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    private VisualElement root;

    private PanelManager panelManager;

    public AudioMixer mixer;

    private Slider effectsSlider;
    private Slider musicSlider;

    private DropdownField resolutionsDropdown;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        panelManager = GetComponentInParent<PanelManager>();

        effectsSlider = root.Q<Slider>("effects");
        musicSlider = root.Q<Slider>("music");
    }   

    private void OnEffectsVolumeChange()
    {
        mixer.SetFloat("EffectsVolume", Mathf.Lerp(-80f, 0f, effectsSlider.value));
    }

    private void OnMusicVolumeChanged()
    {
        mixer.SetFloat("MusicVolume", Mathf.Lerp(-80f, 0f, effectsSlider.value));
    }
}
