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

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        panelManager = panelManager.GetComponent<PanelManager>();

        backButton = root.Q<Button>("backButton");
        soundToggle = root.Q<Toggle>("soundToggle");
        effectsSlider = root.Q<Slider>("effectsVolumeSlider");
        musicSlider = root.Q<Slider>("musicVolumeSlider");
        resolutionsDropdown = root.Q<DropdownField>("resolutionsDropdown");

        resolutionsDropdown.choices = new()
        {
            "UHD 3840x2160",
            "QHD 2560x1440",
            "FHD 1920x1080",
            "HD  1366x768"
        };
        resolutionsDropdown.value = "FHD 1920x1080";

        soundToggle.RegisterValueChangedCallback(tog => GameSettings.SoundOn = soundToggle.value);

        backButton.clicked += panelManager.GoBack;

        effectsSlider.RegisterValueChangedCallback(_ => OnEffectsVolumeChange());
        musicSlider.RegisterValueChangedCallback(_ => OnMusicVolumeChanged());

        resolutionsDropdown.RegisterValueChangedCallback(_ => OnResolutionChanged());
    }

    private void OnResolutionChanged()
    {
        Resolution n = new() { refreshRate = Screen.currentResolution.refreshRate };
        switch (resolutionsDropdown.value)
        {
            case "HD 1366x768":
                n.height = 1366;
                n.width = 768;
                break;
            case "FHD 1920x1080":
                n.height = 1920;
                n.width = 1080;
                break;
            case "QHD 2560x1440":
                n.height = 1366;
                n.width = 768;
                break;
            case "UHD 3840x2160":
                n.height = 1366;
                n.width = 768;
                break;
            default:
                break;
        }
        GameSettings.ScreenResolution = n;
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
