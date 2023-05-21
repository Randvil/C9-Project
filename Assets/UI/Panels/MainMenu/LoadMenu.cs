using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadMenu : MonoBehaviour
{
    DirectoryInfo directory;
    const string path = "Saves";

    [SerializeField] private NewGameSave newGameSave;
    [SerializeField] private NewGameSave arcadeGameSave;
    [SerializeField] private NewGameSave bossroomGameSave;

    private PanelManager panelManager;

    private LoadScreen loadScreen;

    public LoadMenu()
    {
        if (!Directory.Exists(path))
            directory = Directory.CreateDirectory(path);
        else
            directory = new(path);
    }

    private void Awake()
    {
        panelManager = GetComponentInParent<PanelManager>();
        loadScreen = panelManager.GetComponentInChildren<LoadScreen>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button continueB = root.Q<Button>("continueB");

        if (IsAnySaveFile)
        {
            continueB.clicked += LoadSave;
            continueB.RemoveFromClassList("inactive-menu-b");
            continueB.AddToClassList("menu-b");
        }

        Button toArcadeCenter = root.Q<Button>("arcadeCenterB");
        toArcadeCenter.clicked += () => NewGame(arcadeGameSave);
        Button toBossfight = root.Q<Button>("bossfightB");
        toBossfight.clicked += () => NewGame(bossroomGameSave);
        Button toEndComics = root.Q<Button>("endComicsB");
        toEndComics.clicked += () =>
        {
            loadScreen.EndOfScene();
            StartCoroutine(LoadSceneCoroutine("TheEnd"));
        };
    }

    private void LoadSave()
    {
        loadScreen.EndOfScene();

        FileDataHandler handler = new("Saves", "LastSave");
        GameData gameData = handler.Load();

        DeathLoad deathLoad = new DeathLoad();
        deathLoad.RewriteData();

        StaticAudio.Instance.SnapshotName = "InGame";

        switch (gameData.CheckpointData.latestScene)
        {
            case eSceneName.CityLocation:
                StartCoroutine(LoadSceneCoroutine("CityLocation"));
                break;
            case eSceneName.ArcadeCenter:
                StartCoroutine(LoadSceneCoroutine("ArcadeCenter"));
                break;
            case eSceneName.BossLocation:
                StartCoroutine(LoadSceneCoroutine("BossLocation"));
                break;
        }
        
    }

    public bool IsAnySaveFile => directory.GetFiles("LastSave").Length > 0;

    public void NewGame() => NewGame(newGameSave);

    public void NewGame(NewGameSave gameSave)
    {
        loadScreen.EndOfScene(); // To load screen

        StaticAudio.Instance.SnapshotName = "InGame";

        gameSave.CreateNewGameSave();
        switch (gameSave.gameData.CheckpointData.latestScene)
        {
            case eSceneName.CityLocation:
                StartCoroutine(LoadSceneCoroutine("CityLocation"));
                break;
            case eSceneName.ArcadeCenter:
                StartCoroutine(LoadSceneCoroutine("ArcadeCenter"));
                break;
            case eSceneName.BossLocation:
                StartCoroutine(LoadSceneCoroutine("BossLocation"));
                break;
        }
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSecondsRealtime(panelManager.PanelTweenDuration);

        SceneManager.LoadScene(sceneName);
    }
}
