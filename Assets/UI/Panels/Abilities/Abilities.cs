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
