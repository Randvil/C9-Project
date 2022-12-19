using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public LayerMask enemyLayerMask;
    public float damage;

    private void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f), Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == enemyLayerMask.value)
        {
            NewDamageInteraction interactableEnemy = other.GetComponent<NewDamageInteraction>();
            if (interactableEnemy != null)
            {
                interactableEnemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
