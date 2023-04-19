using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour, IPanel
{
    public PanelManager panelManager;

    public AudioMixer audioMixer;

    private PlayerInput input;

    public void SetInput(PlayerInput _input)
    {
        input = _input;
        input.onActionTriggered += context =>
        {
            switch (context.action.name)
            {
                case "Pause":
                    input.SwitchCurrentActionMap("UI");

                    //Плавно замедляем время до полной остановки за время анимации смены панелей
                    DOTween.To(t => Time.timeScale = t, 1f, 0f, panelManager.PanelTweenDuration).SetUpdate(true);

                    audioMixer.FindSnapshot("Pause").TransitionTo(1f);

                    panelManager.SwitchTo(1);
                    break;

                case "Collection":
                    input.SwitchCurrentActionMap("UI");

                    DOTween.To(t => Time.timeScale = t, 1f, 0f, panelManager.PanelTweenDuration).SetUpdate(true);

                    audioMixer.FindSnapshot("Pause").TransitionTo(1f);

                    panelManager.SwitchTo(2);
                    break;

                case "ChangeAbilityLayout":
                    ChangeLayout();
                    break;
            }
        };
    }

    private void ChangeLayout()
    {

    }
}
