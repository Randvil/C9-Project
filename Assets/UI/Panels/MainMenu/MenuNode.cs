using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuNode
{
    private bool active;
    public bool Active
    {
        get => active;
        set
        {
            if (value == active) return;

            if (value)
                Activate();
            else
                Deactivate();
        }
    }

    readonly Dictionary<Button, MenuNode> children = new();

    const string hiddenClass = "hidden-menu";
    const string activeB = "menu-b-active";

    public VisualElement Panel { get; }

    Button parentButton;
    
    public MenuNode(string name, VisualElement root, bool active)
	{
        Name = name;
        Panel = root.Q<VisualElement>(Name);
        parentButton = root.Q<Button>(Name + "B");

        Deactivate(); //вдруг в ui-билдере что-то включено уже
        Active = active;
    }

    /// <summary>
    /// Имя кнопки активации меню должно быть таким же как и панель + "B"
    /// </summary>
    public void AddChild(MenuNode child)
    {
        Button button = Panel.Q<Button>(child.Name + "B");
        button.clicked += () =>
        {
            foreach (MenuNode child in children.Values)
                if (child != children[button])
                    child.Active = false;

            child.Active = !child.Active;
        };

        child.Panel.SendToBack();

        children.Add(button, child);
    }

    /// <summary>
    /// Use only in property Active or ctor
    /// </summary>
    private void Deactivate()
    {
        foreach (var child in children.Values)
            child.Deactivate();

        parentButton?.RemoveFromClassList(activeB);
        Panel.AddToClassList(hiddenClass);

        active = false;
    }

    /// <summary>
    /// Use only in property Active
    /// </summary>
    private void Activate()
    {
        parentButton?.AddToClassList(activeB);
        Panel.RemoveFromClassList(hiddenClass);

        active = true;
    }

    public string Name { get; }
}
