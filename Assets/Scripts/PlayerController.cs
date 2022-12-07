using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private float tresholdAngle;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private Transform bottomEdge;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private float checkGroundRadius;

    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private LayerMask enemyLayerMask;

    [SerializeField]
    private float attackRadius;

    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float attackCooldown;


    private enum eAttackType
    {
        Single,
        Splash
    }

    [SerializeField]
    private eAttackType attackType;

    private float direction;
    private bool isJumping;
    private Coroutine turnCoroutine;
    private Coroutine attackCoroutine;

    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // moving
        float deltaX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rigidbody.velocity = new Vector3(deltaX, rigidbody.velocity.y, 0f);

        // turning
        if (deltaX > 0f && direction != 0f)
        {
            direction = 0f;
            if (turnCoroutine != null) StopCoroutine(turnCoroutine);
            turnCoroutine = StartCoroutine(TurnCoroutine(direction));
        }
        else if (deltaX < 0f && direction != 180f)
        {
            direction = 180;
            if (turnCoroutine != null) StopCoroutine(turnCoroutine);
            turnCoroutine = StartCoroutine(TurnCoroutine(direction));
        }

        // jumping
        isJumping = !Physics.CheckSphere(bottomEdge.position, checkGroundRadius, groundLayerMask);
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) rigidbody.AddForce(new Vector3(0f, jumpSpeed, 0f), ForceMode.VelocityChange);

        // attack
        if (Input.GetMouseButtonDown(0) && attackCoroutine == null) attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator TurnCoroutine(float direction)
    {
        float remainingAngle = direction - transform.rotation.eulerAngles.y;
        while ((direction > 0) ? (remainingAngle > tresholdAngle) : (remainingAngle < -tresholdAngle))
        {
            float deltaAngle = (direction > 0) ? (turnSpeed * Time.deltaTime) : -(turnSpeed * Time.deltaTime);
            //rigidbody.MoveRotation(Quaternion.AngleAxis(transform.rotation.eulerAngles.y + deltaAngle, Vector3.up));
            transform.Rotate(new(0f, deltaAngle, 0f));
            remainingAngle -= deltaAngle;
            yield return null;
        }
        //rigidbody.MoveRotation(Quaternion.AngleAxis(direction, Vector3.up));
        transform.eulerAngles = new(0f, direction, 0f);
    }

    private IEnumerator AttackCoroutine(float damage = 0f)
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

    private void DealDamage(Collider enemy, float damage)
    {
        Destroy(enemy.gameObject);
    }
}
