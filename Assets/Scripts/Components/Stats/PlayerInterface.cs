using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInterface : MonoBehaviour, IUIComponent
{
    public UIDocument uiDocument;

    private ProgressBar healthBar;
    private ProgressBar manaBar;

    private IStats stats;

    private void Start()
    {
        VisualElement root = uiDocument.rootVisualElement;

        stats = GetComponent<Stats>();
        stats.ChangeStatEvent.AddListener(OnHealthChange);

        healthBar = root.Q<ProgressBar>("healthBar");
        manaBar = root.Q<ProgressBar>("manaBar");

        healthBar.value = healthBar.highValue = stats.GetStat(eStatType.MaxHealth);
        manaBar.value = manaBar.highValue;
    }

    public void OnHealthChange(eStatType stat, float value)
    {
        if (stat == eStatType.CurrentHealth)
            healthBar.value = Mathf.Clamp(value, 0f, healthBar.highValue);
    }
}
