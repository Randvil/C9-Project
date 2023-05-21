using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DeathScreen : MonoBehaviour, IPanel
{
    private IDeathManager deathManager;
    public void SetDeathManager(IDeathManager manager) => deathManager = manager;

    private PanelManager panelManager;

    [SerializeField] private LoadScreen loadScreen;

    private void Start()
    {
        panelManager = GetComponentInParent<PanelManager>();

        deathManager.DeathEvent.AddListener(OnDeath);

        panelManager.panels[3].Q<Button>("retryB").clicked += TryAgainClicked;
    }

    private void OnDeath()
    {
        StaticAudio.Instance.PlayEffect(eAudioEffect.Death);
        StaticAudio.Instance.SnapshotName = "Pause";

        panelManager.SwitchTo(3);

        //There's no need to switch back because DeathLoad reloads the scene)
    }

    private void TryAgainClicked()
    {
        loadScreen.EndOfScene();

        DeathLoad deathLoad = new();
        deathLoad.RewriteData();   
        StartCoroutine(ReloadSceneCoroutine(deathLoad));
    }

    private IEnumerator ReloadSceneCoroutine(DeathLoad deathLoad)
    {
        yield return new WaitForSecondsRealtime(panelManager.PanelTweenDuration);
        StaticAudio.Instance.SnapshotName = "InGame";
        deathLoad.LoadCheckpoint();
    }

    public void SetInput(PlayerInput input)
    {

    }
}
