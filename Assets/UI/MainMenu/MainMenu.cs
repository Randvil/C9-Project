using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private VisualElement root;

    public PanelManager panelManager;

    private Button startButton;
    private Button quitButton;
    private Button settingsButton;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        panelManager = panelManager.GetComponent<PanelManager>();

        startButton = root.Q<Button>("startButton");
        quitButton = root.Q<Button>("quitButton");
        settingsButton = root.Q<Button>("settingsButton");


        Debug.Log(startButton.clickable);

        startButton.clicked += OnStart;
        settingsButton.clicked += OnSettings;
        quitButton.clicked += OnQuit;
    }

    void OnStart()
    {
        Debug.Log("2324");
        panelManager.CurrentPanel = null;
        SceneManager.LoadScene(1);
    }

    void OnSettings()
    {
        
        panelManager.CurrentPanel = panelManager.settingsDoc.rootVisualElement;
    }

    void OnQuit()
    {
        Application.Quit();
    }
}
