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
}
