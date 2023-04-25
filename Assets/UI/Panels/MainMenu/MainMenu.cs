using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private VisualElement root;

    [SerializeField] private PlayerInput input;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        MenuNode main = new("main", root, true);

        MenuNode play = new("play", root, false);
        main.AddChild(play);

        MenuNode settings = new("settings", root, false);
        main.AddChild(settings);

        MenuNode video = new("video", root, false);
        settings.AddChild(video);

        MenuNode audio = new("audio", root, false);
        settings.AddChild(audio);

        MenuNode controls = new("controlsMenu", root, false);
        settings.AddChild(controls);
    }
}
