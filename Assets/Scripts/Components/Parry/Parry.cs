using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Parry : MonoBehaviour, IParry
{
    [SerializeField]
    protected Damage parryDamage;
    public Damage ParryDamage { get => parryDamage; }

    [SerializeField]
    protected float parryRadius;
    public float ParryRadius { get => parryRadius; }

    [SerializeField]
    protected float parryDelay;
    public float ParryDelay { get => parryDelay; }

    public UnityEvent StartParryEvent { get; } = new();
    public UnityEvent StopParryEvent { get; } = new();
    public UnityEvent<IDamageReduced> WeaponWasParriedEvent { get; } = new();

    public bool isParrying;
    public bool IsParrying { get => isParrying; }

    public void StartParry(Vector3 direction)
    {
        StartCoroutine(ParryingCoroutine(direction));
    }

    public void StopParry(Vector3 direction)
    {
        StopCoroutine(ParryingCoroutine(direction));
    }

    //events can be used for animation
    public IEnumerator ParryingCoroutine(Vector3 direction)
    { 
        StartParryEvent.Invoke();
        isParrying = true;
        ApplyParry(direction);
        yield return new WaitForSeconds(parryDelay);
        isParrying = false;
        StopParryEvent.Invoke();
    }

    protected void ApplyParry(Vector3 direction)
    {
        Collider2D[] objectsNear = Physics2D.OverlapCircleAll(transform.position, parryRadius);

        if (objectsNear.Length == 0)
            return;

        foreach (Collider2D obj in objectsNear){

            if(obj.gameObject.GetComponent<IDamageReduced>() != null && obj.gameObject.name != "Player")
            {
                if ((direction == Vector3.right && obj.transform.position.x >= transform.position.x) || 
                    (direction == Vector3.left && obj.transform.position.x <= transform.position.x))
                {
                    WeaponWasParriedEvent.Invoke(obj.gameObject.GetComponent<IDamageReduced>());
                }
            }
        }
    }
}
