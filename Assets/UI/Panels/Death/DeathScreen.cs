using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathScreen : MonoBehaviour, IPanel
{
    private IDeathManager deathManager;
    public void SetDeathManager(IDeathManager manager) => deathManager = manager;

    private PanelManager panelManager;

    private void Start()
    {
        panelManager = GetComponentInParent<PanelManager>();

        deathManager.DeathEvent.AddListener(OnDeath);
    }

    private void OnDeath()
    {
        StaticAudio.Instance.PlayEffect(eAudioEffect.Death);

        panelManager.SwitchTo(3);

        //There's no need to switch back because DeathLoad reloads the scene)
    }

    public void SetInput(PlayerInput input) { }
}
