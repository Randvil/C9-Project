using DG.Tweening;
using NS.RomanLib;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInterface : IPlayerInterface, IHealthBarView
{
    public UIDocument uiDocument;
    private IHealthManager healthManager;
    private IEnergyManager energyManager;

    private ProgressBar healthBar;
    private Label hpLabel;
    private RadialFill energyBar;

    const float tweenDuration = 0.4f;

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
        hpLabel = root.Q<Label>("hpLabel");
        energyBar = root.Q<RadialFill>("energyBar");

        healthBar.value = healthBar.highValue = healthManager.Health.maxHealth;
        hpLabel.text = healthBar.value / healthBar.highValue * 100f + "%";
        energyBar.value = energyManager.Energy.currentEnergy / energyManager.Energy.maxEnergy;
    }

    public void OnCurrentHealthChange(Health health)
    {
        if (health.currentHealth < 0)
            return;

        DOTween.To(x => healthBar.value = Mathf.Clamp(x, 0f, healthBar.highValue),
            healthBar.value, health.currentHealth, tweenDuration);
        hpLabel.text = Mathf.Round(health.currentHealth / healthBar.highValue * 100f) + "%";
    }

    public void OnMaxHealthChange(Health health)
    {
        healthBar.highValue = health.maxHealth;
    }

    private void OnCurrentEnergyChange(Energy energy)
    {
        DOTween.To(x => energyBar.value = (float)System.Math.Round(x, 2),
            energyBar.value, energy.currentEnergy / energy.maxEnergy, tweenDuration);
    }

    private void OnMaxEnergyChange(Energy energy)
    {
        //energyBar.height = energy.maxEnergy; хаха а там такого нету)
    }

    public void ChangeUIDocument(UIDocument newDocument)
    {
        uiDocument = newDocument;
        InitializeUIDocument();
    }
}
