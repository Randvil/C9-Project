using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : IHealthBarView
{
    private Slider healthBarSlider;

    private IHealthManager healthManager;
    private IDeathManager deathManager;

    public HealthBarView(Slider healthBarSlider, IHealthManager healthManager, IDeathManager deathManager)
    {
        this.healthBarSlider = healthBarSlider;
        this.healthManager = healthManager;
        this.deathManager = deathManager;

        healthManager.CurrentHealthChangedEvent.AddListener(OnCurrentHealthChange);
        healthManager.MaxHealthChangedEvent.AddListener(OnMaxHealthChange);
        deathManager.DeathEvent.AddListener(OnDie);

        healthBarSlider.minValue = 0f;
        healthBarSlider.maxValue = healthManager.Health.maxHealth;
        healthBarSlider.value = healthManager.Health.currentHealth;
    }

    public void OnCurrentHealthChange(Health health)
    {
        if (health.currentHealth == health.maxHealth)
        {
            //healthBarSlider.gameObject.SetActive(false);
        }
        else
        {
            healthBarSlider.gameObject.SetActive(true);
        }

        healthBarSlider.value = health.currentHealth;
    }

    public void OnMaxHealthChange(Health health)
    {
        healthBarSlider.maxValue = health.maxHealth;
    }

    private void OnDie()
    {
        healthBarSlider.gameObject.SetActive(false);

        healthManager.CurrentHealthChangedEvent.RemoveListener(OnCurrentHealthChange);
        healthManager.MaxHealthChangedEvent.RemoveListener(OnMaxHealthChange);
        deathManager.DeathEvent.RemoveListener(OnDie);
    }
}
