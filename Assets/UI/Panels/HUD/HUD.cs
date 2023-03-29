using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class HUD : MonoBehaviour
{
    public PanelManager panelManager;

    public AudioMixer audioMixer;

    [SerializeField] private PlayerInput input;

    private void Awake()
    {
        input.onActionTriggered += context =>
        {
            switch (context.action.name)
            {
                case "Pause":
                    input.SwitchCurrentActionMap("Menu");

                    //Плавно замедляем время до полной остановки за время анимации смены панелей
                    DOTween.To(t => Time.timeScale = t, 1f, 0f, panelManager.PanelTweenDuration).SetUpdate(true);

                    audioMixer.FindSnapshot("Pause").TransitionTo(1f);

                    panelManager.SwitchTo(1);
                    break;

                case "Collection":
                    input.SwitchCurrentActionMap("Menu");

                    DOTween.To(t => Time.timeScale = t, 1f, 0f, panelManager.PanelTweenDuration).SetUpdate(true);

                    audioMixer.FindSnapshot("Pause").TransitionTo(1f);

                    panelManager.SwitchTo(2);
                    break;
            }
        };
    }
}
