using UnityEngine;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    public UIDocument mainDoc;
    public UIDocument settingsDoc;

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
        settingsDoc.rootVisualElement.style.display = DisplayStyle.None;        
        CurrentPanel = mainDoc.rootVisualElement;
    }
}