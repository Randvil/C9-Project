using UnityEngine;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    public UIDocument[] docs;
    public UIDocument firstDoc;

    private VisualElement currentMenu;
    public VisualElement CurrentPanel
    {
        get => currentMenu;
        set
        {
            if (currentMenu != null)
                currentMenu.style.display = DisplayStyle.None;
            currentMenu = value;
            if (currentMenu != null)
                currentMenu.style.display = DisplayStyle.Flex;
        }
    }

    private void Start()
    {
        foreach (var panel in docs)
            panel.rootVisualElement.style.display = DisplayStyle.None;
                
        CurrentPanel = docs[0].rootVisualElement;
    }
}