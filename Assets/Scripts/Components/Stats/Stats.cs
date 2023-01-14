using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour, IStats
{
    [SerializeField]
    private float initialHealth;

    private Dictionary<eStatType, float> stats = new();

    public UnityEvent<eStatType, float> ChangeStatEvent { get; } = new();

    private void Awake()
    {
        stats.Add(eStatType.MaxHealth, initialHealth);
        stats.Add(eStatType.CurrentHealth, initialHealth);
    }

    public float GetStat(eStatType statType)
    {
        return stats.GetValueOrDefault(statType, 0f);
    }

    public void SetStat(eStatType statType, float value)
    {
        stats[statType] = value;

        ChangeStatEvent.Invoke(statType, value);
    }
}
