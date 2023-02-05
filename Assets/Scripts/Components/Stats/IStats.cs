using UnityEngine.Events;

public interface IStats
{
    public float GetStat(eStatType statType);
    public void SetStat(eStatType statType, float value);
    public UnityEvent<eStatType, float> ChangeStatEvent { get; }
}