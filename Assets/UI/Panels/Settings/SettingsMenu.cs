using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    private VisualElement root;

    public PanelManager panelManager;

    public AudioMixer mixer;

    private Toggle soundToggle;

    private Slider effectsSlider;
    private Slider musicSlider;

    private DropdownField resolutionsDropdown;

    private Button backButton;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        panelManager = panelManager.GetComponent<PanelManager>();

        backButton = root.Q<Button>("backButton");
        soundToggle = root.Q<Toggle>("soundToggle");
        effectsSlider = root.Q<Slider>("effectsVolumeSlider");
        musicSlider = root.Q<Slider>("musicVolumeSlider");
        resolutionsDropdown = root.Q<DropdownField>("resolutionsDropdown");

        resolutionsDropdown.choices = new();

        foreach (Resolution res in Screen.resolutions)
            resolutionsDropdown.choices.Add($"{res.width}x{res.height} {res.refreshRate}@");

        Resolution curRes = Screen.currentResolution;
        resolutionsDropdown.value = $"{curRes.width}x{curRes.height} {curRes.refreshRate}@";


        soundToggle.RegisterValueChangedCallback(tog => GameSettings.SoundOn = soundToggle.value);

        backButton.clicked += panelManager.GoBack;

        effectsSlider.RegisterValueChangedCallback(_ => OnEffectsVolumeChange());
        musicSlider.RegisterValueChangedCallback(_ => OnMusicVolumeChanged());

        resolutionsDropdown.RegisterValueChangedCallback(_ => OnResolutionChanged());
    }

    private void OnResolutionChanged()
    {
        string[] parsed = resolutionsDropdown.value.Split(' ', 'x', '@');
        Resolution n = new() { width = int.Parse(parsed[0]), height = int.Parse(parsed[1]), refreshRate = int.Parse(parsed[2]) };

        Screen.SetResolution(n.width, n.height, true, n.refreshRate);
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
