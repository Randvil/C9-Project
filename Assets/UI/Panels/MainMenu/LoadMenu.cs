using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadMenu : MonoBehaviour
{
    DirectoryInfo directory = new("Saves");

    [SerializeField] private NewGameSave newGameSave;

    private PanelManager panelManager;

    private void Awake()
    {
        panelManager = GetComponentInParent<PanelManager>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button continueB = root.Q<Button>("continueB");

        if (IsAnySaveFile)
        {
            continueB.clicked += LoadSave;
            continueB.RemoveFromClassList("inactive-menu-b");
            continueB.AddToClassList("menu-b");
        }
    }

    private void LoadSave()
    {
        panelManager.SwitchTo(2);

        FileDataHandler handler = new("Saves", "LastSave");
        GameData gameData = handler.Load();
        StartCoroutine(LoadSceneCoroutine(gameData.CheckpointData.latestScene));
    }

    public bool IsAnySaveFile => directory.GetFiles("LastSave").Length > 0;

    public void NewGame()
    {
        panelManager.SwitchTo(2, true, false); // To load screen

        newGameSave.CreateNewGameSave();
        StartCoroutine(LoadSceneCoroutine(newGameSave.gameData.CheckpointData.latestScene));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSecondsRealtime(panelManager.PanelTweenDuration);

        SceneManager.LoadScene(sceneName);
    }

    private void Start()
    {
        panelManager.SwitchTo(0);
        StaticAudio.Instance.ChangeBackgroundTrack("mainTheme");
    }
}
