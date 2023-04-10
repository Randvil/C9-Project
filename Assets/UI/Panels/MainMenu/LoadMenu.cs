using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadMenu : MonoBehaviour
{
    VisualElement slots;

    DirectoryInfo directory = new("Saves");

    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button newGameB = root.Q<Button>("newGameB");
        newGameB.clicked += NewGame;

        Button continueB = root.Q<Button>("continueB");
        continueB.clicked += ContinueGame;
    }

    private void LoadSave(FileInfo save)
    {
        FileDataHandler handler = new(directory.Name, save.Name);
        GameData gameData = handler.Load();

        SceneManager.LoadScene(gameData.CheckpointData.scene);
    }

    private void ContinueGame()
    {

    }

    private void NewGame()
    {
        Button newGameB = GetComponent<UIDocument>().rootVisualElement.Q<Button>("newGameB");
        newGameB.clicked += NewGame;
    }
}
