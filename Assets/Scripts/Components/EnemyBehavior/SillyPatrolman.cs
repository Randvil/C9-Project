using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SillyPatrolman : MonoBehaviour, IEnemyBehavior
{
    [SerializeField]
    private float senseDelay;

    [SerializeField]
    private float thinkDelay;

    [SerializeField]
    private float actDelay;

    [SerializeField]
    private float searchPlayerDistance;

    [SerializeField]
    private float loseSightOfPlayerDistance;

    [SerializeField]
    private Transform checkPlatformAheadTransform;

    [SerializeField]
    private float checkPlatformAheadRadius;

    [SerializeField]
    private float checkWallDistance;

    [SerializeField]
    private float attackerSearchRadius;

    [SerializeField]
    private float attackRadius;

    [SerializeField]
    private LayerMask enemyLayerMask;

    [SerializeField]
    private LayerMask platformLayerMask;

    public enum eState
    {
        Patrolling,
        Chasing,
        Attacking
    }

    private Coroutine senseCoroutine;
    private Coroutine thinkCoroutine;
    private Coroutine actCoroutine;
    private eState state = eState.Patrolling;
    private GameObject player;
    private eDirection playerDirection;
    private bool platformIsAhead;
    private bool wallIsAhead;
    private bool haveToTurn = false;

    private new BoxCollider2D collider;
    private ITurning turning;
    private IDamageHandler damageHandler;

    public UnityEvent<eDirection> DirectionalMoveEvent { get; } = new();
    public UnityEvent<eDirection> TurnEvent { get; } = new();
    public UnityEvent<Vector2> FreeMoveEvent { get; } = new();
    public UnityEvent StopEvent { get; } = new();
    public UnityEvent<Vector2> JumpEvent { get; } = new();
    public UnityEvent AttackEvent { get; } = new();
    public UnityEvent<eAbilityType> AbilityEvent { get; } = new();

    public void Activate()
    {
        senseCoroutine = StartCoroutine(SenseCoroutine());
        thinkCoroutine = StartCoroutine(ThinkCoroutine());
        actCoroutine = StartCoroutine(ActCoroutine());
    }

    public void Deactivate()
    {
        StopCoroutine(senseCoroutine);
        StopCoroutine(thinkCoroutine);
        StopCoroutine(actCoroutine);
    }

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        turning = GetComponent<ITurning>();
        damageHandler = GetComponent<IDamageHandler>();

        damageHandler.TakeDamageEvent.AddListener(OnTakeDamage);
    }

    private IEnumerator SenseCoroutine()
    {
        while (true)
        {
            SearchPlayer();

            if (player != null && Vector2.Distance(player.transform.position, transform.position) > loseSightOfPlayerDistance)
            {
                player = null;
            }

            CheckPlatformAhead();
            CheckWallAhead();

            yield return new WaitForSeconds(senseDelay);
        }
    }

    private IEnumerator ThinkCoroutine()
    {
        while (true)
        {
            if (!platformIsAhead || wallIsAhead)
            {
                haveToTurn = true;
            }

            if (player != null)
            {
                if (player.transform.position.x - transform.position.x > 0f)
                    playerDirection = eDirection.Right;
                else
                    playerDirection = eDirection.Left;

                if (Vector2.Distance(player.transform.position, transform.position) < attackRadius)
                    state = eState.Attacking;
                else
                    state = eState.Chasing;
            }
            else
                state = eState.Patrolling;

            yield return new WaitForSeconds(thinkDelay);
        }
    }

    private IEnumerator ActCoroutine()
    {
        while (true)
        {
            switch (state)
            {
                case eState.Patrolling:

                    if (haveToTurn)
                    {
                        ChangeDirection();
                        haveToTurn = false;
                    }
                    else
                    {
                        DirectionalMoveEvent.Invoke(turning.Direction);
                    }

                    break;

                case eState.Chasing:

                    DirectionalMoveEvent.Invoke(playerDirection);

                    break;

                case eState.Attacking:

                    StopEvent.Invoke();
                    TurnEvent.Invoke(playerDirection);

                    AttackEvent.Invoke();

                    break;
            }

            yield return new WaitForSeconds(actDelay);
        }
    }

    private void ChangeDirection()
    {
        switch (turning.Direction)
        {
            case eDirection.Right:
                DirectionalMoveEvent.Invoke(eDirection.Left);
                break;

            case eDirection.Left:
                DirectionalMoveEvent.Invoke(eDirection.Right);
                break;
        }
    }

    private bool SearchPlayer()
    {
        Vector2 viewDirection = turning.Direction == eDirection.Right ? Vector2.right : Vector2.left;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, viewDirection, searchPlayerDistance, enemyLayerMask);
        if (hitInfo.collider != null)
        {
            player = hitInfo.collider.gameObject;
            return true;
        }

        return false;
    }

    private bool CheckPlatformAhead()
    {
        platformIsAhead = Physics2D.OverlapCircle(checkPlatformAheadTransform.position, checkPlatformAheadRadius, platformLayerMask) != null;

        return platformIsAhead;
    }

    private bool CheckWallAhead()
    {
        float offset = (collider.size.x + checkWallDistance) / 2f;
        if (turning.Direction == eDirection.Left)
            offset = -offset;
        Vector2 boxOrigin = new(transform.position.x + offset, transform.position.y);

        Vector2 boxSize = new(checkWallDistance / 2f, collider.size.y * 0.9f);

        wallIsAhead = Physics2D.OverlapBox(boxOrigin, boxSize, 0f, platformLayerMask) != null;

        return wallIsAhead;
    }

    private void OnTakeDamage(float incomingDamage)
    {
        if (player != null)
            return;

        Collider2D attacker = Physics2D.OverlapCircle(transform.position, attackRadius, enemyLayerMask);
        if (attacker != null)
            player = attacker.gameObject;
    }
}