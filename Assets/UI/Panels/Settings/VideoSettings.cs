using UnityEngine;
using UnityEngine.UIElements;

public class VideoSettings : MonoBehaviour
{
    private DropdownField resolutionsDropdown;

    private SliderInt qualitySlider;

    private Toggle fullScreenToggle;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        RegisterResolutions(root);

        RegisterQualities(root);

        RegisterFullScreen(root);
    }

    private void RegisterFullScreen(VisualElement root)
    {
        fullScreenToggle = root.Q<Toggle>("fullscreen");
        fullScreenToggle.value = Screen.fullScreen;

        fullScreenToggle.RegisterValueChangedCallback(evt => Screen.fullScreen = evt.newValue);
    }

    private void RegisterResolutions(VisualElement root)
    {
        resolutionsDropdown = root.Q<DropdownField>("resolutions");

        resolutionsDropdown.choices = new();

        foreach (Resolution res in Screen.resolutions)
            resolutionsDropdown.choices.Add($"{res.width}x{res.height} {res.refreshRate}@");

        Resolution curRes = Screen.currentResolution;
        resolutionsDropdown.value = $"{curRes.width}x{curRes.height} {curRes.refreshRate}@";

        resolutionsDropdown.RegisterValueChangedCallback(_ => OnResolutionChanged());
    }

    private void RegisterQualities(VisualElement root)
    {
        qualitySlider = root.Q<SliderInt>("quality");

        qualitySlider.highValue = QualitySettings.names.Length - 1;
        qualitySlider.value = QualitySettings.GetQualityLevel();

        qualitySlider.RegisterValueChangedCallback(evt => QualitySettings.SetQualityLevel(evt.newValue));
    }

    private void OnResolutionChanged()
    {
        string[] parsed = resolutionsDropdown.value.Split(' ', 'x', '@');
        Resolution n = new() { width = int.Parse(parsed[0]), height = int.Parse(parsed[1]), refreshRate = int.Parse(parsed[2]) };

        Screen.SetResolution(n.width, n.height, true, n.refreshRate);
    }
}
