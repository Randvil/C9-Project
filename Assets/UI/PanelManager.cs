using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class PanelManager : MonoBehaviour
{
    public UIDocument[] docs;

    private Stack<VisualElement> history = new();

    private VisualElement currentPanel;
    public VisualElement CurrentPanel
    {
        get => currentPanel;
        private set
        {
            if (currentPanel != null)
            {
                tweener = DOTween.To(x => currentPanel.style.opacity = x, currentPanel.style.opacity.value, 
                    0f, panelTweenDuration).SetUpdate(true);
                currentPanel.style.display = DisplayStyle.None;
            }

            currentPanel = value;

            if (currentPanel != null)
            {
                currentPanel.style.opacity = 0;
                currentPanel.style.display = DisplayStyle.Flex;
                tweener = DOTween.To(x => currentPanel.style.opacity = x, 0f, 1f, panelTweenDuration).SetUpdate(true);
            }

            
        }
    }

    [SerializeField] private float panelTweenDuration = 0.5f;
    private Tweener tweener;

    public void GoBack() => CurrentPanel = history.Pop();

    public void SwitchTo(int index)
    {
        history.Push(CurrentPanel);
        CurrentPanel = docs[index].rootVisualElement;
    }

    private void Start()
    {
        foreach (var panel in docs)
            panel.rootVisualElement.style.display = DisplayStyle.None;
                
        SwitchTo(0);
    }
}