using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerWeaponViewData", menuName = "Data/Weapon/View/New Player Weapon View Data")]
public class PlayerWeaponViewData : ScriptableObject
{
    public AudioClip takeSword;
    public AudioClip putAwaySword;
    public AudioClip hitEnemy;
    public AudioClip missEnemy;
}
