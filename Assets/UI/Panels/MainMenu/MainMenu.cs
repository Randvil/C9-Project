using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private VisualElement root;

    private PanelManager panelManager;

    [SerializeField] private PlayerInput input;

    private void Awake()
    {
        panelManager = GetComponentInParent<PanelManager>();

        root = GetComponent<UIDocument>().rootVisualElement;

        MenuNode main = new("main", root, true);

        MenuNode play = new("play", root, false);
        main.AddChild(play);

        MenuNode settings = new("settings", root, false);
        main.AddChild(settings);

        MenuNode load = new("load", root, false);
        play.AddChild(load);

        MenuNode video = new("video", root, false);
        settings.AddChild(video);

        MenuNode audio = new("audio", root, false);
        settings.AddChild(audio);

        MenuNode controls = new("controlsMenu", root, false);
        settings.AddChild(controls);

        //input.onActionTriggered += context =>
        //{
        //    if (context.action.name == "Return")
        //        ReturnToGame(main);
        //};
    }
}
