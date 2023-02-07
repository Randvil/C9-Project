using UnityEngine;
using System;
using System.Collections;

public class ShowWeaponModel : MonoBehaviour
{
    public GameObject rightArm;
    public GameObject weaponContainer;
    public GameObject weapon;
    public GameObject weaponHandler;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") || animator.GetCurrentAnimatorStateInfo(0).IsName("Parry"))
        {
            ShowWeapon();
        }
        else HideWeapon();
        
    }

    private void ShowWeapon()
    {
        weapon.SetActive(true);
        weapon.transform.parent = rightArm.transform;
        weaponHandler.transform.position = rightArm.transform.position;
        weaponHandler.transform.rotation = rightArm.transform.rotation;
    }

    private void HideWeapon()
    {
        weapon.SetActive(false);
        weapon.transform.parent = weaponContainer.transform;
    }
}