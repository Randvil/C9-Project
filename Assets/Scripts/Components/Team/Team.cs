using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTeam : ITeam
{
    private eTeam team;
    public eTeam Team => team;

    public CharacterTeam(eTeam initialTeam)
    {
        team = initialTeam;
    }

    public void ChangeTeam(eTeam newTeam)
    {
        team = newTeam;
    }

    public bool IsSame(ITeam characterTeam)
    {
        return characterTeam.Team == team;
    }

    public bool IsSame(Component character)
    {
        if (character.TryGetComponent(out ITeamMember teamMemeber) == false)
        {
            return false;
        }

        return IsSame(teamMemeber.CharacterTeam);
    }

    public bool IsSame(GameObject character)
    {
        if (character.TryGetComponent(out ITeamMember teamMemeber) == false)
        {
            return false;
        }

        return IsSame(teamMemeber.CharacterTeam);
    }
}
