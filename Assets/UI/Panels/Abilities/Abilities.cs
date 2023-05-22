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

        MenuNode effects = new("effects", root, false);
        main.AddChild(effects);
    }

    private void RegisterDescriptions(MenuNode abilities)
    {
        abilityDescriptions = new MenuNode[4];

        for (int i = 1; i <= 4; i++)
        {
            eAbilityType type = (eAbilityType)i;

            MenuNode description = new(type + "Description", root, false);

            abilityDescriptions[i - 1] = description;
            description.ParentButton.RegisterCallback<MouseEnterEvent>(ZIndexFix);

            Button learnB = description.Panel.Q<Button>("learn" + type + "B");
            learnB.clicked += () =>
            {
                ToggleAbilityLearning(type);
                ToggleLearnButton(learnB);
            };

            if (IsAbilityLearned(type))
                ToggleLearnButton(learnB);
            
            var icon = description.ParentButton.Q<VisualElement>("icon");
            icon.style.display = DisplayStyle.None;
            panelManager.Abilities.AbilityLearnEvent.AddListener(learnedType =>
            {
                if (icon.style.display == DisplayStyle.None && learnedType == type)
                {
                    ActivateSetPossibility(abilities, description, icon);
                    ToggleLearnButton(learnB);
                }
            });

            if (IsAbilityLearned(type)) // изучение при загрузке происходит раньше чем подпись на события
                ActivateSetPossibility(abilities, description, icon);
        }

        //special govnocode for parry
        MenuNode parryDescription = new("ParryDescription", root, false);
        parryDescription.ParentButton.RegisterCallback<MouseEnterEvent>(ZIndexFix);
        abilities.AddChild(parryDescription);
    }

    private void ActivateSetPossibility(MenuNode abilities, MenuNode description, VisualElement icon)
    {
        icon.style.display = DisplayStyle.Flex;
        abilities.AddChild(description);
    }

    private void ToggleLearnButton(Button button)
    {
        button.ToggleInClassList("inactive-menu-b");
        button.text = button.text == "SET" ? "UNSET" : "SET";
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
        panelManager.SwitchTo(0);
        input.SwitchCurrentActionMap("Player");
        StaticAudio.Instance.SnapshotName = "InGame";
    }
}
