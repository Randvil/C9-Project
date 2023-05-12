using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

class BroodmotherHealthBar : HealthBarUITK
{
    private readonly Label healthLabel;

    public BroodmotherHealthBar(VisualElement root, IHealthManager healthManager, IDeathManager deathManager, string name)
        : base(root, healthManager, deathManager)
    {
        this.root = root.Q<VisualElement>("bossStats");
        healthBar = root.Q<ProgressBar>(name);
        healthLabel = root.Q<Label>(name + "Label");

        healthBar.value = healthManager.Health.currentHealth / healthManager.Health.maxHealth;

        this.root.style.display = DisplayStyle.Flex;
    }

    public override void OnCurrentHealthChange(Health health)
    {
        base.OnCurrentHealthChange(health);

        float partOfHealth = health.currentHealth / health.maxHealth;

        if (healthLabel != null)
            healthLabel.text = Mathf.Round(partOfHealth * 100f) + "%";
    }

    protected override void OnDie()
    {
        base.OnDie();
        DOTween.To(x => root.style.opacity = x, 1f, 0f, 1f);
    }
}