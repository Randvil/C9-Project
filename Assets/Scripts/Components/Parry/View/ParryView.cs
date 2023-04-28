using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryView
{
    private GameObject weaponObject;
    private Transform weaponContainer;
    private Transform rightHand;

    private Animator animator;
    private IParry parry;

    public ParryView(GameObject weaponObject, Transform weaponContainer, Transform rightHand, IParry parry, Animator animator)
    {
        this.weaponObject = weaponObject;
        this.weaponContainer = weaponContainer;
        this.rightHand = rightHand;

        this.animator = animator;
        this.parry = parry;

        parry.StartParryEvent.AddListener(OnStartParry);
        parry.BreakParryEvent.AddListener(OnBreakParry);
        parry.SuccessfulParryEvent.AddListener(OnSuccessfulParry);
    }

    public void OnStartParry()
    {
        weaponObject.SetActive(true);
        weaponObject.transform.parent = rightHand;
        weaponObject.transform.position = rightHand.position;
        weaponObject.transform.localRotation = Quaternion.identity;

        animator.SetBool("IsParrying", true);
    }

    public void OnBreakParry()
    {
        weaponObject.SetActive(false);
        weaponObject.transform.parent = weaponContainer;

        animator.SetBool("IsParrying", false);
    }

    public void OnSuccessfulParry()
    {
        animator.SetTrigger("ParryTrigger");
    }

}
