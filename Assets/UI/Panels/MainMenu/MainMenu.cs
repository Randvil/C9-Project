using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    //private const string choosenClass = "choosen-element";

    private VisualElement root;

    private PanelManager panelManager;

    [SerializeField] private PlayerInput input;

    [SerializeField] private ComicsSwitcher comicsSwitcher;

    //Button choosenButton;

    private void Awake()
    {
        panelManager = GetComponentInParent<PanelManager>();

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

        Button newGameB = root.Q<Button>("newGameB");
        newGameB.clicked += () => ToComics(true);

        main.Panel.Q<Button>("quitB").clicked += Application.Quit;

        if (GetComponentInChildren<LoadMenu>().IsAnySaveFile)
        {
            Button comicsButton = play.Panel.Q<Button>("comicsB");
            comicsButton.clicked += () => ToComics(false);
            comicsButton.RemoveFromClassList("inactive-menu-b");
            comicsButton.AddToClassList("menu-b");
        }
    }

    private void ToComics(bool startNG)
    {
        comicsSwitcher.StartNGAfterComics = startNG;
        comicsSwitcher.ToNextPage();
        panelManager.SwitchTo(1);
    }

    //private void SetInput()
    //{
    //    input.onActionTriggered += context =>
    //    {
    //        switch (context.action.name)
    //        {
    //            case "Navigate":
    //                NavigateCommand(context.action.ReadValue<Vector2>());
    //                break;
    //        }
    //    };
    //}

    //private void ChooseNewButton(Button button)
    //{
    //    choosenButton.RemoveFromClassList(choosenClass);
    //    button.AddToClassList(choosenClass);
    //    choosenButton = button;
    //}

    //private void NavigateCommand(Vector2 vector)
    //{
    //    if (vector.magnitude == 0f)
    //        return;
    //}

    private void Start()
    {
        StaticAudio.Instance.ChangeBackgroundTrack("mainTheme");
    }
}
