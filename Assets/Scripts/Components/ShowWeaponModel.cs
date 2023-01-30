using UnityEngine;
using System;
using System.Collections;

public class ShowWeaponModel : MonoBehaviour
{
    public GameObject rightArm;
    public GameObject weaponContainer;
    public GameObject weapon;
    private IWeapon weaponComponent;

    private void Awake()
    {
        weaponComponent = GetComponent<IWeapon>();
    }
    private void Start()
    {
        weaponComponent.StartAttackEvent.AddListener(ShowWeapon);
        weaponComponent.StopAttackEvent.AddListener(HideWeapon);
    }

    private void ShowWeapon()
    {
        weapon.SetActive(true);
        weapon.transform.parent = rightArm.transform;
        weapon.transform.position = rightArm.transform.position;
    }

    private void HideWeapon()
    {
        StartCoroutine(HideWeaponCoroutine());
    }

    private IEnumerator HideWeaponCoroutine()
    {
        yield return new WaitForSeconds(1);
        weapon.SetActive(false);
        weapon.transform.parent = weaponContainer.transform;
    }
}