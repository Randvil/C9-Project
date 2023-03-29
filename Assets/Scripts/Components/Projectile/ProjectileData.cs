using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Component Data/Model/New Projectile Data", order = 190)]
public class ProjectileData : ScriptableObject
{
    public GameObject prefab;
    public float speed = 5f;
    public float zRotation = 0f;
    public float lifeTime = 5f;
}
