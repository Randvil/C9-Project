using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private VisualElement root;

    private PanelManager panelManager;

    [SerializeField] private PlayerInput input;

    [SerializeField] private ComicsSwitcher comicsSwitcher;

    MenuNode main;

    private void Awake()
    {
        panelManager = GetComponentInParent<PanelManager>();

        root = GetComponent<UIDocument>().rootVisualElement;

        main = new("main", root, false);
        
        MenuNode play = new("play", root, false);
        main.AddChild(play);

        MenuNode settings = new("settings", root, false);
        main.AddChild(settings);

        MenuNode spoilers = new("spoilers", root, false);
        main.AddChild(spoilers);

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
            Button comicsButton = spoilers.Panel.Q<Button>("comicsB");
            comicsButton.clicked += () => ToComics(false);
            comicsButton.RemoveFromClassList("inactive-menu-b");
            comicsButton.AddToClassList("menu-b");
        }

        comicsSwitcher.ComicsEndsEvent.AddListener(() => StaticAudio.Instance.ChangeBackgroundTrack("mainTheme"));  
    }

    private void ToComics(bool startNG)
    {
        StaticAudio.Instance.ChangeBackgroundTrack("startComicsTheme");

        comicsSwitcher.StartNGAfterComics = startNG;
        comicsSwitcher.ToNextPage();
        panelManager.SwitchTo(1);
    }

    private void Start()
    {
        panelManager.SwitchTo(0);
        StaticAudio.Instance.ChangeBackgroundTrack("mainTheme");
        StartCoroutine(ActivateMenuCoroutine());
    }

    private IEnumerator ActivateMenuCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        main.Active = true;
    }
}
