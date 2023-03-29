using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class Abilities : MonoBehaviour
{
    private PanelManager panelManager;

    private VisualElement root;

    [SerializeField] private PlayerInput input;

    private void Awake()
    {
        panelManager = GetComponentInParent<PanelManager>();

        root = GetComponent<UIDocument>().rootVisualElement;

        MenuNode main = new("main", root, true);

        MenuNode abilities = new("abilities", root, false);
        main.AddChild(abilities);

        MenuNode collection = new("collection", root, false);
        main.AddChild(collection);

        MenuNode description = new("descriptionCont", root, false);
        abilities.AddChild(description);


        input.onActionTriggered += context =>
        {
            if (context.action.name == "Return")
                ReturnToGame(main);
        };
    }

    private void ReturnToGame(MenuNode main)
    {
        main.DeactivateChildren();
        DOTween.To(t => Time.timeScale = t, 0f, 1f, panelManager.PanelTweenDuration).SetUpdate(true);
        panelManager.GoBack();
        input.SwitchCurrentActionMap("Player");
    }
}
