using System.Collections;
using UnityEngine;

public class HintWithLifeTime : Hintable
{
    [field: SerializeField] public float LifeTime { get; private set; } = 4f;

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ITeamMember team) && team.CharacterTeam.Team == eTeam.Player)
        {               
            ShowHint.Invoke(LabelName);

            yield return new WaitForSeconds(LifeTime);

            HideHint.Invoke(LabelName);
        }
    }
}