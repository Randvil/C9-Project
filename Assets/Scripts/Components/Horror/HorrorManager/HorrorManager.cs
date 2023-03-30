using UnityEngine;
using UnityEngine.Events;

public class HorrorManager : IHorrorManager
{
    private Horror horror;
    public Horror Horror => horror;

    public UnityEvent<Horror> MaxHorrorChangedEvent { get; } = new();
    public UnityEvent<Horror> CurrentHorrorChangedEvent { get; } = new();

    public HorrorManager(HorrorManagerData horrorManagerData)
    {
        horror = horrorManagerData.initialHorror;
    }

    public void ChangeMaxHorror(float value)
    {
        horror.maxHorror = Mathf.Clamp(horror.maxHorror + value, 0f, float.MaxValue);
        MaxHorrorChangedEvent.Invoke(horror);

        ChangeCurrentHorror(0f);
    }

    public void ChangeCurrentHorror(float value)
    {
        horror.currentHorror = Mathf.Clamp(horror.currentHorror + value, 0f, horror.maxHorror);
        CurrentHorrorChangedEvent.Invoke(horror);
    }
}
