using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMeleeWeapon : AbstractWeapon
{
    protected Damage[] damages;

    public AbstractMeleeWeapon(MonoBehaviour owner, GameObject weaponOwner, WeaponData weaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(owner, weaponOwner, weaponData, weaponModifierManager, team, turning)
    {
        DamageData[] damageDatas = weaponData.damageDatas;
        damages = new Damage[damageDatas.Length];
        for (int damageDataNumber = 0; damageDataNumber < damageDatas.Length; damageDataNumber++)
        {
            damages[damageDataNumber] = new Damage(weaponOwner, weaponOwner, damageDatas[damageDataNumber], weaponModifierManager);
        }
    }
}
