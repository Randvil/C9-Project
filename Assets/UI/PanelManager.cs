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

    private readonly Stack<VisualElement> history = new();

    private bool needToTweenNext = true;
    private bool needToTweenPrev = true;

    public bool Blocked { get; set; } = false;

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
                if (needToTweenPrev)
                    DOTween.To(x => lastPanel.style.opacity = x, 1f, 0f, PanelTweenDuration).SetUpdate(true);

                StartCoroutine(DisplayDisableTween(lastPanel));
            }

            if (currentPanel != null)
            {
                if (currentPanel.style.opacity.value > 0.001f) // Если игрок слишком быстро переключает панели (балуется)
                    DisplayDisableImmediate(lastPanel);

                if (needToTweenNext)
                    currentPanel.style.opacity = 0f;
                currentPanel.style.display = DisplayStyle.Flex;

                if (needToTweenNext)
                    DOTween.To(x => currentPanel.style.opacity = x, 0f, 1f, PanelTweenDuration).SetUpdate(true);
                else
                    currentPanel.style.opacity = 1f;
            }
        }
    }

    [SerializeField] private float panelTweenDuration = 0.5f;
    public float PanelTweenDuration => panelTweenDuration;

    private PlayerInput _input;
    public PlayerInput Input
    {
        get => _input;
        set
        {
            _input = value;
            foreach (var doc in docs)
                if (Input != null)
                    doc.GetComponent<IPanel>().SetInput(Input);
        }
    }

    public IAbilityManager Abilities { get; set; }

    public void GoBack()
    {
        if (history.Count > 0 && history.Peek() != CurrentPanel)
            CurrentPanel = history.Pop();
    }

    public void SwitchTo(int index, bool tweenNext, bool tweenPrev)
    {
        needToTweenNext = tweenNext;
        needToTweenPrev = tweenPrev;

        SwitchTo(index);

        needToTweenNext = true;
        needToTweenPrev = true;
    }

    public void SwitchTo(int index)
    {
        SwitchTo(panels[index]);
    }

    public void SwitchTo(VisualElement panel)
    {
        if (Blocked || panel == CurrentPanel)
            return;

        if (CurrentPanel != null && CurrentPanel.name.Contains("Death") && !panel.name.Contains("Load"))
            return;
        
        if (CurrentPanel != null)
            history.Push(CurrentPanel);
        CurrentPanel = panel;
    }

    public void AddPanel(VisualElement panel)
    {
        panels.Add(panel);
        DisablePanel(panel);
    }

    private void Awake()
    {
        foreach (var doc in docs)
        {
            panels.Add(doc.rootVisualElement);
            DisablePanel(doc.rootVisualElement);
        }

        //if (docs.Length > 0)
        //    SwitchTo(0);
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

    private void DisplayDisableImmediate(VisualElement panelToDisable)
    {
        StopAllCoroutines();
        if (panelToDisable != null)
            DisablePanel(panelToDisable);
    }
}