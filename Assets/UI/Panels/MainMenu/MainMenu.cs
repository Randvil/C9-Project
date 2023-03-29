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

        MenuNode settings = new("settings", root, false);
        main.AddChild(settings);

        MenuNode video = new("video", root, false);
        settings.AddChild(video);

        MenuNode audio = new("audio", root, false);
        settings.AddChild(audio);

        MenuNode controls = new("controlsMenu", root, false);
        settings.AddChild(controls);


        main.Panel.Q<Button>("continueB").clicked += () => ReturnToGame(main);

        input.onActionTriggered += context =>
        {
            if (context.action.name == "Return")
                ReturnToGame(main);
        };
    }

    private void ReturnToGame(MenuNode main)
    {
        main.DeactivateChildren();
        DOTween.To(t => Time.timeScale = t, 0f, 1f, panelManager.PanelTweenDuration).SetUpdate(true);
        panelManager.GoBack();
        input.SwitchCurrentActionMap("Player");
    }
}
