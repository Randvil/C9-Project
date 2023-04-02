using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadMenu : MonoBehaviour
{
    VisualElement slots;

    DirectoryInfo directory = new("Saves");

    const string saveSlotClass = "save-slot";
    const string saveSlotLabelClass = "save-label";

    private bool anyoneSave = false;

    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        slots = root.Q<VisualElement>("saves");
        slots.Clear(); // Delete UI-builder samples

        ShowSaves();
        ActivateLoadButton(root);

        Button newGameB = root.Q<Button>("newGameB");
        newGameB.clicked += NewGame;
    }

    private void ActivateLoadButton(VisualElement root)
    {
        if (anyoneSave)
        {
            Button loadB = root.Q<Button>("loadB");
            loadB.AddToClassList("menu-b");
            loadB.RemoveFromClassList("inactive-menu-b");
        }
    }

    private void ShowSaves()
    {
        foreach (var save in directory.GetFiles())
        {
            anyoneSave = true;

            VisualElement slot = new();
            slot.AddToClassList(saveSlotClass);

            Label name = new(save.Name);
            name.AddToClassList(saveSlotLabelClass);
            slot.Add(name);

            Label date = new(save.LastWriteTime.ToString());
            date.AddToClassList(saveSlotLabelClass);
            slot.Add(date);

            slot.RegisterCallback<ClickEvent>((evt) => LoadSave(save));

            slots.Add(slot);
        }
    }

    private void LoadSave(FileInfo save)
    {
        FileDataHandler handler = new(directory.Name, save.Name);
        GameData gameData = handler.Load();

        SceneManager.LoadScene(gameData.CheckpointData.scene);
    }

    private void NewGame()
    {
        Button newGameB = GetComponent<UIDocument>().rootVisualElement.Q<Button>("newGameB");
        newGameB.clicked += NewGame;
    }
}
