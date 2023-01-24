using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    public UIDocument[] docs;

    private Stack<VisualElement> history;

    private VisualElement currentPanel;
    public VisualElement CurrentPanel
    {
        get => currentPanel;
        set
        {
            if (currentPanel != null)
            {
                currentPanel.style.display = DisplayStyle.None;
                history.Push(currentPanel);
            }                
            currentPanel = value;
            if (currentPanel != null)
                currentPanel.style.display = DisplayStyle.Flex;
        }
    }

    public void GoBack() => CurrentPanel = history.Pop();

    public void SwitchTo(int index) => CurrentPanel = docs[index].rootVisualElement;

    private void Start()
    {
        foreach (var panel in docs)
            panel.rootVisualElement.style.display = DisplayStyle.None;
                
        SwitchTo(0);
    }
}