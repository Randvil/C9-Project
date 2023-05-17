using UnityEngine;

public class HintNearTrigger : Hintable
{
    private bool showed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITeamMember team) && team.CharacterTeam.Team == eTeam.Player)
        {
            ShowHint.Invoke(LabelName);

            showed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (showed && collision.TryGetComponent(out ITeamMember team) && team.CharacterTeam.Team == eTeam.Player)
        {
            HideHint.Invoke(LabelName);

            showed = false;
        }
    }
}