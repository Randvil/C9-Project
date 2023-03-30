using UnityEngine;

public interface IReflectableProjectile : IProjectile
{
    public GameObject CreateReflectedProjectile(ITeam newTeam);
}
