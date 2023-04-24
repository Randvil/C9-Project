using UnityEngine;

public class HintByCollision : Hintable
{
    private ITeamMember team;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent(out team);

        if (team != null && team.CharacterTeam.Team == eTeam.Player)
        {               
            ShowHint.Invoke(LabelName);
        }
    }
}