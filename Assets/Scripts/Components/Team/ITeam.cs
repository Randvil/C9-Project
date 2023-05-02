using UnityEngine;

public interface ITeam
{
    public void ChangeTeam(eTeam newTeam);
    public bool IsSame(ITeam characterTeam);
    public bool IsSame(Component character);
    public bool IsSame(GameObject character);
    public eTeam Team { get; }
}
