using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathZone : MonoBehaviour
{
    private ITeam team;
    public UnityEvent enterDeathZoneEvent = new();

    public void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent(out team);

        if (team != null && team.Team == eTeam.Player)
        {
            enterDeathZoneEvent.Invoke();
        }
    }
}
