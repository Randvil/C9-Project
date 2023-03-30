using UnityEngine;

public class DeathZone : MonoBehaviour
{
	private ITeam team;
	private void OnTriggerEnter2D(Collider2D other)
	{
		other.TryGetComponent(out team);

		if (team != null && team.Team == eTeam.Player)
		{
			other.GetComponent<Player>().HealthManager.ChangeCurrentHealth(-other.GetComponent<Player>().HealthManager.Health.currentHealth);
		}
	}
}
