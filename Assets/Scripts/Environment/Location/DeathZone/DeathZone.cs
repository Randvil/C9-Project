using UnityEngine;

public class DeathZone : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{		
		if (other.TryGetComponent(out IMortal mortalCreature))
		{
			IDeathManager deathManager = mortalCreature.DeathManager;

            if (deathManager is IForbiddableDeath forbiddable)
			{
				forbiddable.Die(true);
            }
			else
			{
				deathManager.Die();
			}
		}
	}
}
