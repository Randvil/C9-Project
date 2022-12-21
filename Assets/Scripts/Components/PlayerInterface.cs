using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInterface : MonoBehaviour, IUIComponent
{
    public UIDocument uiDocument;

    private ProgressBar healthBar;
    private ProgressBar manaBar;

    private void Awake()
    {
        VisualElement root = uiDocument.rootVisualElement;

        healthBar = root.Q<ProgressBar>("healthBar");
        manaBar = root.Q<ProgressBar>("manaBar");

        healthBar.value = healthBar.highValue = GetComponent<NewDamageInteraction>().maxHealth;
        manaBar.value = manaBar.highValue;
    }

    public void OnDamageTaken(float damage)
    {
        if (healthBar.value > damage)
            healthBar.value -= damage;
        else
            healthBar.value = 0f;
    }
}
