using UnityEngine;

public class HintByCollision : Hintable
{
    private ITeam team;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent(out team);

        if (team != null && team.Team == eTeam.Player)
        {               
            ShowHint.Invoke(LabelName);
        }
    }
}