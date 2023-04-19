using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SillyPatrolman : IEnemyBehavior
{
    private GameObject character;
    private Transform checkPlatformAheadTransform;

    private SillyPatrolmanData sillyPatrolmanData;

    private BoxCollider2D collider;
    private ITurning turning;
    private IDamageHandler damageHandler;

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

    public UnityEvent<eDirection> DirectionalMoveEvent { get; } = new();
    public UnityEvent<eDirection> TurnEvent { get; } = new();
    public UnityEvent<Vector2> FreeMoveEvent { get; } = new();
    public UnityEvent StopEvent { get; } = new();
    public UnityEvent<Vector2> JumpEvent { get; } = new();
    public UnityEvent AttackEvent { get; } = new();
    public UnityEvent<eAbilityType> AbilityEvent { get; } = new();

    public SillyPatrolman(GameObject character, Transform checkPlatformAheadTransform, SillyPatrolmanData sillyPatrolmanData, BoxCollider2D collider, ITurning turning, IDamageHandler damageHandler)
    {
        this.character = character;
        this.checkPlatformAheadTransform = checkPlatformAheadTransform;
        this.sillyPatrolmanData = sillyPatrolmanData;
        this.collider = collider;
        this.turning = turning;
        this.damageHandler = damageHandler;

        damageHandler.TakeDamageEvent.AddListener(OnTakeDamage);
    }

    public void Activate()
    {
        senseCoroutine = Coroutines.StartCoroutine(SenseCoroutine());
        thinkCoroutine = Coroutines.StartCoroutine(ThinkCoroutine());
        actCoroutine = Coroutines.StartCoroutine(ActCoroutine());
    }

    public void Deactivate()
    {
        Coroutines.StopCoroutine(ref senseCoroutine);
        Coroutines.StopCoroutine(ref thinkCoroutine);
        Coroutines.StopCoroutine(ref actCoroutine);
    }

    private IEnumerator SenseCoroutine()
    {
        while (true)
        {
            SearchPlayer();

            if (player != null && Vector2.Distance(player.transform.position, character.transform.position) > sillyPatrolmanData.loseSightOfPlayerDistance)
            {
                player = null;
            }

            CheckPlatformAhead();
            CheckWallAhead();

            yield return new WaitForSeconds(sillyPatrolmanData.senseDelay);
        }
    }

    private IEnumerator ThinkCoroutine()
    {
        while (true)
        {
            if (platformIsAhead == false || wallIsAhead == true)
            {
                haveToTurn = true;
            }

            if (player != null)
            {
                if (player.transform.position.x - character.transform.position.x > 0f)
                {
                    playerDirection = eDirection.Right;
                }
                else
                {
                    playerDirection = eDirection.Left;
                }

                if (Vector2.Distance(player.transform.position, character.transform.position) < sillyPatrolmanData.attackRadius)
                {
                    state = eState.Attacking;
                }
                else
                {
                    state = eState.Chasing;
                }

            }
            else
            {
                state = eState.Patrolling;
            }                

            yield return new WaitForSeconds(sillyPatrolmanData.thinkDelay);
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

            yield return new WaitForSeconds(sillyPatrolmanData.actDelay);
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
        RaycastHit2D hitInfo = Physics2D.Raycast(character.transform.position, viewDirection, sillyPatrolmanData.searchPlayerDistance, sillyPatrolmanData.enemyLayerMask);
        if (hitInfo.collider != null)
        {
            player = hitInfo.collider.gameObject;
            return true;
        }

        return false;
    }

    private bool CheckPlatformAhead()
    {
        platformIsAhead = Physics2D.OverlapCircle(checkPlatformAheadTransform.position, sillyPatrolmanData.checkPlatformAheadRadius, sillyPatrolmanData.platformLayerMask) != null;

        return platformIsAhead;
    }

    private bool CheckWallAhead()
    {
        float offset = (collider.size.x * collider.transform.lossyScale.x + sillyPatrolmanData.checkWallDistance) / 2f;
        if (turning.Direction == eDirection.Left)
        {
            offset = -offset;
        }

        Vector2 boxOrigin = new(character.transform.position.x + offset, character.transform.position.y);

        Vector2 boxSize = new(sillyPatrolmanData.checkWallDistance / 2f, collider.size.y * collider.transform.lossyScale.y * 0.9f);

        wallIsAhead = Physics2D.OverlapBox(boxOrigin, boxSize, 0f, sillyPatrolmanData.platformLayerMask) != null;

        return wallIsAhead;
    }

    private void OnTakeDamage(DamageInfo damageInfo)
    {
        player = damageInfo.damageOwnerObject;
    }
}