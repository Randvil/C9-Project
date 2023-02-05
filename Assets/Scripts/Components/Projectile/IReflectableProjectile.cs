using UnityEngine;

public interface IReflectableProjectile : IProjectile
{
    public GameObject CreateReflectedProjectile(eTeam newTeam);
}
