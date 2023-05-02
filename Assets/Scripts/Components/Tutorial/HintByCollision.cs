using UnityEngine;

public class HintByCollision : Hintable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ITeamMember team) && team.CharacterTeam.Team == eTeam.Player)
        {               
            ShowHint.Invoke(LabelName);
        }
    }
}