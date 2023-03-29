using UnityEngine;
using UnityEngine.Events;

public class HintByCollision : MonoBehaviour, IHintable
{
    private ITeam team;

    public UnityEvent ShowHint { get; } = new();

    public string HintType { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent(out team);

        if (team != null && team.Team == eTeam.Player)
        {
            ShowHint.Invoke();
        }
    }
}