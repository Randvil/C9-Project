using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInterface : IPlayerInterface
{
    public UIDocument uiDocument;
    private IHealthManager healthManager;
    private IEnergyManager energyManager;

    private ProgressBar healthBar;
    private ProgressBar energyBar;

    public PlayerInterface(UIDocument uiDocument, IHealthManager healthManager, IEnergyManager energyManager)
    {
        this.uiDocument = uiDocument;
        this.healthManager = healthManager;
        this.energyManager = energyManager;

        AddEventListeners();
        InitializeUIDocument();
    }

    private void AddEventListeners()
    {
        healthManager.CurrentHealthChangedEvent.AddListener(OnCurrentHealthChange);
        healthManager.MaxHealthChangedEvent.AddListener(OnMaxHealthChange);

        energyManager.CurrentEnergyChangedEvent.AddListener(OnCurrentEnergyChange);
        energyManager.MaxEnergyChangedEvent.AddListener(OnMaxEnergyChange);        
    }

    private void InitializeUIDocument()
    {
        VisualElement root = uiDocument.rootVisualElement;

        healthBar = root.Q<ProgressBar>("healthBar");
        energyBar = root.Q<ProgressBar>("energyBar");

        healthBar.value = healthBar.highValue = healthManager.Health.maxHealth;
        energyBar.value = energyBar.highValue = energyManager.Energy.maxEnergy;
    }

    private void OnCurrentHealthChange(Health health)
    {
        healthBar.value = Mathf.Clamp(health.currentHealth, 0f, healthBar.highValue);
    }

    private void OnMaxHealthChange(Health health)
    {
        healthBar.highValue = health.maxHealth;
    }

    private void OnCurrentEnergyChange(Energy energy)
    {
        energyBar.value = Mathf.Clamp(energy.currentEnergy, 0f, healthBar.highValue);
    }

    private void OnMaxEnergyChange(Energy energy)
    {
        energyBar.highValue = energy.maxEnergy;
    }

    public void ChangeUIDocument(UIDocument newDocument)
    {
        uiDocument = newDocument;
        InitializeUIDocument();
    }
}
