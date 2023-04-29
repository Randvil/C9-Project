using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : Climb, IPlayerClimb
{
    private float fallProhibitionPeriod;
    private float searchPeriod;
    private float searchRadius;
    private LayerMask climbableObjectLayer;

    private Coroutine searchCoroutine;
    private IClimbableObject climbableObject;

    public bool CanClimb => climbableObject != null;
    public bool HaveToTurn { get; private set; }

    public PlayerClimb(MonoBehaviour owner, ClimbData climbData, Rigidbody2D rigidbody, IGravity gravity, ITurning turning) : base(owner, climbData, rigidbody, gravity, turning)
    {
        fallProhibitionPeriod = climbData.fallProhibitionPeriod;
        searchPeriod = climbData.searchPeriod;
        searchRadius = climbData.searchRadius;
        climbableObjectLayer = climbData.climbableObjectLayer;

        this.rigidbody = rigidbody;
        this.gravity = gravity;
        this.turning = turning;

        searchCoroutine = owner.StartCoroutine(SearchClimbableObject());
    }

    private IEnumerator SearchClimbableObject()
    {
        yield return new WaitForSeconds(fallProhibitionPeriod);

        while (true)
        {
            yield return new WaitForSeconds(searchPeriod);

            if (rigidbody == null)
            {
                owner.StopCoroutine(searchCoroutine);
                break;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(rigidbody.position, searchRadius, climbableObjectLayer);
            if (colliders.Length == 0)
            {
                IsClimbing = false;
                continue;
            }

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out climbableObject) == true)
                {
                    IsClimbing = true;
                    break;
                }
            }
        }
    }

    public override void StartClimb()
    {
        gravity.Disable(this);
        rigidbody.MovePosition(new(climbableObject.XPosition, rigidbody.position.y));
        rigidbody.velocity = new(0f, 0f);

        HaveToTurn = false;
        if (turning.Direction != climbableObject.PlayerDirection)
        {
            HaveToTurn = true;
        }

        StartClimbEvent.Invoke();
    }

    public override void BreakClimb()
    {
        IsClimbing = false;
        gravity.Enable(this);
        rigidbody.velocity = new(0f, 0f);
        climbableObject = null;

        owner.StopCoroutine(searchCoroutine);
        searchCoroutine = owner.StartCoroutine(SearchClimbableObject());

        BreakClimbEvent.Invoke();
    }
}
