using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private const string choosenClass = "choosen-element";

    private VisualElement root;

    [SerializeField] private PlayerInput input;

    Button choosenButton;

    private void Awake()
    {
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

        main.Panel.Q<Button>("quitB").clicked += Application.Quit;

        SetInput();
    }

    private void SetInput()
    {
        input.onActionTriggered += context =>
        {
            switch (context.action.name)
            {
                case "Navigate":
                    NavigateCommand(context.action.ReadValue<Vector2>());
                    break;
            }
        };
    }

    private void ChooseNewButton(Button button)
    {
        choosenButton.RemoveFromClassList(choosenClass);
        button.AddToClassList(choosenClass);
        choosenButton = button;
    }

    private void NavigateCommand(Vector2 vector)
    {
        if (vector.magnitude == 0f)
            return;

        
    }

    private void Start()
    {
        StaticAudio.Instance.ChangeBackgroundTrack("mainTheme");
    }
}
