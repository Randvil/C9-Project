using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    private VisualElement root;

    private PanelManager panelManager;

    private const string hiddenClass = "hidden-menu";
    private const string activeB = "menu-b-active";

    private void OnEnable()
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

        root.RegisterCallback<ChangeEvent<DisplayStyle>>((evt) => {; });

        main.Panel.Q<Button>("continueB").clicked += () =>
        {
            main.Active = false;
            Time.timeScale = 1f;
            panelManager.GoBack();
        };
    }
}
