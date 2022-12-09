using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected float turnSpeed;

    [SerializeField]
    protected GameObject model;

    [SerializeField]
    protected float jumpSpeed;

    [SerializeField]
    protected Transform bottomEdge;

    [SerializeField]
    protected LayerMask groundLayerMask;

    [SerializeField]
    protected float checkGroundRadius;

    [SerializeField]
    protected GameObject weapon;

    [SerializeField]
    protected LayerMask enemyLayerMask;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float attackRadius;

    [SerializeField]
    protected float attackDelay;

    [SerializeField]
    protected float attackCooldown;

    protected enum eAttackType
    {
        Single,
        Splash
    }

    [SerializeField]
    protected eAttackType attackType;

    [SerializeField]
    protected int health;

    [SerializeField]
    protected int maxHealth;

    [SerializeField]
    protected Slider healthBarSlider;


    protected float direction;
    protected bool isJumping;
    protected Coroutine turnCoroutine;
    protected Coroutine attackCoroutine;

    protected new Rigidbody rigidbody;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();


        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected IEnumerator AttackCoroutine(float damage = 0f)
    {
        weapon.SetActive(true);

        yield return new WaitForSeconds(attackDelay);

        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRadius, enemyLayerMask);

        switch (attackType)
        {


            case eAttackType.Single:

                Collider nearestEnemy = null;
                float distanceToNearestEnemy = float.MaxValue;

                foreach (Collider enemy in enemies)
                {
                    if ((direction == 0 && enemy.transform.position.x >= transform.position.x) || (direction == 180 && enemy.transform.position.x <= transform.position.x))
                    {
                        if (Mathf.Abs(enemy.transform.position.x - transform.position.x) < distanceToNearestEnemy) nearestEnemy = enemy;
                    }
                }

                if (nearestEnemy != null) DealDamage(nearestEnemy, damage);

                break;

            case eAttackType.Splash:

                foreach (Collider enemy in enemies)
                {
                    if ((direction == 0 && enemy.transform.position.x >= transform.position.x) || (direction == 180 && enemy.transform.position.x <= transform.position.x)) DealDamage(enemy, damage);
                }

                break;
        }

        yield return new WaitForSeconds(attackCooldown - attackDelay);

        weapon.SetActive(false);

        attackCoroutine = null;
    }

    protected void TakeDamage(int damage)
    {
        health -= damage;
        healthBarSlider.value = health;

        if (health <= 0)
            Die();
    }

    protected void Die()
    {
        //TODO: ƒобавить запуск анимации смерти, когда та будет готова
        GetComponent<Collider>().enabled = false;

        //TODO: ѕосле внедрени€ анимации, убрать это, чтоб труп оставалс€ лежать
        Destroy(gameObject);
    }

    protected void DealDamage(Collider enemy, float damage)
    {
        //TODO: ¬ыбрать систему хп между целыми и нецелыми числами
        enemy.GetComponent<Entity>().TakeDamage((int)damage);
    }

    protected IEnumerator TurnCoroutine(float direction)
    {
        float remainingAngle = direction - model.transform.rotation.eulerAngles.y;
        while ((direction > 0) ? (remainingAngle > 0f) : (remainingAngle < 0f))
        {
            float deltaAngle = (direction > 0) ? (turnSpeed * Time.deltaTime) : -(turnSpeed * Time.deltaTime);
            //rigidbody.MoveRotation(Quaternion.AngleAxis(transform.rotation.eulerAngles.y + deltaAngle, Vector3.up));
            model.transform.Rotate(new(0f, deltaAngle, 0f));
            remainingAngle -= deltaAngle;
            yield return null;
        }
        //rigidbody.MoveRotation(Quaternion.AngleAxis(direction, Vector3.up));
        model.transform.eulerAngles = new(0f, direction, 0f);
    }
}
