using UnityEngine;

public class PlayerAudio : AudioComponent
{
    [SerializeField] private AudioSource parrySound;
    IParry parry;

    protected override void Awake()
    {
        base.Awake();

        parry = GetComponent<IParry>();
        parry.StartParryEvent.AddListener(parrySound.Play);
    }
}