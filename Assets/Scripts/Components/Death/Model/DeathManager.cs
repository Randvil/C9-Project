using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathManager : IDeathManager, IForbiddableDeath
{
    private List<object> forbiddingObjects = new List<object>();

    private IHealthManager healthManager;

    public bool IsAlive { get; private set; } = true;
    public bool IsForbidden => forbiddingObjects.Count > 0;

    public UnityEvent DeathEvent { get; } = new();
    public UnityEvent ResurrectionEvent { get; } = new();
    public UnityEvent PreventedDeathEvent { get; } = new();

    public DeathManager(IHealthManager healthManager)
    {
        this.healthManager = healthManager;

        healthManager.CurrentHealthChangedEvent.AddListener(OnHealthChange);
    }

    private void OnHealthChange(Health health)
    {
        if (health.currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        if (IsAlive == false)
        {
            return;
        }

        if (IsForbidden == true)
        {
            PreventedDeathEvent.Invoke();
            return;
        }

        IsAlive = false;
        DeathEvent.Invoke();
    }

    public void Resurrect()
    {
        IsAlive = true;
        ResurrectionEvent.Invoke();
    }

    public void ForbidDying(object forbiddingObject)
    {
        forbiddingObjects.Add(forbiddingObject);
    }

    public void AllowDying(object forbiddingObject)
    {
        forbiddingObjects.Remove(forbiddingObject);

        OnHealthChange(healthManager.Health);
    }

    public void Die(bool ignoreForbidding)
    {
        if (ignoreForbidding == false)
        {
            Die();
            return;
        }

        IsAlive = false;
        DeathEvent.Invoke();
    }
}
