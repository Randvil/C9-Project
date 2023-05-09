using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class HealthBarUITK : IHealthBarView
{
    protected VisualElement root;

    protected IHealthManager healthManager;
    protected IDeathManager deathManager;

    protected ProgressBar healthBar;

    protected const float tweenDuration = 0.4f;

    public HealthBarUITK(VisualElement root, IHealthManager healthManager, IDeathManager deathManager)
    {
        this.root = root;
        this.healthManager = healthManager;
        this.deathManager = deathManager;


        healthManager.CurrentHealthChangedEvent.AddListener(OnCurrentHealthChange);
        healthManager.MaxHealthChangedEvent.AddListener(OnMaxHealthChange);
        deathManager.DeathEvent.AddListener(OnDie);
    }

    public virtual void OnCurrentHealthChange(Health health)
    {
        if (health.currentHealth < 0f)
            return;

        float partOfHealth = health.currentHealth / health.maxHealth;
        DOTween.To(x => healthBar.value = Mathf.Clamp(x, 0f, healthBar.highValue),
            healthBar.value, partOfHealth, tweenDuration);
    }

    public virtual void OnMaxHealthChange(Health health)
    {
        DOTween.To(x => healthBar.highValue = x, healthBar.highValue, health.maxHealth, tweenDuration);
    }

    protected virtual void OnDie()
    {
        healthManager.CurrentHealthChangedEvent.RemoveListener(OnCurrentHealthChange);
        healthManager.MaxHealthChangedEvent.RemoveListener(OnMaxHealthChange);
        deathManager.DeathEvent.RemoveListener(OnDie);
    }
}