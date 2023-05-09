using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadScreen : MonoBehaviour, IPanel
{
    private PanelManager panelManager;

    public void SetInput(PlayerInput input) { }

    void Awake()
    {
        panelManager = GetComponentInParent<PanelManager>();

        panelManager.SwitchTo(4, false, false); // To this load screen
    }

    IEnumerator Start()
    {
        // Если вообще не ждать, то будет дёргано
        yield return new WaitForSecondsRealtime(panelManager.PanelTweenDuration); 

        panelManager.SwitchTo(0, true, true); // To HUD
    }

    public void EndOfScene()
    {
        panelManager.SwitchTo(4);
        // mb some code for cool animation
    }
}
