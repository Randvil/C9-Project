using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Collections;
using UnityEngine.InputSystem;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private UIDocument[] docs;

    public readonly List<VisualElement> panels = new();

    private Stack<VisualElement> history = new();

    private VisualElement lastPanel;

    private VisualElement currentPanel;
    public VisualElement CurrentPanel
    {
        get => currentPanel;

        private set
        {        
            lastPanel = currentPanel;
            currentPanel = value;           

            if (lastPanel != null)
            {
                DOTween.To(x => lastPanel.style.opacity = x, 1f, 0f, PanelTweenDuration).SetUpdate(true);
                StartCoroutine(DisplayDisableTween(lastPanel));  
            }

            if (currentPanel != null)
            {
                if (currentPanel.style.opacity != 0f) // Если игрок слишком быстро переключает панели (балуется)
                    StopDisplayDisabling();

                currentPanel.style.display = DisplayStyle.Flex;
                DOTween.To(x => currentPanel.style.opacity = x, 0f, 1f, PanelTweenDuration).SetUpdate(true);
            }      
        }
    }

    [SerializeField] private float panelTweenDuration = 0.5f;
    public float PanelTweenDuration => panelTweenDuration;

    public PlayerInput Input { get; set; }

    public void GoBack()
    {
        if (history.Count > 0 && history.Peek() != CurrentPanel)
            CurrentPanel = history.Pop();
    }

    public void SwitchTo(int index)
    {
        SwitchTo(panels[index]);
    }

    public void SwitchTo(VisualElement panel)
    {
        if (panel == CurrentPanel)
            return;

        if (CurrentPanel != null)
            history.Push(CurrentPanel);
        CurrentPanel = panel;
    }

    public void AddPanel(VisualElement panel)
    {
        panels.Add(panel);
        DisablePanel(panel);
        CurrentPanel ??= panel;
    }

    private void Awake()
    {
        //Remove then load system is finished!
        Input = FindObjectOfType<PlayerInput>(); //~0.001s

        foreach (var doc in docs)
        {
            if (Input != null)
                doc.GetComponent<IPanel>().SetInput(Input);
            panels.Add(doc.rootVisualElement);
            DisablePanel(doc.rootVisualElement);
        }   

        if (docs.Length > 0) // Если были задействованы ui-доки из инспектора
            SwitchTo(0);
    }

    private void DisablePanel(VisualElement panel)
    {
        panel.style.display = DisplayStyle.None;
    }

    private IEnumerator DisplayDisableTween(VisualElement panelToDisable)
    {
        yield return new WaitForSecondsRealtime(PanelTweenDuration);
        DisablePanel(panelToDisable);
    }

    private void StopDisplayDisabling()
    {
        StopAllCoroutines();
    }
}