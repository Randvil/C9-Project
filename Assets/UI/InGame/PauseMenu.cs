using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public PanelManager panelManager;

    private Button continueButton;
    private Button settingsButton;
    private Button exitButton;

    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        continueButton = root.Q<Button>("continueButton");
        settingsButton = root.Q<Button>("settingsButton");
        exitButton = root.Q<Button>("exitButton");

        continueButton.clicked += OnContinue;
        settingsButton.clicked += OnSettings;
        exitButton.clicked += OnExit;
    }

    void OnContinue()
    {
        panelManager.CurrentPanel = panelManager.docs[0].rootVisualElement;
        Time.timeScale = 1f;
    }

    void OnSettings()
    {
        panelManager.CurrentPanel = panelManager.docs[2].rootVisualElement;
    }

    void OnExit()
    {
        //TODO: Serialize (auto-save) or add manual-save system
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
