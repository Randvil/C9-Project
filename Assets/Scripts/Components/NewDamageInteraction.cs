using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewDamageInteraction : MonoBehaviour
{
    public float maxHealth;

    private float currentHealth;

    public UnityEvent DieEvent = new();

    public UnityEvent<float> DamageEvent = new();

    private void Start()
    {
        currentHealth = maxHealth;

        DamageEvent.AddListener(TakeDamage);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            DieEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
