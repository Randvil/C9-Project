using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBombData", menuName = "Data/Projectiles/Bomb/New Bomb Data")]
public class BombData : ProjectileData
{
    [Min(0f)]
    public float explosionRadius = 2f;
}
