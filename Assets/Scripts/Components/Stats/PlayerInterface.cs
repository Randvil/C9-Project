using DG.Tweening;
using NS.RomanLib;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInterface : MonoBehaviour, IUIComponent
{
    public UIDocument uiDocument;

    private ProgressBar healthBar;
    private Label hpLabel;
    private RadialFill manaBar;

    [SerializeField] private float tweenDuration = 0.4f;

    private IStats stats;

    private void Start()
    {
        VisualElement root = uiDocument.rootVisualElement;

        stats = GetComponent<Stats>();
        stats.ChangeStatEvent.AddListener(OnHealthChange);

        healthBar = root.Q<ProgressBar>("healthBar");
        hpLabel = root.Q<Label>("hpLabel");
        manaBar = root.Q<RadialFill>("manaBar");

        healthBar.value = healthBar.highValue = stats.GetStat(eStatType.MaxHealth);
        hpLabel.text = healthBar.value / healthBar.highValue * 100f + "%";
        manaBar.value = 1f;
    }

    public void OnHealthChange(eStatType stat, float value)
    {
        if (stat == eStatType.CurrentHealth)
        {
            DOTween.To(x => healthBar.value = Mathf.Clamp(x, 0f, healthBar.highValue), healthBar.value, value, tweenDuration);          
            DOTween.To(x => hpLabel.text = Mathf.Round(x / healthBar.highValue * 100f) + "%", healthBar.value, value, tweenDuration);
        }
    }
}
