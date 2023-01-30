using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour, IInteractive
{
    private new Collider2D collider;
    private ITeam team;

    public Collider2D GetCollider2D { get => collider; }

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent(out team);

        if (team != null && team.Team == eTeam.Player)
        {
            Debug.Log("You can interact");
        }
    }
}
