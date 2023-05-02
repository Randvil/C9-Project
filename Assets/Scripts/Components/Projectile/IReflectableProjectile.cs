using UnityEngine;

public interface IReflectableProjectile : IProjectile
{
    public void CreateReflectedProjectile(ITeam newTeam);
}
