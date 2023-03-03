using UnityEngine;
using UnityEngine.Audio;

public class InGameUI : MonoBehaviour
{
    public PanelManager panelManager;

    public AudioMixer audioMixer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Switch to new input system
        {
            Time.timeScale = 0f;

            audioMixer.FindSnapshot("Pause").TransitionTo(1f);

            panelManager.SwitchTo(1);
        }
    }
}
