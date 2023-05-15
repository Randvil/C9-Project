using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class LoadScreen : MonoBehaviour, IPanel
{
    private PanelManager panelManager;

    [SerializeField] private int indOfThisPanel;

    [SerializeField] bool needToStartWith = true;

    private Label logo;

    const string hiddenRightClass = "hidden-right";
    const string hiddenLeftClass = "hidden-left";

    public void SetInput(PlayerInput input) { }

    void Awake()
    {
        panelManager = GetComponentInParent<PanelManager>();
        logo = GetComponent<UIDocument>().rootVisualElement.Q<Label>("logo");

        if (needToStartWith)
            BeginOfScene();
    }
    
    IEnumerator Start()
    {      
        // Если вообще не ждать, то будет дёргано
        yield return new WaitForSecondsRealtime(panelManager.PanelTweenDuration);

        logo.AddToClassList(hiddenRightClass);

        if (needToStartWith)
            panelManager.SwitchTo(0, true, true); // To HUD
    }

    public void BeginOfScene()
    {
        panelManager.SwitchTo(indOfThisPanel, false, false); // To this load screen      
    }

    public void EndOfScene()
    {
        logo.RemoveFromClassList(hiddenRightClass);
        logo.AddToClassList(hiddenLeftClass); 
        panelManager.SwitchTo(indOfThisPanel);
              
        logo.RemoveFromClassList(hiddenLeftClass);
    }
}
