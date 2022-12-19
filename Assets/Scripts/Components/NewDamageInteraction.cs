using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewDamageInteraction : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    public UnityEvent DieEvent = new();

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            DieEvent.Invoke();
        }

        healthBar.value = currentHealth;
    }
}
