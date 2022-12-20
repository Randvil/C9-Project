using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAttack : MonoBehaviour
{
    private enum eAttackType
    {
        Single,
        Splash,
        Range
    }

    [SerializeField]
    private eAttackType attackType;

    [SerializeField]
    private GameObject weaponGameObject;

    [SerializeField]
    private LayerMask enemyLayerMask;

    [SerializeField]
    private float attackDamage;

    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private float attackRadius;

    [SerializeField]
    private GameObject projectilePrefab;


    private Coroutine attackCoroutine;


    public void StartAttack(eDirection direction)
    {
        if (attackCoroutine != null) return;
        attackCoroutine = StartCoroutine(AttackCoroutine(direction));
    }

    private IEnumerator AttackCoroutine(eDirection direction)
    {
        weaponGameObject.SetActive(true);

        yield return new WaitForSeconds(attackDelay);

        Collider[] enemies;
        NewDamageInteraction interactableEnemy;

        switch (attackType)
        {
            case eAttackType.Single:

                enemies = Physics.OverlapSphere(transform.position, attackRadius, enemyLayerMask);

                NewDamageInteraction nearestEnemy = null;
                float distanceToNearestEnemy = float.MaxValue;

                foreach (Collider enemy in enemies)
                {
                    interactableEnemy = enemy.GetComponent<NewDamageInteraction>();
                    if (interactableEnemy == null) continue;
                    if ((direction == eDirection.Right && interactableEnemy.transform.position.x >= transform.position.x) || (direction == eDirection.Left && interactableEnemy.transform.position.x <= transform.position.x))
                    {
                        if (Mathf.Abs(interactableEnemy.transform.position.x - transform.position.x) < distanceToNearestEnemy) nearestEnemy = interactableEnemy;
                    }
                }

                if (nearestEnemy != null) nearestEnemy.TakeDamage(attackDamage);

                break;

            case eAttackType.Splash:

                enemies = Physics.OverlapSphere(transform.position, attackRadius, enemyLayerMask);

                foreach (Collider enemy in enemies)
                {
                    interactableEnemy = enemy.GetComponent<NewDamageInteraction>();
                    if (interactableEnemy == null) continue;
                    if ((direction == eDirection.Right && interactableEnemy.transform.position.x >= transform.position.x) || (direction == eDirection.Left && enemy.transform.position.x <= transform.position.x)) interactableEnemy.TakeDamage(attackDamage);
                }

                break;

            case eAttackType.Range:

                NewProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0f, (float)direction, 0f))).GetComponent<NewProjectile>();
                projectile.damage = attackDamage;
                projectile.enemyLayerMask = enemyLayerMask;

                break;
        }

        yield return new WaitForSeconds(attackCooldown - attackDelay);

        weaponGameObject.SetActive(false);

        attackCoroutine = null;
    }
}
