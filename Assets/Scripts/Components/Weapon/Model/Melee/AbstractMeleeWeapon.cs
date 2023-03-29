using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMeleeWeapon : AbstractWeapon
{
    protected Damage damage;

    public AbstractMeleeWeapon(GameObject weaponOwner, WeaponData weaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(weaponOwner, weaponData, weaponModifierManager, team, turning)
    {
        damage = new Damage(weaponOwner, weaponOwner, weaponData.damageData, weaponModifierManager);
    }
}
