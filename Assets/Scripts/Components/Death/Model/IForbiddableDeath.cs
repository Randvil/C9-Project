using UnityEngine.Events;

public interface IForbiddableDeath
{
    public bool IsForbidden { get; }
    public void ForbidDying(object forbiddingObject);
    public void AllowDying(object forbiddingObject);
    public void Die(bool ignoreForbidding);
    public UnityEvent PreventedDeathEvent { get; }
}
