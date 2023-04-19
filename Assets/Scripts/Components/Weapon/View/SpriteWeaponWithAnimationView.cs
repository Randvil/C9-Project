using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteWeaponWithAnimationView : IWeaponView
{
    private GameObject weaponGameObject;

    public SpriteWeaponWithAnimationView(GameObject weaponGameObject)
    {
        this.weaponGameObject = weaponGameObject;
    }

    public void StartAttack()
    {
        weaponGameObject.SetActive(true);
    }

    public void BreakAttack()
    {
        weaponGameObject.SetActive(false);
    }

    public void ReleaseAttack()
    {
        Debug.Log("Weapon have done something");
    }
}
