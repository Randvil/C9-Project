using UnityEngine;

public class AudioEffectByTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private bool disableAfterPlaying = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITeamMember team) && team.CharacterTeam.Team == eTeam.Player)
        {
            StaticAudio.Instance.PlayLocationClip(soundEffect);

            if (disableAfterPlaying)
            {
                gameObject.SetActive(false);
            }
        }
    }
}