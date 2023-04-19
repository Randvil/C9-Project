using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    //[SerializeField] private AudioSource attackSound;
    //[SerializeField] private AudioSource hurtSound;
    //[SerializeField] private AudioSource walkSound;

    //IDamageHandler damageHandler;
    //IMovement movement;
    //IWeapon weapon;

    //protected virtual void Awake()
    //{
    //    damageHandler = GetComponent<IDamageHandler>();
    //    damageHandler.TakeDamageEvent.AddListener((_, _) => HurtSound());

    //    weapon = GetComponent<IWeapon>();
    //    weapon.StartAttackEvent.AddListener(SwordSwingSound);

    //    movement = GetComponent<IMovement>();
    //    movement.EntityMoveEvent.AddListener(isMoving => { if (isMoving) MoveSound(); });
    //}

    //void HurtSound()
    //{
    //    hurtSound.pitch = Random.Range(0.8f, 1.2f);
    //    hurtSound.Play();
    //}

    //void MoveSound()
    //{
    //    // TODO: сделать получше и покрасивее, связать по скорости с анимацией,
    //    // добавить смену звука в зависимости от типа поверхености
    //    if (!walkSound.isPlaying)
    //    {
    //        walkSound.pitch = Random.Range(0.8f, 1.2f);
    //        walkSound.Play();
    //    }
    //}

    //void SwordSwingSound()
    //{
    //    attackSound.Play();
    //    attackSound.pitch = Random.Range(0.8f, 1.2f);
    //}
}