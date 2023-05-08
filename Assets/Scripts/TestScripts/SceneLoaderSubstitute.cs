using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SceneLoaderSubstitute : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerInput unityPlayerInput;
    [SerializeField] UIDocument playerUI;
    [SerializeField] PanelManager panelManager;
    [SerializeField] DeathScreen deathScreen;
    [SerializeField] AbilityUiDependencies abilityUiDependencies;

    private void Awake()
    {
        player.Initialize(unityPlayerInput);
        player.Document = playerUI;

        panelManager.Input = unityPlayerInput;
        panelManager.Abilities = player.AbilityManager;
        deathScreen.SetDeathManager(player.DeathManager);
        abilityUiDependencies.Parry = player.Parry;

        DeathLoad deathLoad = new DeathLoad(player.DeathManager);
    }
}
