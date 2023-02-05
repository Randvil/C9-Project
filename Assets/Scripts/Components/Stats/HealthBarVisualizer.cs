using UnityEngine;
using UnityEngine.UI;

public class HealthBarVisualizer : MonoBehaviour, IUIComponent
{
    [SerializeField]
    private Slider healthBarSlider;

    private IStats stats;

    void Start()
    {
        stats = GetComponent<IStats>();

        stats.ChangeStatEvent.AddListener(OnHealthChange);

        healthBarSlider.minValue = 0f;
        healthBarSlider.maxValue = stats.GetStat(eStatType.MaxHealth);
        healthBarSlider.value = stats.GetStat(eStatType.CurrentHealth);
    }

    public void OnHealthChange(eStatType stat, float value)
    {
        switch (stat)
        {
            case eStatType.CurrentHealth:
                healthBarSlider.value = value;
                if (value == 0f)
                    OnDie();                   
                break;

            case eStatType.MaxHealth:
                healthBarSlider.maxValue = value;
                break;
        }
    }

    private void OnDie()
    {
        healthBarSlider.gameObject.SetActive(false);
    }
}
