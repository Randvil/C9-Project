using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon View Data", menuName = "Component Data/View/New Weapon View Data", order = 112)]
public class PlayerWeaponViewData : ScriptableObject
{
    public AudioClip takeSword;
    public AudioClip putAwaySword;
    public AudioClip hitEnemy;
    public AudioClip missEnemy;
}
