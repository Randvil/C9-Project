using UnityEngine;
using UnityEngine.UI;

public class EnemyInterface : MonoBehaviour, IUIComponent
{
    public Slider healthBar;

    //����� ������ ��� � ������ ������������ ����������
    void Awake()
    {
        healthBar.value = healthBar.maxValue = GetComponent<NewDamageInteraction>().maxHealth;
    }

    public void OnDamageTaken(float damage)
    {
        if (healthBar.value > damage)
            healthBar.value -= damage;
        else
            healthBar.value = 0;
    }
}
