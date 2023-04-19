using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManagersCreator : Creator
{
    public PlayerInput PlayerInput { get => newGameObject.GetComponent<PlayerInput>(); }

}
