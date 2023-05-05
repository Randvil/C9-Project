using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUD : MonoBehaviour, IPanel
{
    public PanelManager panelManager;

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

                    panelManager.SwitchTo(1);

                    StaticAudio.Instance.SnapshotName = "Pause";
                    break;

                case "Collection":
                    input.SwitchCurrentActionMap("UI");

                    DOTween.To(t => Time.timeScale = t, 1f, 0f, panelManager.PanelTweenDuration).SetUpdate(true);

                    panelManager.SwitchTo(2);

                    StaticAudio.Instance.SnapshotName = "Pause";
                    break;
            }
        };
    }
}
