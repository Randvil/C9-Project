using UnityEngine;

public class DeathZone : MonoBehaviour
{
	private ITeamMember team;
	private void OnTriggerEnter2D(Collider2D other)
	{
		other.TryGetComponent(out team);

		if (team != null && team.CharacterTeam.Team == eTeam.Player)
		{
			other.GetComponent<Player>().HealthManager.ChangeCurrentHealth(-other.GetComponent<Player>().HealthManager.Health.currentHealth);
		}
	}
}
