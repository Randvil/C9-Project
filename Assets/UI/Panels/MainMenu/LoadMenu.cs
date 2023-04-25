using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadMenu : MonoBehaviour
{
    VisualElement slots;

    DirectoryInfo directory = new("Saves");

    [SerializeField] private NewGameSave newGameSave;

    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button newGameB = root.Q<Button>("newGameB");
        newGameB.clicked += NewGame;

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
        FileDataHandler handler = new("Saves", "LastSave");
        GameData gameData = handler.Load();

        SceneManager.LoadScene(gameData.CheckpointData.latestScene);
    }

    private bool IsAnySaveFile
    {
        get
        {
            return directory.GetFiles("LastSave").Length > 0;
        }
    }

    private void NewGame()
    {
        newGameSave.CreateNewGameSave();
        SceneManager.LoadScene(newGameSave.gameData.CheckpointData.latestScene);
    }
}
