using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteWithAnimationVisualizer : MonoBehaviour
{
    [SerializeField]
    protected GameObject weaponGameObject;

    private IWeapon weapon;

    private void Start()
    {
        weapon = GetComponent<IWeapon>();

        weapon.StartAttackEvent.AddListener(OnStartAttack);
        weapon.StopAttackEvent.AddListener(OnStopAttack);
    }

    private void OnStartAttack()
    {
        weaponGameObject.SetActive(true);
    }

    private void OnStopAttack()
    {
        weaponGameObject.SetActive(false);
    }
}
