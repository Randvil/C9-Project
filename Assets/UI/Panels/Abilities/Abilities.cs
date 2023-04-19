using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Abilities : MonoBehaviour, IPanel
{
    private PanelManager panelManager;

    private VisualElement root;

    private PlayerInput input;

    private MenuNode main;

    private MenuNode[] abilityDescriptions;

    private void Start()
    {
        panelManager = GetComponentInParent<PanelManager>();

        root = GetComponent<UIDocument>().rootVisualElement;

        main = new("main", root, true);

        MenuNode abilities = new("abilities", root, true);
        main.AddChild(abilities);

        RegisterDescriptions(abilities);
    }

    private void RegisterDescriptions(MenuNode abilities)
    {
        abilityDescriptions = new MenuNode[3];

        for (int i = 1; i <= 3; i++)
        {
            eAbilityType type = (eAbilityType)i;

            MenuNode description = new(type + "Description", root, false);

            abilities.AddChild(description);
            abilityDescriptions[i - 1] = description;
            description.ParentButton.RegisterCallback<MouseEnterEvent>(ZIndexFix);

            Button learnB = description.Panel.Q<Button>("learn" + type + "B");

            if (IsAbilityLearned(type))
                ToggleLearnButton(learnB);
            else
                learnB.clicked += () =>
                {
                    ToggleAbilityLearning(type);
                    ToggleLearnButton(learnB);
                };
        }
    }

    private void ToggleLearnButton(Button button)
    {
        button.ToggleInClassList("inactive-menu-b");
        button.text = button.text == "LEARN" ? "LEARNED" : "LEARN";
    }

    private void ToggleAbilityLearning(eAbilityType type)
    {
        if (IsAbilityLearned(type))
            panelManager.Abilities.ForgetAbility(type);
        else
            panelManager.Abilities.LearnAbility(type);
    }

    // Just a wrapper
    private bool IsAbilityLearned(eAbilityType type) =>
        panelManager.Abilities.LearnedAbilities.ContainsValue(panelManager.Abilities.GetAbilityByType(type));

    private void ZIndexFix(MouseEnterEvent runEvent)
    {
        VisualElement target = (VisualElement)runEvent.target;
        target.BringToFront();
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
