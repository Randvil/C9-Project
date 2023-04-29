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

        SceneManager.LoadScene(gameData.CheckpointData.scene);
    }

    public bool IsAnySaveFile => directory.GetFiles("LastSave").Length > 0;

    public void NewGame()
    {
        newGameSave.CreateNewGameSave();
        SceneManager.LoadScene(newGameSave.gameData.CheckpointData.scene);
    }
}
