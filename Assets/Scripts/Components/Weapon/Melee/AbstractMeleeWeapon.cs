using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMeleeWeapon : AbstractWeapon
{
    [SerializeField]
    protected LayerMask enemyLayerMask;    
}
