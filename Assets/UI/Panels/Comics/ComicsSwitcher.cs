using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ComicsSwitcher : MonoBehaviour
{
    public List<Texture2D> pages;
    public List<string> phrases;

    private int index = 0;

    private VisualElement root;

    private VisualElement page;
    private Label label;
    private VisualElement panel;

    private Button skipButton;

    [SerializeField] private PanelManager parentPanelManager;

    [SerializeField] private PanelManager pageManager;

    [SerializeField] private LoadMenu loadMenu;

    const string labelClass = "comics-label";
    const string pageClass = "comics-page";
    const string panelClass = "comics-panel";
    const string animatedPageClass = "animated-comics-page";

    public bool StartNGAfterComics { get; set; } 

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        skipButton = root.Q<Button>("skipB");
        skipButton.clicked += EndOfComics;
    }

    private void CreateNewPage()
    {
        if (panel != null)
            root.Remove(panel);

        panel = new();
        panel.AddToClassList(panelClass);

        page = new();
        page.AddToClassList(pageClass);
        page.name = "page";
        page.style.backgroundImage = pages[index];
        panel.Add(page);

        label = new();
        label.AddToClassList(labelClass);
        label.text = phrases[index];
        panel.Add(label);

        pageManager.AddPanel(panel);
        root.Add(panel);

        panel.RegisterCallback<ClickEvent>(e => ToNextPage());
    }

    public void ToNextPage()
    {
        if (index > 0)
            pageManager.panels[index - 1].Q<VisualElement>("page").RemoveFromClassList(animatedPageClass);

        if (index >= pages.Count)
        {
            EndOfComics();
            return;
        }

        CreateNewPage();
        pageManager.SwitchTo(index);
        StartCoroutine(AnimateAfterTween());

        skipButton.BringToFront();

        index++;
    }

    private IEnumerator AnimateAfterTween()
    {
        yield return new WaitForSeconds(pageManager.PanelTweenDuration);
        page.AddToClassList(animatedPageClass);
    }

    private void EndOfComics()
    {
        if (!StartNGAfterComics)
        {
            index = 0;
            pageManager.panels.Clear();

            parentPanelManager.GoBack();
        }
        else
            loadMenu.NewGame();
    }
}
