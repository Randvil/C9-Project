using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileData", menuName = "Data/Projectiles/New Projectile Data")]
public class ProjectileData : ScriptableObject
{
    [Min(0f)]
    public float speed = 5f;
    [Range(-180f, 180f), Tooltip("Determines the direction of movement [degrees]")]
    public float zRotation = 0f;
    [Min(0f), Tooltip("The projectile will be destroyed after this period of time from the moment of spawn [seconds]")]
    public float lifeTime = 5f;
}
