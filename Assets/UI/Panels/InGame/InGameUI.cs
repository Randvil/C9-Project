using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public PanelManager panelManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;

            panelManager.SwitchTo(1);          
        }
    }
}
