using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BroodmotherDeathCheck : MonoBehaviour, ICheckEntrance
{
    public bool IsDead = false;
    public UnityEvent EntranceOpenEvent { get; } = new();

    public void ChangeDeathStatus()
    {
        IsDead = true;
        EntranceOpenEvent.Invoke();
    }

    public bool EntranceOpen()
    {
        return IsDead;
    }
}
