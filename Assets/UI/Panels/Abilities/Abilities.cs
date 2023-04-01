using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Abilities : MonoBehaviour, IPanel
{
    private PanelManager panelManager;

    private VisualElement root;

    private PlayerInput input;

    private MenuNode main;

    private void Awake()
    {
        panelManager = GetComponentInParent<PanelManager>();

        root = GetComponent<UIDocument>().rootVisualElement;

        main = new("main", root, true);

        MenuNode abilities = new("abilities", root, false);
        main.AddChild(abilities);

        MenuNode collection = new("collection", root, false);
        main.AddChild(collection);

        MenuNode description = new("descriptionCont", root, false);
        abilities.AddChild(description);

        RegisterClicks(abilities.Panel);
    }

    private void RegisterClicks(VisualElement abilities)
    {
        VisualElement p1 = abilities.Q<VisualElement>("1");
        VisualElement p2 = abilities.Q<VisualElement>("2");
        VisualElement p3 = abilities.Q<VisualElement>("3");

        p1.RegisterCallback<ClickEvent>(e => panelManager.Abilities.LearnAbility(eAbilityType.Kanabo));
        p2.RegisterCallback<ClickEvent>(e => panelManager.Abilities.LearnAbility(eAbilityType.Tessen));
        p3.RegisterCallback<ClickEvent>(e => panelManager.Abilities.LearnAbility(eAbilityType.Daikyu));
    }

    public void SetInput(PlayerInput _input)
    {
        input = _input;
        input.onActionTriggered += context =>
        {
            if (context.action.name == "Return")
                ReturnToGame();
        };
    }

    private void ReturnToGame()
    {
        main.DeactivateChildren();
        DOTween.To(t => Time.timeScale = t, 0f, 1f, panelManager.PanelTweenDuration).SetUpdate(true);
        panelManager.GoBack();
        input.SwitchCurrentActionMap("Player");
    }
}
